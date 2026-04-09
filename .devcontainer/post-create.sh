#!/usr/bin/env bash
set -euo pipefail

echo "=== Installing TaskForge Lab Prerequisites ==="

# Copilot CLI
echo "Installing GitHub Copilot CLI..."
npm install -g @github/copilot

# jq (used by hook scripts)
echo "Installing jq..."
sudo apt-get update -qq && sudo apt-get install -y -qq jq

# Restore .NET packages
echo "Restoring .NET packages..."
dotnet restore src/TaskForge/TaskForge.slnx

# Build the solution to verify everything works
echo "Building solution..."
dotnet build src/TaskForge/TaskForge.slnx --no-restore

# Make hook scripts executable
echo "Setting hook scripts as executable..."
chmod +x scripts/hooks/*.sh 2>/dev/null || true

echo ""
echo "=== Setup Complete ==="
echo ""
echo "Verify your tools:"
echo "  dotnet --version          → .NET SDK"
echo "  node --version            → Node.js"
echo "  gh --version              → GitHub CLI"
echo "  copilot --version         → Copilot CLI"
echo "  jq --version              → JSON processor"
echo ""
echo "Next steps:"
echo "  1. Run 'copilot login' to authenticate the Copilot CLI"
echo "  2. Run 'gh auth login' to authenticate the GitHub CLI"
echo "  3. Start with Lab 01: labs/lab01.md"
