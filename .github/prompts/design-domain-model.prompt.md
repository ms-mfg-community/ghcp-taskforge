---
description: "Design a domain model with entities, relationships, and validation rules"
---

# Design a Domain Model

Design a domain model for the following feature or concept. Provide a complete design that includes entities, relationships, validation rules, and EF Core configuration.

## Requirements

Describe the feature or domain concept to model:

**Domain area:** {{ domain_area }}

## Expected Output

### 1. Entities

For each entity, define:
- Class name and purpose
- Properties with data types and nullability
- Computed properties or domain methods
- Data annotations or validation attributes

### 2. Relationships

Define all relationships between entities:
- **One-to-One** — identify navigation properties on both sides
- **One-to-Many** — identify the parent and child, with foreign key
- **Many-to-Many** — identify the join entity if needed

### 3. Validation Rules

For each entity, specify:
- Required fields and string length constraints
- Business rules (e.g., due date must be in the future, status transitions)
- Custom validation logic

### 4. EF Core Configuration

Provide Fluent API configuration for:
- Table and column mappings
- Relationship configuration (cascade behavior, required/optional)
- Indexes (unique, composite)
- Value conversions if applicable

### 5. C# Class Definitions

Generate complete C# classes for each entity following TaskForge conventions:
- File-scoped namespaces under `TaskForge.Core.Entities`
- Nullable reference types enabled
- XML documentation comments
- Data annotations for validation

Also generate the corresponding EF Core `IEntityTypeConfiguration<T>` classes under `TaskForge.Data.Configurations`.
