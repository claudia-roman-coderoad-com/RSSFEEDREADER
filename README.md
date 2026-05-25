# RSS Feed Reader (MVP)

This repo contains a minimal MVP for subscription management using an ASP.NET Core Web API backend and a Blazor WebAssembly frontend.

To build and run locally:

```bash
# Backend
cd backend/RSSFeedReader.Api
dotnet restore
dotnet run

# Frontend
cd frontend/RSSFeedReader.UI
dotnet restore
dotnet run
```

Run tests (from repo root):

```bash
dotnet test backend/RSSFeedReader.Api.Tests
```

Create a feature branch and commit (helper):

```bash
./scripts/create-pr.sh mvp-rss-reader
```

Smoke test API (after backend is running):

```bash
./scripts/smoke-test.sh
```
