# Engineering Principles — Security, Maintainability, and Code Quality

These principles are specific, actionable, and tailored to this RSS Feed Reader project (MVP: subscription management only; Tech: ASP.NET Core Web API + Blazor WebAssembly). Each principle includes a short rationale and a hands-on checklist or implementation notes you can use immediately.

## Security Principles

1. Enforce origin and transport policies (CORS + HTTPS)

- Rationale: Frontend and backend run on separate localhost ports; misconfigured CORS or plain HTTP can cause hard-to-debug failures or unsafe behavior.
- Action checklist:
  - Ensure backend CORS explicitly allows the frontend origin in `backend/RSSFeedReader.Api/Program.cs` (use exact ports from `launchSettings.json`).
  - Use HTTPS for both frontend and backend in local development where possible; verify dev certificates are trusted.
  - In `frontend/.../wwwroot/appsettings.json` keep only the API base URL and do not embed secrets.
  - CI: fail builds if CORS is configured as `*` in production builds.

2. Validate and sanitize external content before rendering

- Rationale: Extended-MVP will render feed content from external sources which can include unsafe HTML or scripts.
- Action checklist:
  - Do not render raw HTML from feeds without sanitization. Add `HtmlSanitizer` (or equivalent) in the backend or sanitize on the server before returning content.
  - For the MVP (no fetching), still define a clear sanitization policy so it can be plugged in during Extended-MVP.
  - Add a unit test that demonstrates sanitization on a malicious HTML sample.

3. Never commit secrets; secure configuration handling

- Rationale: Secrets or keys in source control are a common vulnerability.
- Action checklist:
  - Ensure `appsettings.*.json` contains no secrets. Use environment variables or user secrets for local dev when needed.
  - Add a `.gitignore` entry for any secret storage and document how to set local secrets in `README.md`.
  - Add a small CI check (or Git pre-commit hook) that scans for high-risk patterns (e.g., `AKIA`, `-----BEGIN PRIVATE KEY-----`).

4. Safe HTTP client usage and resource limits (for Extended-MVP)

- Rationale: Fetching feeds must be robust to slow or malicious servers.
- Action checklist:
  - Use `HttpClient` with a configured timeout and `CancellationToken`.
  - Limit response size (read a maximum number of bytes) and validate content type before parsing.
  - Parse feeds using a library that tolerates common RSS/Atom variations, and handle parsing exceptions gracefully.

## Maintainability Principles

1. Keep the MVP small but extensible via clear abstractions

- Rationale: MVP uses in-memory storage but we should avoid coupling code to it—swap-in persistence later with minimal changes.
- Action checklist:
  - Define `ISubscriptionRepository` (or similar) in the backend and implement an in-memory `InMemorySubscriptionRepository` for MVP.
  - Inject the repository via DI (use `builder.Services.AddSingleton<ISubscriptionRepository, InMemorySubscriptionRepository>()`).
  - Add integration tests that use the interface so switching to EF Core later only affects composition.

2. Remove template cruft and keep routes deterministic

- Rationale: Blazor templates include demo pages that cause ambiguous route exceptions if left in place.
- Action checklist:
  - As documented in `TechStack.md`, delete `Home.razor`, `Counter.razor`, `Weather.razor` from `frontend/[ProjectName].UI/Pages/`.
  - Ensure only one page has `@page "/"` and update `NavMenu.razor` accordingly.
  - Add a small verification task to the repo’s checklist that runs on new branches: `dotnet build` for frontend and backend.

3. Use clear project layout and naming conventions

- Rationale: New contributors should find code quickly and understand responsibility boundaries.
- Action checklist:
  - Backend project: `backend/RSSFeedReader.Api` — controllers, models, services, repositories.
  - Frontend project: `frontend/RSSFeedReader.UI` — `Pages/`, `Shared/`, `Services/`.
  - Document folder responsibilities in `README.md` and add a simple architecture diagram (markdown ASCII or small SVG).

4. Keep changes small and reviews fast

- Rationale: Small PRs reduce review time and risk.
- Action checklist:
  - Prefer PRs that change one logical area (API, UI, infra). Avoid mixing feature + formatting changes.
  - Add a PR template that lists: build status, tests passed, migration notes (if any), and manual verification steps.

## Code Quality Principles

1. Enforce consistent formatting and static analysis

- Rationale: Code style problems create noise and slow reviews.
- Action checklist:
  - Add an `.editorconfig` at repo root and enable `dotnet format` in CI.
  - Enable Roslyn analyzers (Microsoft.CodeAnalysis.FxCopAnalyzers or the built-in analyzers) and treat warnings as build warnings; escalate important rules to errors as needed.
  - Add a CI job that runs `dotnet format --verify-no-changes` and `dotnet build`.

2. Test the important behaviors (API contract + UI state)

- Rationale: The MVP's public contract is small (add subscription, list subscriptions); these should be tested and guarded.
- Action checklist:
  - Backend: create xUnit tests for controller endpoints (happy path + 1 error path) under `backend/tests/`.
  - Frontend: add a minimal bUnit test for the Subscriptions page that verifies adding a subscription updates the list.
  - CI: run `dotnet test` for all test projects on each PR.

3. Use clear, structured logging and meaningful errors

- Rationale: Logs are the fastest way to triage runtime problems during development and in production.
- Action checklist:
  - Use `ILogger<T>` via DI in controllers and services.
  - Log at appropriate levels: `Information` for high-level operations, `Warning` for recoverable issues, `Error` for exceptions.
  - Do not log sensitive data (API keys, secrets, or full HTTP responses containing credentials).

4. Make the code easy to change: small functions and single-responsibility types

- Rationale: Easier to test and reason about.
- Action checklist:
  - Keep controllers thin: delegate business logic to services (e.g., `SubscriptionService`).
  - Each service should have a single responsibility and be covered by unit tests.
  - Limit method length and complexity; prefer descriptive method names.

## Practical checklists (quick start)

- Before merging a PR:
  - [ ] Repo builds (`dotnet build` for backend and frontend)
  - [ ] Tests pass (`dotnet test`)
  - [ ] Formatting check passes (`dotnet format --verify-no-changes`)
  - [ ] No accidental secrets in diffs
  - [ ] PR description explains verification steps

- Minimal CI jobs to add (order matters):
  1. Restore & Build (backend + frontend)
  2. Run unit tests
  3. Run `dotnet format --verify-no-changes` and static analyzers

## Notes specific to the MVP constraints

- The MVP intentionally omits feed fetching and persistence. Still:
  - Define interfaces (repositories, fetchers) now so Extended-MVP implementation is a composition change, not a rewrite.
  - Even though URL validation is "assumed valid" for MVP, add a lightweight validation utility that can be enabled later (e.g., simple Regex or Uri.TryCreate) and unit-tested.
  - Keep UI simple: focus tests on state changes rather than rendering exact markup.

---

If you want, I can:

- Add the `ISubscriptionRepository` interface and a tiny in-memory implementation now, with unit tests; or
- Create CI job definitions (GitHub Actions) that run the build/test/format steps above; or
- Generate a PR template and `README` section summarizing these principles.

Which of those would you like me to do next?
