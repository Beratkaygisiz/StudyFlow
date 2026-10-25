# OCSTS — Öğrenci Çalışma ve Sınav Takip Sistemi
> **Tek bir yerden planla, takip et, düzenle.**  
> Üniversite öğrencilerinin dağınık ders, sınav tarihi, hedef puan ve çalışma süresi bilgilerini tek bir merkezde yönetmesini sağlayan **ASP.NET Core MVC (.NET 10.0)** web uygulaması.

---

## 📌 Proje Genel Bilgileri
- **Proje Adı:** OCSTS (Öğrenci Çalışma ve Sınav Takip Sistemi)
- **Mimari:** ASP.NET Core MVC (Model • View • Controller + ViewComponent + PartialView + HtmlHelpers)
- **Platform:** .NET 10.0 (`net10.0`)
- **Tasarım:** Bootstrap 5.3 + Bootstrap Icons + Plus Jakarta Sans + Değişken Tabanlı Modern CSS (`site.css`, Açık & Koyu Mod)
- **Veri Saklama Yöntemi:** Dosya Tabanlı JSON Kalıcılığı (`exams.json`, `studyplans.json`)
- **Proje Konumu:** `C:\Users\ARDA\source\repos\SSET`
- **Solution Dosyası:** `OCSTS.slnx` / `SSET.slnx`

---

# 🔷 KISIM 1 — Temel MVC İskeleti ve Akıllı Takip Özellikleri

### 1. Problem, İhtiyaç ve Projenin Amacı
- **Dağınık Bilgiler (01):** Sınav tarihleri ve konular defter, telefon ve mesajlar arasında dağınık kalır.
- **Takip Güçlüğü (02):** Hangi derse kaç saat çalışıldığı ve hangi konunun eksik kaldığı zamanla karışır.
- **Görünürlük Eksikliği (03):** Tamamlanan ve bekleyen çalışmalar tek bir yerde net biçimde görülemez.
- **Çözüm (OCSTS):** Sınavları tek listede toplamak, hedef not/tarih kaydetmek, çalışma süresi planlamak ve tamamlanan görevleri otomatik işaretlemek.

### 2. Temel Modüller ve Dinamik Özellikler
1. **Modül 01 — Sınav Yönetimi (`ExamController`):**
   - Sınav tarihi geçmişse (`exam.ExamDate < DateTime.Now`) ve henüz not girilmemişse tabloda **"Sınav Notu Gir"** butonu belirir.
   - Açılan pencereden girilen gerçek not `ExamController.UpdateScore` ile `exams.json` dosyasına kaydedilir ve hedef not ile karşılaştırılır.
2. **Modül 02 — Çalışma Planı (`StudyPlanController`):**
   - Her konu için hedeflenen saat (`PlannedHours`) ve çalışılan saat (`CompletedHours`) tutulur.
   - `plan.IsCompleted = plan.CompletedHours >= plan.PlannedHours;` kontrolü ile çalışılan süre hedefe ulaştığında durum otomatik olarak **"Tamamlandı"** rozetine dönüşür.
3. **Kalıcı JSON Veri Saklama:**
   - Uygulama kapatılıp yeniden açılsa dahi eklenen sınavlar, girilen notlar ve çalışma saatleri `exams.json` ve `studyplans.json` dosyalarında kalıcı olarak korunur.

### 3. Kısım 1 Kavram Haritası

| Kavram | Ne İşe Yarar? | Bu Projede Nerede Kullanıldı? |
| :--- | :--- | :--- |
| **Model** | Uygulamanın veri yapılarını temsil eden C# sınıflarıdır. | `Models/Exam.cs`, `Models/StudyPlan.cs`, `Models/DashboardViewModel.cs`, `Models/AcademicSummaryViewModel.cs` |
| **Controller** | HTTP isteklerini karşılayan, iş mantığını çalıştıran ve View döndüren sınıflardır. | `HomeController.cs`, `ExamController.cs`, `StudyPlanController.cs` |
| **View** | Razor motoru ile üretilen `.cshtml` kullanıcı arayüzleridir. | `Home/Index`, `Home/About`, `Exam/Index`, `Exam/Create`, `StudyPlan/Index`, `StudyPlan/Create` |
| **Strongly Typed View** | View tepesinde `@model` tanımlanarak tip güvenli veri kullanılmasıdır. | Tüm listeleme, form ve bileşen görünümlerinde |
| **Razor (`@foreach`, `@if`)** | C# döngü ve koşullarının HTML içinde dinamik çıktı üretmesidir. | Tablo satırları, kalan gün hesabı, ilerleme yüzdesi ve durum rozetlerinde |
| **Layout & Koyu Mod** | Ortak menü/altbilgi şablonu ve tema tercihinin (`localStorage`) saklanmasıdır. | `Views/Shared/_Layout.cshtml` ve `wwwroot/css/site.css` |

---

# 🚀 KISIM 2 — Yeni Eklenen İsterler, Mimari Yapılar ve Modern Arayüz

Kısım 2 kapsamında projeye **ViewComponent**, **PartialView**, **HtmlHelpers ile Form Üretimi**, **Sayfalar Arası Doğrudan Veri Erişimi** ve **Modernize Edilmiş Kullanıcı Arayüzü** eklenmiştir.

---

### 1. ViewComponent Mimarisi (`AcademicSummaryViewComponent`)

- **Nedir ve Neden Kullanıldı?**  
  `ViewComponent`, `PartialView`'dan farklı olarak kendi bağımsız C# sınıfına ve `Invoke()` iş mantığına sahip mini bir MVC bileşenidir. Ana sayfanın veya alt sayfaların Controller'larına ekstra hesaplama yükü bindirmeden doğrudan `exams.json` ve `studyplans.json` verilerini okur; yaklaşan sınav sayısını, not ortalamasını, toplam çalışma saatini ve genel tamamlanma yüzdesini hesaplar.
- **Kod Dosyaları (Kod İçinde Açıklamalı):**
  - C# Sınıfı: `SSET/ViewComponents/AcademicSummaryViewComponent.cs`
  - Görünüm Dosyası: `SSET/Views/Shared/Components/AcademicSummary/Default.cshtml`
  - ViewModel: `SSET/Models/AcademicSummaryViewModel.cs`
- **Farklı Sayfalarda Uygun Senaryolarda Gösterimi (4 Farklı Sayfa):**

| Gösterildiği Sayfa | Çağrılma Kodu | Senaryo ve Gösterilen İçerik |
| :--- | :--- | :--- |
| **`Views/Home/Index.cshtml`** | `@await Component.InvokeAsync("AcademicSummary", new { mode = "full" })` | **Ana Gösterge Paneli:** Kayıtlı/yaklaşan sınav, sıradaki sınav, hedef/gerçek not ortalaması, tamamlanan konu ve çalışma saati ilerleme çubuğu. |
| **`Views/Exam/Index.cshtml`** | `@await Component.InvokeAsync("AcademicSummary", new { mode = "exams" })` | **Sınav Odaklı Özet:** Toplam sınav, yaklaşan sınav, not girişi bekleyen sınav ve ortalama hedef puan kartları. |
| **`Views/StudyPlan/Index.cshtml`** | `@await Component.InvokeAsync("AcademicSummary", new { mode = "study" })` | **Çalışma Odaklı Özet:** Tamamlanan/toplam konu sayısı, çalışılan/hedeflenen saat ve genel hedef ilerleme çubuğu. |
| **`Views/Home/About.cshtml`** | `@await Component.InvokeAsync("AcademicSummary", new { mode = "compact" })` | **Canlı Sistem Özeti:** Proje tanıtım sayfasında sistemdeki anlık kayıt ve ilerleme durumunu gösteren kompakt bilgi şeridi. |

---

### 2. PartialView Mimarisi (`_ExamCardPartial`, `_StudyPlanCardPartial`, `_AlertMessagePartial`)

- **Nedir ve Neden Kullanıldı?**  
  `PartialView`, kendi C# Controller mantığı olmayan ancak üst sayfadan kendisine gönderilen modeli (`@model`) alıp tekrar kullanılabilir HTML kartlarına dönüştüren parça görünümdür. Kod tekrarını (DRY prensibi) ortadan kaldırır.
- **Tanımlanan PartialView Dosyaları ve Farklı Sayfalardaki Senaryoları:**

| PartialView Dosyası | Aldığı Model | Gösterildiği Farklı Sayfalar ve Senaryolar |
| :--- | :--- | :--- |
| **`Views/Shared/_ExamCardPartial.cshtml`** | `@model OCSTS.Models.Exam` | 1. **`Home/Index.cshtml`:** Ana sayfadaki *"Sınav Takvimi ve Hedefler"* vitrininde.<br>2. **`Exam/Create.cshtml`:** Yeni sınav eklerken sağ paneldeki *"Mevcut Sınav Takvimim"* listesinde (öğrencinin tarih çakışmalarını görmesi için). |
| **`Views/Shared/_StudyPlanCardPartial.cshtml`** | `@model OCSTS.Models.StudyPlan` | 1. **`Home/Index.cshtml`:** Ana sayfadaki *"Çalışma Planı ve İlerleme"* vitrininde.<br>2. **`StudyPlan/Create.cshtml`:** Yeni konu eklerken sağ paneldeki *"Mevcut Çalışma Konularım"* önizlemesinde (aynı konuyu tekrar eklememek için). |
| **`Views/Shared/_AlertMessagePartial.cshtml`** | `TempData["SuccessMessage"]` | **`Home/Index.cshtml`**, **`Exam/Index.cshtml`** ve **`StudyPlan/Index.cshtml`** sayfalarında form kaydı sonrası kullanıcıya başarı bildirimi göstermek için. |

- **Kod İçinde Çağrılma Örneği:**
  ```razor
  @foreach (var exam in Model.UpcomingAndRecentExams.Take(4))
  {
      @await Html.PartialAsync("_ExamCardPartial", exam)
  }
  ```

---

### 3. HtmlHelpers ile Form Oluşturma ve Sayfalar Arası Doğrudan Veri Erişimi

- **HtmlHelpers Zorunluluğunun Uygulanışı:**  
  Kısım 2 isteri doğrultusunda veri girişi yapılan **tüm formlar** (`Exam/Create.cshtml`, `StudyPlan/Create.cshtml`, `Exam/Index.cshtml` not giriş modalı ve `StudyPlan/Index.cshtml` süre güncelleme formu) Tag Helper yerine **`HtmlHelpers`** metotlarıyla oluşturulmuştur:

| Kullanılan HtmlHelper Metodu | Ürettiği HTML Elemanı | Kullanıldığı Dosyalar |
| :--- | :--- | :--- |
| `@using (Html.BeginForm(...))` | `<form action="..." method="post">` | `Exam/Create`, `StudyPlan/Create`, `Exam/Index`, `StudyPlan/Index` |
| `@Html.AntiForgeryToken()` | Güvenlik doğrulama gizli alanı (CSRF koruması) | Tüm POST formlarında |
| `@Html.LabelFor(m => m.Alan)` | Modeldeki `[Display(Name="...")]` özniteliğine bağlı `<label>` | `Exam/Create.cshtml`, `StudyPlan/Create.cshtml` |
| `@Html.TextBoxFor(m => m.Alan, ...)` | Metin, sayı (`number`) ve tarih (`datetime-local`) `<input>` alanı | `Exam/Create.cshtml`, `StudyPlan/Create.cshtml` |
| `@Html.DropDownListFor(m => m.Alan, ...)` | Sınav Türü (`ExamType`) ve Öncelik (`Priority`) için `<select>` | `Exam/Create.cshtml`, `StudyPlan/Create.cshtml` |
| `@Html.TextAreaFor(m => m.Description, ...)` | Sınav kapsam notu için çok satırlı `<textarea>` | `Exam/Create.cshtml` |
| `@Html.CheckBoxFor(m => m.IsCompleted, ...)` | Konu tamamlandı işareti için `<input type="checkbox">` | `StudyPlan/Create.cshtml` |
| `@Html.Hidden(...)` & `@Html.TextBox(...)` | Satır içi süre güncelleme ve modal sınav notu giriş alanları | `Exam/Index.cshtml`, `StudyPlan/Index.cshtml` |

- **Bir Sayfadan Girilen Verinin Başka Sayfalarda Doğrudan Görülmesi (Veri Akışı):**
  1. Kullanıcı **`/Exam/Create`** sayfasında `HtmlHelpers` form elemanlarıyla yeni bir sınav (Örn: *Web Programlama, Vize, Hedef: 90*) girip **Kaydet** butonuna basar.
  2. `ExamController.Create [HttpPost]` veriyi alır ve kalıcı veri kaynağına kaydeder.
  3. Kullanıcı otomatik olarak **`/Exam/Index`** sayfasına yönlendirilir; üstte `_AlertMessagePartial` bildirimini, üst şeritte güncellenen `AcademicSummary` ViewComponent istatistiklerini ve tabloda yeni eklediği sınavı **doğrudan görür**.
  4. Kullanıcı **`/Home/Index`** (Ana Sayfa) veya **`/Home/About`** sayfalarına geçtiğinde de eklediği sınav ve çalışma konularını **doğrudan görür**.

---

### 4. Modernize Edilmiş Web Arayüzü ve Hakkında (`Home/About`) Güncellemeleri
- **Modern Tasarım Dili (`wwwroot/css/site.css` & `_Layout.cshtml`):**
  - `Plus Jakarta Sans` modern yazı tipi, cam efektli (`backdrop-filter: blur`) yapışkan üst menü, aktif sayfa vurgusu ve hızlı ekleme butonları (`+ Sınav Ekle`, `+ Konu Ekle`) eklendi.
  - CSS değişkenleri (`--ocsts-bg`, `--ocsts-surface`, `--ocsts-primary` vb.) ile **Açık Mod** ve **Koyu Mod** arasında yüksek kontrastlı, göz yormayan geçiş sağlandı.
  - Çalışma planı tablosuna ve kartlarına yüzdelik **görsel ilerleme çubukları (progress bar)**, sınav türlerine ve öncelik düzeylerine göre yumuşak renkli rozetler (soft badges) eklendi.
- **Hakkında Sayfası (`Views/Home/About.cshtml`):**
  - Proje sunumuyla birebir uyumlu *Problem & İhtiyaç*, *Hedef Kullanıcı Senaryosu*, *Temel Modüller* ve *Kısım 2 Teknik Altyapı (ViewComponent, PartialView, HtmlHelpers, Modern UI)* kartları ile canlı `AcademicSummary` ViewComponent bileşeni eklendi.

---

## 📁 Güncel Proje Dosya ve Dizin Mimarisi

```text
C:\Users\ARDA\source\repos\SSET\
│
├── OCSTS.slnx / SSET.slnx                           --> Visual Studio Çözüm Dosyaları
├── README.md                                        --> Proje Dokümantasyonu (Kısım 1 + Kısım 2)
│
└── SSET\
    ├── OCSTS.csproj                                 --> ASP.NET Core MVC (.NET 10.0) Proje Dosyası
    ├── README.md                                    --> Proje İçi Dokümantasyon Kopyası
    ├── exams.json                                   --> [KALICI VERİ] Sınav kayıtları ve notlar
    ├── studyplans.json                              --> [KALICI VERİ] Çalışma konuları ve süreler
    │
    ├── Controllers\                                 --> KONTROLCÜLER (İŞ MANTIĞI)
    │   ├── HomeController.cs                        --> Ana Sayfa (DashboardViewModel) ve Hakkında
    │   ├── ExamController.cs                        --> Sınav Listeleme, Create (POST/GET), Delete, UpdateScore
    │   └── StudyPlanController.cs                   --> Konu Listeleme, Create (POST/GET), Delete, UpdateHours
    │
    ├── Models\                                      --> VERİ MODELLERİ & VIEWMODEL'LER
    │   ├── Exam.cs                                  --> Sınav Modeli ([Display] nitelikleri + ExamType)
    │   ├── StudyPlan.cs                             --> Çalışma Planı Modeli ([Display] nitelikleri + Priority)
    │   ├── DashboardViewModel.cs                    --> [KISIM 2] Ana Sayfa çapraz veri görünümü modeli
    │   ├── AcademicSummaryViewModel.cs              --> [KISIM 2] ViewComponent istatistik modeli
    │   └── ErrorViewModel.cs                        --> Hata Modeli
    │
    ├── ViewComponents\                              --> [KISIM 2] BAĞIMSIZ C# GÖRÜNÜM BİLEŞENLERİ
    │   └── AcademicSummaryViewComponent.cs          --> Akademik Özet ViewComponent (full/exams/study/compact)
    │
    ├── Views\                                       --> KULLANICI ARAYÜZLERİ (RAZOR .cshtml)
    │   ├── Home\
    │   │   ├── Index.cshtml                         --> Hero, ViewComponent(full) ve PartialView vitrinleri
    │   │   └── About.cshtml                         --> Sunum Vizyonu, Kısım 2 Mimarisi ve ViewComponent(compact)
    │   ├── Exam\
    │   │   ├── Index.cshtml                         --> ViewComponent(exams), Tablo, HtmlHelpers Not Modalı
    │   │   └── Create.cshtml                        --> [KISIM 2] HtmlHelpers Sınav Formu + _ExamCardPartial
    │   ├── StudyPlan\
    │   │   ├── Index.cshtml                         --> ViewComponent(study), Tablo, HtmlHelpers Süre Formu
    │   │   └── Create.cshtml                        --> [KISIM 2] HtmlHelpers Konu Formu + _StudyPlanCardPartial
    │   └── Shared\
    │       ├── _Layout.cshtml                       --> Modern Navbar, Hızlı Ekleme Linkleri, Koyu Mod, Footer
    │       ├── _ExamCardPartial.cshtml              --> [KISIM 2] Tekrar Kullanılabilir Sınav Kartı PartialView
    │       ├── _StudyPlanCardPartial.cshtml         --> [KISIM 2] Tekrar Kullanılabilir Çalışma Kartı PartialView
    │       ├── _AlertMessagePartial.cshtml          --> [KISIM 2] İşlem Geri Bildirim PartialView
    │       └── Components\
    │           └── AcademicSummary\
    │               └── Default.cshtml               --> [KISIM 2] AcademicSummaryViewComponent Görünümü
    │
    ├── wwwroot\css\site.css                         --> Modern Değişken Tabanlı Açık/Koyu Mod Tasarımı
    └── Program.cs                                   --> Uygulama Giriş Noktası ve Varsayılan MVC Rotası
```

---

## 🛠️ Projeyi Çalıştırma Adımları

1. **Visual Studio ile:**  
   `C:\Users\ARDA\source\repos\SSET\OCSTS.slnx` dosyasını açın ve **F5** / **Ctrl + F5** tuşuna basın.
2. **Terminal ile:**  
   ```powershell
   cd C:\Users\ARDA\source\repos\SSET\SSET
   dotnet run
   ```
   Ardından tarayıcıda görüntülenen `http://localhost:...` adresini açın.
