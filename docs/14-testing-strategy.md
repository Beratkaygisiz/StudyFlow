# Test stratejisi

Durum: tasarım; bu depoda çalışan uygulama veya yürütülmüş uygulama testleri yoktur.

## Hedef ve mevcut durum
Bu depo test planıdır; aşağıdaki senaryolar çalıştırılmış test sonucu değildir. Test yazımı her fazın işine dahildir; tüm testler Faz6'ya bırakılmaz. Tersine her küçük CSS/metin değişimine birim test yazma: riskli hesap/kısıtlar otomatik, görsel ayrıntılar manuel doğrulanır.

| Seviye | Ne doğrulanır | Araç / veri |
| --- | --- | --- |
| Birim | Oran, hafta sınırı, gecikme, durum geçişi, zaman aralığı kırpma | xUnit; sabit IClock; entity/DTO |
| MVC entegrasyon | GET HTML, form ModelState, PRG, anti-forgery, owner, auth redirect | WebApplicationFactory + test auth handler; gerçek auth için ayrıca Identity cookie testi |
| DB entegrasyon | Check/FK/unique/rowversion/transaction/çakışma yarışı | İzole gerçek SQL Server; sağlayıcı kritik |
| API entegrasyon (opsiyonel) | JSON/DTO, status, ProblemDetails, If-Match, idempotency | API uygulanırsa WebApplicationFactory; uygulanmadıysa N/A |
| Güvenlik | A/B ve ilişkili FK izolasyonu, CSRF, XSS, anonim erişim | MVC+SQL entegrasyon ve tarayıcı |
| UI/responsive | 360/768/1440, focus,label,menü, boş/hata/success | Manuel tarayıcı kontrolü; gerekirse Playwright |
| Kabul | FR ve faz kontrol listeleri | TC envanteri ve gösterim; üç ekip üyesi çapraz kontrol |
| Performans | NFR-002 hedefi; yavaş sorgu, p95 | Faz6 sabit makine/veri ve ölçüm raporu |

## Sağlayıcı farkı
EF InMemory ilişkisel FK/transaction/rowversion garantisi vermez; davranışın kanıtı olarak kullanma. SQLite hızlı sorgu smoke test için olabilir fakat SQL Server filtered index, datetimeoffset, collation, lock hints, SQL error code ve concurrency farklarını doğrulamaz. Kritik veri bütünlüğü ve yarış testleri gerçek SQL Server üzerinde zorunlu; yerel instance/kurum sunucusu/container seçimi ortam kararıdır. Unit testlerin DB'siz olması tercih edilir.

## Test verisi ve bağımsızlık
Her entegrasyon test suite için benzersiz DB adı; migration apply; A ve B sahipleri, aktif/arşivli ders ve tarih sınırı seed. SQL yarış testleri iki ayrı DbContext/transaction ve eşzamanlı başlatma bariyeriyle; tek transaction rollback fixture yarış izolasyonunu bozabilir. Test sonunda DB sil; kullanıcı gerçek verisi yok. Saat sabit2026-11-12T12:00:00Z; varsayılan Europe/Istanbul. DST testi ayrı Europe/Berlin. Testler sırasız/bağımsız, paralel ortak DB kullanmaz. Identity middleware davranışı fake handler ile tam kanıtlanmaz; en az gerçek kayıt-login-cookie-logout entegrasyonu zorunlu.

## Çalıştırma ve kanıt
Gelecekte `dotnet test` komutu solution test projelerini çalıştırır. Şu depoda csproj yok; komut bugün uygulanabilir değildir. Faz geçiş raporu test adı, sürüm/commit, ortam, sonuç ve başarısızlıkları kaydeder. Kritik Must başarısızsa kapı kapanır. API yoksa API’ye özel test N/A gerekçesi yazılır, MVC karşılığı test edilir. Sadece coverage yüzdesi kalite ölçüsü değildir; BR'lerin olumsuz/sınır senaryoları önemlidir.

[Dokümantasyon indeksi](../README.md)
