namespace StudyFlow.Web.DemoData;

/// <summary>
/// Uygulama genelinde zaman ve yerel gün/hafta hesaplamalarını soyutlayan saat arayüzü.
/// Varsayılan zaman dilimi Europe/Istanbul ve hafta başlangıcı Pazartesi olarak kullanılır.
/// </summary>
public interface IClock
{
    DateTimeOffset UtcNow { get; }
    TimeZoneInfo TimeZone { get; }
    DateTimeOffset LocalNow { get; }
    DateOnly Today { get; }
    DateOnly CurrentWeekStart { get; }
    DateOnly CurrentWeekEnd { get; }

    DateOnly ToLocalDate(DateTimeOffset timestamp);
    DateTimeOffset ToLocalTime(DateTimeOffset timestamp);
    DateOnly GetStartOfWeek(DateOnly date);
    DateTimeOffset CreateLocalDateTime(DateOnly date, TimeOnly time);
}

public sealed class SystemClock : IClock
{
    private static readonly TimeZoneInfo TurkeyTimeZone = ResolveTurkeyTimeZone();

    public DateTimeOffset UtcNow
    {
        get
        {
            var now = DateTimeOffset.UtcNow;
            return new DateTimeOffset(
                now.Year,
                now.Month,
                now.Day,
                now.Hour,
                now.Minute,
                now.Second,
                TimeSpan.Zero);
        }
    }

    public TimeZoneInfo TimeZone => TurkeyTimeZone;

    public DateTimeOffset LocalNow => TimeZoneInfo.ConvertTime(UtcNow, TimeZone);

    public DateOnly Today => DateOnly.FromDateTime(LocalNow.DateTime);

    public DateOnly CurrentWeekStart => GetStartOfWeek(Today);

    public DateOnly CurrentWeekEnd => CurrentWeekStart.AddDays(6);

    public DateOnly ToLocalDate(DateTimeOffset timestamp)
    {
        var local = TimeZoneInfo.ConvertTime(timestamp, TimeZone);
        return DateOnly.FromDateTime(local.DateTime);
    }

    public DateTimeOffset ToLocalTime(DateTimeOffset timestamp)
    {
        return TimeZoneInfo.ConvertTime(timestamp, TimeZone);
    }

    public DateOnly GetStartOfWeek(DateOnly date)
    {
        int diff = ((int)date.DayOfWeek - (int)DayOfWeek.Monday + 7) % 7;
        return date.AddDays(-diff);
    }

    public DateTimeOffset CreateLocalDateTime(DateOnly date, TimeOnly time)
    {
        var unspecified = date.ToDateTime(time, DateTimeKind.Unspecified);
        var offset = TimeZone.GetUtcOffset(unspecified);
        return new DateTimeOffset(unspecified, offset);
    }

    private static TimeZoneInfo ResolveTurkeyTimeZone()
    {
        string[] candidateIds = ["Europe/Istanbul", "Turkey Standard Time"];
        foreach (var id in candidateIds)
        {
            try
            {
                return TimeZoneInfo.FindSystemTimeZoneById(id);
            }
            catch (TimeZoneNotFoundException)
            {
                // Sonraki aday kimliği dene.
            }
            catch (InvalidTimeZoneException)
            {
                // Sonraki aday kimliği dene.
            }
        }

        return TimeZoneInfo.CreateCustomTimeZone(
            "Europe/Istanbul",
            TimeSpan.FromHours(3),
            "Türkiye Saati (UTC+03:00)",
            "Türkiye Standart Saati");
    }
}
