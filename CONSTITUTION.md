CONSTITUTION — Repository Governance and Agent Behavior

## Purpose

This document captures the repository-level governance rules, agent behavior, and engineering expectations for contributors and automated agents (including the `speckit.constitution` assistant). It translates the repository prompt into human-readable policies and concrete actions.

## Core principles

- Be constructive, respectful, and inclusive in all interactions and contributions.
- Prioritize security, maintainability, and code quality.
- Keep changes minimal, well-scoped, and easy to review.

## Agent responsibilities

Automated agents and maintainers following this constitution should:

1. Follow repository norms
   - Respect the contribution guidelines and code of conduct.
   - Ask for human clarification when policy conflicts or legal/security/privacy concerns arise.

2. Make minimal, safe edits
   - Prefer small, focused changes that preserve existing style and public APIs.
   - When implementing larger features, split work into smaller PRs.

3. Validate changes locally
   - Run quick validations where applicable: builds, linters, and tests.
   - Report results clearly as PASS/FAIL and include commands or logs if failures occur.

4. Avoid secrets and unsafe network actions
   - Never expose or request secrets in commits, PRs, or prompts.
   - Avoid network calls that upload repository contents or secrets without explicit approval.

## Behavioral tone and output

- Use a friendly, concise, and professional tone.
- When referencing files, wrap paths in backticks (e.g., `path/to/file`).
- Use short headings and bullet lists for readability.

## Required checks before merging

- Repo builds successfully for backend and frontend (`dotnet build`).
- Unit tests pass (`dotnet test`).
- Formatting check passes (`dotnet format --verify-no-changes`).
- No accidental secrets in diffs.

## Practical guidance for developers

- Implement repository interfaces (e.g., `ISubscriptionRepository`) for MVP-first simplicity and future extensibility.
- Remove template/demo pages before adding new UI pages to avoid ambiguous route errors in Blazor projects.
- Keep the PR scope small and add a clear verification checklist to PR descriptions.

If the repository owner or maintainers need stricter rules, this constitution can be updated via a PR and should include rationale for any tightening of constraints.

---

For questions or proposed changes to this constitution, open an issue or create a PR referencing `CONSTITUTION.md`.
