---
description: "Perform a thorough code review checking for bugs, security issues, and best practices"
---

# Code Review

Perform a thorough code review of the provided code. Evaluate it against the TaskForge project standards, .NET best practices, and security guidelines.

## Review Categories

### 1. Correctness
- Does the code produce the expected results?
- Are there logic errors or off-by-one mistakes?
- Are edge cases handled (null, empty, boundary values)?
- Are async methods awaited properly?

### 2. Security (OWASP Top 10)
- **Injection** — Are queries parameterized? Any raw SQL concatenation?
- **XSS** — Is user input sanitized before rendering in views?
- **CSRF** — Are anti-forgery tokens present on state-changing forms?
- **Authentication** — Are endpoints properly protected with `[Authorize]`?
- **Sensitive Data** — Are secrets, passwords, or PII exposed in logs or responses?

### 3. Performance
- Are there N+1 query problems (use `.Include()` or projection)?
- Is pagination used for list endpoints?
- Are there unnecessary allocations or redundant computations?
- Are database connections and resources properly disposed?

### 4. Maintainability
- Does the code follow SOLID principles?
- Is the code DRY (no unnecessary duplication)?
- Are names descriptive and consistent with project conventions?
- Is the complexity manageable (no deeply nested logic)?

### 5. Test Coverage
- Are there tests for the new/changed code?
- Do tests cover both happy paths and error scenarios?
- Are mocks set up correctly?

### 6. Error Handling
- Are exceptions caught at appropriate levels?
- Are error messages user-friendly and non-leaking?
- Is validation applied on all public inputs?

## Output Format

Present findings in the following table:

| # | Severity | Issue | Location | Suggestion |
|---|----------|-------|----------|------------|
| 1 | 🔴 Critical | Description | File:Line | How to fix |
| 2 | 🟡 Warning | Description | File:Line | How to fix |
| 3 | 🔵 Info | Description | File:Line | How to fix |

### Summary
- **Critical issues:** count
- **Warnings:** count
- **Info:** count
- **Overall assessment:** (Approve / Request Changes / Needs Discussion)

### Detailed Fixes
For each Critical and Warning item, provide a specific code fix with before/after examples.
