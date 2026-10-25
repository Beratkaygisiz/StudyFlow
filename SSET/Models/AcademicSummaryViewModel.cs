namespace OCSTS.Models
{
    /// <summary>
    /// KISIM 2 - VIEWCOMPONENT VERİ MODELİ:
    /// AcademicSummaryViewComponent tarafından hesaplanan istatistiksel verileri
    /// Default.cshtml görünümüne taşımak için kullanılır.
    /// </summary>
    public class AcademicSummaryViewModel
    {
        // Görünüm Modu: "full" (Ana Sayfa), "exams" (Sınavlar), "study" (Çalışma Planı), "compact" (Hakkında)
        public string Mode { get; set; } = "full";

        // Sınav İstatistikleri
        public int TotalExams { get; set; }
        public int UpcomingExamsCount { get; set; }
        public int PendingScoreExamsCount { get; set; }
        public double AverageTargetScore { get; set; }
        public double? AverageActualScore { get; set; }
        public Exam? NextUpcomingExam { get; set; }

        // Çalışma Planı İstatistikleri
        public int TotalTopics { get; set; }
        public int CompletedTopics { get; set; }
        public int RemainingTopics => Math.Max(0, TotalTopics - CompletedTopics);
        public int TotalPlannedHours { get; set; }
        public int TotalCompletedHours { get; set; }
        public int CompletionPercentage { get; set; }
    }
}
