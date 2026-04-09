#!/bin/bash
set -e

INPUT=$(cat)
SOURCE=$(echo "$INPUT" | jq -r '.source // "unknown"')
TIMESTAMP=$(date -u +"%Y-%m-%dT%H:%M:%SZ")

mkdir -p logs
echo "[$TIMESTAMP] Session started (source: $SOURCE)" >> logs/session.log
