# Fonksiyonel gereksinimler

Durum: tasarım; bu depoda çalışan uygulama veya yürütülmüş uygulama testleri yoktur.

Tüm kayıtlar planlanan gereksinimlerdir. Faz 1 eğitim teslimi, Faz 2–4 yerel geliştirme, Faz 5 güvenli çok kullanıcılı ürün, Faz 6 yayın adayıdır. Must/Should öncelikleri ürün sürümü içindir; dersin ilk teslimi ayrı kapıdır.

## FR-001 — Ders oluşturma

| Alan | Tanım |
| --- | --- |
| Modül / faz / öncelik | Courses / Faz 2 / Must |
| Açıklama | Ad trim sonrası 1–120 karakter; hedef 0–10080 tam dakika. Geçerli kayıt listede görünür. |
| Ön koşul | Faz 1: örnek veri; Faz 2–4: yerel demo sahibi; Faz 5+: giriş yapan öğrenci. İlişkili varlık varsa sahibi ve etkinlik koşulu sağlanır. |
| Girdiler | Name=Algoritmalar; WeeklyTargetMinutes=120 |
| Beklenen davranış ve kabul kriterleri | Ad trim sonrası 1–120 karakter; hedef 0–10080 tam dakika. Geçerli kayıt listede görünür. |
| Hata durumları ve olumsuz kabul | Boş ad veya sınır dışı hedef kaydı engeller. |
| İş kuralı / test | BR-001 / TC-001 |

## FR-002 — Ders listeleme ve filtre

| Alan | Tanım |
| --- | --- |
| Modül / faz / öncelik | Courses / Faz 2 / Must |
| Açıklama | Sadece mevcut kullanıcı kapsamı; ad içinde büyük/küçük harf duyarsız arama; Active/Archived filtresi; ad, sonra Id sırası. |
| Ön koşul | Faz 1: örnek veri; Faz 2–4: yerel demo sahibi; Faz 5+: giriş yapan öğrenci. İlişkili varlık varsa sahibi ve etkinlik koşulu sağlanır. |
| Girdiler | search=algo; status=Active |
| Beklenen davranış ve kabul kriterleri | Sadece mevcut kullanıcı kapsamı; ad içinde büyük/küçük harf duyarsız arama; Active/Archived filtresi; ad, sonra Id sırası. |
| Hata durumları ve olumsuz kabul | Geçersiz durum 400; sonuç yoksa boş liste. |
| İş kuralı / test | BR-002 / TC-002 |

## FR-003 — Ders detay ve düzenleme

| Alan | Tanım |
| --- | --- |
| Modül / faz / öncelik | Courses / Faz 2 / Must |
| Açıklama | Sahip olunan ders getirilir; geçerli değişiklik kaydedilir; RowVersion eskiyse 409. |
| Ön koşul | Faz 1: örnek veri; Faz 2–4: yerel demo sahibi; Faz 5+: giriş yapan öğrenci. İlişkili varlık varsa sahibi ve etkinlik koşulu sağlanır. |
| Girdiler | id=1; Name=Veri Yapıları; WeeklyTargetMinutes=180 |
| Beklenen davranış ve kabul kriterleri | Sahip olunan ders getirilir; geçerli değişiklik kaydedilir; RowVersion eskiyse 409. |
| Hata durumları ve olumsuz kabul | Eksik kayıt 404; hatalı değer 400. |
| İş kuralı / test | BR-003 / TC-003 |

## FR-004 — Ders arşivleme ve geri açma

| Alan | Tanım |
| --- | --- |
| Modül / faz / öncelik | Courses / Faz 2 / Must |
| Açıklama | Arşiv geçmişi korur; arşivli derste yeni konu, sınav, görev, plan, oturum engellenir; açık oturum varken arşiv 409. |
| Ön koşul | Faz 1: örnek veri; Faz 2–4: yerel demo sahibi; Faz 5+: giriş yapan öğrenci. İlişkili varlık varsa sahibi ve etkinlik koşulu sağlanır. |
| Girdiler | id=1; IsArchived=true/false |
| Beklenen davranış ve kabul kriterleri | Arşiv geçmişi korur; arşivli derste yeni konu, sınav, görev, plan, oturum engellenir; açık oturum varken arşiv 409. |
| Hata durumları ve olumsuz kabul | Arşivli dersin tamamlanmamış görev ve planları salt okunur; geri açılınca yeniden etkin. |
| İş kuralı / test | BR-004 / TC-004 |

## FR-005 — Ders silme

| Alan | Tanım |
| --- | --- |
| Modül / faz / öncelik | Courses / Faz 2 / Must |
| Açıklama | Yalnız hiçbir ilişkili Exam/Topic/StudyTask/StudyPlanItem/StudySession bulunmayan ders silinir. İlişkili kayıt varsa arşiv önerilir. |
| Ön koşul | Faz 1: örnek veri; Faz 2–4: yerel demo sahibi; Faz 5+: giriş yapan öğrenci. İlişkili varlık varsa sahibi ve etkinlik koşulu sağlanır. |
| Girdiler | id=1 |
| Beklenen davranış ve kabul kriterleri | Yalnız hiçbir ilişkili Exam/Topic/StudyTask/StudyPlanItem/StudySession bulunmayan ders silinir. İlişkili kayıt varsa arşiv önerilir. |
| Hata durumları ve olumsuz kabul | İlişki varsa 409; veritabanında NO ACTION. |
| İş kuralı / test | BR-005 / TC-005 |

## FR-006 — Sınav oluşturma

| Alan | Tanım |
| --- | --- |
| Modül / faz / öncelik | Exams / Faz 2 / Must |
| Açıklama | Aktif ve sahip olunan derse sınav eklenir; başlık 1–120; desteklenen tür; geçmiş tarih tarihsel kayıt olarak kabul edilir. |
| Ön koşul | Faz 1: örnek veri; Faz 2–4: yerel demo sahibi; Faz 5+: giriş yapan öğrenci. İlişkili varlık varsa sahibi ve etkinlik koşulu sağlanır. |
| Girdiler | CourseId=1; Title=Vize; Type=Midterm; ScheduledAt=2026-11-10T10:00:00+03:00 |
| Beklenen davranış ve kabul kriterleri | Aktif ve sahip olunan derse sınav eklenir; başlık 1–120; desteklenen tür; geçmiş tarih tarihsel kayıt olarak kabul edilir. |
| Hata durumları ve olumsuz kabul | Bozuk/eksik tarih 400; ders yok veya başkasına aitse 404. |
| İş kuralı / test | BR-006 / TC-006 |

## FR-007 — Sınav liste ve detay

| Alan | Tanım |
| --- | --- |
| Modül / faz / öncelik | Exams / Faz 2 / Must |
| Açıklama | Upcoming: now <= tarih < now+14 gün; Past: tarih < now; kalan gün yerel takvim tarih farkı. |
| Ön koşul | Faz 1: örnek veri; Faz 2–4: yerel demo sahibi; Faz 5+: giriş yapan öğrenci. İlişkili varlık varsa sahibi ve etkinlik koşulu sağlanır. |
| Girdiler | scope=Upcoming/Past/All; id=1 |
| Beklenen davranış ve kabul kriterleri | Upcoming: now <= tarih < now+14 gün; Past: tarih < now; kalan gün yerel takvim tarih farkı. |
| Hata durumları ve olumsuz kabul | Boş liste geçerlidir; bilinmeyen scope 400. |
| İş kuralı / test | BR-007 / TC-007 |

## FR-008 — Sınav düzenleme ve silme

| Alan | Tanım |
| --- | --- |
| Modül / faz / öncelik | Exams / Faz 2 / Must |
| Açıklama | Aktif derste geçerli değişiklik; sahip olunan kayıt silinebilir. |
| Ön koşul | Faz 1: örnek veri; Faz 2–4: yerel demo sahibi; Faz 5+: giriş yapan öğrenci. İlişkili varlık varsa sahibi ve etkinlik koşulu sağlanır. |
| Girdiler | id=1; tarih ve tür değişikliği |
| Beklenen davranış ve kabul kriterleri | Aktif derste geçerli değişiklik; sahip olunan kayıt silinebilir. |
| Hata durumları ve olumsuz kabul | Arşivli derste düzenleme/silme 409; eski RowVersion 409. |
| İş kuralı / test | BR-008 / TC-008 |

## FR-009 — Konu ekleme ve liste

| Alan | Tanım |
| --- | --- |
| Modül / faz / öncelik | Topics / Faz 3 / Must |
| Açıklama | Başlık 1–160; sıra >=0; ders içinde SortOrder, Id sıralaması. |
| Ön koşul | Faz 1: örnek veri; Faz 2–4: yerel demo sahibi; Faz 5+: giriş yapan öğrenci. İlişkili varlık varsa sahibi ve etkinlik koşulu sağlanır. |
| Girdiler | CourseId=1; Title=Diziler; SortOrder=10 |
| Beklenen davranış ve kabul kriterleri | Başlık 1–160; sıra >=0; ders içinde SortOrder, Id sıralaması. |
| Hata durumları ve olumsuz kabul | Arşivli ders 409; geçersiz sıra 400. |
| İş kuralı / test | BR-009 / TC-009 |

## FR-010 — Konu durumu ve notu

| Alan | Tanım |
| --- | --- |
| Modül / faz / öncelik | Topics / Faz 3 / Should |
| Açıklama | Not en fazla 2000; NotStarted/InProgress/Completed; tekrar tarihi opsiyonel; geçmiş tekrar tarihi kabul edilir. |
| Ön koşul | Faz 1: örnek veri; Faz 2–4: yerel demo sahibi; Faz 5+: giriş yapan öğrenci. İlişkili varlık varsa sahibi ve etkinlik koşulu sağlanır. |
| Girdiler | Status=Completed; Notes=Örnekleri çöz; ReviewOn=2026-11-12 |
| Beklenen davranış ve kabul kriterleri | Not en fazla 2000; NotStarted/InProgress/Completed; tekrar tarihi opsiyonel; geçmiş tekrar tarihi kabul edilir. |
| Hata durumları ve olumsuz kabul | Bilinmeyen durum veya geçersiz takvim tarihi 400. |
| İş kuralı / test | BR-010 / TC-010 |

## FR-011 — Konu düzenleme, sıra ve silme

| Alan | Tanım |
| --- | --- |
| Modül / faz / öncelik | Topics / Faz 3 / Must |
| Açıklama | Aktif ders koşulu; görev veya oturum konusu olarak kullanılan konu silinemez; plan ilişkisi görev üzerinden dolaylıdır. |
| Ön koşul | Faz 1: örnek veri; Faz 2–4: yerel demo sahibi; Faz 5+: giriş yapan öğrenci. İlişkili varlık varsa sahibi ve etkinlik koşulu sağlanır. |
| Girdiler | id=1; SortOrder=20 |
| Beklenen davranış ve kabul kriterleri | Aktif ders koşulu; görev veya oturum konusu olarak kullanılan konu silinemez; plan ilişkisi görev üzerinden dolaylıdır. |
| Hata durumları ve olumsuz kabul | İlişkili kayıt 409; diğerleri geçerli değişiklik/silme. |
| İş kuralı / test | BR-011 / TC-011 |

## FR-012 — Konu ilerleme

| Alan | Tanım |
| --- | --- |
| Modül / faz / öncelik | Dashboard / Faz 3 / Must |
| Açıklama | Tamamlanan / toplam * 100; gösterim bir ondalık; konu yoksa 0 ve henüz konu yok etiketi. |
| Ön koşul | Faz 1: örnek veri; Faz 2–4: yerel demo sahibi; Faz 5+: giriş yapan öğrenci. İlişkili varlık varsa sahibi ve etkinlik koşulu sağlanır. |
| Girdiler | 1 derste 4 konu; 1 Completed |
| Beklenen davranış ve kabul kriterleri | Tamamlanan / toplam * 100; gösterim bir ondalık; konu yoksa 0 ve henüz konu yok etiketi. |
| Hata durumları ve olumsuz kabul | Oturum süresi konuyu otomatik tamamlamaz. |
| İş kuralı / test | BR-012 / TC-012 |

## FR-013 — Görev oluşturma

| Alan | Tanım |
| --- | --- |
| Modül / faz / öncelik | StudyTasks / Faz 3 / Must |
| Açıklama | Konu opsiyonel ve aynı derste; başlık 1–160; tahmin 1–1440; başlangıç Pending. |
| Ön koşul | Faz 1: örnek veri; Faz 2–4: yerel demo sahibi; Faz 5+: giriş yapan öğrenci. İlişkili varlık varsa sahibi ve etkinlik koşulu sağlanır. |
| Girdiler | CourseId=1; TopicId=1; Title=20 soru; EstimatedMinutes=60; DueOn=2026-11-12; Priority=High |
| Beklenen davranış ve kabul kriterleri | Konu opsiyonel ve aynı derste; başlık 1–160; tahmin 1–1440; başlangıç Pending. |
| Hata durumları ve olumsuz kabul | Başka dersin konusu 400; sahip olunmayan ilişki 404. |
| İş kuralı / test | BR-013 / TC-013 |

## FR-014 — Görev düzenleme ve silme

| Alan | Tanım |
| --- | --- |
| Modül / faz / öncelik | StudyTasks / Faz 3 / Must |
| Açıklama | Ders değişimi yalnız plan ve oturum yoksa; konu yeni dersle uyumlu; bağlı oturum varsa TopicId değişimi engellenir; plan/oturum bağlı görev silinemez. |
| Ön koşul | Faz 1: örnek veri; Faz 2–4: yerel demo sahibi; Faz 5+: giriş yapan öğrenci. İlişkili varlık varsa sahibi ve etkinlik koşulu sağlanır. |
| Girdiler | id=1; EstimatedMinutes=45 |
| Beklenen davranış ve kabul kriterleri | Ders değişimi yalnız plan ve oturum yoksa; konu yeni dersle uyumlu; bağlı oturum varsa TopicId değişimi engellenir; plan/oturum bağlı görev silinemez. |
| Hata durumları ve olumsuz kabul | Bağlı kayıt 409; RowVersion uyuşmazsa 409. |
| İş kuralı / test | BR-014 / TC-014 |

## FR-015 — Görev durum geçişi

| Alan | Tanım |
| --- | --- |
| Modül / faz / öncelik | StudyTasks / Faz 3 / Must |
| Açıklama | Pending/InProgress/Completed arasında geçiş; CompletedAt yalnız Completed durumunda UTC now; tekrar Completed değişiklik yapmaz. |
| Ön koşul | Faz 1: örnek veri; Faz 2–4: yerel demo sahibi; Faz 5+: giriş yapan öğrenci. İlişkili varlık varsa sahibi ve etkinlik koşulu sağlanır. |
| Girdiler | Pending -> InProgress -> Completed -> Pending |
| Beklenen davranış ve kabul kriterleri | Pending/InProgress/Completed arasında geçiş; CompletedAt yalnız Completed durumunda UTC now; tekrar Completed değişiklik yapmaz. |
| Hata durumları ve olumsuz kabul | Arşivli derste geçiş 409; bilinmeyen durum 400. |
| İş kuralı / test | BR-015 / TC-015 |

## FR-016 — Görev filtreleri ve gecikme

| Alan | Tanım |
| --- | --- |
| Modül / faz / öncelik | StudyTasks / Faz 3 / Must |
| Açıklama | Gecikme: DueOn < kullanıcının bugünü ve Status != Completed; null DueOn gecikmiş değildir; ders/tarih/öncelik/durum filtreleri birlikte AND. |
| Ön koşul | Faz 1: örnek veri; Faz 2–4: yerel demo sahibi; Faz 5+: giriş yapan öğrenci. İlişkili varlık varsa sahibi ve etkinlik koşulu sağlanır. |
| Girdiler | DueOn=dün; Status=Pending |
| Beklenen davranış ve kabul kriterleri | Gecikme: DueOn < kullanıcının bugünü ve Status != Completed; null DueOn gecikmiş değildir; ders/tarih/öncelik/durum filtreleri birlikte AND. |
| Hata durumları ve olumsuz kabul | Bitiş filtresi başlangıçtan küçükse 400. |
| İş kuralı / test | BR-016 / TC-016 |

## FR-017 — Plan oluşturma

| Alan | Tanım |
| --- | --- |
| Modül / faz / öncelik | StudyPlans / Faz 3 / Must |
| Açıklama | Görev ve ders etkin; plan günü takvim tarihi; süre 1–1440; aynı görev farklı günlerde planlanabilir. |
| Ön koşul | Faz 1: örnek veri; Faz 2–4: yerel demo sahibi; Faz 5+: giriş yapan öğrenci. İlişkili varlık varsa sahibi ve etkinlik koşulu sağlanır. |
| Girdiler | TaskId=1; PlannedOn=2026-11-12; PlannedMinutes=60 |
| Beklenen davranış ve kabul kriterleri | Görev ve ders etkin; plan günü takvim tarihi; süre 1–1440; aynı görev farklı günlerde planlanabilir. |
| Hata durumları ve olumsuz kabul | Aynı görev/gün tekrarı 409; geçersiz süre 400. |
| İş kuralı / test | BR-017 / TC-017 |

## FR-018 — Plan düzenleme ve silme

| Alan | Tanım |
| --- | --- |
| Modül / faz / öncelik | StudyPlans / Faz 3 / Must |
| Açıklama | Plan bağımsız planlama kaydı; silmek gerçekleşmiş oturumu veya görevi silmez. |
| Ön koşul | Faz 1: örnek veri; Faz 2–4: yerel demo sahibi; Faz 5+: giriş yapan öğrenci. İlişkili varlık varsa sahibi ve etkinlik koşulu sağlanır. |
| Girdiler | id=1; PlannedMinutes=90 |
| Beklenen davranış ve kabul kriterleri | Plan bağımsız planlama kaydı; silmek gerçekleşmiş oturumu veya görevi silmez. |
| Hata durumları ve olumsuz kabul | Arşivli derste değişiklik 409; tarih değişiminde benzersizlik korunur. |
| İş kuralı / test | BR-018 / TC-018 |

## FR-019 — Günlük ve haftalık plan

| Alan | Tanım |
| --- | --- |
| Modül / faz / öncelik | StudyPlans / Faz 3 / Must |
| Açıklama | Hafta Pazartesi başlar; tarih aralığı uçları dahil; görev Completed ise plan görünümünde tamamlanmış etiketi. |
| Ön koşul | Faz 1: örnek veri; Faz 2–4: yerel demo sahibi; Faz 5+: giriş yapan öğrenci. İlişkili varlık varsa sahibi ve etkinlik koşulu sağlanır. |
| Girdiler | from=2026-11-09; to=2026-11-15 |
| Beklenen davranış ve kabul kriterleri | Hafta Pazartesi başlar; tarih aralığı uçları dahil; görev Completed ise plan görünümünde tamamlanmış etiketi. |
| Hata durumları ve olumsuz kabul | Planın bağımsız Completed alanı yoktur; boş günde boş durum. |
| İş kuralı / test | BR-019 / TC-019 |

## FR-020 — Plan ve gerçekleşen karşılaştırma

| Alan | Tanım |
| --- | --- |
| Modül / faz / öncelik | Statistics / Faz 4 / Must |
| Açıklama | Günlük görev-plan karşılaştırması yalnız aynı TaskId ve yerel gün oturum dakikalarını sayar; pazartesi 45, salı ayrı 30. Haftalık toplam tüm oturumları içerir. |
| Ön koşul | Faz 1: örnek veri; Faz 2–4: yerel demo sahibi; Faz 5+: giriş yapan öğrenci. İlişkili varlık varsa sahibi ve etkinlik koşulu sağlanır. |
| Girdiler | Görev A pazartesi plan 60; aynı gün oturum 45; salı 30 |
| Beklenen davranış ve kabul kriterleri | Günlük görev-plan karşılaştırması yalnız aynı TaskId ve yerel gün oturum dakikalarını sayar; pazartesi 45, salı ayrı 30. Haftalık toplam tüm oturumları içerir. |
| Hata durumları ve olumsuz kabul | Plan kaydı olmayan çalışma plansız olarak gösterilir; süre iki kez sayılmaz. |
| İş kuralı / test | BR-020 / TC-020 |

## FR-021 — Oturum başlatma

| Alan | Tanım |
| --- | --- |
| Modül / faz / öncelik | StudySessions / Faz 4 / Must |
| Açıklama | Sunucu StartAt=now; EndAt=null; kullanıcı başına tek açık oturum; görev/konu aynı ders ve sahiplik. |
| Ön koşul | Faz 1: örnek veri; Faz 2–4: yerel demo sahibi; Faz 5+: giriş yapan öğrenci. İlişkili varlık varsa sahibi ve etkinlik koşulu sağlanır. |
| Girdiler | CourseId=1; TaskId=1; TopicId=1 |
| Beklenen davranış ve kabul kriterleri | Sunucu StartAt=now; EndAt=null; kullanıcı başına tek açık oturum; görev/konu aynı ders ve sahiplik. |
| Hata durumları ve olumsuz kabul | İkinci açık oturum 409; arşivli ders 409. |
| İş kuralı / test | BR-021 / TC-021 |

## FR-022 — Oturum bitirme

| Alan | Tanım |
| --- | --- |
| Modül / faz / öncelik | StudySessions / Faz 4 / Must |
| Açıklama | EndAt > StartAt ve süre <=24 saat; tamamlanan oturum gerçek süre kaynağıdır; yeniden bitirme ilk EndAt değerini korur. |
| Ön koşul | Faz 1: örnek veri; Faz 2–4: yerel demo sahibi; Faz 5+: giriş yapan öğrenci. İlişkili varlık varsa sahibi ve etkinlik koşulu sağlanır. |
| Girdiler | id=1; EndAt sunucu now |
| Beklenen davranış ve kabul kriterleri | EndAt > StartAt ve süre <=24 saat; tamamlanan oturum gerçek süre kaynağıdır; yeniden bitirme ilk EndAt değerini korur. |
| Hata durumları ve olumsuz kabul | 24 saat aşılırsa 409; kullanıcı manuel düzeltmeye yönlendirilir; açık kayıt silinebilir. |
| İş kuralı / test | BR-022 / TC-022 |

## FR-023 — Manuel oturum kaydı

| Alan | Tanım |
| --- | --- |
| Modül / faz / öncelik | StudySessions / Faz 4 / Must |
| Açıklama | Başlangıç < bitiş <= now; süre <=24 saat; mevcut oturumlarla [start,end) çakışması yok. |
| Ön koşul | Faz 1: örnek veri; Faz 2–4: yerel demo sahibi; Faz 5+: giriş yapan öğrenci. İlişkili varlık varsa sahibi ve etkinlik koşulu sağlanır. |
| Girdiler | StartAt=2026-11-12T09:00:00+03:00; EndAt=2026-11-12T10:00:00+03:00 |
| Beklenen davranış ve kabul kriterleri | Başlangıç < bitiş <= now; süre <=24 saat; mevcut oturumlarla [start,end) çakışması yok. |
| Hata durumları ve olumsuz kabul | Gelecek, negatif/sıfır süre, çakışma 400/409. |
| İş kuralı / test | BR-023 / TC-023 |

## FR-024 — Oturum düzeltme ve silme

| Alan | Tanım |
| --- | --- |
| Modül / faz / öncelik | StudySessions / Faz 4 / Must |
| Açıklama | Manuel veya bitmiş oturum geçerli zamanlarla düzeltilebilir; açık oturum sadece bitirilir veya silinir. |
| Ön koşul | Faz 1: örnek veri; Faz 2–4: yerel demo sahibi; Faz 5+: giriş yapan öğrenci. İlişkili varlık varsa sahibi ve etkinlik koşulu sağlanır. |
| Girdiler | id=1; zamanlar; RowVersion |
| Beklenen davranış ve kabul kriterleri | Manuel veya bitmiş oturum geçerli zamanlarla düzeltilebilir; açık oturum sadece bitirilir veya silinir. |
| Hata durumları ve olumsuz kabul | Kendi kaydı dışı 404; çakışma 409; arşivli ders salt okunur. |
| İş kuralı / test | BR-024 / TC-024 |

## FR-025 — Günlük haftalık süre

| Alan | Tanım |
| --- | --- |
| Modül / faz / öncelik | Statistics / Faz 4 / Must |
| Açıklama | Bitmiş oturum yerel gün/hafta sınırlarında bölünür; her güne 30 dakika; açık oturum resmi toplama dahil edilmez. |
| Ön koşul | Faz 1: örnek veri; Faz 2–4: yerel demo sahibi; Faz 5+: giriş yapan öğrenci. İlişkili varlık varsa sahibi ve etkinlik koşulu sağlanır. |
| Girdiler | Pazar 23:30 -> Pazartesi 00:30; Europe/Istanbul |
| Beklenen davranış ve kabul kriterleri | Bitmiş oturum yerel gün/hafta sınırlarında bölünür; her güne 30 dakika; açık oturum resmi toplama dahil edilmez. |
| Hata durumları ve olumsuz kabul | Toplam saniyeler hesaplanır; yalnız gösterimde toplam dakika bir ondalık yuvarlanır. |
| İş kuralı / test | BR-025 / TC-025 |

## FR-026 — Dashboard özetleri

| Alan | Tanım |
| --- | --- |
| Modül / faz / öncelik | Dashboard / Faz 4 / Must |
| Açıklama | Aktif ders sayısı; 14 günlük sınavlar; geciken/bekleyen görevler; en son 5 CompletedAt; konu oranları; haftalık hedef ve süre; 7 günlük grafik. |
| Ön koşul | Faz 1: örnek veri; Faz 2–4: yerel demo sahibi; Faz 5+: giriş yapan öğrenci. İlişkili varlık varsa sahibi ve etkinlik koşulu sağlanır. |
| Girdiler | Aktif dersler, sınavlar, görevler, oturumlar |
| Beklenen davranış ve kabul kriterleri | Aktif ders sayısı; 14 günlük sınavlar; geciken/bekleyen görevler; en son 5 CompletedAt; konu oranları; haftalık hedef ve süre; 7 günlük grafik. |
| Hata durumları ve olumsuz kabul | Arşivli dersler operasyonel kartlardan çıkar; tarihsel oturum toplamları dahil. |
| İş kuralı / test | BR-026 / TC-026 |

## FR-027 — İstatistikler ve değişim

| Alan | Tanım |
| --- | --- |
| Modül / faz / öncelik | Statistics / Faz 4 / Should |
| Açıklama | Ders dağılımı, konu oranı, görev Completed/toplam, gecikme, plan-gerçek; tamamlanma oranı mevcut aktif görevlerin anlık oranıdır. Haftalık değişim önceki eş uzunluklu aralığa göre. |
| Ön koşul | Faz 1: örnek veri; Faz 2–4: yerel demo sahibi; Faz 5+: giriş yapan öğrenci. İlişkili varlık varsa sahibi ve etkinlik koşulu sağlanır. |
| Girdiler | from=2026-11-09; to=2026-11-15 |
| Beklenen davranış ve kabul kriterleri | Ders dağılımı, konu oranı, görev Completed/toplam, gecikme, plan-gerçek; tamamlanma oranı mevcut aktif görevlerin anlık oranıdır. Haftalık değişim önceki eş uzunluklu aralığa göre. |
| Hata durumları ve olumsuz kabul | Önceki süre 0 ise yüzde null, açıklama önceki dönemde çalışma yok; boş değerler 0/boş dizi. |
| İş kuralı / test | BR-027 / TC-027 |

## FR-028 — Çalışma hedefleri

| Alan | Tanım |
| --- | --- |
| Modül / faz / öncelik | Courses / Faz 2 / Must |
| Açıklama | Haftalık kişisel hedef aktif derslerin WeeklyTargetMinutes toplamıdır; ayrıca User hedefi tutulmaz. |
| Ön koşul | Faz 1: örnek veri; Faz 2–4: yerel demo sahibi; Faz 5+: giriş yapan öğrenci. İlişkili varlık varsa sahibi ve etkinlik koşulu sağlanır. |
| Girdiler | Aktif hedefler 120,180; arşivli 60 |
| Beklenen davranış ve kabul kriterleri | Haftalık kişisel hedef aktif derslerin WeeklyTargetMinutes toplamıdır; ayrıca User hedefi tutulmaz. |
| Hata durumları ve olumsuz kabul | Toplam 300; hedef 0 seçilebilir; tarihsel hedef snapshot MVP dışında. |
| İş kuralı / test | BR-028 / TC-028 |

## FR-029 — Kayıt ve giriş

| Alan | Tanım |
| --- | --- |
| Modül / faz / öncelik | Account / Faz 5 / Must |
| Açıklama | Identity ile e-posta benzersiz; giriş genel hata verir; hesap oluşturma ve çıkış akışları. |
| Ön koşul | Faz 1: örnek veri; Faz 2–4: yerel demo sahibi; Faz 5+: giriş yapan öğrenci. İlişkili varlık varsa sahibi ve etkinlik koşulu sağlanır. |
| Girdiler | email ve parola |
| Beklenen davranış ve kabul kriterleri | Identity ile e-posta benzersiz; giriş genel hata verir; hesap oluşturma ve çıkış akışları. |
| Hata durumları ve olumsuz kabul | Giriş hatasında hesabın varlığı ifşa edilmez; kilitleme ve parola ilkesi yapılandırılır. |
| İş kuralı / test | BR-029 / TC-029 |

## FR-030 — Profil ve zaman dilimi

| Alan | Tanım |
| --- | --- |
| Modül / faz / öncelik | Account / Faz 5 / Should |
| Açıklama | DisplayName 1–80; desteklenen IANA zaman dilimi; mevcut hesap dışında profil düzenlenemez. |
| Ön koşul | Faz 1: örnek veri; Faz 2–4: yerel demo sahibi; Faz 5+: giriş yapan öğrenci. İlişkili varlık varsa sahibi ve etkinlik koşulu sağlanır. |
| Girdiler | DisplayName=Berat; TimeZoneId=Europe/Istanbul |
| Beklenen davranış ve kabul kriterleri | DisplayName 1–80; desteklenen IANA zaman dilimi; mevcut hesap dışında profil düzenlenemez. |
| Hata durumları ve olumsuz kabul | Bilinmeyen zaman dilimi 400; saat dilimi değişimi geçmiş günlük gruplamayı değiştirir. |
| İş kuralı / test | BR-030 / TC-030 |

## FR-031 — Veri izolasyonu

| Alan | Tanım |
| --- | --- |
| Modül / faz / öncelik | Security / Faz 5 / Must |
| Açıklama | Her okuma/yazma sunucu kullanıcı kapsamıyla; FK ilişkilerinin sahipliği doğrulanır; UserId body kabul edilmez. |
| Ön koşul | Faz 1: örnek veri; Faz 2–4: yerel demo sahibi; Faz 5+: giriş yapan öğrenci. İlişkili varlık varsa sahibi ve etkinlik koşulu sağlanır. |
| Girdiler | A kullanıcısı B kaydının id değerini gönderir |
| Beklenen davranış ve kabul kriterleri | Her okuma/yazma sunucu kullanıcı kapsamıyla; FK ilişkilerinin sahipliği doğrulanır; UserId body kabul edilmez. |
| Hata durumları ve olumsuz kabul | B kaydı 404; girişsiz MVC login yönlendirmesi, API 401. |
| İş kuralı / test | BR-031 / TC-031 |

## FR-032 — İlk teslim MVC ekranları

| Alan | Tanım |
| --- | --- |
| Modül / faz / öncelik | UI / Faz 1 / Must |
| Açıklama | 3 Controller, her birinde 2 ayrı View; örnek veri; layout menüsü; Razor foreach ve koşullu boş durum; Bootstrap/CSS. |
| Ön koşul | Faz 1: örnek veri; Faz 2–4: yerel demo sahibi; Faz 5+: giriş yapan öğrenci. İlişkili varlık varsa sahibi ve etkinlik koşulu sağlanır. |
| Girdiler | Dashboard/Index, Dashboard/Weekly; Courses/Index, Courses/Details; Exams/Index, Exams/Details |
| Beklenen davranış ve kabul kriterleri | 3 Controller, her birinde 2 ayrı View; örnek veri; layout menüsü; Razor foreach ve koşullu boş durum; Bootstrap/CSS. |
| Hata durumları ve olumsuz kabul | Geçersiz örnek id 404 sayfası; henüz veri kaydı yapıldığı iddia edilmez. |
| İş kuralı / test | BR-032 / TC-032 |

## FR-033 — Responsive ve erişilebilir ekranlar

| Alan | Tanım |
| --- | --- |
| Modül / faz / öncelik | UI / Faz 1 / Must |
| Açıklama | Ana içerikte yatay taşma yok; menü mobilde açılır; etiket ve odağı görünür; hata yalnız renkle aktarılmaz. |
| Ön koşul | Faz 1: örnek veri; Faz 2–4: yerel demo sahibi; Faz 5+: giriş yapan öğrenci. İlişkili varlık varsa sahibi ve etkinlik koşulu sağlanır. |
| Girdiler | 360,768,1440px; klavye |
| Beklenen davranış ve kabul kriterleri | Ana içerikte yatay taşma yok; menü mobilde açılır; etiket ve odağı görünür; hata yalnız renkle aktarılmaz. |
| Hata durumları ve olumsuz kabul | Tablo dar ekranda etiketli kart veya kontrollü tablo kaydırması. |
| İş kuralı / test | BR-033 / TC-033 |

## FR-034 — Tekrarlanan gönderim kontrolü

| Alan | Tanım |
| --- | --- |
| Modül / faz / öncelik | Platform / Faz 2 / Must |
| Açıklama | MVC Post/Redirect/Get ve kullanıcı+işlem+OperationId benzersiz SubmissionReceipt; aynı hash tekrarında önceki sonuç, farklı payload 409. |
| Ön koşul | Faz 1: örnek veri; Faz 2–4: yerel demo sahibi; Faz 5+: giriş yapan öğrenci. İlişkili varlık varsa sahibi ve etkinlik koşulu sağlanır. |
| Girdiler | Aynı OperationId ile aynı POST iki kez |
| Beklenen davranış ve kabul kriterleri | MVC Post/Redirect/Get ve kullanıcı+işlem+OperationId benzersiz SubmissionReceipt; aynı hash tekrarında önceki sonuç, farklı payload 409. |
| Hata durumları ve olumsuz kabul | API eklendiğinde Idempotency-Key aynı mekanizma; iş kaydı ve receipt tek transaction. |
| İş kuralı / test | BR-034 / TC-034 |

## FR-035 — Eşzamanlı düzenleme

| Alan | Tanım |
| --- | --- |
| Modül / faz / öncelik | Platform / Faz 2 / Must |
| Açıklama | SQL Server rowversion; MVC hidden base64 token; API güncelleme/silmede If-Match; ilk başarılı, ikinci 409. |
| Ön koşul | Faz 1: örnek veri; Faz 2–4: yerel demo sahibi; Faz 5+: giriş yapan öğrenci. İlişkili varlık varsa sahibi ve etkinlik koşulu sağlanır. |
| Girdiler | İki sekme aynı RowVersion |
| Beklenen davranış ve kabul kriterleri | SQL Server rowversion; MVC hidden base64 token; API güncelleme/silmede If-Match; ilk başarılı, ikinci 409. |
| Hata durumları ve olumsuz kabul | Eksik token API 428; yanlış biçim 400; veri sessiz ezilmez. |
| İş kuralı / test | BR-035 / TC-035 |

## FR-036 — Kimlik öncesi yerel geliştirme

| Alan | Tanım |
| --- | --- |
| Modül / faz / öncelik | Security / Faz 2 / Must |
| Açıklama | Faz 2–4 sadece yerelde bir sabit demo sahibi; User tablosuna demo sahip eklenir; her sorgu owner filtresiyle tasarlanır. |
| Ön koşul | Faz 1: örnek veri; Faz 2–4: yerel demo sahibi; Faz 5+: giriş yapan öğrenci. İlişkili varlık varsa sahibi ve etkinlik koşulu sağlanır. |
| Girdiler | DemoOwnerId=development-demo |
| Beklenen davranış ve kabul kriterleri | Faz 2–4 sadece yerelde bir sabit demo sahibi; User tablosuna demo sahip eklenir; her sorgu owner filtresiyle tasarlanır. |
| Hata durumları ve olumsuz kabul | Identity öncesi internete yayın yasak; Production başlangıcı demo modunda başarısız olur. |
| İş kuralı / test | BR-036 / TC-036 |

[Dokümantasyon indeksi](../README.md)
