# Kararlar ve varsayımlar

Durum: tasarım; bu depoda çalışan uygulama veya yürütülmüş uygulama testleri yoktur.

## Bilinenler
ASP.NET Core MVC; ilk teslim en az3 Controller, her biri2 anlamlı View, toplam6; responsive CSS, Razor dinamik içerik, navigasyon; ekip3 kişi. Sonraki ders isterleri açıklanmadı. Uygulama henüz bu depoda yok.

## Varsayımlar ve açık sorular
- A-001: Hafta Pazartesi başlar, default Europe/Istanbul. Öğretim görevlisi/ürün onayıyla değişebilir.
- A-002: Yaklaşan sınav14 gün, operasyonel kartlarda arşiv dışı; geçmiş süre istatistiklere dahil.
- A-003: Bir öğrenci aynı anda tek çalışma yapar; oturum maksimum24saat. Zamanlayıcı durdurulmazsa manuel düzeltme/iptal gerekir; background auto-stop yok.
- A-004: Ders adı aynı kullanıcıda tekrarlanabilir (farklı dönem), dönem entity’si MVP dışında.
- A-005: Geçmiş sınav ve plan tarihsel kayıt için kabul edilir; gerçek oturum gelecekte olamaz.
- A-006: Ders bazlı hedeflerin toplamı kişisel haftalık hedeftir; ikinci User hedef alanı yok.
- A-007: Ekip Üye A/B/C yer tutucudur; gerçek isim/kapasite bilinmiyor. Eşit puan başlangıç, eşit saat garantisi değil.
- Q-001: Desteklenen .NET/EF/test paket sürümü ve lab uyumu — Faz1 başlangıcında doğrula; şu an sürüm seçilmedi.
- Q-002: Hosting/bütçe/SQL erişimi — Faz6 öncesi karşılaştırma ve karar.
- Q-003: Lisans/depo public-private/ders kod paylaşım politikası — üç yazar+öğretim görevlisi; lisans atanmadı.
- Q-004: Identity e-posta doğrulama/kurtarma, sağlayıcı, rate-limit ayarı — Faz5/6; servis varmış gibi gösterme.
- Q-005: Veri saklama, hesap silme/export ve receipt temizleme — MVP sonrası; hukuk uygunluğu iddiası yok.

## Bilinmeyen ders isterleri
Hafta2+ DB, API, auth, dağıtım, ekip değerlendirme ve sunum şartları bilinmiyor. Bu belgelerdeki Faz2–6 ürün geliştirme önerisidir, hocanın açıklanmış takvimi değildir. Yeni resmi ister source/date ile issue’ya eklenir; faz bağımlılığı ve ekip puanları revize edilir.

## ADR-001 — Modüler MVC monolit
Durum: önerilen başlangıç kararı. Bağlam: küçük öğrenci projesi, ekip3. Karar: tek proje içinde katman klasörleri, tek deployment; servis iş mantığı. Alternatif: mikroservis/CQRS/ayrı katman assembly’leri. Gerekçe: dağıtık operasyon/soyutlama yükü gereksiz. Etki: DbContext bağımlılığı Application’da pragmatik; ölçek/ekip artarsa yeniden değerlendir.

## ADR-002 — Ders silmek yerine arşiv
Durum: normatif tasarım. Karar: ilişkili ders delete409; arşiv salt okunur alt kayıtları korur; açık session arşivi engeller. Alternatif cascade delete. Gerekçe: çalışma geçmişi veri kaybını engellemek. Etki: kullanıcı silmek için bağlı kayıtları bilinçli temizler; UI arşiv önerir.

## ADR-003 — Gerçek süre tek kaynak
Durum: normatif. Karar: Session StartAt/EndAt farkı; süre/özet tabloları yok. Alternatif task üzerinde actualMinutes. Gerekçe: düzeltme sonrası tutarsız tekrarları engellemek. Etki: hesap sorgusu maliyeti; indeks/ölçüm yeterli değilse sonra aggregate cache ADR’si.

## ADR-004 — Owner altyapısı erken, kimlik deneyimi sonra
Durum: normatif. Faz2 ApplicationUser demo owner; her servis owner filtreli; Faz5 gerçek Identity UI/cookie/claims. Alternatif nullable owner sonradan doldurmak. Gerekçe: sahipsiz kayıt migration riskini azaltmak. Etki: Faz2–4 yerel; Production guard; auth öncesi internete yayın yok.

## ADR-005 — MVC öncelikli, API opsiyonel
Durum: normatif. Karar: API sözleşmesi tasarım; MVC Application servislerini doğrudan çağırır. Alternatif MVC'nin HTTP API’ye bağımlılığı. Gerekçe: ders MVC ve ilk teslim; tekrar iş mantığı yok. Etki: API kabulü ancak geliştirildiyse; HTTP testleri N/A gerekçeli.

## ADR-006 — Basit güvenilir yazma koordinasyonu
Durum: SQL tasarım önerisi, gerçek SQL yarış testleriyle onaylanacak. Karar: kullanıcı satır kilidi ile kritik yazmalar serialize, rowversion ve filtered unique ikinci savunma. Alternatif uygulama RAM lock (çok instance güvenilir değil) veya yalnız kontrol-sonra-insert (yarış riski). Etki: kullanıcı başına yazma paralelliği azalır; proje ölçeğinde kabul; performans hedefiyle tekrar ölç.

Her karar değişiminde eski ADR durumunu superseded yap, FR/BR/TC/model/faz zincirini aynı PR’da güncelle. Şu anda bu kararlar uygulamayla doğrulanmış değildir.

[Dokümantasyon indeksi](../README.md)
