# Test ve kabul

[Ana sayfa](../README.md) · [Kapsam ve kurallar](01-kapsam-ve-kurallar.md)

**Durum:** Aşağıdaki liste test planıdır; çalıştırılmış uygulama sonucu değildir. Özellik geliştirildiği fazda sınanır. Should senaryolar yalnız özellik uygulanmışsa zorunludur; ertelenenler gerekçeli kaydedilir.

## Test yaklaşımı

| Seviye | Kapsam |
| --- | --- |
| Birim / xUnit | Oran, durum geçişi, gecikme, zaman aralığı ve gün/hafta bölme; sabit IClock. |
| MVC entegrasyon | Form/ModelState, PRG, anti-forgery, giriş ve owner; gerçek Identity kayıt/cookie/çıkış akışı ayrıca. |
| Gerçek SQL Server | FK, check, unique, rowversion, receipt transaction ve eşzamanlı yarışlar. EF InMemory/SQLite bu kanıtın yerine geçmez. |
| Tarayıcı / kabul | Altı View, gezinme, boş/hata/başarı, klavye ve responsive; gerekirse Playwright. |

Ortak fixture: A ve B kullanıcıları, aktif/arşivli dersler; saat `2026-11-12T12:00:00Z`, zaman dilimi Europe/Istanbul. DST senaryosu Europe/Berlin kullanır. Test başına/suite başına izole veritabanı ve temizleme gerekir; gerçek kullanıcı verisi kullanılmaz. Yarış testleri iki ayrı DbContext/transaction ve eşzamanlı başlatma bariyeriyle yürütülür.

## Temel özellik kabulü

Her satırdaki geçerli akışın yanında boş, sınır dışı ve bulunamayan ilişki varyantı da sınanır. Form hatası HTTP 200 ve alan mesajıdır; bulunamadı 404, iş çatışması 409'dur. Beklenen DB değişikliği veya değişmemesi ayrıca kontrol edilir.

| İlgili FR | Kabul senaryosu |
| --- | --- |
| 001–005,028 | Ders oluştur/listele/düzenle; ad filtresi ve sırası; arşivle/geri aç; ilişkisiz sil; bağlı silmeyi engelle. Aktif hedefler 120+180 ve arşivli 60 ise kişisel hedef 300. |
| 006–008 | Geçerli geçmiş/yaklaşan sınav ekle, sırala, detay/düzenle/sil; bozuk tarih ve geçersiz tür reddedilsin. Tarih now ise Upcoming; saat 1 saniye ilerleyince Past; now+14 gün dahil edilmesin. |
| 009–012 | Konu ekle/sırala/durum değiştir; dersi değiştirilemesin; bağlı konu silinmesin. Dört konudan biri Completed ise %25,0; sıfır konuda 0 ve açıklama. Not/tekrar tarihi Should. |
| 013–016 | Görev oluşturma Pending; filtreler AND; dün tarihli tamamlanmamış gecikmiş, bugünkü/null tarih gecikmemiş. CompletedAt ilk geçişte atanır, tekrarda korunur, geri açmada temizlenir. Ders değişmez; bağlı oturum varsa konu değişmez; plan/oturum varken silinmez. |
| 017–020 | Aynı görev farklı günlerde planlanabilir; aynı gün tekrarı farklı OperationId ile bile 409. Gün/süre/görev değişimi kısıtları korur; plan silmek gerçek oturumu etkilemez; tamamlanma etiketi görevden gelir. |
| 021–025 | Timer başlat/bitir, manuel ekle, bitmiş kaydı düzelt/sil; açık kayıt düzenlenemesin. Görev/konu ve ders uyumlu olsun; gerçek süre yalnız bitmiş oturumlardan gelsin. |
| 026 | Dashboard aktif kartları, son 5 CompletedAt, hedef/süre ve yedi günlük grafiği doğru göstersin. Arşivli dersin geçmiş süresi toplamda, görev/sınavları operasyonel kartlarda olmasın. Boş grafikte yedi adet 0. |
| 027 (Should) | Ders dağılımı, anlık görev/konu oranı ve önceki eş uzunluklu aralık. Önceki süre 0 iken yüzde null ve açıklama; NaN/bölme hatası yok. |
| 029–031 | Gerçek kayıt/giriş/cookie/POST çıkış; normalize e-posta benzersizliği; genel giriş hatası ve kilitleme. A yalnız kendi veri ve toplamlarını görsün. Profil Should; geçersiz dilim reddedilsin. |
| 032–033 | Üç Controller ve altı ayrı anlamlı View; layout/partial sayılmaz; demo etiketi, dinamik Razor, menü/detay/filtre, yanlış Id ve boş durum. 360/768/1440px ve klavye. |
| 034–036 | Aynı oluşturma isteği tek kayıt; eski version veri ezmesin; demo sahibi yerel geliştirmeyle sınırlı kalsın. Aşağıdaki kritik varyantlar geçsin. |
| 037–039 | Faz 1B `Courses/Create` (`HtmlHelpers`, tek anti-forgery token) geçerli girdide `Session`'a aktif ders ekleyip PRG ile `/Courses/Details/{id}` sayfasına yönlendirsin; yeni ders `/Courses` ve `/Dashboard` hedef/aktif ders toplamlarında görünsün; geçersiz girdide aynı View ve hatalar korunsun, `Session`'a kayıt eklenmesin; `_CourseCard` PartialView (`Courses/Index`, `Dashboard/Index`) ve `UpcomingExams` ViewComponent (`Dashboard/Index`, `Courses/Details`) doğru bağlamda çalışsın. |

## Kritik olumsuz ve sınır senaryoları

| Senaryo | Beklenen sonuç |
| --- | --- |
| Boşluk ad; metin üst sınırları; hedef -1/10081; tahmin veya plan 0/1441 | Alan hatası; iş kaydı ve receipt oluşmaz. Geçerli alt/üst sınırlar kabul edilir. |
| Bilinmeyen enum veya ters tarih filtresi | Form hatası; veri değişmez. |
| A, B'nin detail/edit/delete kimliğini veya CourseId/TopicId/TaskId ilişkisini gönderir | Genel 404; listeler/sayımlar B verisini içermez; SQL bileşik FK doğrudan geçersiz insert'i de engeller. |
| Kullanıcı formda UserId/CreatedAt/CompletedAt gönderir | Sunucu alanları atanamaz; başka kullanıcıya kayıt üretilemez. |
| Giriş cookie'si var, anti-forgery yok | Yazma ve logout 400; veri/oturum değişmez. |
| Başlık veya uygulanmış not alanında script/HTML metni | Encode edilmiş metin; script çalışmaz. |
| Aynı OperationId/girdiyle iki paralel create | Tek iş kaydı ve receipt; aynı kaynak. Aynı anahtar/farklı girdi çatışır. |
| İlk oluşturulan kaynak silindikten sonra aynı OperationId tekrarı | 409; kaynak yeniden oluşturulmaz. |
| İki sekme aynı RowVersion ile değiştirir veya siler | İlk işlem başarılı; ikinci 409; ilk sonuç ezilmez. |
| Aynı kullanıcı iki ayrı anahtarla eşzamanlı Timer başlatır | Yalnız bir açık kayıt; diğeri 409. |
| Timer'ı bitir; saati ilerlet; eski formdan tekrar bitir | Mevcut EndAt ve süre korunur; ikinci süre yazılmaz. |
| Timer aynı saniyede bitirilir; sonra yeniden denenir | İlk deneme süre üretmez; geçerli pozitif süreli ikinci deneme başarılı. |
| Manuel süre 0/negatif; bitiş now+1 saniye | Alan hatası; toplam değişmez. |
| Manuel kayıt 86400 saniye ve ayrı fixture'da 86401 saniye | İlki kabul, ikincisi ret. |
| 24 saati aşan açık Timer | Stop 409; UI iptal/manuel kayıt yolunu sunar. İptalden sonra geçerli manuel kayıt eklenir ve yeni Timer açılabilir; iptal edilen kayıt toplamda yoktur. |
| Mevcut 09–10; yeni 09:30–10:30 ve ardından 10–11 | Çakışan 409; bitişik kabul; toplam 120 dakika. |
| İki paralel, farklı anahtarlı çakışan manuel kayıt | Yalnız biri kaydolur; diğer işlem 409; çift sayım yoktur. |
| Açık Timer varken arşiv; ayrıca eşzamanlı start/archive | Açık varken arşiv 409; yarış sonunda arşivli derste açık Timer bulunmaz. |
| Arşivli derse alt kayıt create/edit/delete/status; ardından history GET | Yazmalar 409; geçmiş okunabilir; bağlı kayıtlar korunur. |
| Konu/görev dersini değiştirme; oturum bağlı görevin konusunu değiştirme | Ders değişimi her durumda, bağlı görevde konu değişimi engellenir; geçmiş dağılım değişmez. |
| 2026-11-08 23:30 → 2026-11-09 00:30 +03:00 | Her güne/haftaya 30 dakika; toplam 60; aralıkla kesişim doğru kırpılır. |
| Europe/Berlin: 2026-10-25 01:30+02:00 → 03:30+01:00 | Gerçek 180 dakika; aynı yerel güne 180; 120 dakika sayılmaz. |
| Pazartesi plan60/gerçek45, salı aynı görevde30, pazartesi görevsiz15 | Toplam90; plan farkı -15; plansız45. Plan silinse de toplam90. |
| DemoMode Development dışında açık; ayrıca kullanıcı bağlamı yok | Host güvenli başlangıç hatası; sahipsiz okuma/yazma yapılmaz. |
| Kasıtlı beklenmedik DB hatası | Genel 500 ve traceId; UI/logda body, not, parola, token veya bağlantı bilgisi yok. |

## Yayın kapısı ve ölçümler

- Bütün Must davranışlar ve kritik güvenlik/veri bütünlüğü senaryoları geçer. Should işler uygulanmışsa test edilir, ertelenmişse kaydedilir.
- A/B izolasyonunda sıfır sızıntı; gerçek Identity giriş/çıkış ve CSRF kanıtı bulunur. Güvenlik hatası açıkken yayın yapılmaz.
- 360/768/1440px ana içerik taşması sıfır; klavye, etiket, focus ve kontrast kontrol edilir.
- Kullanılabilirlik hedefi: üç öğrenci ders ekleme → görev planlama → oturum kaydetme akışını beş dakika içinde tamamlar. Sonuç ölçülüp kaydedilir.
- Performans hedefi: seçilen makinede kullanıcı başına 100 ders/1000 görev/10000 oturum ve 10 eşzamanlı kullanıcı; warm-up sonrası 5 dakikalık ölçümde dashboard p95 <2 saniye. Makine, commit ve sorgu sayısı kaydedilir; sonuç yokken başarı iddia edilmez.
- Boş DB ve önceki şemadan migration; staging backup restore; production demo guard, sır ve izin kontrolleri doğrulanır.

Geçiş kanıtı issue/PR'da test adı, commit, ortam, sonuç ve varsa ertelenen işlerle tutulur. Ayrı bir uzun kalite raporu veya aynı bilgiyi tekrar eden izlenebilirlik dosyası gerekmez.
