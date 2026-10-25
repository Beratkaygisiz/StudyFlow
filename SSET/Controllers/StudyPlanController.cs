using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OCSTS.Models;

namespace OCSTS.Controllers
{
    /// <summary>
    /// MODÜL 02 — ÇALIŞMA PLANI KONTROLCÜSÜ (StudyPlanController):
    /// Öğrencinin ders konularını planlama, HtmlHelpers formu ile yeni konu ekleme,
    /// çalışılan süreyi güncelleme ve otomatik tamamlanma durumunu yönetir.
    /// </summary>
    public class StudyPlanController : Controller
    {
        private static readonly string FilePath = Path.Combine(Directory.GetCurrentDirectory(), "studyplans.json");

        // JSON dosyasından çalışma planı listesini okur
        public static List<StudyPlan> GetStudyPlans()
        {
            if (System.IO.File.Exists(FilePath))
            {
                var json = System.IO.File.ReadAllText(FilePath);
                var list = JsonSerializer.Deserialize<List<StudyPlan>>(json);
                if (list != null) return list;
            }

            var defaultPlans = new List<StudyPlan>
            {
                new StudyPlan { Id = 1, CourseName = "Web Programlama", TopicTitle = "MVC Mimarisi ve Controller", Priority = "Yüksek", PlannedHours = 4, CompletedHours = 4, IsCompleted = true },
                new StudyPlan { Id = 2, CourseName = "Web Programlama", TopicTitle = "Razor, ViewComponent ve PartialView", Priority = "Yüksek", PlannedHours = 3, CompletedHours = 3, IsCompleted = true },
                new StudyPlan { Id = 3, CourseName = "Veri Tabanı", TopicTitle = "SQL Sorguları ve Normalizasyon", Priority = "Orta", PlannedHours = 5, CompletedHours = 2, IsCompleted = false },
                new StudyPlan { Id = 4, CourseName = "Algoritmalar", TopicTitle = "Sıralama ve Arama Algoritmaları", Priority = "Orta", PlannedHours = 4, CompletedHours = 0, IsCompleted = false }
            };

            SaveStudyPlans(defaultPlans);
            return defaultPlans;
        }

        // Güncel çalışma planlarını JSON dosyasına kalıcı olarak yazar
        public static void SaveStudyPlans(List<StudyPlan> plans)
        {
            var json = JsonSerializer.Serialize(plans, new JsonSerializerOptions { WriteIndented = true });
            System.IO.File.WriteAllText(FilePath, json);
        }

        // Html.DropDownListFor için öncelik seçeneklerini hazırlar
        private static List<SelectListItem> GetPriorityItems()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Text = "Yüksek Öncelik (Sınava Yakın)", Value = "Yüksek" },
                new SelectListItem { Text = "Orta Öncelik (Haftalık Plan)", Value = "Orta" },
                new SelectListItem { Text = "Düşük Öncelik (Genel Tekrar)", Value = "Düşük" }
            };
        }

        // 1. View: Index (Çalışma Konularının Listelendiği Sayfa)
        [HttpGet]
        public IActionResult Index()
        {
            var plans = GetStudyPlans();
            return View(plans);
        }

        // 2. View: Create [GET] (HtmlHelpers ile Yeni Konu Ekleme Formunu Açar)
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Priorities = GetPriorityItems();
            // Sağ panelde _StudyPlanCardPartial ile mevcut çalışma konularını önizlemek için gönderilir
            ViewBag.ExistingPlans = GetStudyPlans();

            var defaultModel = new StudyPlan
            {
                Priority = "Orta",
                PlannedHours = 3,
                CompletedHours = 0,
                IsCompleted = false
            };

            return View(defaultModel);
        }

        // 2. Create [POST] (Formdan HtmlHelpers ile Gönderilen Konuyu Kaydeder)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(StudyPlan plan)
        {
            if (string.IsNullOrWhiteSpace(plan.CourseName) || string.IsNullOrWhiteSpace(plan.TopicTitle))
            {
                ModelState.AddModelError("", "Lütfen ders adını ve çalışılacak konu başlığını giriniz.");
                ViewBag.Priorities = GetPriorityItems();
                ViewBag.ExistingPlans = GetStudyPlans();
                return View(plan);
            }

            var plans = GetStudyPlans();
            plan.Id = plans.Count > 0 ? plans.Max(p => p.Id) + 1 : 1;
            plan.Priority = string.IsNullOrWhiteSpace(plan.Priority) ? "Orta" : plan.Priority;
            plan.PlannedHours = Math.Max(1, plan.PlannedHours);
            plan.CompletedHours = Math.Max(0, plan.CompletedHours);

            // Eğer kullanıcı "Tamamlandı" kutusunu işaretlediyse veya çalışılan süre hedefe ulaştıysa tamamlandı yap
            if (plan.IsCompleted && plan.CompletedHours < plan.PlannedHours)
            {
                plan.CompletedHours = plan.PlannedHours;
            }
            plan.IsCompleted = plan.CompletedHours >= plan.PlannedHours;

            plans.Add(plan);
            SaveStudyPlans(plans);

            TempData["SuccessMessage"] = $"\"{plan.CourseName} - {plan.TopicTitle}\" konusu çalışma planına eklendi. Girdiğiniz veriler Çalışma Planı ve Ana Sayfada doğrudan görüntülenmektedir.";

            return RedirectToAction("Index");
        }

        // 3. Çalışma Konusu Silme İşlemi
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var plans = GetStudyPlans();
            var planToDelete = plans.FirstOrDefault(p => p.Id == id);
            if (planToDelete != null)
            {
                plans.Remove(planToDelete);
                SaveStudyPlans(plans);
                TempData["SuccessMessage"] = $"\"{planToDelete.TopicTitle}\" konusu çalışma planından silindi.";
            }

            return RedirectToAction("Index");
        }

        // 4. Çalışılan Süreyi Güncelleme İşlemi (Hedefe ulaşılırsa otomatik Tamamlandı olur)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateHours(int id, int completedHours)
        {
            var plans = GetStudyPlans();
            var plan = plans.FirstOrDefault(p => p.Id == id);
            if (plan != null)
            {
                plan.CompletedHours = Math.Max(0, completedHours);
                plan.IsCompleted = plan.CompletedHours >= plan.PlannedHours;

                SaveStudyPlans(plans);
                TempData["SuccessMessage"] = $"\"{plan.TopicTitle}\" için çalışılan süre {plan.CompletedHours} saat olarak güncellendi.";
            }

            return RedirectToAction("Index");
        }
    }
}
