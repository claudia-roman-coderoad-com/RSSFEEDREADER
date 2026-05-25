# Implementation Tasks — MVP RSS Feed Reader

This document lists the implementation tasks derived from the MVP plan and the acceptance criteria for each.

## Tasks

1. Scaffold backend & frontend projects (status: not-started)
   - Description: Create the backend Web API (`backend/RSSFeedReader.Api`) and frontend Blazor WASM (`frontend/RSSFeedReader.UI`) using dotnet templates. Include launchSettings placeholders.
   - Acceptance: `backend/RSSFeedReader.Api` and `frontend/RSSFeedReader.UI` folders exist with runnable template projects and no build errors.

2. Clean Blazor template demos (status: not-started)
   - Description: Remove demo pages (`Home.razor`, `Counter.razor`, `FetchData.razor`) and update `frontend/RSSFeedReader.UI/Shared/NavMenu.razor` to include `Subscriptions`.
   - Acceptance: No ambiguous route errors when building frontend; `Pages/` contains only MVP pages.

3. Implement in-memory backend API (status: not-started)
   - Description: Add an in-memory repository service and `SubscriptionsController` with POST `/api/subscriptions` and GET `/api/subscriptions`. Ensure CORS allows the frontend origin.
   - Acceptance: POST returns 201 and GET returns created subscriptions within the running session.

4. Implement frontend Subscriptions page (status: not-started)
   - Description: Create `Pages/Subscriptions.razor` (route `/`) with input, Add button, and list rendering. Use `HttpClient` configured from `appsettings.json` to call backend endpoints.
   - Acceptance: Adding URL appends it to UI list without reload; empty input is prevented client-side.

5. Configure ports & appsettings/CORS (status: not-started)
   - Description: Choose and set backend and frontend ports; update `launchSettings.json` files and `frontend/.../wwwroot/appsettings.json` (ApiBaseUrl). Update backend `Program.cs` CORS to allow frontend origin.
   - Acceptance: Frontend can call backend API from browser without CORS errors.

6. Add quickstart documentation (status: not-started)
   - Description: Create `docs/quickstart.md` describing how to build/run backend and frontend, how to change ports, and note in-memory behavior.
   - Acceptance: A developer can follow `docs/quickstart.md` to run the app locally and reproduce acceptance scenarios.

7. Add backend unit tests (status: not-started)
   - Description: Create a minimal xUnit test project that tests POST then GET subscription behavior against the in-memory repository/controller.
   - Acceptance: Tests pass locally with `dotnet test`.

8. Manual verification & E2E smoke test (status: not-started)
   - Description: Run local verification checklist: backend running, frontend running, add subscription, restart clears list. Optionally create Playwright/Selenium script for adding a URL and verifying list append.
   - Acceptance: Manual checklist items pass and optional E2E script (if added) succeeds.

9. Create PR and update agent context (status: not-started)
   - Description: Commit changes on a feature branch `mvp-rss-reader`, open a PR, and confirm `.github/copilot-instructions.md` references the plan.
   - Acceptance: PR created with correct branch name and updated copilot instructions referencing `.specify/plans/mvp-implementation-plan.md`.

---

## Notes

- This TODO list maps directly to `.specify/plans/mvp-implementation-plan.md`.
- Follow the "CRITICAL" cleanup steps for Blazor templates before implementing any UI features to avoid ambiguous routing errors.

---

If you want, I can now:

- Mark the `Scaffold backend & frontend projects` task as in-progress and run the `dotnet new` commands to scaffold the projects, or
- Start implementing one of the other tasks (you choose which).
