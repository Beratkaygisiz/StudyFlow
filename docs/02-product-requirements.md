# Ürün gereksinimleri

Durum: tasarım; bu depoda çalışan uygulama veya yürütülmüş uygulama testleri yoktur.

## Vizyon ve öncelik
Öğrenci yaklaşan sınavını görür, konudan görev üretir, görevi bir güne planlar, çalışmasını kaydeder ve farkı inceler. İlk ders teslimi ürün MVP'si değildir: yalnız altı anlamlı örnek veri ekranıdır.

| MoSCoW | Kapsam |
| --- | --- |
| Must | FR-001–009,011–026,028–029,031–036: temel takip, güvenlik ve ilk teslim |
| Should | FR-010 kişisel konu notu/tekrar tarihi; FR-027 dönemler arası karşılaştırma; FR-030 profil zamanı |
| Could | Daha sonraki sürüm: CSV dışa aktarma ve basit kural tabanlı çalışma önerisi; bu depoda taahhüt/test yok |
| Won’t | İlk ürün sürümünde AI, mobil istemci, sosyal sistem, okul entegrasyonu |

## Modüller ve MVP
Dashboard özet sunar; ders akademik bağlamdır; sınav takvim olayıdır; konu ilerleme birimidir; görev yapılacak iştir; plan yerleştirmedir; oturum gerçekleşmiş zamandır; istatistik bunlardan hesaplanır. Ürün MVP’si Faz 5 sonrası Must davranışların kalıcı SQL Server üzerinde güvenli çalışmasıdır; Faz 6 yayın kapısıdır. REST API opsiyoneldir ve MVP gereği değildir.

Başarı hedefleri: tüm Must kabul senaryoları geçer; çapraz kullanıcı erişimi 0; 360px'de ana içerik taşması 0; üç kullanıcı kullanılabilirlik deneyi. Şu anda ölçüm yok.

## Değişiklik yönetimi
Yeni ders isteri issue olarak kaydedilir; FR/BR/model/UI/test/faz etkisi yazılır; üç kişi haftalık 20 dakikalık toplantıda Must kapsamından hangi işin erteleneceğini kararlaştırır. Dersin zorunlu isteri önce gelir; yeni iş otomatik olarak mevcut tahmine eklenmez. Kimlikleri yeniden numaralandırma; kaldırılan gereksinimi retired olarak koru.

[Dokümantasyon indeksi](../README.md)
