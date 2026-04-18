# Breaking changes: .NET 10 migration

**Languages:** English | [Türkçe](BREAKING-CHANGES-NET10.tr.md)

---

This document summarizes **what changes for developers and operators** when moving this solution to **.NET 10** (`net10.0`), including tooling, NuGet, and client applications. It is **not** a full duplicate of Microsoft’s platform release notes; use the official links below for framework-wide breaking changes.

**Related docs**

- English README: [README.md](../README.md)
- Turkish README: [README.tr.md](../README.tr.md)

---

## 1. Who is affected?

| Area | Impact |
|------|--------|
| **Backend / libraries** | All `*.csproj` target **`net10.0`**. Older SDKs cannot build. |
| **Tests** | `Tests` project targets **`net10.0`**. |
| **Blazor** | `clients/blazor-admin` (WASM) and `clients/blazor-admin-server` target **`net10.0`**. |
| **Tooling** | Local **`global.json`** pins the .NET SDK (see §2). CI/agents must install a compatible SDK. |
| **NuGet** | **Central Package Management** via **`Directory.Packages.props`**; versions are no longer declared per-project for centrally managed packages. |
| **JavaScript admin apps** | **Node.js LTS** still required; framework upgrade is orthogonal but README versions apply. |

Downstream forks that referenced **`net7.0` / `net8.0` / `net9.0`** must retarget and resolve package drift.

---

## 2. SDK and runtime requirements

- **Install [.NET SDK 10.x](https://dotnet.microsoft.com/download)** on every machine and CI agent that builds this repository.
- Repository root **`global.json`** pins the SDK, for example:

  ```json
  {
    "sdk": {
      "version": "10.0.201",
      "rollForward": "latestPatch"
    }
  }
  ```

  Adjust the patch version as your team standardizes; `rollForward: latestPatch` allows newer patches on the same minor.

- **Deployment**: hosting (IIS, Kestrel, containers) must run a **.NET 10** runtime where applicable.

---

## 3. Target framework

All main projects use:

```xml
<TargetFramework>net10.0</TargetFramework>
```

**Breaking:** Any external project that referenced this solution’s packages or projects as **`netstandard2.0`-only** consumers may need to align to **`net10.0`** (or a compatible TFM) depending on how you ship libraries.

---

## 4. NuGet: central package management (CPM)

- **`Directory.Packages.props`** defines **one version per package** for the whole repo (`ManagePackageVersionsCentrally`).
- Individual `csproj` files use `PackageReference` **without** `Version` for centrally managed packages.

**Breaking for maintainers:**

- Adding or bumping a package version is done in **`Directory.Packages.props`**, not scattered across projects.
- CI and local restores must succeed with this layout; duplicate version attributes in `csproj` can cause restore errors.

**`NuGet.config`:** If present, it defines package sources (e.g. nuget.org and optional private feeds). Corporate environments must mirror or allowlisted those feeds or restore will fail.

---

## 5. Microsoft.AspNetCore.* and Entity Framework Core 10

Aligned package versions (examples from `Directory.Packages.props`):

- `Microsoft.AspNetCore.Authentication.JwtBearer` **10.x**
- `Microsoft.EntityFrameworkCore.*` **10.x**
- `Microsoft.AspNetCore.Components.WebAssembly*` **10.x** (Blazor WASM)

**Action:** Review Microsoft’s own breaking-change and migration docs for ASP.NET Core and EF Core when upgrading from older major versions:

- [.NET 10 release notes](https://learn.microsoft.com/dotnet/core/whats-new/dotnet-10/overview)
- [ASP.NET Core migration guides](https://learn.microsoft.com/aspnet/core/migration/)
- [EF Core release planning](https://learn.microsoft.com/ef/core/what-is-new/)

Expect possible adjustments in **startup**, **minimal APIs**, **OpenAPI/Swagger**, **JWT**, and **database providers** if you merge upstream framework changes.

---

## 6. WebAPI and hosting

- **`WebAPI/Properties/launchSettings.json`** profiles (e.g. `Dev_*`) bind **HTTP/HTTPS** ports documented in [README.md](../README.md).
- **`WebAPI/Dockerfile`** (if used) must target a **.NET 10** base image; rebuild images after the migration.

**Breaking:** Environment-specific configuration (connection strings, secrets) must be revalidated on the new runtime.

---

## 7. Client applications under `clients/`

These are **not** .NET 10 binaries, but they ship alongside the same API contract:

| Client | Notes |
|--------|--------|
| **Vue / Angular / React** | Require **Node.js LTS** and `npm install` per app. See README for pinned major versions. |
| **Blazor WASM + Server** | Require **.NET 10 SDK**; Radzen and WASM packages align with **`Directory.Packages.props`**. |

Default API base remains **`https://localhost:5101/api/v1`** unless you override per environment.

---

## 8. UiPreparation samples

- **`UiPreparation/UI`**: Angular app; Node engine requirements in `package.json`.
- **`UiPreparation/FlutterUi/...`**: Flutter/Dart SDK constraints in `pubspec.yaml`.

These are **samples**; breaking changes there are independent of `net10.0` except where you integrate with the same WebAPI.

---

## 9. CI / GitHub Actions

Workflows under **`.github/workflows/`** (e.g. `ci.yml`, `codeql.yml`, `clients-ui.yml`) should use a **.NET 10** setup action or container. If pipelines still pin `dotnet-version: 8.x` or `9.x`, update them to **10.x** or they will fail to build.

---

## 10. Migration checklist

- [ ] Install **.NET SDK 10.x** locally; verify `dotnet --version`.
- [ ] `dotnet restore` at repo root; fix any feed or CPM issues.
- [ ] `dotnet build` full solution; fix analyzer or API breaks from dependencies.
- [ ] Run **`Tests`** project; fix failing tests.
- [ ] Run **WebAPI** and smoke-test Swagger and auth.
- [ ] Build and run **each** client under `clients/` (and Blazor server host if used).
- [ ] Update **Dockerfiles** and **deployment** images to .NET 10.
- [ ] Update **CI** matrices to SDK **10.x**.

---

## 11. Rollback strategy

Keep a Git branch or tag **before** the `net10.0` migration. Downgrade is only practical by reverting that merge; mixing `net10.0` projects with obsolete package locks without care will not be supported.

---

*Last updated to reflect the repository state at the time of the .NET 10 migration. Amend this file when you change TFM, CPM, or supported SDK bands.*
