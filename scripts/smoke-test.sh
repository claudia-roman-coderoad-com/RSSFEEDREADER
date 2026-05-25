#!/usr/bin/env bash
# Simple smoke test for the backend API
set -euo pipefail
API_BASE=${API_BASE:-http://localhost:5151/api}

echo "Creating subscription..."
resp=$(curl -s -o /dev/stderr -w "%{http_code}" -X POST "$API_BASE/subscriptions" -H "Content-Type: application/json" -d '{"url":"https://example.com/feed"}')
if [ "$resp" != "201" ]; then
  echo "POST failed with status $resp"
  exit 2
fi

echo "Listing subscriptions..."
curl -s "$API_BASE/subscriptions" | jq .

echo "Smoke test completed."
