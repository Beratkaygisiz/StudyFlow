# Geliştirme yol haritası

Durum: tasarım; bu depoda çalışan uygulama veya yürütülmüş uygulama testleri yoktur.

## Planın kullanımı
Fazlar takvim haftası değildir; yalnız Faz1 ilk ders teslimidir. Tahminler insan ekip saatidir, AI yanıt süresi değildir; üç kişinin çalışma süresi toplanır. Toplam 192–312 ekip saati (kişi başına64–104) başlangıç aralığı; C#/EF öğrenme ve yeni ders isterleri artırabilir. Faz0 doküman dosyalarının üretilmesi kapsam değerlendirmesinin ekipçe tamamlandığı anlamına gelmez.

API Faz6 kabulünün zorunlu girdisi değildir. Yapılırsa güvenli temel sonrası ayrıca tahminlendirilir; bu tabloda saatleri yoktur. Üç kişi görevlendirmesi [ekip planında](22-team-plan.md).

## Faz 0 — Analiz ve dokümantasyon

| Alan | Tanım |
| --- | --- |
| Amaç/kapsam | Gereksinim/model/sözleşme/test tutarlılığını oluştur |
| Ön koşul/bağımlılık | Kullanıcı promptu; sonraki ders isterleri bilinmiyor |
| İşler | Belgeleri oku; açık kararları issue’ya çevir; üç kişi aynı örnek akışı açıklasın |
| Bileşen/dosyalar | StudyFlow-Documentation; backlog |
| Tahmini yük | 18–30 ekip saati |
| Risk | Gereksiz detay/kapsam |
| Tamamlanma | Faz 0 kabul listesi; önceki faz regresyonu |
| Sunum çıktısı | ZIP, 3 Controller/6 View matrisi ve veri akışı göster |
| Gereksinimler | Faz0 tüm sözleşme incelemesi; Faz6 tüm FR ve NFR regresyon |

## Faz 1 — İlk ders teslimi: MVC iskeleti

| Alan | Tanım |
| --- | --- |
| Amaç/kapsam | Bilinen ilk teslimi çalışan UI ile karşıla |
| Ön koşul/bağımlılık | Faz0; desteklenen SDK/paket ve ders lab uyumu doğrulandı |
| İşler | MVC template; DemoData; altı View; CSS/layout; Razor/filter; UI smoke |
| Bileşen/dosyalar | StudyFlow.Web; Controllers/Views/ViewModels/DemoData/site.css |
| Tahmini yük | 18–30 ekip saati |
| Risk | Sahte CRUD veya aynı sayfayı kopyalama |
| Tamamlanma | Faz 1 kabul listesi; önceki faz regresyonu |
| Sunum çıktısı | Altı ekranda gezin, farklı Id verisi ve boş durum göster |
| Gereksinimler | FR-032, FR-033 |

## Faz 2 — Domain, veri erişimi ve CRUD

| Alan | Tanım |
| --- | --- |
| Amaç/kapsam | Kalıcı ders ve sınav |
| Ön koşul/bağımlılık | Faz1; yerel SQL Server; SDK/paket karar kaydı |
| İşler | ApplicationUser demo owner; DbContext; Course/Exam; migration; services; CRUD forms; receipt/rowversion; DB test |
| Bileşen/dosyalar | Domain; Infrastructure; Application; Courses/Exams forms; test projeleri |
| Tahmini yük | 30–48 ekip saati |
| Risk | SQL/EF deneyimsizliği, concurrency |
| Tamamlanma | Faz 2 kabul listesi; önceki faz regresyonu |
| Sunum çıktısı | Yeniden başlatmada kayıtlar; arşiv ve bağlı silme engeli |
| Gereksinimler | FR-001, FR-002, FR-003, FR-004, FR-005, FR-006, FR-007, FR-008, FR-028, FR-034, FR-035, FR-036 |

## Faz 3 — Konu, görev ve çalışma planı

| Alan | Tanım |
| --- | --- |
| Amaç/kapsam | Yapılacak iş ve planlamayı bağla |
| Ön koşul/bağımlılık | Faz2 kalıcı owner/course |
| İşler | Topic/Task/Plan mappings+migration; service/UI; durum ve tarih filtreleri; plan unique testi |
| Bileşen/dosyalar | TopicsController; StudyTasksController; StudyPlansController; Views; tests |
| Tahmini yük | 36–54 ekip saati |
| Risk | Görev/plan kavram karışıklığı |
| Tamamlanma | Faz 3 kabul listesi; önceki faz regresyonu |
| Sunum çıktısı | Konu→görev→hafta planı; Completed etiketi |
| Gereksinimler | FR-009, FR-010, FR-011, FR-012, FR-013, FR-014, FR-015, FR-016, FR-017, FR-018, FR-019 |

## Faz 4 — Oturumlar ve istatistik

| Alan | Tanım |
| --- | --- |
| Amaç/kapsam | Gerçek çalışmayı ölç |
| Ön koşul/bağımlılık | Faz3 task ve plan servisleri |
| İşler | Session timer/manual; owner transaction; aralık/DST; Dashboard/Statistics sorguları; test |
| Bileşen/dosyalar | StudySessionsController; StatisticsController; services; migration; Views |
| Tahmini yük | 36–60 ekip saati |
| Risk | Çifte sayım/zaman sınırı/yazma yarışı |
| Tamamlanma | Faz 4 kabul listesi; önceki faz regresyonu |
| Sunum çıktısı | 60dk plan ve45dk gerçek; haftalık fark ve tarih kırpma |
| Gereksinimler | FR-020, FR-021, FR-022, FR-023, FR-024, FR-025, FR-026, FR-027 |

## Faz 5 — Kullanıcı yönetimi ve güvenlik

| Alan | Tanım |
| --- | --- |
| Amaç/kapsam | Gerçek kullanıcı izolasyonu |
| Ön koşul/bağımlılık | Faz4 yerel akış; Identity altyapısı mevcut |
| İşler | Register/login/logout/profile; CurrentUser claims; middleware; CSRF/XSS; A/B gerçek Identity test; demo guard |
| Bileşen/dosyalar | Identity pages; ProfileController; authorization; security tests |
| Tahmini yük | 30–48 ekip saati |
| Risk | Owner filtresi atlama; demo hesap sızıntısı |
| Tamamlanma | Faz 5 kabul listesi; önceki faz regresyonu |
| Sunum çıktısı | A/B login ve çapraz Id404; profil ve logout |
| Gereksinimler | FR-029, FR-030, FR-031 |

## Faz 6 — Sistem kabulü ve dağıtım

| Alan | Tanım |
| --- | --- |
| Amaç/kapsam | Yayın adayını doğrula |
| Ön koşul/bağımlılık | Faz5 Must + güvenlik testleri geçiyor |
| İşler | Regression; responsive; NFR ölçüm; bağımsız DB yükseltme; yedek geri yükleme; ortam/host kararı; doküman kod eşleme |
| Bileşen/dosyalar | CI; deployment config; acceptance results; v1.0.0 candidate |
| Tahmini yük | 24–42 ekip saati |
| Risk | Dağıtım/secret/DB migration |
| Tamamlanma | Faz 6 kabul listesi; önceki faz regresyonu |
| Sunum çıktısı | HTTPS ortamda akış; yedek geri yükleme kanıtı |
| Gereksinimler | Faz0 tüm sözleşme incelemesi; Faz6 tüm FR ve NFR regresyon |

## Haftalık yeni isterler
Yeni ders metnini kaydet → issue ve yeni FR kimliği aç → model/UI/test/bağımlılık etkisini incele → kabul listesini revize et → aşağı öncelikli işi ertele veya yükü artır → üç kişinin puan toplamlarını dengele. Bilinmeyen haftalar için tarihe sabit özellik taahhüdü verme. İlk teslimde yalnız bilinen şartlar zorunlu. Öğretim görevlisi DB/auth erken isterse ilgili faz işini öne al; auth olmadan public deploy etme.

[Dokümantasyon indeksi](../README.md)
