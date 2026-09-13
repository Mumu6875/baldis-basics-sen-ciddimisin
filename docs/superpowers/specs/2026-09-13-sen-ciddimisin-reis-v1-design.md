# Sen Ciddimisin Reis? V1 — Tasarım Şartnamesi

## Amaç

Mevcut Baldi's Basics Number Slop şablonundan, oynanış mekaniğini değiştirmeden Android odaklı ve Unity 6 ile açılabilir **Sen Ciddimisin Reis?** adlı V1 modu üretilecek. Teslimat düzenlenebilir Unity projesini içeren `BaldisCiddimisinV1.zip` olacaktır.

## Kapsam

- Şablondaki okul, defter toplama, matematik soruları, düşman yapay zekâsı, kazanma ve kaybetme akışları korunacak.
- Oyun adı, kullanıcıya görünen başlık alanlarında `Sen Ciddimisin Reis?` olarak değiştirilecek.
- Verilen karakter; oyun içi Baldi, ana menü Baldi'si ve matematik/YCTP ekranındaki Baldi yerine kullanılacak.
- Her defter alındığında ekranın ortasında iki saniye boyunca pikselli biçimde `SEN CİDDİ MİSİN?` yazısı gösterilecek. Oyun bu sırada durmayacak ve bildirim her defterde yeniden tetiklenecek.
- Verilen mobil kontrol paketi hareket, kamera, koşma ve etkileşim girdilerine bağlanacak.
- Android arayüzü yatay ekran için düzenlenecek.
- Yeni oyun mekaniği, yeni harita, yeni bölüm veya yeni yan karakter eklenmeyecek.

## Karakter Görsel Sistemi

Kaynak karakter görselindeki siyah zemin kaldırılacak ve gerçek alfa kanallı PNG üretilecek. Şapka, vücut ve ayaklar kırpılmayacak. Karakterin temel görünüşü değiştirilmeden aşağıdaki kullanım durumlarına uygun kareler hazırlanacak:

- oyun içi normal/boşta duruş,
- el sallama,
- konuşma,
- kızgın duruş ve cetvel vurma,
- ana menü sunumu,
- matematik/YCTP konuşma ve tepki durumları.

Mevcut animasyon kliplerinin zamanlaması ve durum geçişleri korunacak. Yeni kareler, eski Baldi görsellerinin yerine proje varlığı seviyesinde bağlanacak; çalışma zamanında eski-yeni görsel değiştiren bir sistem kurulmayacak. Artık kullanılmayan eski Baldi görselleri yalnızca hiçbir sahne, animasyon veya prefab tarafından referanslanmadıkları doğrulandıktan sonra kaldırılacak.

## Defter Bildirimi

Defter kazanım olayına tek sorumluluğu bildirim göstermek olan bir bileşen bağlanacak. Bildirim ekran merkezinde, pikselli ve yüksek kontrastlı olacak; farklı Android en-boy oranlarında güvenli alan içinde kalacak. Aynı defter olayının iki kez bildirim üretmemesi sağlanacak. Zaman ölçeği değişse bile iki saniyelik gerçek süre sonunda kaybolması için ölçeklenmemiş zaman kullanılacak.

## Mobil Kontroller

Mobil paket önce zararlı veya gereksiz yürütülebilir içerik açısından incelenecek, ardından yalnızca Unity varlıkları projeye alınacak. Sol tarafta hareket kontrolü, sağ tarafta bakış alanı; sağ tarafta etkileşim ve koşma düğmeleri bulunacak. Masaüstü klavye/fare girdileri korunacak. Mobil arayüz sadece dokunmatik platformlarda etkinleşecek ve mevcut oyun mantığına yeni giriş kaynakları olarak bağlanacak.

## Unity 6 ve Android Uyumluluğu

Kaynak şablon Unity `2018.3.9f1` olduğundan proje kopyası Unity 6 biçimine yükseltilecek. Eski paket manifesti, TextMesh Pro/UI kullanımı, giriş ayarları, sahne serileştirmesi ve Android Player Settings kontrollü biçimde güncellenecek. Hedef yatay ekran ve ARM64 destekli Android olacaktır. Paket kimliği özgün ve geçerli bir ters alan adı biçiminde ayarlanacaktır.

## Hata Yönetimi

- Eksik mobil paket referansları derlemeyi bozmayacak biçimde tespit edilecek.
- Eksik karakter karesi varsa sessizce eski Baldi'ye dönmek yerine doğrulama testi başarısız olacak.
- Sahne veya animasyon referansları değiştirilmeden önce GUID eşleşmeleri denetlenecek.
- Unity 6 dönüşümünden kaynaklanan derleme hataları giderilmeden teslim paketi oluşturulmayacak.

## Doğrulama

- Tüm C# dosyalarının Unity 6 API'leriyle derlenebilirliği kontrol edilecek.
- Build Settings içindeki oyun sahneleri ve sıraları doğrulanacak.
- Ana menü, School ve YCTP sahnelerinde eski Baldi görseli referansları taranacak.
- Defter bildiriminin her defter için bir kez, iki saniye göründüğü test edilecek.
- Mobil hareket, bakış, koşma ve etkileşim bağlantıları sahne/prefab düzeyinde doğrulanacak.
- Android yatay yön, uygulama adı ve paket kimliği kontrol edilecek.
- Nihai ZIP açılıp zorunlu `Assets`, `Packages` ve `ProjectSettings` klasörlerinin bulunduğu doğrulanacak.

## Teslimat

`BaldisCiddimisinV1.zip`, Unity 6 ile açılabilir düzenlenebilir proje kaynaklarını içerecek. `Library`, `Temp`, `Logs`, `obj` ve kullanıcıya özel IDE önbellekleri pakete eklenmeyecek.
