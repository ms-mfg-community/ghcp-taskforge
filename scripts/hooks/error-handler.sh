#!/bin/bash
set -e

INPUT=$(cat)
ERROR_MESSAGE=$(echo "$INPUT" | jq -r '.error.message // "No message"')
ERROR_CODE=$(echo "$INPUT" | jq -r '.error.code // "UnknownError"')
TIMESTAMP=$(date -u +"%Y-%m-%dT%H:%M:%SZ")

mkdir -p logs
echo "[$TIMESTAMP] ERROR $ERROR_CODE: $ERROR_MESSAGE" >> logs/errors.log
