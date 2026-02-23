#!/bin/bash
set -e

INPUT=$(cat)
PROMPT=$(echo "$INPUT" | jq -r '.userPrompt // "empty"')
TIMESTAMP=$(date -u +"%Y-%m-%dT%H:%M:%SZ")

mkdir -p logs
echo "[$TIMESTAMP] Prompt: $PROMPT" >> logs/prompts.log
