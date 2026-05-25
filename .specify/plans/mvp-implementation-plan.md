# MVP Implementation Plan: RSS Feed Reader

Path: `.specify/plans/mvp-implementation-plan.md`

Created: 2026-05-24

Purpose: Provide a step-by-step implementation plan to deliver the MVP subscription-management feature using ASP.NET Core Web API (backend) and Blazor WebAssembly (frontend), following the project's TechStack guidance.

## Plan overview

Phases:

- Phase 0 — Foundational setup & cleanup
- Phase 1 — MVP feature implementation (subscriptions add + list)
- Phase 2 — Local verification, tests, and documentation

Goals:

- Deliver a local single-user app that accepts a feed URL and displays subscriptions in memory
- Keep the project clean of template demo pages to avoid ambiguous routes
- Provide exact commands and a verification checklist for developers

---

## Phase 0 — Foundational setup & cleanup (Required)

Objectives:

- Create project scaffolding (backend + frontend)
- Remove Blazor template demo pages (Home/Counter/Weather)
- Ensure routing conflicts are resolved before feature work

Steps:

1. Scaffold projects (if not already present)
   - Create backend Web API project (recommended location: `backend/RSSFeedReader.Api`)
   - Create frontend Blazor WebAssembly project (recommended location: `frontend/RSSFeedReader.UI`)

   Example commands (run from repo root):

   ```bash
   # Backend
   dotnet new webapi -o backend/RSSFeedReader.Api --no-https

   # Frontend (Blazor WebAssembly hosted model is optional; using standalone Blazor WASM)
   dotnet new blazorwasm -o frontend/RSSFeedReader.UI
   ```

2. Remove template demo pages (CRITICAL)
   - Delete the following files from `frontend/RSSFeedReader.UI/Pages/`:
     - `Home.razor`
     - `Counter.razor`
     - `FetchData.razor` (sometimes named `Weather.razor` in templates)

   - Update `frontend/RSSFeedReader.UI/Shared/NavMenu.razor` to remove navigation entries for deleted pages and add a `Subscriptions` link.

   - Verify only your MVP pages (e.g., `Subscriptions.razor`, `NotFound.razor`) remain in `Pages/`.

3. Verify routing and build

   ```bash
   dotnet clean frontend/RSSFeedReader.UI
   dotnet build frontend/RSSFeedReader.UI
   dotnet run --project frontend/RSSFeedReader.UI
   ```

   - Open the frontend URL (usually http://localhost:5000 or as shown in the console) and verify there are no ambiguous route errors in the browser console.

4. Configure ports (choose and keep consistent)
   - Backend default: `http://localhost:5151`
   - Frontend default: `http://localhost:5213`

   Update these in:
   - `backend/RSSFeedReader.Api/Properties/launchSettings.json`
   - `frontend/RSSFeedReader.UI/Properties/launchSettings.json`
   - `frontend/RSSFeedReader.UI/wwwroot/appsettings.json` (ApiBaseUrl)

   Example `appsettings.json` entry:

   ```json
   { "ApiBaseUrl": "http://localhost:5151/api/" }
   ```

   Ensure backend CORS allows frontend origin in `Program.cs`.

---

## Phase 1 — MVP feature implementation

Objectives:

- Implement a backend API to add and list subscriptions (in-memory storage)
- Implement a frontend `Subscriptions.razor` page with an input and a list display
- Keep behavior fully client-server (frontend uses HttpClient configured with ApiBaseUrl)

Backend (minimal API endpoints):

- POST /api/subscriptions
  - Request body: { "url": "https://example.com/feed" }
  - Response: 201 Created with created subscription object

- GET /api/subscriptions
  - Response: 200 OK with list of subscription objects

Subscription model (in-memory):

- id: GUID
- url: string
- createdAt: ISO timestamp

Example backend steps:

1. Implement an in-memory repository (singleton service) for subscriptions.
2. Add controller `SubscriptionsController` with POST and GET handlers.
3. Enable CORS to allow the frontend origin.

Frontend (Subscriptions page):

- Page: `Pages/Subscriptions.razor` using route `@page "/"` (root)
- UI elements:
  - Input textbox (bind to newSubscriptionUrl)
  - Add button (or handle Enter key)
  - Subscriptions list (display url and createdAt)

Frontend behavior:

1. On page load, GET `/api/subscriptions` and populate the list.
2. On Add, POST `/api/subscriptions` with the URL; on success, append to the list.
3. Basic client-side guard: do not POST empty string.

Example snippet (Program.cs) for HttpClient:

```csharp
var apiBaseUrl = builder.Configuration["ApiBaseUrl"] ?? "http://localhost:5151/api/";
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(apiBaseUrl) });
```

---

## Phase 2 — Local verification, tests, and documentation

Objectives:

- Verify the end-to-end flow locally
- Add minimal tests (unit tests for backend controller and a UI test if desired)
- Document quickstart and how to run the app

Local verification checklist:

- [ ] Backend runs and listens on configured port
- [ ] Frontend runs and loads in browser
- [ ] `frontend/.../wwwroot/appsettings.json` points to the backend URL
- [ ] Backend CORS allows the frontend origin
- [ ] Adding a subscription shows it in the UI immediately
- [ ] Restarting the app clears subscriptions (confirm in-memory)

Minimal test suggestions:

- Backend unit test: POST a subscription and ensure GET returns it
- Frontend integration test (optional): Use Playwright or Selenium to automate adding a URL and verify it appears in the list

Documentation (quickstart.md):

- Commands to run backend and frontend locally (dotnet run)
- How to change ports and appsettings
- Note about in-memory storage and Extended-MVP steps

---

## Acceptance mapping (user stories → implementation)

- Add subscription by URL (P1) → POST /api/subscriptions and frontend Add action
- View subscription list (P1) → GET /api/subscriptions and frontend list rendering
- Single-session local app (P2) → In-memory repository and restart clearing state

---

## Deliverables

- `backend/RSSFeedReader.Api/` minimal Web API with in-memory subscription storage
- `frontend/RSSFeedReader.UI/` Blazor WebAssembly app with `Subscriptions.razor` as root page
- `docs/quickstart.md` (or `.specify/quickstart.md`) with run instructions
- Minimal backend unit test project (xUnit)

---

## Notes & assumptions

- Follow TechStack guidance strictly: remove template demo pages before feature work
- This plan assumes dotnet SDK is installed and available on developer machines
- Ports used in examples are configurable; maintain consistency across launchSettings and appsettings.json

---

End of plan
