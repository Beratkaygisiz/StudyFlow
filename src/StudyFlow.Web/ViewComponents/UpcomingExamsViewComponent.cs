using Microsoft.AspNetCore.Mvc;
using StudyFlow.Web.DemoData;
using StudyFlow.Web.ViewModels;

namespace StudyFlow.Web.ViewComponents;

/// <summary>
/// Önümüzdeki 14 gün içindeki ([now, now + 14 gün)) yaklaşan sınavları hazırlayıp sıralayan ViewComponent.
/// Dashboard/Index ekranında tüm aktif derslerin genel yaklaşan sınavlarını;
/// Courses/Details ekranında ise yalnızca görüntülenen dersin yaklaşan sınavlarını sunar.
/// </summary>
public sealed class UpcomingExamsViewComponent : ViewComponent
{
    private readonly IDemoDataProvider _demoDataProvider;
    private readonly IClock _clock;

    public UpcomingExamsViewComponent(IDemoDataProvider demoDataProvider, IClock clock)
    {
        _demoDataProvider = demoDataProvider;
        _clock = clock;
    }

    /// <summary>
    /// Veri kaynağı (IDemoDataProvider ve IClock) bellek/Session üzerinde senkron çalıştığından
    /// yapay Task sarmalaması yerine doğrudan senkron Invoke tercih edilmiştir.
    /// </summary>
    /// <param name="courseId">Belirtilirse yalnız ilgili dersin yaklaşan sınavları filtrelenir; null ise tüm aktif dersler taranır.</param>
    /// <param name="maxResults">Gösterilecek en fazla sınav sayısı (varsayılan: 3).</param>
    public IViewComponentResult Invoke(int? courseId = null, int maxResults = 3)
    {
        int effectiveMax = maxResults > 0 ? maxResults : 3;
        var dataset = _demoDataProvider.GetDataset();

        var nowUtc = _clock.UtcNow;
        var windowEndUtc = nowUtc.AddDays(14);

        if (courseId.HasValue)
        {
            var course = dataset.FindCourse(courseId.Value);
            if (course is null)
            {
                return View(new UpcomingExamsComponentViewModel(
                    CourseId: courseId.Value,
                    CourseName: null,
                    IsArchivedCourse: false,
                    MaxResults: effectiveMax,
                    TotalMatchingUpcomingCount: 0,
                    Exams: Array.Empty<ExamListItemViewModel>()));
            }

            // FR-028: Arşivli dersler operasyonel yaklaşan sınav özetlerine dahil edilmez.
            if (course.IsArchived)
            {
                return View(new UpcomingExamsComponentViewModel(
                    CourseId: course.Id,
                    CourseName: course.Name,
                    IsArchivedCourse: true,
                    MaxResults: effectiveMax,
                    TotalMatchingUpcomingCount: 0,
                    Exams: Array.Empty<ExamListItemViewModel>()));
            }

            var matchingCourseExams = dataset.Exams
                .Where(e =>
                    e.CourseId == course.Id &&
                    e.ScheduledAt >= nowUtc &&
                    e.ScheduledAt < windowEndUtc)
                .OrderBy(e => e.ScheduledAt)
                .ThenBy(e => e.Id)
                .ToList();

            var courseExamItems = matchingCourseExams
                .Take(effectiveMax)
                .Select(e => MapToExamListItem(e, course, dataset))
                .ToList();

            return View(new UpcomingExamsComponentViewModel(
                CourseId: course.Id,
                CourseName: course.Name,
                IsArchivedCourse: false,
                MaxResults: effectiveMax,
                TotalMatchingUpcomingCount: matchingCourseExams.Count,
                Exams: courseExamItems));
        }

        var activeCoursesById = dataset.Courses
            .Where(c => !c.IsArchived)
            .ToDictionary(c => c.Id);

        var matchingGeneralExams = dataset.Exams
            .Where(e =>
                activeCoursesById.ContainsKey(e.CourseId) &&
                e.ScheduledAt >= nowUtc &&
                e.ScheduledAt < windowEndUtc)
            .OrderBy(e => e.ScheduledAt)
            .ThenBy(e => e.Id)
            .ToList();

        var generalExamItems = matchingGeneralExams
            .Take(effectiveMax)
            .Select(e => MapToExamListItem(e, activeCoursesById[e.CourseId], dataset))
            .ToList();

        return View(new UpcomingExamsComponentViewModel(
            CourseId: null,
            CourseName: null,
            IsArchivedCourse: false,
            MaxResults: effectiveMax,
            TotalMatchingUpcomingCount: matchingGeneralExams.Count,
            Exams: generalExamItems));
    }

    private ExamListItemViewModel MapToExamListItem(DemoExam exam, DemoCourse course, DemoDataset dataset)
    {
        return new ExamListItemViewModel(
            Id: exam.Id,
            CourseId: exam.CourseId,
            CourseName: course.Name,
            CourseIsArchived: course.IsArchived,
            Title: exam.Title,
            Type: exam.Type,
            TypeDisplayName: exam.Type.GetDisplayName(),
            TypeBadgeClass: exam.Type.GetBadgeClass(),
            ScheduledAtLocal: _clock.ToLocalTime(exam.ScheduledAt),
            DaysRemaining: dataset.GetDaysRemaining(exam, _clock),
            IsPast: false,
            IsUpcomingWithin14Days: true,
            ScopeSummary: exam.ScopeSummary);
    }
}
