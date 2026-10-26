namespace StudyFlow.Web.DemoData;

/// <summary>
/// Öğrencinin takip ettiği bir dersi temsil eden örnek veri kaydı (FR-001..FR-005).
/// </summary>
public sealed record DemoCourse(
    int Id,
    string Name,
    bool IsArchived,
    int WeeklyTargetMinutes,
    string Description);

/// <summary>
/// Bir derse ait çalışma konusunu temsil eden örnek veri kaydı (FR-009..FR-012).
/// </summary>
public sealed record DemoTopic(
    int Id,
    int CourseId,
    string Title,
    TopicStatus Status,
    int SortOrder,
    string? Notes,
    DateOnly? ReviewOn);

/// <summary>
/// Bir derse bağlı geçmiş veya yaklaşan sınavı temsil eden örnek veri kaydı (FR-006..FR-008).
/// </summary>
public sealed record DemoExam(
    int Id,
    int CourseId,
    string Title,
    ExamType Type,
    DateTimeOffset ScheduledAt,
    string ScopeSummary);

/// <summary>
/// Bir derse ve isteğe bağlı konuya bağlı çalışma görevini temsil eden örnek veri kaydı (FR-013..FR-016).
/// </summary>
public sealed record DemoTask(
    int Id,
    int CourseId,
    int? TopicId,
    string Title,
    StudyTaskStatus Status,
    TaskPriority Priority,
    DateOnly? DueOn,
    int EstimatedMinutes,
    DateTimeOffset? CompletedAt)
{
    /// <summary>
    /// FR-016: DueOn &lt; yerel bugün ve tamamlanmamış görev gecikmiştir; null son tarih gecikmez.
    /// </summary>
    public bool IsOverdue(DateOnly today) =>
        Status != StudyTaskStatus.Completed && DueOn.HasValue && DueOn.Value < today;
}

/// <summary>
/// Bir görevin belirli bir yerel güne planlanan çalışma süresini temsil eden örnek kayıt (FR-017..FR-020).
/// </summary>
public sealed record DemoPlanItem(
    int Id,
    int CourseId,
    int TaskId,
    DateOnly PlannedOn,
    int PlannedMinutes);

/// <summary>
/// Tamamlanmış bir çalışma oturumunu temsil eden örnek kayıt (FR-021..FR-025).
/// İlk teslimde tüm örnek oturumlar bitmiş ve geçmişte (&lt;= LocalNow) üretilir.
/// </summary>
public sealed record DemoSession(
    int Id,
    int CourseId,
    int? TaskId,
    int? TopicId,
    DateTimeOffset StartAt,
    DateTimeOffset EndAt,
    SessionSource Source)
{
    /// <summary>
    /// Oturumun saniye cinsinden gerçek süresi.
    /// </summary>
    public int DurationSeconds => (int)Math.Max(0, (EndAt - StartAt).TotalSeconds);

    /// <summary>
    /// FR-025: Saniyelerden yuvarlanmış gösterim dakikası.
    /// </summary>
    public int DurationMinutes => (int)Math.Round(DurationSeconds / 60.0, MidpointRounding.AwayFromZero);
}

/// <summary>
/// Tüm ekranların aynı anda paylaştığı tutarlı örnek veri kümesi.
/// </summary>
public sealed record DemoDataset(
    DateTimeOffset UtcNow,
    DateTimeOffset LocalNow,
    DateOnly Today,
    DateOnly WeekStart,
    DateOnly WeekEnd,
    IReadOnlyList<DemoCourse> Courses,
    IReadOnlyList<DemoTopic> Topics,
    IReadOnlyList<DemoExam> Exams,
    IReadOnlyList<DemoTask> Tasks,
    IReadOnlyList<DemoPlanItem> PlanItems,
    IReadOnlyList<DemoSession> Sessions)
{
    /// <summary>
    /// FR-028: Kişisel haftalık hedef yalnızca aktif derslerin haftalık hedef toplamıdır;
    /// arşivli derslerin hedefi bu toplama katılmaz.
    /// </summary>
    public int PersonalWeeklyTargetMinutes =>
        Courses.Where(c => !c.IsArchived).Sum(c => c.WeeklyTargetMinutes);

    /// <summary>
    /// Kimliğe (Id) göre ders arar; bulunamazsa null döner.
    /// </summary>
    public DemoCourse? FindCourse(int id) =>
        Courses.FirstOrDefault(c => c.Id == id);

    /// <summary>
    /// Kimliğe (Id) göre sınav arar; bulunamazsa null döner.
    /// </summary>
    public DemoExam? FindExam(int id) =>
        Exams.FirstOrDefault(e => e.Id == id);

    /// <summary>
    /// Kimliğe (Id) göre konu arar; bulunamazsa null döner.
    /// </summary>
    public DemoTopic? FindTopic(int id) =>
        Topics.FirstOrDefault(t => t.Id == id);

    /// <summary>
    /// Kimliğe (Id) göre görev arar; bulunamazsa null döner.
    /// </summary>
    public DemoTask? FindTask(int id) =>
        Tasks.FirstOrDefault(t => t.Id == id);

    /// <summary>
    /// FR-012: Konu ilerlemesini Completed / toplam * 100 olarak (bir ondalık basamakla) hesaplar.
    /// İlgili derste hiç konu yoksa 0.0 döner.
    /// </summary>
    public double CalculateTopicProgressPercent(int? courseId = null)
    {
        var query = courseId.HasValue
            ? Topics.Where(t => t.CourseId == courseId.Value)
            : Topics.Where(t => Courses.Any(c => c.Id == t.CourseId && !c.IsArchived));

        var list = query.ToList();
        if (list.Count == 0)
        {
            return 0.0;
        }

        int completedCount = list.Count(t => t.Status == TopicStatus.Completed);
        return Math.Round(completedCount * 100.0 / list.Count, 1, MidpointRounding.AwayFromZero);
    }

    /// <summary>
    /// FR-007: Yaklaşan sınav aralığı [now, now + 14 gün) olarak tanımlanır (now dahil, now+14 gün hariç).
    /// </summary>
    public bool IsUpcomingExam(DemoExam exam)
    {
        var windowEnd = UtcNow.AddDays(14);
        return exam.ScheduledAt >= UtcNow && exam.ScheduledAt < windowEnd;
    }

    /// <summary>
    /// FR-007: Sınav zamanı şu andan önceyse geçmiş sınav kabul edilir.
    /// </summary>
    public bool IsPastExam(DemoExam exam) => exam.ScheduledAt < UtcNow;

    /// <summary>
    /// FR-007: Kalan gün hesabı yerel takvim günleri arasındaki farktır.
    /// </summary>
    public int GetDaysRemaining(DemoExam exam, IClock clock)
    {
        var examLocalDate = clock.ToLocalDate(exam.ScheduledAt);
        return examLocalDate.DayNumber - Today.DayNumber;
    }
}
