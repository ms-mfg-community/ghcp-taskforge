#!/bin/bash
set -e

INPUT=$(cat)
ERROR_NAME=$(echo "$INPUT" | jq -r '.errorName // "UnknownError"')
ERROR_MESSAGE=$(echo "$INPUT" | jq -r '.errorMessage // "No message"')
TIMESTAMP=$(date -u +"%Y-%m-%dT%H:%M:%SZ")

mkdir -p logs
echo "[$TIMESTAMP] ERROR $ERROR_NAME: $ERROR_MESSAGE" >> logs/errors.log
