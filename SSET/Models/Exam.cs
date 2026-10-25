using System.ComponentModel.DataAnnotations;

namespace OCSTS.Models
{
    /// <summary>
    /// MODEL (Veri Yapısı):
    /// Öğrencinin sınav bilgilerini temsil eden C# sınıfıdır.
    /// Kısım 2 kapsamında HtmlHelpers (Html.LabelFor, Html.TextBoxFor, Html.DropDownListFor vb.)
    /// ile tam uyumlu çalışması için [Display] nitelikleri ve ExamType alanı eklenmiştir.
    /// </summary>
    public class Exam
    {
        public int Id { get; set; }                                 // Sınav Numarası

        [Display(Name = "Ders Adı")]
        public string CourseName { get; set; } = "";                // Ders Adı

        [Display(Name = "Sınav Türü")]
        public string ExamType { get; set; } = "Vize";              // Sınav Türü (Vize, Final, Quiz, Proje/Ödev)

        [Display(Name = "Sınav Tarihi ve Saati")]
        public DateTime ExamDate { get; set; } = DateTime.Now.AddDays(7); // Sınav Tarihi

        [Display(Name = "Hedeflenen Not (0 - 100)")]
        public int TargetScore { get; set; } = 85;                  // Hedeflenen Not

        [Display(Name = "Alınan Gerçek Not")]
        public int? ActualScore { get; set; }                       // Alınan Not (Sınav sonrası girilir)

        [Display(Name = "Çalışma Notu / Sınav Kapsamı")]
        public string? Description { get; set; }                    // Kısa Açıklama / Konu Kapsamı
    }
}
