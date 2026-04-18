# GitHub metadata

This folder mirrors the layout used in [Intentum’s `.github`](https://github.com/keremvaris/Intentum/tree/master/.github): **issue templates**, **workflows**, and (at repo root) **`cliff.toml`** for changelog automation.

| Path | Purpose |
|------|---------|
| [`ISSUE_TEMPLATE/`](ISSUE_TEMPLATE/) | Bug, feature, documentation, and question forms; [`config.yml`](ISSUE_TEMPLATE/config.yml) contact links. |
| [`pull_request_template.md`](pull_request_template.md) | Default PR checklist. |
| [`workflows/ci.yml`](workflows/ci.yml) | **Main CI:** restore, vulnerability check, build, test, Docker build + `/healthz` smoke. Replaces the former `dotnet.yml`. |
| [`workflows/changelog.yml`](workflows/changelog.yml) | On `master` push: regenerate **`CHANGELOG.md`** with [git-cliff](https://git-cliff.org/) (commits with `[skip ci]` to avoid loops). |
| [`workflows/github-release.yml`](workflows/github-release.yml) | On `v*` tag push or manual run: create GitHub Release with notes from git-cliff. |
| [`workflows/integration-tests.yml`](workflows/integration-tests.yml) | Manual full test run (`workflow_dispatch`). |
| [`workflows/nuget-release.yml`](workflows/nuget-release.yml) | Manual: pack Core/Business/Entities/DataAccess → artifact; optional NuGet.org push if `NUGET_API_KEY` + input enabled. |
| [`workflows/pages.yml`](workflows/pages.yml) | Placeholder until a DocFX/static site exists (Intentum builds docs + coverage). |
| [`workflows/codeql.yml`](workflows/codeql.yml) | CodeQL for C# and JS/TS. |
| [`workflows/clients-ui.yml`](workflows/clients-ui.yml) | Build Vue/React/Angular/Blazor clients when `clients/**` changes. |

Root **[`cliff.toml`**](../../cliff.toml) configures git-cliff for **`changelog.yml`** and **`github-release.yml`**.

**Intentum differences**

- **ci.yml** here has no SonarCloud step (Intentum uses `SONAR_TOKEN` + `dotnet-sonarscanner`). Add SonarCloud later if you create a project and secret.
- **pages.yml** here is a no-op; Intentum runs DocFX, coverage HTML, and `docs/en`/`docs/tr` toc sync.
- **integration-tests.yml** here runs the full `dotnet test` suite; Intentum runs OpenAI-specific filtered tests.
