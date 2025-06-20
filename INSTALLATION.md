# Kurulum Rehberi - Animals Merge

Bu döküman, Animals Merge projesini çalıştırmak ve olası sorunları gidermek isteyen kullanıcılar ve geliştiriciler için detaylı kurulum rehberi niteliğindedir.

#
## Sistem Gereksinimleri

### Geliştirme Ortamı İçin

- **İşletim Sistemi:** Windows 10/11 veya macOS 12+
- **RAM:** En az 8 GB (Tavsiye edilen: 16 GB)
- **Depolama:** En az 50 GB boş alan
- **Unity:** 6000.0.26f1 LTS (LTS = Long Term Support)
- **Visual Studio:** 2019/2022 Unity entegrasyonlu
- **Android SDK & NDK:** Unity Hub üzerinden indirilebilir
- **Git:** (isteğe bağlı)

### Oyun Çalıştırmak İçin (Android Cihazda)

- **İşletim Sistemi:** Android 8.0 (Oreo) ve üzeri
- **Boş Alan:** Minimum 200 MB
- **Ekran:** 5 inch ve üzeri, dokunmatik destekli

#
## Kurulum Adımları

### 1. Gerekli Programların Kurulumu

- **Unity Hub**'ı indir ve kur: [https://unity.com/download](https://unity.com/download)
-  Unity Hub içinden **Unity 6000.0.26f1 LTS** sürümünü indir.
   - Android Build Support
   - Android SDK & NDK Tools
   - OpenJDK
   - TextMeshPro desteği
- **Visual Studio** (2022 önerilir) kurarken Unity desteğini seç.

### 2. Projenin İndirilmesi
```bash
git clone https://github.com/EnesKarapinar/AnimalsMerge.git
```

### 3. Projeyi Unity ile Açma
- **Unity Hub > Projects > Add > AnimalsMerge** klasörünü seç.
- İlk açılışta gerekli bağımlılıklar otomatik olarak indirilecektir.
### 4. Platform Ayarları
- **File > Build Settings** menüsünden Android platformunu seçin.
- "Switch Platform" butonuna tıklayın.
- Gerekirse **"Player Settings"** üzerinden uygulama adı, ikon ve versiyon ayarlarını yapın.
### 5. Derleme (Build)
- Android cihazı USB ile bağlayın ve geliştirici modunu açın.
- Build And Run tuşuna basarak uygulamayı cihaza kurun.
#
## Troubleshooting Rehberi
### Unity Paketleri Eksik veya Hata Veriyor
- **Window > Package Manager** menüsünden şu paketlerin yüklü olduğundan emin olun:
  - TextMeshPro
  - Input System
- Gerekirse sağ üstten **"Reset Packages to default"** yaparak yeniden deneyin.
### Android cihaz görünmüyor
- Telefonunuzda USB Hata Ayıklama (USB Debugging) aktif olmalı.
- Gerekli sürücüler (driver) yüklenmemiş olabilir. Özellikle Windows kullanıcıları üretici sürücüsünü yüklemeli.
- **adb devices** komutu ile cihazın algılanıp algılanmadığını kontrol edin.
### Ekran çözünürlüğü ya da UI bozuk gözüküyor
- Canvas ayarlarını kontrol edin:
  - **Canvas > Canvas Scaler > UI Scale Mode** = Scale with Screen Size
  - Referans çözünürlük = 1080x1920

