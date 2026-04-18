# DevArchitecture

**Languages:** English | [Türkçe (Turkish)](README.tr.md)

---

DevArchitecture is an open-source framework for rapid application development using a layered .NET architecture and a shared WebAPI contract. This repository includes a **.NET 10** backend, a **`/api/v1`** REST surface, and **multiple admin clients** (Vue, Angular, React, Blazor). Under **`UiPreparation`** you will also find legacy Angular and Flutter samples.

- Documentation: [devarchitecture.net](https://www.devarchitecture.net) (see also [devarchitecture.netlify.app](https://devarchitecture.netlify.app/))
- Visual Studio extensions: [Visual Studio Marketplace — DevArchitecture](https://marketplace.visualstudio.com/search?term=devarchitecture&target=VS&category=All%20categories&vsVersion=&sortBy=Relevance)
- **.NET 10 migration / breaking changes:** [docs/BREAKING-CHANGES-NET10.md](docs/BREAKING-CHANGES-NET10.md) · [Türkçe](docs/BREAKING-CHANGES-NET10.tr.md)
- **GitHub:** [Issue templates](.github/ISSUE_TEMPLATE/) and [pull request template](.github/pull_request_template.md) (bug, feature, docs, question)

![](https://www.devarchitecture.net/assets/images/image1-ce8537e256c57d119ad5559b6217d4c9.png)

---

## Prerequisites

| Tool | Version / notes |
|------|------------------|
| [.NET SDK](https://dotnet.microsoft.com/download) | **10.x** (`TargetFramework`: `net10.0`) |
| [Node.js](https://nodejs.org/) | **LTS** (for most clients); `UiPreparation/UI` declares **Node ≥ 20.11.1** and **npm ≥ 10.5.0** in `package.json` `engines` |
| npm | Ships with Node |
| [Flutter SDK](https://docs.flutter.dev/get-started/install) | For `UiPreparation/FlutterUi`: `pubspec.yaml` **Dart SDK `>=3.4.1 <4.0.0`** (comment: tested **3.22.3**) |

For local HTTPS (API and clients), run once:

```bash
dotnet dev-certs https --trust
```

---

## Backend (WebAPI)

| | |
|---|---|
| **Project version** | `WebAPI/WebAPI.csproj` → `Version` **1.0.0**, `TargetFramework` **net10.0** |
| **NuGet** | Central versions: **`Directory.Packages.props`** |

**Install / build**

```bash
dotnet build
```

**Run (development)**

```bash
dotnet watch run --project ./WebAPI/WebAPI.csproj
```

- Swagger (Development): `https://localhost:5101/swagger`
- HTTP: `5100`, HTTPS: `5101` (`WebAPI/Properties/launchSettings.json` → profile `Dev_Development`)
- Default API base: **`https://localhost:5101/api/v1`**

---

## Clients — `clients/`

Quick reference (details in the sections below):

| Folder | Install | Dev command | Typical URL |
|--------|---------|-------------|-------------|
| `clients/vue-admin` | `npm install` | `npm run dev` | `http://localhost:5174` |
| `clients/angular-admin` | `npm install` | `npm start` | `http://localhost:4200` |
| `clients/react-admin` | `npm install` | `npm run dev` | `http://localhost:5173` |
| `clients/blazor-admin` | — | `dotnet run --project ./clients/blazor-admin/Blazor.Admin.csproj` | See console URL |
| `clients/blazor-admin-server` | — | `dotnet run --project ./clients/blazor-admin-server/Blazor.Admin.Server.csproj` | e.g. `https://localhost:7003` |

### Vue — `clients/vue-admin`

| | |
|---|---|
| **Purpose** | PrimeVue + Aura themed admin UI |
| **Install** | `cd clients/vue-admin` → `npm install` |
| **Development** | `npm run dev` (Vite `vite.config.ts`: default port **5174**, next free port if busy) |
| **Production build** | `npm run build` |
| **Preview** | `npm run preview` |

**Dependency versions** (`package.json`): `vue` **^3.5.32**, `primevue` **^4.5.5**, `@primeuix/themes` **^2.0.3**, `primeicons` **^7.0.0**, `vue-router` **^5.0.4**, `axios` **^1.15.0**, `jwt-decode` **^4.0.0**, `vite` **^8.0.4**, `typescript` **~6.0.2**

API base: `src/config/modules.ts` → `API_BASE_URL` = `https://localhost:5101/api/v1`

### Angular — `clients/angular-admin`

| | |
|---|---|
| **Purpose** | PrimeNG + Aura themed admin UI (**recommended for new Angular admin work**) |
| **Install** | `cd clients/angular-admin` → `npm install` |
| **Development** | `npm start` (`ng serve --poll 1000`, default **4200**) |
| **Production build** | `npm run build` |

**Dependency versions** (`package.json`): `@angular/*` **^19.2.x**, `primeng` **^19.1.4**, `@primeuix/themes` **^2.0.3**, `primeicons` **^7.0.0**, `rxjs` **~7.8.0**, `zone.js` **~0.15.0**, `typescript` **~5.7.2**, `@angular/cli` **^19.2.24**

API base: `src/environments/environment.ts` → `apiBaseUrl` = `https://localhost:5101/api/v1`

### React — `clients/react-admin`

| | |
|---|---|
| **Purpose** | PrimeReact admin UI (Lara green light/dark) |
| **Install** | `cd clients/react-admin` → `npm install` |
| **Development** | `npm run dev` (Vite default port **5173**) |
| **Production build** | `npm run build` |
| **Lint** | `npm run lint` |

**Dependency versions** (`package.json`): `react` / `react-dom` **^19.2.4**, `primereact` **^10.9.7**, `@primeuix/themes` **^2.0.3**, `primeicons` **^7.0.0**, `react-router-dom` **^7.14.1**, `axios` **^1.15.0**, `vite` **^8.0.4**, `typescript` **~6.0.2**

API base: `src/config/modules.ts` → `API_BASE_URL` = `https://localhost:5101/api/v1`

### Blazor WebAssembly — `clients/blazor-admin`

| | |
|---|---|
| **Purpose** | WASM client using Radzen components against the WebAPI |
| **Install** | NuGet dependencies; `dotnet restore` or building the solution is enough |
| **Run** | `dotnet run --project ./clients/blazor-admin/Blazor.Admin.csproj` |
| **Publish** | `dotnet publish ./clients/blazor-admin/Blazor.Admin.csproj -c Release` |

**Versions**: `TargetFramework` **net10.0**; `Radzen.Blazor` version in **`Directory.Packages.props`** (e.g. **6.0.0**); `Microsoft.AspNetCore.Components.WebAssembly` etc. **10.0.6** in the same file.

API base: `Services/ApiClient.cs` → `BaseUrl` = `https://localhost:5101/api/v1`

> For local WASM / HTTPS issues, running **`clients/blazor-admin-server`** may be more reliable (see project notes).

### Blazor Server (host) — `clients/blazor-admin-server`

| | |
|---|---|
| **Purpose** | Host the same Blazor WASM app via ASP.NET Core |
| **Run** | `dotnet run --project ./clients/blazor-admin-server/Blazor.Admin.Server.csproj` |
| **URLs** | `Properties/launchSettings.json`: e.g. **https://localhost:7003**, **http://localhost:5062** (`https` profile) |

---

## UiPreparation samples

### Angular (legacy) — `UiPreparation/UI`

| | |
|---|---|
| **Status** | Older **Material Dashboard** sample; **prefer `clients/angular-admin` for new work.** |
| **Install** | `cd UiPreparation/UI` → `npm install` |
| **Engine requirement** | `package.json` → `engines`: **Node ≥ 20.11.1**, **npm ≥ 10.5.0** |
| **Development** | `npm start` (`ng serve`, default **http://localhost:4200**) |
| **Production build** | `npm run build` |
| **Package name / version** | `material-dashboard-angular` **2.5.0** |

**Notable dependencies** (`package.json`): `@angular/core` **^19.2.15**, `@angular/material` **^19.2.19**, `@angular/cdk` **^19.2.19**, `bootstrap` **4.5.2**, `@ngx-translate/core` **^15.0.0**, `@auth0/angular-jwt` **^4.2.0**, `rxjs` **^7.8.2**, `typescript` **~5.5.4**, `@angular/cli` **^19.2.17**

API: `src/environments/environment.ts` → `getApiUrl` = `https://localhost:5101/api/v1`

> Port **4200** conflicts if you run **`clients/angular-admin`** at the same time; stop one or use `ng serve --port`.

### Flutter — `UiPreparation/FlutterUi/flutter_devarchitecture`

| | |
|---|---|
| **Purpose** | Mobile / multi-platform sample (Bloc, Firebase, etc.) |
| **Install** | `cd UiPreparation/FlutterUi/flutter_devarchitecture` → `flutter pub get` |
| **Run** | `flutter run` (targets: `flutter devices` → `flutter run -d chrome` / `-d macos` / Android emulator) |
| **SDK** | `pubspec.yaml` → `environment.sdk`: **`>=3.4.1 <4.0.0`** (comment: tested **3.22.3**) |
| **App version** | `version` **0.1.0** |

**Notable dependencies** (`pubspec.yaml`): `flutter_bloc` **8.1.6**, `http` **1.3.0**, `flutter_modular` **6.3.4**, `get_it` **8.0.0**, `intl` **0.20.2**, Firebase packages (e.g. `firebase_core` **3.8.1**, `cloud_firestore` **5.5.0**), `data_table_2` **2.5.15**, `lottie` **3.1.2**, `jwt_decoder` **2.0.1**

---

## Shared features and theming

- **API**: JWT, versioned REST (`/api/v1`), shared contract for translations / language codes and admin modules.
- **Look & feel (`clients/*`)**: Sakai-style shell (emerald primary, slate neutrals). Vue / Angular / React use dark mode via **`localStorage`** key **`devarch.darkMode`**; Blazor aligns Radzen theme variables to the same palette.
- **NuGet**: **`Directory.Packages.props`** centralizes package versions.

---

## Support this project

If you find DevArchitecture useful, please consider starring this repository.
