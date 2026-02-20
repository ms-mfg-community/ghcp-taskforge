---
description: "Razor view conventions for TaskForge"
applyTo: "**/*.cshtml"
---

# Razor View Conventions

## Tag Helpers

- Use **Tag Helpers** instead of HTML Helpers (e.g., `<a asp-controller="Tasks" asp-action="Index">` instead of `@Html.ActionLink(...)`).
- Use `asp-for` for model binding on form inputs.
- Use `asp-validation-for` for validation messages.

## View Logic

- Keep views **thin** — no business logic in Razor files.
- Use view models, not domain entities, as `@model` types.
- Complex display logic should be in view models, display templates, or Tag Helpers.
- Use `@if` and `@foreach` sparingly — prefer partial views or view components for complex rendering.

## Partial Views and View Components

- Extract reusable UI blocks into **partial views** (`_PartialName.cshtml`).
- Use **View Components** for reusable sections that require server-side logic (e.g., navigation menus, notification badges).
- Use `<partial name="_PartialName" model="..." />` Tag Helper syntax.

## Forms and Security

- Include **anti-forgery tokens** on all forms using `<form asp-antiforgery="true">` or `@Html.AntiForgeryToken()`.
- All POST, PUT, and DELETE forms must validate anti-forgery tokens.
- Use `asp-route-*` attributes for passing route parameters.

## Layout and Structure

- Use `_Layout.cshtml` for consistent page structure.
- Define `@section` blocks for page-specific scripts and styles.
- Use `_ViewImports.cshtml` for shared `@using` and `@addTagHelper` directives.

## Validation

- Display validation summaries with `<div asp-validation-summary="ModelOnly"></div>`.
- Display field-level validation with `<span asp-validation-for="PropertyName"></span>`.
- Include jQuery validation scripts for client-side validation.
