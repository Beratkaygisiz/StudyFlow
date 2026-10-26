# Ekranlar

[Ana sayfa](../README.md) · [Kapsam ve kurallar](01-kapsam-ve-kurallar.md)

## İlk teslim: altı anlamlı View

| Controller | View / route | Gösterilecek içerik |
| --- | --- | --- |
| DashboardController | Index / `/Dashboard` | Aktif ders sayısı, yaklaşan sınavlar, özet kartları ve boş durum |
| DashboardController | Weekly / `/Dashboard/Weekly` | Yedi günlük örnek plan/çalışma tablosu ve hedef çubuğu |
| CoursesController | Index / `/Courses` | Ders kartları; çalışan ad/durum GET filtresi |
| CoursesController | Details / `/Courses/Details/1` | Id'ye göre farklı ders, hedef, örnek konular ve sınav bağlantısı |
| ExamsController | Index / `/Exams` | Tarih sıralı örnek sınavlar; yaklaşan/geçmiş filtreleri |
| ExamsController | Details / `/Exams/Details/1` | Sınav türü, tarihi, kalan gün ve ders bağlantısı |
| CoursesController *(Faz 1B ek tam View)* | Create / `/Courses/Create` | `HtmlHelpers` ile geçici ders oluşturma formu (`Name`, `WeeklyTargetMinutes`), `Session` kaydı ve PRG ile `Courses/Details/{id}` yönlendirmesi |

`/` Dashboard/Index'e açılır. Razor foreach ve koşullar ViewModel'deki gerçek örnek veriyle çalışır. Demo tarihler IClock'a göre üretilir; sayfa eski tarih yüzünden boş görünmez. Yanlış Id bulunamadı sayfasına gider.

`_Layout.cshtml`, `_ValidationScriptsPartial.cshtml`, `_CourseCard.cshtml` partial görünümü ve `Views/Shared/Components/UpcomingExams/Default.cshtml` ViewComponent görünümü tam View sayısına dahil edilmez; `Views/Courses/Create.cshtml` ile birlikte planlanan tam View sayısı yedidir (`Dashboard` Index/Weekly, `Courses` Index/Details/Create, `Exams` Index/Details). `_CourseCard.cshtml` hem `Courses/Index` hem `Dashboard/Index` içinde `Html.PartialAsync` ile; `UpcomingExamsViewComponent` ise hem `Dashboard/Index` (genel aktif dersler) hem `Courses/Details` (ders bazlı) içinde `Component.InvokeAsync` ile çalıştırılır. Faz 1B'de yalnız `Session` tabanlı geçici ders ekleme formu çalışır; kalıcı veritabanı kaydı vaat edilmez ve tüm ekranlarda “Örnek veri” / geçici oturum açıklaması bulunur. Menü ve detay bağlantıları gerçekten çalışır.

## Sonraki ekranlar

| Faz | Ekran ve form içeriği |
| --- | --- |
| 2 | Courses: Create/Edit/DeleteConfirm; ad, hedef, arşiv/geri açma. Exams: Create/Edit/DeleteConfirm; ders, başlık, tür, tarih/saat. Var olan liste/detay ekranları servis verisine geçer. |
| 3 | Topics: Index/Create/Edit; ders bağlamı, başlık, durum, sıra. Not/tekrar tarihi Should. StudyTasks: Index/Details/Create/Edit; konu, başlık, öncelik, son gün, tahmin, durum. StudyPlans: Index/Create/Edit; görev, gün, dakika ve gün/hafta görünümü. |
| 4 | StudySessions: Index/Create/Edit; Timer başlat/bitir/iptal, manuel başlangıç/bitiş, ilişkiler. Dashboard/Weekly ve Statistics temel süre/plan karşılaştırmaları. Gelişmiş istatistikler Should. |
| 5 | Identity kayıt/giriş/POST çıkış; isteğe bağlı Profile/Index/Edit. |

Konu/görev düzenleme formunda ders değiştirilemez. Plan CourseId'si görevden üretilir. Create formunda hidden OperationId, düzenleme/silmede mevcut RowVersion bulunur. Arşivli dersin alt kayıtlarında yazma kontrolleri yerine geri açma açıklaması gösterilir; sunucu kuralı ayrıca uygular.

## Geri bildirim

- Boş liste: kısa açıklama ve sonraki anlamlı işlem. İlk teslimde oluştur CTA'sı yerine demo açıklaması.
- Başarı: POST/redirect sonrası tek bildirim. Gönderim sırasında buton bekleme durumu.
- Alan hatası: girilen değerler korunur; hata özeti alanlara bağlanır; label ve aria-describedby kullanılır.
- Eski sekme: 409 açıklaması, yeniden yükleme ve kullanıcının girdiğini kopyalayabilme seçeneği; sessiz veri ezme yok.
- Silme: isim ve etki gösterilen onay; bağlı ders için arşiv önerisi. Açık Timer iptali zaman kaydını siler; manuel kaydı ayrı oluşturmak gerekir.
- Boş istatistik: toplam 0, açıklama; dashboard grafiği yedi adet 0 değeri gösterir. Önceki dönem 0 ise yüzde yerine açıklama.

## Görsel ve erişilebilirlik kuralları

Bootstrap ve ortak `site.css` kullanılır. Arka plan `#F7F8FC`, kartlar `#FFFFFF` (vurgulu yüzey `#EFF6FF`), ana vurgu `#1D4ED8`, başlık/ana metin `#0F172A`, ikincil metin `#475569`, kenarlıklar `#E2E8F0` olarak tanımlanır; normal metinde en az 4.5:1 kontrast hedeflenir. `Segoe UI / system-ui` font ailesi, sayfa başlıklarında 28–32px, gövde içeriğinde 14–16px tipografi ölçeği, mobilde 16px ve masaüstünde 24–32px içerik boşlukları, kartlarda 14–16px köşeler ve hafif gölge kullanılır. `Courses/Create` formu 680–760px (`720px`) genişlikte ve en az 44px yüksekliğinde giriş alanlarıyla sunulur.

Masaüstünde yaklaşık 240px (`244px`) sol menü (tutarlı çizgi ikonlar ve belirgin aktif sayfa vurgusuyla), mobilde klavye ile açılan (`aria-expanded`, `aria-current`) açılır menü bulunur. 360px tek kolon, 768px iki kolon, geniş ekranda ana içerik ve ikincil özetler için dengeli çok kolonlu yerleşim kullanılır. Ana içerik yatay taşmaz; geniş tablolar kontrollü kaydırmayla sunulur. Görünür klavye odağı (`:focus-visible`), `prefers-reduced-motion` desteği ve grafik yanında aynı verinin tablosu bulunur. Tam WCAG uygunluğu, ölçülmeden iddia edilmez.
