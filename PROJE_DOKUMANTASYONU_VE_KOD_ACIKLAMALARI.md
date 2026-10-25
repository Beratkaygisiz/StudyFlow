# OCSTS — Öğrenci Çalışma ve Sınav Takip Sistemi

> Bu dokümanın en güncel Kısım 1 + Kısım 2 tam sürümü [`README.md`](./README.md) ve [`SSET/README.md`](./SSET/README.md) dosyalarında yer almaktadır.

---

## Kısım 2 Özeti (Yeni Eklenen İsterler)

1. **ViewComponent (`AcademicSummaryViewComponent`):**
   - `SSET/ViewComponents/AcademicSummaryViewComponent.cs` ve `SSET/Views/Shared/Components/AcademicSummary/Default.cshtml` dosyalarında tanımlanmıştır.
   - `Home/Index` (`full`), `Exam/Index` (`exams`), `StudyPlan/Index` (`study`) ve `Home/About` (`compact`) olmak üzere **4 farklı sayfada** senaryoya uygun modlarda gösterilmektedir.
2. **PartialView (`_ExamCardPartial`, `_StudyPlanCardPartial`, `_AlertMessagePartial`):**
   - `Home/Index` vitrininde ve `Exam/Create` ile `StudyPlan/Create` sayfalarının sağ önizleme panellerinde tekrar kullanılarak kod tekrarını önler.
3. **HtmlHelpers ile Form Elemanları & Sayfalar Arası Doğrudan Veri Erişimi:**
   - Tüm formlarda `Html.BeginForm`, `Html.AntiForgeryToken`, `Html.LabelFor`, `Html.TextBoxFor`, `Html.DropDownListFor`, `Html.TextAreaFor`, `Html.CheckBoxFor` ve `Html.Hidden` kullanılmıştır.
   - Ekleme sayfalarından girilen veriler hem ilgili modülün listeleme sayfasında hem de Ana Sayfada anında görüntülenir.
4. **Modernize Edilmiş Tasarım ve Hakkında Sayfası:**
   - `site.css`, `_Layout.cshtml` ve `Home/About.cshtml` sunum akışıyla uyumlu, Açık/Koyu mod destekli modern bir arayüze kavuşturulmuştur.
