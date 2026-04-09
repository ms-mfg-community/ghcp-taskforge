#!/bin/bash
set -e

INPUT=$(cat)
PROMPT=$(echo "$INPUT" | jq -r '.prompt // "empty"')
TIMESTAMP=$(date -u +"%Y-%m-%dT%H:%M:%SZ")

# Redact potential secrets before logging
PROMPT=$(echo "$PROMPT" | sed -E 's/[A-Za-z0-9_\-]{20,}/[REDACTED]/g')
# Truncate to first 200 chars to limit sensitive data exposure
PROMPT=$(echo "$PROMPT" | cut -c1-200)

mkdir -p logs
echo "[$TIMESTAMP] Prompt: $PROMPT" >> logs/prompts.log
