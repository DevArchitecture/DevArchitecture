# DevArchitecture

Build modern .NET applications faster with a clean layered architecture, shared API contracts, and ready-to-use admin dashboards.

DevArchitecture is an open-source framework that helps you create scalable applications using:

- **.NET 10 backend**
- Versioned REST API (`/api/v1`)
- Multiple frontend admin clients:
  - Vue
  - Angular
  - React
  - Blazor
- Extra sample projects for Angular and Flutter

---

## 🚀 Quick Links

- Documentation: https://www.devarchitecture.net
- Alternative docs site: https://devarchitecture.netlify.app
- Visual Studio Extensions:
  https://marketplace.visualstudio.com/search?term=devarchitecture&target=VS&category=All%20categories&vsVersion=&sortBy=Relevance

### .NET 10 Migration Guide

- English: `docs/BREAKING-CHANGES-NET10.md`
- Türkçe: `docs/BREAKING-CHANGES-NET10.tr.md`

---

## 📦 Project Structure

```text
DevArchitecture
├── WebAPI                    → .NET 10 backend
├── clients
│   ├── vue-admin             → Vue admin panel
│   ├── angular-admin         → Angular admin panel
│   ├── react-admin           → React admin panel
│   ├── blazor-admin          → Blazor WASM admin
│   └── blazor-admin-server   → Hosted Blazor app
└── UiPreparation
    ├── UI                    → Legacy Angular sample
    └── FlutterUi             → Flutter sample app
```

---

# 🛠 Prerequisites

Install the following tools before starting:

| Tool | Required Version |
|---|---|
| .NET SDK | 10.x |
| Node.js | Latest LTS |
| npm | Included with Node.js |
| Flutter SDK | Optional (for Flutter sample) |

## Install .NET SDK

https://dotnet.microsoft.com/download

## Install Node.js

https://nodejs.org

## Install Flutter

https://docs.flutter.dev/get-started/install

---

## 🔐 Enable Local HTTPS

Run this once:

```bash
dotnet dev-certs https --trust
```

---

# ⚡ Getting Started

## 1. Clone the Repository

```bash
git clone <your-repository-url>
cd DevArchitecture
```

---

## 2. Build the Backend

```bash
dotnet build
```

---

## 3. Run the API

```bash
dotnet watch run --project ./WebAPI/WebAPI.csproj
```

### API URLs

| Service | URL |
|---|---|
| Swagger | https://localhost:5101/swagger |
| REST API Base | https://localhost:5101/api/v1 |
| HTTP | http://localhost:5100 |
| HTTPS | https://localhost:5101 |

---

# 🎨 Frontend Clients

Choose any frontend you want to use.

---

## Vue Admin

Modern Vue 3 + PrimeVue admin dashboard.

### Start

```bash
cd clients/vue-admin
npm install
npm run dev
```

### Default URL

```text
http://localhost:5174
```

### Production Build

```bash
npm run build
```

---

## Angular Admin

Recommended Angular admin dashboard for new projects.

### Start

```bash
cd clients/angular-admin
npm install
npm start
```

### Default URL

```text
http://localhost:4200
```

### Production Build

```bash
npm run build
```

---

## React Admin

React + PrimeReact admin dashboard.

### Start

```bash
cd clients/react-admin
npm install
npm run dev
```

### Default URL

```text
http://localhost:5173
```

### Production Build

```bash
npm run build
```

---

## Blazor WebAssembly

Blazor WASM client using Radzen components.

### Start

```bash
dotnet run --project ./clients/blazor-admin/Blazor.Admin.csproj
```

### Publish

```bash
dotnet publish ./clients/blazor-admin/Blazor.Admin.csproj -c Release
```

---

## Blazor Hosted Server

Hosts the Blazor WASM app with ASP.NET Core.

### Start

```bash
dotnet run --project ./clients/blazor-admin-server/Blazor.Admin.Server.csproj
```

### Typical URL

```text
https://localhost:7003
```

---

# 📱 Flutter Sample App

Location:

```text
UiPreparation/FlutterUi/flutter_devarchitecture
```

### Install Dependencies

```bash
flutter pub get
```

### Run

```bash
flutter run
```

### Example Targets

```bash
flutter run -d chrome
flutter run -d macos
```

---

# 🧩 Features

- Layered .NET architecture
- Shared API contract
- JWT authentication
- Versioned REST API
- Translation and language support
- Multiple frontend implementations
- Dark mode support
- Centralized NuGet package management

---

# 🌙 Theme & UI

The Vue, Angular, and React clients include:

- Sakai-style admin layout
- Emerald primary theme
- Dark mode support
- Shared UI behavior across frameworks

Dark mode preference is stored using:

```text
localStorage → devarch.darkMode
```

---

# 📚 Important Notes

## Recommended Frontends

| Framework | Recommendation |
|---|---|
| Angular | `clients/angular-admin` |
| Vue | `clients/vue-admin` |
| React | `clients/react-admin` |
| Blazor | `clients/blazor-admin` |

## Legacy Projects

These folders are older samples and mainly kept for reference:

```text
UiPreparation/UI
UiPreparation/FlutterUi
```

---

# 🤝 Contributing

Contributions are welcome.

You can help by:

- Reporting bugs
- Suggesting features
- Improving documentation
- Submitting pull requests

See:

```text
.github/ISSUE_TEMPLATE/
.github/pull_request_template.md
```

---

# ⭐ Support the Project

If DevArchitecture helps you, please consider giving the repository a star on GitHub.