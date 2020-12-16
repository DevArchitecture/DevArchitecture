namespace Core.Utilities.Messages
{
    public static class SwaggerMessages
    {
        public static string Version => "v1";
        public static string Title => "SFw";
        public static string TermsOfService => "https://example.com/terms";
        public static string ContactName => "SennedjemFw";
        public static string LicenceName => "Use under LICX";
        public static string ContactEMail => "Use under LICX";
        public static string ContactUrl => "http://keremvaris.com";
        public static string LicenceUrl => "https://example.com/license";
        public static string Description => "";
        public static string Description1 => @"### Login

Sisteme Person, Staff ve Agent'lar tarfından giriş yapılabilir. 
Bu bölümde providerlara göre API kullanım akışları anlatılmaktadır.

#### Person

**Adım 1:** Aşağıdaki alanlar doldurulur ve Auth/login metodu çağırılır.

``` json
{
  ""externalUserId"": ""TCKimlik"",
  ""mobilePhone"": ""İlk Çağırımda Boş"",
  ""provider"": ""Person""
}
```

Sistem kişinin kayıt durumuna göre aşağıdaki sonucu döner:

``` json
{
  ""status"": ""Durum"",
  ""message"": """",
  ""mobilePhones"": [ ""Tel1"" ]
}
```


**Adım 2:** Client dönen status koduna göre aşağıdaki olasılıklarla devam eder.

*UserNotFound:* İki ihtimal olabilir. 

- HemOnline'da bağışçı varsa sistem mobilePhones alanında telefonlarını döner.
- Yeni bağışçı ise boş cevap döner.
Bu durumda client gereken bilgileri doldurtup Donors metoduna POST istemi yapar.
Kullanıcı tüm sistemlerde yaratılmıştır. Bu aşamada Cep Telefonu alındığı için
doldurulup login metodu tekrar çağırılır.

*WrongCredentials:* Bu durum Person provider'ı için geçerli değildir.

*PhoneNumberRequired:* Seçilen ya da girilen cep telefonu girilerek tekrar login
çağırılır.

*ServiceError:* Veri tabanları gibi dış sistemlerden herhangi birine ulaşılamadığında
gönderilir.

*Ok:* Client One Time Password doğrulaması için kodu bekleme moduna geçer.

**Adım 3:** Sistem OTP kodu ile Auth/verify metodunu çağırır.

``` json
{
  ""provider"": ""Person"",
  ""externalUserId"": ""TcKimlik"",
  ""code"": 957225
}
```

Eğer kod doğru ise, sistem token cevabı döner:

{
    ""token"": """",
    ""expiration"": ""2020-04-21T23:54:05.5975801+03:00""
}

Hatalı ise BadRequest (Kod 400) döner.

**Adım 4:** Login tamamlanmıştır. Client Donors üzerinden citizenId parametresi
vererek (girişte kullandığı ExternalId) GET metodunu çağırır ve ana sayfasını açar.

#### Staff

**Adım 1:** Aşağıdaki alanlar doldurulur ve Auth/login metodu çağırılır.

``` json
{
  ""externalUserId"": ""Kurum Sicil No"",
  ""password"": ""HemOnline Şifresi""
  ""mobilePhone"": ""İlk Çağırımda Boş"",
  ""provider"": ""Staff""
}
```

Sistem kişinin kayıt durumuna göre aşağıdaki sonucu döner:

``` json
{
  ""status"": ""Durum"",
  ""message"": """",
  ""mobilePhones"": [ ""Tel1"" ]
}
```


**Adım 2:** Client dönen status koduna göre aşağıdaki olasılıklarla devam eder.

*UserNotFound:* Kullanıcı giriş yapamaz.

*WrongCredentials:* Tekrar deneyebilir.

*PhoneNumberRequired:* Seçilen cep telefonu girilerek tekrar login
çağırılır.

*ServiceError:* Veri tabanları gibi dış sistemlerden herhangi birine ulaşılamadığında
gönderilir.

*Ok:* Client One Time Password doğrulaması için kodu bekleme moduna geçer.

**Adım 3:** Sistem OTP kodu ile Auth/verify metodunu çağırır.

``` json
{
  ""provider"": ""Staff"",
  ""externalUserId"": ""Kurum Sicil No"",
  ""code"": 957225
}
```

Eğer kod doğru ise, sistem token cevabı döner:

{
    ""token"": """",
    ""expiration"": ""2020-04-21T23:54:05.5975801+03:00""
}

Hatalı ise BadRequest (Kod 400) döner.

**Adım 4:** Login tamamlanmıştır. Client Kurum personelinin göreceği ana
sayfaya geçer.
";
    }
}
