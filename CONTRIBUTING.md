# Katkı rehberi

## Bir işin akışı

1. Issue aç: amaç, ilgili FR, kabul koşulu, faz, sorumlu ve bağımlılıkları yaz.
2. Kısa ömürlü branch kullan: `feature/SF-001-course-list`.
3. Özelliği controller, ViewModel/View, servis, doğrulama ve gerekli testleriyle tamamla. Değişen kuralı ilgili belgede güncelle.
4. Pull request'te problem ve sonucu, doğrulamayı, UI varsa görseli, migration varsa riskini belirt.
5. Başka bir ekip üyesi inceleyip onayladıktan sonra birleştir. Main üzerinde kısa çalıştırma kontrolü yap.

Bir iş tercihen 1–2 günlük parçaya ayrılır. `main` gösterilebilir ve mevcut kontrolleri geçen durumdadır. Üç kişilik ekipte ayrı develop/release branch düzeni zorunlu değildir.

## Commit örnekleri

- `feat(courses): ders arşivleme ekle`
- `fix(sessions): hafta sınırındaki süreyi böl`
- `docs(scope): konu durumunun önceliğini netleştir`
- `test(security): başka kullanıcıya erişimi kontrol et`

## Ortak dosyalar ve inceleme

`Program.cs`, DbContext, migration ve `_Layout.cshtml` için her fazda bir koordinatör belirlenir. Aynı dosyadaki değişiklikler kısa branch ve PR sırasıyla yürütülür. İnceleme döngüsü: A'nın PR'sini B, B'ninkini C, C'ninkini A inceler.

AI tarafından üretilen kodun veri akışını, iş kuralını ve hata davranışını PR sahibi açıklayabilmelidir. Katkı satır veya dosya sayısıyla ölçülmez.

## Birleştirme öncesi

- İlgili FR kabul koşulu ve olumsuz senaryolar doğrulandı.
- Uygulama varsa build ve ilgili testler geçti; ilk UI aşamasında ekranlar tarayıcıda kontrol edildi.
- FK, transaction, rowversion ve yarış davranışı gerçek SQL Server üzerinde sınandı.
- Sır, parola, bağlantı bilgisi, kullanıcı notu ve gerçek kullanıcı verisi eklenmedi.
- Belge bağlantıları geçerli; uygulanan davranış ile belgeler aynı.

Uygulama ve test projeleri oluşturulduğunda CI'a restore/build/test adımları eklenir. Şu an bu pakette çalıştırılabilir proje veya CI sonucu bulunmaz.

Kapsam değişirse [kapsam ve kurallar](docs/01-kapsam-ve-kurallar.md), ilgili teknik tasarım ve test kabulü aynı PR'da güncellenir. Kaldırılan FR kimliği başka bir davranış için yeniden kullanılmaz.
