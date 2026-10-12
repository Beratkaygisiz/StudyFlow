namespace SSET.Models
{
    /// <summary>
    /// MODEL (Veri Yapısı):
    /// Öğrencinin çalışma planı konularını temsil eden basit C# sınıfıdır.
    /// </summary>
    public class StudyPlan
    {
        public int Id { get; set; }                     // Konu Numarası
        public string CourseName { get; set; } = "";    // Ders Adı
        public string TopicTitle { get; set; } = "";    // Çalışılacak Konu Başlığı
        public int PlannedHours { get; set; }          // Hedeflenen Çalışma Süresi (Saat)
        public bool IsCompleted { get; set; }          // Tamamlandı mı? (true / false)
    }
}
