# C# Temel Konular -  Örnekleri

Bu proje, stajyer arkadaşlar için C# temel konularında kapsamlı örnekler içermektedir. Her konu için ayrı bir solution ve detaylı açıklamalar bulunmaktadır.

## 📁 Proje Yapısı

```
örnekler/
├── 01-Transaction/
│   ├── TransactionExample.sln
│   └── TransactionExample/
│       ├── TransactionExample.csproj
│       └── Program.cs
├── 02-TryCatch/
│   ├── TryCatchExample.sln
│   └── TryCatchExample/
│       ├── TryCatchExample.csproj
│       └── Program.cs
├── 03-ExceptionManagement/
│   ├── ExceptionManagementExample.sln
│   └── ExceptionManagementExample/
│       ├── ExceptionManagementExample.csproj
│       └── Program.cs
├── 04-GerçekHayatÖrnekleri/
│   ├── GercekHayatOrnekleri.sln
│   └── GercekHayatOrnekleri/
│       ├── GercekHayatOrnekleri.csproj
│       └── Program.cs
└── README.md
```

## 🎯 Konu Başlıkları

### 1. Transaction (İşlem Yönetimi)
**Dosya:** `01-Transaction/TransactionExample.sln`

**İçerik:**
- ✅ Basit Transaction Örneği (Banka transfer simülasyonu)
- ✅ İç İçe Transaction Örneği (İç içe işlemler)
- ✅ Transaction Rollback Örneği (Hata durumunda geri alma)
- ✅ SavePoint Örneği (Kontrol noktası)

**Öğrenilecekler:**
- Transaction başlatma ve commit etme
- Rollback işlemleri
- İç içe transaction'lar
- SavePoint kullanımı
- ACID özellikleri

### 2. Try-Catch (Hata Yakalama)
**Dosya:** `02-TryCatch/TryCatchExample.sln`

**İçerik:**
- ✅ Basit Try-Catch Örneği (Temel hata yakalama)
- ✅ Multiple Catch Blocks Örneği (Çoklu hata türleri)
- ✅ Try-Catch-Finally Örneği (Kaynak temizleme)
- ✅ İç İçe Try-Catch Örneği (İç içe hata yakalama)
- ✅ Custom Exception Örneği (Özel hata sınıfları)

**Öğrenilecekler:**
- Try-catch bloklarının kullanımı
- Farklı exception türleri
- Finally bloğunun önemi
- Özel exception sınıfları oluşturma
- Hata hiyerarşisi

### 3. Exception Management (Hata Yönetimi)
**Dosya:** `03-ExceptionManagement/ExceptionManagementExample.sln`

**İçerik:**
- ✅ Exception Logging Örneği (Hata kaydetme)
- ✅ Exception Handling Strategy Örneği (Hata stratejileri)
- ✅ Global Exception Handler Örneği (Genel hata yakalama)
- ✅ Retry Pattern Örneği (Tekrar deneme deseni)
- ✅ Exception Aggregation Örneği (Hata toplama)

**Öğrenilecekler:**
- Hata loglama teknikleri
- Farklı hata yönetim stratejileri
- Global exception handling
- Retry pattern implementasyonu
- Aggregate exception kullanımı

### 4. Gerçek Hayat Örnekleri (Pratik Senaryolar)
**Dosya:** `04-GerçekHayatÖrnekleri/GercekHayatOrnekleri.sln`

**İçerik:**
- ✅ ATM Para Çekme Örneği (Transaction kullanımı)
- ✅ E-Ticaret Sepeti Örneği (Try-Catch kullanımı)
- ✅ Hastane Randevu Sistemi Örneği (Exception Management)
- ✅ Kütüphane Kitap Ödünç Alma Örneği (Transaction ve Try-Catch)
- ✅ Online Sınav Sistemi Örneği (Kapsamlı hata yönetimi)

**Öğrenilecekler:**
- Gerçek hayat senaryolarında C# kullanımı
- İş kurallarına uygun hata yönetimi
- Custom exception sınıfları oluşturma
- Pratik problem çözme yaklaşımları
- Sistem tasarımı ve implementasyonu

## 🚀 Nasıl Çalıştırılır?

### Gereksinimler
- .NET 8.0 SDK
- Visual Studio 2022 veya Visual Studio Code

### Adımlar
1. İstediğiniz konunun solution dosyasını açın
2. Projeyi build edin
3. Programı çalıştırın
4. Console çıktılarını takip edin

### Örnek Komutlar
```bash
# Transaction örneği
cd 01-Transaction/TransactionExample
dotnet run

# Try-Catch örneği
cd 02-TryCatch/TryCatchExample
dotnet run

# Exception Management örneği
cd 03-ExceptionManagement/ExceptionManagementExample
dotnet run

# Gerçek Hayat Örnekleri
cd 04-GerçekHayatÖrnekleri/GercekHayatOrnekleri
dotnet run

## 📚 Öğrenme Hedefleri

### Transaction
- [ ] Transaction'ın ne olduğunu anlama
- [ ] ACID özelliklerini kavrama
- [ ] Commit ve Rollback işlemlerini öğrenme
- [ ] İç içe transaction'ları anlama
- [ ] SavePoint kullanımını öğrenme

### Try-Catch
- [ ] Exception handling mantığını kavrama
- [ ] Farklı exception türlerini tanıma
- [ ] Finally bloğunun önemini anlama
- [ ] Custom exception oluşturmayı öğrenme
- [ ] Exception hierarchy'yi kavrama

### Exception Management
- [ ] Hata loglama tekniklerini öğrenme
- [ ] Farklı hata stratejilerini anlama
- [ ] Global exception handling'i kavrama
- [ ] Retry pattern'i implement etmeyi öğrenme
- [ ] Aggregate exception kullanımını anlama

### Gerçek Hayat Örnekleri
- [ ] Gerçek hayat senaryolarında C# kullanımını öğrenme
- [ ] İş kurallarına uygun hata yönetimini anlama
- [ ] Custom exception sınıfları oluşturmayı öğrenme
- [ ] Pratik problem çözme yaklaşımlarını kavrama
- [ ] Sistem tasarımı ve implementasyonunu öğrenme

## 💡 Önemli Notlar

1. **Her örnek tam çalışır durumdadır** - Gerçek veritabanı bağlantısı gerektirmez
2. **Console.WriteLine ile çıktılar** - Her adımda ne olduğu açıkça gösterilir
3. **Türkçe açıklamalar** - Tüm kodlar ve açıklamalar Türkçe'dir
4. **Pratik örnekler** - Gerçek hayat senaryoları kullanılmıştır
5. **Adım adım öğrenme** - Basit'ten karmaşığa doğru ilerler

## 🔧 Özelleştirme

Her örnek kendi başına çalışır durumdadır. İsterseniz:
- Parametreleri değiştirebilirsiniz
- Yeni senaryolar ekleyebilirsiniz
- Farklı hata türleri deneyebilirsiniz
- Kodları modifiye edebilirsiniz

## 📞 Destek

Herhangi bir sorunuz olursa:
- Kodları dikkatlice inceleyin
- Console çıktılarını takip edin
- Hata mesajlarını okuyun
- Gerekirse debug modunda çalıştırın

## 🎉 Başarılar!

Bu örnekleri tamamladıktan sonra C# temel konularında güçlü bir temel oluşturmuş olacaksınız. Her konuyu anlayarak ve pratik yaparak ilerleyin.

**İyi çalışmalar! 🚀** 
