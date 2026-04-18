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

echo "Waiting for API startup"
sleep 15

echo "Checking health endpoint"
curl --fail --show-error --silent "http://127.0.0.1:${HOST_PORT}/healthz" >/dev/null

echo "Smoke test passed"
