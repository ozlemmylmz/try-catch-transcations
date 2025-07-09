using System;
using System.Collections.Generic;
using System.IO;
using System.Linq; // Added for .Where()

namespace GercekHayatOrnekleri
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== C# GERÇEK HAYAT ÖRNEKLERİ ===\n");

            // 1. ATM Para Çekme Örneği
            Console.WriteLine("1. ATM PARA ÇEKME ÖRNEĞİ");
            Console.WriteLine("===========================");
            AtmParaCekmeOrnegi();
            Console.WriteLine();

            // 2. E-Ticaret Sepeti Örneği
            Console.WriteLine("2. E-TİCARET SEPETİ ÖRNEĞİ");
            Console.WriteLine("===========================");
            ETicaretSepetiOrnegi();
            Console.WriteLine();

            // 3. Hastane Randevu Sistemi Örneği
            Console.WriteLine("3. HASTANE RANDEVU SİSTEMİ ÖRNEĞİ");
            Console.WriteLine("=================================");
            HastaneRandevuSistemiOrnegi();
            Console.WriteLine();

            // 4. Kütüphane Kitap Ödünç Alma Örneği
            Console.WriteLine("4. KÜTÜPHANE KİTAP ÖDÜNÇ ALMA ÖRNEĞİ");
            Console.WriteLine("=====================================");
            KutuphaneKitapOduncAlmaOrnegi();
            Console.WriteLine();

            // 5. Online Sınav Sistemi Örneği
            Console.WriteLine("5. ONLINE SINAV SİSTEMİ ÖRNEĞİ");
            Console.WriteLine("===============================");
            OnlineSinavSistemiOrnegi();
            Console.WriteLine();

            Console.WriteLine("Tüm gerçek hayat örnekleri tamamlandı!");
            Console.ReadKey();
        }

        // 1. ATM Para Çekme Örneği - Transaction Kullanımı
        static void AtmParaCekmeOrnegi()
        {
            Console.WriteLine("ATM'den para çekme işlemi simülasyonu:");
            
            // Kullanıcı bilgileri
            string kartNo = "1234-5678-9012-3456";
            int pin = 1234;
            decimal hesapBakiyesi = 5000.00m;
            decimal cekilecekMiktar = 1000.00m;
            
            Console.WriteLine($"Kart No: {kartNo}");
            Console.WriteLine($"Hesap Bakiyesi: {hesapBakiyesi:C}");
            Console.WriteLine($"Çekilecek Miktar: {cekilecekMiktar:C}");
            Console.WriteLine();

            // Simüle edilmiş veritabanı bağlantısı
            using (var connection = new SimulatedDatabaseConnection())
            {
                try
                {
                    connection.Open();
                    Console.WriteLine("✓ Veritabanı bağlantısı açıldı");

                    // Transaction başlat
                    using (var transaction = connection.BeginTransaction())
                    {
                        Console.WriteLine("✓ Transaction başlatıldı");

                        try
                        {
                            // PIN doğrulama
                            Console.WriteLine("→ PIN doğrulanıyor...");
                            if (pin != 1234)
                            {
                                throw new Exception("Yanlış PIN kodu!");
                            }
                            Console.WriteLine("✓ PIN doğrulandı");

                            // Bakiye kontrolü
                            Console.WriteLine("→ Bakiye kontrol ediliyor...");
                            if (cekilecekMiktar > hesapBakiyesi)
                            {
                                throw new Exception("Yetersiz bakiye!");
                            }
                            Console.WriteLine("✓ Bakiye yeterli");

                            // Hesaptan para çek - İlk işlem
                            Console.WriteLine("→ Hesaptan para çekiliyor...");
                            hesapBakiyesi -= cekilecekMiktar;
                            Console.WriteLine("✓ Hesaptan para çekildi");

                            // ATM'den para ver - İkinci işlem
                            Console.WriteLine("→ ATM'den para veriliyor...");
                            Console.WriteLine("✓ Para başarıyla verildi");

                            // İşlem kaydı oluştur - Üçüncü işlem
                            Console.WriteLine("→ İşlem kaydı oluşturuluyor...");
                            Console.WriteLine($"✓ İşlem kaydı: {DateTime.Now} - {cekilecekMiktar:C} çekildi");

                            // Transaction'ı commit et
                            transaction.Commit();
                            Console.WriteLine("✓ Transaction başarıyla commit edildi");
                            Console.WriteLine($"✓ İşlem başarılı! Yeni bakiye: {hesapBakiyesi:C}");
                        }
                        catch (Exception ex)
                        {
                            // Hata durumunda rollback
                            transaction.Rollback();
                            Console.WriteLine($"✗ Hata: {ex.Message}");
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


        }

        // 2. E-Ticaret Sepeti Örneği - Try-Catch Kullanımı
        static void ETicaretSepetiOrnegi()
        {
            Console.WriteLine("E-ticaret sepeti işlemleri:");
            
            var sepet = new List<Urun>();
            var stokDurumu = new Dictionary<string, int>
            {
                {"Laptop", 5},
                {"Mouse", 20},
                {"Klavye", 15},
                {"Monitör", 3}
            };

            try
            {
                // Ürün ekleme işlemleri
                Console.WriteLine("→ Sepete ürünler ekleniyor...");
                
                // Laptop ekle
                UrunEkle(sepet, "Laptop", 15000.00m, 1, stokDurumu);
                Console.WriteLine("✓ Laptop sepete eklendi");

                // Mouse ekle
                UrunEkle(sepet, "Mouse", 150.00m, 2, stokDurumu);
                Console.WriteLine("✓ Mouse sepete eklendi");

                // Stokta olmayan ürün eklemeye çalış
                UrunEkle(sepet, "Tablet", 5000.00m, 1, stokDurumu);
                Console.WriteLine("✓ Tablet sepete eklendi");

                // Sepet özeti
                Console.WriteLine("\n=== SEPET ÖZETİ ===");
                decimal toplamTutar = 0;
                foreach (var urun in sepet)
                {
                    Console.WriteLine($"{urun.Ad} - {urun.Adet} adet - {urun.Fiyat * urun.Adet:C}");
                    toplamTutar += urun.Fiyat * urun.Adet;
                }
                Console.WriteLine($"Toplam Tutar: {toplamTutar:C}");
            }
            catch (StokYetersizException ex)
            {
                Console.WriteLine($"✗ Stok hatası: {ex.Message}");
            }
            catch (UrunBulunamadiException ex)
            {
                Console.WriteLine($"✗ Ürün hatası: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ Genel hata: {ex.Message}");
            }


        }

        // 3. Hastane Randevu Sistemi Örneği - Exception Management
        static void HastaneRandevuSistemiOrnegi()
        {
            Console.WriteLine("Hastane randevu sistemi:");
            
            var randevular = new List<Randevu>();
            var doktorlar = new List<Doktor>
            {
                new Doktor { Id = 1, Ad = "Dr. Ahmet Yılmaz", Uzmanlik = "Kardiyoloji" },
                new Doktor { Id = 2, Ad = "Dr. Ayşe Demir", Uzmanlik = "Nöroloji" },
                new Doktor { Id = 3, Ad = "Dr. Mehmet Kaya", Uzmanlik = "Ortopedi" }
            };

            try
            {
                Console.WriteLine("→ Randevu işlemleri başlatılıyor...");

                // Randevu oluştur
                RandevuOlustur(randevular, doktorlar, 1, "Ali Veli", DateTime.Now.AddDays(1), "14:00");
                Console.WriteLine("✓ Randevu oluşturuldu");

                // Aynı saatte başka randevu oluşturmaya çalış
                RandevuOlustur(randevular, doktorlar, 1, "Fatma Demir", DateTime.Now.AddDays(1), "14:00");
                Console.WriteLine("✓ İkinci randevu oluşturuldu");

                // Geçmiş tarihte randevu oluşturmaya çalış
                RandevuOlustur(randevular, doktorlar, 2, "Can Özkan", DateTime.Now.AddDays(-1), "10:00");
                Console.WriteLine("✓ Geçmiş randevu oluşturuldu");

                // Randevu listesi
                Console.WriteLine("\n=== RANDEVU LİSTESİ ===");
                foreach (var randevu in randevular)
                {
                    Console.WriteLine($"{randevu.HastaAdi} - {randevu.DoktorAdi} - {randevu.Tarih:dd.MM.yyyy} {randevu.Saat}");
                }
            }
            catch (RandevuCakismasiException ex)
            {
                Console.WriteLine($"✗ Randevu çakışması: {ex.Message}");
            }
            catch (GecmisTarihException ex)
            {
                Console.WriteLine($"✗ Tarih hatası: {ex.Message}");
            }
            catch (DoktorBulunamadiException ex)
            {
                Console.WriteLine($"✗ Doktor hatası: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ Genel hata: {ex.Message}");
            }


        }

        // 4. Kütüphane Kitap Ödünç Alma Örneği - Transaction ve Try-Catch
        static void KutuphaneKitapOduncAlmaOrnegi()
        {
            Console.WriteLine("Kütüphane kitap ödünç alma sistemi:");
            
            var kitaplar = new List<Kitap>
            {
                new Kitap { Id = 1, Ad = "Suç ve Ceza", Yazar = "Dostoyevski", OduncDurumu = false },
                new Kitap { Id = 2, Ad = "1984", Yazar = "George Orwell", OduncDurumu = false },
                new Kitap { Id = 3, Ad = "Küçük Prens", Yazar = "Saint-Exupéry", OduncDurumu = true }
            };

            var uyeler = new List<Uye>
            {
                new Uye { Id = 1, Ad = "Ahmet Yılmaz", OduncKitapSayisi = 0, MaxKitap = 3 },
                new Uye { Id = 2, Ad = "Ayşe Demir", OduncKitapSayisi = 2, MaxKitap = 3 }
            };

            // Simüle edilmiş veritabanı bağlantısı
            using (var connection = new SimulatedDatabaseConnection())
            {
                try
                {
                    connection.Open();
                    Console.WriteLine("✓ Veritabanı bağlantısı açıldı");

                    // Transaction başlat
                    using (var transaction = connection.BeginTransaction())
                    {
                        Console.WriteLine("✓ Transaction başlatıldı");

                        try
                        {
                            Console.WriteLine("→ Kitap ödünç alma işlemi başlatılıyor...");

                            // Kitap ödünç al - İlk işlem
                            KitapOduncAl(kitaplar, uyeler, 1, 1); // Ahmet Yılmaz - Suç ve Ceza
                            Console.WriteLine("✓ Kitap ödünç alındı");

                            // Ödünç alma kaydı oluştur - İkinci işlem
                            Console.WriteLine("→ Ödünç alma kaydı oluşturuluyor...");
                            Console.WriteLine("✓ Ödünç alma kaydı oluşturuldu");

                            // Üye bilgilerini güncelle - Üçüncü işlem
                            Console.WriteLine("→ Üye bilgileri güncelleniyor...");
                            Console.WriteLine("✓ Üye bilgileri güncellendi");

                            // Transaction'ı commit et
                            transaction.Commit();
                            Console.WriteLine("✓ Transaction başarıyla commit edildi");
                            Console.WriteLine("✓ Kitap ödünç alma işlemi tamamlandı");

                            Console.WriteLine("\n=== ÖDÜNÇ ALINAN KİTAPLAR ===");
                            foreach (var kitap in kitaplar.Where(k => k.OduncDurumu))
                            {
                                Console.WriteLine($"{kitap.Ad} - {kitap.Yazar}");
                            }
                        }
                        catch (KitapZatenOduncException ex)
                        {
                            // Hata durumunda rollback
                            transaction.Rollback();
                            Console.WriteLine($"✗ Kitap hatası: {ex.Message}");
                            Console.WriteLine("✗ Transaction rollback edildi");
                        }
                        catch (UyeLimitAsimiException ex)
                        {
                            // Hata durumunda rollback
                            transaction.Rollback();
                            Console.WriteLine($"✗ Üye limit hatası: {ex.Message}");
                            Console.WriteLine("✗ Transaction rollback edildi");
                        }
                        catch (KitapBulunamadiException ex)
                        {
                            // Hata durumunda rollback
                            transaction.Rollback();
                            Console.WriteLine($"✗ Kitap bulunamadı: {ex.Message}");
                            Console.WriteLine("✗ Transaction rollback edildi");
                        }
                        catch (Exception ex)
                        {
                            // Hata durumunda rollback
                            transaction.Rollback();
                            Console.WriteLine($"✗ Genel hata: {ex.Message}");
                            Console.WriteLine("✗ Transaction rollback edildi");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"✗ Bağlantı hatası: {ex.Message}");
                }
            }


        }

        // 5. Online Sınav Sistemi Örneği - Kapsamlı Hata Yönetimi
        static void OnlineSinavSistemiOrnegi()
        {
            Console.WriteLine("Online sınav sistemi:");
            
            var ogrenciler = new List<Ogrenci>
            {
                new Ogrenci { Id = 1, Ad = "Ali Veli", Sinif = 10, Aktif = true },
                new Ogrenci { Id = 2, Ad = "Fatma Demir", Sinif = 11, Aktif = false },
                new Ogrenci { Id = 3, Ad = "Can Özkan", Sinif = 9, Aktif = true }
            };

            var sinavlar = new List<Sinav>
            {
                new Sinav { Id = 1, Ad = "Matematik Sınavı", Sinif = 10, Baslangic = DateTime.Now.AddHours(1), Bitis = DateTime.Now.AddHours(2) },
                new Sinav { Id = 2, Ad = "Fizik Sınavı", Sinif = 11, Baslangic = DateTime.Now.AddHours(-1), Bitis = DateTime.Now.AddHours(1) },
                new Sinav { Id = 3, Ad = "Kimya Sınavı", Sinif = 9, Baslangic = DateTime.Now.AddDays(1), Bitis = DateTime.Now.AddDays(1).AddHours(1) }
            };

            try
            {
                Console.WriteLine("→ Sınav giriş işlemleri başlatılıyor...");

                // Sınav girişi yap
                SinavaGir(ogrenciler, sinavlar, 1, 1); // Ali Veli - Matematik Sınavı
                Console.WriteLine("✓ Sınav girişi yapıldı");

                // Aktif olmayan öğrenci sınav girişi
                SinavaGir(ogrenciler, sinavlar, 2, 2); // Fatma Demir - Fizik Sınavı
                Console.WriteLine("✓ Aktif olmayan öğrenci sınav girişi yapıldı");

                // Yanlış sınıf sınavına giriş
                SinavaGir(ogrenciler, sinavlar, 1, 3); // Ali Veli - Kimya Sınavı (yanlış sınıf)
                Console.WriteLine("✓ Yanlış sınıf sınavına giriş yapıldı");

                // Henüz başlamamış sınava giriş
                SinavaGir(ogrenciler, sinavlar, 3, 3); // Can Özkan - Kimya Sınavı (henüz başlamadı)
                Console.WriteLine("✓ Henüz başlamamış sınava giriş yapıldı");

                Console.WriteLine("\n=== SINAV DURUMU ===");
                foreach (var sinav in sinavlar)
                {
                    string durum = DateTime.Now >= sinav.Baslangic && DateTime.Now <= sinav.Bitis ? "AKTİF" : "PASİF";
                    Console.WriteLine($"{sinav.Ad} - {durum}");
                }
            }
            catch (OgrenciAktifDegilException ex)
            {
                Console.WriteLine($"✗ Öğrenci hatası: {ex.Message}");
            }
            catch (SinifUyumsuzluguException ex)
            {
                Console.WriteLine($"✗ Sınıf hatası: {ex.Message}");
            }
            catch (SinavZamaniException ex)
            {
                Console.WriteLine($"✗ Zaman hatası: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ Genel hata: {ex.Message}");
            }


        }

        // Yardımcı metodlar
        static void UrunEkle(List<Urun> sepet, string ad, decimal fiyat, int adet, Dictionary<string, int> stokDurumu)
        {
            if (!stokDurumu.ContainsKey(ad))
            {
                throw new UrunBulunamadiException($"{ad} ürünü bulunamadı!");
            }

            if (stokDurumu[ad] < adet)
            {
                throw new StokYetersizException($"{ad} için yeterli stok yok! Mevcut: {stokDurumu[ad]}");
            }

            sepet.Add(new Urun { Ad = ad, Fiyat = fiyat, Adet = adet });
            stokDurumu[ad] -= adet;
        }

        static void RandevuOlustur(List<Randevu> randevular, List<Doktor> doktorlar, int doktorId, string hastaAdi, DateTime tarih, string saat)
        {
            // Geçmiş tarih kontrolü
            if (tarih < DateTime.Now.Date)
            {
                throw new GecmisTarihException("Geçmiş tarihte randevu oluşturulamaz!");
            }

            // Doktor kontrolü
            var doktor = doktorlar.FirstOrDefault(d => d.Id == doktorId);
            if (doktor == null)
            {
                throw new DoktorBulunamadiException("Doktor bulunamadı!");
            }

            // Randevu çakışması kontrolü
            var cakisanRandevu = randevular.FirstOrDefault(r => r.DoktorId == doktorId && r.Tarih.Date == tarih.Date && r.Saat == saat);
            if (cakisanRandevu != null)
            {
                throw new RandevuCakismasiException("Bu saatte başka bir randevu bulunmaktadır!");
            }

            randevular.Add(new Randevu
            {
                DoktorId = doktorId,
                DoktorAdi = doktor.Ad,
                HastaAdi = hastaAdi,
                Tarih = tarih,
                Saat = saat
            });
        }

        static void KitapOduncAl(List<Kitap> kitaplar, List<Uye> uyeler, int uyeId, int kitapId)
        {
            // Kitap kontrolü
            var kitap = kitaplar.FirstOrDefault(k => k.Id == kitapId);
            if (kitap == null)
            {
                throw new KitapBulunamadiException("Kitap bulunamadı!");
            }

            // Kitap ödünç durumu kontrolü
            if (kitap.OduncDurumu)
            {
                throw new KitapZatenOduncException("Bu kitap zaten ödünç alınmış!");
            }

            // Üye kontrolü
            var uye = uyeler.FirstOrDefault(u => u.Id == uyeId);
            if (uye == null)
            {
                throw new Exception("Üye bulunamadı!");
            }

            // Üye limit kontrolü
            if (uye.OduncKitapSayisi >= uye.MaxKitap)
            {
                throw new UyeLimitAsimiException("Maksimum kitap sayısına ulaşıldı!");
            }

            // Kitap ödünç al
            kitap.OduncDurumu = true;
            uye.OduncKitapSayisi++;
        }

        static void SinavaGir(List<Ogrenci> ogrenciler, List<Sinav> sinavlar, int ogrenciId, int sinavId)
        {
            // Öğrenci kontrolü
            var ogrenci = ogrenciler.FirstOrDefault(o => o.Id == ogrenciId);
            if (ogrenci == null)
            {
                throw new Exception("Öğrenci bulunamadı!");
            }

            // Öğrenci aktiflik kontrolü
            if (!ogrenci.Aktif)
            {
                throw new OgrenciAktifDegilException("Bu öğrenci aktif değil!");
            }

            // Sınav kontrolü
            var sinav = sinavlar.FirstOrDefault(s => s.Id == sinavId);
            if (sinav == null)
            {
                throw new Exception("Sınav bulunamadı!");
            }

            // Sınıf uyumluluğu kontrolü
            if (ogrenci.Sinif != sinav.Sinif)
            {
                throw new SinifUyumsuzluguException("Öğrenci sınıfı ile sınav sınıfı uyumsuz!");
            }

            // Sınav zamanı kontrolü
            if (DateTime.Now < sinav.Baslangic)
            {
                throw new SinavZamaniException("Sınav henüz başlamadı!");
            }

            if (DateTime.Now > sinav.Bitis)
            {
                throw new SinavZamaniException("Sınav süresi dolmuş!");
            }
        }
    }

    // Model sınıfları
    public class Urun
    {
        public string Ad { get; set; } = "";
        public decimal Fiyat { get; set; }
        public int Adet { get; set; }
    }

    public class Doktor
    {
        public int Id { get; set; }
        public string Ad { get; set; } = "";
        public string Uzmanlik { get; set; } = "";
    }

    public class Randevu
    {
        public int DoktorId { get; set; }
        public string DoktorAdi { get; set; } = "";
        public string HastaAdi { get; set; } = "";
        public DateTime Tarih { get; set; }
        public string Saat { get; set; } = "";
    }

    public class Kitap
    {
        public int Id { get; set; }
        public string Ad { get; set; } = "";
        public string Yazar { get; set; } = "";
        public bool OduncDurumu { get; set; }
    }

    public class Uye
    {
        public int Id { get; set; }
        public string Ad { get; set; } = "";
        public int OduncKitapSayisi { get; set; }
        public int MaxKitap { get; set; }
    }

    public class Ogrenci
    {
        public int Id { get; set; }
        public string Ad { get; set; } = "";
        public int Sinif { get; set; }
        public bool Aktif { get; set; }
    }

    public class Sinav
    {
        public int Id { get; set; }
        public string Ad { get; set; } = "";
        public int Sinif { get; set; }
        public DateTime Baslangic { get; set; }
        public DateTime Bitis { get; set; }
    }

    // Custom Exception sınıfları
    public class StokYetersizException : Exception
    {
        public StokYetersizException(string message) : base(message) { }
    }

    public class UrunBulunamadiException : Exception
    {
        public UrunBulunamadiException(string message) : base(message) { }
    }

    public class RandevuCakismasiException : Exception
    {
        public RandevuCakismasiException(string message) : base(message) { }
    }

    public class GecmisTarihException : Exception
    {
        public GecmisTarihException(string message) : base(message) { }
    }

    public class DoktorBulunamadiException : Exception
    {
        public DoktorBulunamadiException(string message) : base(message) { }
    }

    public class KitapZatenOduncException : Exception
    {
        public KitapZatenOduncException(string message) : base(message) { }
    }

    public class UyeLimitAsimiException : Exception
    {
        public UyeLimitAsimiException(string message) : base(message) { }
    }

    public class KitapBulunamadiException : Exception
    {
        public KitapBulunamadiException(string message) : base(message) { }
    }

    public class OgrenciAktifDegilException : Exception
    {
        public OgrenciAktifDegilException(string message) : base(message) { }
    }

    public class SinifUyumsuzluguException : Exception
    {
        public SinifUyumsuzluguException(string message) : base(message) { }
    }

    public class SinavZamaniException : Exception
    {
        public SinavZamaniException(string message) : base(message) { }
    }

    // Simüle edilmiş veritabanı bağlantısı ve transaction sınıfları
    public class SimulatedDatabaseConnection : IDisposable
    {
        public void Open()
        {
            // Simüle edilmiş bağlantı açma
        }

        public SimulatedTransaction BeginTransaction()
        {
            return new SimulatedTransaction();
        }

        public void Dispose()
        {
            // Simüle edilmiş bağlantı kapatma
        }
    }

    public class SimulatedTransaction : IDisposable
    {
        public void Commit()
        {
            // Simüle edilmiş commit işlemi
        }

        public void Rollback()
        {
            // Simüle edilmiş rollback işlemi
        }

        public void Dispose()
        {
            // Simüle edilmiş transaction kapatma
        }
    }
} 