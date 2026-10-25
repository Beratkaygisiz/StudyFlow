using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OCSTS.Models;

namespace OCSTS.Controllers
{
    /// <summary>
    /// MODÜL 01 — SINAV YÖNETİMİ KONTROLCÜSÜ (ExamController):
    /// Öğrencinin sınavlarını listeleme, HtmlHelpers formu ile yeni sınav ekleme,
    /// geçmiş sınavlara not girme ve silme işlemlerini yönetir.
    /// </summary>
    public class ExamController : Controller
    {
        private static readonly string FilePath = Path.Combine(Directory.GetCurrentDirectory(), "exams.json");

        // JSON dosyasından sınav listesini okur (yoksa örnek başlangıç verisi oluşturur)
        public static List<Exam> GetExams()
        {
            if (System.IO.File.Exists(FilePath))
            {
                var json = System.IO.File.ReadAllText(FilePath);
                var list = JsonSerializer.Deserialize<List<Exam>>(json);
                if (list != null) return list;
            }

            var defaultExams = new List<Exam>
            {
                new Exam { Id = 1, CourseName = "Web Programlama", ExamType = "Vize", ExamDate = DateTime.Now.AddDays(14), TargetScore = 90, ActualScore = null, Description = "MVC Mimarisi, ViewComponent, PartialView ve HtmlHelpers" },
                new Exam { Id = 2, CourseName = "Veri Tabanı", ExamType = "Quiz", ExamDate = DateTime.Now.AddDays(-8), TargetScore = 80, ActualScore = 88, Description = "SQL Sorguları Değerlendirildi" },
                new Exam { Id = 3, CourseName = "Algoritmalar", ExamType = "Final", ExamDate = DateTime.Now.AddDays(-2), TargetScore = 85, ActualScore = null, Description = "Graf Yapıları ve Ağaç Gezinme" }
            };

            SaveExams(defaultExams);
            return defaultExams;
        }

        // Güncel sınav listesini JSON dosyasına kalıcı olarak yazar
        public static void SaveExams(List<Exam> exams)
        {
            var json = JsonSerializer.Serialize(exams, new JsonSerializerOptions { WriteIndented = true });
            System.IO.File.WriteAllText(FilePath, json);
        }

        // Html.DropDownListFor için sınav türü seçeneklerini hazırlar
        private static List<SelectListItem> GetExamTypeItems()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Text = "Vize Sınavı", Value = "Vize" },
                new SelectListItem { Text = "Final Sınavı", Value = "Final" },
                new SelectListItem { Text = "Kısa Sınav (Quiz)", Value = "Quiz" },
                new SelectListItem { Text = "Proje / Ödev Teslimi", Value = "Proje / Ödev" }
            };
        }

        // 1. View: Index (Sınavların Listelendiği Sayfa)
        [HttpGet]
        public IActionResult Index()
        {
            var exams = GetExams()
                .OrderBy(e => e.ExamDate < DateTime.Now)
                .ThenBy(e => e.ExamDate)
                .ToList();
            return View(exams);
        }

        // 2. View: Create [GET] (HtmlHelpers ile Yeni Sınav Ekleme Formunu Açar)
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.ExamTypes = GetExamTypeItems();
            // Sağ panelde _ExamCardPartial ile mevcut sınavları önizlemek için gönderilir
            ViewBag.ExistingExams = GetExams().OrderBy(e => e.ExamDate).ToList();

            var defaultModel = new Exam
            {
                ExamType = "Vize",
                ExamDate = DateTime.Now.Date.AddDays(7).AddHours(10),
                TargetScore = 90
            };

            return View(defaultModel);
        }

        // 2. Create [POST] (Formdan HtmlHelpers ile Gönderilen Veriyi Kaydeder ve Diğer Sayfaya Yönlendirir)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Exam exam)
        {
            if (string.IsNullOrWhiteSpace(exam.CourseName))
            {
                ModelState.AddModelError(nameof(Exam.CourseName), "Lütfen ders adını giriniz.");
                ViewBag.ExamTypes = GetExamTypeItems();
                ViewBag.ExistingExams = GetExams().OrderBy(e => e.ExamDate).ToList();
                return View(exam);
            }

            var exams = GetExams();
            exam.Id = exams.Count > 0 ? exams.Max(e => e.Id) + 1 : 1;
            exam.ExamType = string.IsNullOrWhiteSpace(exam.ExamType) ? "Vize" : exam.ExamType;
            exam.TargetScore = Math.Clamp(exam.TargetScore, 0, 100);

            exams.Add(exam);
            SaveExams(exams);

            // Diğer sayfada (_AlertMessagePartial ile) gösterilecek başarı mesajı
            TempData["SuccessMessage"] = $"\"{exam.CourseName}\" ({exam.ExamType}) sınavı başarıyla eklendi. Girdiğiniz veriler artık Sınav Listesi ve Ana Sayfada doğrudan görüntülenmektedir.";

            return RedirectToAction("Index");
        }

        // 3. Sınav Silme İşlemi
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var exams = GetExams();
            var examToDelete = exams.FirstOrDefault(e => e.Id == id);
            if (examToDelete != null)
            {
                exams.Remove(examToDelete);
                SaveExams(exams);
                TempData["SuccessMessage"] = $"\"{examToDelete.CourseName}\" sınav kaydı listeden silindi.";
            }

            return RedirectToAction("Index");
        }

        // 4. Tarihi Geçmiş Sınav İçin Not Girme İşlemi
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateScore(int id, int actualScore)
        {
            var exams = GetExams();
            var exam = exams.FirstOrDefault(e => e.Id == id);
            if (exam != null)
            {
                exam.ActualScore = Math.Clamp(actualScore, 0, 100);
                SaveExams(exams);
                TempData["SuccessMessage"] = $"\"{exam.CourseName}\" sınavı için alınan not ({exam.ActualScore}) kaydedildi.";
            }

            return RedirectToAction("Index");
        }
    }
}
