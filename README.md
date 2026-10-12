# StudyFlow

Üniversite öğrencileri için responsive akademik çalışma takip uygulaması tasarımı. Ders, sınav, konu, görev, günlük/haftalık plan ve gerçekleşmiş çalışma kayıtlarını birleştirir. Parçalı çalışma kayıtlarını görünür kılar; başarı/not artışı garantisi vermez.

**Durum:** Analiz ve dokümantasyon taslağı hazır; uygulama kaynak kodu, yürütülmüş test, ekran görüntüsü, dağıtılmış hizmet veya GitHub repository oluşturulmadı. Paket GitHub’a yüklenmeye hazırdır. Üç kişilik ekip için iş yükü dengeli plan içerir.

## Özellikler ve teknoloji
Ders/sınav CRUD, konu ilerlemesi, görev öncelik/son tarih, günlük/haftalık plan, timer/manuel oturum, plan-gerçek farkı, istatistik, Identity hesap ve veri izolasyonu. İlk sürüm deterministik; AI zorunlu değil.

C#, ASP.NET Core MVC/Razor, HTML/CSS/Bootstrap, ihtiyaçta JavaScript, EF Core ve SQL Server, Faz5 ASP.NET Core Identity; xUnit ve ASP.NET entegrasyon test altyapısı; Git/GitHub. Sürüm numaraları geliştirme başlangıcında resmi destek ve lab/paket uyumuyla doğrulanacak. Modüler monolit; controller sunum, servis iş mantığı, EF veri erişimi. API sözleşmesi tasarım, API geliştirme opsiyonel; MVC API’yi HTTP üzerinden çağırmaz.

## İlk teslim ve yol haritası
Faz0 analiz → Faz1 üç Controller/her birinde iki anlamlı View, responsive CSS/navigasyon/Razor → Faz2 yerel CRUD → Faz3 konu/görev/plan → Faz4 oturum/istatistik → Faz5 gerçek kullanıcı/güvenlik → Faz6 kabul/dağıtım. Fazlar dersin henüz açıklanmamış haftalık isterleri değildir. Auth öncesi yerel demo public yayınlanmaz.

İlk adım: üç kişi kapsamı ve [ekip planını](docs/22-team-plan.md) inceleyip desteklenen SDK/lab uyumunu doğrular; sonra `StudyFlow.Web` MVC projesi ve altı örnek veri ekranı oluşturulur. Bu depoda csproj bulunmadığından `dotnet run` bugün çalıştırılamaz. Gelecekteki kurulum/migration/secret komutları [ortam rehberinde](docs/19-deployment-and-configuration.md).

## Dokümantasyon indeksi

- [Proje özeti](docs/01-project-overview.md)
- [Ürün gereksinimleri](docs/02-product-requirements.md)
- [Fonksiyonel gereksinimler](docs/03-functional-requirements.md)
- [Kalite gereksinimleri](docs/04-non-functional-requirements.md)
- [Kullanıcı hikâyeleri ve kullanım senaryoları](docs/05-user-stories-and-use-cases.md)
- [Sistem mimarisi](docs/06-system-architecture.md)
- [Domain modeli](docs/07-domain-model.md)
- [Veritabanı tasarımı](docs/08-database-design.md)
- [REST API tasarım sözleşmeleri](docs/09-api-contracts.md)
- [Arayüz ve deneyim](docs/10-ui-ux-specification.md)
- [İş kuralları](docs/11-business-rules.md)
- [Doğrulama ve hata yönetimi](docs/12-validation-and-error-handling.md)
- [Güvenlik tasarımı](docs/13-security-design.md)
- [Test stratejisi](docs/14-testing-strategy.md)
- [Test senaryoları](docs/15-test-cases.md)
- [Geliştirme yol haritası](docs/16-development-roadmap.md)
- [Faz kabul kriterleri](docs/17-phase-acceptance-criteria.md)
- [Git iş akışı](docs/18-git-workflow.md)
- [Dağıtım ve yapılandırma](docs/19-deployment-and-configuration.md)
- [Risk kaydı](docs/20-risk-register.md)
- [Kararlar ve varsayımlar](docs/21-decisions-and-assumptions.md)
- [Üç kişilik eşit iş yükü planı](docs/22-team-plan.md)
- [İzlenebilirlik matrisi](docs/23-traceability-matrix.md)
- [Kalite kontrol ve teslim raporu](docs/24-delivery-report.md)

## Diyagram kaynakları

- [Sistem bağlamı](docs/diagrams/system-context.mmd)
- [Bileşen mimarisi](docs/diagrams/component-architecture.mmd)
- [ER](docs/diagrams/entity-relationship.mmd)
- [Öğrenci akışı](docs/diagrams/user-flow.mmd)
- [Dağıtım](docs/diagrams/deployment.mmd)

Aynı kaynaklar ilgili Markdown dokümanlarında fenced Mermaid olarak yer alır; GitHub'da o sayfalarda görüntülenebilir. .mmd dosyaları düzenlenebilir kaynaklardır.

## Ekran görüntüleri
Dashboard, ders listesi/detayı, sınav listesi/detayı ve mobil görünüm için görüntüler uygulama çalışınca eklenecek. Şu an görsel dosyası yok; kırık görüntü bağlantısı eklenmedi.

## Katkı ve lisans
[CONTRIBUTING](CONTRIBUTING.md) ve [Git akışı](docs/18-git-workflow.md). Lisans, üç yazarın ve ders politikasının ortak kararını bekliyor; lisans dosyası atanmadı. Dokümanın internette görüntülenmesi yeniden kullanım izni anlamına gelmez.
