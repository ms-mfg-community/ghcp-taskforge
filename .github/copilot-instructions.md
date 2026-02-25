# TaskForge - GitHub Copilot Instructions

## Project Description

TaskForge is an N-tier .NET task management application designed for teams. It provides project and task tracking with user authentication, role-based authorization, and a clean web interface built with ASP.NET Core MVC.

## Architecture Overview

TaskForge follows a clean N-tier architecture with three primary layers:

- **TaskForge.Web** — ASP.NET Core MVC presentation layer. Controllers, views, view models, and middleware.
- **TaskForge.Core** — Business logic and domain layer. Services, domain entities, interfaces, DTOs, and validation rules.
- **TaskForge.Data** — Data access layer. EF Core DbContext, repositories, migrations, and entity configurations.

Dependencies flow inward: Web → Core ← Data. The Core layer defines interfaces that Data implements.

## Technology Stack

- **.NET 10**
- **ASP.NET Core MVC** with Razor views
- **Entity Framework Core** with SQLite provider
- **ASP.NET Core Identity** for authentication and authorization
- **xUnit** and **Moq** for testing
- **C# 12** with nullable reference types enabled

## Coding Standards

- Use **file-scoped namespaces** in all C# files.
- Follow standard **C# naming conventions**: PascalCase for public members, camelCase for private fields with `_` prefix.
- Use **async/await** for all I/O-bound operations. Suffix async methods with `Async`.
- Use **dependency injection** — register services in `Program.cs` and inject via constructors.
- Prefer **LINQ** over manual loops for collection operations.
- Use **pattern matching** where it improves readability.
- Prefer **records** for DTOs and immutable data types.
- Include **XML documentation comments** on all public types and members.
- Keep controllers thin — delegate business logic to service classes in the Core layer.

## Testing Preferences

- Use **xUnit** as the test framework.
- Use **Moq** for mocking dependencies.
- Follow the **Arrange/Act/Assert** pattern.
- Name tests using the convention: `MethodName_Scenario_ExpectedResult`.
- Test both happy paths and edge cases (null inputs, empty collections, boundary values).
- Aim for high coverage on service and repository layers.

## Security Considerations

- Always **validate user input** on both client and server side.
- Use **parameterized queries** via EF Core — never concatenate raw SQL.
- Apply **anti-forgery tokens** on all forms.
- Use **ASP.NET Core Identity** for authentication — never implement custom auth.
- Apply **[Authorize]** attributes on controllers and actions that require authentication.
- Sanitize output to prevent **XSS** attacks.
- Follow the **principle of least privilege** for role-based access.
