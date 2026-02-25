# 1 — Mastering the Copilot CLI

In this lab you'll get hands-on with GitHub Copilot CLI — learning how to navigate the interface, customize it for your project, and leverage power features that will make you productive from day one. By the end, you'll feel confident using the CLI as your primary AI coding partner.

> ⏱️ Duration: ~10 minutes · 8 exercises

References:

- [Using GitHub Copilot CLI](https://docs.github.com/en/copilot/how-tos/copilot-cli/use-copilot-cli)
- [Quickstart for Customizing Copilot CLI](https://docs.github.com/en/copilot/how-tos/copilot-cli/customize-copilot/quickstart-for-customizing)
- [CLI Command Reference](https://docs.github.com/en/copilot/reference/cli-command-reference)
- [Custom Instructions](https://docs.github.com/en/copilot/how-tos/copilot-cli/customize-copilot/add-custom-instructions)
- [Custom Agents](https://docs.github.com/en/copilot/how-tos/copilot-cli/customize-copilot/create-custom-agents-for-cli)

---

## 1.1 Your First CLI Session

GitHub Copilot CLI is a full AI agent that lives in your terminal. It can read your project, write code, run commands, and even interact with GitHub — all without opening an editor.

| Mode | How to use | Description |
|------|-----------|-------------|
| Interactive | `copilot` | Start a conversation — explore, iterate, and build |
| Programmatic | `copilot -p "prompt"` | One-shot prompt — get an answer and exit |

### Exercise 1 — Start Copilot and explore the project

**Part A — Interactive mode.** Open your terminal, navigate to the TaskForge project directory, and start Copilot CLI:

```
copilot
```

Copilot asks you to **trust the folder**. You'll see three options:

| Option | What it does |
|--------|-------------|
| **Yes, proceed** | Trust for this session only |
| **Yes, and remember this folder** | Trust permanently — won't ask again |
| **No, exit (Esc)** | End the session immediately |

Choose **Yes, proceed** (or **Yes, and remember** if this is your own machine). Then ask:

```
What is the structure of this project?
```

Watch how Copilot reads files and directories to discover the N-tier architecture (`TaskForge.Web`, `TaskForge.Core`, `TaskForge.Data`) without you having to explain it.

**Part B — Programmatic mode.** Type `/exit` to leave, then try a one-shot prompt:

```bash
copilot -p "Explain what the command 'dotnet new mvc --auth Individual --use-local-db' does"
```

Copilot explains each flag and exits — perfect for scripting or quick lookups.

**Part C — GitHub integration.** Copilot CLI has a built-in GitHub MCP server:

```bash
copilot -p "List my open pull requests"
```

> 💡 The GitHub MCP server lets Copilot search issues, review PRs, merge branches, and more — all from your terminal.

> ✅ **Checkpoint:** You've used both interactive and programmatic modes, and seen Copilot read your project and talk to GitHub.

---

## 1.2 Navigating the CLI

Now that you have a session running, let's learn the essential interaction patterns that make the CLI fast and fluid.

### Exercise 2 — File context and shell commands

Start a new interactive session with `copilot`, then try these two shortcuts:

1. **`@` — File context.** Reference a file to focus Copilot's attention:

   ```
   @src/TaskForge/TaskForge.Web/Program.cs Explain the middleware pipeline in this file
   ```

   The `@` prefix adds the file's contents directly into your prompt. As you type a path, matching files appear — use **arrow keys** and **Tab** to autocomplete.

2. **`!` — Shell escape.** Run terminal commands without leaving Copilot:

   ```
   !git status
   ```

   This executes directly in your local shell — no AI involved. Useful for checking state without breaking your conversation flow.

### Exercise 3 — Modes, context, and usage

1. **Cycle modes** — Press **Shift+Tab** to switch between:
   - **Interactive** (default) — back-and-forth conversation
   - **Plan** — Copilot creates a structured plan before making changes
   - **Autopilot** — Copilot works autonomously without prompting for input at each step

2. **Stop an operation** — Press **Esc** to cancel while Copilot is thinking or working.

3. **Check context** — Run `/context` to see a visual breakdown of your token usage, then run `/usage` to see session statistics (premium requests, duration, tokens per model).

> 💡 If your context window fills up during a long session, use `/compact` to summarize the conversation history and free up space. Copilot also does this automatically when you approach 95% of the token limit.

### Knowledge Check

<details>
<summary>❓ What's the difference between <code>@</code> and <code>!</code> in the CLI?</summary>

- **`@` (file reference)** adds a file's contents to your prompt as context for Copilot. It's an AI interaction — Copilot reads and reasons about the file. Example: `@src/Program.cs Explain this file`
- **`!` (shell command)** runs a command directly in your local shell, bypassing Copilot entirely. It's a quick escape hatch for terminal commands. Example: `!git log --oneline -5`

A good mental model:
```
@ = "Hey Copilot, look at this file"
! = "Hey terminal, run this command"
```

</details>

---

## 1.3 Customizing Copilot for Your Project

Out of the box, Copilot gives generic suggestions. **Customization** makes it a team member who knows your conventions, architecture, and tools. This repo comes pre-configured with several customization layers — let's explore each one.

### Custom Instructions

Custom instructions are Markdown files that automatically shape every Copilot response. TaskForge uses two types:

```
.github/
├── copilot-instructions.md          ← Global: always included in every interaction
└── instructions/
    ├── csharp.instructions.md       ← Conditional: only when working with *.cs files
    └── razor.instructions.md        ← Conditional: only when working with *.cshtml files
```

### Exercise 4 — Explore loaded instructions

1. In your Copilot CLI session, run:

   ```
   /instructions
   ```

   This lists every custom instruction file Copilot has loaded from the repo.

2. Now reference the global instructions file directly:

   ```
   @.github/copilot-instructions.md Summarize the key standards defined in this file
   ```

   Copilot will describe the architecture (N-tier), tech stack (.NET 10, EF Core), coding standards (file-scoped namespaces, async/await), testing patterns (xUnit, Arrange/Act/Assert), and security rules.

### Exercise 5 — See conditional instructions in action

Ask Copilot a question that involves a C# file — this triggers the `csharp.instructions.md` file (which has `applyTo: "**/*.cs"`):

```
@src/TaskForge/TaskForge.Data/Models/TaskItem.cs How should I add a new property to track estimated hours?
```

Notice how Copilot's response follows project conventions: `/// <summary>` XML docs, `_camelCase` private fields, data annotations for validation. That's the conditional instructions at work — they only activate when you're working with C# files.

> 💡 Custom instructions are **automatically loaded** — no setup needed. Global instructions apply everywhere; conditional instructions activate based on file type via the `applyTo` glob in their YAML front matter.

### Custom Agents & MCP Servers

Custom agents are specialized AI personas defined as Markdown files. **Model Context Protocol (MCP)** servers extend Copilot with external tools and data sources.

### Exercise 6 — Discover agents and MCP servers

1. **Browse agents** — run `/agent` to see TaskForge's four custom agents:

   | Agent | File | Role |
   |-------|------|------|
   | 🏗️ **Blueprint** | `architect.agent.md` | Solution Architect |
   | 🔨 **Forge** | `developer.agent.md` | .NET Developer |
   | 🛡️ **Shield** | `reviewer.agent.md` | Code Reviewer |
   | 📚 **Sage** | `doc-writer.agent.md` | Documentation Writer |

2. **Try an agent** — exit the session, then invoke one programmatically:

   ```bash
   copilot --agent=architect -p "What architectural pattern does TaskForge follow?"
   ```

3. **Check MCP servers** — start a new session and run `/mcp` to see connected servers, including the built-in **GitHub MCP server** and any project-configured servers like `microsoft-learn` and `context7`.

> 💡 Custom agents run as **subagents** with their own context window. Copilot can also choose to use agents automatically if it judges they're a good fit.

> 💡 This repo also includes **hooks**, **skills**, and **plugins**. We'll explore these in [Lab 04](lab04.md).

> ✅ **Checkpoint:** You've explored custom instructions, custom agents, and MCP servers — the three main customization layers that make Copilot project-aware.

### Knowledge Check

<details>
<summary>❓ How do custom instructions, agents, and MCP servers work together?</summary>

Think of them as three complementary layers:

1. **Custom instructions** set the *rules* — coding standards, architecture patterns, and conventions that shape every response. They answer: "How should Copilot write code for this project?"

2. **Custom agents** provide *expertise* — specialized personas for specific tasks like architecture review, implementation, or documentation. They answer: "Who should handle this task?"

3. **MCP servers** supply *tools and data* — external capabilities like documentation lookup, GitHub integration, or database access. They answer: "What information and tools can Copilot access?"

Together: instructions define the standards → agents apply the right expertise → MCP servers provide the data and tools to get it done.

</details>

---

## 1.4 Power Features

Copilot CLI has a rich set of commands for managing your workflow without leaving the terminal. Let's tour the most useful ones.

### Exercise 7 — Code review workflow

Make sure you're in an interactive session, then try these:

1. **See what's changed** in the working directory:

   ```
   /diff
   ```

   This shows all uncommitted changes — a quick way to review your work.

2. **Run a code review** on your changes:

   ```
   /review
   ```

   Copilot analyzes your changes and provides feedback — like having a reviewer on demand before you commit.

### Exercise 8 — Session management

Try these commands to manage your session:

1. **Export your session** as a markdown file or GitHub gist:

   ```
   /share
   ```

   Choose between saving to a local file or creating a secret gist — great for sharing design decisions or debugging sessions with teammates.

2. **Switch AI models** on the fly:

   ```
   /model
   ```

   Select from available models to balance speed, cost, and capability.

3. **Delegate to Copilot coding agent** — push your session context to GitHub, where Copilot opens a draft PR and works in the background:

   ```
   /delegate complete the API integration tests and fix any failing edge cases
   ```

   Or use the `&` shortcut: `& complete the API integration tests`.

> 💡 **Autopilot from the command line:** For fully autonomous tasks, run `copilot --autopilot --yolo -p "YOUR PROMPT"`. Add `--max-autopilot-continues 10` to limit continuation cycles. You can also enter autopilot mode interactively by pressing **Shift+Tab**.

> 💡 **Resume sessions:** Use `copilot --continue` to pick up your most recent session, or `copilot --resume` to choose from a list.

> ✅ **Checkpoint:** You've used review, diff, share, delegate, and model switching — the power features that make the CLI a complete development environment.

---

## 1.5 Quick Reference & Summary

### Keyboard Shortcuts

| Shortcut | Action |
|----------|--------|
| **Shift+Tab** | Cycle modes: interactive → plan → autopilot |
| **Ctrl+T** | Toggle reasoning visibility (show/hide Copilot's thinking) |
| **Ctrl+S** | Save (e.g., when editing MCP server config) |
| **Esc** | Cancel the current operation |
| **`!`** | Run a shell command directly (bypasses AI) |
| **`@`** | Include a file's contents in your prompt |
| **Ctrl+C** | Cancel / clear input (press twice to exit) |
| **Ctrl+L** | Clear the screen |
| **↑ / ↓** | Navigate command history |

### Slash Commands Cheat Sheet

| Command | What it does |
|---------|-------------|
| `/agent` | Browse and select custom agents |
| `/compact` | Summarize conversation to free context space |
| `/context` | Show token usage visualization |
| `/delegate` | Push session to Copilot coding agent on GitHub |
| `/diff` | Review uncommitted changes |
| `/exit` | Leave the CLI session |
| `/instructions` | View loaded custom instruction files |
| `/mcp` | Manage MCP server connections |
| `/model` | Switch between AI models |
| `/review` | Run an AI code review on your changes |
| `/share` | Export session as markdown or gist |
| `/usage` | Show session statistics |

### Key Takeaways

- **Two modes, one tool** — Use interactive (`copilot`) for exploration and iteration; programmatic (`copilot -p`) for quick answers and scripting.
- **Customization is automatic** — Custom instructions, agents, and MCP servers load from your repo with zero manual setup. Your whole team benefits.
- **You don't need to leave the terminal** — Review code, switch models, delegate to coding agent, check context usage, and export sessions — all without opening a browser or editor.
- **Start simple, go autonomous** — Begin in interactive mode, use plan mode for structured work, and graduate to autopilot for hands-free execution.

> **Next:** In [Lab 02](lab02.md) you'll use these CLI skills to design the TaskForge domain model — from entity relationships to reusable prompt templates.
