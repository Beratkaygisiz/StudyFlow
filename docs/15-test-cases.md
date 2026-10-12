# Test senaryoları

Durum: tasarım; bu depoda çalışan uygulama veya yürütülmüş uygulama testleri yoktur.

Her senaryo tasarlanmıştır, henüz yürütülmemiştir. API’ye özel varyantlar API yoksa N/A; MVC/domain karşılığı geçiş kapısında zorunlu. Faz bilgisi özelliğin uygulanacağı noktadır; Faz5 auth şartları önceki yerel senaryoların gerçek kullanıcıyla tekrarını gerektirir.

## TC-001 — Ders oluşturma

| Alan | İçerik |
| --- | --- |
| İlgili gereksinim | FR-001 |
| Ön koşul | Faz 2 özelliği uygulanmış; A sahibi etkin Course1; B sahibi Course2; aksi yazılmadıkça clock sabit2026-11-12T12:00Z. |
| Test verisi | Name=Algoritmalar; WeeklyTargetMinutes=120 |
| Uygulama adımları | 1) Girdideki fixture değerlerini oluştur. 2) İlgili MVC ekran/servis işlemini A olarak çalıştır. 3) HTML sonucunu ve gerekiyorsa DB/sorgu sonucunu kontrol et. 4) İşlem kabul sonucunu aşağıdaki değerlere göre karşılaştır. |
| Beklenen sonuç | Ad trim sonrası 1–120 karakter; hedef 0–10080 tam dakika. Geçerli kayıt listede görünür. Hata varyantında: Boş ad veya sınır dışı hedef kaydı engeller. |
| Test seviyesi | SQL Server + MVC entegrasyon |
| Öncelik | Yüksek |
| Durum | Not run; uygulama kodu yok |

## TC-002 — Ders listeleme ve filtre

| Alan | İçerik |
| --- | --- |
| İlgili gereksinim | FR-002 |
| Ön koşul | Faz 2 özelliği uygulanmış; A sahibi etkin Course1; B sahibi Course2; aksi yazılmadıkça clock sabit2026-11-12T12:00Z. |
| Test verisi | search=algo; status=Active |
| Uygulama adımları | 1) Girdideki fixture değerlerini oluştur. 2) İlgili MVC ekran/servis işlemini A olarak çalıştır. 3) HTML sonucunu ve gerekiyorsa DB/sorgu sonucunu kontrol et. 4) İşlem kabul sonucunu aşağıdaki değerlere göre karşılaştır. |
| Beklenen sonuç | Sadece mevcut kullanıcı kapsamı; ad içinde büyük/küçük harf duyarsız arama; Active/Archived filtresi; ad, sonra Id sırası. Hata varyantında: Geçersiz durum 400; sonuç yoksa boş liste. |
| Test seviyesi | Birim + MVC entegrasyon |
| Öncelik | Yüksek |
| Durum | Not run; uygulama kodu yok |

## TC-003 — Ders detay ve düzenleme

| Alan | İçerik |
| --- | --- |
| İlgili gereksinim | FR-003 |
| Ön koşul | Faz 2 özelliği uygulanmış; A sahibi etkin Course1; B sahibi Course2; aksi yazılmadıkça clock sabit2026-11-12T12:00Z. |
| Test verisi | id=1; Name=Veri Yapıları; WeeklyTargetMinutes=180 |
| Uygulama adımları | 1) Girdideki fixture değerlerini oluştur. 2) İlgili MVC ekran/servis işlemini A olarak çalıştır. 3) HTML sonucunu ve gerekiyorsa DB/sorgu sonucunu kontrol et. 4) İşlem kabul sonucunu aşağıdaki değerlere göre karşılaştır. |
| Beklenen sonuç | Sahip olunan ders getirilir; geçerli değişiklik kaydedilir; RowVersion eskiyse 409. Hata varyantında: Eksik kayıt 404; hatalı değer 400. |
| Test seviyesi | SQL Server + MVC entegrasyon |
| Öncelik | Yüksek |
| Durum | Not run; uygulama kodu yok |

## TC-004 — Ders arşivleme ve geri açma

| Alan | İçerik |
| --- | --- |
| İlgili gereksinim | FR-004 |
| Ön koşul | Faz 2 özelliği uygulanmış; A sahibi etkin Course1; B sahibi Course2; aksi yazılmadıkça clock sabit2026-11-12T12:00Z. |
| Test verisi | id=1; IsArchived=true/false |
| Uygulama adımları | 1) Girdideki fixture değerlerini oluştur. 2) İlgili MVC ekran/servis işlemini A olarak çalıştır. 3) HTML sonucunu ve gerekiyorsa DB/sorgu sonucunu kontrol et. 4) İşlem kabul sonucunu aşağıdaki değerlere göre karşılaştır. |
| Beklenen sonuç | Arşiv geçmişi korur; arşivli derste yeni konu, sınav, görev, plan, oturum engellenir; açık oturum varken arşiv 409. Hata varyantında: Arşivli dersin tamamlanmamış görev ve planları salt okunur; geri açılınca yeniden etkin. |
| Test seviyesi | SQL Server + MVC entegrasyon |
| Öncelik | Yüksek |
| Durum | Not run; uygulama kodu yok |

## TC-005 — Ders silme

| Alan | İçerik |
| --- | --- |
| İlgili gereksinim | FR-005 |
| Ön koşul | Faz 2 özelliği uygulanmış; A sahibi etkin Course1; B sahibi Course2; aksi yazılmadıkça clock sabit2026-11-12T12:00Z. |
| Test verisi | id=1 |
| Uygulama adımları | 1) Girdideki fixture değerlerini oluştur. 2) İlgili MVC ekran/servis işlemini A olarak çalıştır. 3) HTML sonucunu ve gerekiyorsa DB/sorgu sonucunu kontrol et. 4) İşlem kabul sonucunu aşağıdaki değerlere göre karşılaştır. |
| Beklenen sonuç | Yalnız hiçbir ilişkili Exam/Topic/StudyTask/StudyPlanItem/StudySession bulunmayan ders silinir. İlişkili kayıt varsa arşiv önerilir. Hata varyantında: İlişki varsa 409; veritabanında NO ACTION. |
| Test seviyesi | SQL Server + MVC entegrasyon |
| Öncelik | Yüksek |
| Durum | Not run; uygulama kodu yok |

## TC-006 — Sınav oluşturma

| Alan | İçerik |
| --- | --- |
| İlgili gereksinim | FR-006 |
| Ön koşul | Faz 2 özelliği uygulanmış; A sahibi etkin Course1; B sahibi Course2; aksi yazılmadıkça clock sabit2026-11-12T12:00Z. |
| Test verisi | CourseId=1; Title=Vize; Type=Midterm; ScheduledAt=2026-11-10T10:00:00+03:00 |
| Uygulama adımları | 1) Girdideki fixture değerlerini oluştur. 2) İlgili MVC ekran/servis işlemini A olarak çalıştır. 3) HTML sonucunu ve gerekiyorsa DB/sorgu sonucunu kontrol et. 4) İşlem kabul sonucunu aşağıdaki değerlere göre karşılaştır. |
| Beklenen sonuç | Aktif ve sahip olunan derse sınav eklenir; başlık 1–120; desteklenen tür; geçmiş tarih tarihsel kayıt olarak kabul edilir. Hata varyantında: Bozuk/eksik tarih 400; ders yok veya başkasına aitse 404. |
| Test seviyesi | SQL Server + MVC entegrasyon |
| Öncelik | Yüksek |
| Durum | Not run; uygulama kodu yok |

## TC-007 — Sınav liste ve detay

| Alan | İçerik |
| --- | --- |
| İlgili gereksinim | FR-007 |
| Ön koşul | Faz 2 özelliği uygulanmış; A sahibi etkin Course1; B sahibi Course2; aksi yazılmadıkça clock sabit2026-11-12T12:00Z. |
| Test verisi | scope=Upcoming/Past/All; id=1 |
| Uygulama adımları | 1) Girdideki fixture değerlerini oluştur. 2) İlgili MVC ekran/servis işlemini A olarak çalıştır. 3) HTML sonucunu ve gerekiyorsa DB/sorgu sonucunu kontrol et. 4) İşlem kabul sonucunu aşağıdaki değerlere göre karşılaştır. |
| Beklenen sonuç | Upcoming: now <= tarih < now+14 gün; Past: tarih < now; kalan gün yerel takvim tarih farkı. Hata varyantında: Boş liste geçerlidir; bilinmeyen scope 400. |
| Test seviyesi | Birim + MVC entegrasyon |
| Öncelik | Yüksek |
| Durum | Not run; uygulama kodu yok |

## TC-008 — Sınav düzenleme ve silme

| Alan | İçerik |
| --- | --- |
| İlgili gereksinim | FR-008 |
| Ön koşul | Faz 2 özelliği uygulanmış; A sahibi etkin Course1; B sahibi Course2; aksi yazılmadıkça clock sabit2026-11-12T12:00Z. |
| Test verisi | id=1; tarih ve tür değişikliği |
| Uygulama adımları | 1) Girdideki fixture değerlerini oluştur. 2) İlgili MVC ekran/servis işlemini A olarak çalıştır. 3) HTML sonucunu ve gerekiyorsa DB/sorgu sonucunu kontrol et. 4) İşlem kabul sonucunu aşağıdaki değerlere göre karşılaştır. |
| Beklenen sonuç | Aktif derste geçerli değişiklik; sahip olunan kayıt silinebilir. Hata varyantında: Arşivli derste düzenleme/silme 409; eski RowVersion 409. |
| Test seviyesi | SQL Server + MVC entegrasyon |
| Öncelik | Yüksek |
| Durum | Not run; uygulama kodu yok |

## TC-009 — Konu ekleme ve liste

| Alan | İçerik |
| --- | --- |
| İlgili gereksinim | FR-009 |
| Ön koşul | Faz 3 özelliği uygulanmış; A sahibi etkin Course1; B sahibi Course2; aksi yazılmadıkça clock sabit2026-11-12T12:00Z. |
| Test verisi | CourseId=1; Title=Diziler; SortOrder=10 |
| Uygulama adımları | 1) Girdideki fixture değerlerini oluştur. 2) İlgili MVC ekran/servis işlemini A olarak çalıştır. 3) HTML sonucunu ve gerekiyorsa DB/sorgu sonucunu kontrol et. 4) İşlem kabul sonucunu aşağıdaki değerlere göre karşılaştır. |
| Beklenen sonuç | Başlık 1–160; sıra >=0; ders içinde SortOrder, Id sıralaması. Hata varyantında: Arşivli ders 409; geçersiz sıra 400. |
| Test seviyesi | SQL Server + MVC entegrasyon |
| Öncelik | Yüksek |
| Durum | Not run; uygulama kodu yok |

## TC-010 — Konu durumu ve notu

| Alan | İçerik |
| --- | --- |
| İlgili gereksinim | FR-010 |
| Ön koşul | Faz 3 özelliği uygulanmış; A sahibi etkin Course1; B sahibi Course2; aksi yazılmadıkça clock sabit2026-11-12T12:00Z. |
| Test verisi | Status=Completed; Notes=Örnekleri çöz; ReviewOn=2026-11-12 |
| Uygulama adımları | 1) Girdideki fixture değerlerini oluştur. 2) İlgili MVC ekran/servis işlemini A olarak çalıştır. 3) HTML sonucunu ve gerekiyorsa DB/sorgu sonucunu kontrol et. 4) İşlem kabul sonucunu aşağıdaki değerlere göre karşılaştır. |
| Beklenen sonuç | Not en fazla 2000; NotStarted/InProgress/Completed; tekrar tarihi opsiyonel; geçmiş tekrar tarihi kabul edilir. Hata varyantında: Bilinmeyen durum veya geçersiz takvim tarihi 400. |
| Test seviyesi | Birim + MVC entegrasyon |
| Öncelik | Yüksek |
| Durum | Not run; uygulama kodu yok |

## TC-011 — Konu düzenleme, sıra ve silme

| Alan | İçerik |
| --- | --- |
| İlgili gereksinim | FR-011 |
| Ön koşul | Faz 3 özelliği uygulanmış; A sahibi etkin Course1; B sahibi Course2; aksi yazılmadıkça clock sabit2026-11-12T12:00Z. |
| Test verisi | id=1; SortOrder=20 |
| Uygulama adımları | 1) Girdideki fixture değerlerini oluştur. 2) İlgili MVC ekran/servis işlemini A olarak çalıştır. 3) HTML sonucunu ve gerekiyorsa DB/sorgu sonucunu kontrol et. 4) İşlem kabul sonucunu aşağıdaki değerlere göre karşılaştır. |
| Beklenen sonuç | Aktif ders koşulu; görev veya oturum konusu olarak kullanılan konu silinemez; plan ilişkisi görev üzerinden dolaylıdır. Hata varyantında: İlişkili kayıt 409; diğerleri geçerli değişiklik/silme. |
| Test seviyesi | SQL Server + MVC entegrasyon |
| Öncelik | Yüksek |
| Durum | Not run; uygulama kodu yok |

## TC-012 — Konu ilerleme

| Alan | İçerik |
| --- | --- |
| İlgili gereksinim | FR-012 |
| Ön koşul | Faz 3 özelliği uygulanmış; A sahibi etkin Course1; B sahibi Course2; aksi yazılmadıkça clock sabit2026-11-12T12:00Z. |
| Test verisi | 1 derste 4 konu; 1 Completed |
| Uygulama adımları | 1) Girdideki fixture değerlerini oluştur. 2) İlgili MVC ekran/servis işlemini A olarak çalıştır. 3) HTML sonucunu ve gerekiyorsa DB/sorgu sonucunu kontrol et. 4) İşlem kabul sonucunu aşağıdaki değerlere göre karşılaştır. |
| Beklenen sonuç | Tamamlanan / toplam * 100; gösterim bir ondalık; konu yoksa 0 ve henüz konu yok etiketi. Hata varyantında: Oturum süresi konuyu otomatik tamamlamaz. |
| Test seviyesi | Birim + MVC entegrasyon |
| Öncelik | Yüksek |
| Durum | Not run; uygulama kodu yok |

## TC-013 — Görev oluşturma

| Alan | İçerik |
| --- | --- |
| İlgili gereksinim | FR-013 |
| Ön koşul | Faz 3 özelliği uygulanmış; A sahibi etkin Course1; B sahibi Course2; aksi yazılmadıkça clock sabit2026-11-12T12:00Z. |
| Test verisi | CourseId=1; TopicId=1; Title=20 soru; EstimatedMinutes=60; DueOn=2026-11-12; Priority=High |
| Uygulama adımları | 1) Girdideki fixture değerlerini oluştur. 2) İlgili MVC ekran/servis işlemini A olarak çalıştır. 3) HTML sonucunu ve gerekiyorsa DB/sorgu sonucunu kontrol et. 4) İşlem kabul sonucunu aşağıdaki değerlere göre karşılaştır. |
| Beklenen sonuç | Konu opsiyonel ve aynı derste; başlık 1–160; tahmin 1–1440; başlangıç Pending. Hata varyantında: Başka dersin konusu 400; sahip olunmayan ilişki 404. |
| Test seviyesi | SQL Server + MVC entegrasyon |
| Öncelik | Yüksek |
| Durum | Not run; uygulama kodu yok |

## TC-014 — Görev düzenleme ve silme

| Alan | İçerik |
| --- | --- |
| İlgili gereksinim | FR-014 |
| Ön koşul | Faz 3 özelliği uygulanmış; A sahibi etkin Course1; B sahibi Course2; aksi yazılmadıkça clock sabit2026-11-12T12:00Z. |
| Test verisi | id=1; EstimatedMinutes=45 |
| Uygulama adımları | 1) Girdideki fixture değerlerini oluştur. 2) İlgili MVC ekran/servis işlemini A olarak çalıştır. 3) HTML sonucunu ve gerekiyorsa DB/sorgu sonucunu kontrol et. 4) İşlem kabul sonucunu aşağıdaki değerlere göre karşılaştır. |
| Beklenen sonuç | Ders değişimi yalnız plan ve oturum yoksa; konu yeni dersle uyumlu; plan/oturum bağlı görev silinemez. Hata varyantında: Bağlı kayıt 409; RowVersion uyuşmazsa 409. |
| Test seviyesi | SQL Server + MVC entegrasyon |
| Öncelik | Yüksek |
| Durum | Not run; uygulama kodu yok |

## TC-015 — Görev durum geçişi

| Alan | İçerik |
| --- | --- |
| İlgili gereksinim | FR-015 |
| Ön koşul | Faz 3 özelliği uygulanmış; A sahibi etkin Course1; B sahibi Course2; aksi yazılmadıkça clock sabit2026-11-12T12:00Z. |
| Test verisi | Pending -> InProgress -> Completed -> Pending |
| Uygulama adımları | 1) Girdideki fixture değerlerini oluştur. 2) İlgili MVC ekran/servis işlemini A olarak çalıştır. 3) HTML sonucunu ve gerekiyorsa DB/sorgu sonucunu kontrol et. 4) İşlem kabul sonucunu aşağıdaki değerlere göre karşılaştır. |
| Beklenen sonuç | Pending/InProgress/Completed arasında geçiş; CompletedAt yalnız Completed durumunda UTC now; tekrar Completed değişiklik yapmaz. Hata varyantında: Arşivli derste geçiş 409; bilinmeyen durum 400. |
| Test seviyesi | Birim + MVC entegrasyon |
| Öncelik | Yüksek |
| Durum | Not run; uygulama kodu yok |

## TC-016 — Görev filtreleri ve gecikme

| Alan | İçerik |
| --- | --- |
| İlgili gereksinim | FR-016 |
| Ön koşul | Faz 3 özelliği uygulanmış; A sahibi etkin Course1; B sahibi Course2; aksi yazılmadıkça clock sabit2026-11-12T12:00Z. |
| Test verisi | DueOn=dün; Status=Pending |
| Uygulama adımları | 1) Girdideki fixture değerlerini oluştur. 2) İlgili MVC ekran/servis işlemini A olarak çalıştır. 3) HTML sonucunu ve gerekiyorsa DB/sorgu sonucunu kontrol et. 4) İşlem kabul sonucunu aşağıdaki değerlere göre karşılaştır. |
| Beklenen sonuç | Gecikme: DueOn < kullanıcının bugünü ve Status != Completed; null DueOn gecikmiş değildir; ders/tarih/öncelik/durum filtreleri birlikte AND. Hata varyantında: Bitiş filtresi başlangıçtan küçükse 400. |
| Test seviyesi | Birim + MVC entegrasyon |
| Öncelik | Yüksek |
| Durum | Not run; uygulama kodu yok |

## TC-017 — Plan oluşturma

| Alan | İçerik |
| --- | --- |
| İlgili gereksinim | FR-017 |
| Ön koşul | Faz 3 özelliği uygulanmış; A sahibi etkin Course1; B sahibi Course2; aksi yazılmadıkça clock sabit2026-11-12T12:00Z. |
| Test verisi | TaskId=1; PlannedOn=2026-11-12; PlannedMinutes=60 |
| Uygulama adımları | 1) Girdideki fixture değerlerini oluştur. 2) İlgili MVC ekran/servis işlemini A olarak çalıştır. 3) HTML sonucunu ve gerekiyorsa DB/sorgu sonucunu kontrol et. 4) İşlem kabul sonucunu aşağıdaki değerlere göre karşılaştır. |
| Beklenen sonuç | Görev ve ders etkin; plan günü takvim tarihi; süre 1–1440; aynı görev farklı günlerde planlanabilir. Hata varyantında: Aynı görev/gün tekrarı 409; geçersiz süre 400. |
| Test seviyesi | SQL Server + MVC entegrasyon |
| Öncelik | Yüksek |
| Durum | Not run; uygulama kodu yok |

## TC-018 — Plan düzenleme ve silme

| Alan | İçerik |
| --- | --- |
| İlgili gereksinim | FR-018 |
| Ön koşul | Faz 3 özelliği uygulanmış; A sahibi etkin Course1; B sahibi Course2; aksi yazılmadıkça clock sabit2026-11-12T12:00Z. |
| Test verisi | id=1; PlannedMinutes=90 |
| Uygulama adımları | 1) Girdideki fixture değerlerini oluştur. 2) İlgili MVC ekran/servis işlemini A olarak çalıştır. 3) HTML sonucunu ve gerekiyorsa DB/sorgu sonucunu kontrol et. 4) İşlem kabul sonucunu aşağıdaki değerlere göre karşılaştır. |
| Beklenen sonuç | Plan bağımsız planlama kaydı; silmek gerçekleşmiş oturumu veya görevi silmez. Hata varyantında: Arşivli derste değişiklik 409; tarih değişiminde benzersizlik korunur. |
| Test seviyesi | SQL Server + MVC entegrasyon |
| Öncelik | Yüksek |
| Durum | Not run; uygulama kodu yok |

## TC-019 — Günlük ve haftalık plan

| Alan | İçerik |
| --- | --- |
| İlgili gereksinim | FR-019 |
| Ön koşul | Faz 3 özelliği uygulanmış; A sahibi etkin Course1; B sahibi Course2; aksi yazılmadıkça clock sabit2026-11-12T12:00Z. |
| Test verisi | from=2026-11-09; to=2026-11-15 |
| Uygulama adımları | 1) Girdideki fixture değerlerini oluştur. 2) İlgili MVC ekran/servis işlemini A olarak çalıştır. 3) HTML sonucunu ve gerekiyorsa DB/sorgu sonucunu kontrol et. 4) İşlem kabul sonucunu aşağıdaki değerlere göre karşılaştır. |
| Beklenen sonuç | Hafta Pazartesi başlar; tarih aralığı uçları dahil; görev Completed ise plan görünümünde tamamlanmış etiketi. Hata varyantında: Planın bağımsız Completed alanı yoktur; boş günde boş durum. |
| Test seviyesi | Birim + MVC entegrasyon |
| Öncelik | Yüksek |
| Durum | Not run; uygulama kodu yok |

## TC-020 — Plan ve gerçekleşen karşılaştırma

| Alan | İçerik |
| --- | --- |
| İlgili gereksinim | FR-020 |
| Ön koşul | Faz 4 özelliği uygulanmış; A sahibi etkin Course1; B sahibi Course2; aksi yazılmadıkça clock sabit2026-11-12T12:00Z. |
| Test verisi | Görev A pazartesi plan 60; aynı gün oturum 45; salı 30 |
| Uygulama adımları | 1) Girdideki fixture değerlerini oluştur. 2) İlgili MVC ekran/servis işlemini A olarak çalıştır. 3) HTML sonucunu ve gerekiyorsa DB/sorgu sonucunu kontrol et. 4) İşlem kabul sonucunu aşağıdaki değerlere göre karşılaştır. |
| Beklenen sonuç | Günlük görev-plan karşılaştırması yalnız aynı TaskId ve yerel gün oturum dakikalarını sayar; pazartesi 45, salı ayrı 30. Haftalık toplam tüm oturumları içerir. Hata varyantında: Plan kaydı olmayan çalışma plansız olarak gösterilir; süre iki kez sayılmaz. |
| Test seviyesi | Birim + MVC entegrasyon |
| Öncelik | Yüksek |
| Durum | Not run; uygulama kodu yok |

## TC-021 — Oturum başlatma

| Alan | İçerik |
| --- | --- |
| İlgili gereksinim | FR-021 |
| Ön koşul | Faz 4 özelliği uygulanmış; A sahibi etkin Course1; B sahibi Course2; aksi yazılmadıkça clock sabit2026-11-12T12:00Z. |
| Test verisi | CourseId=1; TaskId=1; TopicId=1 |
| Uygulama adımları | 1) Girdideki fixture değerlerini oluştur. 2) İlgili MVC ekran/servis işlemini A olarak çalıştır. 3) HTML sonucunu ve gerekiyorsa DB/sorgu sonucunu kontrol et. 4) İşlem kabul sonucunu aşağıdaki değerlere göre karşılaştır. |
| Beklenen sonuç | Sunucu StartAt=now; EndAt=null; kullanıcı başına tek açık oturum; görev/konu aynı ders ve sahiplik. Hata varyantında: İkinci açık oturum 409; arşivli ders 409. |
| Test seviyesi | SQL Server + MVC entegrasyon |
| Öncelik | Yüksek |
| Durum | Not run; uygulama kodu yok |

## TC-022 — Oturum bitirme

| Alan | İçerik |
| --- | --- |
| İlgili gereksinim | FR-022 |
| Ön koşul | Faz 4 özelliği uygulanmış; A sahibi etkin Course1; B sahibi Course2; aksi yazılmadıkça clock sabit2026-11-12T12:00Z. |
| Test verisi | id=1; EndAt sunucu now |
| Uygulama adımları | 1) Girdideki fixture değerlerini oluştur. 2) İlgili MVC ekran/servis işlemini A olarak çalıştır. 3) HTML sonucunu ve gerekiyorsa DB/sorgu sonucunu kontrol et. 4) İşlem kabul sonucunu aşağıdaki değerlere göre karşılaştır. |
| Beklenen sonuç | EndAt > StartAt ve süre <=24 saat; tamamlanan oturum gerçek süre kaynağıdır; yeniden bitirme ilk EndAt değerini korur. Hata varyantında: 24 saat aşılırsa 409; kullanıcı manuel düzeltmeye yönlendirilir; açık kayıt silinebilir. |
| Test seviyesi | SQL Server + MVC entegrasyon |
| Öncelik | Yüksek |
| Durum | Not run; uygulama kodu yok |

## TC-023 — Manuel oturum kaydı

| Alan | İçerik |
| --- | --- |
| İlgili gereksinim | FR-023 |
| Ön koşul | Faz 4 özelliği uygulanmış; A sahibi etkin Course1; B sahibi Course2; aksi yazılmadıkça clock sabit2026-11-12T12:00Z. |
| Test verisi | StartAt=2026-11-12T09:00:00+03:00; EndAt=2026-11-12T10:00:00+03:00 |
| Uygulama adımları | 1) Girdideki fixture değerlerini oluştur. 2) İlgili MVC ekran/servis işlemini A olarak çalıştır. 3) HTML sonucunu ve gerekiyorsa DB/sorgu sonucunu kontrol et. 4) İşlem kabul sonucunu aşağıdaki değerlere göre karşılaştır. |
| Beklenen sonuç | Başlangıç < bitiş <= now; süre <=24 saat; mevcut oturumlarla [start,end) çakışması yok. Hata varyantında: Gelecek, negatif/sıfır süre, çakışma 400/409. |
| Test seviyesi | SQL Server + MVC entegrasyon |
| Öncelik | Yüksek |
| Durum | Not run; uygulama kodu yok |

## TC-024 — Oturum düzeltme ve silme

| Alan | İçerik |
| --- | --- |
| İlgili gereksinim | FR-024 |
| Ön koşul | Faz 4 özelliği uygulanmış; A sahibi etkin Course1; B sahibi Course2; aksi yazılmadıkça clock sabit2026-11-12T12:00Z. |
| Test verisi | id=1; zamanlar; RowVersion |
| Uygulama adımları | 1) Girdideki fixture değerlerini oluştur. 2) İlgili MVC ekran/servis işlemini A olarak çalıştır. 3) HTML sonucunu ve gerekiyorsa DB/sorgu sonucunu kontrol et. 4) İşlem kabul sonucunu aşağıdaki değerlere göre karşılaştır. |
| Beklenen sonuç | Manuel veya bitmiş oturum geçerli zamanlarla düzeltilebilir; açık oturum sadece bitirilir veya silinir. Hata varyantında: Kendi kaydı dışı 404; çakışma 409; arşivli ders salt okunur. |
| Test seviyesi | SQL Server + MVC entegrasyon |
| Öncelik | Yüksek |
| Durum | Not run; uygulama kodu yok |

## TC-025 — Günlük haftalık süre

| Alan | İçerik |
| --- | --- |
| İlgili gereksinim | FR-025 |
| Ön koşul | Faz 4 özelliği uygulanmış; A sahibi etkin Course1; B sahibi Course2; aksi yazılmadıkça clock sabit2026-11-12T12:00Z. |
| Test verisi | Pazar 23:30 -> Pazartesi 00:30; Europe/Istanbul |
| Uygulama adımları | 1) Girdideki fixture değerlerini oluştur. 2) İlgili MVC ekran/servis işlemini A olarak çalıştır. 3) HTML sonucunu ve gerekiyorsa DB/sorgu sonucunu kontrol et. 4) İşlem kabul sonucunu aşağıdaki değerlere göre karşılaştır. |
| Beklenen sonuç | Bitmiş oturum yerel gün/hafta sınırlarında bölünür; her güne 30 dakika; açık oturum resmi toplama dahil edilmez. Hata varyantında: Toplam saniyeler hesaplanır; yalnız gösterimde toplam dakika bir ondalık yuvarlanır. |
| Test seviyesi | Birim + MVC entegrasyon |
| Öncelik | Yüksek |
| Durum | Not run; uygulama kodu yok |

## TC-026 — Dashboard özetleri

| Alan | İçerik |
| --- | --- |
| İlgili gereksinim | FR-026 |
| Ön koşul | Faz 4 özelliği uygulanmış; A sahibi etkin Course1; B sahibi Course2; aksi yazılmadıkça clock sabit2026-11-12T12:00Z. |
| Test verisi | Aktif dersler, sınavlar, görevler, oturumlar |
| Uygulama adımları | 1) Girdideki fixture değerlerini oluştur. 2) İlgili MVC ekran/servis işlemini A olarak çalıştır. 3) HTML sonucunu ve gerekiyorsa DB/sorgu sonucunu kontrol et. 4) İşlem kabul sonucunu aşağıdaki değerlere göre karşılaştır. |
| Beklenen sonuç | Aktif ders sayısı; 14 günlük sınavlar; geciken/bekleyen görevler; en son 5 CompletedAt; konu oranları; haftalık hedef ve süre; 7 günlük grafik. Hata varyantında: Arşivli dersler operasyonel kartlardan çıkar; tarihsel oturum toplamları dahil. |
| Test seviyesi | Birim + MVC entegrasyon |
| Öncelik | Yüksek |
| Durum | Not run; uygulama kodu yok |

## TC-027 — İstatistikler ve değişim

| Alan | İçerik |
| --- | --- |
| İlgili gereksinim | FR-027 |
| Ön koşul | Faz 4 özelliği uygulanmış; A sahibi etkin Course1; B sahibi Course2; aksi yazılmadıkça clock sabit2026-11-12T12:00Z. |
| Test verisi | from=2026-11-09; to=2026-11-15 |
| Uygulama adımları | 1) Girdideki fixture değerlerini oluştur. 2) İlgili MVC ekran/servis işlemini A olarak çalıştır. 3) HTML sonucunu ve gerekiyorsa DB/sorgu sonucunu kontrol et. 4) İşlem kabul sonucunu aşağıdaki değerlere göre karşılaştır. |
| Beklenen sonuç | Ders dağılımı, konu oranı, görev Completed/toplam, gecikme, plan-gerçek; tamamlanma oranı mevcut aktif görevlerin anlık oranıdır. Haftalık değişim önceki eş uzunluklu aralığa göre. Hata varyantında: Önceki süre 0 ise yüzde null, açıklama önceki dönemde çalışma yok; boş değerler 0/boş dizi. |
| Test seviyesi | Birim + MVC entegrasyon |
| Öncelik | Yüksek |
| Durum | Not run; uygulama kodu yok |

## TC-028 — Çalışma hedefleri

| Alan | İçerik |
| --- | --- |
| İlgili gereksinim | FR-028 |
| Ön koşul | Faz 2 özelliği uygulanmış; A sahibi etkin Course1; B sahibi Course2; aksi yazılmadıkça clock sabit2026-11-12T12:00Z. |
| Test verisi | Aktif hedefler 120,180; arşivli 60 |
| Uygulama adımları | 1) Girdideki fixture değerlerini oluştur. 2) İlgili MVC ekran/servis işlemini A olarak çalıştır. 3) HTML sonucunu ve gerekiyorsa DB/sorgu sonucunu kontrol et. 4) İşlem kabul sonucunu aşağıdaki değerlere göre karşılaştır. |
| Beklenen sonuç | Haftalık kişisel hedef aktif derslerin WeeklyTargetMinutes toplamıdır; ayrıca User hedefi tutulmaz. Hata varyantında: Toplam 300; hedef 0 seçilebilir; tarihsel hedef snapshot MVP dışında. |
| Test seviyesi | Birim + MVC entegrasyon |
| Öncelik | Yüksek |
| Durum | Not run; uygulama kodu yok |

## TC-029 — Kayıt ve giriş

| Alan | İçerik |
| --- | --- |
| İlgili gereksinim | FR-029 |
| Ön koşul | Faz 5 özelliği uygulanmış; A sahibi etkin Course1; B sahibi Course2; aksi yazılmadıkça clock sabit2026-11-12T12:00Z. |
| Test verisi | email ve parola |
| Uygulama adımları | 1) Girdideki fixture değerlerini oluştur. 2) İlgili MVC ekran/servis işlemini A olarak çalıştır. 3) HTML sonucunu ve gerekiyorsa DB/sorgu sonucunu kontrol et. 4) İşlem kabul sonucunu aşağıdaki değerlere göre karşılaştır. |
| Beklenen sonuç | Identity ile e-posta benzersiz; giriş genel hata verir; hesap oluşturma ve çıkış akışları. Hata varyantında: Giriş hatasında hesabın varlığı ifşa edilmez; kilitleme ve parola ilkesi yapılandırılır. |
| Test seviyesi | SQL Server + MVC entegrasyon |
| Öncelik | Yüksek |
| Durum | Not run; uygulama kodu yok |

## TC-030 — Profil ve zaman dilimi

| Alan | İçerik |
| --- | --- |
| İlgili gereksinim | FR-030 |
| Ön koşul | Faz 5 özelliği uygulanmış; A sahibi etkin Course1; B sahibi Course2; aksi yazılmadıkça clock sabit2026-11-12T12:00Z. |
| Test verisi | DisplayName=Berat; TimeZoneId=Europe/Istanbul |
| Uygulama adımları | 1) Girdideki fixture değerlerini oluştur. 2) İlgili MVC ekran/servis işlemini A olarak çalıştır. 3) HTML sonucunu ve gerekiyorsa DB/sorgu sonucunu kontrol et. 4) İşlem kabul sonucunu aşağıdaki değerlere göre karşılaştır. |
| Beklenen sonuç | DisplayName 1–80; desteklenen IANA zaman dilimi; mevcut hesap dışında profil düzenlenemez. Hata varyantında: Bilinmeyen zaman dilimi 400; saat dilimi değişimi geçmiş günlük gruplamayı değiştirir. |
| Test seviyesi | SQL Server + MVC entegrasyon |
| Öncelik | Yüksek |
| Durum | Not run; uygulama kodu yok |

## TC-031 — Veri izolasyonu

| Alan | İçerik |
| --- | --- |
| İlgili gereksinim | FR-031 |
| Ön koşul | Faz 5 özelliği uygulanmış; A sahibi etkin Course1; B sahibi Course2; aksi yazılmadıkça clock sabit2026-11-12T12:00Z. |
| Test verisi | A kullanıcısı B kaydının id değerini gönderir |
| Uygulama adımları | 1) Girdideki fixture değerlerini oluştur. 2) İlgili MVC ekran/servis işlemini A olarak çalıştır. 3) HTML sonucunu ve gerekiyorsa DB/sorgu sonucunu kontrol et. 4) İşlem kabul sonucunu aşağıdaki değerlere göre karşılaştır. |
| Beklenen sonuç | Her okuma/yazma sunucu kullanıcı kapsamıyla; FK ilişkilerinin sahipliği doğrulanır; UserId body kabul edilmez. Hata varyantında: B kaydı 404; girişsiz MVC login yönlendirmesi, API 401. |
| Test seviyesi | SQL Server + MVC entegrasyon |
| Öncelik | Yüksek |
| Durum | Not run; uygulama kodu yok |

## TC-032 — İlk teslim MVC ekranları

| Alan | İçerik |
| --- | --- |
| İlgili gereksinim | FR-032 |
| Ön koşul | Faz 1 özelliği uygulanmış; A sahibi etkin Course1; B sahibi Course2; aksi yazılmadıkça clock sabit2026-11-12T12:00Z. |
| Test verisi | Dashboard/Index, Dashboard/Weekly; Courses/Index, Courses/Details; Exams/Index, Exams/Details |
| Uygulama adımları | 1) Girdideki fixture değerlerini oluştur. 2) İlgili MVC ekran/servis işlemini A olarak çalıştır. 3) HTML sonucunu ve gerekiyorsa DB/sorgu sonucunu kontrol et. 4) İşlem kabul sonucunu aşağıdaki değerlere göre karşılaştır. |
| Beklenen sonuç | 3 Controller, her birinde 2 ayrı View; örnek veri; layout menüsü; Razor foreach ve koşullu boş durum; Bootstrap/CSS. Hata varyantında: Geçersiz örnek id 404 sayfası; henüz veri kaydı yapıldığı iddia edilmez. |
| Test seviyesi | Manuel UI/kabul |
| Öncelik | Yüksek |
| Durum | Not run; uygulama kodu yok |

## TC-033 — Responsive ve erişilebilir ekranlar

| Alan | İçerik |
| --- | --- |
| İlgili gereksinim | FR-033 |
| Ön koşul | Faz 1 özelliği uygulanmış; A sahibi etkin Course1; B sahibi Course2; aksi yazılmadıkça clock sabit2026-11-12T12:00Z. |
| Test verisi | 360,768,1440px; klavye |
| Uygulama adımları | 1) Girdideki fixture değerlerini oluştur. 2) İlgili MVC ekran/servis işlemini A olarak çalıştır. 3) HTML sonucunu ve gerekiyorsa DB/sorgu sonucunu kontrol et. 4) İşlem kabul sonucunu aşağıdaki değerlere göre karşılaştır. |
| Beklenen sonuç | Ana içerikte yatay taşma yok; menü mobilde açılır; etiket ve odağı görünür; hata yalnız renkle aktarılmaz. Hata varyantında: Tablo dar ekranda etiketli kart veya kontrollü tablo kaydırması. |
| Test seviyesi | Manuel UI/kabul |
| Öncelik | Yüksek |
| Durum | Not run; uygulama kodu yok |

## TC-034 — Tekrarlanan gönderim kontrolü

| Alan | İçerik |
| --- | --- |
| İlgili gereksinim | FR-034 |
| Ön koşul | Faz 2 özelliği uygulanmış; A sahibi etkin Course1; B sahibi Course2; aksi yazılmadıkça clock sabit2026-11-12T12:00Z. |
| Test verisi | Aynı OperationId ile aynı POST iki kez |
| Uygulama adımları | 1) Girdideki fixture değerlerini oluştur. 2) İlgili MVC ekran/servis işlemini A olarak çalıştır. 3) HTML sonucunu ve gerekiyorsa DB/sorgu sonucunu kontrol et. 4) İşlem kabul sonucunu aşağıdaki değerlere göre karşılaştır. |
| Beklenen sonuç | MVC Post/Redirect/Get ve kullanıcı+işlem+OperationId benzersiz SubmissionReceipt; aynı hash tekrarında önceki sonuç, farklı payload 409. Hata varyantında: API eklendiğinde Idempotency-Key aynı mekanizma; iş kaydı ve receipt tek transaction. |
| Test seviyesi | SQL Server + MVC entegrasyon |
| Öncelik | Yüksek |
| Durum | Not run; uygulama kodu yok |

## TC-035 — Eşzamanlı düzenleme

| Alan | İçerik |
| --- | --- |
| İlgili gereksinim | FR-035 |
| Ön koşul | Faz 2 özelliği uygulanmış; A sahibi etkin Course1; B sahibi Course2; aksi yazılmadıkça clock sabit2026-11-12T12:00Z. |
| Test verisi | İki sekme aynı RowVersion |
| Uygulama adımları | 1) Girdideki fixture değerlerini oluştur. 2) İlgili MVC ekran/servis işlemini A olarak çalıştır. 3) HTML sonucunu ve gerekiyorsa DB/sorgu sonucunu kontrol et. 4) İşlem kabul sonucunu aşağıdaki değerlere göre karşılaştır. |
| Beklenen sonuç | SQL Server rowversion; MVC hidden base64 token; API güncelleme/silmede If-Match; ilk başarılı, ikinci 409. Hata varyantında: Eksik token API 428; yanlış biçim 400; veri sessiz ezilmez. |
| Test seviyesi | SQL Server + MVC entegrasyon |
| Öncelik | Yüksek |
| Durum | Not run; uygulama kodu yok |

## TC-036 — Kimlik öncesi yerel geliştirme

| Alan | İçerik |
| --- | --- |
| İlgili gereksinim | FR-036 |
| Ön koşul | Faz 2 özelliği uygulanmış; A sahibi etkin Course1; B sahibi Course2; aksi yazılmadıkça clock sabit2026-11-12T12:00Z. |
| Test verisi | DemoOwnerId=development-demo |
| Uygulama adımları | 1) Girdideki fixture değerlerini oluştur. 2) İlgili MVC ekran/servis işlemini A olarak çalıştır. 3) HTML sonucunu ve gerekiyorsa DB/sorgu sonucunu kontrol et. 4) İşlem kabul sonucunu aşağıdaki değerlere göre karşılaştır. |
| Beklenen sonuç | Faz 2–4 sadece yerelde bir sabit demo sahibi; User tablosuna demo sahip eklenir; her sorgu owner filtresiyle tasarlanır. Hata varyantında: Identity öncesi internete yayın yasak; Production başlangıcı demo modunda başarısız olur. |
| Test seviyesi | SQL Server + MVC entegrasyon |
| Öncelik | Yüksek |
| Durum | Not run; uygulama kodu yok |

## TC-037 — Boş ders adı

| Alan | İçerik |
| --- | --- |
| İlgili gereksinim | FR-001 |
| Ön koşul | Faz 2; izole fixture; A/B sahipler; IClock sabit; aksi yazılmadıkça etkin Course1. |
| Test verisi | Name="   "; hedef120 |
| Uygulama adımları | A ile Courses/Create form gönder; DB kayıt sayısını önce/sonra karşılaştır |
| Beklenen sonuç | Alan hatası; trim boş kabul edilmez; yeni Course/receipt yok. |
| Test seviyesi | MVC+SQL |
| Öncelik | Yüksek |
| Durum | Not run; uygulama kodu yok |

## TC-038 — Bozuk sınav tarihi

| Alan | İçerik |
| --- | --- |
| İlgili gereksinim | FR-006 |
| Ön koşul | Faz 2; izole fixture; A/B sahipler; IClock sabit; aksi yazılmadıkça etkin Course1. |
| Test verisi | scheduledAt="2026-99-99"; CourseId1 |
| Uygulama adımları | Exam form/JSON parser üzerinden gönder |
| Beklenen sonuç | MVC alan hatası; API varsa400 errors; yeni Exam yok. |
| Test seviyesi | MVC/API opsiyonel |
| Öncelik | Yüksek |
| Durum | Not run; uygulama kodu yok |

## TC-039 — Bulunmayan ders

| Alan | İçerik |
| --- | --- |
| İlgili gereksinim | FR-006 |
| Ön koşul | Faz 2; izole fixture; A/B sahipler; IClock sabit; aksi yazılmadıkça etkin Course1. |
| Test verisi | CourseId999999; geçerli sınav zamanı |
| Uygulama adımları | Sınav create servisini A bağlamında çağır |
| Beklenen sonuç | 404; kayıt ve receipt yok. |
| Test seviyesi | MVC+SQL |
| Öncelik | Yüksek |
| Durum | Not run; uygulama kodu yok |

## TC-040 — Çapraz kullanıcı okuma/yazma

| Alan | İçerik |
| --- | --- |
| İlgili gereksinim | FR-031 |
| Ön koşul | Faz 5; izole fixture; A/B sahipler; IClock sabit; aksi yazılmadıkça etkin Course1. |
| Test verisi | A session; B CourseId2 |
| Uygulama adımları | GET B detail, POST edit/delete ve list query dene; DB önce/sonra karşılaştır |
| Beklenen sonuç | Detail/yazma404; liste B kaydını ve sayısını içermez; DB değişmez. |
| Test seviyesi | Güvenlik MVC+SQL |
| Öncelik | Yüksek |
| Durum | Not run; uygulama kodu yok |

## TC-041 — Çapraz sahip FK

| Alan | İçerik |
| --- | --- |
| İlgili gereksinim | FR-013, FR-031 |
| Ön koşul | Faz 5; izole fixture; A/B sahipler; IClock sabit; aksi yazılmadıkça etkin Course1. |
| Test verisi | A Course1; B Topic2 |
| Uygulama adımları | Task create TopicId2 gönder; aynı dersmiş gibi courseId manipüle et |
| Beklenen sonuç | 404; cross-owner Task oluşmaz; SQL bileşik FK doğrudan insert’i de engeller. |
| Test seviyesi | Güvenlik+SQL |
| Öncelik | Yüksek |
| Durum | Not run; uygulama kodu yok |

## TC-042 — CSRF token yok

| Alan | İçerik |
| --- | --- |
| İlgili gereksinim | FR-031, FR-034 |
| Ön koşul | Faz 5; izole fixture; A/B sahipler; IClock sabit; aksi yazılmadıkça etkin Course1. |
| Test verisi | Giriş cookie var; POST token yok |
| Uygulama adımları | Course oluşturma/logout formunu token olmadan gönder |
| Beklenen sonuç | 400; veri değişmez; kullanıcı çıkışı gerçekleşmez. |
| Test seviyesi | Gerçek Identity MVC |
| Öncelik | Yüksek |
| Durum | Not run; uygulama kodu yok |

## TC-043 — XSS metni

| Alan | İçerik |
| --- | --- |
| İlgili gereksinim | FR-010, FR-033 |
| Ön koşul | Faz 5; izole fixture; A/B sahipler; IClock sabit; aksi yazılmadıkça etkin Course1. |
| Test verisi | Notes=<script>alert(1)</script> |
| Uygulama adımları | Not kaydet; konu ekranını tarayıcıda aç |
| Beklenen sonuç | Metin encode görünür; script çalışmaz; DB plain text olabilir. |
| Test seviyesi | UI güvenlik |
| Öncelik | Yüksek |
| Durum | Not run; uygulama kodu yok |

## TC-044 — Tamamlamayı tekrarla

| Alan | İçerik |
| --- | --- |
| İlgili gereksinim | FR-015 |
| Ön koşul | Faz 3; izole fixture; A/B sahipler; IClock sabit; aksi yazılmadıkça etkin Course1. |
| Test verisi | Task Pending; aynı Completed update iki defa |
| Uygulama adımları | İlk geçişte CompletedAt oku; clock ileri al; tekrar Completed gönder |
| Beklenen sonuç | Tek durum; CompletedAt ilk değeri korunur; oturum oluşturulmaz. |
| Test seviyesi | Birim+MVC |
| Öncelik | Yüksek |
| Durum | Not run; uygulama kodu yok |

## TC-045 — Negatif ve sıfır çalışma

| Alan | İçerik |
| --- | --- |
| İlgili gereksinim | FR-023 |
| Ön koşul | Faz 4; izole fixture; A/B sahipler; IClock sabit; aksi yazılmadıkça etkin Course1. |
| Test verisi | end=start-60sn; sonra end=start |
| Uygulama adımları | İki manuel form gönder |
| Beklenen sonuç | İki kayıt da reddedilir; duration oluşmaz. |
| Test seviyesi | Birim+MVC |
| Öncelik | Yüksek |
| Durum | Not run; uygulama kodu yok |

## TC-046 — 24 saat sınırı

| Alan | İçerik |
| --- | --- |
| İlgili gereksinim | FR-023 |
| Ön koşul | Faz 4; izole fixture; A/B sahipler; IClock sabit; aksi yazılmadıkça etkin Course1. |
| Test verisi | start=now-24h,end=now; varyant start=now-24h-1sn |
| Uygulama adımları | Manuel kayıtları ayrı temiz fixture üzerinde dene |
| Beklenen sonuç | 86400sn kabul;86401sn ret; ikinci kayıt yok. |
| Test seviyesi | Birim+SQL |
| Öncelik | Yüksek |
| Durum | Not run; uygulama kodu yok |

## TC-047 — Gelecekteki bitiş

| Alan | İçerik |
| --- | --- |
| İlgili gereksinim | FR-023 |
| Ön koşul | Faz 4; izole fixture; A/B sahipler; IClock sabit; aksi yazılmadıkça etkin Course1. |
| Test verisi | start=now-1h; end=now+1sn |
| Uygulama adımları | Manuel oturum ekle |
| Beklenen sonuç | 400/alan hatası; gerçek süre toplamı artmaz. |
| Test seviyesi | Birim+MVC |
| Öncelik | Yüksek |
| Durum | Not run; uygulama kodu yok |

## TC-048 — Sınav zamanı geçişi

| Alan | İçerik |
| --- | --- |
| İlgili gereksinim | FR-007 |
| Ön koşul | Faz 2; izole fixture; A/B sahipler; IClock sabit; aksi yazılmadıkça etkin Course1. |
| Test verisi | ScheduledAt=clock; clock+1sn varyant |
| Uygulama adımları | Upcoming/Past query çalıştır |
| Beklenen sonuç | Tam eşitlik Upcoming; 1sn sonra Past; bugün geçmiş sınav kalan gün0 olabilir. |
| Test seviyesi | Birim |
| Öncelik | Yüksek |
| Durum | Not run; uygulama kodu yok |

## TC-049 — Arşiv ve silme

| Alan | İçerik |
| --- | --- |
| İlgili gereksinim | FR-004, FR-005 |
| Ön koşul | Faz 3; izole fixture; A/B sahipler; IClock sabit; aksi yazılmadıkça etkin Course1. |
| Test verisi | Course1 altında Exam1 ve Topic1 |
| Uygulama adımları | Delete Course1; Archive; alt kayıt create/edit dene; history GET |
| Beklenen sonuç | Delete409; arşiv geçerli; Exam/Topic korunur; yeni yazma409; GET görünür. |
| Test seviyesi | SQL+MVC |
| Öncelik | Yüksek |
| Durum | Not run; uygulama kodu yok |

## TC-050 — Boş istatistik

| Alan | İçerik |
| --- | --- |
| İlgili gereksinim | FR-027 |
| Ön koşul | Faz 4; izole fixture; A/B sahipler; IClock sabit; aksi yazılmadıkça etkin Course1. |
| Test verisi | A hiçbir session/task/topic kaydı yok |
| Uygulama adımları | Bu haftanın statistics ve dashboard sorgusunu çalıştır |
| Beklenen sonuç | Süre/oran0, diziler boş; previousPeriodChangePercent=null; NaN/exception yok. |
| Test seviyesi | Birim+MVC |
| Öncelik | Yüksek |
| Durum | Not run; uygulama kodu yok |

## TC-051 — Aynı POST tekrar

| Alan | İçerik |
| --- | --- |
| İlgili gereksinim | FR-034 |
| Ön koşul | Faz 2; izole fixture; A/B sahipler; IClock sabit; aksi yazılmadıkça etkin Course1. |
| Test verisi | OperationId sabitUUID; Course nameAlgoritmalar |
| Uygulama adımları | Aynı formu iki paralel request ile gönder |
| Beklenen sonuç | Tek Course+tek receipt; aynı resource; farklı body aynı key409. |
| Test seviyesi | SQL yarış+MVC |
| Öncelik | Yüksek |
| Durum | Not run; uygulama kodu yok |

## TC-052 — Eksik body ve overposting

| Alan | İçerik |
| --- | --- |
| İlgili gereksinim | FR-001, FR-031 |
| Ön koşul | Faz 5; izole fixture; A/B sahipler; IClock sabit; aksi yazılmadıkça etkin Course1. |
| Test verisi | {}; {name:"A",userId:"B"}; bilinmeyen alan |
| Uygulama adımları | API varsa create JSON gönder; MVC’de UserId manipüle et |
| Beklenen sonuç | API400; MVC sahibi A olarak kalır, B kayıt oluşturamaz; eksik ad reddedilir. |
| Test seviyesi | MVC/API opsiyonel |
| Öncelik | Yüksek |
| Durum | Not run; uygulama kodu yok |

## TC-053 — Tek açık oturum yarışı

| Alan | İçerik |
| --- | --- |
| İlgili gereksinim | FR-021 |
| Ön koşul | Faz 4; izole fixture; A/B sahipler; IClock sabit; aksi yazılmadıkça etkin Course1. |
| Test verisi | A aynı anda iki start; iki DbContext |
| Uygulama adımları | Bariyerle iki transaction başlat; ikisini commit etmeyi dene |
| Beklenen sonuç | Bir açık session; diğeri409; filtered unique var. |
| Test seviyesi | SQL yarış |
| Öncelik | Yüksek |
| Durum | Not run; uygulama kodu yok |

## TC-054 — Çakışma ve bitişik oturum

| Alan | İçerik |
| --- | --- |
| İlgili gereksinim | FR-023 |
| Ön koşul | Faz 4; izole fixture; A/B sahipler; IClock sabit; aksi yazılmadıkça etkin Course1. |
| Test verisi | 09–10 mevcut; yeni09:30–10:30; yeni10–11 |
| Uygulama adımları | Aynı yerel geçmiş gün için iki ekleme dene |
| Beklenen sonuç | Çakışan409; bitişik kabul; toplam120dk. |
| Test seviyesi | SQL+birim |
| Öncelik | Yüksek |
| Durum | Not run; uygulama kodu yok |

## TC-055 — Hafta sınırında bölme

| Alan | İçerik |
| --- | --- |
| İlgili gereksinim | FR-025 |
| Ön koşul | Faz 4; izole fixture; A/B sahipler; IClock sabit; aksi yazılmadıkça etkin Course1. |
| Test verisi | 2026-11-08 23:30 → 2026-11-09 00:30 +03:00 |
| Uygulama adımları | Önceki hafta ve sonraki haftayı sorgula |
| Beklenen sonuç | Her haftaya30dk; birleşik toplam60; çift sayma yok. |
| Test seviyesi | Birim |
| Öncelik | Yüksek |
| Durum | Not run; uygulama kodu yok |

## TC-056 — Plan eşleştirme ve plansız

| Alan | İçerik |
| --- | --- |
| İlgili gereksinim | FR-020 |
| Ön koşul | Faz 4; izole fixture; A/B sahipler; IClock sabit; aksi yazılmadıkça etkin Course1. |
| Test verisi | Task1 pazartesi plan60; pazartesi45/salı30 session; tasksiz15 pazartesi |
| Uygulama adımları | Günlük karşılaştırma ve iki günlük toplamı sorgula |
| Beklenen sonuç | Pazartesi plan gerçek45, fark-15; toplam90; plansız45; session toplamı task tahmininden etkilenmez. |
| Test seviyesi | Birim+SQL |
| Öncelik | Yüksek |
| Durum | Not run; uygulama kodu yok |

## TC-057 — Rowversion yarışı

| Alan | İçerik |
| --- | --- |
| İlgili gereksinim | FR-035 |
| Ön koşul | Faz 2; izole fixture; A/B sahipler; IClock sabit; aksi yazılmadıkça etkin Course1. |
| Test verisi | İki form Course1 aynı version; adA/adB |
| Uygulama adımları | İlk update kaydet; ikinci eski token ile kaydet |
| Beklenen sonuç | İlk200/redirect; ikinci409; DB adıA kalır; API eksik If-Match428. |
| Test seviyesi | SQL+MVC |
| Öncelik | Yüksek |
| Durum | Not run; uygulama kodu yok |

## TC-058 — Anonim erişim ve profil

| Alan | İçerik |
| --- | --- |
| İlgili gereksinim | FR-029, FR-030, FR-031 |
| Ön koşul | Faz 5; izole fixture; A/B sahipler; IClock sabit; aksi yazılmadıkça etkin Course1. |
| Test verisi | Faz5 logged out; sonra A login |
| Uygulama adımları | Course list aç; API varsaGET; A profilinde B id manipüle et |
| Beklenen sonuç | MVC login redirect; API401; B profil değişmez; logout POST cookie kapanır. |
| Test seviyesi | Gerçek Identity MVC |
| Öncelik | Yüksek |
| Durum | Not run; uygulama kodu yok |

## TC-059 — Demo Production guard

| Alan | İçerik |
| --- | --- |
| İlgili gereksinim | FR-036 |
| Ön koşul | Faz 2; izole fixture; A/B sahipler; IClock sabit; aksi yazılmadıkça etkin Course1. |
| Test verisi | Environment=Production; DemoMode=true |
| Uygulama adımları | Uygulama host start dene |
| Beklenen sonuç | Fail-fast güvenli konfigürasyon hatası; internet yayını başlamaz. |
| Test seviyesi | Host entegrasyon |
| Öncelik | Yüksek |
| Durum | Not run; uygulama kodu yok |

## TC-060 — Mobil ve klavye

| Alan | İçerik |
| --- | --- |
| İlgili gereksinim | FR-032, FR-033 |
| Ön koşul | Faz 1; izole fixture; A/B sahipler; IClock sabit; aksi yazılmadıkça etkin Course1. |
| Test verisi | 360/768/1440px; yalnızTab/Enter |
| Uygulama adımları | Altı View aç; menu/filter/link/odağı dene |
| Beklenen sonuç | 3 Controller/6 farklı View; menü işler; ana içerik taşmaz; form labels/focus var. |
| Test seviyesi | Manuel UI |
| Öncelik | Yüksek |
| Durum | Not run; uygulama kodu yok |

## TC-061 — DST gece bölme

| Alan | İçerik |
| --- | --- |
| İlgili gereksinim | FR-025, FR-030 |
| Ön koşul | Faz 4; izole fixture; A/B sahipler; IClock sabit; aksi yazılmadıkça etkin Course1. |
| Test verisi | Europe/Berlin; 2026-10-25T01:30+02:00→03:30+01:00 |
| Uygulama adımları | UTC farkı ve yerel gün toplamını hesapla |
| Beklenen sonuç | Gerçek180 dakika,120 değil; aynı güne180; kullanıcı tz değişirse gruplama yeniden hesaplanır. |
| Test seviyesi | Birim |
| Öncelik | Yüksek |
| Durum | Not run; uygulama kodu yok |

## TC-062 — Arşiv açık oturum yarışı

| Alan | İçerik |
| --- | --- |
| İlgili gereksinim | FR-004, FR-021 |
| Ön koşul | Faz 4; izole fixture; A/B sahipler; IClock sabit; aksi yazılmadıkça etkin Course1. |
| Test verisi | Course1 açık Timer |
| Uygulama adımları | Archive dene; açık sil; archive dene; eşzamanlı start/archive ayrı context |
| Beklenen sonuç | Açık varken409; silince archive geçerli; yarış sonunda arşivli derste açık session oluşmaz. |
| Test seviyesi | SQL yarış |
| Öncelik | Yüksek |
| Durum | Not run; uygulama kodu yok |

## TC-063 — Konu değişimi geçmiş koruması

| Alan | İçerik |
| --- | --- |
| İlgili gereksinim | FR-014, FR-024 |
| Ön koşul | Faz 4; izole fixture; A/B sahipler; IClock sabit; aksi yazılmadıkça etkin Course1. |
| Test verisi | Task1 Topic1 ve bağlı bitmiş Session |
| Uygulama adımları | Task.TopicId=3 update; Task.CourseId değişimi; Session’ı başka sahibin göreviyle güncelle |
| Beklenen sonuç | Bağlı oturum varsa konu/ders değişimi409; cross-owner404; geçmiş dağılım değişmez. |
| Test seviyesi | SQL+MVC |
| Öncelik | Yüksek |
| Durum | Not run; uygulama kodu yok |

## TC-064 — Sır ve güvenli hata

| Alan | İçerik |
| --- | --- |
| İlgili gereksinim | FR-031, FR-036 |
| Ön koşul | Faz 6; izole fixture; A/B sahipler; IClock sabit; aksi yazılmadıkça etkin Course1. |
| Test verisi | Kasıtlı DB erişim hatası; config env secret |
| Uygulama adımları | 500 sonucu ve logu incele; git tracked içerik taramasını çalıştır |
| Beklenen sonuç | UI/API stack trace/connection string yok; traceId var; loglarda body/note/token/parola yok; repoda gerçek sır yok. |
| Test seviyesi | Manuel+entegrasyon |
| Öncelik | Yüksek |
| Durum | Not run; uygulama kodu yok |

## TC-065 — Performans hedefi

| Alan | İçerik |
| --- | --- |
| İlgili gereksinim | FR-026 |
| Ön koşul | Faz 6; izole fixture; A/B sahipler; IClock sabit; aksi yazılmadıkça etkin Course1. |
| Test verisi | NFR002 veri hacmi;10 eşzamanlı kullanıcı; 5dk ölçüm |
| Uygulama adımları | Warm-up sonra p95 response ölç; makine/commit/SQL plan kaydet |
| Beklenen sonuç | Hedef p95<2s; geçmezse yayın öncesi sorgu/indeks azaltma planı ve tekrar ölçüm. |
| Test seviyesi | Performans |
| Öncelik | Yüksek |
| Durum | Not run; uygulama kodu yok |

## TC-066 — Geçersiz enum ve plan duplicate

| Alan | İçerik |
| --- | --- |
| İlgili gereksinim | FR-010, FR-015, FR-017 |
| Ön koşul | Faz 3; izole fixture; A/B sahipler; IClock sabit; aksi yazılmadıkça etkin Course1. |
| Test verisi | Status=Alien; aynı task/day iki plan |
| Uygulama adımları | Durum form gönder; plan create iki kez farklı OperationId ile |
| Beklenen sonuç | Enum400/ModelState; ilk plan başarılı, ikinci409; unique constraint korunur. |
| Test seviyesi | MVC+SQL |
| Öncelik | Yüksek |
| Durum | Not run; uygulama kodu yok |

[Dokümantasyon indeksi](../README.md)
