# Faz kabul kriterleri

Durum: tasarım; bu depoda çalışan uygulama veya yürütülmüş uygulama testleri yoktur.

Kontrol listeleri planlanan kapılardır; hiçbir madde kod yokken tamamlandı sayılmaz.

## Faz 0

- [ ] README ve tüm bağlantılar geçerli; FR/BR/TC kimlikleri tutarlı
- [ ] Üç kişi kapsam ve task/plan/session ayrımını örnekle açıklayabiliyor
- [ ] Açık sürüm/host/ders kararları issue olarak kaydedildi

## Faz 1

- [ ] DashboardController Index/Weekly; CoursesController Index/Details; ExamsController Index/Details — 3 Controller, her biri2 View (FR-032)
- [ ] 6 anlamlı View ayrı amaç ve veri gösteriyor; partial/layout sayılmıyor (FR-032)
- [ ] Ortak navigasyon ve tüm Details→Course bağlantıları çalışıyor (FR-032)
- [ ] 360/768/1440 responsive ve CSS site.css mevcut (FR-033)
- [ ] Razor foreach/koşul gerçek demo ViewModel verisi gösteriyor; örnek veri etiketi var (FR-032)
- [ ] Uygulama seçilen SDK’da starts; yanlış Id404 ve boş listeler tanımlı (FR-032)
- [ ] TC-032,033,060 geçiyor; DB/auth/API zorunlu değil

## Faz 2

- [ ] FR-001 — Ders oluşturma; TC-001 ve ilgili olumsuz senaryolar geçti
- [ ] FR-002 — Ders listeleme ve filtre; TC-002 ve ilgili olumsuz senaryolar geçti
- [ ] FR-003 — Ders detay ve düzenleme; TC-003 ve ilgili olumsuz senaryolar geçti
- [ ] FR-004 — Ders arşivleme ve geri açma; TC-004 ve ilgili olumsuz senaryolar geçti
- [ ] FR-005 — Ders silme; TC-005 ve ilgili olumsuz senaryolar geçti
- [ ] FR-006 — Sınav oluşturma; TC-006 ve ilgili olumsuz senaryolar geçti
- [ ] FR-007 — Sınav liste ve detay; TC-007 ve ilgili olumsuz senaryolar geçti
- [ ] FR-008 — Sınav düzenleme ve silme; TC-008 ve ilgili olumsuz senaryolar geçti
- [ ] FR-028 — Çalışma hedefleri; TC-028 ve ilgili olumsuz senaryolar geçti
- [ ] FR-034 — Tekrarlanan gönderim kontrolü; TC-034 ve ilgili olumsuz senaryolar geçti
- [ ] FR-035 — Eşzamanlı düzenleme; TC-035 ve ilgili olumsuz senaryolar geçti
- [ ] FR-036 — Kimlik öncesi yerel geliştirme; TC-036 ve ilgili olumsuz senaryolar geçti
- [ ] Önceki faz regresyonu; migration gerçek SQL Server üzerinde; belge güncel; çapraz PR inceleme

## Faz 3

- [ ] FR-009 — Konu ekleme ve liste; TC-009 ve ilgili olumsuz senaryolar geçti
- [ ] FR-010 — Konu durumu ve notu; TC-010 ve ilgili olumsuz senaryolar geçti
- [ ] FR-011 — Konu düzenleme, sıra ve silme; TC-011 ve ilgili olumsuz senaryolar geçti
- [ ] FR-012 — Konu ilerleme; TC-012 ve ilgili olumsuz senaryolar geçti
- [ ] FR-013 — Görev oluşturma; TC-013 ve ilgili olumsuz senaryolar geçti
- [ ] FR-014 — Görev düzenleme ve silme; TC-014 ve ilgili olumsuz senaryolar geçti
- [ ] FR-015 — Görev durum geçişi; TC-015 ve ilgili olumsuz senaryolar geçti
- [ ] FR-016 — Görev filtreleri ve gecikme; TC-016 ve ilgili olumsuz senaryolar geçti
- [ ] FR-017 — Plan oluşturma; TC-017 ve ilgili olumsuz senaryolar geçti
- [ ] FR-018 — Plan düzenleme ve silme; TC-018 ve ilgili olumsuz senaryolar geçti
- [ ] FR-019 — Günlük ve haftalık plan; TC-019 ve ilgili olumsuz senaryolar geçti
- [ ] Önceki faz regresyonu; migration gerçek SQL Server üzerinde; belge güncel; çapraz PR inceleme

## Faz 4

- [ ] FR-020 — Plan ve gerçekleşen karşılaştırma; TC-020 ve ilgili olumsuz senaryolar geçti
- [ ] FR-021 — Oturum başlatma; TC-021 ve ilgili olumsuz senaryolar geçti
- [ ] FR-022 — Oturum bitirme; TC-022 ve ilgili olumsuz senaryolar geçti
- [ ] FR-023 — Manuel oturum kaydı; TC-023 ve ilgili olumsuz senaryolar geçti
- [ ] FR-024 — Oturum düzeltme ve silme; TC-024 ve ilgili olumsuz senaryolar geçti
- [ ] FR-025 — Günlük haftalık süre; TC-025 ve ilgili olumsuz senaryolar geçti
- [ ] FR-026 — Dashboard özetleri; TC-026 ve ilgili olumsuz senaryolar geçti
- [ ] FR-027 — İstatistikler ve değişim; TC-027 ve ilgili olumsuz senaryolar geçti
- [ ] Önceki faz regresyonu; migration gerçek SQL Server üzerinde; belge güncel; çapraz PR inceleme

## Faz 5

- [ ] FR-029 — Kayıt ve giriş; TC-029 ve ilgili olumsuz senaryolar geçti
- [ ] FR-030 — Profil ve zaman dilimi; TC-030 ve ilgili olumsuz senaryolar geçti
- [ ] FR-031 — Veri izolasyonu; TC-031 ve ilgili olumsuz senaryolar geçti
- [ ] Önceki faz regresyonu; migration gerçek SQL Server üzerinde; belge güncel; çapraz PR inceleme

## Faz 6

- [ ] Tüm Must FR ve güvenlik olumsuz TC geçiyor; kod/şema/belge farkları giderildi
- [ ] NFR001–011 ölçüldü veya açık kısıt raporlandı; güvenlik Must için waiver yok
- [ ] Migration önceki şemadan denenmiş; yedek restore testi kanıtlı
- [ ] HTTPS Production demo guard/secret/DB izinleri doğru
- [ ] API yoksa API testleri N/A gerekçesi; varsa sözleşme testleri geçer
- [ ] Release tag ve teslim sunumu hazır; bilinmeyen ders isterleri varmış gibi işaretlenmedi

[Dokümantasyon indeksi](../README.md)
