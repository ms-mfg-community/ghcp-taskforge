# Lab 04 - Orchestrating the AI Pipeline

> 🎨 **Theme: Using AI in the Design Process**
> Combine MCP servers, extensions, Copilot CLI, and custom agents into a full AI-orchestrated design-to-delivery workflow.

> **Duration:** ~13 minutes

**References:**
- [Model Context Protocol (MCP)](https://modelcontextprotocol.io/)
- [GitHub Copilot Extensions](https://docs.github.com/en/copilot/using-github-copilot/using-extensions-to-integrate-external-tools)
- [GitHub Copilot in the CLI](https://docs.github.com/en/copilot/github-copilot-in-the-cli)
- [Copilot Spaces](https://docs.github.com/en/copilot/using-github-copilot/using-copilot-spaces)

---

## 4.1 Extending Copilot with MCP Servers

### What Is MCP?

**Model Context Protocol (MCP)** is an open standard that provides a standardized way for Copilot to connect to external tools and data sources. Think of it as a "USB port" for AI — any tool that speaks MCP can plug into Copilot and extend what it knows and can do.

Without MCP, Copilot relies only on its training data and the files in your workspace. With MCP, Copilot can:

- **Query live documentation** — get up-to-date API references
- **Access external databases** — pull real data into its context
- **Call external services** — interact with deployment pipelines, monitoring tools, and more

### MCP in This Repository

This repo includes a pre-configured `.vscode/mcp.json` file with two MCP servers. The Copilot CLI also supports MCP — use the `/mcp` command inside an interactive session to manage MCP server connections directly from your terminal.

```json
{
  "servers": {
    "microsoft-learn": {
      "type": "sse",
      "url": "https://learn.microsoft.com/api/mcp"
    },
    "context7": {
      "type": "stdio",
      "command": "npx",
      "args": ["-y", "@upstash/context7-mcp"]
    }
  }
}
```

| Server | Type | Purpose |
|--------|------|---------|
| `microsoft-learn` | SSE (Server-Sent Events) | Access to official Microsoft Learn documentation |
| `context7` | stdio (Standard I/O) | Third-party library documentation and context |

> **How agents use MCP:** Notice that all custom agents in this repo have `tools: ["*"]` in their frontmatter. This grants them access to every available tool — including MCP servers. When you ask an agent a question that requires external documentation, it can automatically invoke MCP tools to fetch answers.

### Exercise 1: Query External Documentation

Start a `copilot` interactive session (or use one-shot mode with `copilot -p`) and enter the following prompt:

```
Using @microsoft-learn, explain how to configure EF Core with SQLite 
in an ASP.NET Core application with Identity
```

**Observe:**
- Copilot calls the `microsoft-learn` MCP tool to fetch relevant documentation
- The response includes specific, up-to-date configuration steps — not just general knowledge
- References to official Microsoft Learn pages may appear in the response

Now try a query that leverages the `context7` MCP server:

```
Using context7, show me the latest Entity Framework Core migration 
commands and their options
```

**Compare:** How does the MCP-enhanced response differ from what Copilot would provide without external documentation access?

<details>
<summary><strong>�� What to Look For</strong></summary>

MCP-enhanced responses typically:
- Include **specific version details** and **current API signatures**
- Reference **official documentation** rather than relying on training data that may be outdated
- Provide **more accurate code examples** that match the latest library versions
- May include **direct links** to relevant documentation pages

</details>

### Knowledge Check

<details>
<summary><strong>When would you create a custom MCP server?</strong></summary>

You would create a custom MCP server when you need Copilot to access **proprietary or internal data sources** that aren't available through existing MCP servers. Examples:

- **Internal API documentation** — Your company's private API specs hosted on an internal wiki
- **Database schemas** — Live database metadata for generating accurate queries
- **Deployment status** — Integration with your CI/CD pipeline to check build/deploy state
- **Design systems** — Your organization's component library documentation
- **Ticketing systems** — Pull context from Jira, Azure DevOps, or internal trackers

MCP servers can be built in any language that supports JSON-RPC over stdio or SSE.

</details>

---

## 4.2 Copilot Extensions & CLI Plugins

Copilot's capabilities extend beyond MCP through two additional ecosystems: **IDE extensions** for visual editor workflows and **CLI plugins** for terminal workflows. Both bring third-party tools directly into the Copilot experience.

```
┌─────────────────────────────────────────────────────┐
│                  Copilot Ecosystem                   │
│                                                     │
│  Built-in:    @workspace  @terminal                  │
│  Agents:      @blueprint  @forge  @shield  @sage     │
│  Extensions:  @docker     @azure  @sentry  ...       │
│  MCP:         microsoft-learn  context7  ...         │
│  CLI Plugins: marketplace & Git repo plugins         │
│                                                     │
│  💡 Primary interface: Copilot CLI (`copilot`)       │
│     VS Code is used for code review only             │
└─────────────────────────────────────────────────────┘
```

### How These Differ

| Capability | Custom Agents | MCP Servers | IDE Extensions | CLI Plugins |
|------------|--------------|-------------|----------------|-------------|
| **Defined in** | `.github/agents/` | `.vscode/mcp.json` | GitHub Marketplace | CLI marketplaces / Git repos |
| **Scope** | Repository-specific | Workspace-wide | Account/Organization | Terminal / command line |
| **Purpose** | AI persona with instructions | External data/tool access | Third-party integration (visual) | Third-party integration (terminal) |
| **Invoked via** | `@agent-name` | Automatic or `@tool` | `@extension-name` in Chat | Plugin commands in CLI |
| **Examples** | @blueprint, @forge | microsoft-learn | @docker, @azure | database-data-management |

### Exercise 2: Explore the Extension & Plugin Ecosystem

**Part A — IDE Extensions:**

1. Open your browser and navigate to the **GitHub Copilot Extensions marketplace**:
   👉 [github.com/marketplace?type=apps&copilot_app=true](https://github.com/marketplace?type=apps&copilot_app=true)

2. Browse available extensions. Some popular ones include:

   | Extension | Use Case |
   |-----------|----------|
   | **@docker** | Container management, Dockerfile generation |
   | **@azure** | Azure resource management, deployment |
   | **@sentry** | Error tracking and debugging |
   | **@mermaid** | Diagram generation |

**Part B — CLI Plugins:**

3. List registered plugin marketplaces and browse available plugins:
   ```bash
   copilot plugin marketplace list
   copilot plugin marketplace browse awesome-copilot
   ```

4. View and manage installed plugins:
   ```bash
   copilot plugin list          # View installed plugins
   copilot plugin install database-data-management@awesome-copilot   # Install example
   copilot plugin uninstall NAME # Remove a plugin
   ```

> **Note:** You don't need to install any extensions or plugins for this lab — this is an exploration exercise to understand the ecosystem.

> 💡 Plugins can also be installed from any Git repository: `copilot plugin install OWNER/REPO`. The repo must contain a `plugin.json` file.

### Enterprise Considerations

When evaluating extensions or plugins for your organization, consider:

- **Approved extensions** — Enterprise admins can control which extensions are available
- **Data handling** — Understand what data the extension accesses and where it's processed
- **Security posture** — Extensions from verified publishers are preferred
- **Custom marketplaces** — Teams can create private plugin marketplaces: `copilot plugin marketplace add OWNER/REPO`

<details>
<summary><strong>📊 Knowledge Check: Extension vs Agent vs MCP vs Plugin — When Do I Use Each?</strong></summary>

| Scenario | Best Choice | Why |
|----------|------------|-----|
| "I need a code reviewer that follows our team standards" | **Custom Agent** | Repository-specific persona with tailored instructions |
| "I need to query our internal API documentation" | **MCP Server** | Standardized protocol for connecting to external data |
| "I want to manage Docker containers from Copilot Chat" | **Extension** | Third-party tool integration maintained by vendor |
| "I want Copilot to follow our coding conventions" | **Custom Instructions** | `.github/copilot-instructions.md` for baseline context |
| "I need a templated prompt for creating API endpoints" | **Reusable Prompt** | `.github/prompts/*.prompt.md` for shareable templates |
| "I need custom terminal tooling for my CI/CD pipeline" | **CLI Plugin** | Terminal-based extensibility via marketplaces or Git repos |

**Rule of thumb:**
- **Agents** = who (persona)
- **MCP** = what it knows (external data)
- **Extensions** = what it can do (third-party actions, visual)
- **Plugins** = what it can do (third-party actions, terminal)
- **Instructions** = how it behaves (baseline rules)
- **Prompts** = what to ask (templated requests)

</details>

### ✅ Checkpoint

- [ ] You browsed the extensions marketplace and understand what's available
- [ ] You can list and browse CLI plugin marketplaces
- [ ] You know the difference between agents, MCP, extensions, and CLI plugins
- [ ] You understand enterprise considerations for evaluating third-party integrations

---

## 4.3 Copilot CLI: Beyond the Basics

In **Lab 01** you learned the fundamentals of Copilot CLI's interactive and programmatic modes. Now let's use it for real workflow automation tasks.

> **Reminder:** `copilot` starts an interactive session | `copilot -p "..."` runs a single prompt

### Exercise 3: CLI Workflow Exploration

Use Copilot CLI to handle common Git workflow tasks without memorizing commands:

```bash
# Discover pull request status
copilot -p "Show me my open pull requests on this repository"

# Create a new issue directly from the terminal
copilot -p "Create a new issue titled 'Add task export to CSV' with a description about exporting task data"

# Understand recent changes
copilot -p "Explain what the last 5 commits changed"
```

**Observe:** Copilot CLI executes the tasks directly — it can interact with GitHub.com to list PRs, create issues, and more. Use `--allow-tool 'shell(git)'` to let it run git commands without prompting.

Now start an interactive session for a deeper exploration:

```bash
copilot
```

Then try these prompts in the conversation:

```
Analyze the TaskForge project architecture and identify areas for improvement

What design patterns are used in the Core layer?

Create a summary of all the service interfaces and their methods
```

> 💡 Press **Shift+Tab** to switch to **plan mode** — Copilot will build a structured plan before making changes.
>
> 💡 If your context gets long during exploration, use `/compact` to summarize the conversation and free up space.

### CLI vs IDE Copilot: When to Use Each

| Scenario | Use CLI (`copilot`) | Use IDE (VS Code) |
|----------|------------------------|----------------------|
| Quick command lookup | ✅ Primary | |
| Multi-file code generation | ✅ Primary | |
| Git/GitHub workflow | ✅ Primary | |
| Architecture design | ✅ Primary | |
| CI/CD troubleshooting | ✅ Primary | |
| Refactoring across files | ✅ Primary | |
| Code review (PR review) | | ✅ Primary |

> **Pro tip:** The Copilot CLI is the primary interface for AI-assisted development. It's a full agent that can read, write, and execute files — use it for everything from design to documentation. Reserve VS Code for code review workflows where visual diff context is valuable.

> 💡 Use `/model` to switch between available AI models during a session. Different models may perform better for different tasks — try a larger model for complex architecture decisions.

> 💡 Enable experimental features with `copilot --experimental` or `/experimental` to unlock autopilot mode, where Copilot continues working autonomously until a task is complete.

---

## 4.4 Full Workflow Orchestration: Design to Delivery

This is the **capstone exercise** that brings together everything you've learned across all four labs. You'll walk through a complete AI-assisted workflow from design to delivery.

### Scenario

> **Feature Request:** *"Add a Task Assignment Notification feature to TaskForge that alerts users when they are assigned to a task."*

We'll use the full Copilot toolkit to design, implement, and review this feature.

### The AI Pipeline

```
┌──────────┐   ┌──────────┐   ┌──────────┐
│Blueprint │──▶│  Forge/  │──▶│  Shield  │
│(Design)  │   │Copilot   │   │(Review)  │
│  [CLI]   │   │  [CLI]   │   │[VS Code] │
└──────────┘   └──────────┘   └──────────┘
     ▲                              │
     └──── Iterate if issues ───────┘
```

> **Note:** Only the Review phase uses VS Code — Design and Implement phases use the Copilot CLI as the primary interface.

---

### Exercise 4 — Phase 1: Design (Blueprint Agent + MCP)

**Goal:** Create an architectural design for the notification feature.

Start a `copilot` interactive session with the architect agent:

```bash
copilot --agent=architect
```

Then enter the following prompt:

```
Design a notification system for TaskForge that alerts users when they 
are assigned to a task. Consider the architecture implications across 
all three layers (Web, Core, Data). Include:
- Domain models needed
- Service interfaces
- Controller actions
- How it integrates with the existing Identity system
```

> 💡 For complex design questions, try `/research` to launch a deep investigation using GitHub search and web sources before committing to an architecture.

**What to observe:**
- Blueprint uses the custom instructions from `.github/copilot-instructions.md` to understand the N-tier architecture
- It may invoke MCP tools to reference ASP.NET Core Identity documentation
- The design follows the patterns established in the existing codebase

<details>
<summary><strong>✅ Verification Checkpoint</strong></summary>

Your design output should include:
- [ ] A `Notification` domain model in `TaskForge.Core`
- [ ] An `INotificationService` interface
- [ ] A `NotificationRepository` or data access pattern
- [ ] Controller actions for viewing/dismissing notifications
- [ ] Integration points with the existing `TaskItem` assignment flow

</details>

---

### Exercise 5 — Phase 2: Implement (Copilot CLI / Forge Agent)

**Goal:** Generate the implementation across multiple files.

You have two options:

**Option A — Forge Agent (guided):**

Start a `copilot` interactive session with the developer agent:

```bash
copilot --agent=developer
```

Then enter the following prompt:

```
Implement the notification system based on this design:
1. Create a Notification model in TaskForge.Core/Models/
2. Create INotificationRepository in TaskForge.Core/Interfaces/
3. Create NotificationService in TaskForge.Core/Services/
4. Create NotificationRepository in TaskForge.Data/Repositories/
5. Create NotificationController in TaskForge.Web/Controllers/
6. Register all services in DI
```

**Option B — Copilot CLI (autonomous):**

Start a `copilot` interactive session — the CLI is agentic by default, so it will autonomously create and edit files:

```bash
copilot
```

Then describe the feature:

```
Implement a task assignment notification feature for TaskForge. When a 
task is assigned to a user, create a notification. Users should be able 
to view their notifications and dismiss them. Follow the existing 
patterns in the codebase.
```

Copilot CLI will autonomously create and edit multiple files, running terminal commands as needed.

> **💡 Option C — Coding Agent (async):** For a real project, you could also create a GitHub Issue describing this feature and assign it to Copilot. The **Coding Agent** (from Lab 03) would work on it asynchronously and open a PR when done.
> You can also use `/delegate` in your CLI session to push your current context directly to the coding agent.

> 💡 Use `/fleet` to run parallel sub-agents — e.g., one generating the model/service layer while another generates the controller/views.

<details>
<summary><strong>✅ Verification Checkpoint</strong></summary>

After implementation, verify:
- [ ] New model file exists in `TaskForge.Core/Models/`
- [ ] Service interface and implementation are created
- [ ] Repository follows the existing data access pattern
- [ ] Controller uses `[Authorize]` attribute
- [ ] Services are registered in `Program.cs` or DI configuration
- [ ] Code compiles: `dotnet build` (from `src/TaskForge`)

</details>

---

### Exercise 6 — Phase 3: Review (Shield Agent + Code Review)

**Goal:** Validate the implementation for security, performance, and correctness.

In **VS Code**, open Copilot Chat and select **@shield** from the agent dropdown (code review benefits from the IDE's inline diff view):

```
Review the notification implementation for:
- Security issues (authentication, authorization, input validation)
- Performance concerns (N+1 queries, missing pagination)
- Adherence to our coding standards in copilot-instructions.md
- Proper async/await usage
- Missing error handling
```

**What to observe:**
- Shield references the coding standards from custom instructions
- It identifies issues specific to the N-tier architecture
- Suggestions are actionable and prioritized

> **In a real workflow:** You would also create a PR and use **Copilot Code Review** (from Lab 03) to get automated review comments directly on the pull request.

<details>
<summary><strong>✅ Verification Checkpoint</strong></summary>

Shield's review should cover:
- [ ] Authorization checks on notification endpoints (users can only see their own)
- [ ] Input validation on notification IDs
- [ ] Async correctness (no `.Result` or `.Wait()` calls)
- [ ] Null reference safety
- [ ] Any missing anti-forgery tokens on POST endpoints

</details>

---

### The Complete Pipeline in Action

You've just walked through the **AI-assisted development lifecycle**:

```
 📐 DESIGN              🔨 BUILD               🛡️ REVIEW
 ─────────             ──────────             ──────────
 copilot               copilot                @shield +
 --agent=              --agent=               Code Review
 architect             developer              [VS Code]
 [CLI]                 [CLI]
```

Each phase leverages a different Copilot capability, and the output of each phase feeds into the next. When Shield identifies issues, you iterate back to the Design or Build phase — creating a continuous improvement loop.

> 💡 Use `/share` to export your entire pipeline session as a markdown file or GitHub gist — a complete record of your design-to-delivery workflow.

---

## 4.5 Copilot Spaces (Bonus)

### What Are Copilot Spaces?

**Copilot Spaces** let you curate and organize context — files, documentation, notes — into a shareable collection that Copilot can reference. Think of it as a "knowledge folder" that gives Copilot focused context for better responses.

**Use cases for TaskForge:**
- Create a space with all architecture documents and design decisions
- Share a space with your team so everyone gets consistent Copilot responses
- Include external references (links, documentation) alongside code files

> **Note:** Copilot Spaces is an evolving feature. Check the [latest documentation](https://docs.github.com/en/copilot/using-github-copilot/using-copilot-spaces) for current capabilities and availability.

---

## 4.6 Best Practices & What's Next

### Feature Summary: All Labs at a Glance

| Feature | Lab(s) | Key Takeaway |
|---------|--------|--------------|
| Copilot CLI | 1, 4 | Terminal-first AI for command discovery and explanation |
| Custom Instructions | 1 | Repository-wide AI context for consistent suggestions |
| Reusable Prompts | 1, 4 | Shareable, templated prompts for common tasks |
| Custom Agents | 2 | Specialized AI personas for different roles |
| Sub-agents | 2 | Parallel execution for independent tasks |
| Copilot CLI (Agentic) | 2, 4 | Autonomous multi-file editing |
| Coding Agent | 3 | Autonomous issue-to-PR workflow |
| Code Review | 3, 4 | AI-powered PR review |
| PR Summaries | 3 | Auto-generated change descriptions |
| MCP Servers | 4 | External data and documentation access |
| Extensions & CLI Plugins | 4 | Third-party tool integrations (visual & terminal) |
| Hooks | 2 | Lifecycle hooks for custom automation triggers |
| Copilot Spaces | 4 | Curated context for better responses |

### Best Practices Checklist

Use this as a reference when adopting Copilot in your own projects:

- [ ] **Start with custom instructions** — Establish project context in `.github/copilot-instructions.md` before anything else
- [ ] **Use reusable prompts for team consistency** — Template common tasks in `.github/prompts/` so everyone gets the same quality output
- [ ] **Choose the right agent for the task** — Don't ask a reviewer to write code; don't ask a developer to design architecture
- [ ] **Always review AI-generated code** — Copilot is a powerful assistant, not a replacement for human judgment
- [ ] **Iterate and refine prompts** — If the output isn't right, improve the prompt rather than manually fixing the output
- [ ] **Use quality gates between phases** — Review the output of each pipeline phase before moving to the next
- [ ] **Leverage MCP for up-to-date information** — Connect to documentation sources for accurate, current references
- [ ] **Combine CLI and IDE** — Use each where it's strongest: CLI for commands, IDE for code
- [ ] **Explore extensions and plugins** — Browse marketplaces for tools that accelerate your workflows

### Resources for Further Learning

| Resource | Link |
|----------|------|
| GitHub Copilot Documentation | [docs.github.com/en/copilot](https://docs.github.com/en/copilot) |
| Custom Instructions Guide | [docs.github.com/en/copilot/customizing-copilot/adding-repository-custom-instructions-for-github-copilot](https://docs.github.com/en/copilot/customizing-copilot/adding-repository-custom-instructions-for-github-copilot) |
| MCP Specification | [modelcontextprotocol.io](https://modelcontextprotocol.io/) |
| Copilot Extensions Marketplace | [github.com/marketplace?type=apps&copilot_app=true](https://github.com/marketplace?type=apps&copilot_app=true) |
| GitHub Copilot Blog | [github.blog/tag/github-copilot](https://github.blog/tag/github-copilot/) |

---

## Summary

### What You Accomplished in Lab 04

✅ Configured and used **MCP servers** to extend Copilot's knowledge with live documentation
✅ Explored the **Copilot Extensions & CLI Plugins** ecosystem and understood when to use each
✅ Mastered **Copilot CLI** for git workflows and project exploration
✅ Orchestrated a **3-phase AI pipeline** — Design, Implement, Review — using the full Copilot toolkit
✅ Learned **best practices** for adopting Copilot in real projects

### What You Accomplished Across All 4 Labs

| Lab | Theme | You Learned To... |
|-----|-------|-------------------|
| **Lab 01** | The AI Design Studio | Set up Copilot CLI, custom instructions, and reusable prompts |
| **Lab 02** | Building with Your AI Team | Create custom agents, use sub-agents, and leverage Copilot CLI |
| **Lab 03** | The Autonomous Developer | Use the Coding Agent, code review, and inline suggestions |
| **Lab 04** | Orchestrating the AI Pipeline | Combine MCP, extensions, CLI plugins, CLI, and agents into a full workflow |

### Call to Action

You now have a complete toolkit for AI-assisted software development. Here's how to apply it:

1. **Start small** — Add a `copilot-instructions.md` to your current project today
2. **Build your team** — Create custom agents that match your team's roles and expertise
3. **Automate the routine** — Use reusable prompts for tasks your team does repeatedly
4. **Extend your reach** — Connect MCP servers for the documentation and tools your team relies on
5. **Iterate continuously** — Refine your prompts, agents, and instructions as you learn what works

> **🎉 Congratulations!** You've completed the GitHub Copilot Design Workshop. Go build something amazing with your AI-powered team!
