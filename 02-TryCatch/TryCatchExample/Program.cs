using System;
using System.IO;

namespace TryCatchExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== C# TRY-CATCH ÖRNEKLERİ ===\n");

            // 1. Basit Try-Catch Örneği
            Console.WriteLine("1. BASİT TRY-CATCH ÖRNEĞİ");
            Console.WriteLine("===========================");
            BasitTryCatchOrnegi();
            Console.WriteLine();

            // 2. Multiple Catch Blocks Örneği
            Console.WriteLine("2. MULTIPLE CATCH BLOCKS ÖRNEĞİ");
            Console.WriteLine("================================");
            MultipleCatchBlocksOrnegi();
            Console.WriteLine();

            // 3. Try-Catch-Finally Örneği
            Console.WriteLine("3. TRY-CATCH-FINALLY ÖRNEĞİ");
            Console.WriteLine("============================");
            TryCatchFinallyOrnegi();
            Console.WriteLine();

            // 4. İç İçe Try-Catch Örneği
            Console.WriteLine("4. İÇ İÇE TRY-CATCH ÖRNEĞİ");
            Console.WriteLine("===========================");
            IcIceTryCatchOrnegi();
            Console.WriteLine();

            // 5. Custom Exception Örneği
            Console.WriteLine("5. CUSTOM EXCEPTION ÖRNEĞİ");
            Console.WriteLine("==========================");
            CustomExceptionOrnegi();
            Console.WriteLine();

            Console.WriteLine("Tüm örnekler tamamlandı!");
            Console.ReadKey();
        }

        // 1. Basit Try-Catch Örneği
        // Bu örnek, temel try-catch kullanımını gösterir
        // Try: Hata oluşabilecek kodları bu bloğa yazarız
        // Catch: Hata oluştuğunda çalışacak kodları bu bloğa yazarız
        // Exception Handling: Programın çökmesini engelleyerek kontrollü hata yönetimi sağlar
        static void BasitTryCatchOrnegi()
        {
            Console.WriteLine("Basit try-catch örneği:");
            
            try
            {
                // Try bloğu: Hata oluşabilecek kodlar buraya yazılır
                Console.WriteLine("→ Sayı bölme işlemi yapılıyor...");
                int sayi1 = 10;
                int sayi2 = 0; // Sıfıra bölme hatası oluşturacak
                int sonuc = sayi1 / sayi2; // Bu satır hata verecek - DivideByZeroException
                Console.WriteLine($"Sonuç: {sonuc}"); // Bu satır hiç çalışmayacak
            }
            catch (DivideByZeroException ex)
            {
                // Catch bloğu: Belirli bir hata türü oluştuğunda çalışır
                // ex: Hata bilgilerini içeren exception nesnesi
                Console.WriteLine($"✗ Hata yakalandı: {ex.Message}");
                Console.WriteLine("✓ Program çökmek yerine hatayı yakaladı ve devam ediyor");
            }

            // Try-catch bloğu dışındaki kodlar her durumda çalışır
            Console.WriteLine("→ Program normal şekilde devam ediyor...");
            Console.WriteLine("✓ İşlem tamamlandı!");

            Console.WriteLine("\nÇIKTI:");
            Console.WriteLine("→ Sayı bölme işlemi yapılıyor...");
            Console.WriteLine("✗ Hata yakalandı: Attempted to divide by zero.");
            Console.WriteLine("✓ Program çökmek yerine hatayı yakaladı ve devam ediyor");
            Console.WriteLine("→ Program normal şekilde devam ediyor...");
            Console.WriteLine("✓ İşlem tamamlandı!");
        }

        // 2. Multiple Catch Blocks Örneği
        // Bu örnek, birden fazla catch bloğu kullanımını gösterir
        // Farklı hata türleri için farklı işlemler yapabiliriz
        // Catch blokları spesifik'ten genel'e doğru sıralanmalıdır
        // Exception hierarchy'ye dikkat edilmelidir
        static void MultipleCatchBlocksOrnegi()
        {
            Console.WriteLine("Multiple catch blocks örneği:");
            
            try
            {
                // Try bloğu: Dosya işlemleri
                Console.WriteLine("→ Dosya okuma işlemi başlatılıyor...");
                
                // Farklı hata türleri için test
                string dosyaYolu = "olmayan_dosya.txt";
                
                // Dosya var mı kontrol et
                if (!File.Exists(dosyaYolu))
                {
                    // Manuel olarak hata fırlat
                    throw new FileNotFoundException("Dosya bulunamadı!");
                }
                
                // Dosyayı oku
                string icerik = File.ReadAllText(dosyaYolu);
                Console.WriteLine($"Dosya içeriği: {icerik}");
            }
            catch (FileNotFoundException ex)
            {
                // En spesifik hata türü - Dosya bulunamadı
                Console.WriteLine($"✗ Dosya hatası: {ex.Message}");
                Console.WriteLine("✓ Dosya bulunamadı hatası yakalandı");
            }
            catch (UnauthorizedAccessException ex)
            {
                // Yetki hatası - Dosyaya erişim izni yok
                Console.WriteLine($"✗ Yetki hatası: {ex.Message}");
                Console.WriteLine("✓ Dosyaya erişim yetkisi yok");
            }
            catch (IOException ex)
            {
                // Genel dosya işlem hatası
                Console.WriteLine($"✗ Giriş/Çıkış hatası: {ex.Message}");
                Console.WriteLine("✓ Dosya okuma/yazma hatası");
            }
            catch (Exception ex)
            {
                // En genel hata türü - Tüm diğer hatalar için
                // Bu blok en sonda olmalı, çünkü tüm exception'ları yakalar
                Console.WriteLine($"✗ Genel hata: {ex.Message}");
                Console.WriteLine("✓ Beklenmeyen bir hata oluştu");
            }

            Console.WriteLine("→ Program devam ediyor...");
            Console.WriteLine("✓ İşlem tamamlandı!");

            Console.WriteLine("\nÇIKTI:");
            Console.WriteLine("→ Dosya okuma işlemi başlatılıyor...");
            Console.WriteLine("✗ Dosya hatası: Dosya bulunamadı!");
            Console.WriteLine("✓ Dosya bulunamadı hatası yakalandı");
            Console.WriteLine("→ Program devam ediyor...");
            Console.WriteLine("✓ İşlem tamamlandı!");
        }

        // 3. Try-Catch-Finally Örneği
        // Bu örnek, finally bloğunun kullanımını gösterir
        // Finally: Try veya catch bloklarından hangisi çalışırsa çalışsın, her durumda çalışır
        // Kaynak temizleme (resource cleanup) için çok önemlidir
        // Dosya kapatma, veritabanı bağlantısı kapatma gibi işlemler için kullanılır
        static void TryCatchFinallyOrnegi()
        {
            Console.WriteLine("Try-catch-finally örneği:");
            
            StreamReader? reader = null; // Dosya okuyucu nesnesi
            
            try
            {
                // Try bloğu: Dosya işlemleri
                Console.WriteLine("→ Dosya açılıyor...");
                reader = new StreamReader("test.txt"); // Dosyayı aç
                Console.WriteLine("✓ Dosya başarıyla açıldı");
                
                Console.WriteLine("→ Dosya okunuyor...");
                string icerik = reader.ReadToEnd(); // Dosyayı oku
                Console.WriteLine($"✓ Dosya içeriği okundu: {icerik.Length} karakter");
                
            }
            catch (FileNotFoundException ex)
            {
                // Dosya bulunamadı hatası
                Console.WriteLine($"✗ Dosya bulunamadı: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Genel hata
                Console.WriteLine($"✗ Genel hata: {ex.Message}");
            }
            finally
            {
                // Finally bloğu: Her durumda çalışır (hata olsa da olmasa da)
                Console.WriteLine("→ Finally bloğu çalışıyor...");
                if (reader != null)
                {
                    reader.Close(); // Dosyayı kapat
                    Console.WriteLine("✓ Dosya kapatıldı");
                }
                Console.WriteLine("✓ Kaynaklar temizlendi");
            }

            Console.WriteLine("→ Program devam ediyor...");
            Console.WriteLine("✓ İşlem tamamlandı!");

            Console.WriteLine("\nÇIKTI:");
            Console.WriteLine("→ Dosya açılıyor...");
            Console.WriteLine("✓ Dosya başarıyla açıldı");
            Console.WriteLine("→ Dosya okunuyor...");
            Console.WriteLine("✓ Dosya içeriği okundu: 0 karakter");
            Console.WriteLine("✗ Genel hata: Beklenmeyen bir hata oluştu!");
            Console.WriteLine("→ Finally bloğu çalışıyor...");
            Console.WriteLine("✓ Dosya kapatıldı");
            Console.WriteLine("✓ Kaynaklar temizlendi");
            Console.WriteLine("→ Program devam ediyor...");
            Console.WriteLine("✓ İşlem tamamlandı!");
        }

        // 4. İç İçe Try-Catch Örneği
        // Bu örnek, iç içe try-catch bloklarının kullanımını gösterir
        // İç İçe Try-Catch: Bir try-catch bloğu içinde başka try-catch blokları olabilir
        // İç catch'ten fırlatılan hatalar dış catch tarafından yakalanabilir
        // Hata yönetiminde hiyerarşik yapı oluşturmak için kullanılır
        static void IcIceTryCatchOrnegi()
        {
            Console.WriteLine("İç içe try-catch örneği:");
            
            try
            {
                // Dış try bloğu - Ana işlemler
                Console.WriteLine("→ Dış try bloğu başladı");
                
                try
                {
                    // İç try bloğu - Alt işlemler
                    Console.WriteLine("→ İç try bloğu başladı");
                    Console.WriteLine("→ Matematik işlemi yapılıyor...");
                    
                    int[] sayilar = { 1, 2, 3 }; // 3 elemanlı dizi
                    int sonuc = sayilar[10]; // IndexOutOfRangeException - 10. indeks yok
                    
                    Console.WriteLine($"Sonuç: {sonuc}"); // Bu satır hiç çalışmayacak
                }
                catch (IndexOutOfRangeException ex)
                {
                    // İç catch: Dizi indeks hatası yakalandı
                    Console.WriteLine($"✗ İç catch: Dizi indeks hatası: {ex.Message}");
                    Console.WriteLine("✓ İç catch bloğu hatayı yakaladı");
                    
                    // İç catch'ten yeni bir hata fırlat - Bu hata dış catch'e gider
                    throw new InvalidOperationException("İç işlem başarısız!");
                }
                catch (Exception ex)
                {
                    // İç catch: Genel hata (bu örnekte çalışmayacak)
                    Console.WriteLine($"✗ İç catch: Genel hata: {ex.Message}");
                }
                
                Console.WriteLine("→ İç try bloğu tamamlandı"); // Bu satır hiç çalışmayacak
            }
            catch (InvalidOperationException ex)
            {
                // Dış catch: İç catch'ten fırlatılan InvalidOperationException yakalandı
                Console.WriteLine($"✗ Dış catch: İşlem hatası: {ex.Message}");
                Console.WriteLine("✓ Dış catch bloğu hatayı yakaladı");
            }
            catch (Exception ex)
            {
                // Dış catch: Genel hata (bu örnekte çalışmayacak)
                Console.WriteLine($"✗ Dış catch: Genel hata: {ex.Message}");
            }

            Console.WriteLine("→ Program devam ediyor...");
            Console.WriteLine("✓ İşlem tamamlandı!");

            Console.WriteLine("\nÇIKTI:");
            Console.WriteLine("→ Dış try bloğu başladı");
            Console.WriteLine("→ İç try bloğu başladı");
            Console.WriteLine("→ Matematik işlemi yapılıyor...");
            Console.WriteLine("✗ İç catch: Dizi indeks hatası: Index was outside the bounds of the array.");
            Console.WriteLine("✓ İç catch bloğu hatayı yakaladı");
            Console.WriteLine("✗ Dış catch: İşlem hatası: İç işlem başarısız!");
            Console.WriteLine("✓ Dış catch bloğu hatayı yakaladı");
            Console.WriteLine("→ Program devam ediyor...");
            Console.WriteLine("✓ İşlem tamamlandı!");
        }

        // 5. Custom Exception Örneği
        // Bu örnek, özel exception sınıflarının nasıl oluşturulduğunu ve kullanıldığını gösterir
        // Custom Exception: Kendi iş mantığımıza uygun hata türleri oluşturabiliriz
        // Exception sınıfından türetilerek oluşturulur
        // İş kurallarına özel hata mesajları ve kodları içerebilir
        static void CustomExceptionOrnegi()
        {
            Console.WriteLine("Custom exception örneği:");
            
            try
            {
                // Try bloğu: Yaş kontrolü işlemi
                Console.WriteLine("→ Kullanıcı yaşı kontrol ediliyor...");
                int yas = 15; // Test için 15 yaş (18'den küçük)
                
                // İş kuralı kontrolü
                if (yas < 18)
                {
                    // Özel exception fırlat - Kendi oluşturduğumuz hata türü
                    throw new YasKontrolException("Kullanıcı 18 yaşından küçük olamaz!");
                }
                
                Console.WriteLine("✓ Yaş kontrolü başarılı"); // Bu satır hiç çalışmayacak
            }
            catch (YasKontrolException ex)
            {
                // Özel exception yakalandı
                Console.WriteLine($"✗ Yaş hatası: {ex.Message}");
                Console.WriteLine($"✗ Hata kodu: {ex.HataKodu}"); // Özel property
                Console.WriteLine("✓ Custom exception yakalandı");
            }
            catch (Exception ex)
            {
                // Genel hata (bu örnekte çalışmayacak)
                Console.WriteLine($"✗ Genel hata: {ex.Message}");
            }

            Console.WriteLine("→ Program devam ediyor...");
            Console.WriteLine("✓ İşlem tamamlandı!");

            Console.WriteLine("\nÇIKTI:");
            Console.WriteLine("→ Kullanıcı yaşı kontrol ediliyor...");
            Console.WriteLine("✗ Yaş hatası: Kullanıcı 18 yaşından küçük olamaz!");
            Console.WriteLine("✗ Hata kodu: YAS_001");
            Console.WriteLine("✓ Custom exception yakalandı");
            Console.WriteLine("→ Program devam ediyor...");
            Console.WriteLine("✓ İşlem tamamlandı!");
        }
    }

    // Custom Exception Sınıfı
    // Exception sınıfından türetilerek özel hata türü oluşturulur
    // Kendi iş mantığımıza uygun hata türleri tanımlayabiliriz
    // Özel property'ler ve constructor'lar ekleyebiliriz
    public class YasKontrolException : Exception
    {
        // Özel property - Hata kodu
        public string HataKodu { get; }

        // Constructor 1: Sadece mesaj ile
        public YasKontrolException(string message) : base(message)
        {
            HataKodu = "YAS_001"; // Varsayılan hata kodu
        }

        // Constructor 2: Mesaj ve hata kodu ile
        public YasKontrolException(string message, string hataKodu) : base(message)
        {
            HataKodu = hataKodu; // Özel hata kodu
        }
    }
} 