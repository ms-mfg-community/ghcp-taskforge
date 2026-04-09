# TaskForge: GitHub Copilot Design Labs

> 🎨 **Theme: Using AI in the Design Process**
> Learn to leverage every GitHub Copilot capability to design, build, review, and document software — from idea to production.

> ⏱️ **Pre-work** — complete setup before the session begins.

---

## Overview

A hands-on workshop consisting of **4 labs (~45 minutes total)** that teaches developers how to use GitHub Copilot as a full design and development partner. You'll work with **TaskForge**, a real .NET N-tier application, progressing from initial design through autonomous development and AI-orchestrated pipelines.

---

## What You'll Build

**TaskForge** — a team task & project management application built with:

| Layer | Technology | Description |
|-------|-----------|-------------|
| Presentation | ASP.NET Core MVC | Web UI with Razor views |
| Business Logic | Service Layer | Application services and domain rules |
| Data Access | Entity Framework Core + SQLite | Repository pattern with EF Core |
| Authentication | ASP.NET Core Identity | User management and authorization |

---

## Prerequisites

### Must-Have Now

| Requirement | Details |
|-------------|---------|
| **GitHub account** | With Copilot license (Individual, Business, or Enterprise) |
| **VS Code** | Latest version with [GitHub Copilot](https://marketplace.visualstudio.com/items?itemName=GitHub.copilot) extension |
| **Git** | [Install](https://git-scm.com/downloads) — configured with your GitHub credentials |

### Optional — Required Only for Specific Labs

| When needed | Requirement |
|-------------|-------------|
| All labs | **GitHub CLI** (`gh`) — [Install](https://cli.github.com/) — verify with `gh --version` |
| All labs | **Copilot CLI** — [Install guide](https://docs.github.com/en/copilot/how-tos/copilot-cli/set-up-copilot-cli/install-copilot-cli): `npm install -g @github/copilot` — verify with `copilot --version` |
| Manual Setup only | **.NET 10 SDK** — [Download](https://dotnet.microsoft.com/download/dotnet/10.0) — verify with `dotnet --version` — **devcontainer users: already included in the container** |
| Manual Setup only | **Node.js 22+** — [Download](https://nodejs.org/) — verify with `node --version` — **devcontainer users: already included in the container** |

---

## Choose Your Path

| Path | Time | For | Recommendation |
|------|------|-----|----------------|
| [**Codespaces**](#option-a--github-codespaces) | 5–10 min | In-person workshops, no local install | ⭐ **Start here** |
| [**Docker Desktop**](#option-b--vs-code--docker-desktop) | ~15 min | Already using Docker | ✅ Popular |
| [**Podman**](#option-c--vs-code--podman) | ~15 min | Enterprise (Docker restricted) | ✅ Supported |
| [**Manual**](#option-d--manual-setup) | ~20 min | Prefer direct install | Advanced |

---

### Option A — GitHub Codespaces

**5–10 min | Zero local install**

1. Fork this repository (see [Fork & Clone](#fork--clone) below)
2. On your fork: click **Code** → **Codespaces** → **Create codespace on main**
3. Wait for the codespace to build (first time takes a few minutes — the post-create script installs all tools and builds the solution)
4. Once the terminal is ready, authenticate:
   ```bash
   copilot login
   gh auth login
   ```
5. ➜ **[Jump to Verify Copilot CLI](#verify-copilot-cli)**

> **Tip:** If _"Codespaces"_ does not appear under the **Code** button, your organization may not have Codespaces enabled. Ask your admin, or use Option B or C below.

---

### Option B — VS Code + Docker Desktop

**~15 min | Has Docker**

1. Install the [Dev Containers](https://marketplace.visualstudio.com/items?itemName=ms-vscode-remote.remote-containers) VS Code extension
2. Ensure [Docker Desktop](https://www.docker.com/products/docker-desktop/) is running
3. Fork and clone this repository (see [Fork & Clone](#fork--clone) below)
4. Open the repo folder in VS Code
5. When prompted _"Reopen in Container"_, click **Yes** — or run **Dev Containers: Reopen in Container** from the Command Palette (`Ctrl+Shift+P` / `Cmd+Shift+P`)
6. Wait for the container to build (first time takes a few minutes)
7. Once the terminal is ready, authenticate:
   ```bash
   copilot login
   gh auth login
   ```
8. ➜ **[Jump to Verify Copilot CLI](#verify-copilot-cli)**

The container includes: .NET 10 SDK, Node.js 22, GitHub CLI, Copilot CLI, jq, and all recommended VS Code extensions.

---

### Option C — VS Code + Podman

**~15 min | Enterprise/Docker restricted**

1. Install [Podman](https://podman.io/docs/installation) and the [Dev Containers](https://marketplace.visualstudio.com/items?itemName=ms-vscode-remote.remote-containers) VS Code extension

2. Configure VS Code to use Podman as the container engine. Open **Settings** (`Ctrl+,` / `Cmd+,`) and set:
   ```json
   "dev.containers.dockerPath": "podman"
   ```

3. Start the Podman machine (if not already running):

   **macOS / Windows:**
   ```bash
   podman machine init
   podman machine start
   ```

   **Linux:** Podman runs natively — no machine needed. Ensure the Podman socket is active:
   ```bash
   systemctl --user enable --now podman.socket
   ```

4. Fork and clone this repository (see [Fork & Clone](#fork--clone) below)

5. Open the repo folder in VS Code

6. Run **Dev Containers: Reopen in Container** from the Command Palette (`Ctrl+Shift+P` / `Cmd+Shift+P`)

7. Wait for the container to build (first time takes a few minutes)

8. Once the terminal is ready, authenticate:
   ```bash
   copilot login
   gh auth login
   ```

9. ➜ **[Jump to Verify Copilot CLI](#verify-copilot-cli)**

> **Podman troubleshooting:** If the container fails to start, verify the Podman socket path matches what VS Code expects. On Linux, set `"dev.containers.dockerSocketPath": "/run/user/1000/podman/podman.sock"` (adjust the UID if yours differs). On macOS/Windows, `podman machine start` handles this automatically.

---

### Option D — Manual Setup

**~20 min | Prefer direct install**

Before starting, ensure you have all [prerequisites](#prerequisites) installed.

See the [Lab Setup guide](labs/setup.md) for detailed manual installation instructions.

**Quick steps:**

1. Fork and clone this repository (see [Fork & Clone](#fork--clone) below)
2. Install .NET 10 SDK, Node.js 22+, GitHub CLI, and Copilot CLI
3. Build the solution:
   ```bash
   cd src/TaskForge
   dotnet build TaskForge.slnx
   ```
4. _(Optional)_ Run the application:
   ```bash
   cd TaskForge.Web
   dotnet run
   ```
   The app starts at **http://localhost:5000** (or https://localhost:5001). Press `Ctrl+C` to stop.
5. ➜ **[Jump to Verify Copilot CLI](#verify-copilot-cli)**

---

## Fork & Clone

> **GitHub EMU users:** If your organization uses [GitHub Enterprise Managed Users (EMU)](https://docs.github.com/en/enterprise-cloud@latest/admin/identity-and-access-management/understanding-iam-for-enterprises/about-enterprise-managed-users) and **cannot fork** external repositories, skip to [Clone into Your Own Namespace](#clone-into-your-own-namespace-github-emu-users) below.

🌐 **On GitHub:**

1. Fork this repository: click **Fork** at the top of this page
2. Go to your fork's **Settings** → **Actions** → **General** and ensure Actions are enabled
3. Go to the **Actions** tab and click _"I understand my workflows, go ahead and enable them"_ if prompted

🖥️ **On your machine:**

4. Clone your fork:

   **WSL/Bash:**
   ```bash
   git clone https://github.com/YOUR-USERNAME/ghcp-taskforge.git
   cd ghcp-taskforge
   ```

   **PowerShell:**
   ```powershell
   git clone https://github.com/YOUR-USERNAME/ghcp-taskforge.git
   Set-Location ghcp-taskforge
   ```

5. Verify the .NET project builds:
   ```bash
   cd src/TaskForge
   dotnet build TaskForge.slnx
   ```

   You should see:
   ```
   Build succeeded.
       0 Warning(s)
       0 Error(s)
   ```

6. _(Optional)_ Run the application:
   ```bash
   cd TaskForge.Web
   dotnet run
   ```
   The app starts at **http://localhost:5000** (or https://localhost:5001). On first run, the database is automatically created and seeded with sample data.

   Press `Ctrl+C` to stop.

---

### Clone into Your Own Namespace (GitHub EMU Users)

> **Who is this for?** If your organization uses [GitHub Enterprise Managed Users (EMU)](https://docs.github.com/en/enterprise-cloud@latest/admin/identity-and-access-management/understanding-iam-for-enterprises/about-enterprise-managed-users), you **cannot fork** repositories that live outside your enterprise. Follow these steps to create a copy in your own GitHub namespace.

🌐 **On GitHub:**

1. Navigate to [github.com/new](https://github.com/new) to create a new repository
2. Set the **Owner** to your EMU account (e.g., `YOUR-USERNAME_ENTERPRISE`)
3. Name the repository `ghcp-taskforge`
4. Set visibility to **Private** (or Internal, per your organization's policy)
5. **Do not** initialize the repository with a README, `.gitignore`, or license
6. Click **Create repository**
7. After pushing the lab content (see steps below), go to **Settings** → **Actions** → **General** and ensure Actions are enabled

🖥️ **On your machine:**

1. Clone the source repository:

   **WSL/Bash:**
   ```bash
   git clone https://github.com/ms-mfg-community/ghcp-taskforge.git
   cd ghcp-taskforge
   ```

   **PowerShell:**
   ```powershell
   git clone https://github.com/ms-mfg-community/ghcp-taskforge.git
   Set-Location ghcp-taskforge
   ```

2. Change the remote `origin` to point to **your** new repository:

   **WSL/Bash:**
   ```bash
   git remote set-url origin https://github.com/YOUR-USERNAME_ENTERPRISE/ghcp-taskforge.git
   ```

   **PowerShell:**
   ```powershell
   git remote set-url origin https://github.com/YOUR-USERNAME_ENTERPRISE/ghcp-taskforge.git
   ```

3. Push all branches and tags to your new repository:
   ```bash
   git push --all origin
   git push --tags origin
   ```

4. Verify the .NET project builds:
   ```bash
   cd src/TaskForge
   dotnet build TaskForge.slnx
   ```

5. _(Optional)_ Run the application:
   ```bash
   cd TaskForge.Web
   dotnet run
   ```

> **Tip:** To keep your copy in sync with the source, add it as an `upstream` remote:
> ```bash
> git remote add upstream https://github.com/ms-mfg-community/ghcp-taskforge.git
> git fetch upstream
> git merge upstream/main
> ```

Once complete, continue with [Verify Copilot CLI](#verify-copilot-cli) below.

---

## Verify Copilot CLI

🖥️ **On your machine (or inside the devcontainer):**

1. Verify Copilot CLI is installed:
   ```bash
   copilot --version
   ```

2. Authenticate the Copilot CLI:
   ```bash
   copilot login
   ```

   > **Note:** The Copilot CLI has its own authentication — it does **not** use `gh auth`. Running `copilot login` opens a browser-based device code flow. Alternatively, set a `GH_TOKEN` environment variable with a fine-grained PAT that has the **Copilot Requests** permission.

3. Verify Copilot CLI is authenticated by sending a test prompt:
   ```bash
   copilot -p "hello"
   ```

   If authenticated, you'll receive a response. If not, the CLI will prompt you to log in.

---

## ✅ Setup Complete

You now have:

- A forked repository with all Copilot configurations
- A building .NET project (TaskForge) — optionally verified running at http://localhost:5000
- Copilot CLI authenticated
- An understanding of the workshop structure

**➜ [Start Lab 01: The AI Design Studio](labs/lab01.md)**

---

## GitHub Copilot Features Covered

| Feature | Lab | Description |
|---------|-----|-------------|
| 🖥️ Copilot CLI | 1, 4 | Standalone terminal AI agent (`copilot`) |
| 📋 Custom Instructions | 1 | `.github/copilot-instructions.md` |
| 📝 Reusable Prompts | 1, 4 | `.github/prompts/*.prompt.md` |
| 🤖 Custom Agents | 2 | `.github/agents/*.agent.md` |
| 🔄 Sub-agents | 2 | Parallel agent execution |
| ⚡ Copilot CLI (Agentic) | 2, 4 | Autonomous multi-file editing |
| 🚀 Copilot Coding Agent | 3 | Issue → autonomous PR creation |
| 🔍 Code Review | 3, 4 | AI-powered PR review |
| 📊 PR Summaries | 3 | Auto-generated PR descriptions |
| 🪝 Agent Hooks | 2 | `.github/hooks/hooks.json` lifecycle events |
| 🔌 MCP/Plugins | 4 | External tool integration |
| 🧩 CLI Plugins | 4 | Extend Copilot CLI with marketplace plugins |
| 💡 Inline Suggestions | 3 | Real-time code completion |

---

## Labs

### [Lab 01: The AI Design Studio](labs/lab01.md) (~10 min)
> Use Copilot CLI, custom instructions, and reusable prompts to design a new feature from scratch.

### [Lab 02: Building with Your AI Team](labs/lab02.md) (~12 min)
> Deploy custom agents (Blueprint, Forge, Shield, Sage) and sub-agents to implement the design.

### [Lab 03: The Autonomous Developer](labs/lab03.md) (~12 min)
> Leverage the Copilot Coding Agent for issue-to-PR automation, code review, and inline suggestions.

### [Lab 04: Orchestrating the AI Pipeline](labs/lab04.md) (~11 min)
> Combine MCP servers, agent mode, reusable prompts, and Copilot CLI into a full AI-orchestrated workflow.

---

## Repository Structure

```
ghcp-taskforge/
├── .devcontainer/
│   ├── devcontainer.json        # Dev container configuration (Docker/Podman/Codespaces)
│   └── post-create.sh           # Post-create setup script
├── .github/
│   ├── hooks/                   # Copilot agent hook configurations
│   │   └── hooks.json
│   ├── agents/                  # Custom Copilot agent definitions
│   ├── instructions/            # Custom instructions for Copilot
│   └── prompts/                 # Reusable prompt templates
├── .vscode/
│   └── mcp.json                 # MCP server configuration
├── labs/
│   ├── setup.md                 # Manual setup guide (if not using devcontainer)
│   ├── lab01.md                 # Lab 01: The AI Design Studio
│   ├── lab02.md                 # Lab 02: Building with Your AI Team
│   ├── lab03.md                 # Lab 03: The Autonomous Developer
│   └── lab04.md                 # Lab 04: Orchestrating the AI Pipeline
├── src/
│   └── TaskForge/
│       ├── TaskForge.Core/      # Domain models and interfaces
│       ├── TaskForge.Data/      # EF Core data access layer
│       ├── TaskForge.Web/       # ASP.NET Core MVC web application
│       └── TaskForge.slnx       # Solution file
├── scripts/
│   └── hooks/                   # Hook scripts (session logging, security checks)
└── README.md                    # This file
```

---

## Custom Agents

| Agent | Role | Description |
|-------|------|-------------|
| 🏗️ **@blueprint** | Architect | Designs system architecture, defines patterns, and creates technical specifications |
| 🔨 **@forge** | Developer | Implements features following architectural guidance and coding standards |
| 🛡️ **@shield** | Reviewer | Reviews code for security, performance, best practices, and correctness |
| 📖 **@sage** | Doc Writer | Generates documentation, API references, and user guides |

---

## Getting Started

1. **Choose your setup path** from [Choose Your Path](#choose-your-path) above — Codespaces is recommended for workshops.
2. **Work through the labs sequentially** — each lab builds on concepts from the previous one.
3. **Experiment freely** — the labs provide guided exercises, but you're encouraged to explore beyond the instructions.

---

## Troubleshooting

<details>
<summary><strong>Devcontainer not starting</strong></summary>

- Ensure Docker Desktop or Podman is running
- For Podman users, verify the socket path is correct (see [Podman setup](#option-c--vs-code--podman))
- Try rebuilding the container: Command Palette → **Dev Containers: Rebuild Container**

</details>

<details>
<summary><strong>Copilot CLI not found</strong></summary>

- Verify the install succeeded: `copilot --version`
- If installed via npm, ensure the global npm bin directory is on your PATH
- Try reinstalling: `npm install -g @github/copilot`

</details>

<details>
<summary><strong>.NET build failures</strong></summary>

- Verify .NET 10 SDK is installed: `dotnet --list-sdks`
- Try a clean restore: `dotnet clean && dotnet restore && dotnet build`

</details>

<details>
<summary><strong>Custom agents not appearing</strong></summary>

- Confirm your terminal is at the **repository root** (not a subdirectory)
- Check that `.github/agents/` contains the agent definition files
- Exit and restart the `copilot` session
- Ensure your Copilot subscription supports custom agents (Business/Enterprise/Pro+)

</details>

See [labs/setup.md](labs/setup.md) for additional troubleshooting tips.

---

## License

This workshop is provided for **educational purposes**. The TaskForge application and all lab materials are intended for use in learning GitHub Copilot features and capabilities.
