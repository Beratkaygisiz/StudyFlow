using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace StudyFlow.Web.DemoData;

/// <summary>
/// Salt okunur başlangıç (seed) örnek dersleri ile kullanıcının mevcut tarayıcı oturumunda (Session)
/// geçici olarak eklediği dersleri birleştiren mağaza sözleşmesi (Faz 1B / FR-037..FR-039).
/// </summary>
public interface IDemoCourseStore
{
    /// <summary>
    /// Salt okunur seed dersler ile aktif Session içindeki geçici derslerin birleşimini döndürür.
    /// </summary>
    IReadOnlyList<DemoCourse> GetAllCourses();

    /// <summary>
    /// Mevcut oturuma (Session) yeni, aktif bir ders ekler ve üretilen dersi yeni kimliğiyle döndürür.
    /// </summary>
    DemoCourse AddCourse(string trimmedName, int weeklyTargetMinutes);

    /// <summary>
    /// Belirtilen ders kimliğinin bu oturumda geçici olarak eklenip eklenmediğini döndürür.
    /// </summary>
    bool IsSessionCourse(int courseId);
}

/// <summary>
/// Değiştirilebilir static/singleton koleksiyon kullanmadan, yeni dersleri yalnızca
/// <see cref="ISession"/> içinde JSON olarak saklayan ve salt okunur seed verisiyle birleştiren sınıf.
/// </summary>
public sealed class DemoCourseStore : IDemoCourseStore
{
    private const string SessionCoursesKey = "StudyFlow.SessionCourses.v1";

    /// <summary>
    /// Salt okunur, değiştirilemez başlangıç (seed) ders kayıtları (Id: 1..5).
    /// </summary>
    private static readonly IReadOnlyList<DemoCourse> SeedCourses =
    [
        new(
            Id: 1,
            Name: "Web Programlama",
            IsArchived: false,
            WeeklyTargetMinutes: 240,
            Description: "ASP.NET Core MVC mimarisi, Razor görünüm motoru ve erişilebilir form tasarımı."),
        new(
            Id: 2,
            Name: "Veritabanı Yönetim Sistemleri",
            IsArchived: false,
            WeeklyTargetMinutes: 180,
            Description: "İlişkisel şema tasarımı, normalizasyon, indeksleme ve SQL sorgu planları."),
        new(
            Id: 3,
            Name: "Algoritma Analizi ve Tasarımı",
            IsArchived: false,
            WeeklyTargetMinutes: 180,
            Description: "Asimptotik karmaşıklık analizi, böl ve yönet, dinamik programlama ve çizge algoritmaları."),
        new(
            Id: 4,
            Name: "Ayrık Matematik",
            IsArchived: true,
            WeeklyTargetMinutes: 120,
            Description: "Önermeler mantığı, ispat yöntemleri, bağıntılar ve kombinatorik."),
        new(
            Id: 5,
            Name: "İşletim Sistemleri",
            IsArchived: false,
            WeeklyTargetMinutes: 120,
            Description: "Süreç yönetimi, iş parçacığı senkronizasyonu, bellek adresleme ve dosya sistemi temelleri.")
    ];

    private static readonly int MaxSeedCourseId = SeedCourses.Max(c => c.Id);

    private readonly IHttpContextAccessor _httpContextAccessor;

    public DemoCourseStore(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public IReadOnlyList<DemoCourse> GetAllCourses()
    {
        var sessionCourses = ReadSessionCourses();
        if (sessionCourses.Count == 0)
        {
            return SeedCourses;
        }

        var combined = new List<DemoCourse>(SeedCourses.Count + sessionCourses.Count);
        combined.AddRange(SeedCourses);
        combined.AddRange(sessionCourses);
        return combined;
    }

    public DemoCourse AddCourse(string trimmedName, int weeklyTargetMinutes)
    {
        var session = _httpContextAccessor.HttpContext?.Session
            ?? throw new InvalidOperationException("Session yapılandırması etkin değil.");

        var sessionCourses = ReadSessionCourses();

        int maxExistingId = Math.Max(
            MaxSeedCourseId,
            sessionCourses.Select(c => c.Id).DefaultIfEmpty(0).Max());

        int nextId = maxExistingId + 1;

        var newCourse = new DemoCourse(
            Id: nextId,
            Name: trimmedName,
            IsArchived: false,
            WeeklyTargetMinutes: weeklyTargetMinutes,
            Description: "Bu tarayıcı oturumunda (Session) geçici olarak oluşturulan aktif ders kaydı.");

        sessionCourses.Add(newCourse);

        string json = JsonSerializer.Serialize(sessionCourses);
        session.SetString(SessionCoursesKey, json);

        return newCourse;
    }

    public bool IsSessionCourse(int courseId) => courseId > MaxSeedCourseId;

    private List<DemoCourse> ReadSessionCourses()
    {
        var session = _httpContextAccessor.HttpContext?.Session;
        if (session is null)
        {
            return [];
        }

        string? json = session.GetString(SessionCoursesKey);
        if (string.IsNullOrWhiteSpace(json))
        {
            return [];
        }

        try
        {
            return JsonSerializer.Deserialize<List<DemoCourse>>(json) ?? [];
        }
        catch (JsonException)
        {
            return [];
        }
    }
}
