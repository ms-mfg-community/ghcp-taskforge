# TaskForge: GitHub Copilot Design Labs

> 🎨 **Theme: Using AI in the Design Process**
> Learn to leverage every GitHub Copilot capability to design, build, review, and document software — from idea to production.

---

## Overview

A hands-on workshop consisting of **4 labs (~45 minutes total)** that teaches developers how to use GitHub Copilot as a full design and development partner. You'll work with **TaskForge**, a real .NET N-tier application, progressing from initial design through autonomous development and AI-orchestrated pipelines.

---

## What You'll Build

**TaskForge** — a team task & project management application built with:

| Layer | Technology | Description |
|-------|-----------|-------------|
| Presentation | ASP.NET Core MVC | Web UI with Razor views |
| Business Logic | Service Layer | Application services and domain rules |
| Data Access | Entity Framework Core + SQLite | Repository pattern with EF Core |
| Authentication | ASP.NET Core Identity | User management and authorization |

---

## GitHub Copilot Features Covered

| Feature | Lab | Description |
|---------|-----|-------------|
| 🖥️ Copilot CLI | 1, 4 | Terminal-based AI assistant (`ghcs`, `ghce`) |
| 📋 Custom Instructions | 1 | `.github/copilot-instructions.md` |
| 📝 Reusable Prompts | 1, 4 | `.github/prompts/*.prompt.md` |
| 🤖 Custom Agents | 2 | `.github/agents/*.agent.md` |
| 🔄 Sub-agents | 2 | Parallel agent execution |
| ⚡ Agent Mode | 2, 4 | Autonomous multi-file editing |
| 🚀 Copilot Coding Agent | 3 | Issue → autonomous PR creation |
| 🔍 Code Review | 3, 4 | AI-powered PR review |
| 📊 PR Summaries | 3 | Auto-generated PR descriptions |
| 🔌 MCP/Plugins | 4 | External tool integration |
| 💡 Inline Suggestions | 3 | Real-time code completion |

---

## Labs

### [Lab Setup](labs/setup.md) — Environment Configuration
> Install prerequisites, verify tools, and configure your workspace.

### [Lab 01: The AI Design Studio](labs/lab01.md) (~10 min)
> Use Copilot CLI, custom instructions, and reusable prompts to design a new feature from scratch.

### [Lab 02: Building with Your AI Team](labs/lab02.md) (~12 min)
> Deploy custom agents (Blueprint, Forge, Shield, Sage) and sub-agents to implement the design.

### [Lab 03: The Autonomous Developer](labs/lab03.md) (~12 min)
> Leverage the Copilot Coding Agent for issue-to-PR automation, code review, and inline suggestions.

### [Lab 04: Orchestrating the AI Pipeline](labs/lab04.md) (~11 min)
> Combine MCP servers, agent mode, reusable prompts, and Copilot CLI into a full AI-orchestrated workflow.

---

## Prerequisites

- **VS Code** with the GitHub Copilot extension
- **.NET 8 SDK** — [Download](https://dotnet.microsoft.com/download/dotnet/8.0)
- **GitHub CLI** with the Copilot extension — [Download](https://cli.github.com/)
- **Node.js 18+** — [Download](https://nodejs.org/)

See the [Lab Setup guide](labs/setup.md) for detailed installation instructions.

---

## Repository Structure

```
ghcp-taskforge-copilot-labs/
├── .github/
│   ├── agents/                  # Custom Copilot agent definitions
│   ├── instructions/            # Custom instructions for Copilot
│   └── prompts/                 # Reusable prompt templates
├── .vscode/
│   └── mcp.json                 # MCP server configuration
├── labs/
│   ├── setup.md                 # Environment setup guide
│   ├── lab01.md                 # Lab 01: The AI Design Studio
│   ├── lab02.md                 # Lab 02: Building with Your AI Team
│   ├── lab03.md                 # Lab 03: The Autonomous Developer
│   └── lab04.md                 # Lab 04: Orchestrating the AI Pipeline
├── src/
│   └── TaskForge/
│       ├── TaskForge.Core/      # Domain models and interfaces
│       ├── TaskForge.Data/      # EF Core data access layer
│       ├── TaskForge.Web/       # ASP.NET Core MVC web application
│       └── TaskForge.slnx       # Solution file
└── README.md                    # This file
```

---

## Custom Agents

| Agent | Role | Description |
|-------|------|-------------|
| 🏗️ **@blueprint** | Architect | Designs system architecture, defines patterns, and creates technical specifications |
| 🔨 **@forge** | Developer | Implements features following architectural guidance and coding standards |
| 🛡️ **@shield** | Reviewer | Reviews code for security, performance, best practices, and correctness |
| 📖 **@sage** | Doc Writer | Generates documentation, API references, and user guides |

---

## Getting Started

1. **Complete the [Lab Setup](labs/setup.md)** — install all prerequisites and verify your environment.
2. **Work through the labs sequentially** — each lab builds on concepts from the previous one.
3. **Experiment freely** — the labs provide guided exercises, but you're encouraged to explore beyond the instructions.

---

## License

This workshop is provided for **educational purposes**. The TaskForge application and all lab materials are intended for use in learning GitHub Copilot features and capabilities.
