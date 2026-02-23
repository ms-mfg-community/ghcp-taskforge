#!/bin/bash
set -e

INPUT=$(cat)
TOOL_NAME=$(echo "$INPUT" | jq -r '.toolName // "unknown"')
RESULT_TYPE=$(echo "$INPUT" | jq -r '.toolResult.type // "unknown"')
TIMESTAMP=$(date -u +"%Y-%m-%dT%H:%M:%SZ")

mkdir -p logs

# Add CSV header if file doesn't exist
if [ ! -f logs/tool-stats.csv ]; then
  echo "timestamp,tool_name,result_type" > logs/tool-stats.csv
fi

echo "$TIMESTAMP,$TOOL_NAME,$RESULT_TYPE" >> logs/tool-stats.csv
