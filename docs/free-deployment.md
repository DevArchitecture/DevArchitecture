# Free public demo deployment

Run the API and all four admin clients on free tiers:

| Piece | Host | URL shape |
|-------|------|-----------|
| API (ASP.NET Core 10, Docker) | [Render](https://render.com) free web service | `https://<service>.onrender.com` |
| Vue, React, Angular, Blazor | GitHub Pages | `<pages-url>/vue/`, `/react/`, `/angular/`, `/blazor/` |
| Database (optional) | [Neon](https://neon.tech) free Postgres | `postgres://…` |

The API image defaults to `ASPNETCORE_ENVIRONMENT=Docker`, which uses the **in-memory
database and the fake data** seeded at startup. That is the fastest path to a live demo and
needs no database account. Data is lost whenever the instance restarts or wakes from sleep;
switch to the Neon steps at the end if you need persistence.

## 1. API on Render

1. Create a Render account and connect the `DevArchitecture/DevArchitecture` repository.
2. **New → Blueprint**, pick the repository. Render reads [`render.yaml`](../render.yaml).
3. When prompted for `TokenOptions__SecurityKey`, generate a value and paste it:

   ```bash
   openssl rand -base64 48
   ```

4. Deploy. The health check is `/healthz`; the first deploy takes a few minutes.
5. Note the URL, e.g. `https://devarchitecture-api.onrender.com`. The API root is
   `https://devarchitecture-api.onrender.com/api/v1`.

Free instances spin down after 15 minutes without traffic and take about a minute to wake.
The first request after a pause is slow; that is expected.

## 2. UI on GitHub Pages

1. Repository **Settings → Pages → Source: GitHub Actions**.
2. **Settings → Secrets and variables → Actions → Variables → New variable**:
   - Name: `API_BASE_URL`
   - Value: `https://<your-service>.onrender.com/api/v1`
3. **Actions → Deploy demo UI to GitHub Pages → Run workflow**.

The workflow builds the four clients with that API URL, places them under `/vue`, `/react`,
`/angular` and `/blazor`, and publishes a small landing page at the site root.

The admin credentials come from the seeded data: `admin@adminmail.com` / `Q1w212*_*`.

## 3. Persistent database (optional)

The demo uses the in-memory store. For a database that survives restarts:

1. Create a free Postgres database on [Neon](https://neon.tech) and copy the connection
   string (it must include `SSL Mode=Require`).
2. Apply the schema and seed data **once** from your machine:

   ```bash
   export ASPNETCORE_ENVIRONMENT=Production
   export AppSettings__Mode=Production
   export TokenOptions__SecurityKey="$(openssl rand -base64 48)"
   export ConnectionStrings__DArchPgContext="Host=…;Database=…;Username=…;Password=…;SSL Mode=Require"

   dotnet tool install --global dotnet-ef   # once
   dotnet ef database update --project WebAPI/WebAPI.csproj
   ```

   Languages and translations are seeded by `HasData` in the migrations. The admin user is
   created by `OperationClaimCreatorMiddleware` on startup.

3. In Render, add these environment variables to the service:

   | Key | Value |
   |-----|-------|
   | `ASPNETCORE_ENVIRONMENT` | `Production` |
   | `AppSettings__Mode` | `Production` |
   | `ConnectionStrings__DArchPgContext` | the Neon connection string |

4. Redeploy. Swagger is disabled in Production; `/healthz` stays available.

Redis is not required. In non-Development modes the API tries Redis, falls back to the
in-memory cache when it cannot connect, and keeps working as a single instance.

## Limits and troubleshooting

- **Data resets / admin user disappears** — demo mode uses the in-memory database. Use the
  Neon steps for persistence.
- **API does not start** — check the Render logs for `TokenOptions:SecurityKey`. The key must
  be at least 32 bytes and cannot be the sample key from `appsettings.Development.json`.
- **502 / first request times out** — the free instance is waking up; retry after ~1 minute.
- **Login fails from the UI** — confirm `API_BASE_URL` ends with `/api/v1` and matches the
  Render URL exactly (no trailing slash), then rerun the Pages workflow.
- **Deep-link refresh lands on the landing page** — GitHub Pages has no server-side rewrites;
  open the app root and navigate inside the app.
- **Rate limits** — `auth` 10, `crud` 100, `read` 200 requests per minute per client.

## Local check before deploying

```bash
docker build -t devarch-api -f WebAPI/Dockerfile .
docker run --rm -p 8080:8080 \
  -e TokenOptions__SecurityKey="$(openssl rand -base64 48)" \
  devarch-api
curl -fsS http://localhost:8080/healthz
```
