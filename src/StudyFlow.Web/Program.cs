using System.IO;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.WebEncoders;
using StudyFlow.Web.DemoData;

var builder = WebApplication.CreateBuilder(args);

var dpKeysPath = Path.Combine(builder.Environment.ContentRootPath, "obj", "DataProtection-Keys");
Directory.CreateDirectory(dpKeysPath);
builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo(dpKeysPath));

builder.Services.AddControllersWithViews();
builder.Services.Configure<WebEncoderOptions>(options =>
{
    options.TextEncoderSettings = new TextEncoderSettings(
        UnicodeRanges.BasicLatin,
        UnicodeRanges.Latin1Supplement,
        UnicodeRanges.LatinExtendedA);
});

// Faz 1B: Oturum (Session) tabanlı geçici ders saklama yapılandırması
builder.Services.AddHttpContextAccessor();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".StudyFlow.Session";
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});

builder.Services.AddSingleton<IClock, SystemClock>();
builder.Services.AddScoped<IDemoCourseStore, DemoCourseStore>();
builder.Services.AddScoped<IDemoDataProvider, DemoDataProvider>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Session middleware UseRouting'den sonra, MapControllerRoute'tan önce çalışır.
app.UseSession();

app.UseAuthorization();

// "/" adresi doğrudan Dashboard/Index'e açılır (docs/03-ekranlar.md).
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=Index}/{id?}");

app.Run();
