using System.ComponentModel.DataAnnotations;

namespace OCSTS.Models
{
    /// <summary>
    /// MODEL (Veri Yapısı):
    /// Öğrencinin çalışma planı konularını temsil eden C# sınıfıdır.
    /// Kısım 2 kapsamında HtmlHelpers ile form üretimi için [Display] nitelikleri
    /// ve öncelik düzeyi (Priority) eklenmiştir.
    /// </summary>
    public class StudyPlan
    {
        public int Id { get; set; }                                 // Konu Numarası

        [Display(Name = "Ders Adı")]
        public string CourseName { get; set; } = "";                // Ders Adı

        [Display(Name = "Çalışılacak Konu Başlığı")]
        public string TopicTitle { get; set; } = "";                // Konu Başlığı

        [Display(Name = "Öncelik Düzeyi")]
        public string Priority { get; set; } = "Orta";              // Öncelik (Yüksek, Orta, Düşük)

        [Display(Name = "Hedeflenen Süre (Saat)")]
        public int PlannedHours { get; set; } = 3;                  // Planlanan Çalışma Süresi (Saat)

        [Display(Name = "Şu Ana Kadar Çalışılan Süre (Saat)")]
        public int CompletedHours { get; set; } = 0;                // Gerçekte Çalışılan Süre (Saat)

        [Display(Name = "Konu şimdiden tamamlandı olarak işaretlensin")]
        public bool IsCompleted { get; set; }                       // Tamamlandı mı?
    }
}
