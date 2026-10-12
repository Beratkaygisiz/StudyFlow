# Arayüz ve deneyim

Durum: tasarım; bu depoda çalışan uygulama veya yürütülmüş uygulama testleri yoktur.

## Bilgi mimarisi ve ilk teslim
Ortak üst/yan menü Dashboard, Dersler, Sınavlar; Faz 3’te Görevler/Plan, Faz 4’te Oturumlar/İstatistik, Faz 5’te Profil/Giriş. Aktif sayfa aria-current ile; mobil collapse butonu label ve aria-expanded ile. Ortak `_Layout.cshtml`, `_ValidationScriptsPartial.cshtml`, `site.css` paylaşılan görünüm dosyalarıdır; altı anlamlı View hesabına dahil edilmez.

| Controller | View / route | Amaç ve bileşen | Faz1 dinamik içerik |
| --- | --- | --- | --- |
| DashboardController | Views/Dashboard/Index.cshtml / /Dashboard | Özet kartları, yaklaşan sınavlar, boş durum | Razor foreach örnek sınav ve aktif ders sayısı |
| DashboardController | Views/Dashboard/Weekly.cshtml / /Dashboard/Weekly | 7 günlük çalışma/plan demo tablosu ve hedef çubuğu | Razor gün döngüsü; örnek süreler |
| CoursesController | Views/Courses/Index.cshtml / /Courses | Ders kartları ve ad/durum GET filtresi | Filtrelenmiş DemoData listesi |
| CoursesController | Views/Courses/Details.cshtml / /Courses/Details/1 | Ders hedefi, örnek konu listesi, sınav linki | Id’ye bağlı farklı ders ViewModel |
| ExamsController | Views/Exams/Index.cshtml / /Exams | Tarih sıralı sınavlar, yaklaşan/geçmiş filtre | IClock demo tarihleri üzerinden hesap |
| ExamsController | Views/Exams/Details.cshtml / /Exams/Details/1 | Sınav tarihi/türü, kalan gün, ders bağlantısı | Kalan gün ve ilişkili ders koşullu Razor |

Varsayılan `/` route Dashboard/Index’e açılır. Faz1 GET filtre ve detay navigasyonu işler; Create/Edit gibi kalıcı yazmalar Faz2’ye kadar etkinmiş gibi gösterilmez.

Varsayılan `/` route Dashboard/Index’e açılır. Faz1 GET filtre ve detay navigasyonu işler; Create/Edit gibi kalıcı yazmalar Faz2’ye kadar etkinmiş gibi gösterilmez.

## Sonraki ekran envanteri
Faz2 Courses/Create,Edit,DeleteConfirm; Exams/Create,Edit,DeleteConfirm; var olan Index/Details canlı servis verisine geçer. Form course name/hedef, exam course/title/type/date-time+zaman dilimi; hidden OperationId(create)/RowVersion(edit). Archive Courses/Edit formunda açık onay; bağlı silme 409 mesajı.

Faz3 Topics/Index,Create,Edit (course context, title,status,sortOrder,notes,reviewOn); StudyTasks/Index,Details,Create,Edit (ders/opsiyonel konu,başlık,durum,tarih,tahmin,öncelik); StudyPlans/Index,Create,Edit (gün/hafta toggle, task,date,minutes). Faz4 StudySessions/Index,Create,Edit (start/stop ayrı POST form butonları; manuel başlama/bitme,ders,task,topic); Statistics/Index (aralık,bar/table,plan farkı). Faz5 Identity scaffold register/login/logout, Profile/Index,Edit. Ayrı API ekranı yok.

## Durumlar ve geribildirim
Boş ders: Henüz ders yok + oluştur CTA (Faz1 CTA yerine örnek veri açıklaması). Boş istatistik: 0 ve kayıt yok; null yüzde için açıklama. Sunucu render edilen GET’te yapay skeleton zorunlu değil; submit sırasında buton bekleme/disabled, iş bitince server cevabı ve aria-live. Form hata özetinde alan linkleri; alanlarda aria-describedby. Başarı TempData bildirimi PRG sonrası. 404 genel kayıt bulunamadı; 409 eski sekme için yeniden yükle/kopya değer koruma; silme onayı ders adı ve ilişkilerin davranışını açıklar.

## Görsel dil
Ana #1D4ED8, açık yüzey #EFF6FF, metin #0F172A, ikincil #475569, beyaz #FFFFFF. Beyaz-mavi buton kontrastını ölç; açık maviyi metin için kullanma. Sistem fontu, gövde16px, başlık24–32px, satır1.5. Boşluk4/8/16/24/32px; kart border radius12px, gölge hafif, ana CTA tek. Form label üstte; mobil buton dokunma alanı en az44px hedef. 360px tek kolon; 768px iki; 1440px 3–4 kart. Grafik yanında aynı veri tablosu bulunur; yalnız renk farkına güvenilmez; prefers-reduced-motion desteklenir.

Ekran görüntüleri henüz yok; README metinsel yer tutucuları kırık görsel bağlantısı üretmez. [Kullanıcı akışı](diagrams/user-flow.mmd).

[Dokümantasyon indeksi](../README.md)
