# 🚀 GitHub Copilot CLI Cheat Sheet

> **Your quick reference guide to mastering GitHub Copilot CLI**  
> Print this, bookmark it, or keep it handy for fast lookups.

---

<details>
<summary><strong>📋 Table of Contents</strong></summary>

- [📦 Installation](#-installation)
- [🔐 Authentication](#-authentication)
- [🎮 Modes of Operation](#-modes-of-operation)
- [⌨️ Keyboard Shortcuts](#️-keyboard-shortcuts)
- [🔧 Command-Line Flags](#-command-line-flags)
- [📝 Slash Commands](#-slash-commands)
  - [Session Management](#session-management)
  - [Code & Review](#code--review)
  - [Configuration](#configuration)
  - [Advanced](#advanced)
- [🔤 Special Prefixes](#-special-prefixes)
- [🤖 Custom Agents](#-custom-agents)
- [🔌 MCP (Model Context Protocol)](#-mcp-model-context-protocol)
- [🧩 Plugins](#-plugins)
- [📁 Custom Instructions](#-custom-instructions)
- [🎯 Quick Recipes](#-quick-recipes)
  - [Explore a new codebase](#explore-a-new-codebase)
  - [Code review workflow](#code-review-workflow)
  - [Autonomous feature implementation](#autonomous-feature-implementation)
  - [Delegate to Copilot coding agent](#delegate-to-copilot-coding-agent)
  - [Quick lookup (non-interactive)](#quick-lookup-non-interactive)
  - [Switch model mid-session](#switch-model-mid-session)
- [🔄 Session Continuity](#-session-continuity)
- [⚡ Power Tips](#-power-tips)
- [🆘 Troubleshooting](#-troubleshooting)
- [📚 Resources](#-resources)

</details>

---

## 📦 Installation

| Platform | Command |
|----------|---------|
| **npm** (all platforms) | `npm install -g @github/copilot` |
| **WinGet** (Windows) | `winget install GitHub.Copilot` |
| **Homebrew** (macOS/Linux) | `brew install copilot-cli` |
| **Install Script** (macOS/Linux) | `curl -fsSL https://gh.io/copilot-install \| bash` |

**Verify installation:**
```bash
copilot --version
```

---

## 🔐 Authentication

| Command | Description |
|---------|-------------|
| `copilot login` | Authenticate via browser-based device code flow |
| `copilot login --host HOST` | Authenticate to a specific GitHub Enterprise instance |
| `copilot logout` | Sign out and remove stored credentials |

**Alternative:** Set a fine-grained PAT with **Copilot Requests** permission:
```bash
export GH_TOKEN=your_token_here
```

---

## 🎮 Modes of Operation

| Mode | How to Start | Description |
|------|--------------|-------------|
| **Interactive** | `copilot` | Full conversation — explore, iterate, and build |
| **Programmatic** | `copilot -p "prompt"` | One-shot prompt — get an answer and exit |
| **Autopilot** | `copilot --autopilot` | Fully autonomous mode |
| **Agent Mode** | `copilot --agent=NAME` | Use a specific custom agent |

**Switch modes during a session:** Press **Shift+Tab** to cycle:
```
Interactive → Plan → Autopilot → Interactive
```

---

## ⌨️ Keyboard Shortcuts

| Shortcut | Action |
|----------|--------|
| **Shift+Tab** | Cycle modes: interactive → plan → autopilot |
| **Ctrl+T** | Toggle reasoning visibility (show/hide thinking) |
| **Ctrl+S** | Save (e.g., when editing config) |
| **Ctrl+G** | Open prompt in your default text editor |
| **Ctrl+L** | Clear the terminal screen |
| **Ctrl+C** | Cancel / clear input (press twice to exit) |
| **Ctrl+D** | Exit the CLI session |
| **Esc** | Cancel the current operation |
| **↑ / ↓** | Navigate command history |
| **Tab** | Autocomplete file paths with `@` |

---

## 🔧 Command-Line Flags

| Flag | Description |
|------|-------------|
| `-p "prompt"` / `--prompt` | Run in non-interactive (programmatic) mode |
| `--agent=NAME` | Select a specific custom agent |
| `--model=MODEL` | Override the AI model (e.g., `gpt-4`) |
| `--autopilot` | Enable autonomous mode |
| `--yolo` / `--allow-all` | Enable all permissions (use with caution!) |
| `--experimental` | Enable experimental features |
| `--no-experimental` | Explicitly disable experimental features |
| `--continue` | Resume your most recent session |
| `--resume` | Choose from a list of previous sessions |
| `--max-autopilot-continues N` | Limit autopilot continuation cycles |
| `--config-dir PATH` | Use a custom configuration directory |
| `--output-format FORMAT` | Specify output format (`json`, `markdown`) |
| `--share` | Share session context externally |
| `--acp` | Start as Agent Client Protocol server |

**Example — Autonomous mode with agent:**
```bash
copilot --agent=architect --autopilot --yolo -p "Design a microservices API"
```

---

## 📝 Slash Commands

### Session Management

| Command | Description |
|---------|-------------|
| `/help` | List all available slash commands |
| `/exit` or `/quit` | Leave the CLI session |
| `/clear` or `/new` | Start a fresh conversation |
| `/compact` | Summarize conversation to free context space |
| `/context` | Show token usage visualization |
| `/usage` | Display session statistics (requests, tokens, duration) |
| `/share` | Export session as markdown file or GitHub gist |

### Code & Review

| Command | Description |
|---------|-------------|
| `/diff` | Show uncommitted changes in working directory |
| `/review` | Run AI code review on your changes |
| `/tests` | Generate tests for your code |
| `/fix` | Attempt to fix issues in your code |
| `/explain` | Get an explanation of code |
| `/refactor` | Refactor selected code |

### Configuration

| Command | Description |
|---------|-------------|
| `/instructions` | View loaded custom instruction files |
| `/agent` | Browse and select custom agents |
| `/mcp` | Manage MCP server connections |
| `/model` | Switch between available AI models |
| `/experimental` | Enable experimental features in-session |
| `/cwd` | Confirm working directory scope |
| `/add-dir PATH` | Allow Copilot to read/write a directory |
| `/allow-all` or `/yolo` | Enable all permissions for this session |

### Advanced

| Command | Description |
|---------|-------------|
| `/delegate TASK` | Push session to Copilot coding agent on GitHub |
| `/run CMD` | Run shell commands within the session |
| `/changelog [VERSION]` | Show the CLI changelog |
| `/login` | Authenticate (if not already logged in) |

---

## 🔤 Special Prefixes

| Prefix | Purpose | Example |
|--------|---------|---------|
| **`@`** | Include file contents in prompt | `@src/Program.cs Explain this file` |
| **`!`** | Run shell command (bypasses AI) | `!git status` |
| **`&`** | Shortcut for `/delegate` | `& complete the API tests` |

**Quick mental model:**
```
@ = "Hey Copilot, look at this file"
! = "Hey terminal, run this command"
& = "Hey GitHub, take this task and run with it"
```

---

## 🤖 Custom Agents

Custom agents are specialized AI personas defined in `.github/agents/`.

| Command | Description |
|---------|-------------|
| `/agent` | Browse available agents in-session |
| `copilot --agent=NAME` | Start session with specific agent |
| `copilot --agent=NAME -p "..."` | One-shot prompt with agent |

**Example agents:**

| Agent | Role |
|-------|------|
| 🏗️ **blueprint** / **architect** | System architecture & design |
| 🔨 **forge** / **developer** | Code implementation |
| 🛡️ **shield** / **reviewer** | Code review & security |
| 📚 **sage** / **doc-writer** | Documentation generation |

---

## 🔌 MCP (Model Context Protocol)

MCP extends Copilot with external tools and data sources.

| Command | Description |
|---------|-------------|
| `/mcp` | View and manage MCP server connections |
| `@server-name` | Reference a specific MCP server in prompts |

**Config locations:**
- CLI: `.github/copilot/mcp.json`
- VS Code: `.vscode/mcp.json`

**Example MCP query:**
```
Using @microsoft-learn, explain EF Core migrations
```

---

## 🧩 Plugins

| Command | Description |
|---------|-------------|
| `copilot plugin marketplace list` | List available plugin marketplaces |
| `copilot plugin marketplace add NAME` | Add a plugin marketplace |
| `copilot plugin install NAME` | Install a plugin |
| `copilot plugin list` | List installed plugins |

**Default marketplaces:** `copilot-plugins`, `awesome-copilot`

---

## 📁 Custom Instructions

Copilot automatically loads instruction files from your repo:

```
.github/
├── copilot-instructions.md          ← Global: always included
└── instructions/
    ├── csharp.instructions.md       ← Conditional: *.cs files
    └── razor.instructions.md        ← Conditional: *.cshtml files
```

**View loaded instructions:**
```
/instructions
```

---

## 🎯 Quick Recipes

### Explore a new codebase
```bash
copilot
> What is the structure of this project?
> @README.md Summarize the key features
> @src/main.ts Explain the entry point
```

### Code review workflow
```bash
copilot
> /diff
> /review
> /share
```

### Autonomous feature implementation
```bash
copilot --autopilot --yolo -p "Add input validation to the user registration form"
```

### Delegate to Copilot coding agent
```bash
copilot
> Design a REST API for user management
> /delegate implement this design and open a PR
```

### Quick lookup (non-interactive)
```bash
copilot -p "What does the --no-restore flag do in dotnet build?"
```

### Switch model mid-session
```
/model
# Select from available options
```

---

## 🔄 Session Continuity

| Action | Command |
|--------|---------|
| Resume most recent session | `copilot --continue` |
| Choose from session history | `copilot --resume` |
| Export session for sharing | `/share` |

---

## ⚡ Power Tips

1. **Batch context loading:** Reference multiple files at once:
   ```
   @src/models/User.cs @src/models/Task.cs Compare these models
   ```

2. **Context management:** When your context fills up, use `/compact` to summarize and free space.

3. **Tab completion:** Type `@` then start typing a file path — use Tab to autocomplete.

4. **Escape hatch:** Use `!` for quick shell commands without leaving your conversation.

5. **Trust once:** Choose "Yes, and remember this folder" to skip the trust prompt next time.

6. **Model switching:** Use `/model` to balance speed vs. capability based on task complexity.

7. **Session export:** Use `/share` to save design discussions as markdown for team review.

---

## 🆘 Troubleshooting

| Issue | Solution |
|-------|----------|
| `copilot: command not found` | Ensure npm global bin is in PATH: `npm bin -g` |
| Custom agents not appearing | Run from repo root, check `.github/agents/` exists |
| MCP server not connecting | Verify Node.js 22+, check JSON config syntax |
| Session context full | Run `/compact` to summarize and free space |
| Authentication expired | Run `/login` or `copilot login` |

---

## 📚 Resources

- [GitHub Copilot CLI Documentation](https://docs.github.com/en/copilot/github-copilot-in-the-cli)
- [CLI Command Reference](https://docs.github.com/en/copilot/reference/copilot-cli-reference/cli-command-reference)
- [Custom Instructions Guide](https://docs.github.com/en/copilot/how-tos/copilot-cli/customize-copilot/add-custom-instructions)
- [Custom Agents Guide](https://docs.github.com/en/copilot/how-tos/copilot-cli/customize-copilot/create-custom-agents-for-cli)
- [MCP Protocol](https://modelcontextprotocol.io/)

---

> 💡 **Pro tip:** Type `/help` inside any Copilot CLI session to see the complete list of available commands!

---

*Last updated: April 2025*
