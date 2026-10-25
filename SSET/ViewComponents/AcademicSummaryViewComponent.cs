using Microsoft.AspNetCore.Mvc;
using OCSTS.Controllers;
using OCSTS.Models;

namespace OCSTS.ViewComponents
{
    /// <summary>
    /// ============================================================================
    /// [KISIM 2 İSTERİ] - VIEWCOMPONENT TANIMI (AcademicSummaryViewComponent)
    /// ============================================================================
    /// ViewComponent Nedir ve Neden Kullanılır?
    /// - PartialView'dan farklı olarak kendi bağımsız C# iş mantığına (Invoke metodu) sahiptir.
    /// - Ana sayfanın Controller'ına (HomeController, ExamController vb.) ekstra kod yüklemeden,
    ///   hem Sınav verilerini (exams.json) hem de Çalışma Planı verilerini (studyplans.json)
    ///   okuyup istatistiksel özet hesaplar ve kendi görünümünü (Default.cshtml) üretir.
    ///
    /// Hangi Sayfalarda Gösterilmektedir? (Farklı Sayfalarda Uygun Senaryo):
    /// 1. Home/Index.cshtml      -> mode = "full"    (Tüm akademik özet kartları ve genel ilerleme)
    /// 2. Exam/Index.cshtml      -> mode = "exams"   (Sadece sınav odaklı KPI özet şeridi)
    /// 3. StudyPlan/Index.cshtml -> mode = "study"   (Sadece çalışma planı ve saat ilerleme özeti)
    /// 4. Home/About.cshtml      -> mode = "compact" (Hakkında sayfasında canlı sistem durum özeti)
    /// ============================================================================
    /// </summary>
    public class AcademicSummaryViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(string mode = "full")
        {
            // 1. Kalıcı JSON dosyalarından güncel sınav ve çalışma planı listelerini al
            var exams = ExamController.GetExams();
            var plans = StudyPlanController.GetStudyPlans();
            var now = DateTime.Now;

            // 2. Sınav istatistiklerini hesapla
            var upcomingExams = exams
                .Where(e => e.ExamDate >= now)
                .OrderBy(e => e.ExamDate)
                .ToList();

            var scoredExams = exams
                .Where(e => e.ActualScore.HasValue)
                .ToList();

            var pendingScoreExams = exams
                .Count(e => e.ExamDate < now && !e.ActualScore.HasValue);

            // 3. Çalışma planı ilerleme yüzdesini hesapla
            int totalPlannedHours = plans.Sum(p => p.PlannedHours);
            int totalCompletedHours = plans.Sum(p => p.CompletedHours);
            int completionPercentage = totalPlannedHours > 0
                ? Math.Min(100, (int)Math.Round((double)totalCompletedHours / totalPlannedHours * 100))
                : 0;

            // 4. Hesaplanan verileri ViewModel içerisine doldur
            var summaryModel = new AcademicSummaryViewModel
            {
                Mode = mode,
                TotalExams = exams.Count,
                UpcomingExamsCount = upcomingExams.Count,
                PendingScoreExamsCount = pendingScoreExams,
                AverageTargetScore = exams.Count > 0 ? Math.Round(exams.Average(e => e.TargetScore), 1) : 0,
                AverageActualScore = scoredExams.Count > 0 ? Math.Round(scoredExams.Average(e => e.ActualScore!.Value), 1) : null,
                NextUpcomingExam = upcomingExams.FirstOrDefault(),

                TotalTopics = plans.Count,
                CompletedTopics = plans.Count(p => p.IsCompleted),
                TotalPlannedHours = totalPlannedHours,
                TotalCompletedHours = totalCompletedHours,
                CompletionPercentage = completionPercentage
            };

            // Views/Shared/Components/AcademicSummary/Default.cshtml dosyasını render eder
            return View(summaryModel);
        }
    }
}
