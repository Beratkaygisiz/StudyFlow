# Güvenlik tasarımı

Durum: tasarım; bu depoda çalışan uygulama veya yürütülmüş uygulama testleri yoktur.

## Durum ve aşamalar
Güvenlik planlanmıştır, uygulanmış değildir. Faz1 demo görüntüleme; Faz2–4 sabit owner ile yalnız Development/yerel; Faz5 Identity UI ve auth middleware, zorunlu sahiplik; Faz6 HTTPS Production ve güvenlik kapısı. Demo modu Production’da fail-fast. `ICurrentUser` yoksa servis işlem yapamaz. Faz2 AspNetUsers seed altyapısı bulunması giriş yeteneği değildir.

## Identity ve yetki
Identity password hashing/validator/lockout altyapısı kullanılır, özel hash yazılmaz. Proje hedef ayarı: en az12 karakter, 5 başarısız denemede15 dakika lockout; parola üst sınırı128, paste/password manager desteklenir; e-posta doğrulama ve kurtarma servisi henüz kararlaştırılmadı. Doğrulama servisi olmadan confirmed-email şartını açıp kullanıcıları kilitleme. Production yayında spam/rate-limit politikası seçilen framework sürümünde doğrulanmalı. Authentication kim olduğunu, Authorization bu kaydın sahibi olup olmadığını belirler; yalnız Authorize attribute sahipliği çözmez.

Her sorgu UserId=current claim kapsamı; id ile bulup owner kontrolünü atlama. Alt kaynak FK’leri owner+ders uyumuyla. İstemci UserId güvenilir değil ve DTO’da yok. Başka kayda 404; liste ve toplamlar da owner filtreli. Identity rolü/admin bulunmaz; profil sadece kendisinin.

## Web güvenliği
Cookie HttpOnly/Secure/SameSite=Lax; HTTPS, Production HSTS; local HTTP geliştirmenin istisnası belgelenir. MVC state-changing formda ValidateAntiForgeryToken, logout POST; GET değişiklik yapmaz. Cookie API de token header; CORS varsayılan kapalı, wildcard credential yok. SameSite anti-forgery yerine geçmez. Razor otomatik encode; notes plain text; Html.Raw kullanıcı içeriklerinde yasak. DOM textContent; HTML destek gereksinimi yok. CSP önce report-only ve Bootstrap kaynaklarını doğrulayarak uygulanır; asset sürümleri başlangıçta sabitlenir. EF parametreli sorgu; lock SQL sabit şablon ve parametreli, string birleştirme yok.

## Sırlar ve işletim
Development user-secrets; Production environment/sağlayıcı secret store. ConnectionStrings__StudyFlow, kimlik/DataProtection anahtarları repoya konmaz. DB hesabı gerekli tablo izinleriyle; migration ayrı izinli işlem. Restart sonrası cookie geçerliliği için DataProtection keyring kalıcı/güvenli. Loglarda e-posta, notlar, parola, cookie, token, body yok; traceId ve olay/servis türü var. Hata sayfası genel. Yedekler erişim kontrollü; anonim test verileri. Saklama/hesap silme hukuki politikası ve retention süreleri açık karar, bu doküman hukuki uygunluk iddia etmez.

## Doğrulama
TC-031,036,040–043,052,058–059 ve 064: A/B izolasyonu; cross-owner FK; anonymous; anti-forgery; XSS; secret taraması; Production guard; yetkili/izinsiz profil. SQL testleri bileşik FK’yi, UI testleri tokenı gerçekten doğrular. Çok kullanıcı yayını bu kapıdan önce yapılamaz.

[Dokümantasyon indeksi](../README.md)
