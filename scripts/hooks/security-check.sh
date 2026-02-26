#!/bin/bash
set -e

INPUT=$(cat)
TOOL_NAME=$(echo "$INPUT" | jq -r '.toolName // ""')

# Check for dangerous bash commands
if [ "$TOOL_NAME" = "bash" ] || [ "$TOOL_NAME" = "terminal" ]; then
  COMMAND=$(echo "$INPUT" | jq -r '.toolInput.command // ""')

  if echo "$COMMAND" | grep -qEi 'rm\s+-[rf]{1,2}\s*/|DROP\s+TABLE|format\s+|sudo\s+rm'; then
    echo '{"permissionDecision":"deny","reason":"Blocked dangerous command: potential destructive operation detected"}'
    exit 0
  fi
fi

# Check for edits outside allowed directories
if [ "$TOOL_NAME" = "edit" ] || [ "$TOOL_NAME" = "create" ]; then
  FILE_PATH=$(echo "$INPUT" | jq -r '.toolInput.path // .toolInput.file // ""')

  if [ -n "$FILE_PATH" ]; then
    if ! echo "$FILE_PATH" | grep -qE '(^|\/)src\/|(^|\/)test\/'; then
      echo '{"permissionDecision":"deny","reason":"Blocked edit: file is outside allowed src/ and test/ directories"}'
      exit 0
    fi
  fi
fi

# Allow by default
echo '{"permissionDecision":"allow"}'
