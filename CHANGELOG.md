# Changelog

This file is generated from **conventional commits** via [git-cliff](https://git-cliff.org/). Use `feat:`, `fix:`, `docs:`, `feat!:`, or a `BREAKING CHANGE:` footer for clear entries.

| Marker | Meaning |
|--------|---------|
| **[version]** | Changes in that release. |
| **Breaking changes** | Incompatible changes; consumers may need code updates. |
| **Features** | `feat:` → minor bump. |
| **Bug Fixes** | `fix:` → patch. |

---

## [unreleased]







### Bug Fixes

- **Add DefaultConnection to Docker settings for health check** *(docker)*


- **Add RateLimiting config for Docker smoke tests** *(docker)*


- **Remove npm workspaces config, fix Docker Hangfire health check, clean up playwright**


- **Fix root package.json JSON error and sync React lock file**


- **Set health check ResultStatusCodes to always return 200** *(docker)*


- **Update SharpCompress to 0.49.1 and Snappier to 1.3.1** *(security)*


- **Update login page selector for PrimeReact 10 compatibility** *(e2e)*

PrimeReact 10 InputText renders without explicit type="text" attribute.
Changed selector from input[type="text"] to input.p-inputtext:not([type="password"])
and replaced snapshot isVisible() with waitFor({ state: 'visible' }).



- **Enforce JWT signing key validation and max token lifetime (#123)** *(security)*

- fail fast at startup when TokenOptions:SecurityKey is missing or < 32 bytes
- reject previously published sample keys in Production/Staging
- remove committed key from base appsettings.json (env override supported)
- reject tokens expiring more than AccessTokenExpiration+1min in the future
- add TokenOptionsValidatorTests and ForgedTokenLifetimeTests
- document secure key configuration in README (EN/TR)



- **Unwrap paginated responses and fetch all list pages** *(clients)*

- honor pageNumber/pageSize query params in UsersController.GetList
- add fetch-all helper (Vue/React) and getAllRows (Angular) that unwrap
  PaginatedResult bodies and loop pages, keeping bare-array endpoints working
- use it for resource lists and relation-dialog lookups (group users list)
- add UsersPaginationTests, client unit tests and e2e coverage for the
  users list and group-users dialog
- repair stale Angular scaffold spec that broke test compilation



- **Start WebAPI and seed group data in E2E workflow** *(e2e)*

- the scheduled E2E run never started the backend, so the login-dependent
  specs timed out waiting for devarch.token on all three jobs
- run the API in Development (EF InMemory) on https://localhost:5101 in the
  background while npm/playwright install, wait on /healthz and seed a group
  row so the group-users relation dialog test has something to select
- ignore HTTPs errors in Playwright because CI only has an untrusted dev cert



- **Reject committed JWT keys outside Development (#125)** *(security)*

- remove the committed smoke-test signing key from appsettings.Docker.json
- fail startup on publicly known sample keys in every environment except Development
  (previously Production/Staging only, so the Docker image silently kept a public key)
- smoke script now injects a per-run random TokenOptions__SecurityKey
- cover Docker and unknown environments in TokenOptionsValidatorTests



- **Pass a generated JWT signing key to the Docker smoke container (#125)** *(ci)*


- **Restore query cache and align seeded data**

- CacheAspect: re-enable cache read/write. The aspect had been left in a
  pass-through "safe mode"; MemoryCacheManager now keeps a registry of the
  keys it writes and implements RemoveByPattern, so CacheRemoveAspect
  invalidation works with the in-memory provider as well.
- GetUserClaimsFromCacheQuery: drop [CacheAspect]. Its key is built from the
  request, which carries no user id, so enabling the cache would serve one
  user's claims to another. The per-user value is already cached under
  UserIdForClaim={userId} at login.
- FakeDataMiddlleware: seed en-US instead of en-EN, and create the default
  group through the repository because CreateGroupCommand is guarded by
  SecuredOperation and there is no HttpContext during startup.
- Tests: cover MemoryCacheManager task materialization and pattern removal.





### Documentation

- **Add pagination usage example for issue #97**




### Features

- **Add Playwright E2E test workflow for all clients** *(e2e)*


- **Add page object models, fixtures, and comprehensive E2E test suite** *(e2e)*


- **Add webServer auto-start, test scripts, fix test runner** *(e2e)*


- **Fix security vulns, add Users handler tests, update README**


- **Add scaffolding script for new entities (handlers, controller, tests)** *(cli)*


- **Add jti claim and server-side token revocation (#125)** *(security)*

- every access token now carries a jti claim; tokens without one are rejected
- POST /api/v1/auth/logout revokes the caller's jti and clears the stored refresh token
- JwtBearer OnTokenValidated rejects revoked jti values through ICacheManager
- integration tests cover logout revocation and missing-jti rejection





### Miscellaneous

- **Regenerate lock files from scratch for CI consistency**


- **Add gitignore and cleanup generated files** *(e2e)*


- **Add free demo deployment (Render + GitHub Pages)**

- render.yaml: free Docker web service for the API, with the JWT key as a
  required deploy-time variable (the app refuses to start without it)
- GitHub Actions workflow builds the four admin clients under /vue, /react,
  /angular and /blazor on GitHub Pages, plus a landing page and a 404 fallback
- externalize the API base URL: VITE_API_BASE_URL for Vue/React, a CI sed for
  Angular, wwwroot/appsettings.json for Blazor
- honour X-Forwarded-For/Proto so UseHttpsRedirection does not loop behind a
  reverse proxy
- docs/free-deployment.md: end-to-end steps, limits and optional Neon Postgres



- **Replace the Pages placeholder with the demo deploy workflow**




### Testing

- **Add API integration tests (health, auth, swagger) and fix health check duplicate**


- **Add React unit tests, Playwright E2E config, fix Vue tests**


- **Add Blazor component verification tests**


- **Add Groups (5) and Languages (22) handler tests**


- **Add 6 handler test suites (GroupClaims, Logs, OpClaims, Showcase, Translates, UserClaims) + Blazor component tests**


## [2.0.0] - 2026-06-25









### Bug Fixes

- **Unnecessary redis cache initialization removed and naming improved**


- **Missing deps added**


- **Enhance system security and API robustness** *(security)*

Replace insecure `System.Random` with cryptographically strong `System.Security.Cryptography.RandomNumberGenerator` for generating random passwords and numbers.
Add `X-Frame-Options: DENY` header to WebAPI's web.config to prevent clickjacking attacks.
Improve Flutter client's `api_translate_service` to robustly parse various API response formats, including direct dictionaries and JSON strings within the 'message' field, enhancing data handling resilience.



- **Allow SDK roll-forward across feature bands for Docker compatibility** *(build)*


- **Improve rate limiting configuration and error response** *(backend)*

- Extract rate limit values to appsettings.json for environment-specific tuning
- Add OnRejected handler with structured JSON response and Retry-After header
- Apply crud rate limiting to POST/PUT/DELETE actions across all controllers
- Apply read rate limiting to GET actions across all controllers
- Add explicit QueueLimit = 0 to crud and read policies for consistency



- **Consolidate exception handling and remove duplicate middleware** *(backend)*


- **Add SecurityTokenException handling to return 401** *(security)*


- **Exclude test files from Vue build and install vitest** *(build)*




### Documentation

- **Remove Intentum references and generalize workflow descriptions** *(github)*

The `.github/README.md` file, which contained detailed comparisons to the 'Intentum' project, has been removed. References and specific comparisons to 'Intentum' have also been stripped from comments within GitHub Actions workflows (`ci.yml`, `nuget-release.yml`, `pages.yml`). This change streamlines the repository's internal documentation and workflow explanations, making them self-contained and directly relevant to this project without external contextual dependencies.



- **Enhance Swagger documentation with JWT auth** *(api)*




### Features

- **Pagination filter class added** *(pagination)*


- **URI manager and resolved dependency added** *(pagination)*


- **Pagination result added** *(pagination)*


- **IsConnected method added to enhance resiliency**


- **Multi-stack admin clients, CP/M NuGet, API and UI hardening**

Add first-party admin apps under clients/ (Vue + PrimeVue/Aura, Angular +
PrimeNG/Aura, React + PrimeReact/Lara green, Blazor WASM + Radzen) with shared
API contract, auth, i18n, CRUD-style resource modules, and Sakai-style emerald
shell; dark mode (devarch.darkMode) on Vue/Angular/React; Blazor theme overrides
for the same palette. Add Blazor.Admin.Server host for WASM. Brand top bar as
DevArchitecture.

Backend & core: central package management (Directory.Packages.props, NuGet.config),
solution wiring, caching aspects/memory cache updates, translate repository and
middleware/startup adjustments, WebAPI controller and Startup changes, Docker
and launch profiles.



- **Add performance showcase for large in-memory dataset** *(showcase)*

Introduces a new API endpoint (`/showcase/rows`) that dynamically generates paginated in-memory data, providing performance metrics for generation and server processing.

Includes corresponding UI components in Angular, Blazor, React, and Vue clients to display this data and the associated performance indicators. This feature serves as a development tool to demonstrate and measure API response times and data generation efficiency.



- **Add rate limiting to API endpoints** *(backend)*


- **Add global exception handling middleware** *(backend)*


- **Add health check endpoints** *(backend)*


- **Add response caching for performance** *(backend)*


- **Integrate pagination into user queries** *(backend)*


- **Add shared Vue components** *(frontend)*


- **Add Angular API client and error handling** *(frontend)*


- **Add error handling and form validation composables** *(frontend)*


- **Add Vue API client wrapper** *(frontend)*


- **V2.0.0 - security fixes, tests, React client, Swagger enhancement**




### Miscellaneous

- **Update package-lock.json**




### Other

- **Include central package management files for WebAPI Docker build** *(webapi-docker)*


- **Configure dedicated runtime and stabilize smoke tests** *(webapi-docker)*

Introduces specific configuration for the WebAPI when running in a Docker environment, treating it as development mode. Enhances both CI and local smoke test scripts to reliably wait for service startup and provide clearer failure diagnostics.





### Refactor

- **RedisExtensions class refactored**


- **Remove unused 'nexus' NuGet source and format Blazor admin HTML** *(build)*




### Testing

- **Add Angular error handler tests** *(frontend)*


- **Ignore TokenExpiredTest (pre-existing issue)**


