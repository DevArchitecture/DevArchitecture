#!/usr/bin/env bash
# One-time setup for an Ubuntu VM (Oracle Always Free Ampere A1, arm64).
# Run from the repository: sudo bash deploy/oracle/setup.sh
set -euo pipefail

cd "$(dirname "$0")"

if [ "$(id -u)" -eq 0 ]; then SUDO=""; else SUDO="sudo"; fi

if ! command -v docker >/dev/null 2>&1; then
  echo "==> Installing Docker"
  curl -fsSL https://get.docker.com | $SUDO sh
fi

DOCKER="docker"
if ! docker info >/dev/null 2>&1; then
  DOCKER="$SUDO docker"
fi

# Oracle Ubuntu images drop almost everything at the iptables level, below ufw.
if command -v iptables >/dev/null 2>&1; then
  for port in 80 443; do
    $SUDO iptables -C INPUT -p tcp --dport "$port" -j ACCEPT 2>/dev/null \
      || $SUDO iptables -I INPUT -p tcp --dport "$port" -j ACCEPT || true
  done
  $SUDO netfilter-persistent save >/dev/null 2>&1 || true
fi
if command -v ufw >/dev/null 2>&1; then
  $SUDO ufw allow 80/tcp >/dev/null 2>&1 || true
  $SUDO ufw allow 443/tcp >/dev/null 2>&1 || true
fi

if [ ! -f .env ]; then
  ip="$(curl -fsS https://api.ipify.org || curl -fsS https://ifconfig.me || echo 127.0.0.1)"
  {
    echo "POSTGRES_DB=devarchitecture"
    echo "POSTGRES_USER=devarch"
    echo "POSTGRES_PASSWORD=$(openssl rand -hex 16)"
    echo "TOKEN_OPTIONS_SECURITY_KEY=$(openssl rand -base64 48)"
    echo "DOMAIN=${DOMAIN:-${ip}.sslip.io}"
    echo "HTTP_PORT=${HTTP_PORT:-80}"
    echo "HTTPS_PORT=${HTTPS_PORT:-443}"
  } > .env
  echo "==> Wrote deploy/oracle/.env"
fi

$DOCKER compose --env-file .env up -d --build

domain="$(grep '^DOMAIN=' .env | cut -d= -f2)"
echo
echo "==> DevArchitecture is starting at https://${domain}/"
echo "    admin@adminmail.com / Q1w212*_*"
echo "    First build takes several minutes: $DOCKER compose logs -f web api"
