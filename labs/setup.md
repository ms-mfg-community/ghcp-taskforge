# Lab Setup

In this lab you will configure your development environment and verify all tools needed for the GitHub Copilot workshop.

> **Duration:** 10-15 minutes

**References:**
- [GitHub Copilot Documentation](https://docs.github.com/en/copilot)
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [GitHub CLI](https://cli.github.com/)

---

## Prerequisites

| Requirement | Version | Purpose |
|-------------|---------|---------|
| VS Code | Latest | Primary IDE with GitHub Copilot integration |
| GitHub Copilot | Pro/Pro+/Business/Enterprise | Required for all lab exercises |
| GitHub CLI | Latest | Required for Copilot CLI features |
| Copilot CLI Extension | Latest | Terminal-based AI assistant (`ghcs`, `ghce`) |
| .NET SDK | 8.0+ | TaskForge application |
| Node.js | 18+ | MCP server execution |
| Git | Latest | Version control |

---

## Install GitHub Copilot CLI

1. **Install GitHub CLI:** Download from [https://cli.github.com/](https://cli.github.com/) and follow the installer for your OS.
2. **Authenticate with GitHub:**
   ```bash
   gh auth login
   ```
3. **Install the Copilot extension:**
   ```bash
   gh extension install github/gh-copilot
   ```
4. **Verify the installation:**
   ```bash
   gh copilot --help
   ```
5. **Set up aliases (optional):**
   ```bash
   alias ghcs='gh copilot suggest'
   alias ghce='gh copilot explain'
   ```

---

## Clone and Build

1. **Clone the repository:**
   ```bash
   git clone <repository-url>
   cd ghcp-taskforge-copilot-labs
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
4. **Verify** the build completes with no errors.

---

## Enable Custom Agents

1. Open VS Code in the repository root.
2. Ensure the **GitHub Copilot** extension is installed and active.
3. Open **Copilot Chat** (`Ctrl+Shift+I` or `⌘+Shift+I`).
4. Type `@` in the chat input to see available agents.
5. Verify the following custom agents appear:
   - **@blueprint** — Architect
   - **@forge** — Developer
   - **@shield** — Reviewer
   - **@sage** — Doc Writer

---

## Configure MCP Servers

- The `.vscode/mcp.json` file is pre-configured in this repository.
- When VS Code prompts you to start MCP servers, click **Allow**.
- Verify MCP server connectivity in the Copilot Chat panel (look for the tool icon).

---

## Verification Checklist

Run through each item to confirm your environment is ready:

- [ ] GitHub Copilot is active in VS Code (check the status bar icon)
- [ ] Copilot CLI works: `gh copilot suggest "hello world"`
- [ ] .NET project builds successfully: `dotnet build` (from `src/TaskForge`)
- [ ] Custom agents are visible in Copilot Chat (type `@`)
- [ ] MCP servers are connected (optional — check Copilot Chat panel)

---

## Troubleshooting

<details>
<summary><strong>Copilot CLI not found</strong></summary>

- Ensure GitHub CLI is installed and on your `PATH`: `gh --version`
- Re-install the Copilot extension: `gh extension install github/gh-copilot`
- If you get a permissions error, run: `gh auth refresh -s copilot`

</details>

<details>
<summary><strong>Custom agents not appearing</strong></summary>

- Confirm you opened VS Code at the **repository root** (not a subdirectory).
- Check that `.github/agents/` contains the agent definition files.
- Reload VS Code: `Ctrl+Shift+P` → **Developer: Reload Window**.
- Ensure your Copilot subscription supports custom agents (Business/Enterprise/Pro+).

</details>

<details>
<summary><strong>.NET build failures</strong></summary>

- Verify .NET 8 SDK is installed: `dotnet --list-sdks`
- If the SDK version is missing, download it from [https://dotnet.microsoft.com/download/dotnet/8.0](https://dotnet.microsoft.com/download/dotnet/8.0).
- Try a clean restore: `dotnet clean && dotnet restore && dotnet build`

</details>

<details>
<summary><strong>MCP server connection issues</strong></summary>

- Ensure Node.js 18+ is installed: `node --version`
- Check that `.vscode/mcp.json` exists and is valid JSON.
- Restart VS Code and re-allow MCP server startup when prompted.
- Review the VS Code **Output** panel (select **MCP** from the dropdown) for error details.

</details>

---

[**Next: Lab 01 — The AI Design Studio →**](lab01.md)
