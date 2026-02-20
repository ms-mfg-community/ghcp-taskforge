# 1 - The AI Design Studio

In this lab you will use GitHub Copilot to design the foundation of a software application — from project setup to domain model design — entirely with AI assistance. You'll discover how Copilot works beyond the editor: in your terminal, through reusable prompts, and with custom instructions that keep your entire team on the same page.

> Duration: ~10 minutes

References:

- [GitHub Copilot in the CLI](https://docs.github.com/en/copilot/github-copilot-in-the-cli)
- [Custom Instructions for Copilot](https://docs.github.com/en/copilot/customizing-copilot/adding-repository-custom-instructions-for-github-copilot)
- [Reusable Prompt Files](https://docs.github.com/en/copilot/customizing-copilot/adding-repository-custom-instructions-for-github-copilot#repository-level-prompt-files)
- [Copilot Chat in VS Code](https://docs.github.com/en/copilot/using-github-copilot/copilot-chat/using-github-copilot-chat-in-your-ide)

---

## 1.1 Meet Your AI Design Partner: Copilot CLI

GitHub Copilot isn't just an editor feature — it lives in your terminal too. **Copilot CLI** gives you two powerful commands:

| Command | What it does |
|---------|-------------|
| `ghcs` | **Copilot Suggest** — Describe what you want to do in plain English and get a shell command back. |
| `ghce` | **Copilot Explain** — Paste a command you don't understand and get a human-readable explanation. |

Think of `ghcs` as *"I know what I want but not the command"* and `ghce` as *"I see this command but don't know what it does."*

### Exercise 1 — Suggest a command

Open your terminal and ask Copilot to suggest a `dotnet` command:

```
ghcs "create a new ASP.NET Core MVC project with individual authentication"
```

Copilot will respond with a suggested command like:

```
dotnet new mvc --auth Individual --use-local-db
```

> 💡 You can accept, revise, or reject the suggestion. Copilot CLI is interactive — it will ask what you'd like to do next.

### Exercise 2 — Explain a command

Now let's go the other direction. Suppose you encounter the command from Exercise 1 and want to understand every flag:

```
ghce "dotnet new mvc --auth Individual --use-local-db"
```

You should see output that breaks down each part:

```
This command creates a new ASP.NET Core MVC project with:
  • --auth Individual  →  Configures ASP.NET Core Identity for user login/registration
  • --use-local-db     →  Uses SQLite (LocalDB) instead of SQL Server for the identity store
```

> ✅ **Checkpoint:** You successfully used both `ghcs` and `ghce` in your terminal.

### Knowledge Check

<details>
<summary>❓ When would you use <code>ghcs</code> vs <code>ghce</code>?</summary>

- Use **`ghcs`** (suggest) when you know *what* you want to accomplish but aren't sure of the exact command or flags. Example: *"How do I scaffold an EF Core migration?"*
- Use **`ghce`** (explain) when you see an unfamiliar command — perhaps in a README, a CI pipeline, or a teammate's script — and want to understand what it does before running it.

A good rule of thumb:
```
I want to DO something   →  ghcs
I want to UNDERSTAND something  →  ghce
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

### Exercise 3 — Explore the global instructions

Open `.github/copilot-instructions.md` in your editor and read through it. Notice how it defines:

- **Architecture** — N-tier layers (Web → Core ← Data)
- **Tech stack** — .NET 8, EF Core, ASP.NET Core Identity
- **Coding standards** — file-scoped namespaces, async/await, DI patterns
- **Testing** — xUnit, Moq, Arrange/Act/Assert
- **Security** — input validation, anti-forgery tokens, parameterized queries

> 💡 This file acts as a persistent "system prompt" for Copilot. Everything in here shapes every response you get.

### Conditional Instructions: `*.instructions.md`

Sometimes instructions should only apply to certain files. That's where the `applyTo` front matter comes in.

### Exercise 4 — Examine the C# instructions

Open `.github/instructions/csharp.instructions.md`. Notice the YAML front matter at the top:

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

### Exercise 5 — See instructions in action

1. Open any `.cs` file in the `src/TaskForge` project (e.g., `TaskForge.Data/Models/TaskItem.cs`)
2. Open **Copilot Chat** (Ctrl+Shift+I or ⌘+Shift+I)
3. Ask: *"How should I add a new property to track estimated hours on a TaskItem?"*

Notice how Copilot's response follows the conventions from the instructions:
- Uses `/// <summary>` XML documentation
- Follows the naming conventions
- Suggests data annotations for validation

> ✅ **Checkpoint:** You can see that Copilot's suggestions align with the project's coding standards.

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

### Exercise 6 — Explore the design-domain-model prompt

Open `.github/prompts/design-domain-model.prompt.md` and notice its structure:

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

> 💡 Prompt files are checked into your repo alongside your code. This means your team shares the same "playbook" for common tasks — no more copy-pasting prompts from Slack or Notion.

### Exercise 7 — Use the prompt to design the TaskForge domain

1. Open **Copilot Chat** (Ctrl+Shift+I or ⌘+Shift+I)
2. In the chat panel, type `#` and select the `design-domain-model` prompt from the list
3. When the prompt loads, provide this as the domain area:

```
Design a domain model for a task management application with the following
entities: Projects, Tasks, Comments, Labels, and User Assignments.
A project contains many tasks. Tasks can have comments, labels (many-to-many),
and be assigned to users. Track creation and modification timestamps on all entities.
```

4. Review the output — Copilot should produce entity classes, relationships, and validation rules that align with the existing models in `TaskForge.Data/Models/`.

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

### Exercise 8 — Try the write-unit-tests prompt

1. Open `TaskForge.Data/Models/TaskItem.cs` in the editor
2. Open **Copilot Chat** and type `#` to select the `write-unit-tests` prompt
3. Provide:
   - **Class to test:** `TaskItem`
   - **Test focus:** `validation rules and property constraints`

Observe how the generated tests follow the conventions from `write-unit-tests.prompt.md`:
- `MethodName_Scenario_ExpectedResult` naming
- Arrange/Act/Assert structure
- Both happy path and edge case coverage

---

## 1.4 Designing with AI: The Domain Model

Now let's put it all together. In this section, you'll have an **iterative design conversation** with Copilot — the same way you'd whiteboard with a colleague, except your colleague has read every line of your project's instructions.

### Exercise 9 — Iterative domain design

Open **Copilot Chat** and walk through this design conversation. After each prompt, read Copilot's response before moving to the next one.

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

### Exercise 10 — Compare AI-assisted design with manual design

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

In this lab you learned how to use GitHub Copilot as a design partner across multiple surfaces:

- ✅ **Copilot CLI** — Used `ghcs` to suggest commands and `ghce` to explain them, right in your terminal
- ✅ **Custom Instructions** — Explored how `copilot-instructions.md` provides global context and `*.instructions.md` files add conditional, file-type-specific guidance
- ✅ **Reusable Prompts** — Used `.prompt.md` files to get structured, repeatable output for domain design and test generation
- ✅ **Copilot Chat** — Had an iterative design conversation to shape the TaskForge domain model, with Copilot respecting your project's architecture and coding standards

**Key takeaway:** Copilot is most powerful when you give it context. Custom instructions and reusable prompts turn generic AI suggestions into team-aligned, project-specific guidance.

> **Next:** In [Lab 02](lab02.md) you will move from design to implementation — building out the TaskForge service layer with Copilot's help.
