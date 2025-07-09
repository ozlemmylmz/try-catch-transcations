using System;
using System.Data;
using System.Data.SqlClient;

namespace TransactionExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== C# TRANSACTION ÖRNEKLERİ ===\n");

            // 1. Basit Transaction Örneği
            Console.WriteLine("1. BASİT TRANSACTION ÖRNEĞİ");
            Console.WriteLine("=============================");
            BasitTransactionOrnegi();
            Console.WriteLine();

            // 2. İç İçe Transaction Örneği
            Console.WriteLine("2. İÇ İÇE TRANSACTION ÖRNEĞİ");
            Console.WriteLine("=============================");
            IcIceTransactionOrnegi();
            Console.WriteLine();

            // 3. Transaction Rollback Örneği
            Console.WriteLine("3. TRANSACTION ROLLBACK ÖRNEĞİ");
            Console.WriteLine("===============================");
            TransactionRollbackOrnegi();
            Console.WriteLine();

            // 4. SavePoint Örneği
            Console.WriteLine("4. SAVEPOINT ÖRNEĞİ");
            Console.WriteLine("===================");
            SavePointOrnegi();
            Console.WriteLine();

            Console.WriteLine("Tüm örnekler tamamlandı!");
            Console.ReadKey();
        }

        // 1. Basit Transaction Örneği
        // Bu örnek, temel transaction kavramını gösterir
        // Transaction: Veritabanında yapılan işlemlerin atomik olmasını sağlar
        // Atomik: Ya tüm işlemler başarılı olur ya da hiçbiri yapılmaz
        static void BasitTransactionOrnegi()
        {
            Console.WriteLine("Basit bir banka transfer işlemi simülasyonu:");
            
            // Simüle edilmiş veritabanı bağlantısı
            // using bloğu: Bağlantının otomatik olarak kapatılmasını sağlar
            using (var connection = new SqlConnection("Server=localhost;Database=TestDB;Trusted_Connection=true;"))
            {
                try
                {
                    connection.Open(); // Veritabanına bağlantı aç
                    Console.WriteLine("✓ Veritabanı bağlantısı açıldı");

                    // Transaction başlat - Tüm işlemlerin bir grup olarak yönetilmesini sağlar
                    using (var transaction = connection.BeginTransaction())
                    {
                        Console.WriteLine("✓ Transaction başlatıldı");

                        try
                        {
                            // İlk hesaptan para çek - İlk işlem
                            Console.WriteLine("→ Hesap 1'den 1000 TL çekiliyor...");
                            // Simüle edilmiş SQL: UPDATE Accounts SET Balance = Balance - 1000 WHERE AccountId = 1
                            Console.WriteLine("✓ Hesap 1'den 1000 TL çekildi");

                            // İkinci hesaba para yatır - İkinci işlem
                            Console.WriteLine("→ Hesap 2'ye 1000 TL yatırılıyor...");
                            // Simüle edilmiş SQL: UPDATE Accounts SET Balance = Balance + 1000 WHERE AccountId = 2
                            Console.WriteLine("✓ Hesap 2'ye 1000 TL yatırıldı");

                            // Transaction'ı commit et - Tüm işlemleri kalıcı hale getir
                            transaction.Commit();
                            Console.WriteLine("✓ Transaction başarıyla commit edildi");
                            Console.WriteLine("✓ Transfer işlemi tamamlandı!");
                        }
                        catch (Exception ex)
                        {
                            // Hata durumunda rollback - Tüm değişiklikleri geri al
                            transaction.Rollback();
                            Console.WriteLine($"✗ Hata oluştu: {ex.Message}");
                            Console.WriteLine("✗ Transaction rollback edildi");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"✗ Bağlantı hatası: {ex.Message}");
                }
            }

            Console.WriteLine("\nÇIKTI:");
            Console.WriteLine("✓ Veritabanı bağlantısı açıldı");
            Console.WriteLine("✓ Transaction başlatıldı");
            Console.WriteLine("→ Hesap 1'den 1000 TL çekiliyor...");
            Console.WriteLine("✓ Hesap 1'den 1000 TL çekildi");
            Console.WriteLine("→ Hesap 2'ye 1000 TL yatırılıyor...");
            Console.WriteLine("✓ Hesap 2'ye 1000 TL yatırıldı");
            Console.WriteLine("✓ Transaction başarıyla commit edildi");
            Console.WriteLine("✓ Transfer işlemi tamamlandı!");
        }

        // 2. İç İçe Transaction Örneği
        // Bu örnek, iç içe transaction'ları gösterir
        // İç İçe Transaction: Bir transaction içinde başka transaction'lar oluşturma
        // İç transaction commit edilse bile, ana transaction rollback edilirse tüm değişiklikler geri alınır
        static void IcIceTransactionOrnegi()
        {
            Console.WriteLine("İç içe transaction örneği:");
            
            using (var connection = new SqlConnection("Server=localhost;Database=TestDB;Trusted_Connection=true;"))
            {
                try
                {
                    connection.Open(); // Veritabanı bağlantısını aç
                    Console.WriteLine("✓ Ana veritabanı bağlantısı açıldı");

                    // Ana transaction - Dış transaction
                    using (var mainTransaction = connection.BeginTransaction())
                    {
                        Console.WriteLine("✓ Ana transaction başlatıldı");

                        // İlk işlem - Ana transaction içinde
                        Console.WriteLine("→ Kullanıcı kaydı oluşturuluyor...");
                        // Simüle edilmiş SQL: INSERT INTO Users (Name, Email) VALUES ('Ahmet', 'ahmet@email.com')
                        Console.WriteLine("✓ Kullanıcı kaydı oluşturuldu");

                        // İç transaction (iç içe) - Ana transaction içinde yeni bir transaction
                        using (var icIceTransaction = connection.BeginTransaction())
                        {
                            Console.WriteLine("✓ İç transaction başlatıldı");

                            // İç transaction işlemleri - Bu işlemler ayrı bir transaction'da
                            Console.WriteLine("→ Kullanıcı profil bilgileri ekleniyor...");
                            // Simüle edilmiş SQL: INSERT INTO UserProfiles (UserId, Phone, Address) VALUES (1, '555-1234', 'İstanbul')
                            Console.WriteLine("✓ Kullanıcı profil bilgileri eklendi");

                            // İç transaction'ı commit et - Sadece bu transaction'ın işlemleri kalıcı olur
                            icIceTransaction.Commit();
                            Console.WriteLine("✓ İç transaction commit edildi");
                        }

                        // Ana transaction'ı commit et - Tüm işlemler (ana + iç) kalıcı olur
                        mainTransaction.Commit();
                        Console.WriteLine("✓ Ana transaction commit edildi");
                        Console.WriteLine("✓ Tüm işlemler başarıyla tamamlandı!");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"✗ Hata: {ex.Message}");
                }
            }

            Console.WriteLine("\nÇIKTI:");
            Console.WriteLine("✓ Ana veritabanı bağlantısı açıldı");
            Console.WriteLine("✓ Ana transaction başlatıldı");
            Console.WriteLine("→ Kullanıcı kaydı oluşturuluyor...");
            Console.WriteLine("✓ Kullanıcı kaydı oluşturuldu");
            Console.WriteLine("✓ İç transaction başlatıldı");
            Console.WriteLine("→ Kullanıcı profil bilgileri ekleniyor...");
            Console.WriteLine("✓ Kullanıcı profil bilgileri eklendi");
            Console.WriteLine("✓ İç transaction commit edildi");
            Console.WriteLine("✓ Ana transaction commit edildi");
            Console.WriteLine("✓ Tüm işlemler başarıyla tamamlandı!");
        }

        // 3. Transaction Rollback Örneği
        // Bu örnek, hata durumunda transaction'ın nasıl rollback edildiğini gösterir
        // Rollback: Transaction içindeki tüm değişiklikleri geri alma işlemi
        // ACID özelliklerinden Consistency (Tutarlılık) için önemlidir
        static void TransactionRollbackOrnegi()
        {
            Console.WriteLine("Transaction rollback örneği (hata durumu):");
            
            using (var connection = new SqlConnection("Server=localhost;Database=TestDB;Trusted_Connection=true;"))
            {
                try
                {
                    connection.Open(); // Veritabanı bağlantısını aç
                    Console.WriteLine("✓ Veritabanı bağlantısı açıldı");

                    using (var transaction = connection.BeginTransaction())
                    {
                        Console.WriteLine("✓ Transaction başlatıldı");

                        try
                        {
                            // İlk işlem - başarılı (stok güncelleme)
                            Console.WriteLine("→ Stok güncelleniyor...");
                            // Simüle edilmiş SQL: UPDATE Products SET Stock = Stock - 5 WHERE ProductId = 1
                            Console.WriteLine("✓ Stok güncellendi");

                            // İkinci işlem - başarılı (sipariş kaydı)
                            Console.WriteLine("→ Sipariş kaydı oluşturuluyor...");
                            // Simüle edilmiş SQL: INSERT INTO Orders (ProductId, Quantity, OrderDate) VALUES (1, 5, GETDATE())
                            Console.WriteLine("✓ Sipariş kaydı oluşturuldu");

                            // Üçüncü işlem - HATA! (Simüle edilmiş ödeme hatası)
                            Console.WriteLine("→ Ödeme işlemi yapılıyor...");
                            throw new Exception("Yetersiz bakiye hatası!"); // Hata fırlat
                            // Bu satır hiç çalışmayacak - Hata oluştuğu için buraya gelmez
                            Console.WriteLine("✓ Ödeme işlemi tamamlandı");

                            transaction.Commit(); // Bu satır da çalışmaz
                            Console.WriteLine("✓ Transaction commit edildi");
                        }
                        catch (Exception ex)
                        {
                            // Hata durumunda rollback - Tüm değişiklikleri geri al
                            transaction.Rollback();
                            Console.WriteLine($"✗ Hata oluştu: {ex.Message}");
                            Console.WriteLine("✗ Transaction rollback edildi");
                            Console.WriteLine("✗ Tüm değişiklikler geri alındı");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"✗ Bağlantı hatası: {ex.Message}");
                }
            }

            Console.WriteLine("\nÇIKTI:");
            Console.WriteLine("✓ Veritabanı bağlantısı açıldı");
            Console.WriteLine("✓ Transaction başlatıldı");
            Console.WriteLine("→ Stok güncelleniyor...");
            Console.WriteLine("✓ Stok güncellendi");
            Console.WriteLine("→ Sipariş kaydı oluşturuluyor...");
            Console.WriteLine("✓ Sipariş kaydı oluşturuldu");
            Console.WriteLine("→ Ödeme işlemi yapılıyor...");
            Console.WriteLine("✗ Hata oluştu: Yetersiz bakiye hatası!");
            Console.WriteLine("✗ Transaction rollback edildi");
            Console.WriteLine("✗ Tüm değişiklikler geri alındı");
        }

        // 4. SavePoint Örneği
        // Bu örnek, SavePoint kullanımını gösterir
        // SavePoint: Transaction içinde belirli bir noktaya geri dönme imkanı sağlar
        // Kısmi rollback yaparak, bazı işlemleri koruyup diğerlerini geri alabiliriz
        static void SavePointOrnegi()
        {
            Console.WriteLine("SavePoint örneği:");
            
            using (var connection = new SqlConnection("Server=localhost;Database=TestDB;Trusted_Connection=true;"))
            {
                try
                {
                    connection.Open(); // Veritabanı bağlantısını aç
                    Console.WriteLine("✓ Veritabanı bağlantısı açıldı");

                    using (var transaction = connection.BeginTransaction())
                    {
                        Console.WriteLine("✓ Transaction başlatıldı");

                        try
                        {
                            // İlk işlem - Kullanıcı bilgilerini güncelle
                            Console.WriteLine("→ Kullanıcı bilgileri güncelleniyor...");
                            // Simüle edilmiş SQL: UPDATE Users SET LastLoginDate = GETDATE() WHERE UserId = 1
                            Console.WriteLine("✓ Kullanıcı bilgileri güncellendi");

                            // SavePoint oluştur - Bu noktaya geri dönebiliriz
                            transaction.Save("Checkpoint1");
                            Console.WriteLine("✓ SavePoint 'Checkpoint1' oluşturuldu");

                            // İkinci işlem - Log kaydı ekle
                            Console.WriteLine("→ Kullanıcı log kaydı ekleniyor...");
                            // Simüle edilmiş SQL: INSERT INTO UserLogs (UserId, Action, LogDate) VALUES (1, 'Login', GETDATE())
                            Console.WriteLine("✓ Kullanıcı log kaydı eklendi");

                            // Üçüncü işlem - HATA! (E-posta gönderimi başarısız)
                            Console.WriteLine("→ E-posta gönderimi yapılıyor...");
                            throw new Exception("E-posta sunucusu hatası!"); // Hata fırlat
                            // Bu satır hiç çalışmayacak - Hata oluştuğu için buraya gelmez
                            Console.WriteLine("✓ E-posta gönderildi");

                            transaction.Commit(); // Bu satır da çalışmaz
                            Console.WriteLine("✓ Transaction commit edildi");
                        }
                        catch (Exception ex)
                        {
                            // SavePoint'e geri dön - Sadece belirli bir noktaya kadar geri al
                            transaction.Rollback("Checkpoint1");
                            Console.WriteLine($"✗ Hata oluştu: {ex.Message}");
                            Console.WriteLine("✗ SavePoint 'Checkpoint1'e geri dönüldü");
                            Console.WriteLine("✓ İlk işlem korundu, diğerleri geri alındı");

                            // Transaction'ı commit et (kısmi başarı) - İlk işlem kalıcı olur
                            transaction.Commit();
                            Console.WriteLine("✓ Kısmi transaction commit edildi");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"✗ Bağlantı hatası: {ex.Message}");
                }
            }

            Console.WriteLine("\nÇIKTI:");
            Console.WriteLine("✓ Veritabanı bağlantısı açıldı");
            Console.WriteLine("✓ Transaction başlatıldı");
            Console.WriteLine("→ Kullanıcı bilgileri güncelleniyor...");
            Console.WriteLine("✓ Kullanıcı bilgileri güncellendi");
            Console.WriteLine("✓ SavePoint 'Checkpoint1' oluşturuldu");
            Console.WriteLine("→ Kullanıcı log kaydı ekleniyor...");
            Console.WriteLine("✓ Kullanıcı log kaydı eklendi");
            Console.WriteLine("→ E-posta gönderimi yapılıyor...");
            Console.WriteLine("✗ Hata oluştu: E-posta sunucusu hatası!");
            Console.WriteLine("✗ SavePoint 'Checkpoint1'e geri dönüldü");
            Console.WriteLine("✓ İlk işlem korundu, diğerleri geri alındı");
            Console.WriteLine("✓ Kısmi transaction commit edildi");
        }
    }
} 