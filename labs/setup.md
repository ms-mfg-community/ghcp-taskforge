# Lab Setup — Manual Installation

> **Using Codespaces, Docker, or Podman?** If you're using a devcontainer, skip this guide — your environment is already configured. Return to the [main README](../README.md#verify-copilot-cli) to verify your Copilot CLI setup.

This guide is for users who prefer to install tools manually on their local machine.

> **Duration:** 10-15 minutes

**References:**
- [GitHub Copilot Documentation](https://docs.github.com/en/copilot)
- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [Copilot CLI Installation](https://docs.github.com/en/copilot/how-tos/copilot-cli/set-up-copilot-cli/install-copilot-cli)

---

## Prerequisites

| Requirement | Version | Purpose |
|-------------|---------|---------|
| VS Code | Latest | Code review via GitHub Copilot Chat |
| GitHub Copilot | Pro/Pro+/Business/Enterprise | Required for all lab exercises |
| Copilot CLI | Latest | Standalone terminal AI agent (`copilot`) |
| .NET SDK | 10.0+ | TaskForge application |
| Node.js | 22+ | Copilot CLI npm install & MCP server execution |
| Git | Latest | Version control |
| jq | Latest | JSON parsing for hook scripts (optional on Windows) |

---

## Install GitHub Copilot CLI

GitHub Copilot CLI is a standalone terminal application. Choose the install method for your platform:

**Option A — npm (all platforms, requires Node.js 22+):**
```bash
npm install -g @github/copilot
```

**Option B — WinGet (Windows):**
```powershell
winget install GitHub.Copilot
```

**Option C — Homebrew (macOS / Linux):**
```bash
brew install copilot-cli
```

**Option D — Install script (macOS / Linux):**
```bash
curl -fsSL https://gh.io/copilot-install | bash
```

### Authenticate

On first launch, Copilot CLI will prompt you to log in:

1. Start an interactive session: `copilot`
2. When prompted, enter `/login` and follow the browser-based authentication flow.

Alternatively, set a fine-grained personal access token with the **Copilot Requests** permission as an environment variable:
```bash
export GH_TOKEN=your_token_here
```

### Verify the installation

```bash
copilot --version
```

You should see a version number like `v0.x.x`.

> 💡 For the latest features, enable experimental mode: `copilot --experimental` or run `/experimental` in a session. This unlocks autopilot mode and other preview features.

---

## Platform-Specific Install Notes

<details>
<summary><strong>🐧 Linux</strong></summary>

- **.NET 10 SDK**: Follow the [Install .NET on Linux](https://learn.microsoft.com/dotnet/core/install/linux) guide for your distribution.
- **GitHub CLI**: Install via package manager — see [Installing gh on Linux](https://github.com/cli/cli/blob/trunk/docs/install_linux.md).
- **Node.js**: Install via your package manager or [NodeSource](https://github.com/nodesource/distributions), or use [nvm](https://github.com/nvm-sh/nvm).
- **VS Code**: Download from [code.visualstudio.com](https://code.visualstudio.com/), or install via Snap: `sudo snap install code --classic`.
- **jq**: `sudo apt install jq` or `sudo dnf install jq`.

</details>

<details>
<summary><strong>🍎 macOS</strong></summary>

- **.NET 10 SDK**: Download from [dotnet.microsoft.com](https://dotnet.microsoft.com/download/dotnet/10.0) or: `brew install dotnet-sdk`.
- **GitHub CLI**: `brew install gh`.
- **Node.js**: `brew install node`, or use [nvm](https://github.com/nvm-sh/nvm).
- **VS Code**: Download from [code.visualstudio.com](https://code.visualstudio.com/) or: `brew install --cask visual-studio-code`.
- **jq**: `brew install jq`.

</details>

<details>
<summary><strong>🪟 Windows</strong></summary>

- **.NET 10 SDK**: Download from [dotnet.microsoft.com](https://dotnet.microsoft.com/download/dotnet/10.0) or: `winget install Microsoft.DotNet.SDK.10`.
- **GitHub CLI**: `winget install GitHub.cli` or the [MSI installer](https://cli.github.com/).
- **Node.js**: Download from [nodejs.org](https://nodejs.org/) or: `winget install OpenJS.NodeJS.LTS`.
- **VS Code**: Download from [code.visualstudio.com](https://code.visualstudio.com/) or: `winget install Microsoft.VisualStudioCode`.
- **jq**: `winget install jqlang.jq` or download from [jqlang.github.io/jq](https://jqlang.github.io/jq/download/).

</details>

---

## Clone and Build

1. **Clone the repository:**
   ```bash
   git clone https://github.com/ms-mfg-community/ghcp-taskforge.git
   cd ghcp-taskforge
   ```
2. **Navigate to the solution directory:**
   ```bash
   cd src/TaskForge
   ```
3. **Restore dependencies and build:**
   ```bash
   dotnet restore
   dotnet build
   ```
4. **Run the application** to verify everything works:
   ```bash
   cd TaskForge.Web
   dotnet run
   ```
   You should see `Now listening on: http://localhost:...` in the output. Press `Ctrl+C` to stop the server, then navigate back:
   ```bash
   cd ..
   ```
5. **Verify** the build and run complete with no errors.

---

## Enable Custom Agents

1. Open a terminal and navigate to the repository root.
2. Start an interactive session: `copilot`
3. Use `/agent` to list available agents.
4. Verify the following custom agents appear:
   - **blueprint** — Architect
   - **forge** — Developer
   - **shield** — Reviewer
   - **sage** — Doc Writer

---

## Configure MCP Servers

- Start an interactive `copilot` session from the repository root.
- Use `/mcp` to view and manage MCP servers.
- MCP configuration can live in `.github/copilot/mcp.json` for the CLI (or `.vscode/mcp.json` for VS Code code-review sessions).

---

## Verify Copilot CLI Plugins

If you have the Copilot CLI installed, verify plugin support:

```bash
copilot plugin marketplace list
```

You should see at least two default marketplaces: `copilot-plugins` and `awesome-copilot`.

---

## Verification Checklist

Run through each item to confirm your environment is ready:

- [ ] Copilot CLI works: `copilot --version` returns a version
- [ ] .NET project builds successfully: `dotnet build` (from `src/TaskForge`)
- [ ] .NET project runs successfully: `dotnet run` (from `src/TaskForge/TaskForge.Web`) — verify it starts listening
- [ ] Custom agents visible: start `copilot`, `/agent` lists custom agents
- [ ] MCP servers connected: start `copilot`, `/mcp` shows connected servers
- [ ] Model selection works: start `copilot`, type `/model` to see available models
- [ ] (Optional) Plugin marketplaces accessible: `copilot plugin marketplace list`
- [ ] `.github/hooks/` directory contains `hooks.json`

---

## Troubleshooting

<details>
<summary><strong>Copilot CLI not found</strong></summary>

- Verify the install succeeded: `copilot --version`
- If installed via npm, ensure the global npm bin directory is on your PATH: `npm bin -g`
- If installed via WinGet, restart your terminal after installation
- Try reinstalling: `npm install -g @github/copilot`
- On Windows, ensure you are using PowerShell v6+

</details>

<details>
<summary><strong>Custom agents not appearing</strong></summary>

- Confirm your terminal is at the **repository root** (not a subdirectory).
- Check that `.github/agents/` contains the agent definition files.
- Exit and restart the `copilot` session.
- Ensure your Copilot subscription supports custom agents (Business/Enterprise/Pro+).

</details>

<details>
<summary><strong>.NET build failures</strong></summary>

- Verify .NET 10 SDK is installed: `dotnet --list-sdks`
- If the SDK version is missing, download it from [https://dotnet.microsoft.com/download/dotnet/8.0](https://dotnet.microsoft.com/download/dotnet/8.0).
- Try a clean restore: `dotnet clean && dotnet restore && dotnet build`

</details>

<details>
<summary><strong>MCP server connection issues</strong></summary>

- Ensure Node.js 22+ is installed: `node --version`
- Check that MCP config exists (`.github/copilot/mcp.json` or `.vscode/mcp.json`) and is valid JSON.
- Restart the `copilot` session.
- Run `/mcp` inside the session to diagnose connection issues.

</details>

<details>
<summary><strong>Hook scripts not executing</strong></summary>

1. Ensure hooks.json is in `.github/hooks/` on the default branch
2. Verify scripts are executable: `chmod +x scripts/hooks/*.sh`
3. Check JSON syntax: `jq . .github/hooks/hooks.json`
4. Verify jq is installed for bash hooks: `jq --version`

</details>

<details>
<summary><strong>Copilot CLI plugins not working</strong></summary>

1. Ensure Copilot CLI is installed: `copilot --version`
2. Check plugin support: `copilot plugin --help`
3. List marketplaces: `copilot plugin marketplace list`
4. If no marketplaces appear, try: `copilot plugin marketplace add github/copilot-plugins`

</details>

---

[**Next: Lab 01 — The AI Design Studio →**](lab01.md)
