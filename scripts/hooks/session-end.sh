#!/bin/bash
set -e

INPUT=$(cat)
REASON=$(echo "$INPUT" | jq -r '.reason // "unknown"')
TIMESTAMP=$(date -u +"%Y-%m-%dT%H:%M:%SZ")

mkdir -p logs
echo "[$TIMESTAMP] Session ended (reason: $REASON)" >> logs/session.log
