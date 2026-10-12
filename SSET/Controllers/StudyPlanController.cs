using Microsoft.AspNetCore.Mvc;
using SSET.Models;

namespace SSET.Controllers
{
    /// <summary>
    /// CONTROLLER 3: StudyPlanController
    /// Çalışma konularının listelenmesi ve yeni konu eklenmesini yönetir.
    /// Veritabanı yerine geçici static List<StudyPlan> kullanılmıştır.
    /// </summary>
    public class StudyPlanController : Controller
    {
        // Geçici Sahte Veri Listesi (Mock Data)
        public static List<StudyPlan> StudyPlans = new List<StudyPlan>
        {
            new StudyPlan { Id = 1, CourseName = "Web Programlama", TopicTitle = "MVC Mimarisi ve Controller", PlannedHours = 4, IsCompleted = true },
            new StudyPlan { Id = 2, CourseName = "Web Programlama", TopicTitle = "Razor Syntax ve @foreach", PlannedHours = 3, IsCompleted = true },
            new StudyPlan { Id = 3, CourseName = "Veri Tabanı", TopicTitle = "SQL Sorguları ve Normalizasyon", PlannedHours = 5, IsCompleted = false },
            new StudyPlan { Id = 4, CourseName = "Algoritmalar", TopicTitle = "Sıralama ve Arama Algoritmaları", PlannedHours = 4, IsCompleted = false }
        };

        // 1. View: Index (Çalışma Konularının Listelendiği Sayfa)
        [HttpGet]
        public IActionResult Index()
        {
            return View(StudyPlans);
        }

        // 2. View: Create [GET] (Yeni Konu Ekleme Formunu Açar)
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Yeni Konuyu Listeye Kaydetme [POST]
        [HttpPost]
        public IActionResult Create(StudyPlan plan)
        {
            plan.Id = StudyPlans.Count > 0 ? StudyPlans.Max(p => p.Id) + 1 : 1;
            StudyPlans.Add(plan);

            return RedirectToAction("Index");
        }
    }
}
