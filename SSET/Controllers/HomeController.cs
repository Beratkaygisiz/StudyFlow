using Microsoft.AspNetCore.Mvc;

namespace SSET.Controllers
{
    /// <summary>
    /// CONTROLLER 1: HomeController
    /// Sitenin ana karşılama ve tanıtım sayfalarını yönetir.
    /// </summary>
    public class HomeController : Controller
    {
        // 1. View: Index (Karşılama Ekranı)
        public IActionResult Index()
        {
            // ViewBag ile karşılama mesajı gönderiyoruz
            ViewBag.Message = "Öğrenci Çalışma ve Sınav Takip Sistemine Hoş Geldiniz!";
            return View();
        }

        // 2. View: About (Proje Hakkında Bilgi)
        public IActionResult About()
        {
            ViewBag.ProjectName = "SSET - 1. Hafta Temel İskelet";
            ViewBag.Description = "Bu proje ASP.NET Core MVC mimarisinde 3 Controller ve 6 View kullanılarak hazırlanmıştır.";
            return View();
        }

        // Beklenmeyen hatalar için genel hata sayfası
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new SSET.Models.ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
