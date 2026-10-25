namespace OCSTS.Models
{
    /// <summary>
    /// KISIM 2 - SAYFALAR ARASI VERİ ERİŞİMİ VIEWMODEL'İ:
    /// Kullanıcının Sınav Ekle (/Exam/Create) ve Konu Ekle (/StudyPlan/Create) sayfalarından
    /// girdiği verilerin Ana Sayfada (/Home/Index) da doğrudan görüntülenebilmesini sağlar.
    /// </summary>
    public class DashboardViewModel
    {
        public List<Exam> UpcomingAndRecentExams { get; set; } = new();
        public List<StudyPlan> StudyPlans { get; set; } = new();
    }
}
