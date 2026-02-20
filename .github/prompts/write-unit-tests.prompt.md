---
description: "Generate comprehensive unit tests using xUnit and Moq for a given class"
---

# Write Unit Tests

Generate comprehensive unit tests for a given class using xUnit and Moq, following TaskForge testing conventions.

## Input

**Class to test:** {{ class_name }}
**Test focus:** {{ focus (e.g., all public methods, specific method, edge cases) }}

## Testing Conventions

### Naming Convention

Use the pattern: `MethodName_Scenario_ExpectedResult`

Examples:
- `CreateAsync_ValidInput_ReturnsCreatedEntity`
- `GetByIdAsync_NonExistentId_ReturnsNull`
- `DeleteAsync_UnauthorizedUser_ThrowsUnauthorizedException`

### Test Structure

Follow the **Arrange / Act / Assert** pattern in every test:

```csharp
[Fact]
public async Task MethodName_Scenario_ExpectedResult()
{
    // Arrange — set up test data and mock dependencies

    // Act — call the method under test

    // Assert — verify the expected outcome
}
```

### Mock Setup

- Use **Moq** to mock all dependencies injected via constructor.
- Set up mocks with specific inputs where possible (avoid `It.IsAny<>()` unless necessary).
- Verify mock interactions when the test is about side effects (e.g., repository save was called).

### Coverage Requirements

Generate tests for:

1. **Happy path** — valid input produces expected output
2. **Null/empty input** — null arguments, empty strings, empty collections
3. **Not found** — requested entity does not exist
4. **Validation failure** — input violates business rules
5. **Authorization failure** — user lacks permission (if applicable)
6. **Boundary values** — min/max lengths, date boundaries, numeric limits
7. **Concurrent/duplicate** — duplicate entries, race conditions (if applicable)

### Output Format

Generate a complete test class with:
- File-scoped namespace under `TaskForge.Tests`
- All necessary `using` statements
- Constructor that initializes mocks and the system under test
- Organized test methods grouped by the method being tested
- `[Fact]` for single-case tests, `[Theory]` with `[InlineData]` for parameterized tests
