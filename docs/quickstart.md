# Quickstart — MVP RSS Feed Reader

This project contains a minimal ASP.NET Core Web API backend and a Blazor WebAssembly frontend implementing the MVP subscription-management feature.

Prerequisites

- .NET 7 SDK installed locally (https://dotnet.microsoft.com/download)

Run the backend

```bash
cd backend/RSSFeedReader.Api
dotnet restore
dotnet run
```

The backend will listen on `http://localhost:5151` by default (see `Properties/launchSettings.json`).

Run the frontend

```bash
cd frontend/RSSFeedReader.UI
dotnet restore
dotnet run
```

The frontend will run on `http://localhost:5213` by default (see `Properties/launchSettings.json`) and is configured to call the backend at `http://localhost:5151/api/` via `wwwroot/appsettings.json`.

Notes

- The backend uses an in-memory store for subscriptions. Data is lost when the backend stops.
- Before implementing additional UI pages, remove Blazor template demo pages (`Home.razor`, `Counter.razor`, `FetchData.razor`) to avoid ambiguous route errors.
- If you see CORS errors, ensure the backend permits the frontend origin in `Program.cs`.

Local verification

- Navigate to the frontend URL, paste a feed URL into the input, and click Add. The subscription should appear in the list.
- Restart the backend and verify the list is cleared.

If you want, I can now implement CORS configuration, adjust the frontend API base paths to point explicitly at `/api/subscriptions`, and add unit tests. Note: I couldn't run `dotnet` in this environment, so please run the above commands locally to build and test.
