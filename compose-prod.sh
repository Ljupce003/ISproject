#!/usr/bin/env bash
set -euo pipefail
script_dir="$(cd -- "$(dirname -- "${BASH_SOURCE[0]}")" && pwd)"
overlay="docker-compose.native.yml"
if [[ "${1:-}" == "--proxy" ]]; then
  overlay="docker-compose.proxy.yml"
  shift
elif [[ "${1:-}" == "--native" ]]; then
  shift
fi
exec docker compose \
  --env-file "${ISPROJECT_ENV_FILE:-$script_dir/.env.production}" \
  -f "$script_dir/docker-compose.prod.yml" \
  -f "$script_dir/$overlay" "$@"
