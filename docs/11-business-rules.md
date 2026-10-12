# İş kuralları

Durum: tasarım; bu depoda çalışan uygulama veya yürütülmüş uygulama testleri yoktur.

Bu dosya normatif davranış kaynağıdır. Gereksinimdeki kabul sonucu bu kurala bağlıdır.

## BR-001 — Ders oluşturma

**Kural:** Ad trim sonrası 1–120 karakter; hedef 0–10080 tam dakika. Geçerli kayıt listede görünür.

**İhlal:** Boş ad veya sınır dışı hedef kaydı engeller.

**Gerekçe:** Aynı girdinin bütün ekranlarda ve servislerde aynı sonucu üretmesini sağlar.

**Bağlantı:** FR-001; TC-001; CoursesService veya UI için MVC ViewModel.

## BR-002 — Ders listeleme ve filtre

**Kural:** Sadece mevcut kullanıcı kapsamı; ad içinde büyük/küçük harf duyarsız arama; Active/Archived filtresi; ad, sonra Id sırası.

**İhlal:** Geçersiz durum 400; sonuç yoksa boş liste.

**Gerekçe:** Aynı girdinin bütün ekranlarda ve servislerde aynı sonucu üretmesini sağlar.

**Bağlantı:** FR-002; TC-002; CoursesService veya UI için MVC ViewModel.

## BR-003 — Ders detay ve düzenleme

**Kural:** Sahip olunan ders getirilir; geçerli değişiklik kaydedilir; RowVersion eskiyse 409.

**İhlal:** Eksik kayıt 404; hatalı değer 400.

**Gerekçe:** Aynı girdinin bütün ekranlarda ve servislerde aynı sonucu üretmesini sağlar.

**Bağlantı:** FR-003; TC-003; CoursesService veya UI için MVC ViewModel.

## BR-004 — Ders arşivleme ve geri açma

**Kural:** Arşiv geçmişi korur; arşivli derste yeni konu, sınav, görev, plan, oturum engellenir; açık oturum varken arşiv 409.

**İhlal:** Arşivli dersin tamamlanmamış görev ve planları salt okunur; geri açılınca yeniden etkin.

**Gerekçe:** Veri kaybını ve çapraz kullanıcı erişimini engeller.

**Bağlantı:** FR-004; TC-004; CoursesService veya UI için MVC ViewModel.

## BR-005 — Ders silme

**Kural:** Yalnız hiçbir ilişkili Exam/Topic/StudyTask/StudyPlanItem/StudySession bulunmayan ders silinir. İlişkili kayıt varsa arşiv önerilir.

**İhlal:** İlişki varsa 409; veritabanında NO ACTION.

**Gerekçe:** Veri kaybını ve çapraz kullanıcı erişimini engeller.

**Bağlantı:** FR-005; TC-005; CoursesService veya UI için MVC ViewModel.

## BR-006 — Sınav oluşturma

**Kural:** Aktif ve sahip olunan derse sınav eklenir; başlık 1–120; desteklenen tür; geçmiş tarih tarihsel kayıt olarak kabul edilir.

**İhlal:** Bozuk/eksik tarih 400; ders yok veya başkasına aitse 404.

**Gerekçe:** Aynı girdinin bütün ekranlarda ve servislerde aynı sonucu üretmesini sağlar.

**Bağlantı:** FR-006; TC-006; ExamsService veya UI için MVC ViewModel.

## BR-007 — Sınav liste ve detay

**Kural:** Upcoming: now <= tarih < now+14 gün; Past: tarih < now; kalan gün yerel takvim tarih farkı.

**İhlal:** Boş liste geçerlidir; bilinmeyen scope 400.

**Gerekçe:** Aynı girdinin bütün ekranlarda ve servislerde aynı sonucu üretmesini sağlar.

**Bağlantı:** FR-007; TC-007; ExamsService veya UI için MVC ViewModel.

## BR-008 — Sınav düzenleme ve silme

**Kural:** Aktif derste geçerli değişiklik; sahip olunan kayıt silinebilir.

**İhlal:** Arşivli derste düzenleme/silme 409; eski RowVersion 409.

**Gerekçe:** Aynı girdinin bütün ekranlarda ve servislerde aynı sonucu üretmesini sağlar.

**Bağlantı:** FR-008; TC-008; ExamsService veya UI için MVC ViewModel.

## BR-009 — Konu ekleme ve liste

**Kural:** Başlık 1–160; sıra >=0; ders içinde SortOrder, Id sıralaması.

**İhlal:** Arşivli ders 409; geçersiz sıra 400.

**Gerekçe:** Aynı girdinin bütün ekranlarda ve servislerde aynı sonucu üretmesini sağlar.

**Bağlantı:** FR-009; TC-009; TopicsService veya UI için MVC ViewModel.

## BR-010 — Konu durumu ve notu

**Kural:** Not en fazla 2000; NotStarted/InProgress/Completed; tekrar tarihi opsiyonel; geçmiş tekrar tarihi kabul edilir.

**İhlal:** Bilinmeyen durum veya geçersiz takvim tarihi 400.

**Gerekçe:** Aynı girdinin bütün ekranlarda ve servislerde aynı sonucu üretmesini sağlar.

**Bağlantı:** FR-010; TC-010; TopicsService veya UI için MVC ViewModel.

## BR-011 — Konu düzenleme, sıra ve silme

**Kural:** Aktif ders koşulu; görev veya oturum konusu olarak kullanılan konu silinemez; plan ilişkisi görev üzerinden dolaylıdır.

**İhlal:** İlişkili kayıt 409; diğerleri geçerli değişiklik/silme.

**Gerekçe:** Veri kaybını ve çapraz kullanıcı erişimini engeller.

**Bağlantı:** FR-011; TC-011; TopicsService veya UI için MVC ViewModel.

## BR-012 — Konu ilerleme

**Kural:** Tamamlanan / toplam * 100; gösterim bir ondalık; konu yoksa 0 ve henüz konu yok etiketi.

**İhlal:** Oturum süresi konuyu otomatik tamamlamaz.

**Gerekçe:** Aynı girdinin bütün ekranlarda ve servislerde aynı sonucu üretmesini sağlar.

**Bağlantı:** FR-012; TC-012; DashboardService veya UI için MVC ViewModel.

## BR-013 — Görev oluşturma

**Kural:** Konu opsiyonel ve aynı derste; başlık 1–160; tahmin 1–1440; başlangıç Pending.

**İhlal:** Başka dersin konusu 400; sahip olunmayan ilişki 404.

**Gerekçe:** Aynı girdinin bütün ekranlarda ve servislerde aynı sonucu üretmesini sağlar.

**Bağlantı:** FR-013; TC-013; StudyTasksService veya UI için MVC ViewModel.

## BR-014 — Görev düzenleme ve silme

**Kural:** Ders değişimi yalnız plan ve oturum yoksa; konu yeni dersle uyumlu; bağlı oturum varsa TopicId değişimi engellenir; plan/oturum bağlı görev silinemez.

**İhlal:** Bağlı kayıt 409; RowVersion uyuşmazsa 409.

**Gerekçe:** Veri kaybını ve çapraz kullanıcı erişimini engeller.

**Bağlantı:** FR-014; TC-014; StudyTasksService veya UI için MVC ViewModel.

## BR-015 — Görev durum geçişi

**Kural:** Pending/InProgress/Completed arasında geçiş; CompletedAt yalnız Completed durumunda UTC now; tekrar Completed değişiklik yapmaz.

**İhlal:** Arşivli derste geçiş 409; bilinmeyen durum 400.

**Gerekçe:** Aynı girdinin bütün ekranlarda ve servislerde aynı sonucu üretmesini sağlar.

**Bağlantı:** FR-015; TC-015; StudyTasksService veya UI için MVC ViewModel.

## BR-016 — Görev filtreleri ve gecikme

**Kural:** Gecikme: DueOn < kullanıcının bugünü ve Status != Completed; null DueOn gecikmiş değildir; ders/tarih/öncelik/durum filtreleri birlikte AND.

**İhlal:** Bitiş filtresi başlangıçtan küçükse 400.

**Gerekçe:** Aynı girdinin bütün ekranlarda ve servislerde aynı sonucu üretmesini sağlar.

**Bağlantı:** FR-016; TC-016; StudyTasksService veya UI için MVC ViewModel.

## BR-017 — Plan oluşturma

**Kural:** Görev ve ders etkin; plan günü takvim tarihi; süre 1–1440; aynı görev farklı günlerde planlanabilir.

**İhlal:** Aynı görev/gün tekrarı 409; geçersiz süre 400.

**Gerekçe:** Aynı girdinin bütün ekranlarda ve servislerde aynı sonucu üretmesini sağlar.

**Bağlantı:** FR-017; TC-017; StudyPlansService veya UI için MVC ViewModel.

## BR-018 — Plan düzenleme ve silme

**Kural:** Plan bağımsız planlama kaydı; silmek gerçekleşmiş oturumu veya görevi silmez.

**İhlal:** Arşivli derste değişiklik 409; tarih değişiminde benzersizlik korunur.

**Gerekçe:** Aynı girdinin bütün ekranlarda ve servislerde aynı sonucu üretmesini sağlar.

**Bağlantı:** FR-018; TC-018; StudyPlansService veya UI için MVC ViewModel.

## BR-019 — Günlük ve haftalık plan

**Kural:** Hafta Pazartesi başlar; tarih aralığı uçları dahil; görev Completed ise plan görünümünde tamamlanmış etiketi.

**İhlal:** Planın bağımsız Completed alanı yoktur; boş günde boş durum.

**Gerekçe:** Aynı girdinin bütün ekranlarda ve servislerde aynı sonucu üretmesini sağlar.

**Bağlantı:** FR-019; TC-019; StudyPlansService veya UI için MVC ViewModel.

## BR-020 — Plan ve gerçekleşen karşılaştırma

**Kural:** Günlük görev-plan karşılaştırması yalnız aynı TaskId ve yerel gün oturum dakikalarını sayar; pazartesi 45, salı ayrı 30. Haftalık toplam tüm oturumları içerir.

**İhlal:** Plan kaydı olmayan çalışma plansız olarak gösterilir; süre iki kez sayılmaz.

**Gerekçe:** Aynı girdinin bütün ekranlarda ve servislerde aynı sonucu üretmesini sağlar.

**Bağlantı:** FR-020; TC-020; StatisticsService veya UI için MVC ViewModel.

## BR-021 — Oturum başlatma

**Kural:** Sunucu StartAt=now; EndAt=null; kullanıcı başına tek açık oturum; görev/konu aynı ders ve sahiplik.

**İhlal:** İkinci açık oturum 409; arşivli ders 409.

**Gerekçe:** Aynı girdinin bütün ekranlarda ve servislerde aynı sonucu üretmesini sağlar.

**Bağlantı:** FR-021; TC-021; StudySessionsService veya UI için MVC ViewModel.

## BR-022 — Oturum bitirme

**Kural:** EndAt > StartAt ve süre <=24 saat; tamamlanan oturum gerçek süre kaynağıdır; yeniden bitirme ilk EndAt değerini korur.

**İhlal:** 24 saat aşılırsa 409; kullanıcı manuel düzeltmeye yönlendirilir; açık kayıt silinebilir.

**Gerekçe:** Aynı girdinin bütün ekranlarda ve servislerde aynı sonucu üretmesini sağlar.

**Bağlantı:** FR-022; TC-022; StudySessionsService veya UI için MVC ViewModel.

## BR-023 — Manuel oturum kaydı

**Kural:** Başlangıç < bitiş <= now; süre <=24 saat; mevcut oturumlarla [start,end) çakışması yok.

**İhlal:** Gelecek, negatif/sıfır süre, çakışma 400/409.

**Gerekçe:** Aynı girdinin bütün ekranlarda ve servislerde aynı sonucu üretmesini sağlar.

**Bağlantı:** FR-023; TC-023; StudySessionsService veya UI için MVC ViewModel.

## BR-024 — Oturum düzeltme ve silme

**Kural:** Manuel veya bitmiş oturum geçerli zamanlarla düzeltilebilir; açık oturum sadece bitirilir veya silinir.

**İhlal:** Kendi kaydı dışı 404; çakışma 409; arşivli ders salt okunur.

**Gerekçe:** Veri kaybını ve çapraz kullanıcı erişimini engeller.

**Bağlantı:** FR-024; TC-024; StudySessionsService veya UI için MVC ViewModel.

## BR-025 — Günlük haftalık süre

**Kural:** Bitmiş oturum yerel gün/hafta sınırlarında bölünür; her güne 30 dakika; açık oturum resmi toplama dahil edilmez.

**İhlal:** Toplam saniyeler hesaplanır; yalnız gösterimde toplam dakika bir ondalık yuvarlanır.

**Gerekçe:** Aynı girdinin bütün ekranlarda ve servislerde aynı sonucu üretmesini sağlar.

**Bağlantı:** FR-025; TC-025; StatisticsService veya UI için MVC ViewModel.

## BR-026 — Dashboard özetleri

**Kural:** Aktif ders sayısı; 14 günlük sınavlar; geciken/bekleyen görevler; en son 5 CompletedAt; konu oranları; haftalık hedef ve süre; 7 günlük grafik.

**İhlal:** Arşivli dersler operasyonel kartlardan çıkar; tarihsel oturum toplamları dahil.

**Gerekçe:** Aynı girdinin bütün ekranlarda ve servislerde aynı sonucu üretmesini sağlar.

**Bağlantı:** FR-026; TC-026; DashboardService veya UI için MVC ViewModel.

## BR-027 — İstatistikler ve değişim

**Kural:** Ders dağılımı, konu oranı, görev Completed/toplam, gecikme, plan-gerçek; tamamlanma oranı mevcut aktif görevlerin anlık oranıdır. Haftalık değişim önceki eş uzunluklu aralığa göre.

**İhlal:** Önceki süre 0 ise yüzde null, açıklama önceki dönemde çalışma yok; boş değerler 0/boş dizi.

**Gerekçe:** Aynı girdinin bütün ekranlarda ve servislerde aynı sonucu üretmesini sağlar.

**Bağlantı:** FR-027; TC-027; StatisticsService veya UI için MVC ViewModel.

## BR-028 — Çalışma hedefleri

**Kural:** Haftalık kişisel hedef aktif derslerin WeeklyTargetMinutes toplamıdır; ayrıca User hedefi tutulmaz.

**İhlal:** Toplam 300; hedef 0 seçilebilir; tarihsel hedef snapshot MVP dışında.

**Gerekçe:** Aynı girdinin bütün ekranlarda ve servislerde aynı sonucu üretmesini sağlar.

**Bağlantı:** FR-028; TC-028; CoursesService veya UI için MVC ViewModel.

## BR-029 — Kayıt ve giriş

**Kural:** Identity ile e-posta benzersiz; giriş genel hata verir; hesap oluşturma ve çıkış akışları.

**İhlal:** Giriş hatasında hesabın varlığı ifşa edilmez; kilitleme ve parola ilkesi yapılandırılır.

**Gerekçe:** Aynı girdinin bütün ekranlarda ve servislerde aynı sonucu üretmesini sağlar.

**Bağlantı:** FR-029; TC-029; AccountService veya UI için MVC ViewModel.

## BR-030 — Profil ve zaman dilimi

**Kural:** DisplayName 1–80; desteklenen IANA zaman dilimi; mevcut hesap dışında profil düzenlenemez.

**İhlal:** Bilinmeyen zaman dilimi 400; saat dilimi değişimi geçmiş günlük gruplamayı değiştirir.

**Gerekçe:** Aynı girdinin bütün ekranlarda ve servislerde aynı sonucu üretmesini sağlar.

**Bağlantı:** FR-030; TC-030; AccountService veya UI için MVC ViewModel.

## BR-031 — Veri izolasyonu

**Kural:** Her okuma/yazma sunucu kullanıcı kapsamıyla; FK ilişkilerinin sahipliği doğrulanır; UserId body kabul edilmez.

**İhlal:** B kaydı 404; girişsiz MVC login yönlendirmesi, API 401.

**Gerekçe:** Veri kaybını ve çapraz kullanıcı erişimini engeller.

**Bağlantı:** FR-031; TC-031; SecurityService veya UI için MVC ViewModel.

## BR-032 — İlk teslim MVC ekranları

**Kural:** 3 Controller, her birinde 2 ayrı View; örnek veri; layout menüsü; Razor foreach ve koşullu boş durum; Bootstrap/CSS.

**İhlal:** Geçersiz örnek id 404 sayfası; henüz veri kaydı yapıldığı iddia edilmez.

**Gerekçe:** Aynı girdinin bütün ekranlarda ve servislerde aynı sonucu üretmesini sağlar.

**Bağlantı:** FR-032; TC-032; UIService veya UI için MVC ViewModel.

## BR-033 — Responsive ve erişilebilir ekranlar

**Kural:** Ana içerikte yatay taşma yok; menü mobilde açılır; etiket ve odağı görünür; hata yalnız renkle aktarılmaz.

**İhlal:** Tablo dar ekranda etiketli kart veya kontrollü tablo kaydırması.

**Gerekçe:** Aynı girdinin bütün ekranlarda ve servislerde aynı sonucu üretmesini sağlar.

**Bağlantı:** FR-033; TC-033; UIService veya UI için MVC ViewModel.

## BR-034 — Tekrarlanan gönderim kontrolü

**Kural:** MVC Post/Redirect/Get ve kullanıcı+işlem+OperationId benzersiz SubmissionReceipt; aynı hash tekrarında önceki sonuç, farklı payload 409.

**İhlal:** API eklendiğinde Idempotency-Key aynı mekanizma; iş kaydı ve receipt tek transaction.

**Gerekçe:** Veri kaybını ve çapraz kullanıcı erişimini engeller.

**Bağlantı:** FR-034; TC-034; PlatformService veya UI için MVC ViewModel.

## BR-035 — Eşzamanlı düzenleme

**Kural:** SQL Server rowversion; MVC hidden base64 token; API güncelleme/silmede If-Match; ilk başarılı, ikinci 409.

**İhlal:** Eksik token API 428; yanlış biçim 400; veri sessiz ezilmez.

**Gerekçe:** Veri kaybını ve çapraz kullanıcı erişimini engeller.

**Bağlantı:** FR-035; TC-035; PlatformService veya UI için MVC ViewModel.

## BR-036 — Kimlik öncesi yerel geliştirme

**Kural:** Faz 2–4 sadece yerelde bir sabit demo sahibi; User tablosuna demo sahip eklenir; her sorgu owner filtresiyle tasarlanır.

**İhlal:** Identity öncesi internete yayın yasak; Production başlangıcı demo modunda başarısız olur.

**Gerekçe:** Veri kaybını ve çapraz kullanıcı erişimini engeller.

**Bağlantı:** FR-036; TC-036; SecurityService veya UI için MVC ViewModel.

[Dokümantasyon indeksi](../README.md)
