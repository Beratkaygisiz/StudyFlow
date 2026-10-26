using Microsoft.AspNetCore.Mvc;
using StudyFlow.Web.DemoData;
using StudyFlow.Web.ViewModels;

namespace StudyFlow.Web.Controllers;

/// <summary>
/// İlk ders tesliminde derslerin filtrelenerek listelenmesi (/Courses),
/// tekil ders detayının gösterilmesi (/Courses/Details/{id}) ve Faz 1B kapsamında
/// tarayıcı oturumuna (Session) geçici yeni ders eklenmesi (/Courses/Create)
/// işlemlerini yürüten MVC Controller sınıfı.
/// </summary>
public sealed class CoursesController : Controller
{
    private readonly IDemoDataProvider _demoDataProvider;
    private readonly IDemoCourseStore _courseStore;

    public CoursesController(
        IDemoDataProvider demoDataProvider,
        IDemoCourseStore courseStore)
    {
        _demoDataProvider = demoDataProvider;
        _courseStore = courseStore;
    }

    /// <summary>
    /// GET /Courses ve /Courses/Index
    /// Salt okunur seed dersler ile Session'da geçici saklanan dersleri birleştirerek
    /// arama (q) ve durum (status) filtreleriyle ad, ardından Id sırasıyla (FR-002) listeler.
    /// </summary>
    [HttpGet]
    public IActionResult Index([FromQuery] string? q, [FromQuery] string? status)
    {
        var viewModel = _demoDataProvider.BuildCoursesIndex(q, status);
        return View(viewModel);
    }

    /// <summary>
    /// GET /Courses/Details/{id}
    /// Seed veya mevcut Session içinde bulunan dersin detaylarını gösterir.
    /// Olmayan Id için HTTP 404 döndürür.
    /// </summary>
    [HttpGet]
    public IActionResult Details(int? id)
    {
        if (!id.HasValue || id.Value <= 0)
        {
            Response.StatusCode = StatusCodes.Status404NotFound;
            return View("NotFound", new NotFoundViewModel(
                ResourceName: "Ders",
                RequestedId: id,
                Message: "Görüntülemek istediğiniz ders kimliği geçersiz veya belirtilmemiş.",
                ReturnUrl: Url.Action(nameof(Index), "Courses") ?? "/Courses",
                ReturnLabel: "Ders Listesine Dön"));
        }

        var viewModel = _demoDataProvider.BuildCourseDetails(id.Value);
        if (viewModel is null)
        {
            Response.StatusCode = StatusCodes.Status404NotFound;
            return View("NotFound", new NotFoundViewModel(
                ResourceName: "Ders",
                RequestedId: id.Value,
                Message: $"#{id.Value} kimliğine sahip ders kaydı bulunamadı.",
                ReturnUrl: Url.Action(nameof(Index), "Courses") ?? "/Courses",
                ReturnLabel: "Ders Listesine Dön"));
        }

        return View(viewModel);
    }

    /// <summary>
    /// GET /Courses/Create
    /// Oturum (Session) süresince geçici olarak saklanacak yeni ders ekleme formunu gösterir (Faz 1B / FR-037).
    /// </summary>
    [HttpGet]
    public IActionResult Create()
    {
        var model = new CourseCreateViewModel
        {
            WeeklyTargetMinutes = 120
        };

        return View(model);
    }

    /// <summary>
    /// POST /Courses/Create
    /// Yalnızca Name ve WeeklyTargetMinutes alanlarını tipli CourseCreateViewModel üzerinden alır;
    /// Id, IsArchived ve sahiplik bilgisi istemciden kabul edilmez.
    /// Doğrulama başarılıysa dersi aktif olarak Session'a kaydeder ve PRG ile Details/{id} sayfasına yönlendirir.
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(
        [Bind(nameof(CourseCreateViewModel.Name), nameof(CourseCreateViewModel.WeeklyTargetMinutes))]
        CourseCreateViewModel model)
    {
        string trimmedName = (model.Name ?? string.Empty).Trim();
        model.Name = trimmedName;

        // FR-001 / FR-037: Ders adı trim sonrası 1–120 karakter olmalıdır.
        if (string.IsNullOrWhiteSpace(trimmedName))
        {
            ModelState[nameof(CourseCreateViewModel.Name)]?.Errors.Clear();
            ModelState.AddModelError(
                nameof(CourseCreateViewModel.Name),
                "Ders adı zorunludur ve yalnızca boşluktan oluşamaz.");
        }
        else if (trimmedName.Length > 120)
        {
            ModelState[nameof(CourseCreateViewModel.Name)]?.Errors.Clear();
            ModelState.AddModelError(
                nameof(CourseCreateViewModel.Name),
                "Ders adı trim sonrasında 1 ile 120 karakter arasında olmalıdır.");
        }

        // FR-001 / FR-037: Haftalık hedef 0–10080 tam dakika olmalıdır.
        if (!model.WeeklyTargetMinutes.HasValue)
        {
            ModelState[nameof(CourseCreateViewModel.WeeklyTargetMinutes)]?.Errors.Clear();
            ModelState.AddModelError(
                nameof(CourseCreateViewModel.WeeklyTargetMinutes),
                "Haftalık çalışma hedefi 0 ile 10080 dakika arasında geçerli bir tam sayı olmalıdır.");
        }
        else if (model.WeeklyTargetMinutes.Value < 0 || model.WeeklyTargetMinutes.Value > 10080)
        {
            ModelState[nameof(CourseCreateViewModel.WeeklyTargetMinutes)]?.Errors.Clear();
            ModelState.AddModelError(
                nameof(CourseCreateViewModel.WeeklyTargetMinutes),
                "Haftalık çalışma hedefi 0 ile 10080 dakika arasında bir tam sayı olmalıdır.");
        }

        if (!ModelState.IsValid)
        {
            // Geçersiz girdide Session'a kayıt eklenmez; aynı View girilen değerler ve alan hatalarıyla döner.
            return View(model);
        }

        var createdCourse = _courseStore.AddCourse(trimmedName, model.WeeklyTargetMinutes!.Value);

        // TempData yalnızca PRG sonrası tek seferlik başarı bildirimi için kullanılır.
        TempData["SuccessMessage"] =
            $"\"{createdCourse.Name}\" dersi (#{createdCourse.Id}) bu tarayıcı oturumu (Session) için geçici olarak oluşturuldu.";

        return RedirectToAction(nameof(Details), new { id = createdCourse.Id });
    }
}
