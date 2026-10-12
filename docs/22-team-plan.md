# Üç kişilik ekip görev planı

İsimler verilmediği için Üye A/B/C kullanılır. Her üye toplam100 göreli iş puanı taşır; bu ilk eşitlik **iş yükü tahminidir**, eşit satır/dosya/saat değildir. Her puanın yaklaşık saat karşılığı ilk sprintte ölçülür. Faz tahmin aralığı/3 kişi yaklaşık kapasite hedefidir. Deneyim farkı nedeniyle görev büyürse sprint sonuna kadar alt iş transfer edilir; faz sonunda yük farkı hedefi<=%10. Test ve doküman güncelleme puana dahildir.

| Faz | Her üyenin puanı | Üye A | Üye B | Üye C |
| --- | --- | --- | --- | --- |
| 0 | 8 | Gereksinim/MoSCoW ve ders teslimi matrisi | Domain/DB ilişkileri ve veri bütünlüğü | UI/API/test envanteri ve bağlantı kontrolü |
| 1 | 10 | CoursesController Index/Details + ders DemoData | ExamsController Index/Details + sınav DemoData | DashboardController Index/Weekly + Layout/CSS; ortak tasarım yükü diğerlerinin ekran CSS işiyle dengelenir |
| 2 | 15 | CourseService/CRUD/archiving + course testleri | ExamService/CRUD + exam testleri; DbContext ortak owner | Receipt/RowVersion altyapısı + platform testleri; build/CI başlangıcı |
| 3 | 18 | TopicService/Topics ekranları + konu testleri | StudyTaskService/görev ekranları + durum/filter testleri | StudyPlanService/plan ekranları + plan testleri; schema ortak owner |
| 4 | 20 | StudySession timer start/stop/UI + açık oturum/race testleri | Manuel oturum/düzeltme/silme + çakışma testleri; süre split saf fonksiyonu | Dashboard/Statistics UI/query + karşılaştırma testleri; DB indeks ölçümü |
| 5 | 17 | Register/login/logout + gerçek Identity auth testleri | CurrentUser/owner/CSRF inceleme + A/B izolasyon testleri | Profile/zaman dilimi + güvenli hata/Production guard; auth entegrasyon koordinasyonu |
| 6 | 12 | Uçtan uca kabul/responsive ve son ekran düzeltmeleri | SQL migration/backup-restore + Production config runbook | Performans/log/CI-release ve doküman-kod eşleme; sunum entegrasyonu |

## Alt görev standardı
Her hücre bir epic’tir; 2–5 puanlık issue’lara böl. Kendi feature’ının controller/view/service/validation/test/doc zincirini owner yürütür. Ortak Program.cs/DbContext/schema sahibi koordinasyon yapar; öteki owner migration önerisini PR ile getirir. Faz1 C’nin Layout yükünün bir kısmını A/B kendi responsive ekranlarına uygulayarak dengeler; C bütün CSS’yi tek başına yapmaz. Faz4 manuel oturum işi ile timer işi aynı StudySessionService’te olduğundan önce ortak DTO ve transaction contract 30dk görüşmeyle belirlenir; tek owner dosyayı düzenler, diğeri ayrı yardımcı/PR sırası kullanır.

Çapraz reviewer A→B, B→C, C→A (A’nın PR’sini B inceler). Review/test tekrar ve sunum her üyeye aynı2 puan/faz iç bütçesi olarak dahil. Haftalık20dk: tamamlanan/artan tahmin, risk, yeni ders isterleri; artan işi boş kapasiteye aktar. Tüm üyeler son uçtan uca akışı anlatabilir; yalnız UI/yalnız test kişisi yok.

## Bağımlılık ve paralel ilerleme
Faz1 shared ViewModel/DemoData sözleşmesi sonra3 Controller paralel. Faz2 owner/DbContext temelinden sonra Course/Exam/receipt. Faz3 Course FK tamamlanınca Topic; Task konusu opsiyonel olduğundan temel task parallel, konu ilişki kabulü Topic sonrası; plan Task sonrası. Faz4 Session temel zaman DTO’su erken; C mock data ile ekran başlatır fakat gerçek sorgu kabulü Session persistence sonrası. Faz5 cookie/claim sözleşmesi sonra owner/profile testleri; Faz6 herkes kendi modül kusurlarını düzeltir, yayın koordinasyonu tek kişi.

Definition of Done: FR kabul ve olumsuz TC; veri değişimi için gerçek SQL; doküman güncel; owner kod akışını açıklayabiliyor; başka üye review; main build/smoke. Eşit puan sağlamak için anlamsız test/dosya üretme. Yeni ders isteri gelirse100 puan toplamını baz çizgi olarak koru, ertelenen iş/ek yükü açık raporla.

[İndeks](../README.md) · [Yol haritası](16-development-roadmap.md)
