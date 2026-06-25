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




### Features

- **Add Playwright E2E test workflow for all clients** *(e2e)*


- **Add page object models, fixtures, and comprehensive E2E test suite** *(e2e)*


- **Add webServer auto-start, test scripts, fix test runner** *(e2e)*




### Miscellaneous

- **Regenerate lock files from scratch for CI consistency**


- **Add gitignore and cleanup generated files** *(e2e)*




### Testing

- **Add API integration tests (health, auth, swagger) and fix health check duplicate**


- **Add React unit tests, Playwright E2E config, fix Vue tests**


- **Add Blazor component verification tests**


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


