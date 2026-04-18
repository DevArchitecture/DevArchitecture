#!/usr/bin/env bash
set -euo pipefail

ROOT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"
OUTPUT_DIR="${ROOT_DIR}/clients/contracts"
OUTPUT_FILE="${OUTPUT_DIR}/openapi.json"

mkdir -p "${OUTPUT_DIR}"

dotnet tool restore
dotnet build "${ROOT_DIR}/WebAPI/WebAPI.csproj" -c Release > /dev/null
dotnet swagger tofile --output "${OUTPUT_FILE}" "${ROOT_DIR}/WebAPI/bin/Release/net10.0/WebAPI.dll" v1

echo "OpenAPI exported to ${OUTPUT_FILE}"
