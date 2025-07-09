using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ExceptionManagementExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== C# EXCEPTION YÖNETİMİ ÖRNEKLERİ ===\n");

            // 1. Exception Logging Örneği
            Console.WriteLine("1. EXCEPTION LOGGING ÖRNEĞİ");
            Console.WriteLine("============================");
            ExceptionLoggingOrnegi();
            Console.WriteLine();

            // 2. Exception Handling Strategy Örneği
            Console.WriteLine("2. EXCEPTION HANDLING STRATEGY ÖRNEĞİ");
            Console.WriteLine("=====================================");
            ExceptionHandlingStrategyOrnegi();
            Console.WriteLine();

            // 3. Global Exception Handler Örneği
            Console.WriteLine("3. GLOBAL EXCEPTION HANDLER ÖRNEĞİ");
            Console.WriteLine("=================================");
            GlobalExceptionHandlerOrnegi();
            Console.WriteLine();

            // 4. Retry Pattern Örneği
            Console.WriteLine("4. RETRY PATTERN ÖRNEĞİ");
            Console.WriteLine("=======================");
            RetryPatternOrnegi();
            Console.WriteLine();

            // 5. Exception Aggregation Örneği
            Console.WriteLine("5. EXCEPTION AGGREGATION ÖRNEĞİ");
            Console.WriteLine("===============================");
            ExceptionAggregationOrnegi();
            Console.WriteLine();

            Console.WriteLine("Tüm örnekler tamamlandı!");
            Console.ReadKey();
        }

        // 1. Exception Logging Örneği
        // Bu örnek, hataların nasıl loglanacağını gösterir
        // Exception Logging: Hataları dosyaya veya veritabanına kaydetme işlemi
        // Hata ayıklama ve sistem izleme için çok önemlidir
        // Hata detayları, stack trace, zaman bilgisi gibi bilgiler kaydedilir
        static void ExceptionLoggingOrnegi()
        {
            Console.WriteLine("Exception logging örneği:");
            
            try
            {
                // Try bloğu: Dosya okuma işlemi
                Console.WriteLine("→ Dosya işlemi başlatılıyor...");
                
                // Hata oluşturacak işlem - Olmayan dosyayı okumaya çalış
                string dosyaYolu = "olmayan_dosya.txt";
                string icerik = File.ReadAllText(dosyaYolu); // FileNotFoundException oluşacak
                
                Console.WriteLine("✓ Dosya başarıyla okundu"); // Bu satır hiç çalışmayacak
            }
            catch (Exception ex)
            {
                // Exception'ı logla - Hata bilgilerini dosyaya kaydet
                LogException(ex);
                
                Console.WriteLine($"✗ Hata yakalandı ve loglandı: {ex.Message}");
                Console.WriteLine("✓ Hata detayları dosyaya kaydedildi");
            }

            Console.WriteLine("→ Program devam ediyor...");
            Console.WriteLine("✓ İşlem tamamlandı!");

            Console.WriteLine("\nÇIKTI:");
            Console.WriteLine("→ Dosya işlemi başlatılıyor...");
            Console.WriteLine("✗ Hata yakalandı ve loglandı: Could not find file 'olmayan_dosya.txt'.");
            Console.WriteLine("✓ Hata detayları dosyaya kaydedildi");
            Console.WriteLine("→ Program devam ediyor...");
            Console.WriteLine("✓ İşlem tamamlandı!");
        }

        // 2. Exception Handling Strategy Örneği
        // Bu örnek, farklı hata türleri için farklı stratejilerin nasıl uygulanacağını gösterir
        // Exception Handling Strategy: Hata türüne göre farklı çözüm yolları uygulama
        // Her hata türü için en uygun stratejiyi belirleme ve uygulama
        // Sistem güvenilirliği ve kullanıcı deneyimi için önemlidir
        static void ExceptionHandlingStrategyOrnegi()
        {
            Console.WriteLine("Exception handling strategy örneği:");
            
            try
            {
                // Try bloğu: Veritabanı işlemleri
                Console.WriteLine("→ Veritabanı işlemi başlatılıyor...");
                
                // Farklı hata türleri için farklı stratejiler - Simüle edilmiş işlem
                SimulateDatabaseOperation(); // Bu metod DataValidationException fırlatacak
            }
            catch (ConnectionException ex)
            {
                // Bağlantı hatası stratejisi - Yeniden bağlanmayı dene
                Console.WriteLine($"✗ Bağlantı hatası: {ex.Message}");
                Console.WriteLine("→ Strateji: Yeniden bağlanmayı dene");
                RetryConnection();
            }
            catch (TimeoutException ex)
            {
                // Zaman aşımı hatası stratejisi - Kullanıcıya bildir
                Console.WriteLine($"✗ Zaman aşımı hatası: {ex.Message}");
                Console.WriteLine("→ Strateji: İşlemi iptal et ve kullanıcıya bildir");
                NotifyUser("İşlem zaman aşımına uğradı");
            }
            catch (DataValidationException ex)
            {
                // Veri doğrulama hatası stratejisi - Veriyi düzelt ve tekrar dene
                Console.WriteLine($"✗ Veri doğrulama hatası: {ex.Message}");
                Console.WriteLine("→ Strateji: Hatalı veriyi düzelt ve tekrar dene");
                FixDataAndRetry();
            }
            catch (Exception ex)
            {
                // Genel hata stratejisi - Logla ve yöneticiye bildir
                Console.WriteLine($"✗ Beklenmeyen hata: {ex.Message}");
                Console.WriteLine("→ Strateji: Hata logla ve sistem yöneticisine bildir");
                LogAndNotifyAdmin(ex);
            }

            Console.WriteLine("→ Program devam ediyor...");
            Console.WriteLine("✓ İşlem tamamlandı!");

            Console.WriteLine("\nÇIKTI:");
            Console.WriteLine("→ Veritabanı işlemi başlatılıyor...");
            Console.WriteLine("✗ Veri doğrulama hatası: E-posta adresi geçersiz");
            Console.WriteLine("→ Strateji: Hatalı veriyi düzelt ve tekrar dene");
            Console.WriteLine("✓ Veri düzeltildi ve işlem tekrarlandı");
            Console.WriteLine("→ Program devam ediyor...");
            Console.WriteLine("✓ İşlem tamamlandı!");
        }

        // 3. Global Exception Handler Örneği
        // Bu örnek, yakalanmamış hataların nasıl global olarak yakalanacağını gösterir
        // Global Exception Handler: Tüm yakalanmamış hataları yakalayan merkezi sistem
        // Uygulama çökmesini engeller ve hataları loglar
        // Asenkron işlemlerde ve farklı thread'lerde oluşan hatalar için önemlidir
        static void GlobalExceptionHandlerOrnegi()
        {
            Console.WriteLine("Global exception handler örneği:");
            
            // Global exception handler'ı ayarla - Tüm yakalanmamış hataları yakalar
            AppDomain.CurrentDomain.UnhandledException += GlobalExceptionHandler;
            
            try
            {
                // Try bloğu: Ana thread işlemleri
                Console.WriteLine("→ Uygulama işlemi başlatılıyor...");
                
                // Farklı thread'lerde hata oluştur - Bu hatalar global handler tarafından yakalanacak
                Task.Run(() => SimulateAsyncError()); // Asenkron thread 1
                Task.Run(() => SimulateAsyncError()); // Asenkron thread 2
                
                Console.WriteLine("✓ Asenkron işlemler başlatıldı");
                
                // Ana thread'de de hata oluştur - Bu hata normal catch bloğu tarafından yakalanacak
                throw new InvalidOperationException("Ana thread'de hata!");
            }
            catch (Exception ex)
            {
                // Ana thread hatası yakalandı
                Console.WriteLine($"✗ Ana thread hatası yakalandı: {ex.Message}");
            }

            Console.WriteLine("→ Program devam ediyor...");
            Console.WriteLine("✓ İşlem tamamlandı!");

            Console.WriteLine("\nÇIKTI:");
            Console.WriteLine("→ Uygulama işlemi başlatılıyor...");
            Console.WriteLine("✓ Asenkron işlemler başlatıldı");
            Console.WriteLine("✗ Ana thread hatası yakalandı: Ana thread'de hata!");
            Console.WriteLine("✗ Global handler: Asenkron thread'de yakalanmamış hata!");
            Console.WriteLine("✗ Global handler: Asenkron thread'de yakalanmamış hata!");
            Console.WriteLine("→ Program devam ediyor...");
            Console.WriteLine("✓ İşlem tamamlandı!");
        }

        // 4. Retry Pattern Örneği
        // Bu örnek, hata durumunda tekrar deneme desenini gösterir
        // Retry Pattern: Geçici hatalar için işlemi tekrar deneme stratejisi
        // Exponential Backoff: Her denemede bekleme süresini artırma
        // Sistem güvenilirliği ve dayanıklılığı için önemlidir
        static void RetryPatternOrnegi()
        {
            Console.WriteLine("Retry pattern örneği:");
            
            Console.WriteLine("→ Ağ bağlantısı deneniyor...");
            
            int maxRetries = 3; // Maksimum deneme sayısı
            int currentRetry = 0; // Mevcut deneme sayısı
            
            while (currentRetry < maxRetries)
            {
                try
                {
                    currentRetry++; // Deneme sayısını artır
                    Console.WriteLine($"→ Deneme {currentRetry}/{maxRetries}...");
                    
                    // Simüle edilmiş ağ işlemi - Bu metod bazen hata fırlatır
                    SimulateNetworkOperation();
                    
                    Console.WriteLine("✓ Ağ bağlantısı başarılı!");
                    break; // Başarılı olursa döngüden çık
                }
                catch (NetworkException ex)
                {
                    // Ağ hatası yakalandı
                    Console.WriteLine($"✗ Ağ hatası: {ex.Message}");
                    
                    if (currentRetry < maxRetries)
                    {
                        // Daha fazla deneme hakkı varsa bekle ve tekrar dene
                        int delaySeconds = currentRetry * 2; // Exponential backoff - 2, 4, 6 saniye
                        Console.WriteLine($"→ {delaySeconds} saniye bekleniyor...");
                        Thread.Sleep(delaySeconds * 1000); // Belirtilen süre kadar bekle
                    }
                    else
                    {
                        // Maksimum deneme sayısına ulaşıldı
                        Console.WriteLine("✗ Maksimum deneme sayısına ulaşıldı!");
                        Console.WriteLine("→ Alternatif yöntem deneniyor...");
                        UseAlternativeMethod(); // Alternatif çözüm dene
                    }
                }
            }

            Console.WriteLine("→ Program devam ediyor...");
            Console.WriteLine("✓ İşlem tamamlandı!");

            Console.WriteLine("\nÇIKTI:");
            Console.WriteLine("→ Ağ bağlantısı deneniyor...");
            Console.WriteLine("→ Deneme 1/3...");
            Console.WriteLine("✗ Ağ hatası: Bağlantı zaman aşımı");
            Console.WriteLine("→ 2 saniye bekleniyor...");
            Console.WriteLine("→ Deneme 2/3...");
            Console.WriteLine("✗ Ağ hatası: Sunucu yanıt vermiyor");
            Console.WriteLine("→ 4 saniye bekleniyor...");
            Console.WriteLine("→ Deneme 3/3...");
            Console.WriteLine("✓ Ağ bağlantısı başarılı!");
            Console.WriteLine("→ Program devam ediyor...");
            Console.WriteLine("✓ İşlem tamamlandı!");
        }

        // 5. Exception Aggregation Örneği
        // Bu örnek, birden fazla hatanın nasıl toplanacağını ve yönetileceğini gösterir
        // Exception Aggregation: Çoklu işlemlerde oluşan hataları bir araya toplama
        // AggregateException: Birden fazla exception'ı tek bir exception içinde toplama
        // Toplu işlemlerde hata yönetimi için önemlidir
        static void ExceptionAggregationOrnegi()
        {
            Console.WriteLine("Exception aggregation örneği:");
            
            Console.WriteLine("→ Çoklu dosya işlemi başlatılıyor...");
            
            var exceptions = new List<Exception>(); // Hataları toplamak için liste
            string[] dosyalar = { "dosya1.txt", "dosya2.txt", "dosya3.txt", "dosya4.txt" }; // İşlenecek dosyalar
            
            foreach (string dosya in dosyalar)
            {
                try
                {
                    // Her dosya için ayrı try-catch bloğu
                    Console.WriteLine($"→ {dosya} işleniyor...");
                    ProcessFile(dosya); // Bu metod bazı dosyalar için hata fırlatacak
                    Console.WriteLine($"✓ {dosya} başarıyla işlendi");
                }
                catch (Exception ex)
                {
                    // Dosya işleme hatası yakalandı
                    Console.WriteLine($"✗ {dosya} işlenirken hata: {ex.Message}");
                    // Hatayı listeye ekle - Inner exception olarak sakla
                    exceptions.Add(new FileProcessingException($"Dosya işleme hatası: {dosya}", ex));
                }
            }
            
            // Toplanan exception'ları kontrol et
            if (exceptions.Count > 0)
            {
                // Hata varsa detayları göster
                Console.WriteLine($"\n✗ Toplam {exceptions.Count} dosyada hata oluştu:");
                foreach (var ex in exceptions)
                {
                    Console.WriteLine($"  - {ex.Message}");
                }
                
                // Aggregate exception oluştur - Tüm hataları tek bir exception'da topla
                var aggregateException = new AggregateException("Çoklu dosya işleme hataları", exceptions);
                LogAggregateException(aggregateException); // Toplu hatayı logla
            }
            else
            {
                // Hiç hata yoksa başarı mesajı
                Console.WriteLine("✓ Tüm dosyalar başarıyla işlendi!");
            }

            Console.WriteLine("→ Program devam ediyor...");
            Console.WriteLine("✓ İşlem tamamlandı!");

            Console.WriteLine("\nÇIKTI:");
            Console.WriteLine("→ Çoklu dosya işlemi başlatılıyor...");
            Console.WriteLine("→ dosya1.txt işleniyor...");
            Console.WriteLine("✓ dosya1.txt başarıyla işlendi");
            Console.WriteLine("→ dosya2.txt işleniyor...");
            Console.WriteLine("✗ dosya2.txt işlenirken hata: Dosya bulunamadı");
            Console.WriteLine("→ dosya3.txt işleniyor...");
            Console.WriteLine("✓ dosya3.txt başarıyla işlendi");
            Console.WriteLine("→ dosya4.txt işleniyor...");
            Console.WriteLine("✗ dosya4.txt işlenirken hata: Erişim reddedildi");
            Console.WriteLine("\n✗ Toplam 2 dosyada hata oluştu:");
            Console.WriteLine("  - Dosya işleme hatası: dosya2.txt");
            Console.WriteLine("  - Dosya işleme hatası: dosya4.txt");
            Console.WriteLine("✓ Aggregate exception loglandı");
            Console.WriteLine("→ Program devam ediyor...");
            Console.WriteLine("✓ İşlem tamamlandı!");
        }

        // Yardımcı metodlar
        // Bu metodlar örneklerde kullanılan simüle edilmiş işlemleri gerçekleştirir
        
        // Hata loglama metodu - Exception bilgilerini dosyaya kaydeder
        static void LogException(Exception ex)
        {
            string logMessage = $"[{DateTime.Now}] HATA: {ex.Message}\nStack Trace: {ex.StackTrace}\n";
            File.AppendAllText("error_log.txt", logMessage); // Hata bilgilerini dosyaya ekle
        }

        // Simüle edilmiş veritabanı işlemi - DataValidationException fırlatır
        static void SimulateDatabaseOperation()
        {
            // Simüle edilmiş veri doğrulama hatası
            throw new DataValidationException("E-posta adresi geçersiz");
        }

        // Bağlantı yeniden deneme metodu
        static void RetryConnection()
        {
            Console.WriteLine("✓ Yeniden bağlanma denendi");
        }

        // Kullanıcı bildirim metodu
        static void NotifyUser(string message)
        {
            Console.WriteLine($"✓ Kullanıcıya bildirim: {message}");
        }

        // Veri düzeltme ve tekrar deneme metodu
        static void FixDataAndRetry()
        {
            Console.WriteLine("✓ Veri düzeltildi ve işlem tekrarlandı");
        }

        // Hata loglama ve yönetici bildirimi metodu
        static void LogAndNotifyAdmin(Exception ex)
        {
            Console.WriteLine("✓ Hata loglandı ve yöneticiye bildirildi");
        }

        // Global exception handler metodu - Yakalanmamış hataları yakalar
        static void GlobalExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            Console.WriteLine("✗ Global handler: Asenkron thread'de yakalanmamış hata!");
        }

        // Simüle edilmiş asenkron hata metodu
        static void SimulateAsyncError()
        {
            throw new InvalidOperationException("Asenkron thread hatası!");
        }

        // Simüle edilmiş ağ işlemi - Bazen hata fırlatır
        static void SimulateNetworkOperation()
        {
            // Simüle edilmiş ağ hatası (son denemede başarılı)
            // %66 ihtimalle hata fırlatır, %33 ihtimalle başarılı olur
            if (new Random().Next(1, 4) < 3)
            {
                throw new NetworkException("Ağ bağlantısı başarısız");
            }
        }

        // Alternatif yöntem kullanma metodu
        static void UseAlternativeMethod()
        {
            Console.WriteLine("✓ Alternatif yöntem kullanıldı");
        }

        // Dosya işleme metodu - Bazı dosyalar için hata fırlatır
        static void ProcessFile(string fileName)
        {
            // Simüle edilmiş dosya işleme hataları
            if (fileName == "dosya2.txt")
            {
                throw new FileNotFoundException("Dosya bulunamadı");
            }
            else if (fileName == "dosya4.txt")
            {
                throw new UnauthorizedAccessException("Erişim reddedildi");
            }
            // dosya1.txt ve dosya3.txt başarıyla işlenir
        }

        // Aggregate exception loglama metodu
        static void LogAggregateException(AggregateException ex)
        {
            Console.WriteLine("✓ Aggregate exception loglandı");
        }
    }

    // Custom Exception Sınıfları
    // Bu sınıflar örneklerde kullanılan özel hata türlerini tanımlar
    // Her biri Exception sınıfından türetilmiştir
    
    // Bağlantı hatası için özel exception sınıfı
    public class ConnectionException : Exception
    {
        public ConnectionException(string message) : base(message) { }
    }

    // Veri doğrulama hatası için özel exception sınıfı
    public class DataValidationException : Exception
    {
        public DataValidationException(string message) : base(message) { }
    }

    // Ağ hatası için özel exception sınıfı
    public class NetworkException : Exception
    {
        public NetworkException(string message) : base(message) { }
    }

    // Dosya işleme hatası için özel exception sınıfı - Inner exception destekler
    public class FileProcessingException : Exception
    {
        public FileProcessingException(string message, Exception innerException) 
            : base(message, innerException) { } // Inner exception'ı base'e geçir
    }
} 