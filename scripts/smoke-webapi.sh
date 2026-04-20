#!/usr/bin/env bash

set -euo pipefail

IMAGE_TAG="${1:-devarchitecture-webapi:local}"
HOST_PORT="${2:-18080}"
CONTAINER_NAME="devarchitecture-webapi-smoke"

echo "Building image: ${IMAGE_TAG}"
docker build -t "${IMAGE_TAG}" -f WebAPI/Dockerfile .

echo "Starting container on host port ${HOST_PORT}"
docker run -d --rm --name "${CONTAINER_NAME}" -p "${HOST_PORT}:8080" "${IMAGE_TAG}" >/dev/null

cleanup() {
  echo "Stopping smoke container"
  docker stop "${CONTAINER_NAME}" >/dev/null || true
}
trap cleanup EXIT

echo "Waiting for /healthz (up to 45s)"
ok=0
for _ in $(seq 1 45); do
  if curl --fail --show-error --silent "http://127.0.0.1:${HOST_PORT}/healthz" >/dev/null; then
    ok=1
    break
  fi
  sleep 1
done
if [ "$ok" != "1" ]; then
  echo "Smoke test failed; container logs:"
  docker logs "${CONTAINER_NAME}" 2>&1 || true
  exit 1
fi

echo "Smoke test passed"
