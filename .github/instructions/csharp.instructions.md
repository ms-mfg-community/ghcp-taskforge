---
description: "C# coding standards for the TaskForge project"
applyTo: "**/*.cs"
---

# C# Coding Standards

## Namespace and File Structure

- Use **file-scoped namespaces** in all files (`namespace TaskForge.Core.Services;`).
- One primary type per file. File name must match the type name.

## Type Conventions

- Prefer **records** for DTOs, view models, and other immutable data types.
- Use **nullable reference types** — enable `<Nullable>enable</Nullable>` and handle nullability explicitly.
- Prefer **sealed** classes unless inheritance is explicitly needed.

## Naming Conventions

- `PascalCase` for public members, types, methods, and properties.
- `_camelCase` with underscore prefix for private fields.
- `Async` suffix on all async methods (e.g., `GetByIdAsync`).
- `I` prefix for interfaces (e.g., `ITaskService`).

## Dependency Injection

- Inject dependencies via **constructor injection** only.
- Register services with appropriate lifetimes: `AddScoped` for request-scoped, `AddSingleton` for stateless services, `AddTransient` for lightweight, stateless utilities.
- Depend on **abstractions** (interfaces), not concrete types.

## Async/Await

- Use **async/await** for all I/O-bound operations (database, file, HTTP).
- Never use `.Result` or `.Wait()` — these cause deadlocks.
- Use `CancellationToken` parameters where appropriate.

## LINQ and Collections

- Prefer **LINQ** over imperative loops for querying and transforming collections.
- Use **pattern matching** (`is`, `switch` expressions) for type checks and conditionals.
- Prefer `IReadOnlyList<T>` or `IEnumerable<T>` for return types when mutation is not needed.

## Error Handling

- Throw exceptions for truly exceptional conditions only.
- Use result types or validation results for expected failures.
- Never swallow exceptions with empty catch blocks.
- Include meaningful messages in exceptions.

## Documentation

- Add **XML documentation comments** (`///`) on all public types, methods, and properties.
- Include `<param>`, `<returns>`, and `<exception>` tags as appropriate.
