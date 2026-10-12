# Git iş akışı

Durum: tasarım; bu depoda çalışan uygulama veya yürütülmüş uygulama testleri yoktur.

## Depo ve üç kişilik iş akışı
Bu paket yalnız dokümantasyon deposudur; GitHub'a yüklenmiş değildir. `StudyFlow-Documentation` başlangıçta ayrı olabilir; uygulama başlayınca belgeleri uygulama reposunun docs altına aynı isimlerle taşı ve README yollarını güncelle. Nihai repo: src/StudyFlow.Web, tests/StudyFlow.UnitTests, tests/StudyFlow.IntegrationTests, docs. Bir PR'da davranışla ilgili belge güncellenir; bağımsız eski doküman kopyası tutulmaz.

`main` gösterilebilir ve testleri geçen durum; `feature/SF-001-course-list` kısa ömürlü branch. Üç kişide GitFlow/develop/release branch zorunlu değil. Branch işi1–2 günlük mantıksal parçaya böl; commit kimliği gerçek yazarına aittir. AI üretilen kodu PR sahibi açıklayabilir ve gözden geçirir; satır sayısı katkı ölçüsü değildir.

Conventional Commits: `feat(courses): ders arşivleme ekle`, `fix(sessions): hafta sınırını böl`, `docs(api): plan sözleşmesini düzelt`, `test(security): sahiplik testini ekle`. PR: problem/sonuç, ilgili FR/issue, UI varsa görsel, test kanıtı, migration riski. A'nın PR’si B, B'nin C, C'nin A tarafından; en az bir onay. DbContext/Program.cs/_Layout.cshtml gibi ortak dosyalarda tek aktif owner ve kısa branch; diğerleri koordinasyonla değiştirir. Ortak dosya owner’ı her faz döner.

Issue şablonu: amaç, FR/BR, kabul maddeleri, faz, tahmini puan, owner, reviewer, bağımlılık. Etiket phase:1, area:courses, priority:must, type:bug. Haftalık puan/engeller toplantısı; TODO değişiklikleri sessizce kapsam yapma.

## Birleştirme ve sürüm
PR öncesi yerel ilgili test; CI restore/build/test (SDK seçildikten sonra). SQL suite için CI SQL Server sağlayabilirse çalıştır, sağlayamazsa manuel gerçek SQL kanıtını PR’ye ekle; InMemory ile ikame etme. Merge sonrası main smoke. Doküman link kontrolü her doc PR'da. Etiket `v0.1.0` ilk ders teslimi, `v0.2.0` CRUD, `v1.0.0` güvenli MVP kabulü; şimdi tag oluşturulmuş değildir. Doküman sürümü değişiklik tarih/commit ile, kaldırılan FR kimliği tekrar kullanılmaz.

[Lisans kararı](21-decisions-and-assumptions.md): ortak üç yazar ve ders politikası kararı olmadan lisans dosyası ekleme; GitHub'da görünürlük otomatik açık kaynak lisansı değildir.

[Dokümantasyon indeksi](../README.md)
