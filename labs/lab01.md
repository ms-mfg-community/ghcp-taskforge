# 1 - The AI Design Studio

In this lab you will use GitHub Copilot to design the foundation of a software application — from project setup to domain model design — entirely with AI assistance. You'll discover how Copilot works beyond the editor: in your terminal, through reusable prompts, and with custom instructions that keep your entire team on the same page.

> Duration: ~10 minutes

References:

- [About GitHub Copilot CLI](https://docs.github.com/en/copilot/concepts/agents/about-copilot-cli)
- [Installing Copilot CLI](https://docs.github.com/en/copilot/how-tos/copilot-cli/set-up-copilot-cli/install-copilot-cli)
- [Custom Instructions for Copilot](https://docs.github.com/en/copilot/customizing-copilot/adding-repository-custom-instructions-for-github-copilot)
- [Reusable Prompt Files](https://docs.github.com/en/copilot/customizing-copilot/adding-repository-custom-instructions-for-github-copilot#repository-level-prompt-files)
- [Custom Agents for Copilot CLI](https://docs.github.com/en/copilot/customizing-copilot/copilot-extensions/building-copilot-extensions)

---

## 1.1 Meet Your AI Design Partner: Copilot CLI

GitHub Copilot isn't just an editor feature — it lives in your terminal too. **Copilot CLI** is a standalone AI agent you can interact with directly from the command line.

| Mode | How to use | Description |
|------|-----------|-------------|
| Interactive | `copilot` | Start a conversation — ask questions, request code changes, explore your project |
| Programmatic | `copilot -p "prompt"` | Pass a single prompt, Copilot completes the task and exits |

Think of it as having a senior developer pair-programming with you, right in your terminal.

> 💡 By default, Copilot CLI uses Claude Sonnet 4.5. Run `/model` during a session to switch between available models including Claude Sonnet 4 and GPT-5.

### Exercise 1 — Start an interactive session

Open your terminal in the TaskForge project directory and start Copilot CLI:

```
copilot
```

You'll see a welcome message and a prompt. Ask Copilot about the project:

```
What dotnet command would I use to create a new ASP.NET Core MVC project with individual authentication?
```

Copilot will suggest the command and explain the flags. You can ask follow-up questions, or type `/exit` to leave the session.

> 💡 Press **Shift+Tab** to cycle between modes: **interactive** (default) → **plan** (structured implementation plan). Enable experimental features with `/experimental` to unlock **autopilot mode**, where Copilot continues working until a task is complete.

### Exercise 2 — Use programmatic mode

For quick, one-off questions, use the `-p` flag:

```bash
copilot -p "Explain what the command 'dotnet new mvc --auth Individual --use-local-db' does"
```

Copilot explains each flag and exits. This is perfect for scripting or quick lookups.

### Exercise 3 — Interact with GitHub

Copilot CLI can also interact with GitHub.com:

```bash
copilot -p "List my open pull requests"
```

> ✅ **Checkpoint:** You successfully used both interactive and programmatic modes of Copilot CLI.

### Knowledge Check

<details>
<summary>❓ When would you use interactive vs programmatic mode?</summary>

- Use **interactive mode** (`copilot`) when you want an ongoing conversation — exploring a codebase, iterating on changes, or working through a multi-step task with back-and-forth guidance.
- Use **programmatic mode** (`copilot -p "..."`) when you need a quick, one-shot answer — explaining a command, looking up a Git workflow, or scripting Copilot into a CI pipeline.

A good rule of thumb:
```
I want to EXPLORE or ITERATE  →  copilot (interactive)
I want a QUICK ANSWER         →  copilot -p "..."
```

</details>

---

## 1.2 Establishing Your AI's Context: Custom Instructions

Every team has conventions — naming patterns, architecture rules, preferred libraries. Without context, Copilot gives generic suggestions. **Custom instructions** let you teach Copilot your team's standards so every suggestion is on-brand from the start.

There are two flavors:

```
.github/
├── copilot-instructions.md              ← Always included in every Copilot interaction
└── instructions/
    └── csharp.instructions.md           ← Conditionally included based on file type
```

### Global Instructions: `copilot-instructions.md`

This file is **always** sent to Copilot as context — every chat, every completion, every suggestion. It's the place for project-wide standards.

### Exercise 4 — Explore the global instructions

Start a Copilot CLI session and use the `/instructions` command to see which instruction files are loaded:

```
copilot
```

Then type:

```
/instructions
```

This shows the custom instruction files Copilot has automatically loaded from your repo. You can also reference the file directly to read its contents:

```
@.github/copilot-instructions.md Summarize the key standards defined in this file.
```

Notice how the instructions define:

- **Architecture** — N-tier layers (Web → Core ← Data)
- **Tech stack** — .NET 10, EF Core, ASP.NET Core Identity
- **Coding standards** — file-scoped namespaces, async/await, DI patterns
- **Testing** — xUnit, Moq, Arrange/Act/Assert
- **Security** — input validation, anti-forgery tokens, parameterized queries

> 💡 This file acts as a persistent "system prompt" for Copilot. Everything in here shapes every response — including in the CLI. Custom instructions are **automatically loaded** whenever you start a Copilot session in a repo that contains them.

### Conditional Instructions: `*.instructions.md`

Sometimes instructions should only apply to certain files. That's where the `applyTo` front matter comes in.

### Exercise 5 — Examine the C# instructions

Still in your Copilot CLI session, reference the C# instructions file to explore it:

```
@.github/instructions/csharp.instructions.md What conventions does this file define? Does it have an applyTo pattern?
```

Copilot will show you the YAML front matter:

```yaml
---
description: "C# coding standards for the TaskForge project"
applyTo: "**/*.cs"
---
```

The `applyTo: "**/*.cs"` glob means these instructions are **only** included when you're working with C# files. This keeps Copilot's context focused — no C# conventions cluttering your JavaScript or Markdown work.

Key conventions defined here include:

| Convention | Example |
|-----------|---------|
| File-scoped namespaces | `namespace TaskForge.Core.Services;` |
| Private field naming | `_camelCase` with underscore prefix |
| Async suffix | `GetByIdAsync` |
| Interface prefix | `ITaskService` |
| Sealed by default | `sealed class` unless inheritance is needed |

### Exercise 6 — See instructions in action

In your Copilot CLI session, reference a C# file and ask a design question:

```
@src/TaskForge/TaskForge.Data/Models/TaskItem.cs How should I add a new property to track estimated hours on a TaskItem?
```

Notice how Copilot's response follows the conventions from the custom instructions:
- Uses `/// <summary>` XML documentation
- Follows the naming conventions
- Suggests data annotations for validation

> 💡 The CLI automatically loads both global and conditional instructions — no extra setup needed. When you reference a `.cs` file, the C#-specific instructions kick in.

> ✅ **Checkpoint:** You can see that Copilot's suggestions align with the project's coding standards, right from the CLI.

### Knowledge Check

<details>
<summary>❓ Why do custom instructions improve team consistency?</summary>

Without custom instructions, every developer gets slightly different suggestions from Copilot based on how they phrase their questions. Custom instructions solve this by:

1. **Enforcing standards automatically** — Copilot always knows to use `file-scoped namespaces`, `async/await`, and your naming conventions.
2. **Reducing code review friction** — When Copilot suggests code that already follows your team's patterns, PRs need fewer style corrections.
3. **Onboarding new developers** — New team members get project-consistent suggestions from day one, even before they've memorized the style guide.
4. **Shared context** — The instructions are committed to the repo, so every team member — and every Copilot instance — uses the same rules.

Think of it as pair programming with a colleague who has read and memorized your entire style guide.

</details>

---

## 1.3 Reusable Prompts: Your Design Templates

Custom instructions tell Copilot *how* to write code. **Reusable prompts** tell Copilot *what* to do — they're like templates you can invoke whenever you need a specific kind of output.

```
.github/
└── prompts/
    ├── design-domain-model.prompt.md    ← Design entities & relationships
    ├── write-unit-tests.prompt.md       ← Generate xUnit tests
    ├── create-api-endpoint.prompt.md    ← Scaffold a CRUD controller
    └── review-code.prompt.md            ← Perform a code review
```

Each `.prompt.md` file contains a structured template that produces consistent, high-quality output every time.

### Exercise 7 — Explore the design-domain-model prompt

Reusable prompt files (`.prompt.md`) are designed for use in VS Code and other IDEs, where you can invoke them with the `#` selector. In the CLI, you can't invoke them directly — but you can **read them** to understand the prompt template, then use the content in your CLI conversation.

In your Copilot CLI session, reference the prompt file:

```
@.github/prompts/design-domain-model.prompt.md Explain what this prompt template asks for.
```

Notice its structure:

```yaml
---
description: "Design a domain model with entities, relationships, and validation rules"
---
```

The prompt template asks for five deliverables:

```
1. Entities          → Class names, properties, data types
2. Relationships     → One-to-one, one-to-many, many-to-many
3. Validation Rules  → Required fields, constraints, business rules
4. EF Core Config    → Fluent API, indexes, cascade behavior
5. C# Classes        → Complete code following TaskForge conventions
```

> 💡 Prompt files are checked into your repo alongside your code. This means your team shares the same "playbook" for common tasks. In the CLI, you reference them with `@` to read their content, then apply the template manually. In VS Code, you can invoke them directly with the `#` selector.

### Exercise 8 — Use the prompt to design the TaskForge domain

Now apply the design-domain-model template in a CLI conversation. Reference both the prompt file and relevant source files to give Copilot full context:

```
@.github/prompts/design-domain-model.prompt.md @src/TaskForge/TaskForge.Data/Models/TaskItem.cs Follow the design-domain-model prompt template. Design a domain model for a task management application with the following entities: Projects, Tasks, Comments, Labels, and User Assignments. A project contains many tasks. Tasks can have comments, labels (many-to-many), and be assigned to users. Track creation and modification timestamps on all entities.
```

Review the output — Copilot should produce entity classes, relationships, and validation rules that align with the existing models in `TaskForge.Data/Models/`.

You should see output that looks something like this (your results may vary):

```
┌─────────────┐       ┌─────────────┐
│   Project    │ 1───* │  TaskItem   │
│─────────────│       │─────────────│
│ Name         │       │ Title       │
│ Description  │       │ Description │
│ Status       │       │ Priority    │
│ CreatedAt    │       │ Status      │
│ CreatedById  │       │ DueDate     │
└─────────────┘       │ AssigneeId  │
                      └──────┬──────┘
                         1│     │*
                          │     │
                    ┌─────┘     └──────┐
                    │*                 │*
              ┌─────┴─────┐    ┌──────┴──────┐
              │  Comment   │    │  TaskLabel  │
              │───────────│    │  (join)     │
              │ Content    │    │─────────────│
              │ CreatedAt  │    │ TaskItemId  │
              │ AuthorId   │    │ LabelId     │
              └───────────┘    └──────┬──────┘
                                      │*
                               ┌──────┴──────┐
                               │    Label    │
                               │─────────────│
                               │ Name        │
                               │ Color       │
                               └─────────────┘
```

> ✅ **Checkpoint:** You successfully invoked a reusable prompt and received a structured domain model design.

### Exercise 9 — Try the write-unit-tests prompt

Use the same approach to apply the write-unit-tests prompt in the CLI:

```
@.github/prompts/write-unit-tests.prompt.md @src/TaskForge/TaskForge.Data/Models/TaskItem.cs Follow the write-unit-tests prompt template. Write unit tests for the TaskItem class focusing on validation rules and property constraints.
```

Observe how the generated tests follow the conventions from `write-unit-tests.prompt.md`:
- `MethodName_Scenario_ExpectedResult` naming
- Arrange/Act/Assert structure
- Both happy path and edge case coverage

---

## 1.4 Designing with AI: The Domain Model

Now let's put it all together. In this section, you'll have an **iterative design conversation** with Copilot — the same way you'd whiteboard with a colleague, except your colleague has read every line of your project's instructions.

### Exercise 10 — Iterative domain design

Start a fresh Copilot CLI session (or continue your existing one) and walk through this design conversation. After each prompt, read Copilot's response before moving to the next one.

**Step 1 — Explore relationships:**

```
What relationships should exist between the Project and TaskItem entities in
our TaskForge application? Consider ownership, cascading deletes, and
navigation properties.
```

**Step 2 — Tackle the many-to-many:**

```
How should we handle the many-to-many relationship between TaskItem and Label?
Should we use a join entity or let EF Core handle it implicitly? What are the
trade-offs?
```

**Step 3 — Define validation:**

```
What validation rules should the TaskItem entity have? Consider required fields,
string lengths, date constraints, and status transitions.
```

> 💡 Notice how Copilot references the N-tier architecture, uses `file-scoped namespaces`, and suggests patterns consistent with the existing `TaskForge.Data/Models` classes. That's the custom instructions at work.

> 💡 If your context window fills up during a long design conversation, use `/compact` to summarize the history and free up space.

### Exercise 11 — Compare AI-assisted design with manual design

Take a moment to reflect on the design process you just went through:

<details>
<summary>🤔 How did AI-assisted design differ from designing manually?</summary>

**Speed** — You explored entity relationships, validation rules, and EF Core configuration in minutes instead of hours of documentation reading.

**Consistency** — Because of the custom instructions, every suggestion followed the same conventions. No style drift between entities.

**Completeness** — Copilot surfaced considerations you might have missed: cascade delete behavior, index strategies, nullable reference types.

**Traceability** — The chat history is a design log. You can revisit *why* you made certain decisions.

**But keep in mind:**
- AI suggestions are a starting point, not a final answer. Always review critically.
- Domain expertise still matters — Copilot doesn't know your business rules unless you tell it.
- The quality of the output depends on the quality of your instructions and prompts.

</details>

---

## Summary

In this lab you learned how to use GitHub Copilot as a design partner — entirely from the CLI:

- ✅ **Copilot CLI** — Used interactive and programmatic modes to explore commands and interact with GitHub, right in your terminal
- ✅ **Custom Instructions** — Used `/instructions` and `@` file references to explore how `copilot-instructions.md` provides global context and `*.instructions.md` files add conditional, file-type-specific guidance — all auto-loaded in the CLI
- ✅ **Reusable Prompts** — Referenced `.prompt.md` files with `@` to understand prompt templates, then applied them directly in CLI conversations for domain design and test generation
- ✅ **Iterative Design** — Had an iterative design conversation in the CLI to shape the TaskForge domain model, with Copilot respecting your project's architecture and coding standards

> 💡 Use `/share` to export your design session as a markdown file or GitHub gist — great for sharing design decisions with your team.

**Key takeaway:** Copilot CLI gives you the full power of AI-assisted development without leaving your terminal. Custom instructions are auto-loaded, files are referenced with `@`, and your entire design workflow can happen in a single CLI session.

> **Next:** In [Lab 02](lab02.md) you will move from design to implementation — building out the TaskForge service layer with Copilot's help.
