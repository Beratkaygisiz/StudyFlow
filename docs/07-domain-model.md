# Domain modeli

Durum: tasarım; bu depoda çalışan uygulama veya yürütülmüş uygulama testleri yoktur.

## Kavram ayrımı
User dokümanındaki kullanıcı, uygulamada `ApplicationUser : IdentityUser` olarak adlandırılır. Identity standart tabloları tekrar tasarlanmaz. StudyTask “20 soru çöz”, StudyPlanItem “Pazartesi 60 dakika ayır”, StudySession “Pazartesi 09:00–09:45 gerçekten çalıştım” demektir. Görev tahmini, plan süresi ve gerçek süre birbirinin yerine geçmez. Gerçek süre yalnız bitmiş StudySession zaman farkından türetilir; DurationMinutes/ActualMinutes tabloda tutulmaz.

## ApplicationUser

**Amaç ve yaşam döngüsü:** Ziyaretçi → kayıtlı → oturum açmış/çıkmış. Demo kullanıcı Faz 2 seed ile; Faz 5 temiz demo ortamına taşınır, gerçek hesabın yerine kullanılamaz. Hesap silme MVP dışında; kullanıcı FK NO ACTION.

| Alan | C# | Nullable | Gerekçe |
| --- | --- | --- | --- |
| Id | string | hayır | Identity kimliği ve sahiplik |
| Email | string | evet | Identity standart alanı; kayıt akışında zorunlu ve normalize benzersiz |
| DisplayName | string | hayır | Profil görünen adı |
| TimeZoneId | string | hayır | Yerel gün hesabı; varsayılan Europe/Istanbul |

## Course

**Amaç ve yaşam döngüsü:** Active ↔ Archived; yalnız ilişkisizken hard delete. Arşivde tüm alt kayıtlar görüntülenebilir, yazılamaz. Açık oturum arşivlemeyi engeller.

| Alan | C# | Nullable | Gerekçe |
| --- | --- | --- | --- |
| Id | int | hayır | PK |
| UserId | string | hayır | Sahip FK |
| Name | string | hayır | Ders adı |
| IsArchived | bool | hayır | Geçmişi koruyan etkinlik |
| WeeklyTargetMinutes | int | hayır | Ders haftalık hedefi, varsayılan 0 |
| CreatedAt | DateTimeOffset | hayır | UTC oluşturulma anı |
| RowVersion | byte[] | hayır | Optimistic concurrency; server üretir |

## Exam

**Amaç ve yaşam döngüsü:** Tarih yaklaşan/geçmiş durumunu belirler; ek bir Past alanı yok. Aktif derste düzenle/sil; arşivde salt okunur.

| Alan | C# | Nullable | Gerekçe |
| --- | --- | --- | --- |
| Id | int | hayır | PK |
| UserId | string | hayır | Owner kapsamı |
| CourseId | int | hayır | Ders FK |
| Title | string | hayır | Sınav adı |
| Type | ExamType | hayır | Midterm=0, Final=1, Quiz=2, Other=3 |
| ScheduledAt | DateTimeOffset | hayır | Sınav anı; UTC normalize |
| CreatedAt | DateTimeOffset | hayır | UTC oluşturulma anı |
| RowVersion | byte[] | hayır | Optimistic concurrency; server üretir |

## Topic

**Amaç ve yaşam döngüsü:** NotStarted ↔ InProgress ↔ Completed; tamamlama görevlerden bağımsız. Görev/oturum bağlıysa delete engellenir.

| Alan | C# | Nullable | Gerekçe |
| --- | --- | --- | --- |
| Id | int | hayır | PK |
| UserId | string | hayır | Owner kapsamı |
| CourseId | int | hayır | Ders FK |
| Title | string | hayır | Konu adı |
| Status | TopicStatus | hayır | NotStarted=0, InProgress=1, Completed=2 |
| SortOrder | int | hayır | Kullanıcı sıralaması; eşitlik Id ile |
| Notes | string? | evet | Kişisel konu notu |
| ReviewOn | DateOnly? | evet | Tekrar takvim günü |
| CreatedAt | DateTimeOffset | hayır | UTC oluşturulma anı |
| RowVersion | byte[] | hayır | Optimistic concurrency; server üretir |

## StudyTask

**Amaç ve yaşam döngüsü:** Pending ↔ InProgress ↔ Completed; tekrar Completed idempotent; CompletedAt geri açmada temizlenir. İlişkili plan/oturum varken delete engellenir.

| Alan | C# | Nullable | Gerekçe |
| --- | --- | --- | --- |
| Id | int | hayır | PK |
| UserId | string | hayır | Owner kapsamı |
| CourseId | int | hayır | Ders FK |
| TopicId | int? | evet | Aynı dersin opsiyonel konusu |
| Title | string | hayır | Yapılacak iş |
| Status | TaskStatus | hayır | Pending=0, InProgress=1, Completed=2 |
| Priority | TaskPriority | hayır | Low=0, Normal=1, High=2 |
| DueOn | DateOnly? | evet | Takvim son günü |
| EstimatedMinutes | int | hayır | Tahmin; gerçekleşmiş süre değil |
| CompletedAt | DateTimeOffset? | evet | Son tamamlama anı; durum geri alınırsa null |
| CreatedAt | DateTimeOffset | hayır | UTC oluşturulma anı |
| RowVersion | byte[] | hayır | Optimistic concurrency; server üretir |

## StudyPlanItem

**Amaç ve yaşam döngüsü:** Oluştur → yeniden gün/süre planla → sil. Completed durumu görevden türetilir; geçmiş güne plan yapılabilir, uygulama gerçek çalışma diye saymaz.

| Alan | C# | Nullable | Gerekçe |
| --- | --- | --- | --- |
| Id | int | hayır | PK |
| UserId | string | hayır | Owner kapsamı |
| CourseId | int | hayır | Görevle eş ders; arşiv/sorgu/bileşik FK için kontrollü tekrar |
| TaskId | int | hayır | Planlanan görev |
| PlannedOn | DateOnly | hayır | Görevin planlandığı gün |
| PlannedMinutes | int | hayır | O gün ayrılan tahmini süre |
| CreatedAt | DateTimeOffset | hayır | UTC oluşturulma anı |
| RowVersion | byte[] | hayır | Optimistic concurrency; server üretir |

## StudySession

**Amaç ve yaşam döngüsü:** Timer açık → bitmiş; Manual doğrudan bitmiş. Açık kayıt silinerek iptal edilir; bitmiş zamanlar düzeltilebilir. Görev tamamlanması ayrı kullanıcı kararıdır.

| Alan | C# | Nullable | Gerekçe |
| --- | --- | --- | --- |
| Id | int | hayır | PK |
| UserId | string | hayır | Owner kapsamı |
| CourseId | int | hayır | Gerçekleşen çalışma dersi |
| TaskId | int? | evet | Opsiyonel görev |
| TopicId | int? | evet | Opsiyonel konu; görev konusu doluysa aynı olmak zorunda |
| StartAt | DateTimeOffset | hayır | UTC başlangıç; zamanlayıcıda sunucu üretir |
| EndAt | DateTimeOffset? | evet | UTC bitiş; null açık oturum |
| Source | SessionSource | hayır | Timer=0, Manual=1 |
| CreatedAt | DateTimeOffset | hayır | UTC oluşturulma anı |
| RowVersion | byte[] | hayır | Optimistic concurrency; server üretir |

## SubmissionReceipt

**Amaç ve yaşam döngüsü:** Teknik yardımcı; iş kaydıyla atomik oluşur. MVP boyunca tutulur; key tekrarında aynı payload ilk kaydı döndürür; silinmiş ResourceId için 409 original-resource-deleted. Süreli temizleme kararı ertelendi.

| Alan | C# | Nullable | Gerekçe |
| --- | --- | --- | --- |
| Id | long | hayır | Teknik PK |
| UserId | string | hayır | İşlemi yapan sahip |
| Operation | string | hayır | Örn Course.Create |
| OperationId | Guid | hayır | Form nonce veya API key UUID |
| PayloadHash | string | hayır | Normalize DTO SHA256; gizli bilgi içermez |
| ResourceId | int | hayır | Oluşturulan iş kaydının kimliği; generic FK değil |
| CreatedAt | DateTimeOffset | hayır | UTC receipt zamanı |

## İlişkiler ve kapsam
User 1:N Course/Exam/Topic/StudyTask/StudyPlanItem/StudySession. Course 1:N tüm ders kayıtları; Topic 1:N opsiyonel görev ve oturum; StudyTask 1:N plan ve opsiyonel oturum. Çoktan çoğa ihtiyaç yoktur: bir görev tek derse bağlı, bir plan kaydı tek göreve bağlıdır. Plan ve oturum arasında doğrudan FK yok; TaskId + kullanıcının yerel günü karşılaştırma eşlemesidir.

UserId alt kayıtlarda owner sorgusu ve bileşik FK için bilinçli tekrar. Plan CourseId aynı nedenle tutulur, uygulama bunu Task’tan üretir. İstemci UserId/CourseId(plan) ile sahiplik atayamaz. Oturum TaskId varsa CourseId aynı; Task.TopicId doluysa Session.TopicId aynıdır, servis gerekirse türetir. Konusu olmayan göreve oturum sırasında aynı dersten konu seçilebilir. Göreve bağlanan oturum varken görev CourseId/TopicId değişimi engellenir; eski süreler başka konuya kaymaz.

Çalışma hesabı: UTC anları; DateOnly kullanıcı takvim günü; IClock ve TimeZoneInfo/IANA eşlemesi başlangıçta platformda doğrulanır. JSON enum string, DB enum tinyint. DTO/domain ayrıdır; ViewModel EF entity’si değildir.

[Dokümantasyon indeksi](../README.md)
