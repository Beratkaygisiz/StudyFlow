# Kalite gereksinimleri

Durum: tasarım; bu depoda çalışan uygulama veya yürütülmüş uygulama testleri yoktur.

| Kimlik | Alan | Ölçülebilir hedef |
| --- | --- | --- |
| NFR-001 | Güvenlik | Faz 5 tüm sahiplik senaryolarında başka kullanıcıya kayıt/ilişki sızıntısı 0; Faz 2–4 Production demo guard testi. |
| NFR-002 | Performans | Hedef: seçilen test makinesinde 10 eşzamanlı kullanıcı, sahip başına 100 ders/1000 görev/10000 oturum, sıcak çalışmada dashboard p95 < 2s; ortam ve sorgu sayısını kaydet. Henüz ölçülmedi. |
| NFR-003 | Kullanılabilirlik | 3 öğrenci ana akışı <=5 dakika tamamlar; test moderatör yardımı gerektirmeyen adımları kaydeder. |
| NFR-004 | Responsive | 360/768/1440px ana içerik taşması 0; FR-033. |
| NFR-005 | Bakım | Controller iş hesabı içermez; bir iş kuralı değişimi servis ve ilgili testiyle tek PR. |
| NFR-006 | Test edilebilirlik | Saat IClock üzerinden; testler dış servise bağımlı değil; kritik BR-020–025 sabit zamanla test edilir. |
| NFR-007 | Veri bütünlüğü | FK, check, rowversion ve benzersiz açık oturum kuralları SQL Server entegrasyon testlerinde geçer. |
| NFR-008 | Erişilebilirlik | Klavye ile tüm formlar; etiket, hata ilişkisi, görünür focus; normal metinde kontrast en az 4.5:1 hedef; WCAG AA tam uygunluk henüz iddia edilmez. |
| NFR-009 | Loglama | Beklenmedik her hata traceId ile; parola/token/kişisel not veya request body loglanmaz. |
| NFR-010 | Hata yönetimi | API tasarımı ProblemDetails; MVC formda alan hatası; üretimde stack trace gösterilmez. |
| NFR-011 | Yapılandırma | Git geçmişinde sır yok; Production demo guard; yapılandırma eksikse güvenli başlatma hatası. |

Doğrulama: TC-037–066 ve Faz 6 raporu. Sayılar kabul hedefidir; mevcut sonuç değildir.

[Dokümantasyon indeksi](../README.md)
