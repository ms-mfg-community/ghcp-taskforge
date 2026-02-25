# Lab 02 — Building with Your AI Team

Use custom agents, sub-agents, and the Copilot CLI to design and implement features as a coordinated AI team.

> ⏱️ Duration: ~12 minutes

References:
- [Custom Agents](https://docs.github.com/en/copilot/customizing-copilot/copilot-extensions/agents)
- [GitHub Copilot CLI](https://docs.github.com/en/copilot/github-copilot-in-the-cli)
- [GitHub Copilot Documentation](https://docs.github.com/en/copilot)

---

## 2.1 Understanding Custom Agents

### What Are Custom Agents?

Custom agents are specialized AI personas defined as Markdown files in your repository under `.github/agents/`. Each agent has a distinct role, expertise, and set of instructions that shape how it responds — just like team members with different specialties.

```
.github/agents/
├── architect.agent.md      # Blueprint — Solution Architect
├── developer.agent.md      # Forge    — .NET Developer
├── reviewer.agent.md       # Shield   — Code Reviewer
└── doc-writer.agent.md     # Sage     — Documentation Writer
```

### Anatomy of an Agent File

Agent files use **YAML frontmatter** followed by **Markdown instructions**:

```yaml
---
name: "Blueprint"
description: "Solution Architect — designs system architecture and technical specifications"
tools:
  - codebase
  - fetch
---

# Role
You are Blueprint, a senior solution architect specializing in .NET applications.

# Expertise
- N-tier architecture patterns
- Entity Framework Core design
- SOLID principles and clean architecture

# Response Approach
- Always consider scalability and maintainability
- Reference existing patterns in the codebase
- Provide architectural diagrams when helpful
```

**Key sections:**

| Section | Purpose |
|---------|---------|
| `name` | Display name used with `--agent` flag or `/agent` command |
| `description` | Brief summary of the agent's role — helps Copilot select the right agent |
| `tools` | CLI tools and MCP capabilities the agent can access |
| Main content | Detailed instructions: expertise, principles, response format |

### Exercise 1: Explore an Agent File

1. View an agent file using the CLI:
   ```
   cat .github/agents/architect.agent.md
   ```
   Or reference it in a Copilot session with `@.github/agents/architect.agent.md`
2. Examine its structure — note the YAML frontmatter fields and the instruction sections below

### Your AI Team

TaskForge comes pre-configured with four specialized agents — each modeled after a real team role:

| Agent | File | Specialty | When to Use |
|-------|------|-----------|-------------|
| 🏗️ **Blueprint** | `architect.agent.md` | Solution architecture, patterns, technical specs | Designing features, evaluating architecture, planning |
| 🔨 **Forge** | `developer.agent.md` | .NET implementation, coding standards, EF Core | Writing code, implementing features, fixing bugs |
| 🛡️ **Shield** | `reviewer.agent.md` | Security, performance, code quality review | Reviewing implementations, finding issues |
| 📖 **Sage** | `doc-writer.agent.md` | Documentation, API references, user guides | Writing docs, README files, API documentation |

### Knowledge Check

**Why use specialized agents instead of one general-purpose Copilot?**

<details>
<summary>Answer</summary>

**Specialization produces better results.** Just like a real development team:

- **Focused expertise** — An architect thinks about patterns and scalability; a reviewer thinks about security and edge cases. A single generalist would try to do everything at once and miss nuances.
- **Consistent output** — Each agent follows its own response format and principles, producing predictable, high-quality output for its domain.
- **Role separation** — An implementer shouldn't review their own code. Separate agents enforce the same separation of concerns you'd want on a real team.
- **Composability** — You can orchestrate agents in workflows: architect designs → developer implements → reviewer validates → writer documents.

</details>

> ✅ **Checkpoint:** You can locate the agent files in `.github/agents/` and identify each agent's name, description, and tools.

---

## 2.2 Your First Agent Interaction

### Exercise 2: Ask Blueprint to Analyze the Data Layer

1. Start a Copilot CLI session with the Blueprint agent:
   ```
   copilot --agent=architect
   ```
   Or use a one-shot command:
   ```
   copilot --agent=architect -p "Analyze the data layer in src/TaskForge/TaskForge.Data/. Evaluate the entity relationships and suggest any architectural improvements."
   ```

> �� Use `/model` during a session to switch the underlying AI model. Different models may perform better for different agent tasks.

2. If using an interactive session, enter this prompt:

```
Analyze the data layer in src/TaskForge/TaskForge.Data/.
Evaluate the entity relationships and suggest any architectural improvements.
```

3. Observe Blueprint's response — it should provide:
   - An overview of the current entity model (Project, TaskItem, Comment, Label, TaskLabel, ApplicationUser)
   - Analysis of the relationships (one-to-many, many-to-many via join table)
   - Architectural recommendations

<details>
<summary>Expected Output Format</summary>

Blueprint should produce something like:

```
## Data Layer Analysis — TaskForge.Data

### Entity Overview
- Project → has many TaskItems (cascade delete)
- TaskItem → belongs to Project, assigned to ApplicationUser
- Comment → belongs to TaskItem, authored by ApplicationUser
- Label ↔ TaskItem (many-to-many via TaskLabel join entity)
- ApplicationUser → extends IdentityUser, owns Projects and Comments

### Relationship Assessment
✅ Composite key on TaskLabel (correct many-to-many pattern)
✅ Restrict delete on user references (prevents orphaned data)
✅ Cascade delete from Project → Tasks and Task → Comments
✅ Indexes on Status, Priority, and ProjectId columns

### Recommendations
1. Add a service/repository layer in TaskForge.Core for business logic
2. Consider adding audit fields (UpdatedBy) for traceability
3. Add soft-delete support (IsDeleted flag) for data retention
4. Consider adding a ProjectMember entity for team-based access control
```

Your actual output will vary — the key is that Blueprint focuses on **architecture and patterns**, not implementation details.

</details>

> 💡 **Try another agent:** Run `copilot --agent=developer` with the same data-layer prompt and compare. Forge focuses on **concrete code** — interface definitions, method signatures — while Blueprint provides **design guidance** and trade-offs. The same codebase looks different through each agent's lens.

> ✅ **Checkpoint:** You have interacted with an agent and observed how its specialized focus shapes the response.

---

## 2.3 Agentic Capabilities: Autonomous Development

### What Is Agentic Copilot?

The Copilot CLI is **agentic by default** — it can autonomously **create, edit, and delete files** across your project, run terminal commands, and iterate on its work. There's no mode to switch into; every CLI session has full agentic capabilities from the start. Think of it as handing the keyboard to a developer who can read your code, make changes, and verify their work.

```
┌─────────────────────────────────────────────────────────┐
│              Copilot CLI Capabilities                     │
├─────────────────────────────────────────────────────────┤
│                                                          │
│  🔍 Read & understand your codebase                      │
│  📐 Plan multi-file changes                              │
│  📄 Create, edit, and delete files                       │
│  🔧 Run terminal commands (build, test, lint)            │
│  🔄 Iterate based on errors and feedback                 │
│  🤖 All capabilities available by default — no modes     │
│                                                          │
└─────────────────────────────────────────────────────────┘
```

### What Makes It "Agentic"?

| Capability | Description |
|------------|-------------|
| **File changes** | Creates, edits, and deletes files autonomously |
| **Scope** | Multi-file creation and editing across your project |
| **Autonomy** | Plans and executes — decides which files to touch |
| **Terminal access** | Runs commands like `dotnet build` to verify its work |
| **Iteration** | Detects errors and self-corrects without prompting |

### Exercise 3: Build a Service with Copilot CLI

1. Start an interactive Copilot CLI session (CLI is agentic by default — no mode switching needed):
   ```
   copilot
   ```

2. Enter this prompt:

```
Create a ProjectService in the TaskForge.Core project that implements
IProjectService. Include CRUD operations, input validation, and proper
error handling. Follow the existing project patterns and architecture.
```

3. **Watch the Copilot CLI work** — observe how it:

```
┌──────────────────────────────────────────────────────────────┐
│                Copilot CLI Workflow                            │
├──────────────────────────────────────────────────────────────┤
│                                                               │
│  1. 🔍 Reads existing code to understand patterns             │
│     └── Examines Models/, ApplicationDbContext.cs             │
│                                                               │
│  2. 📐 Plans the changes                                      │
│     └── Determines which files to create/modify               │
│                                                               │
│  3. 📄 Creates the interface                                  │
│     └── IProjectService.cs in TaskForge.Core                 │
│                                                               │
│  4. 📄 Creates the implementation                             │
│     └── ProjectService.cs in TaskForge.Core                  │
│                                                               │
│  5. 🔧 Updates dependency injection                           │
│     └── Registers service in Program.cs                      │
│                                                               │
│  6. 🖥️  May run terminal commands                             │
│     └── dotnet build to verify compilation                   │
│                                                               │
└──────────────────────────────────────────────────────────────┘
```

4. **Review the proposed changes** before accepting:
   - Does the interface define the expected CRUD methods?
   - Does the implementation use `ApplicationDbContext` correctly?
   - Is input validation present (null checks, string length)?
   - Are services registered in the DI container?

5. **Accept or modify** — you can accept all changes, reject specific files, or ask Copilot to revise

<details>
<summary>Expected Files Created</summary>

Copilot CLI should create or modify files similar to:

**`TaskForge.Core/Interfaces/IProjectService.cs`**
```csharp
public interface IProjectService
{
    Task<Project> GetByIdAsync(int id);
    Task<IEnumerable<Project>> GetAllAsync();
    Task<Project> CreateAsync(Project project);
    Task<Project> UpdateAsync(Project project);
    Task DeleteAsync(int id);
}
```

**`TaskForge.Core/Services/ProjectService.cs`**
```csharp
public class ProjectService : IProjectService
{
    private readonly ApplicationDbContext _context;

    public ProjectService(ApplicationDbContext context)
    {
        _context = context;
    }

    // CRUD implementations with validation...
}
```

**`TaskForge.Web/Program.cs`** (modified)
```csharp
builder.Services.AddScoped<IProjectService, ProjectService>();
```

Your actual output may vary in structure and naming — the key is that Copilot CLI handles **multiple files autonomously**.

</details>

> ✅ **Checkpoint:** Copilot CLI created at least an interface and implementation file, and you reviewed the changes before accepting.

---

## 2.4 Sub-Agents and Parallel Workflows

### What Are Sub-Agents?

When you give Copilot a complex prompt that involves multiple independent tasks, it can spawn **sub-agents** — isolated agents that work in parallel and return their results to the main conversation. This is like delegating work to team members who report back when they're done.

```
┌──────────────────────────────────────────────────────────┐
│                  Sub-Agent Execution                      │
├──────────────────────────────────────────────────────────┤
│                                                           │
│          Your Prompt (multiple tasks)                     │
│                    │                                      │
│          ┌─────────┼─────────┐                            │
│          ▼         ▼         ▼                            │
│     ┌─────────┐ ┌─────────┐ ┌─────────┐                  │
│     │ Agent 1 │ │ Agent 2 │ │ Agent 3 │  ← run in        │
│     │(Models) │ │(Services│ │ (Web)   │    parallel       │
│     └────┬────┘ └────┬────┘ └────┬────┘                   │
│          │           │           │                         │
│          └─────────┬─┘───────────┘                        │
│                    ▼                                      │
│          Combined Results                                 │
│          (back in your chat)                              │
│                                                           │
└──────────────────────────────────────────────────────────┘
```

### When Sub-Agents Help

| Scenario | Why Parallel? |
|----------|--------------|
| Analyzing different code areas | Each area is independent |
| Reviewing multiple files | Reviews don't depend on each other |
| Generating docs for separate features | No shared state needed |
| Running independent validations | Results are combined at the end |

### Exercise 4: Parallel Codebase Analysis

> 💡 **Fleet Mode:** Use the `/fleet` command to enable parallel sub-agent execution. This dispatches multiple agents simultaneously — ideal when tasks are independent (e.g., one agent writes tests while another writes documentation).

1. In the Copilot CLI, enter this prompt:

```
Analyze the following areas of the TaskForge codebase in parallel:
1. Review the data models in TaskForge.Data/Models/ for completeness
   and best practices
2. Check the service layer in TaskForge.Core/ for proper dependency
   injection patterns
3. Evaluate TaskForge.Web/ for security best practices
```

2. Observe the execution:
   - Copilot may spawn sub-agents for each analysis area
   - Results from all three areas return to your main conversation
   - Each analysis is independent — they don't wait for each other

3. Review the combined output — you should see findings organized by area

<details>
<summary>Expected Output Structure</summary>

Results should be organized by the three areas:

```
## 1. Data Models Review (TaskForge.Data/Models/)
- ✅ Proper use of data annotations ([Required], [MaxLength])
- ✅ Navigation properties correctly defined
- ✅ Many-to-many via explicit join entity (TaskLabel)
- ⚠️ No soft-delete mechanism
- ⚠️ DateTime.UtcNow in property initializers (should use a service)

## 2. Service Layer Review (TaskForge.Core/)
- ⚠️ Core project is mostly empty (only Class1.cs)
- ⚠️ No interfaces or services defined yet
- 💡 Needs IProjectService, ITaskService, etc.
- 💡 Should reference TaskForge.Data for DbContext access

## 3. Web Layer Security Review (TaskForge.Web/)
- ✅ ASP.NET Core Identity configured
- ✅ HTTPS enforced in production
- ⚠️ No [Authorize] attributes on controllers
- ⚠️ No CSRF protection verification needed
- ⚠️ No input sanitization in views
```

</details>

### Orchestration Patterns

Understanding when to run tasks sequentially, in parallel, or in a hybrid pattern:

```
Sequential           Parallel              Hybrid
─────────           ────────              ──────

A ──▶ B ──▶ C      ┌── A ──┐            A ──▶ ┌── B ──┐ ──▶ D
                    ├── B ──┤                   └── C ──┘
Each step           └── C ──┘            Analyze first,
depends on          All independent,     then parallel tasks,
the previous.       combined at end.     then synthesize.
```

| Pattern | Use When | Example |
|---------|----------|---------|
| **Sequential** | Output of one feeds into the next | Design → Implement → Review |
| **Parallel** | Tasks are fully independent | Analyze Models + Analyze Controllers + Analyze Views |
| **Hybrid** | Some tasks depend, others don't | Analyze → [Implement Service ‖ Write Tests] → Review |

> ✅ **Checkpoint:** You ran a parallel analysis and received combined results from multiple independent tasks.

---

## 2.5 Copilot Agent Hooks: Automating the Guardrails

While custom agents define *what* AI can do, **hooks** define *when* custom actions run during an agent session. Hooks are shell scripts triggered at key lifecycle events — think of them as middleware for your AI workflow.

### What Are Hooks?

Hooks are configured in `.github/hooks/hooks.json` and execute custom shell commands at these trigger points:

| Hook | When It Fires | Can Block? | Use Case |
|------|--------------|------------|----------|
| `sessionStart` | Agent session begins | No | Logging, environment setup |
| `sessionEnd` | Agent session ends | No | Cleanup, reporting |
| `userPromptSubmitted` | User sends a prompt | No | Audit trail, compliance |
| `preToolUse` | Before agent uses a tool | **Yes** | Security gates, policy enforcement |
| `postToolUse` | After a tool completes | No | Quality checks, metrics |
| `errorOccurred` | An error happens | No | Alerts, error logging |

> **Key insight:** The `preToolUse` hook is the most powerful — it can **deny** tool executions, acting as a security gate for your AI agent.

### Exercise 5: Explore the Hooks Configuration

1. Open `.github/hooks/hooks.json` in the repository and examine the structure:

```json
{
  "version": 1,
  "hooks": {
    "preToolUse": [
      {
        "type": "command",
        "bash": "./scripts/hooks/security-check.sh",
        "powershell": "./scripts/hooks/security-check.ps1",
        "cwd": ".",
        "timeoutSec": 15
      }
    ]
  }
}
```

2. Note the key fields:
   - `type`: Always `"command"` for shell scripts
   - `bash` / `powershell`: Platform-specific script paths
   - `cwd`: Working directory for the script
   - `timeoutSec`: Maximum execution time (default: 30s)

3. Now open `scripts/hooks/security-check.sh` and trace how the `preToolUse` security gate works:

```bash
#!/bin/bash
INPUT=$(cat)  # Read JSON input from stdin
TOOL_NAME=$(echo "$INPUT" | jq -r '.toolName')
TOOL_ARGS=$(echo "$INPUT" | jq -r '.toolArgs')

# Block dangerous commands
if echo "$TOOL_ARGS" | grep -qE "rm -rf /|DROP TABLE|format|sudo rm"; then
  echo '{"permissionDecision":"deny","permissionDecisionReason":"Dangerous command blocked by security hook"}'
  exit 0
fi
```

4. Identify the pattern: the hook receives JSON via stdin → inspects for dangerous patterns → outputs a `deny` decision or exits silently to allow

### Hooks in the Design Process

In our "AI in the Design Process" theme, hooks serve as **quality gates**:

```
┌─────────────┐    ┌──────────────┐    ┌─────────────┐
│ Agent wants  │───▶│ preToolUse   │───▶│ Tool runs   │
│ to edit code │    │ hook checks  │    │ if approved  │
└─────────────┘    │ security     │    └─────────────┘
                   └──────┬───────┘
                          │ deny
                          ▼
                   ┌──────────────┐
                   │ Agent gets   │
                   │ denial reason│
                   │ & adapts     │
                   └──────────────┘
```

<details>
<summary>Knowledge Check: When would you use each hook type?</summary>

- **sessionStart**: Initialize project-specific variables, log who started the session
- **preToolUse**: Enforce security policies, restrict file access, block dangerous operations
- **postToolUse**: Run linters after code changes, collect metrics on tool usage
- **userPromptSubmitted**: Audit trail for compliance, detect sensitive prompts
- **errorOccurred**: Alert the team via Slack/email, log for debugging
- **sessionEnd**: Generate session summary reports, cleanup temp files

</details>

> ✅ **Checkpoint:** You can locate and explain the hooks.json configuration, identify the 6 hook trigger types, and understand that `preToolUse` is the only hook that can block actions.

---

## 2.6 Building a Feature with Your AI Team

Now let's bring it all together — use multiple agents in sequence to design and implement a feature, just like a real development team.

### The Workflow

```
┌───────────────────────────────────────────────────┐
│      Multi-Agent Feature Development Pipeline      │
├───────────────────────────────────────────────────┤
│                                                    │
│  Step 1                    Step 2                  │
│  ┌─────────┐               ┌─────────┐            │
│  │Blueprint│──────────────▶│  Forge  │            │
│  │ Design  │               │  Build  │            │
│  └─────────┘               └─────────┘            │
│     🏗️                        🔨                  │
│   Architect                 Developer              │
│                                                    │
└───────────────────────────────────────────────────┘
```

### Exercise 6: Design and Build a Dashboard Feature

**Step 1: Design with Blueprint 🏗️**

1. Start a Copilot CLI session with the Blueprint agent: `copilot --agent=architect`
2. Enter:

```
Design a "Task Dashboard" feature for TaskForge. The dashboard should
display a summary of tasks grouped by status (Todo, InProgress, InReview,
Done) with counts and recent activity. Define the components needed
across all layers (Data, Core, Web) and the contracts between them.
```

3. Review Blueprint's design — it should include:
   - A data query strategy (what to fetch from the database)
   - A service interface definition (what the Core layer exposes)
   - A view model structure (what the Web layer needs)
   - Component interaction diagram

<details>
<summary>Expected Blueprint Output</summary>

Blueprint should produce a design similar to:

```
## Task Dashboard — Architectural Design

### Data Layer
- Query: Group TaskItems by Status, count per group
- Query: Fetch 5 most recently updated TaskItems
- No new entities needed — use existing TaskItem model

### Core Layer (new files)
- IDashboardService interface
  - GetTaskSummaryAsync() → TaskSummaryDto
  - GetRecentActivityAsync(int count) → IEnumerable<TaskItem>
- TaskSummaryDto (Todo/InProgress/InReview/Done counts)

### Web Layer (new files)
- DashboardController with Index action
- DashboardViewModel (summary + recent tasks)
- Views/Dashboard/Index.cshtml

### Component Interaction
Controller → IDashboardService → ApplicationDbContext
              ↓
         TaskSummaryDto
         + Recent TaskItems
              ↓
       DashboardViewModel → View
```

</details>

**Step 2: Implement with Forge 🔨**

> 💡 Between agent sessions, use `/compact` to summarize your conversation and free up context window space before switching to the next agent.

1. Start a Copilot CLI session with the Forge agent: `copilot --agent=developer`
2. Enter:

```
Based on the dashboard design, create the IDashboardService interface
and DashboardService implementation in TaskForge.Core. The service
should query TaskItems grouped by status and return summary counts.
Include a DTO for the dashboard data.
```

3. Review the generated code for correctness

> 💡 **Extending the pipeline:** In a real workflow you'd continue with Shield (`--agent=reviewer`) for code review and Sage (`--agent=doc-writer`) for documentation — the same sequential pattern, with each agent's specialty applied to the output of the previous step. Use `/share` to export your full multi-agent session as a markdown file or GitHub gist.

### Reflection

Consider how each agent contributed to the feature:

| Agent | Contribution | Analogy |
|-------|-------------|---------|
| Blueprint | Defined the architecture and contracts | Architect drawing blueprints |
| Forge | Wrote the implementation code | Developer building from specs |
| Shield *(next step)* | Would find bugs and security issues | Code reviewer in a PR |
| Sage *(next step)* | Would create documentation for the team | Technical writer |

**Could one agent have done all of this?** Yes — but the results would blend concerns. Specialized agents produce sharper, more focused output at each stage.

> ✅ **Checkpoint:** You used Blueprint and Forge in sequence to design and build a feature, experiencing the multi-agent handoff workflow.

---

## Summary

In this lab you learned:

- **Custom agents** are Markdown files in `.github/agents/` with YAML frontmatter defining name, description, tools, and instructions
- **Each agent has a distinct specialty** — Blueprint designs, Forge builds, Shield reviews, Sage documents
- **Copilot CLI** enables autonomous multi-file editing — Copilot decides which files to create, edit, and wire together
- **Sub-agents** run independent tasks in parallel, combining results in your main conversation
- **Multi-agent workflows** chain specialized agents sequentially: design → implement → review → document
- **Copilot Agent Hooks** are shell scripts triggered at lifecycle events (like `preToolUse`) that act as security gates and quality checks for your AI workflow
- **Orchestration patterns** (sequential, parallel, hybrid) match how your tasks depend on each other

> 💡 **Going further:** For larger features, use `/delegate` to push your CLI session to the Copilot coding agent on GitHub. It continues the work autonomously and opens a PR when done.

**Next:** [Lab 03 — The Autonomous Developer](lab03.md) — Leverage the Copilot Coding Agent for issue-to-PR automation, code review, and inline suggestions.