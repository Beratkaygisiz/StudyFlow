var builder = WebApplication.CreateBuilder(args);

// ==========================================================================
// 1. SERVİSLERİN KAYDEDİLMESİ (DEPENDENCY INJECTION / HİZMET KAYITLARI)
// ==========================================================================
// ASP.NET Core MVC yapısını ve Razor View motorunu projeye dahil ediyoruz.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// ==========================================================================
// 2. HTTP İSTEK BORU HATTI (HTTP REQUEST PIPELINE / MIDDLEWARE)
// ==========================================================================
if (!app.Environment.IsDevelopment())
{
    // Sunucu tarafında hata oluştuğunda yönlendirilecek hata sayfası
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

// wwwroot klasöründeki statik dosyaların (CSS, JS, resimler) sunulabilmesini sağlar
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

// ==========================================================================
// 3. VARSAYILAN ROTA TANIMI (DEFAULT ROUTING)
// ==========================================================================
// Tarayıcıdan adres çubuğuna sadece alan adı girildiğinde (örn: localhost:5000),
// varsayılan olarak HomeController içindeki Index metodunun çalışmasını sağlar.
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

// Uygulamayı başlat ve gelen web isteklerini dinle
app.Run();
