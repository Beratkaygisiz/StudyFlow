using Microsoft.AspNetCore.Mvc;
using SSET.Models;

namespace SSET.Controllers
{
    /// <summary>
    /// CONTROLLER 2: ExamController
    /// Sınavların listelenmesi ve yeni sınav eklenmesini yönetir.
    /// Veritabanı yerine geçici olarak bellek içi (In-Memory) static List<Exam> kullanılmıştır.
    /// </summary>
    public class ExamController : Controller
    {
        // Geçici Sahte Veri Listesi (Mock Data)
        public static List<Exam> Exams = new List<Exam>
        {
            new Exam { Id = 1, CourseName = "Web Programlama", ExamDate = DateTime.Now.AddDays(5), TargetScore = 90, Description = "MVC ve Razor Syntax" },
            new Exam { Id = 2, CourseName = "Veri Tabanı", ExamDate = DateTime.Now.AddDays(9), TargetScore = 85, Description = "SQL Sorguları ve Tablolar" },
            new Exam { Id = 3, CourseName = "Algoritmalar", ExamDate = DateTime.Now.AddDays(14), TargetScore = 75, Description = "Ağaç ve Grafik Yapıları" }
        };

        // 1. View: Index (Sınavların Listelendiği Sayfa)
        // Controller'daki listeyi View'a gönderir (Dinamik Razor tablosu için)
        [HttpGet]
        public IActionResult Index()
        {
            return View(Exams);
        }

        // 2. View: Create [GET] (Yeni Sınav Ekleme Formunu Açar)
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Yeni Sınavı Listeye Kaydetme [POST]
        [HttpPost]
        public IActionResult Create(Exam exam)
        {
            // Yeni sınav için otomatik Id belirle ve listeye ekle
            exam.Id = Exams.Count > 0 ? Exams.Max(e => e.Id) + 1 : 1;
            Exams.Add(exam);

            // Kaydettikten sonra sınav listesine geri dön
            return RedirectToAction("Index");
        }
    }
}
