using Microsoft.AspNetCore.Mvc;
using StudyFlow.Web.DemoData;
using StudyFlow.Web.ViewModels;

namespace StudyFlow.Web.Controllers;

/// <summary>
/// İlk ders tesliminde sınavların filtrelenerek listelenmesi (/Exams) ve
/// tekil sınav detayının (/Exams/Details/{id}) ders bağlantısıyla gösterilmesi
/// sorumluluğunu üstlenen salt okunur MVC Controller sınıfı.
/// </summary>
public sealed class ExamsController : Controller
{
    private readonly IDemoDataProvider _demoDataProvider;

    public ExamsController(IDemoDataProvider demoDataProvider)
    {
        _demoDataProvider = demoDataProvider;
    }

    /// <summary>
    /// GET /Exams ve /Exams/Index
    /// Sınavları zaman aralığı (Upcoming / Past / All) ve isteğe bağlı ders kimliği (courseId)
    /// filtreleriyle birlikte (AND mantığında) süzer; ScheduledAt, ardından Id artan sırayla (FR-007) listeler.
    /// </summary>
    [HttpGet]
    public IActionResult Index(
        [FromQuery] string? filter,
        [FromQuery] string? period,
        [FromQuery] int? courseId)
    {
        string? rawPeriod = !string.IsNullOrWhiteSpace(filter) ? filter : period;
        var viewModel = _demoDataProvider.BuildExamsIndex(rawPeriod, courseId);
        return View(viewModel);
    }

    /// <summary>
    /// GET /Exams/Details/{id}
    /// İstenen sınav kimliğine (id) göre sınav başlığını, türünü, yerel tarih/saatini,
    /// kalan gün bilgisini ve ilişkili ders bağlantısını gösterir (FR-007).
    /// Olmayan Id değerlerinde HTTP 404 döndürür (FR-032).
    /// </summary>
    [HttpGet]
    public IActionResult Details(int? id)
    {
        if (!id.HasValue || id.Value <= 0)
        {
            Response.StatusCode = StatusCodes.Status404NotFound;
            return View("NotFound", new NotFoundViewModel(
                ResourceName: "Sınav",
                RequestedId: id,
                Message: "Görüntülemek istediğiniz sınav kimliği geçersiz veya belirtilmemiş.",
                ReturnUrl: Url.Action(nameof(Index), "Exams") ?? "/Exams",
                ReturnLabel: "Sınav Listesine Dön"));
        }

        var viewModel = _demoDataProvider.BuildExamDetails(id.Value);
        if (viewModel is null)
        {
            Response.StatusCode = StatusCodes.Status404NotFound;
            return View("NotFound", new NotFoundViewModel(
                ResourceName: "Sınav",
                RequestedId: id.Value,
                Message: $"#{id.Value} kimliğine sahip sınav kaydı bulunamadı.",
                ReturnUrl: Url.Action(nameof(Index), "Exams") ?? "/Exams",
                ReturnLabel: "Sınav Listesine Dön"));
        }

        return View(viewModel);
    }
}
