# Doğrulama ve hata yönetimi

Durum: tasarım; bu depoda çalışan uygulama veya yürütülmüş uygulama testleri yoktur.

## Katmanlar
HTML required/min/max ve unobtrusive validation kullanıcı deneyimi içindir; istemci kontrolleri atlanabilir. Sunucu ViewModel/DTO DataAnnotations (Required, StringLength, Range), ModelState ve servis ilişki/zaman/owner kontrolleri zorunludur. DB check/FK son bariyer. Domain entity doğrudan model binding'e açılmaz; UserId, CreatedAt, CompletedAt, RowVersion server-owned.

## Veri ve zaman
Adlar trim sonrası değerlendirilir; yalnız boşluk geçersiz. Sınav eski olabilir; bozuk tarih olamaz. Takvim alanları DateOnly, oturum/sınav anları offset ile; kullanıcı yerel saat formu ambiguous/nonexistent DST zamanıysa offset seçimi istenir veya alan hatası. MVC server saatini istemci saatinden türetmez; timer start/stop IClock. Bitiş gelecekte, süre<=0, >24saat geçersiz. Saniye hassasiyetine normalize sonrası karşılaştır; aynı saniye stop 400, kullanıcı sonra yeniden bitirebilir.

| Durum | MVC | API tasarımı | Log |
| --- | --- | --- | --- |
| Alan hatası | View aynı girdiler+ModelState; HTTP200 | 400 errors | Normal doğrulama Error olarak loglanmaz |
| Geçersiz id biçimi | 400 hata sayfası | 400 | Olay tipi |
| Kayıt yok veya başkasının | 404 genel sayfa | 404 aynı detail | Kaynak türü, traceId; kişisel içerik yok |
| Giriş yok | Login returnUrl yerel URL | 401 | Güvenli auth olayı |
| Rol/policy izin yok | 403 | 403 | Güvenli olay |
| Arşiv/unique/çakışma | 409 anlaşılır mesaj | 409 ProblemDetails | Constraint adı güvenli teknik log |
| Eski RowVersion | 409, yeniden yükleme seçeneği | 409; eksik If-Match428 | Concurrency olayı |
| Beklenmedik hata | 500 genel sayfa | 500 traceId | Exception teknik ayrıntısı; request body yok |

## Tekrarlama ve exception
Create OperationId kullanıcı+işlem altında receipt ile; PRG tek başına double-click ağ tekrarını engellemez. DB unique ihlalleri yalnız beklenen constraint adıysa Conflict'e çevrilir; tüm DbUpdateException'ları 409 saymak gerçek hatayı gizler. Receipt hash aynıysa önceki sonucu döndür; farklıysa conflict. Completed/stop tekrarlarında ilk tamamlanma anı korunur. Cancellation beklenen istek iptali olarak ele alınır. Kullanıcıya stack trace, connection string, SQL ve hesap varlığı gönderilmez.

[Dokümantasyon indeksi](../README.md)
