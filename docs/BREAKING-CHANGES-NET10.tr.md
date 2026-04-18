# Kırıcı değişiklikler: .NET 10 geçişi

**Diller:** [English](BREAKING-CHANGES-NET10.md) | Türkçe

---

Bu belge, çözümün **.NET 10** (`net10.0`) seviyesine taşınmasıyla **geliştiriciler ve operasyon** tarafında neyin değiştiğini özetler (araç zinciri, NuGet, istemciler). Microsoft’un platform sürüm notlarının tam kopyası değildir; çerçeve genelindeki kırıcı değişiklikler için resmi bağlantılara bakın.

**İlgili dokümanlar**

- İngilizce README: [README.md](../README.md)
- Türkçe README: [README.tr.md](../README.tr.md)

---

## 1. Kimleri etkiler?

| Alan | Etki |
|------|------|
| **Backend / kütüphaneler** | Tüm `*.csproj` hedefi **`net10.0`**. Eski SDK ile derlenemez. |
| **Testler** | `Tests` projesi **`net10.0`**. |
| **Blazor** | `clients/blazor-admin` (WASM) ve `clients/blazor-admin-server` **`net10.0`**. |
| **Araçlar** | Depo kökündeki **`global.json`** .NET SDK sürümünü sabitler (§2). CI ve ajanlar uyumlu SDK kurmalı. |
| **NuGet** | **`Directory.Packages.props`** ile **merkezi paket yönetimi**; merkezi yönetilen paketlerde sürüm artık çoğunlukla proje bazında değil. |
| **JavaScript admin uygulamaları** | **Node.js LTS** gereksinimi sürer; README’deki sürüm aralıkları geçerlidir. |

Aşağı akış fork’ları **`net7.0` / `net8.0` / `net9.0`** referanslıysa TFM ve paket uyumunu güncellemelidir.

---

## 2. SDK ve çalışma zamanı gereksinimleri

- Bu depoyu derleyen her makine ve CI ajanında **[.NET SDK 10.x](https://dotnet.microsoft.com/download)** kurulu olmalı.
- Depo kökündeki **`global.json`** SDK’yı örneğin şöyle sabitler:

  ```json
  {
    "sdk": {
      "version": "10.0.201",
      "rollForward": "latestPatch"
    }
  }
  ```

  Patch sürümünü takım politikanıza göre güncelleyin; `rollForward: latestPatch` aynı minor içinde yeni yamaları kabul eder.

- **Dağıtım:** IIS, Kestrel, konteyner vb. ortamlar uygun **.NET 10** çalışma zamanını kullanmalıdır.

---

## 3. Hedef çatı (TFM)

Ana projelerde:

```xml
<TargetFramework>net10.0</TargetFramework>
```

**Kırıcı:** Bu çözümün paketlerini **`netstandard2.0` tüketen** harici projeler, dağıtım modelinize göre **`net10.0`** (veya uyumlu TFM) ile hizalanmak zorunda kalabilir.

---

## 4. NuGet: merkezi paket yönetimi (CPM)

- **`Directory.Packages.props`** tüm depo için **paket başına tek sürüm** tanımlar (`ManagePackageVersionsCentrally`).
- İlgili `csproj` dosyalarında merkezi paketler için `PackageReference` genelde **sürümsüzdür**.

**Bakımçılar için kırıcı noktalar:**

- Sürüm eklemek / yükseltmek **`Directory.Packages.props`** üzerinden yapılır; dağınık `Version` kullanımı restore hatalarına yol açabilir.
- **`NuGet.config`**: Varsa paket kaynaklarını tanımlar (nuget.org ve isteğe bağlı kurumsal feed’ler). Kurumsal ağlarda bu feed’lere izin veya ayna gerekir.

---

## 5. Microsoft.AspNetCore.* ve Entity Framework Core 10

`Directory.Packages.props` ile hizalı örnek sürümler:

- `Microsoft.AspNetCore.Authentication.JwtBearer` **10.x**
- `Microsoft.EntityFrameworkCore.*` **10.x**
- `Microsoft.AspNetCore.Components.WebAssembly*` **10.x** (Blazor WASM)

**Yapılacaklar:** Eski ana sürümden geliyorsanız ASP.NET Core ve EF Core için Microsoft’un kendi geçiş / kırıcı değişiklik belgelerini inceleyin:

- [.NET 10 yenilikler](https://learn.microsoft.com/dotnet/core/whats-new/dotnet-10/overview)
- [ASP.NET Core geçiş rehberleri](https://learn.microsoft.com/aspnet/core/migration/)
- [EF Core sürüm planı](https://learn.microsoft.com/ef/core/what-is-new/)

**Başlangıç**, **OpenAPI/Swagger**, **JWT**, **veritabanı sağlayıcıları** gibi alanlarda ek uyarlama gerekebilir.

---

## 6. WebAPI ve barındırma

- **`WebAPI/Properties/launchSettings.json`** profilleri (ör. `Dev_*`) [README.md](../README.md) ile uyumlu HTTP/HTTPS portlarına bağlanır.
- **`WebAPI/Dockerfile`** kullanılıyorsa taban imaj **.NET 10** olmalı; geçişten sonra imajları yeniden üretin.

**Kırıcı:** Bağlantı dizeleri, gizli anahtarlar ve ortam yapılandırması yeni çalışma zamanında yeniden doğrulanmalıdır.

---

## 7. `clients/` altındaki istemciler

Bunlar .NET 10 ikilisi değildir; aynı API sözleşmesiyle birlikte yayınlanır:

| İstemci | Not |
|---------|-----|
| **Vue / Angular / React** | **Node.js LTS** ve uygulama bazında `npm install`. Sürümler README’de. |
| **Blazor WASM + Server** | **.NET 10 SDK** gerekir; Radzen ve WASM paketleri **`Directory.Packages.props`** ile uyumludur. |

Varsayılan API tabanı **`https://localhost:5101/api/v1`** (ortam değişkenleriyle değiştirilebilir).

---

## 8. UiPreparation örnekleri

- **`UiPreparation/UI`**: Angular; `package.json` içindeki Node motor gereksinimleri.
- **`UiPreparation/FlutterUi/...`**: `pubspec.yaml` içinde Dart SDK kısıtları.

Bunlar **örnek**; WebAPI ile entegrasyon dışında `net10.0` dışı bağımsız kırıcılar içerebilir.

---

## 9. CI / GitHub Actions

**`.github/workflows/`** altındaki iş akışları (ör. `ci.yml`, `codeql.yml`, `clients-ui.yml`) **.NET 10** kurulumu veya imaj kullanmalıdır. Hâlâ `dotnet-version: 8.x` / `9.x` sabitliyse derleme başarısız olur.

---

## 10. Geçiş kontrol listesi

- [ ] Yerelde **.NET SDK 10.x** kurulu; `dotnet --version` doğrulandı.
- [ ] Depo kökünde `dotnet restore`; feed / CPM sorunları giderildi.
- [ ] Tam çözüm `dotnet build`; bağımlılık / API kırılmaları düzeltildi.
- [ ] **`Tests`** çalıştırıldı; kırılan testler onarıldı.
- [ ] **WebAPI** ayağa kaldırıldı; Swagger ve kimlik doğrulama duman testi yapıldı.
- [ ] `clients/` altındaki her istemci (ve kullanılıyorsa Blazor server host) denendi.
- [ ] **Dockerfile** ve dağıtım imajları **.NET 10** ile güncellendi.
- [ ] **CI** matrisleri **SDK 10.x** kullanacak şekilde güncellendi.

---

## 11. Geri alma stratejisi

`net10.0` geçişinden önce bir Git dalı veya etiket saklayın. Düşürme, pratikte o birleşmeyi geri almakla mümkündür; TFM ve paket kilitlerini dikkatsizce karıştırmak desteklenmez.

---

*Bu dosya, .NET 10 geçişi sırasındaki depo durumuna göre güncellenmiştir. TFM, CPM veya desteklenen SDK bandı değiştiğinde belgeyi yenileyin.*
