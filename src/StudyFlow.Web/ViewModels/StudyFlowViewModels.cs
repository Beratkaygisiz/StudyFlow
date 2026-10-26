using System.ComponentModel.DataAnnotations;
using StudyFlow.Web.DemoData;

namespace StudyFlow.Web.ViewModels;

/// <summary>
/// Beklenmedik uygulama hatalarında izleme kimliğini (TraceId / RequestId)
/// kullanıcıya güvenli biçimde göstermek için kullanılan hata görünüm modeli.
/// </summary>
public sealed class ErrorViewModel
{
    public string? RequestId { get; set; }
    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}

/// <summary>
/// Bulunamayan kayıtlar (HTTP 404) için ortak bulunamadı ekranına taşınan görünüm modeli.
/// </summary>
public sealed record NotFoundViewModel(
    string ResourceName,
    int? RequestedId,
    string Message,
    string ReturnUrl,
    string ReturnLabel);

/// <summary>
/// Faz 1B / FR-001, FR-037..FR-039 kapsamında geçici ders oluşturma formu (/Courses/Create)
/// için kullanılan tipli girdi modeli.
/// Güvenlik kuralı gereği Id, IsArchived veya UserId alanları istemciden alınmaz;
/// yeni ders her zaman sunucu tarafından yeni kimlik ve aktif (IsArchived = false) durumla oluşturulur.
/// </summary>
public sealed class CourseCreateViewModel
{
    [Display(Name = "Ders Adı")]
    [Required(ErrorMessage = "Ders adı zorunludur.")]
    [StringLength(120, MinimumLength = 1, ErrorMessage = "Ders adı trim sonrasında 1 ile 120 karakter arasında olmalıdır.")]
    public string Name { get; set; } = string.Empty;

    [Display(Name = "Haftalık Çalışma Hedefi (Dakika)")]
    [Required(ErrorMessage = "Haftalık çalışma hedefi (dakika) zorunludur.")]
    [Range(0, 10080, ErrorMessage = "Haftalık çalışma hedefi 0 ile 10080 dakika arasında bir tam sayı olmalıdır.")]
    public int? WeeklyTargetMinutes { get; set; } = 120;
}

/// <summary>
/// Bir güne planlanmış örnek görev kalemini temsil eden veri aktarım nesnesi (DTO).
/// </summary>
public sealed record PlannedTaskItemDto(
    int PlanId,
    int CourseId,
    string CourseName,
    int TaskId,
    string TaskTitle,
    StudyTaskStatus TaskStatus,
    int PlannedMinutes,
    int ActualMinutesForTaskOnDay);

/// <summary>
/// Tamamlanmış bir örnek çalışma oturumunu yerel saat bilgileriyle temsil eden DTO.
/// </summary>
public sealed record StudySessionItemDto(
    int SessionId,
    int CourseId,
    string CourseName,
    int? TaskId,
    string TaskTitle,
    DateTimeOffset StartLocal,
    DateTimeOffset EndLocal,
    int DurationMinutes,
    SessionSource Source,
    bool IsMatchedToPlan);

/// <summary>
/// Haftalık özet ve çalışma tablolarında bir yerel günün planlanan ve gerçekleşen
/// çalışma verilerini birleştiren özet DTO.
/// </summary>
public sealed record DailyStudySummaryDto(
    DateOnly Date,
    string DayName,
    string ShortDayName,
    bool IsToday,
    bool IsFuture,
    int PlannedMinutes,
    int ActualMinutes,
    int MatchedActualMinutes,
    int UnplannedActualMinutes,
    IReadOnlyList<PlannedTaskItemDto> PlannedItems,
    IReadOnlyList<StudySessionItemDto> Sessions)
{
    public int PlanDeltaMinutes => ActualMinutes - PlannedMinutes;

    public string PlanDeltaText =>
        PlanDeltaMinutes > 0
            ? $"+{PlanDeltaMinutes} dk"
            : $"{PlanDeltaMinutes} dk";
}

/// <summary>
/// Dashboard (/Dashboard) ekranında son tamamlanan 5 görev listesi için DTO (FR-026).
/// </summary>
public sealed record DashboardCompletedTaskDto(
    int Id,
    int CourseId,
    string CourseName,
    string Title,
    TaskPriority Priority,
    string PriorityDisplayName,
    string PriorityBadgeClass,
    DateTimeOffset CompletedAtLocal);

/// <summary>
/// UpcomingExamsViewComponent tarafından Views/Shared/Components/UpcomingExams/Default.cshtml
/// görünümüne aktarılan tipli görünüm modeli.
/// </summary>
public sealed record UpcomingExamsComponentViewModel(
    int? CourseId,
    string? CourseName,
    bool IsArchivedCourse,
    int MaxResults,
    int TotalMatchingUpcomingCount,
    IReadOnlyList<ExamListItemViewModel> Exams)
{
    public bool IsCourseScoped => CourseId.HasValue;
    public bool HasMoreThanMax => TotalMatchingUpcomingCount > Exams.Count;
}

/// <summary>
/// DashboardController.Index (/ ve /Dashboard) görünümünün ana ViewModel'i (FR-026, FR-028).
/// Aktif derslerin kartları ortak CourseCardViewModel üzerinden _CourseCard.cshtml partial görünümüyle çizilir.
/// </summary>
public sealed record DashboardIndexViewModel(
    DateOnly Today,
    DateOnly WeekStart,
    DateOnly WeekEnd,
    int ActiveCourseCount,
    int PendingTaskCount,
    int OverdueTaskCount,
    int PersonalWeeklyTargetMinutes,
    int WeeklyActualMinutes,
    int ArchivedCourseWeeklyActualMinutes,
    IReadOnlyList<ExamListItemViewModel> UpcomingExams,
    IReadOnlyList<DashboardCompletedTaskDto> RecentCompletedTasks,
    IReadOnlyList<CourseCardViewModel> CourseProgressItems,
    IReadOnlyList<DailyStudySummaryDto>? WeeklyDays = null)
{
    public int WeeklyTargetAchievementPercent =>
        PersonalWeeklyTargetMinutes <= 0
            ? 0
            : (int)Math.Min(100, Math.Round(WeeklyActualMinutes * 100.0 / PersonalWeeklyTargetMinutes, MidpointRounding.AwayFromZero));

    public int RemainingToTargetMinutes => Math.Max(0, PersonalWeeklyTargetMinutes - WeeklyActualMinutes);

    public int WeeklyPlannedMinutes => WeeklyDays?.Sum(d => d.PlannedMinutes) ?? 0;

    public int ChartScaleMaxMinutes =>
        Math.Max(
            60,
            WeeklyDays is { Count: > 0 }
                ? WeeklyDays.SelectMany(d => new[] { d.PlannedMinutes, d.ActualMinutes }).DefaultIfEmpty(60).Max()
                : 60);
}

/// <summary>
/// DashboardController.Weekly (/Dashboard/Weekly) görünümünün ana ViewModel'i (FR-019, FR-020, FR-025, FR-026, FR-028).
/// </summary>
public sealed record WeeklyOverviewViewModel(
    DateOnly Today,
    DateOnly WeekStart,
    DateOnly WeekEnd,
    int PersonalWeeklyTargetMinutes,
    int WeeklyPlannedMinutes,
    int WeeklyActualMinutes,
    int WeeklyMatchedActualMinutes,
    int WeeklyUnplannedActualMinutes,
    int ArchivedCourseWeeklyActualMinutes,
    int ChartScaleMaxMinutes,
    IReadOnlyList<DailyStudySummaryDto> Days)
{
    public int WeeklyPlanDeltaMinutes => WeeklyActualMinutes - WeeklyPlannedMinutes;

    public string WeeklyPlanDeltaText =>
        WeeklyPlanDeltaMinutes > 0
            ? $"+{WeeklyPlanDeltaMinutes} dk"
            : $"{WeeklyPlanDeltaMinutes} dk";

    public int WeeklyTargetAchievementPercent =>
        PersonalWeeklyTargetMinutes <= 0
            ? 0
            : (int)Math.Min(100, Math.Round(WeeklyActualMinutes * 100.0 / PersonalWeeklyTargetMinutes, MidpointRounding.AwayFromZero));

    public int RemainingToTargetMinutes => Math.Max(0, PersonalWeeklyTargetMinutes - WeeklyActualMinutes);
}

/// <summary>
/// Ders listesi ekranında (/Courses) durum filtresi seçeneklerini temsil eden sabit değerler.
/// </summary>
public static class CourseStatusFilterValues
{
    public const string All = "All";
    public const string Active = "Active";
    public const string Archived = "Archived";

    public static string Normalize(string? rawStatus)
    {
        if (string.Equals(rawStatus, Active, StringComparison.OrdinalIgnoreCase))
        {
            return Active;
        }

        if (string.Equals(rawStatus, Archived, StringComparison.OrdinalIgnoreCase))
        {
            return Archived;
        }

        return All;
    }
}

/// <summary>
/// Ders listesi (/Courses) ekranındaki her bir ders kartının görünüm modeli.
/// </summary>
public sealed record CourseCardViewModel(
    int Id,
    string Name,
    string Description,
    bool IsArchived,
    int WeeklyTargetMinutes,
    int TotalTopicCount,
    int CompletedTopicCount,
    int InProgressTopicCount,
    int NotStartedTopicCount,
    double TopicProgressPercent,
    int UpcomingExamCount,
    int PendingTaskCount,
    bool IsSessionCourse = false)
{
    public bool HasTopics => TotalTopicCount > 0;
}

/// <summary>
/// CoursesController.Index (/Courses) görünümünün ana ViewModel'i.
/// </summary>
public sealed record CoursesIndexViewModel(
    string SearchQuery,
    string StatusFilter,
    int TotalCourseCount,
    int ActiveCourseCount,
    int ArchivedCourseCount,
    int PersonalWeeklyTargetMinutes,
    IReadOnlyList<CourseCardViewModel> Courses)
{
    public bool HasActiveFilter =>
        !string.IsNullOrWhiteSpace(SearchQuery) ||
        !string.Equals(StatusFilter, CourseStatusFilterValues.All, StringComparison.OrdinalIgnoreCase);
}

/// <summary>
/// Ders detay (/Courses/Details/{id}) ekranında listelenen her bir konunun görünüm modeli.
/// </summary>
public sealed record CourseTopicItemViewModel(
    int Id,
    int SortOrder,
    string Title,
    TopicStatus Status,
    string StatusDisplayName,
    string StatusBadgeClass,
    string StatusDescriptionText,
    string? Notes,
    DateOnly? ReviewOn,
    int RelatedTaskCount);

/// <summary>
/// Ders detay (/Courses/Details/{id}) ekranında o derse ait sınavların özet görünüm modeli.
/// </summary>
public sealed record CourseExamItemViewModel(
    int Id,
    string Title,
    ExamType Type,
    string TypeDisplayName,
    string TypeBadgeClass,
    DateTimeOffset ScheduledAtLocal,
    int DaysRemaining,
    bool IsPast,
    bool IsUpcomingWithin14Days,
    string ScopeSummary);

/// <summary>
/// CoursesController.Details (/Courses/Details/{id}) görünümünün ana ViewModel'i.
/// </summary>
public sealed record CourseDetailsViewModel(
    int Id,
    string Name,
    string Description,
    bool IsArchived,
    int WeeklyTargetMinutes,
    int WeeklyPlannedMinutes,
    int WeeklyActualMinutes,
    int TotalTopicCount,
    int CompletedTopicCount,
    int InProgressTopicCount,
    int NotStartedTopicCount,
    double TopicProgressPercent,
    int PendingTaskCount,
    int CompletedTaskCount,
    IReadOnlyList<CourseTopicItemViewModel> Topics,
    IReadOnlyList<CourseExamItemViewModel> Exams,
    bool IsSessionCourse = false)
{
    public bool HasTopics => TotalTopicCount > 0;
}

/// <summary>
/// Sınav listesi ekranında (/Exams) zaman aralığı filtresi seçeneklerini temsil eden sabitler (FR-007).
/// </summary>
public static class ExamFilterValues
{
    public const string All = "All";
    public const string Upcoming = "Upcoming";
    public const string Past = "Past";

    public static string Normalize(string? rawFilter)
    {
        if (string.Equals(rawFilter, Upcoming, StringComparison.OrdinalIgnoreCase))
        {
            return Upcoming;
        }

        if (string.Equals(rawFilter, Past, StringComparison.OrdinalIgnoreCase))
        {
            return Past;
        }

        return All;
    }

    public static string GetDisplayLabel(string normalizedFilter) => normalizedFilter switch
    {
        Upcoming => "Yaklaşan (14 Gün)",
        Past => "Geçmiş Sınavlar",
        _ => "Tümü"
    };
}

/// <summary>
/// Sınav listesi (/Exams) ekranındaki ders seçimi açılır listesi (dropdown) için seçenek modeli.
/// </summary>
public sealed record ExamCourseOptionViewModel(
    int Id,
    string Name,
    bool IsArchived);

/// <summary>
/// Sınav listesi (/Exams) ekranındaki her bir sınav satırının/kartının görünüm modeli (FR-007).
/// </summary>
public sealed record ExamListItemViewModel(
    int Id,
    int CourseId,
    string CourseName,
    bool CourseIsArchived,
    string Title,
    ExamType Type,
    string TypeDisplayName,
    string TypeBadgeClass,
    DateTimeOffset ScheduledAtLocal,
    int DaysRemaining,
    bool IsPast,
    bool IsUpcomingWithin14Days,
    string ScopeSummary)
{
    public string RemainingDaysText =>
        DaysRemaining == 0
            ? (IsPast ? "Bugün (Tamamlandı)" : "Bugün (Sınav Günü)")
            : (IsPast || DaysRemaining < 0
                ? $"{Math.Abs(DaysRemaining)} gün geçti"
                : $"{DaysRemaining} gün kaldı");

    public string RemainingDaysBadgeClass =>
        IsPast
            ? "sf-badge-neutral"
            : (DaysRemaining == 0
                ? "sf-badge-danger"
                : (IsUpcomingWithin14Days ? "sf-badge-info" : "sf-badge-neutral"));
}

/// <summary>
/// ExamsController.Index (/Exams) görünümünün ana ViewModel'i.
/// </summary>
public sealed record ExamsIndexViewModel(
    string PeriodFilter,
    int? SelectedCourseId,
    string? SelectedCourseName,
    int TotalExamCount,
    int UpcomingExamCount,
    int PastExamCount,
    IReadOnlyList<ExamCourseOptionViewModel> CourseOptions,
    IReadOnlyList<ExamListItemViewModel> Exams)
{
    public bool HasActiveFilter =>
        !string.Equals(PeriodFilter, ExamFilterValues.All, StringComparison.OrdinalIgnoreCase) ||
        SelectedCourseId.HasValue;
}

/// <summary>
/// ExamsController.Details (/Exams/Details/{id}) görünümünün ana ViewModel'i.
/// </summary>
public sealed record ExamDetailsViewModel(
    int Id,
    int CourseId,
    string CourseName,
    bool CourseIsArchived,
    int CourseWeeklyTargetMinutes,
    string Title,
    ExamType Type,
    string TypeDisplayName,
    string TypeBadgeClass,
    DateTimeOffset ScheduledAtLocal,
    int DaysRemaining,
    bool IsPast,
    bool IsUpcomingWithin14Days,
    string ScopeSummary,
    IReadOnlyList<CourseTopicItemViewModel> RelatedCourseTopics)
{
    public string RemainingDaysText =>
        DaysRemaining == 0
            ? (IsPast ? "Bugün (Sınav saati geçti)" : "Bugün (Sınav günü)")
            : (IsPast || DaysRemaining < 0
                ? $"{Math.Abs(DaysRemaining)} gün önce gerçekleşti"
                : $"{DaysRemaining} gün kaldı");

    public string PeriodStatusLabel =>
        IsPast
            ? "Geçmiş Sınav"
            : (IsUpcomingWithin14Days ? "Yaklaşan Sınav (14 Gün İçinde)" : "İleri Tarihli Sınav");

    public string PeriodStatusBadgeClass =>
        IsPast
            ? "sf-badge-neutral"
            : (IsUpcomingWithin14Days ? "sf-badge-info" : "sf-badge-primary");
}
