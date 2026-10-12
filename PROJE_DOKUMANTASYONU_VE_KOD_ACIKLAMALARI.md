# SSET — Öğrenci Çalışma ve Sınav Takip Sistemi
## 1. Hafta Proje Temel İskeleti ve Detaylı Kod Rehberi

---

### 🎯 1. Hafta Ders İsterlerinin Tamamı ve Karşılıkları

| Hoca / Ders İsteri | Projedeki Birebir Karşılığı | Dosyalar |
| :--- | :--- | :--- |
| **1. En az 3 Controller** | `HomeController`, `ExamController`, `StudyPlanController` | `Controllers/*.cs` |
| **2. Her Controller'da minimum 2 View** | • **Home:** `Index.cshtml`, `About.cshtml`<br>• **Exam:** `Index.cshtml`, `Create.cshtml`<br>• **StudyPlan:** `Index.cshtml`, `Create.cshtml`<br>*(Toplam 6 adet View)* | `Views/*/*.cshtml` |
| **3. Responsive Görünüm ve CSS Tasarım (Düz beyaz ekran olmaması)** | • Bootstrap 5 Grid sistemi (`col-12`, `col-md-6`, mobil hamburger menü)<br>• Özel `site.css` ile modern soft gri-mavi arka plan (`#f4f6fa`), kart (Card) yapıları ve butonlar | `Views/Shared/_Layout.cshtml`<br>`wwwroot/css/site.css` |
| **4. En az 1 sayfada Razor ile dinamik değişen alan** | `ExamController`'dan View'a gönderilen sahte sınav listesi (`List<Exam>`), `Exam/Index.cshtml` sayfasında **Razor `@foreach` döngüsü** ile şık bir HTML tablosunda dinamik olarak listelenir. | `Views/Exam/Index.cshtml` |

---

## 📁 Proje Dosya Yapısı (Çok Sade ve Anlaşılır)

```text
C:\Users\ARDA\source\repos\SSET\
│
├── SSET.slnx                           --> Visual Studio Çözüm Dosyası
├── PROJE_DOKUMANTASYONU_VE_KOD_ACIKLAMALARI.md --> Bu Açıklama Dosyası
│
└── SSET\
    ├── Controllers\                    --> 3 ADET CONTROLLER
    │   ├── HomeController.cs           --> Index (Karşılama) ve About (Bilgi)
    │   ├── ExamController.cs           --> Index (Sınav Listesi) ve Create (Yeni Sınav Ekleme)
    │   └── StudyPlanController.cs      --> Index (Çalışma Planı) ve Create (Yeni Konu Ekleme)
    │
    ├── Models\                         --> 2 ADET MODEL (VERİ YAPISI)
    │   ├── Exam.cs                     --> Id, CourseName, ExamDate, TargetScore, Description
    │   └── StudyPlan.cs                --> Id, CourseName, TopicTitle, PlannedHours, IsCompleted
    │
    ├── Views\                          --> HER CONTROLLER'A 2 VIEW (TOPLAM 6 VIEW)
    │   ├── Home\
    │   │   ├── Index.cshtml            --> Karşılama ve Hızlı Geçiş Kartları
    │   │   └── About.cshtml            --> Proje Hakkında Bilgi Kartı
    │   ├── Exam\
    │   │   ├── Index.cshtml            --> [ZORUNLU İSTER] @foreach ile Dinamik Sınav Tablosu
    │   │   └── Create.cshtml           --> Kart Yapısında Yeni Sınav Formu
    │   ├── StudyPlan\
    │   │   ├── Index.cshtml            --> @foreach ile Dinamik Çalışma Konuları Tablosu
    │   │   └── Create.cshtml           --> Kart Yapısında Yeni Konu Formu
    │   └── Shared\
    │       └── _Layout.cshtml          --> Ortak Şablon: Responsive Navbar + CSS + Footer
    │
    ├── wwwroot\css\site.css            --> Arka plan rengi (#f4f6fa) ve Kart gölgeleri
    └── Program.cs                      --> Uygulama başlatma ve rota ({controller=Home}/{action=Index})
```

---

## 💡 Kodların Ayrıntılı Açıklamaları

### 1. Model Katmanı (`Models/Exam.cs` ve `Models/StudyPlan.cs`)
Model, uygulamada kullanacağımız verilerin şablonudur (C# sınıfı).
- `Exam.cs`: Bir sınavın hangi derse ait olduğunu (`CourseName`), tarihini (`ExamDate`) ve hedef notunu (`TargetScore`) tutar.
- `StudyPlan.cs`: Bir çalışma konusunun başlığını (`TopicTitle`), hedeflenen çalışma saatini (`PlannedHours`) ve bitip bitmediğini (`IsCompleted`) tutar.

### 2. Controller Katmanı (İş Mantığı & Veri Hazırlama)
- **`ExamController.cs`**:
  ```csharp
  // Controller içinde geçici sahte liste (Mock Data):
  public static List<Exam> Exams = new List<Exam> { ... };

  // 1. View: Index -> Listeyi View'a model olarak gönderir
  public IActionResult Index() => View(Exams);

  // 2. View: Create [GET] -> Boş form sayfasını açar
  public IActionResult Create() => View();

  // Form submit edildiğinde [POST] -> Yeni sınavı listeye ekler ve Index'e döner
  [HttpPost]
  public IActionResult Create(Exam exam)
  {
      exam.Id = Exams.Count + 1;
      Exams.Add(exam);
      return RedirectToAction("Index");
  }
  ```

### 3. View Katmanı ve Dinamik Razor Kullanımı (`Views/Exam/Index.cshtml`)
Dersin en önemli isterlerinden biri olan **dinamik alan**, Razor `@foreach` döngüsü ile yapılmıştır:
```razor
@model List<SSET.Models.Exam>

<table class="table table-hover">
    <thead>
        <tr>
            <th>#</th>
            <th>Ders Adı</th>
            <th>Sınav Tarihi</th>
            <th>Hedeflenen Not</th>
        </tr>
    </thead>
    <tbody>
        @foreach (var exam in Model)
        {
            <tr>
                <td>@exam.Id</td>
                <td><strong>@exam.CourseName</strong></td>
                <td>@exam.ExamDate.ToString("dd.MM.yyyy HH:mm")</td>
                <td><span class="badge bg-success">@exam.TargetScore / 100</span></td>
            </tr>
        }
    </tbody>
</table>
```
- `@model List<SSET.Models.Exam>`: Controller'dan gelen listeyi tanımlar.
- `@foreach`: Listedeki her bir sınav için otomatik bir HTML tablosu satırı (`<tr>`) üretir.

### 4. Tasarım, CSS ve Responsive Görünüm (`_Layout.cshtml` & `site.css`)
- **Düz Beyaz Ekran Engeli:** `site.css` içerisine `background-color: #f4f6fa;` verilerek tüm sayfalara göz alıcı, modern ve yumuşak bir ton kazandırılmıştır.
- **Responsive Navbar:** Bootstrap 5 navbar ve hamburger toggler butonu eklenmiştir. Ekran boyutu küçüldüğünde menü otomatik olarak açılır-kapanır hale gelir.
- **Kart Yapısı:** Her View içeriği `card shadow-sm border-0` sınıflarıyla estetik kartlar içerisinde sunulur.

---

## 🚀 Projeyi Çalıştırma

1. **Visual Studio ile:** `C:\Users\ARDA\source\repos\SSET\SSET.slnx` dosyasını açıp klavyeden **F5** veya **Ctrl + F5** tuşuna basmanız yeterlidir.
2. **Terminal ile:**
   ```powershell
   cd C:\Users\ARDA\source\repos\SSET\SSET
   dotnet run
   ```
   Ekranda beliren adrese (örneğin `http://localhost:5000`) tarayıcınızdan girebilirsiniz.

---

## 🎤 Hoca İçin 2 Dakikalık Sunum Konuşması

> *"Hocam merhaba. 1. hafta temel iskelet ödevimiz için 'SSET (Öğrenci Çalışma ve Sınav Takip Sistemi)' projesini oluşturduk.*
> 
> *İsterler doğrultusunda projemizde tam 3 Controller bulunmaktadır: **HomeController**, **ExamController** ve **StudyPlanController**. Her birinde 2'şer adet olmak üzere toplam **6 adet View** hazırladık.*
> 
> *Sayfalarımızda 'düz beyaz ekran' kalmaması için Bootstrap 5 ve özel CSS ile responsive kart yapıları ve şık bir arka plan rengi kullandık.*
> 
> *Dinamik Razor isteri için **ExamController** Index sayfamızda, Controller'dan gönderilen sınav listesini Razor'ın **@foreach** döngüsüyle dinamik bir HTML tablosuna dönüştürdük. İlk hafta olduğu için veritabanı karmaşıklığı yerine verileri Controller içerisinde geçici static List yapısında tuttuk. Yeni sınav eklediğimizde liste anında güncellenmektedir."*
