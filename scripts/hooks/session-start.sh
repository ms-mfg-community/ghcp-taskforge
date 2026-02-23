#!/bin/bash
set -e

INPUT=$(cat)
SOURCE_TYPE=$(echo "$INPUT" | jq -r '.sourceType // "unknown"')
TIMESTAMP=$(date -u +"%Y-%m-%dT%H:%M:%SZ")

mkdir -p logs
echo "[$TIMESTAMP] Session started (source: $SOURCE_TYPE)" >> logs/session.log
