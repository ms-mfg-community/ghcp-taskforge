# Lab 03 - The Autonomous Developer

In this lab you will explore GitHub Copilot's autonomous and assistive capabilities—from assigning issues to an AI coding agent, to AI-powered code review, PR summaries, and inline suggestions.

> Duration: ~10 minutes (core path) | ~15 minutes with optional CLI exercises

References:
- [GitHub Copilot Coding Agent](https://docs.github.com/en/copilot/using-github-copilot/using-copilot-coding-agent)
- [Copilot Code Review](https://docs.github.com/en/copilot/using-github-copilot/code-review/using-copilot-code-review)
- [Copilot Pull Request Summaries](https://docs.github.com/en/copilot/using-github-copilot/using-github-copilot-for-pull-requests/creating-a-pull-request-summary-with-github-copilot)
- [Copilot Inline Suggestions](https://docs.github.com/en/copilot/using-github-copilot/getting-code-suggestions-in-your-ide-with-github-copilot)

---

## 3.1 The Copilot Coding Agent: Your Autonomous Teammate

The **GitHub Copilot Coding Agent** transforms GitHub Issues into working code—autonomously. Instead of writing every line yourself, you describe what you need, assign the issue to `@copilot`, and the agent handles the implementation.

### How It Works

```
┌──────────────┐     ┌──────────────────┐     ┌──────────────┐     ┌──────────────┐     ┌─────────┐
│  You create  │────▶│  Assign issue to │────▶│ Agent reads  │────▶│  Agent opens │────▶│   You   │
│   an Issue   │     │    @copilot      │     │ repo & codes │     │   a PR       │     │  review │
└──────────────┘     └──────────────────┘     └──────────────┘     └──────────────┘     └─────────┘
     Step 1               Step 2                  Step 3               Step 4             Step 5
```

When you assign an issue to `@copilot`:

1. **The agent reads the issue** and analyzes your repository's codebase
2. **It creates a `copilot/*` branch** and begins implementing changes
3. **It works in a sandboxed environment** powered by GitHub Actions—your local machine is never touched
4. **It opens a Pull Request** with all changes for your review
5. **It iterates on feedback**—if you leave review comments, the agent will address them

### Requirements

| Requirement | Details |
|-------------|---------|
| **Copilot Plan** | Copilot Pro+, Business, or Enterprise |
| **Repository** | Must be hosted on GitHub |
| **Permissions** | You must have write access to the repository |

### Safety Guardrails

The coding agent is designed with safety in mind:

- **Isolated branches** — all work happens on `copilot/*` branches, never directly on `main`
- **Sandboxed execution** — code runs in a GitHub Actions environment, not on your machine
- **Human review required** — the agent opens a PR; a human must approve and merge
- **Iterative feedback** — you can request changes just like with any human contributor

> **Think of it this way:** The coding agent is like a junior developer on your team. You write a clear ticket, they implement it, and you review their work before it ships.

---

## 3.2 Exercise: Create an Issue for the Coding Agent

In this exercise, you'll craft a GitHub Issue that the Copilot coding agent can pick up and implement. The quality of the issue directly impacts the quality of the agent's output—so writing a clear, well-structured issue is a skill worth developing.

### The Feature

We want to add **task filtering by priority and status** to the Tasks Index page in our TaskForge application. Users should be able to filter the task list by `Priority` (Low, Medium, High, Critical) and `Status` (Todo, InProgress, InReview, Done).

### Step 1: Navigate to Your Repository on GitHub

1. Open your browser and go to your TaskForge repository on GitHub
2. Click the **Issues** tab
3. Click **New issue**

> **Running locally?** If your repository isn't on GitHub yet, you can still follow along to learn how to write effective issues. Push your repo to GitHub when you're ready to try the agent.

### Step 2: Write a Well-Crafted Issue

A good issue for the coding agent includes three parts: **what** you want, **how to verify** it's correct, and **technical guidance** on where to make changes.

Copy this into your new issue:

**Title:**
```
Add task filtering by Priority and Status on the Tasks Index page
```

**Body:**
```markdown
## Description

Add filtering capabilities to the Tasks Index page so users can narrow down
the task list by Priority and Status. This improves usability when the task
list grows beyond a handful of items.

## Acceptance Criteria

- [ ] Add a filter dropdown for Priority (Low, Medium, High, Critical)
- [ ] Add a filter dropdown for Status (Todo, InProgress, InReview, Done)
- [ ] Filters should be applied via query parameters (e.g., `?priority=High&status=Todo`)
- [ ] Combined filters should work together using AND logic
- [ ] When no filter is selected, all tasks are displayed
- [ ] Selected filter values should persist in the dropdowns after form submission

## Technical Guidance

- Modify `TasksController.Index()` to accept optional `priority` and `status` parameters
- Use LINQ `Where` clauses to filter the `TaskItem` query
- The `Priority` enum values are: `Low`, `Medium`, `High`, `Critical`
- The `TaskItemStatus` enum values are: `Todo`, `InProgress`, `InReview`, `Done`
- Add filter dropdowns to the `Views/Tasks/Index.cshtml` view
- Follow existing patterns in the project for controller actions and views
```

### Step 3: Assign the Issue to @copilot

1. In the **Assignees** section on the right sidebar, click the gear icon
2. Type `copilot` in the search box
3. Select **Copilot** from the list
4. Click **Submit new issue**

[Screenshot: Assigning an issue to @copilot in the GitHub UI]

> 💡 **CLI Alternative:** If you're already working in a Copilot CLI session, use `/delegate` to push your current session to the Copilot coding agent on GitHub. It creates a branch, implements the changes, and opens a PR — just like assigning to `@copilot`, but from your terminal.

### What Happens Next

Once you submit the issue:
- The coding agent picks up the issue (usually within a minute or two)
- You'll see a comment from Copilot on the issue indicating it has started working
- A `copilot/*` branch is created in your repository
- After a few minutes, a Pull Request will appear

> **⏱ Don't wait — keep going!** The coding agent typically takes **2–10 minutes** to finish. **Proceed to section 3.3 now** and read through the agent's process while it works. You'll come back to the PR in section 3.4 once it's ready.

<details>
<summary>💡 Tips for Writing Effective Agent Issues</summary>

**Do:**
- Include specific acceptance criteria with checkboxes
- Reference exact file names, class names, and method names
- Mention enum values, model properties, and existing patterns to follow
- Keep the scope focused—one feature per issue

**Don't:**
- Write vague descriptions like "improve the tasks page"
- Include multiple unrelated features in one issue
- Assume the agent knows your preferences—be explicit
- Forget to mention edge cases (e.g., "what happens when no filter is selected?")

**Scope Guidance:**
| Good for the Agent | Better Done Manually |
|--------------------|----------------------|
| CRUD operations | Complex architectural refactors |
| Adding filters/sorting | Security-critical authentication flows |
| New model + migration | Performance optimization requiring profiling |
| UI forms and views | Debugging subtle race conditions |
| Unit test generation | Cross-service integration design |

</details>

---

## 3.3 Understanding What the Agent Does

While the coding agent works on your issue, let's walk through what it's actually doing behind the scenes.

### The Agent's Process

```
┌─────────────────────────────────────────────────────────────────────────┐
│                     Copilot Coding Agent Workflow                      │
├─────────────────────────────────────────────────────────────────────────┤
│                                                                         │
│  1. READ          Parses the issue title, description, and             │
│                   acceptance criteria                                   │
│                                                                         │
│  2. EXPLORE       Analyzes the repository structure, models,           │
│                   controllers, views, and existing patterns            │
│                                                                         │
│  3. PLAN          Determines which files to create or modify           │
│                                                                         │
│  4. BRANCH        Creates a copilot/* branch from the default branch   │
│                                                                         │
│  5. IMPLEMENT     Writes code across multiple files—controllers,       │
│                   views, services, tests                               │
│                                                                         │
│  6. VALIDATE      Runs CI/CD checks if configured                      │
│                                                                         │
│  7. PR            Opens a Pull Request linked to the original issue    │
│                                                                         │
└─────────────────────────────────────────────────────────────────────────┘
```

### What the Agent Might Create

For our filtering issue, the agent would likely modify or create:

| File | Changes |
|------|---------|
| `TasksController.cs` | Add `priority` and `status` parameters to `Index()`, apply LINQ filters |
| `Views/Tasks/Index.cshtml` | Add filter dropdown `<select>` elements with a form |
| Possibly a ViewModel | A view model to carry filter state back to the view |

<details>
<summary>🔍 Example: What the Controller Change Might Look Like</summary>

```csharp
public async Task<IActionResult> Index(Priority? priority, TaskItemStatus? status)
{
    var tasks = _context.TaskItems.AsQueryable();

    if (priority.HasValue)
    {
        tasks = tasks.Where(t => t.Priority == priority.Value);
    }

    if (status.HasValue)
    {
        tasks = tasks.Where(t => t.Status == status.Value);
    }

    ViewBag.CurrentPriority = priority;
    ViewBag.CurrentStatus = status;

    return View(await tasks.ToListAsync());
}
```

Notice how the agent follows .NET conventions: nullable parameters for optional filters, LINQ `Where` clauses, and `ViewBag` to persist state in the view.

</details>

### Knowledge Check

> **🤔 What types of tasks work best for the coding agent?**
>
> <details>
> <summary>Answer</summary>
>
> The coding agent excels at **well-defined, bounded tasks** with clear acceptance criteria:
>
> - Adding new CRUD endpoints or pages
> - Implementing filters, sorting, or search
> - Creating data models and migrations
> - Writing unit or integration tests
> - Adding form validation
> - Building new views that follow existing patterns
>
> It's less effective for tasks that require deep domain knowledge, complex architectural decisions, or debugging issues that need runtime observation.
>
> </details>

---

## 3.4 Copilot Code Review

Once the coding agent opens a PR (or any contributor opens a PR), you can request an **AI-powered code review** from GitHub Copilot. Copilot Code Review acts as an additional reviewer that catches issues a human might miss.

### What Copilot Code Review Checks

| Category | Examples |
|----------|----------|
| **Security** | SQL injection, XSS vulnerabilities, exposed secrets |
| **Logic Errors** | Off-by-one errors, null reference risks, incorrect conditionals |
| **Performance** | Unnecessary database queries, N+1 problems, missing `async`/`await` |
| **Best Practices** | Missing input validation, improper error handling, naming conventions |

### Exercise: Request a Copilot Code Review

Once the coding agent has opened a PR for your filtering issue:

1. Navigate to the **Pull Request** created by the agent
2. On the right sidebar, find the **Reviewers** section
3. Click the gear icon and search for **Copilot**
4. Select **Copilot** as a reviewer

[Screenshot: Requesting Copilot as a reviewer on a Pull Request]

Copilot will analyze the diff and leave review comments directly on the code, just like a human reviewer.

> **No PR yet?** If the coding agent hasn't finished, you can practice this on any open PR in your repository.

> 💡 **CLI Alternative:** You can also review code without leaving the terminal. In a Copilot CLI session, use `/review` to analyze staged or unstaged changes, or `/diff` to review all changes in the current directory.

<details>
<summary>🔍 Example: A Copilot Review Comment</summary>

Copilot might leave a comment like this on the controller code:

> **🟡 Suggestion (bug risk):** The `tasks` query is not including related data. If the view accesses `task.Project.Name`, this will throw a `NullReferenceException`. Consider adding `.Include(t => t.Project)`.
>
> ```csharp
> // Before
> var tasks = _context.TaskItems.AsQueryable();
>
> // Suggested
> var tasks = _context.TaskItems.Include(t => t.Project).AsQueryable();
> ```

This is exactly the kind of subtle issue that's easy to miss in manual review but critical for a working application.

</details>

### The Feedback Loop

Here's where it gets powerful: if the coding agent created the PR, it can **respond to review feedback automatically**.

```
┌──────────────┐     ┌──────────────────┐     ┌──────────────────┐
│ Copilot opens │────▶│ You (or Copilot) │────▶│  Agent addresses │
│    the PR     │     │  leave comments  │     │  the feedback    │
└──────────────┘     └──────────────────┘     └──────────────────┘
                              │                         │
                              └────────── repeat ───────┘
```

You can leave comments like:
- *"Please add `.Include(t => t.Project)` to the query."*
- *"The filter form should use GET, not POST."*
- *"Add a 'Clear Filters' link."*

The agent will push new commits addressing your feedback—no need to switch branches or write the fixes yourself.

---

## 3.5 PR Summaries

When creating or editing a Pull Request, Copilot can **auto-generate a summary** that helps reviewers understand changes at a glance.

**How to use it:**

1. Open a Pull Request (either one you created or the agent's PR)
2. In the PR description text area, look for the **Copilot icon** (✨) button
3. Click it to generate a summary

[Screenshot: The Copilot summary button on a PR description]

Copilot analyzes the diff and produces a structured summary:

<details>
<summary>🔍 Example: Auto-Generated PR Summary</summary>

> ### Summary
> This PR adds task filtering capabilities to the Tasks Index page.
>
> ### Changes
> - **`TasksController.cs`** — Updated `Index()` to accept optional `priority` and `status` query parameters. Added LINQ filtering logic.
> - **`Views/Tasks/Index.cshtml`** — Added filter dropdown forms for Priority and Status with submit button.
> - **`TaskFilterViewModel.cs`** — New view model to carry filter state between controller and view.
>
> ### Review Focus
> - Verify filter logic handles null/empty values correctly
> - Check that selected filters persist after form submission

This saves time for both the PR author and reviewers by immediately surfacing what changed and what to pay attention to.

</details>

**Exercise:** Navigate to the PR created by the coding agent and generate a Copilot summary. Compare it to the actual diff—does it accurately capture the changes?

---

## 3.6 The Human-AI Collaboration Loop

Let's step back and look at the full workflow you've explored in this lab:

```
┌─────────┐     ┌─────────────┐     ┌──────────────┐     ┌─────────────┐     ┌─────────┐
│   You    │────▶│   Copilot   │────▶│   Copilot    │────▶│    You      │────▶│  Merge  │
│  create  │     │   Coding    │     │    Code      │     │   final     │     │   to    │
│  issue   │     │   Agent     │     │   Review     │     │   review    │     │  main   │
└─────────┘     └─────────────┘     └──────────────┘     └─────────────┘     └─────────┘
  Define          Implement           Catch issues        Human judgment       Ship it
```

**You are always in control.** The AI handles implementation and catches mistakes, but you define what gets built and make the final call on what ships.

### Best Practices for Working with the Coding Agent

| Practice | Why It Matters |
|----------|----------------|
| **Write clear acceptance criteria** | The agent uses these as its implementation checklist |
| **Include technical guidance** | File names, method names, and patterns reduce guesswork |
| **Keep scope focused** | One feature per issue produces better results |
| **Review the PR carefully** | The agent is good but not perfect—treat it like a junior dev's PR |
| **Use review comments for iteration** | The agent responds to feedback, so guide it rather than fixing manually |

### When to Use Each Tool

| Scenario | Best Tool |
|----------|-----------|
| Well-defined feature with clear requirements | **Coding Agent** (assign issue to @copilot) |
| Writing code interactively with guidance | **Inline Suggestions** (Tab to accept) |
| Reviewing a PR for bugs and best practices | **Copilot Code Review** (add Copilot as reviewer) |
| Summarizing changes for reviewers | **PR Summaries** (click ✨ in PR description) |
| Exploring the codebase or asking questions | **GitHub Copilot CLI** (`copilot` or `copilot -p "question"`) |
| Handing off a task to the coding agent from CLI | **Copilot CLI** (`/delegate`) |
| Reviewing code changes from the terminal | **Copilot CLI** (`/review` or `/diff`) |

---

## Summary

In this lab, you learned how to:

| Capability | What You Did |
|------------|-------------|
| **Copilot Coding Agent** | Created a well-crafted issue and assigned it to `@copilot` for autonomous implementation |
| **Copilot Code Review** | Requested AI-powered review on a Pull Request to catch bugs and security issues |
| **PR Summaries** | Auto-generated a pull request summary to help reviewers understand changes |
| **CLI Code Review** _(bonus)_ | Used `/review` and `/diff` to review the coding agent's changes from the terminal |
| **Delegate from CLI** _(bonus)_ | Used `/delegate` to push work to the coding agent directly from a CLI session |

### Key Takeaway

> The most effective developers don't choose between AI and manual coding—they use both. The coding agent handles well-defined implementation tasks while you focus on design, architecture, and the decisions that require human judgment.

---

## ⭐ Bonus: CLI-Powered Code Review (~3 min)

> **Optional — complete this section if you have extra time.** This exercise demonstrates how to review code changes entirely from the terminal.

Instead of switching to the GitHub UI to review the coding agent's PR, you can review changes directly from the Copilot CLI using `/review` and `/diff`.

**Exercise: Review Changes from the Terminal**

1. First, fetch the coding agent's branch locally:
   ```bash
   git fetch origin
   git diff main..origin/copilot/issue-<number> -- . | head -100
   ```

2. Start a Copilot CLI session:
   ```bash
   copilot
   ```

3. Use `/diff` to get an AI-powered summary of all changes in the working directory:
   ```
   /diff
   ```
   Copilot analyzes the diff and explains what changed, why, and whether anything looks problematic.

4. Use `/review` to get a focused code review of staged or unstaged changes:
   ```
   /review
   ```
   Copilot reviews the changes for bugs, security issues, performance problems, and adherence to coding standards—similar to requesting Copilot Code Review on a PR, but from your terminal.

<details>
<summary>🔍 Example: What a CLI Review Looks Like</summary>

After running `/review`, Copilot might output:

```
## Code Review Summary

### TasksController.cs
✅ Filter parameters correctly use nullable types
⚠️ Missing `.Include(t => t.Project)` — view accesses `task.Project.Name`
   which will throw NullReferenceException without eager loading
⚠️ Consider adding `[FromQuery]` attributes for clarity

### Views/Tasks/Index.cshtml
✅ Anti-forgery token present on form
✅ Selected filter values persist via `asp-for` binding
💡 Consider adding a "Clear Filters" link for better UX
```

This gives you the same quality of review feedback you'd get on a PR, without leaving the terminal.

</details>

> 💡 **Pro tip:** Combine `/review` with the Shield agent for an even more thorough review: `copilot --agent=reviewer` then ask it to review recent changes.

---

## ⭐ Bonus: Delegating from the CLI (~2 min)

> **Optional — complete this section if you have extra time.** This exercise shows how to hand off work from a CLI session to the Copilot coding agent on GitHub.

The `/delegate` command bridges your local CLI session and the remote coding agent. Instead of manually creating a GitHub Issue, you can push your current context directly to the agent—it creates a branch, implements the changes, and opens a PR.

**Exercise: Delegate a Task**

1. Start a Copilot CLI session:
   ```bash
   copilot
   ```

2. Describe a small feature enhancement:
   ```
   Add a "Clear Filters" link to the Tasks Index page that resets all 
   filter dropdowns and shows the full task list.
   ```

3. Instead of having the CLI implement it locally, delegate to the coding agent:
   ```
   /delegate
   ```

4. Copilot packages your prompt and context into a GitHub Issue, assigns it to `@copilot`, and the coding agent takes over asynchronously.

**When to use `/delegate` vs local implementation:**

| Scenario | Use `/delegate` | Implement locally |
|----------|----------------|-------------------|
| Well-defined, bounded feature | ✅ | |
| You want to keep working on other things | ✅ | |
| You need precise control over the implementation | | ✅ |
| Quick one-file fix | | ✅ |
| Feature requires back-and-forth iteration | | ✅ |

> 💡 After delegating, continue working in your CLI session on other tasks. The coding agent runs independently on GitHub and will open a PR when it's done.

---

[Next Lab →](lab04.md)
