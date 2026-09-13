# Sen Ciddimisin Reis? V1

Android odaklı, düzenlenebilir Unity 6 mod projesi.

## Açma ve derleme

1. Proje klasörünü Unity Hub üzerinden bir Unity 6 sürümüyle açın.
2. Unity ilk açılışta varlıkları yeniden içe aktarırken işlemin tamamlanmasını bekleyin.
3. Unity Hub kurulumunda Android Build Support, Android SDK/NDK Tools ve OpenJDK modüllerinin bulunduğunu doğrulayın.
4. Build Profiles bölümünde Android hedefini seçin.
5. Başlangıç sahnesi `Assets/Scene/Warning.unity`, ana menü `Assets/Scene/MainMenu.unity`, oyun sahnesi `Assets/Scene/School.unity` dosyasıdır.

## V1 özellikleri

- Oyun adı: **Sen Ciddimisin Reis?**
- Yeni karakter; oyun içi, ana menü ve matematik/YCTP ekranlarında kullanılır.
- Normal, konuşma, el sallama, kızgın ve cetvelli pozlar bulunur.
- Her defter alımında `SEN CİDDİ MİSİN?` yazısı iki saniye görünür.
- Verilen Control Freak 2 mobil kontrol varlıklarıyla hareket, bakış, koşma, etkileşim, eşya ve duraklatma kontrolleri sağlanır.
- Masaüstü klavye/fare girdileri korunur.
- Android hedefi yatay ekran, IL2CPP ve ARM64 olarak ayarlanmıştır.

## Doğrulama

Proje kökünde:

```bash
bash tools/validate_project.sh
bash tools/validate_character_assets.sh
bash tools/validate_mobile.sh
```

Bu ortamda Unity Editor kurulu olmadığı için gerçek Android derlemesi yapılmamıştır. Proje, Unity 6 ile ilk açılışta otomatik yeniden içe aktarmadan geçirilmeli ve Android cihazda son oynanış testi yapılmalıdır.
