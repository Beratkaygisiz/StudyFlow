# Risk kaydı

Durum: tasarım; bu depoda çalışan uygulama veya yürütülmüş uygulama testleri yoktur.

| Kimlik | Risk | Olasılık | Etki | Öncelik | Önlem | Gerçekleşirse |
| --- | --- | --- | --- | --- | --- | --- |
| RISK-001 | Kapsam büyümesi | Yüksek | Yüksek | Yüksek | Must/MoSCoW ve faz issue | Could/Should ertele; ders zorunlularını koru |
| RISK-002 | Zaman yetersizliği | Yüksek | Yüksek | Yüksek | Haftalık puan ve kapasite ölçümü | Kapsam indir; ders teslimini güvenli küçük demo yap |
| RISK-003 | C#/EF teknik bilgi açığı | Yüksek | Orta | Orta | Küçük spike; eşli gözden geçirme | Belirsiz işi böl; öğrenme süresini plana ekle |
| RISK-004 | Veritabanı/migration veri kaybı | Orta | Yüksek | Yüksek | NO ACTION, review, staging backup | Yazmayı durdur; backup restore ve schema analizi |
| RISK-005 | Kullanıcı verilerinin karışması | Orta | Çok yüksek | Kritik | Owner servis+composite FK+A/B tests | Yayını durdur; erişimi kapat; kapsam/log inceleme |
| RISK-006 | Test eksikliği | Orta | Yüksek | Yüksek | Her owner kendi testini; çapraz review | Geçiş kapısını kapat; kritik olumsuz testleri tamamla |
| RISK-007 | Doküman-kod ayrışması | Yüksek | Orta | Orta | PR FR/BR ve doc değişikliği | Normatif kuralı ekipçe seç; test ve belgeyi düzelt |
| RISK-008 | Oturum çift sayımı/zaman dilimi | Orta | Yüksek | Yüksek | UTC+yerel kırpma; DST ve hafta testleri | Hesap düzelt; türetilen özetleri yeniden hesapla |
| RISK-009 | Üç kişi ortak dosya çatışması | Yüksek | Orta | Orta | Dönen owner; kısa branch; schema koordinasyonu | Bir kişi merge; üç modül regresyonu |
| RISK-010 | Yeni ders isterleri | Yüksek | Yüksek | Yüksek | Varsayım ayrı; haftalık revizyon | İşi öne çek; API/Could ertele; kabul güncelle |
| RISK-011 | Sırların repoya girmesi | Düşük | Çok yüksek | Kritik | User-secrets/env; PR secret taraması | Sırrı iptal/yenile; geçmiş temizliği planla |
| RISK-012 | Identity öncesi public yayın | Düşük | Çok yüksek | Kritik | Production demo guard; yerel geliştirme | Host kapat; auth/owner testleri geçmeden açma |
| RISK-013 | AI kodunun anlaşılmaması | Yüksek | Yüksek | Yüksek | PR sahibi veri akışı/kural/testi açıklayabilmeli | Kod sadeleştir; açıklanamayan soyutlamayı çıkar |
| RISK-014 | SDK/paket uyumsuzluğu | Orta | Orta | Orta | Resmi destek+lab doğrulaması; global.json | Uyumlu desteklenen sürüme geç; build tekrarla |

Risk review haftalık; ilgili modül owner, güvenlik için Faz5 ortak sorumlu. Olasılık/etki nitel değerlendirme, hesaplanmış istatistik değildir.

[Dokümantasyon indeksi](../README.md)
