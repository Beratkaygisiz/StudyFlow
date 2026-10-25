using Microsoft.AspNetCore.Mvc;
using OCSTS.Models;

namespace OCSTS.Controllers
{
    /// <summary>
    /// ANA KONTROLCÜ (HomeController):
    /// Karşılama ekranı (Index) ve Proje Hakkında (About) sayfalarını yönetir.
    /// Kısım 2 kapsamında diğer sayfalardaki formlardan (HtmlHelpers ile) girilen sınav
    /// ve çalışma planı verilerini Ana Sayfada doğrudan göstermek için DashboardViewModel kullanır.
    /// </summary>
    public class HomeController : Controller
    {
        // 1. View: Index (Karşılama Ekranı ve Genel Akademik Gösterge Paneli)
        [HttpGet]
        public IActionResult Index()
        {
            var dashboardModel = new DashboardViewModel
            {
                UpcomingAndRecentExams = ExamController.GetExams()
                    .OrderBy(e => e.ExamDate < DateTime.Now) // Önce yaklaşan sınavlar, sonra geçmiş sınavlar
                    .ThenBy(e => e.ExamDate)
                    .ToList(),
                StudyPlans = StudyPlanController.GetStudyPlans()
                    .OrderBy(p => p.IsCompleted) // Önce devam eden konular, sonra tamamlananlar
                    .ThenByDescending(p => p.Id)
                    .ToList()
            };

            return View(dashboardModel);
        }

        // 2. View: About (Proje Vizyonu, Modüller ve Kısım 1 - Kısım 2 Mimari Bilgileri)
        [HttpGet]
        public IActionResult About()
        {
            ViewBag.ProjectName = "OCSTS — Öğrenci Çalışma ve Sınav Takip Sistemi";
            ViewBag.Subtitle = "Tek bir yerden planla, takip et, düzenle.";
            return View();
        }

        // Hata Sayfası
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
