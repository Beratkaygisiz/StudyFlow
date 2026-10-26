using System.Globalization;
using StudyFlow.Web.ViewModels;

namespace StudyFlow.Web.DemoData;

/// <summary>
/// İlk ders teslimindeki tüm ekranların (Dashboard, Haftalık Özet, Dersler, Sınavlar)
/// aynı kimlik ve ilişkilerle beslenmesini sağlayan ortak örnek veri sağlayıcı sözleşmesi.
/// </summary>
public interface IDemoDataProvider
{
    DemoDataset GetDataset();
    IReadOnlyList<DailyStudySummaryDto> GetWeeklySchedule(DemoDataset dataset);
    DashboardIndexViewModel BuildDashboardIndex();
    WeeklyOverviewViewModel BuildWeeklyOverview();
    CoursesIndexViewModel BuildCoursesIndex(string? searchQuery, string? statusFilter);
    CourseDetailsViewModel? BuildCourseDetails(int courseId);
    ExamsIndexViewModel BuildExamsIndex(string? periodFilter, int? courseId);
    ExamDetailsViewModel? BuildExamDetails(int examId);
}

/// <summary>
/// Sabit takvim tarihlerine bağlı kalmadan, IClock üzerinden Türkiye yerel gününe (Europe/Istanbul)
/// ve Pazartesi başlayan haftaya göre dinamik örnek veri üreten; dersleri ise IDemoCourseStore
/// (salt okunur seed + Session içindeki geçici dersler) üzerinden birleştiren sınıf.
/// </summary>
public sealed class DemoDataProvider : IDemoDataProvider
{
    private static readonly CultureInfo TurkishCulture = new("tr-TR");
    private static readonly StringComparer TurkishIgnoreCaseComparer = StringComparer.Create(TurkishCulture, ignoreCase: true);

    private readonly IClock _clock;
    private readonly IDemoCourseStore _courseStore;

    public DemoDataProvider(IClock clock, IDemoCourseStore courseStore)
    {
        _clock = clock;
        _courseStore = courseStore;
    }

    public DemoDataset GetDataset()
    {
        var utcNow = _clock.UtcNow;
        var localNow = _clock.LocalNow;
        var today = _clock.Today;
        var weekStart = _clock.CurrentWeekStart;
        var weekEnd = _clock.CurrentWeekEnd;

        // Salt okunur seed dersler ile Session'da geçici saklanan dersleri tek kaynakta birleştir.
        var courses = _courseStore.GetAllCourses();

        var topics = new List<DemoTopic>
        {
            // Course 1: Web Programlama
            new(1, 1, "HTTP Yaşam Döngüsü ve MVC Yönlendirme", TopicStatus.Completed, 1,
                "Route şablonları ve durum kodları.", today.AddDays(-5)),
            new(2, 1, "Razor Görünümleri, Layout ve Partial Yapısı", TopicStatus.Completed, 2,
                "Ortak şablon ve erişilebilir menü yapısı.", today.AddDays(-2)),
            new(3, 1, "ViewModel Tasarımı ve Sunucu Doğrulama Kuralları", TopicStatus.InProgress, 3,
                "ModelState ve PRG akışı.", today.AddDays(2)),
            new(4, 1, "Eşzamanlılık Koruması ve Güvenli Form Gönderimi", TopicStatus.NotStarted, 4,
                "RowVersion ve OperationId kalıpları.", today.AddDays(6)),

            // Course 2: Veritabanı Yönetim Sistemleri
            new(5, 2, "Varlık-İlişki (ER) Modelleme ve Kısıtlar", TopicStatus.Completed, 1,
                "Bileşik anahtarlar ve referans bütünlüğü.", today.AddDays(-6)),
            new(6, 2, "Karmaşık JOIN, Gruplama ve Pencere Fonksiyonları", TopicStatus.InProgress, 2,
                "Toplam süre ve aralık kesişimi sorguları.", today.AddDays(1)),
            new(7, 2, "B-Tree İndeksler ve Sorgu Planı İncelemesi", TopicStatus.InProgress, 3,
                "Seçicilik ve covering index yapısı.", today.AddDays(4)),
            new(8, 2, "Transaction İzolasyon Seviyeleri ve Kilitlenme", TopicStatus.NotStarted, 4,
                "UPDLOCK ve HOLDLOCK senaryoları.", today.AddDays(8)),

            // Course 3: Algoritma Analizi ve Tasarımı
            new(9, 3, "Asimptotik Gösterimler ve Rekürans Bağıntıları", TopicStatus.Completed, 1,
                "Master teoremi soru çözümleri.", today.AddDays(-4)),
            new(10, 3, "En Kısa Yol ve Minimum Kapsayan Ağaç Algoritmaları", TopicStatus.InProgress, 2,
                "Dijkstra ve Kruskal karşılaştırması.", today.AddDays(3)),
            new(11, 3, "Dinamik Programlama ve Alt Problem Tablolama", TopicStatus.NotStarted, 3,
                "Sırt çantası ve dizi hizalama problemleri.", today.AddDays(7)),

            // Course 4: Ayrık Matematik (Arşivli ders)
            new(12, 4, "Önermeler Mantığı ve Doğruluk Tabloları", TopicStatus.Completed, 1,
                "Dönem içi özet kartları.", null),
            new(13, 4, "Tümevarım ve Güvercin Yuvası İlkesi", TopicStatus.Completed, 2,
                "Klasik ispat soruları.", null),
            new(14, 4, "Bağıntılar ve Kısmi Sıralama Kümeleri", TopicStatus.Completed, 3,
                "Hasse diyagramı örnekleri.", null)
        };

        var sameDayExamTime = localNow.TimeOfDay < new TimeSpan(23, 45, 0)
            ? new TimeOnly(23, 50)
            : TimeOnly.FromTimeSpan(localNow.TimeOfDay);

        var exams = new List<DemoExam>
        {
            new(
                Id: 1,
                CourseId: 1,
                Title: "Web Programlama Ara Sınavı",
                Type: ExamType.Midterm,
                ScheduledAt: _clock.CreateLocalDateTime(today.AddDays(3), new TimeOnly(10, 0)),
                ScopeSummary: "MVC mimarisi, Razor görünümleri ve ViewModel bağlama."),
            new(
                Id: 2,
                CourseId: 3,
                Title: "Çizge Algoritmaları Kısa Sınavı",
                Type: ExamType.Quiz,
                ScheduledAt: _clock.CreateLocalDateTime(today.AddDays(6), new TimeOnly(13, 30)),
                ScopeSummary: "BFS, DFS, topolojik sıralama ve en kısa yol analizleri."),
            new(
                Id: 3,
                CourseId: 2,
                Title: "Veritabanı Yönetim Sistemleri Ara Sınavı",
                Type: ExamType.Midterm,
                ScheduledAt: _clock.CreateLocalDateTime(today.AddDays(11), new TimeOnly(9, 30)),
                ScopeSummary: "ER diyagramları, normalizasyon, indeks yapısı ve sorgu başarımı."),
            new(
                Id: 4,
                CourseId: 4,
                Title: "Ayrık Matematik Dönem Sonu Sınavı",
                Type: ExamType.Final,
                ScheduledAt: _clock.CreateLocalDateTime(today.AddDays(-35), new TimeOnly(10, 0)),
                ScopeSummary: "Mantık, tümevarım, kombinatorik ve bağıntılar."),
            new(
                Id: 5,
                CourseId: 1,
                Title: "HTTP ve MVC Temelleri Kısa Sınavı",
                Type: ExamType.Quiz,
                ScheduledAt: _clock.CreateLocalDateTime(today.AddDays(-9), new TimeOnly(11, 0)),
                ScopeSummary: "HTTP metotları, durum kodları ve MVC yönlendirme kuralları."),
            new(
                Id: 6,
                CourseId: 2,
                Title: "SQL Sorgulama Uygulama Sınavı",
                Type: ExamType.Quiz,
                ScheduledAt: _clock.CreateLocalDateTime(today.AddDays(-4), new TimeOnly(14, 0)),
                ScopeSummary: "JOIN, GROUP BY, HAVING ve alt sorgu senaryoları."),
            new(
                Id: 7,
                CourseId: 3,
                Title: "Algoritma Analizi Dönem Sonu Sınavı",
                Type: ExamType.Final,
                ScheduledAt: _clock.CreateLocalDateTime(today.AddDays(21), new TimeOnly(15, 0)),
                ScopeSummary: "Rekürans, açgözlü algoritmalar, çizgeler ve dinamik programlama."),
            new(
                Id: 8,
                CourseId: 1,
                Title: "Razor Şablonları Çevrimiçi Değerlendirme",
                Type: ExamType.Other,
                ScheduledAt: _clock.CreateLocalDateTime(today, sameDayExamTime),
                ScopeSummary: "Layout, Partial ve TagHelper kullanımı.")
        };

        var tasks = new List<DemoTask>
        {
            new(
                Id: 1,
                CourseId: 1,
                TopicId: 3,
                Title: "ViewModel doğrulama ve alan hatası senaryoları için 15 örnek incele",
                Status: StudyTaskStatus.InProgress,
                Priority: TaskPriority.High,
                DueOn: today.AddDays(1),
                EstimatedMinutes: 90,
                CompletedAt: null),
            new(
                Id: 2,
                CourseId: 2,
                TopicId: 6,
                Title: "Aralık kesişimi ve günlük toplam SQL sorgu alıştırmalarını çöz",
                Status: StudyTaskStatus.Pending,
                Priority: TaskPriority.High,
                DueOn: today.AddDays(-1),
                EstimatedMinutes: 75,
                CompletedAt: null),
            new(
                Id: 3,
                CourseId: 3,
                TopicId: 10,
                Title: "Dijkstra ve Bellman-Ford adım izleme tablosunu hazırla",
                Status: StudyTaskStatus.InProgress,
                Priority: TaskPriority.Normal,
                DueOn: today.AddDays(2),
                EstimatedMinutes: 60,
                CompletedAt: null),
            new(
                Id: 4,
                CourseId: 2,
                TopicId: 7,
                Title: "Birleşik indeks (composite index) sıra etkisini 3 sorguda karşılaştır",
                Status: StudyTaskStatus.Pending,
                Priority: TaskPriority.Normal,
                DueOn: today.AddDays(4),
                EstimatedMinutes: 60,
                CompletedAt: null),
            new(
                Id: 5,
                CourseId: 1,
                TopicId: 4,
                Title: "Klavye odağı ve mobil menü erişilebilirlik kontrol listesini uygula",
                Status: StudyTaskStatus.Pending,
                Priority: TaskPriority.Low,
                DueOn: today,
                EstimatedMinutes: 45,
                CompletedAt: null),
            new(
                Id: 6,
                CourseId: 1,
                TopicId: 2,
                Title: "Razor ortak Layout ve örnek veri rozeti şablonunu tamamla",
                Status: StudyTaskStatus.Completed,
                Priority: TaskPriority.High,
                DueOn: today.AddDays(-1),
                EstimatedMinutes: 60,
                CompletedAt: _clock.CreateLocalDateTime(today.AddDays(-1), new TimeOnly(18, 20))),
            new(
                Id: 7,
                CourseId: 3,
                TopicId: 9,
                Title: "Master teoremi ve özyineleme ağacı 20 soruluk çalışma setini bitir",
                Status: StudyTaskStatus.Completed,
                Priority: TaskPriority.Normal,
                DueOn: today.AddDays(-2),
                EstimatedMinutes: 80,
                CompletedAt: _clock.CreateLocalDateTime(today.AddDays(-2), new TimeOnly(20, 15))),
            new(
                Id: 8,
                CourseId: 2,
                TopicId: 5,
                Title: "Bileşik yabancı anahtar (composite FK) şema çizimini doğrula",
                Status: StudyTaskStatus.Completed,
                Priority: TaskPriority.High,
                DueOn: today.AddDays(-3),
                EstimatedMinutes: 50,
                CompletedAt: _clock.CreateLocalDateTime(today.AddDays(-3), new TimeOnly(17, 10))),
            new(
                Id: 9,
                CourseId: 1,
                TopicId: 1,
                Title: "MVC yönlendirme ve 404/409 hata durumu notlarını özetle",
                Status: StudyTaskStatus.Completed,
                Priority: TaskPriority.Normal,
                DueOn: today.AddDays(-4),
                EstimatedMinutes: 45,
                CompletedAt: _clock.CreateLocalDateTime(today.AddDays(-4), new TimeOnly(15, 40))),
            new(
                Id: 10,
                CourseId: 4,
                TopicId: 13,
                Title: "Matematiksel tümevarım geçmiş sınav sorularını tekrar et",
                Status: StudyTaskStatus.Completed,
                Priority: TaskPriority.Low,
                DueOn: today.AddDays(-15),
                EstimatedMinutes: 60,
                CompletedAt: _clock.CreateLocalDateTime(today.AddDays(-16), new TimeOnly(19, 0))),
            new(
                Id: 11,
                CourseId: 3,
                TopicId: 9,
                Title: "Böl ve yönet (Merge Sort / Quick Sort) karmaşıklık kanıtlarını tamamla",
                Status: StudyTaskStatus.Completed,
                Priority: TaskPriority.Normal,
                DueOn: today.AddDays(-5),
                EstimatedMinutes: 55,
                CompletedAt: _clock.CreateLocalDateTime(today.AddDays(-5), new TimeOnly(16, 45)))
        };

        var planItems = BuildWeeklyPlanItems(weekStart);
        var sessions = BuildCompletedPastSessions(weekStart, today, localNow);

        return new DemoDataset(
            UtcNow: utcNow,
            LocalNow: localNow,
            Today: today,
            WeekStart: weekStart,
            WeekEnd: weekEnd,
            Courses: courses,
            Topics: topics,
            Exams: exams.OrderBy(e => e.ScheduledAt).ThenBy(e => e.Id).ToList(),
            Tasks: tasks,
            PlanItems: planItems,
            Sessions: sessions);
    }

    public IReadOnlyList<DailyStudySummaryDto> GetWeeklySchedule(DemoDataset dataset)
    {
        var days = new List<DailyStudySummaryDto>(7);

        for (int offset = 0; offset < 7; offset++)
        {
            var date = dataset.WeekStart.AddDays(offset);
            var dayPlans = dataset.PlanItems
                .Where(p => p.PlannedOn == date)
                .OrderBy(p => p.Id)
                .ToList();

            var daySessions = dataset.Sessions
                .Where(s => _clock.ToLocalDate(s.StartAt) == date)
                .OrderBy(s => s.StartAt)
                .ThenBy(s => s.Id)
                .ToList();

            int plannedMinutes = dayPlans.Sum(p => p.PlannedMinutes);
            int actualMinutes = daySessions.Sum(s => s.DurationMinutes);

            var plannedTaskIds = dayPlans.Select(p => p.TaskId).ToHashSet();
            int matchedActualMinutes = daySessions
                .Where(s => s.TaskId.HasValue && plannedTaskIds.Contains(s.TaskId.Value))
                .Sum(s => s.DurationMinutes);

            int unplannedActualMinutes = Math.Max(0, actualMinutes - matchedActualMinutes);

            var plannedDtos = dayPlans.Select(p =>
            {
                var task = dataset.FindTask(p.TaskId);
                var course = dataset.FindCourse(p.CourseId);
                var matchedTaskActual = daySessions
                    .Where(s => s.TaskId == p.TaskId)
                    .Sum(s => s.DurationMinutes);

                return new PlannedTaskItemDto(
                    PlanId: p.Id,
                    CourseId: p.CourseId,
                    CourseName: course?.Name ?? "Ders",
                    TaskId: p.TaskId,
                    TaskTitle: task?.Title ?? "Görev",
                    TaskStatus: task?.Status ?? StudyTaskStatus.Pending,
                    PlannedMinutes: p.PlannedMinutes,
                    ActualMinutesForTaskOnDay: matchedTaskActual);
            }).ToList();

            var sessionDtos = daySessions.Select(s =>
            {
                var course = dataset.FindCourse(s.CourseId);
                var task = s.TaskId.HasValue ? dataset.FindTask(s.TaskId.Value) : null;
                var isPlannedForDay = s.TaskId.HasValue && plannedTaskIds.Contains(s.TaskId.Value);

                return new StudySessionItemDto(
                    SessionId: s.Id,
                    CourseId: s.CourseId,
                    CourseName: course?.Name ?? "Ders",
                    TaskId: s.TaskId,
                    TaskTitle: task?.Title ?? "Görev bağımsız çalışma",
                    StartLocal: _clock.ToLocalTime(s.StartAt),
                    EndLocal: _clock.ToLocalTime(s.EndAt),
                    DurationMinutes: s.DurationMinutes,
                    Source: s.Source,
                    IsMatchedToPlan: isPlannedForDay);
            }).ToList();

            days.Add(new DailyStudySummaryDto(
                Date: date,
                DayName: DemoEnumExtensions.GetTurkishDayName(date),
                ShortDayName: DemoEnumExtensions.GetTurkishShortDayName(date),
                IsToday: date == dataset.Today,
                IsFuture: date > dataset.Today,
                PlannedMinutes: plannedMinutes,
                ActualMinutes: actualMinutes,
                MatchedActualMinutes: matchedActualMinutes,
                UnplannedActualMinutes: unplannedActualMinutes,
                PlannedItems: plannedDtos,
                Sessions: sessionDtos));
        }

        return days;
    }

    public DashboardIndexViewModel BuildDashboardIndex()
    {
        var dataset = GetDataset();
        var weeklyDays = GetWeeklySchedule(dataset);

        var activeCourses = dataset.Courses
            .Where(c => !c.IsArchived)
            .OrderBy(c => c.Name, TurkishIgnoreCaseComparer)
            .ThenBy(c => c.Id)
            .ToList();

        var activeCourseIds = activeCourses.Select(c => c.Id).ToHashSet();
        var activeCourseTasks = dataset.Tasks.Where(t => activeCourseIds.Contains(t.CourseId)).ToList();

        int pendingTaskCount = activeCourseTasks.Count(t =>
            t.Status is StudyTaskStatus.Pending or StudyTaskStatus.InProgress);

        int overdueTaskCount = activeCourseTasks.Count(t => t.IsOverdue(dataset.Today));

        var upcomingExams = dataset.Exams
            .Where(e => activeCourseIds.Contains(e.CourseId) && dataset.IsUpcomingExam(e))
            .OrderBy(e => e.ScheduledAt)
            .ThenBy(e => e.Id)
            .Select(e =>
            {
                var course = dataset.FindCourse(e.CourseId);
                return new ExamListItemViewModel(
                    Id: e.Id,
                    CourseId: e.CourseId,
                    CourseName: course?.Name ?? "Ders",
                    CourseIsArchived: false,
                    Title: e.Title,
                    Type: e.Type,
                    TypeDisplayName: e.Type.GetDisplayName(),
                    TypeBadgeClass: e.Type.GetBadgeClass(),
                    ScheduledAtLocal: _clock.ToLocalTime(e.ScheduledAt),
                    DaysRemaining: dataset.GetDaysRemaining(e, _clock),
                    IsPast: false,
                    IsUpcomingWithin14Days: true,
                    ScopeSummary: e.ScopeSummary);
            })
            .ToList();

        var recentCompletedTasks = activeCourseTasks
            .Where(t => t.Status == StudyTaskStatus.Completed && t.CompletedAt.HasValue)
            .OrderByDescending(t => t.CompletedAt!.Value)
            .ThenByDescending(t => t.Id)
            .Take(5)
            .Select(t =>
            {
                var course = dataset.FindCourse(t.CourseId);
                return new DashboardCompletedTaskDto(
                    Id: t.Id,
                    CourseId: t.CourseId,
                    CourseName: course?.Name ?? "Ders",
                    Title: t.Title,
                    Priority: t.Priority,
                    PriorityDisplayName: t.Priority.GetDisplayName(),
                    PriorityBadgeClass: t.Priority.GetBadgeClass(),
                    CompletedAtLocal: _clock.ToLocalTime(t.CompletedAt!.Value));
            })
            .ToList();

        var courseProgressItems = activeCourses
            .Select(c => BuildCourseCardViewModel(dataset, c))
            .ToList();

        int weeklyActualMinutes = weeklyDays.Sum(d => d.ActualMinutes);
        int archivedCourseWeeklyActualMinutes = dataset.Sessions
            .Where(s =>
            {
                var course = dataset.FindCourse(s.CourseId);
                if (course is null || !course.IsArchived)
                {
                    return false;
                }

                var sessionDate = _clock.ToLocalDate(s.StartAt);
                return sessionDate >= dataset.WeekStart && sessionDate <= dataset.WeekEnd;
            })
            .Sum(s => s.DurationMinutes);

        return new DashboardIndexViewModel(
            Today: dataset.Today,
            WeekStart: dataset.WeekStart,
            WeekEnd: dataset.WeekEnd,
            ActiveCourseCount: activeCourses.Count,
            PendingTaskCount: pendingTaskCount,
            OverdueTaskCount: overdueTaskCount,
            PersonalWeeklyTargetMinutes: dataset.PersonalWeeklyTargetMinutes,
            WeeklyActualMinutes: weeklyActualMinutes,
            ArchivedCourseWeeklyActualMinutes: archivedCourseWeeklyActualMinutes,
            UpcomingExams: upcomingExams,
            RecentCompletedTasks: recentCompletedTasks,
            CourseProgressItems: courseProgressItems,
            WeeklyDays: weeklyDays);
    }

    public WeeklyOverviewViewModel BuildWeeklyOverview()
    {
        var dataset = GetDataset();
        var weeklyDays = GetWeeklySchedule(dataset);

        int weeklyPlannedMinutes = weeklyDays.Sum(d => d.PlannedMinutes);
        int weeklyActualMinutes = weeklyDays.Sum(d => d.ActualMinutes);
        int weeklyMatchedActualMinutes = weeklyDays.Sum(d => d.MatchedActualMinutes);
        int weeklyUnplannedActualMinutes = weeklyDays.Sum(d => d.UnplannedActualMinutes);

        int archivedCourseWeeklyActualMinutes = dataset.Sessions
            .Where(s =>
            {
                var course = dataset.FindCourse(s.CourseId);
                if (course is null || !course.IsArchived)
                {
                    return false;
                }

                var sessionDate = _clock.ToLocalDate(s.StartAt);
                return sessionDate >= dataset.WeekStart && sessionDate <= dataset.WeekEnd;
            })
            .Sum(s => s.DurationMinutes);

        int maxDailyValue = weeklyDays
            .SelectMany(d => new[] { d.PlannedMinutes, d.ActualMinutes })
            .DefaultIfEmpty(60)
            .Max();

        int chartScaleMaxMinutes = Math.Max(60, maxDailyValue);

        return new WeeklyOverviewViewModel(
            Today: dataset.Today,
            WeekStart: dataset.WeekStart,
            WeekEnd: dataset.WeekEnd,
            PersonalWeeklyTargetMinutes: dataset.PersonalWeeklyTargetMinutes,
            WeeklyPlannedMinutes: weeklyPlannedMinutes,
            WeeklyActualMinutes: weeklyActualMinutes,
            WeeklyMatchedActualMinutes: weeklyMatchedActualMinutes,
            WeeklyUnplannedActualMinutes: weeklyUnplannedActualMinutes,
            ArchivedCourseWeeklyActualMinutes: archivedCourseWeeklyActualMinutes,
            ChartScaleMaxMinutes: chartScaleMaxMinutes,
            Days: weeklyDays);
    }

    public CoursesIndexViewModel BuildCoursesIndex(string? searchQuery, string? statusFilter)
    {
        var dataset = GetDataset();

        string normalizedQuery = (searchQuery ?? string.Empty).Trim();
        string normalizedStatus = CourseStatusFilterValues.Normalize(statusFilter);

        IEnumerable<DemoCourse> filteredCourses = dataset.Courses;

        if (string.Equals(normalizedStatus, CourseStatusFilterValues.Active, StringComparison.OrdinalIgnoreCase))
        {
            filteredCourses = filteredCourses.Where(c => !c.IsArchived);
        }
        else if (string.Equals(normalizedStatus, CourseStatusFilterValues.Archived, StringComparison.OrdinalIgnoreCase))
        {
            filteredCourses = filteredCourses.Where(c => c.IsArchived);
        }

        if (!string.IsNullOrEmpty(normalizedQuery))
        {
            filteredCourses = filteredCourses.Where(c =>
                TurkishCulture.CompareInfo.IndexOf(
                    c.Name,
                    normalizedQuery,
                    CompareOptions.IgnoreCase) >= 0);
        }

        var sortedCourseCards = filteredCourses
            .OrderBy(c => c.Name, TurkishIgnoreCaseComparer)
            .ThenBy(c => c.Id)
            .Select(c => BuildCourseCardViewModel(dataset, c))
            .ToList();

        return new CoursesIndexViewModel(
            SearchQuery: normalizedQuery,
            StatusFilter: normalizedStatus,
            TotalCourseCount: dataset.Courses.Count,
            ActiveCourseCount: dataset.Courses.Count(c => !c.IsArchived),
            ArchivedCourseCount: dataset.Courses.Count(c => c.IsArchived),
            PersonalWeeklyTargetMinutes: dataset.PersonalWeeklyTargetMinutes,
            Courses: sortedCourseCards);
    }

    private CourseCardViewModel BuildCourseCardViewModel(DemoDataset dataset, DemoCourse c)
    {
        var courseTopics = dataset.Topics.Where(t => t.CourseId == c.Id).ToList();
        int totalTopics = courseTopics.Count;
        int completedTopics = courseTopics.Count(t => t.Status == TopicStatus.Completed);
        int inProgressTopics = courseTopics.Count(t => t.Status == TopicStatus.InProgress);
        int notStartedTopics = courseTopics.Count(t => t.Status == TopicStatus.NotStarted);

        double progressPercent = dataset.CalculateTopicProgressPercent(c.Id);

        int upcomingExams = dataset.Exams.Count(e => e.CourseId == c.Id && dataset.IsUpcomingExam(e));
        int pendingTasks = dataset.Tasks.Count(t =>
            t.CourseId == c.Id &&
            t.Status is StudyTaskStatus.Pending or StudyTaskStatus.InProgress);

        return new CourseCardViewModel(
            Id: c.Id,
            Name: c.Name,
            Description: c.Description,
            IsArchived: c.IsArchived,
            WeeklyTargetMinutes: c.WeeklyTargetMinutes,
            TotalTopicCount: totalTopics,
            CompletedTopicCount: completedTopics,
            InProgressTopicCount: inProgressTopics,
            NotStartedTopicCount: notStartedTopics,
            TopicProgressPercent: progressPercent,
            UpcomingExamCount: upcomingExams,
            PendingTaskCount: pendingTasks,
            IsSessionCourse: _courseStore.IsSessionCourse(c.Id));
    }

    public CourseDetailsViewModel? BuildCourseDetails(int courseId)
    {
        var dataset = GetDataset();
        var course = dataset.FindCourse(courseId);
        if (course is null)
        {
            return null;
        }

        var courseTopics = dataset.Topics
            .Where(t => t.CourseId == course.Id)
            .OrderBy(t => t.SortOrder)
            .ThenBy(t => t.Id)
            .Select(t => new CourseTopicItemViewModel(
                Id: t.Id,
                SortOrder: t.SortOrder,
                Title: t.Title,
                Status: t.Status,
                StatusDisplayName: t.Status.GetDisplayName(),
                StatusBadgeClass: t.Status.GetBadgeClass(),
                StatusDescriptionText: t.Status.GetDisplayName(),
                Notes: t.Notes,
                ReviewOn: t.ReviewOn,
                RelatedTaskCount: dataset.Tasks.Count(task => task.TopicId == t.Id)))
            .ToList();

        var courseExams = dataset.Exams
            .Where(e => e.CourseId == course.Id)
            .OrderBy(e => e.ScheduledAt)
            .ThenBy(e => e.Id)
            .Select(e => new CourseExamItemViewModel(
                Id: e.Id,
                Title: e.Title,
                Type: e.Type,
                TypeDisplayName: e.Type.GetDisplayName(),
                TypeBadgeClass: e.Type.GetBadgeClass(),
                ScheduledAtLocal: _clock.ToLocalTime(e.ScheduledAt),
                DaysRemaining: dataset.GetDaysRemaining(e, _clock),
                IsPast: dataset.IsPastExam(e),
                IsUpcomingWithin14Days: dataset.IsUpcomingExam(e),
                ScopeSummary: e.ScopeSummary))
            .ToList();

        int weeklyPlannedMinutes = dataset.PlanItems
            .Where(p => p.CourseId == course.Id && p.PlannedOn >= dataset.WeekStart && p.PlannedOn <= dataset.WeekEnd)
            .Sum(p => p.PlannedMinutes);

        int weeklyActualMinutes = dataset.Sessions
            .Where(s =>
            {
                if (s.CourseId != course.Id)
                {
                    return false;
                }

                var sessionDate = _clock.ToLocalDate(s.StartAt);
                return sessionDate >= dataset.WeekStart && sessionDate <= dataset.WeekEnd;
            })
            .Sum(s => s.DurationMinutes);

        int completedTopics = courseTopics.Count(t => t.Status == TopicStatus.Completed);
        int inProgressTopics = courseTopics.Count(t => t.Status == TopicStatus.InProgress);
        int notStartedTopics = courseTopics.Count(t => t.Status == TopicStatus.NotStarted);

        int pendingTasks = dataset.Tasks.Count(t =>
            t.CourseId == course.Id &&
            t.Status is StudyTaskStatus.Pending or StudyTaskStatus.InProgress);
        int completedTasks = dataset.Tasks.Count(t =>
            t.CourseId == course.Id &&
            t.Status == StudyTaskStatus.Completed);

        return new CourseDetailsViewModel(
            Id: course.Id,
            Name: course.Name,
            Description: course.Description,
            IsArchived: course.IsArchived,
            WeeklyTargetMinutes: course.WeeklyTargetMinutes,
            WeeklyPlannedMinutes: weeklyPlannedMinutes,
            WeeklyActualMinutes: weeklyActualMinutes,
            TotalTopicCount: courseTopics.Count,
            CompletedTopicCount: completedTopics,
            InProgressTopicCount: inProgressTopics,
            NotStartedTopicCount: notStartedTopics,
            TopicProgressPercent: dataset.CalculateTopicProgressPercent(course.Id),
            PendingTaskCount: pendingTasks,
            CompletedTaskCount: completedTasks,
            Topics: courseTopics,
            Exams: courseExams,
            IsSessionCourse: _courseStore.IsSessionCourse(course.Id));
    }

    public ExamsIndexViewModel BuildExamsIndex(string? periodFilter, int? courseId)
    {
        var dataset = GetDataset();
        string normalizedPeriod = ExamFilterValues.Normalize(periodFilter);

        var courseOptions = dataset.Courses
            .OrderBy(c => c.Name, TurkishIgnoreCaseComparer)
            .ThenBy(c => c.Id)
            .Select(c => new ExamCourseOptionViewModel(c.Id, c.Name, c.IsArchived))
            .ToList();

        var selectedCourse = courseId.HasValue ? dataset.FindCourse(courseId.Value) : null;

        IEnumerable<DemoExam> query = dataset.Exams;

        if (courseId.HasValue)
        {
            query = query.Where(e => e.CourseId == courseId.Value);
        }

        if (string.Equals(normalizedPeriod, ExamFilterValues.Upcoming, StringComparison.OrdinalIgnoreCase))
        {
            query = query.Where(dataset.IsUpcomingExam);
        }
        else if (string.Equals(normalizedPeriod, ExamFilterValues.Past, StringComparison.OrdinalIgnoreCase))
        {
            query = query.Where(dataset.IsPastExam);
        }

        var examItems = query
            .OrderBy(e => e.ScheduledAt)
            .ThenBy(e => e.Id)
            .Select(e =>
            {
                var course = dataset.FindCourse(e.CourseId);
                return new ExamListItemViewModel(
                    Id: e.Id,
                    CourseId: e.CourseId,
                    CourseName: course?.Name ?? "Ders",
                    CourseIsArchived: course?.IsArchived ?? false,
                    Title: e.Title,
                    Type: e.Type,
                    TypeDisplayName: e.Type.GetDisplayName(),
                    TypeBadgeClass: e.Type.GetBadgeClass(),
                    ScheduledAtLocal: _clock.ToLocalTime(e.ScheduledAt),
                    DaysRemaining: dataset.GetDaysRemaining(e, _clock),
                    IsPast: dataset.IsPastExam(e),
                    IsUpcomingWithin14Days: dataset.IsUpcomingExam(e),
                    ScopeSummary: e.ScopeSummary);
            })
            .ToList();

        return new ExamsIndexViewModel(
            PeriodFilter: normalizedPeriod,
            SelectedCourseId: courseId,
            SelectedCourseName: selectedCourse?.Name,
            TotalExamCount: dataset.Exams.Count,
            UpcomingExamCount: dataset.Exams.Count(dataset.IsUpcomingExam),
            PastExamCount: dataset.Exams.Count(dataset.IsPastExam),
            CourseOptions: courseOptions,
            Exams: examItems);
    }

    public ExamDetailsViewModel? BuildExamDetails(int examId)
    {
        var dataset = GetDataset();
        var exam = dataset.FindExam(examId);
        if (exam is null)
        {
            return null;
        }

        var course = dataset.FindCourse(exam.CourseId);
        if (course is null)
        {
            return null;
        }

        var relatedTopics = dataset.Topics
            .Where(t => t.CourseId == course.Id)
            .OrderBy(t => t.SortOrder)
            .ThenBy(t => t.Id)
            .Select(t => new CourseTopicItemViewModel(
                Id: t.Id,
                SortOrder: t.SortOrder,
                Title: t.Title,
                Status: t.Status,
                StatusDisplayName: t.Status.GetDisplayName(),
                StatusBadgeClass: t.Status.GetBadgeClass(),
                StatusDescriptionText: t.Status.GetDisplayName(),
                Notes: t.Notes,
                ReviewOn: t.ReviewOn,
                RelatedTaskCount: dataset.Tasks.Count(task => task.TopicId == t.Id)))
            .ToList();

        return new ExamDetailsViewModel(
            Id: exam.Id,
            CourseId: course.Id,
            CourseName: course.Name,
            CourseIsArchived: course.IsArchived,
            CourseWeeklyTargetMinutes: course.WeeklyTargetMinutes,
            Title: exam.Title,
            Type: exam.Type,
            TypeDisplayName: exam.Type.GetDisplayName(),
            TypeBadgeClass: exam.Type.GetBadgeClass(),
            ScheduledAtLocal: _clock.ToLocalTime(exam.ScheduledAt),
            DaysRemaining: dataset.GetDaysRemaining(exam, _clock),
            IsPast: dataset.IsPastExam(exam),
            IsUpcomingWithin14Days: dataset.IsUpcomingExam(exam),
            ScopeSummary: exam.ScopeSummary,
            RelatedCourseTopics: relatedTopics);
    }

    private static List<DemoPlanItem> BuildWeeklyPlanItems(DateOnly weekStart)
    {
        return
        [
            new(1, 1, 6, weekStart.AddDays(0), 60),
            new(2, 2, 8, weekStart.AddDays(0), 60),
            new(3, 3, 7, weekStart.AddDays(1), 90),
            new(4, 1, 9, weekStart.AddDays(2), 45),
            new(5, 2, 2, weekStart.AddDays(2), 60),
            new(6, 3, 3, weekStart.AddDays(3), 75),
            new(7, 1, 1, weekStart.AddDays(4), 90),
            new(8, 2, 4, weekStart.AddDays(5), 60),
            new(9, 1, 5, weekStart.AddDays(6), 45)
        ];
    }

    private List<DemoSession> BuildCompletedPastSessions(
        DateOnly weekStart,
        DateOnly today,
        DateTimeOffset localNow)
    {
        var sessions = new List<DemoSession>();
        int nextId = 1;

        void TryAddSessionOnDay(
            DateOnly day,
            TimeOnly preferredStart,
            int durationMinutes,
            int courseId,
            int? taskId,
            int? topicId,
            SessionSource source)
        {
            if (day > today)
            {
                return;
            }

            if (day < today)
            {
                var start = _clock.CreateLocalDateTime(day, preferredStart);
                var end = start.AddMinutes(durationMinutes);
                if (end <= localNow)
                {
                    sessions.Add(new DemoSession(nextId++, courseId, taskId, topicId, start, end, source));
                }

                return;
            }

            var preferredStartDt = _clock.CreateLocalDateTime(day, preferredStart);
            var preferredEndDt = preferredStartDt.AddMinutes(durationMinutes);

            if (preferredEndDt <= localNow)
            {
                sessions.Add(new DemoSession(nextId++, courseId, taskId, topicId, preferredStartDt, preferredEndDt, source));
                return;
            }

            var elapsedTodayMinutes = (int)localNow.TimeOfDay.TotalMinutes;
            if (elapsedTodayMinutes >= durationMinutes + 5)
            {
                var safeEndMinute = elapsedTodayMinutes - 5;
                var safeStartMinute = safeEndMinute - durationMinutes;
                var start = _clock.CreateLocalDateTime(day, TimeOnly.FromTimeSpan(TimeSpan.FromMinutes(safeStartMinute)));
                var end = start.AddMinutes(durationMinutes);
                sessions.Add(new DemoSession(nextId++, courseId, taskId, topicId, start, end, source));
            }
        }

        // Pazartesi: 45 + 60 + 15 = 120 dk
        TryAddSessionOnDay(weekStart.AddDays(0), new TimeOnly(10, 0), 45, 1, 6, 2, SessionSource.Timer);
        TryAddSessionOnDay(weekStart.AddDays(0), new TimeOnly(14, 0), 60, 2, 8, 5, SessionSource.Manual);
        TryAddSessionOnDay(weekStart.AddDays(0), new TimeOnly(19, 30), 15, 1, null, 1, SessionSource.Timer);

        // Salı: 75 + 30 = 105 dk
        TryAddSessionOnDay(weekStart.AddDays(1), new TimeOnly(11, 0), 75, 3, 7, 9, SessionSource.Timer);
        TryAddSessionOnDay(weekStart.AddDays(1), new TimeOnly(16, 0), 30, 1, 6, 2, SessionSource.Manual);

        // Çarşamba: 45 + 45 = 90 dk
        TryAddSessionOnDay(weekStart.AddDays(2), new TimeOnly(9, 30), 45, 1, 9, 1, SessionSource.Timer);
        TryAddSessionOnDay(weekStart.AddDays(2), new TimeOnly(15, 0), 45, 2, 2, 6, SessionSource.Timer);

        // Perşembe: 60 dk
        TryAddSessionOnDay(weekStart.AddDays(3), new TimeOnly(13, 0), 60, 3, 3, 10, SessionSource.Timer);

        // Cuma: 60 dk
        TryAddSessionOnDay(weekStart.AddDays(4), new TimeOnly(10, 30), 60, 1, 1, 3, SessionSource.Timer);

        // Cumartesi: 45 dk aktif ders + 30 dk arşivli ders (#4 Ayrık Matematik) geçmiş oturumu = 75 dk
        TryAddSessionOnDay(weekStart.AddDays(5), new TimeOnly(11, 0), 45, 2, 4, 7, SessionSource.Manual);
        TryAddSessionOnDay(weekStart.AddDays(5), new TimeOnly(17, 0), 30, 4, null, 13, SessionSource.Manual);

        // Pazar (weekStart.AddDays(6)) kasıtlı olarak oturumsuz (0 dk) bırakılmıştır.

        var prevWeekDay1 = weekStart.AddDays(-3);
        var prevWeekDay2 = weekStart.AddDays(-2);
        TryAddSessionOnDay(prevWeekDay1, new TimeOnly(14, 0), 50, 1, 9, 1, SessionSource.Manual);
        TryAddSessionOnDay(prevWeekDay2, new TimeOnly(16, 0), 60, 4, 10, 13, SessionSource.Manual);

        return sessions
            .OrderByDescending(s => s.StartAt)
            .ThenByDescending(s => s.Id)
            .ToList();
    }
}
