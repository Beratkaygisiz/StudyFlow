using Microsoft.AspNetCore.Mvc;
using StudyFlow.Web.DemoData;

namespace StudyFlow.Web.Controllers;

/// <summary>
/// İlk ders tesliminde ana özet ekranı (/ ve /Dashboard) ile 7 günlük haftalık
/// çalışma özeti ekranını (/Dashboard/Weekly) sunan salt okunur MVC Controller sınıfı.
/// </summary>
public sealed class DashboardController : Controller
{
    private readonly IDemoDataProvider _demoDataProvider;

    public DashboardController(IDemoDataProvider demoDataProvider)
    {
        _demoDataProvider = demoDataProvider;
    }

    /// <summary>
    /// GET / ve /Dashboard ve /Dashboard/Index
    /// Aktif ders, bekleyen/geciken görev, haftalık hedef/gerçekleşen süre kartlarını,
    /// önümüzdeki 14 gündeki sınavları, son 5 tamamlanan görevi ve ders konu ilerlemelerini gösterir (FR-026, FR-028).
    /// </summary>
    [HttpGet]
    public IActionResult Index()
    {
        var viewModel = _demoDataProvider.BuildDashboardIndex();
        return View(viewModel);
    }

    /// <summary>
    /// GET /Dashboard/Weekly
    /// Pazartesi-Pazar 7 günlük planlanan dakika, bitmiş oturumlardan hesaplanan gerçek dakika,
    /// fark tablosu, erişilebilir çubuk grafik ve haftalık hedef ilerlemesini gösterir (FR-019, FR-020, FR-025, FR-026, FR-028).
    /// </summary>
    [HttpGet]
    public IActionResult Weekly()
    {
        var viewModel = _demoDataProvider.BuildWeeklyOverview();
        return View(viewModel);
    }
}
