namespace StudyFlow.Web.DemoData;

/// <summary>
/// Sınav türlerini temsil eden sabit liste (Teknik Tasarım: Midterm=0, Final=1, Quiz=2, Other=3).
/// </summary>
public enum ExamType : byte
{
    Midterm = 0,
    Final = 1,
    Quiz = 2,
    Other = 3
}

/// <summary>
/// Bir ders konusunun çalışma durumunu temsil eden sabit liste (NotStarted=0, InProgress=1, Completed=2).
/// </summary>
public enum TopicStatus : byte
{
    NotStarted = 0,
    InProgress = 1,
    Completed = 2
}

/// <summary>
/// Çalışma görevinin durumunu temsil eden sabit liste (Pending=0, InProgress=1, Completed=2).
/// </summary>
public enum StudyTaskStatus : byte
{
    Pending = 0,
    InProgress = 1,
    Completed = 2
}

/// <summary>
/// Çalışma görevinin öncelik düzeyini temsil eden sabit liste (Low=0, Normal=1, High=2).
/// </summary>
public enum TaskPriority : byte
{
    Low = 0,
    Normal = 1,
    High = 2
}

/// <summary>
/// Çalışma oturumunun kayıt kaynağını temsil eden sabit liste (Timer=0, Manual=1).
/// </summary>
public enum SessionSource : byte
{
    Timer = 0,
    Manual = 1
}

/// <summary>
/// Enum değerlerini kullanıcı arayüzünde Türkçe etiketlere ve ortak CSS rozet sınıflarına
/// dönüştüren yardımcı genişletme metotları.
/// </summary>
public static class DemoEnumExtensions
{
    /// <summary>
    /// Sınav türünün Türkçe ekran adını döndürür.
    /// </summary>
    public static string GetDisplayName(this ExamType type) => type switch
    {
        ExamType.Midterm => "Vize",
        ExamType.Final => "Final",
        ExamType.Quiz => "Kısa Sınav (Quiz)",
        ExamType.Other => "Diğer",
        _ => "Bilinmiyor"
    };

    /// <summary>
    /// Sınav türüne uygun ortak rozet (badge) CSS sınıfını döndürür.
    /// </summary>
    public static string GetBadgeClass(this ExamType type) => type switch
    {
        ExamType.Midterm => "sf-badge-primary",
        ExamType.Final => "sf-badge-warning",
        ExamType.Quiz => "sf-badge-info",
        ExamType.Other => "sf-badge-neutral",
        _ => "sf-badge-neutral"
    };

    /// <summary>
    /// Konu tamamlanma durumunun Türkçe ekran adını döndürür.
    /// </summary>
    public static string GetDisplayName(this TopicStatus status) => status switch
    {
        TopicStatus.NotStarted => "Başlanmadı",
        TopicStatus.InProgress => "Devam Ediyor",
        TopicStatus.Completed => "Tamamlandı",
        _ => "Bilinmiyor"
    };

    /// <summary>
    /// Konu tamamlanma durumuna uygun ortak rozet CSS sınıfını döndürür.
    /// </summary>
    public static string GetBadgeClass(this TopicStatus status) => status switch
    {
        TopicStatus.NotStarted => "sf-badge-neutral",
        TopicStatus.InProgress => "sf-badge-primary",
        TopicStatus.Completed => "sf-badge-success",
        _ => "sf-badge-neutral"
    };

    /// <summary>
    /// Görev durumunun Türkçe ekran adını döndürür.
    /// </summary>
    public static string GetDisplayName(this StudyTaskStatus status) => status switch
    {
        StudyTaskStatus.Pending => "Bekliyor",
        StudyTaskStatus.InProgress => "Devam Ediyor",
        StudyTaskStatus.Completed => "Tamamlandı",
        _ => "Bilinmiyor"
    };

    /// <summary>
    /// Görev durumuna uygun ortak rozet CSS sınıfını döndürür.
    /// </summary>
    public static string GetBadgeClass(this StudyTaskStatus status) => status switch
    {
        StudyTaskStatus.Pending => "sf-badge-neutral",
        StudyTaskStatus.InProgress => "sf-badge-primary",
        StudyTaskStatus.Completed => "sf-badge-success",
        _ => "sf-badge-neutral"
    };

    /// <summary>
    /// Görev önceliğinin Türkçe ekran adını döndürür.
    /// </summary>
    public static string GetDisplayName(this TaskPriority priority) => priority switch
    {
        TaskPriority.Low => "Düşük",
        TaskPriority.Normal => "Normal",
        TaskPriority.High => "Yüksek",
        _ => "Normal"
    };

    /// <summary>
    /// Görev önceliğine uygun ortak rozet CSS sınıfını döndürür.
    /// </summary>
    public static string GetBadgeClass(this TaskPriority priority) => priority switch
    {
        TaskPriority.Low => "sf-badge-neutral",
        TaskPriority.Normal => "sf-badge-info",
        TaskPriority.High => "sf-badge-danger",
        _ => "sf-badge-neutral"
    };

    /// <summary>
    /// Oturum kaynağının Türkçe ekran adını döndürür.
    /// </summary>
    public static string GetDisplayName(this SessionSource source) => source switch
    {
        SessionSource.Timer => "Zamanlayıcı",
        SessionSource.Manual => "Manuel Kayıt",
        _ => "Manuel Kayıt"
    };

    /// <summary>
    /// Verilen tarihin Türkçe tam gün adını döndürür (Pazartesi..Pazar).
    /// </summary>
    public static string GetTurkishDayName(DateOnly date) => date.DayOfWeek switch
    {
        DayOfWeek.Monday => "Pazartesi",
        DayOfWeek.Tuesday => "Salı",
        DayOfWeek.Wednesday => "Çarşamba",
        DayOfWeek.Thursday => "Perşembe",
        DayOfWeek.Friday => "Cuma",
        DayOfWeek.Saturday => "Cumartesi",
        DayOfWeek.Sunday => "Pazar",
        _ => ""
    };

    /// <summary>
    /// Verilen tarihin Türkçe kısa gün adını döndürür (Pzt..Paz).
    /// </summary>
    public static string GetTurkishShortDayName(DateOnly date) => date.DayOfWeek switch
    {
        DayOfWeek.Monday => "Pzt",
        DayOfWeek.Tuesday => "Sal",
        DayOfWeek.Wednesday => "Çar",
        DayOfWeek.Thursday => "Per",
        DayOfWeek.Friday => "Cum",
        DayOfWeek.Saturday => "Cmt",
        DayOfWeek.Sunday => "Paz",
        _ => ""
    };
}
