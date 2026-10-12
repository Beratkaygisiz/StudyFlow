namespace SSET.Models
{
    /// <summary>
    /// MODEL (Veri Yapısı):
    /// Sınav bilgilerini temsil eden basit C# sınıfıdır.
    /// </summary>
    public class Exam
    {
        public int Id { get; set; }                     // Sınav Numarası
        public string CourseName { get; set; } = "";    // Ders Adı
        public DateTime ExamDate { get; set; }          // Sınav Tarihi
        public int TargetScore { get; set; }           // Hedeflenen Not (0-100)
        public string? Description { get; set; }       // Kısa Not / Açıklama
    }
}
