# DevArchitecture

**Diller:** [English (İngilizce)](README.md) | Türkçe

---

DevArchitecture, .NET tabanlı katmanlı mimari ve paylaşılan WebAPI sözleşmesiyle hızlı uygulama geliştirmeyi hedefleyen açık kaynak bir çerçevedir. Bu depoda **.NET 10** backend, **`/api/v1`** REST yüzeyi ve **birden fazla yönetim istemcisi** (Vue, Angular, React, Blazor) bir arada tutulur; ayrıca **`UiPreparation`** altında eski Angular ve Flutter örnekleri bulunur.

- Resmi dokümantasyon: [devarchitecture.net](https://www.devarchitecture.net) (ayrıca [devarchitecture.netlify.app](https://devarchitecture.netlify.app/))
- Visual Studio uzantıları: [Visual Studio Marketplace — DevArchitecture](https://marketplace.visualstudio.com/search?term=devarchitecture&target=VS&category=All%20categories&vsVersion=&sortBy=Relevance)
- **.NET 10 geçişi / kırıcı değişiklikler:** [docs/BREAKING-CHANGES-NET10.tr.md](docs/BREAKING-CHANGES-NET10.tr.md) · [English](docs/BREAKING-CHANGES-NET10.md)
- **GitHub:** [Issue şablonları](.github/ISSUE_TEMPLATE/) ve [pull request şablonu](.github/pull_request_template.md) (hata, özellik, dokümantasyon, soru)

![](https://www.devarchitecture.net/assets/images/image1-ce8537e256c57d119ad5559b6217d4c9.png)

---

## Gereksinimler

| Araç | Sürüm / not |
|------|-------------|
| [.NET SDK](https://dotnet.microsoft.com/download) | **10.x** (`TargetFramework`: `net10.0`) |
| [Node.js](https://nodejs.org/) | **LTS** (çoğu istemci için); `UiPreparation/UI` için `package.json` içinde **Node ≥ 20.11.1**, **npm ≥ 10.5.0** tanımlıdır |
| npm | Node ile birlikte gelir |
| [Flutter SDK](https://docs.flutter.dev/get-started/install) | `UiPreparation/FlutterUi` için: `pubspec.yaml` **Dart SDK `>=3.4.1 <4.0.0`** (yorumda test: **3.22.3**) |

HTTPS ile yerel API ve istemciler için (bir kez):

```bash
dotnet dev-certs https --trust
```

---

## Backend (WebAPI)

| | |
|---|---|
| **Çözüm sürümü** | `WebAPI/WebAPI.csproj` → `Version` **1.0.0**, `TargetFramework` **net10.0** |
| **NuGet** | Merkezi sürümler: **`Directory.Packages.props`** |

**Kurulum / derleme**

```bash
dotnet build
```

**Çalıştırma (geliştirme)**

```bash
dotnet watch run --project ./WebAPI/WebAPI.csproj
```

- Swagger (Development): `https://localhost:5101/swagger`
- HTTP: `5100`, HTTPS: `5101` (`WebAPI/Properties/launchSettings.json` → profil `Dev_Development`)
- Varsayılan API kökü: **`https://localhost:5101/api/v1`**

---

## İstemciler — `clients/`

Özet tablo (detaylar aşağıdaki alt başlıklarda):

| Klasör | Kurulum | Geliştirme | Tipik adres |
|--------|---------|------------|-------------|
| `clients/vue-admin` | `npm install` | `npm run dev` | `http://localhost:5174` |
| `clients/angular-admin` | `npm install` | `npm start` | `http://localhost:4200` |
| `clients/react-admin` | `npm install` | `npm run dev` | `http://localhost:5173` |
| `clients/blazor-admin` | — | `dotnet run --project ./clients/blazor-admin/Blazor.Admin.csproj` | Konsoldaki URL |
| `clients/blazor-admin-server` | — | `dotnet run --project ./clients/blazor-admin-server/Blazor.Admin.Server.csproj` | Örn. `https://localhost:7003` |

### Vue — `clients/vue-admin`

| | |
|---|---|
| **Amaç** | PrimeVue + Aura temalı yönetim arayüzü |
| **Kurulum** | `cd clients/vue-admin` → `npm install` |
| **Geliştirme** | `npm run dev` (Vite `vite.config.ts`: varsayılan port **5174**, doluysa sıradaki port) |
| **Üretim derlemesi** | `npm run build` |
| **Önizleme** | `npm run preview` |

**Bağımlılık sürümleri** (`package.json`): `vue` **^3.5.32**, `primevue` **^4.5.5**, `@primeuix/themes` **^2.0.3**, `primeicons` **^7.0.0**, `vue-router` **^5.0.4**, `axios` **^1.15.0**, `jwt-decode` **^4.0.0**, `vite` **^8.0.4**, `typescript` **~6.0.2**

API tabanı: `src/config/modules.ts` → `API_BASE_URL` = `https://localhost:5101/api/v1`

### Angular — `clients/angular-admin`

| | |
|---|---|
| **Amaç** | PrimeNG + Aura temalı yönetim arayüzü (**yeni Angular admin için önerilen**) |
| **Kurulum** | `cd clients/angular-admin` → `npm install` |
| **Geliştirme** | `npm start` (`ng serve --poll 1000`, varsayılan **4200**) |
| **Üretim derlemesi** | `npm run build` |

**Bağımlılık sürümleri** (`package.json`): `@angular/core` ve ilgili paketler **^19.2.x**, `primeng` **^19.1.4**, `@primeuix/themes` **^2.0.3**, `primeicons` **^7.0.0**, `rxjs` **~7.8.0**, `zone.js` **~0.15.0**, `typescript` **~5.7.2**, `@angular/cli` **^19.2.24**

API tabanı: `src/environments/environment.ts` → `apiBaseUrl` = `https://localhost:5101/api/v1`

### React — `clients/react-admin`

| | |
|---|---|
| **Amaç** | PrimeReact (Lara green light/dark) ile yönetim arayüzü |
| **Kurulum** | `cd clients/react-admin` → `npm install` |
| **Geliştirme** | `npm run dev` (Vite varsayılan port **5173**) |
| **Üretim derlemesi** | `npm run build` |
| **Lint** | `npm run lint` |

**Bağımlılık sürümleri** (`package.json`): `react` / `react-dom` **^19.2.4**, `primereact` **^10.9.7**, `@primeuix/themes` **^2.0.3**, `primeicons` **^7.0.0**, `react-router-dom` **^7.14.1**, `axios` **^1.15.0**, `vite` **^8.0.4**, `typescript` **~6.0.2**

API tabanı: `src/config/modules.ts` → `API_BASE_URL` = `https://localhost:5101/api/v1`

### Blazor WebAssembly — `clients/blazor-admin`

| | |
|---|---|
| **Amaç** | Radzen bileşenleriyle WebAPI’ye bağlı WASM istemci |
| **Kurulum** | Bağımlılıklar NuGet ile; `dotnet restore` veya çözüm derlemesi yeterli |
| **Çalıştırma** | `dotnet run --project ./clients/blazor-admin/Blazor.Admin.csproj` |
| **Üretim** | `dotnet publish ./clients/blazor-admin/Blazor.Admin.csproj -c Release` |

**Sürümler**: `TargetFramework` **net10.0**; `Radzen.Blazor` sürümü **`Directory.Packages.props`** içinde (ör. **6.0.0**); `Microsoft.AspNetCore.Components.WebAssembly` vb. aynı dosyada **10.0.6**.

API tabanı: `Services/ApiClient.cs` → `BaseUrl` = `https://localhost:5101/api/v1`

> Yerelde WASM dosyaları / HTTPS sorunlarında **`clients/blazor-admin-server`** ile çalıştırmak daha güvenilir olabilir (proje içi nota bakın).

### Blazor Server (barındırıcı) — `clients/blazor-admin-server`

| | |
|---|---|
| **Amaç** | Aynı Blazor WASM uygulamasını ASP.NET Core üzerinden sunmak |
| **Çalıştırma** | `dotnet run --project ./clients/blazor-admin-server/Blazor.Admin.Server.csproj` |
| **Adres** | `Properties/launchSettings.json`: örn. **https://localhost:7003**, **http://localhost:5062** (profil `https`) |

---

## UiPreparation örnekleri

### Angular (önceki nesil) — `UiPreparation/UI`

| | |
|---|---|
| **Durum** | Eski **Material Dashboard** tabanlı örnek; **yeni çalışmalar için `clients/angular-admin` önceliklidir.** |
| **Kurulum** | `cd UiPreparation/UI` → `npm install` |
| **Motor gereksinimi** | `package.json` → `engines`: **Node ≥ 20.11.1**, **npm ≥ 10.5.0** |
| **Geliştirme** | `npm start` (`ng serve`, varsayılan **http://localhost:4200**) |
| **Üretim derlemesi** | `npm run build` |
| **Paket adı / sürüm** | `material-dashboard-angular` **2.5.0** |

**Öne çıkan bağımlılıklar** (`package.json`): `@angular/core` **^19.2.15**, `@angular/material` **^19.2.19**, `@angular/cdk` **^19.2.19**, `bootstrap` **4.5.2**, `@ngx-translate/core` **^15.0.0**, `@auth0/angular-jwt` **^4.2.0**, `rxjs` **^7.8.2**, `typescript` **~5.5.4**, `@angular/cli` **^19.2.17**

API: `src/environments/environment.ts` → `getApiUrl` = `https://localhost:5101/api/v1`

> Aynı makinede **`clients/angular-admin`** ile birlikte çalıştırırken port **4200** çakışır; birini durdurun veya `ng serve --port` ile port değiştirin.

### Flutter — `UiPreparation/FlutterUi/flutter_devarchitecture`

| | |
|---|---|
| **Amaç** | Mobil / çok platformlu örnek istemci (Bloc, Firebase, vb.) |
| **Kurulum** | `cd UiPreparation/FlutterUi/flutter_devarchitecture` → `flutter pub get` |
| **Çalıştırma** | `flutter run` (hedef için: `flutter devices` → `flutter run -d chrome` / `-d macos` / Android emülatör) |
| **SDK** | `pubspec.yaml` → `environment.sdk`: **`>=3.4.1 <4.0.0`** (yorum: test **3.22.3**) |
| **Uygulama sürümü** | `version` **0.1.0** |

**Öne çıkan bağımlılıklar** (`pubspec.yaml`): `flutter_bloc` **8.1.6**, `http` **1.3.0**, `flutter_modular` **6.3.4**, `get_it` **8.0.0**, `intl` **0.20.2**, Firebase paketleri (ör. `firebase_core` **3.8.1**, `cloud_firestore` **5.5.0**), `data_table_2` **2.5.15**, `lottie` **3.1.2**, `jwt_decoder` **2.0.1**

---

## Ortak özellikler ve tema

- **API**: JWT, sürümlü REST (`/api/v1`), çeviri / dil kodları ve yönetim modülleri için paylaşılan sözleşme.
- **Görünüm (`clients/*`)**: Sakai tarzı tutarlı kabuk (zümrüt birincil, slate nötrler). Vue / Angular / React’ta karanlık mod **`localStorage`** anahtarı **`devarch.darkMode`**; Blazor’da Radzen tema değişkenleri aynı palete hizalanır.
- **NuGet**: **`Directory.Packages.props`** merkezi paket sürümleri.

---

## Bu projeyi beğendiyseniz

Yıldız vererek depoyu destekleyebilirsiniz.
