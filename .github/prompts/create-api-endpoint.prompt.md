---
description: "Generate a complete CRUD controller with service layer for a given entity"
---

# Create a CRUD API Endpoint

Generate a complete CRUD implementation for a given entity following the TaskForge architecture (Web/Core/Data layers).

## Input

**Entity name:** {{ entity_name }}
**CRUD operations needed:** {{ operations (e.g., Create, Read, ReadAll, Update, Delete) }}
**Authorization:** {{ auth_requirements (e.g., Authenticated users, Admin only, Owner only) }}

## Generate the Following Components

### 1. Service Interface (TaskForge.Core)

Define an `I{{ entity_name }}Service` interface in `TaskForge.Core.Interfaces` with methods for each requested CRUD operation. Use async methods returning appropriate result types.

### 2. Service Implementation (TaskForge.Core)

Implement the service interface in `TaskForge.Core.Services`:
- Inject the repository via constructor
- Include input validation with meaningful error messages
- Include business rule enforcement
- Use async/await for all data access
- Return appropriate error results (not exceptions) for expected failures

### 3. Controller (TaskForge.Web)

Create an MVC controller in `TaskForge.Web.Controllers`:
- Use constructor injection for the service
- Apply `[Authorize]` attributes as specified
- Include `[ValidateAntiForgeryToken]` on state-changing actions
- Use view models for input/output (not domain entities)
- Return appropriate views and status codes
- Include error handling with user-friendly messages

### 4. View Models (TaskForge.Web)

Create request/response view models in `TaskForge.Web.Models`:
- Use data annotations for client and server validation
- Use records for immutable response models
- Include display attributes for form labels

### 5. Dependency Injection Registration

Provide the `Program.cs` registration line for the new service:
```csharp
builder.Services.AddScoped<IEntityService, EntityService>();
```

### Conventions

- Follow TaskForge naming conventions and project structure
- Include XML documentation comments on all public members
- Use file-scoped namespaces
- Use nullable reference types
