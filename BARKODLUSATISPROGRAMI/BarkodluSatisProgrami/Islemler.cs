using System;
using System.Data.Entity;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BarkodluSatisProgrami
{
    static class Islemler
    {
        public static Double DoubleYap(string deger)
        {
            Double sonuc;
            double.TryParse(deger, NumberStyles.Currency, CultureInfo.CurrentCulture.NumberFormat, out sonuc);
            return Math.Round( sonuc,2);
        }
        public static void StokAzalt(string barkod, double miktar)
        {
            if (barkod != "1111111111116")
            {
                using (var db = new Entities())
                {
                    var urunbilgi = db.Urun.SingleOrDefault(x => x.Barkod == barkod);
                    urunbilgi.Miktar -= miktar;
                    db.SaveChanges();
                }
            }
            
        }
        public static void StokArttir(string barkod, double miktar)
        {
            if (barkod != "1111111111116")
            {
                using (var db = new Entities())
                {
                    var urunbilgi = db.Urun.SingleOrDefault(x => x.Barkod == barkod);
                    urunbilgi.Miktar += miktar;
                    db.SaveChanges();
                }
            }
               
        }
        public static void GridDuzenle(DataGridView dgv)
        {
            if (dgv.Columns.Count>0)
            {
                for (int i = 0; i < dgv.Columns.Count; i++)
                {
                    switch(dgv.Columns[i].HeaderText)
                    {
                        case "Id":
                            dgv.Columns[i].HeaderText = "Numara";
                            break;
                        case "IslemNo":
                            dgv.Columns[i].HeaderText = "İşlem No";
                            break;
                        case "UruunId":
                            dgv.Columns[i].HeaderText = "Ürün Numarası";
                            break;
                        case "UrunAd":
                            dgv.Columns[i].HeaderText = "Ürün Adı";
                            break;
                        case "Aciklama":
                            dgv.Columns[i].HeaderText = "Açıklama";
                            break;
                        case "UrunGrup":
                            dgv.Columns[i].HeaderText = "Ürün Grubu";
                            break;
                        case "AlisFiyat":
                            dgv.Columns[i].HeaderText = "Alış Fiyatı";
                            dgv.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                            dgv.Columns[i].DefaultCellStyle.Format = "C2";
                            break;
                        case "AlisFiyatToplam":
                            dgv.Columns[i].HeaderText = "Alış Fiyat Toplam";
                            dgv.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                            dgv.Columns[i].DefaultCellStyle.Format = "C2";
                            break;
                        case "SatisFiyat":
                            dgv.Columns[i].HeaderText = "Satış Fiyatı";
                            dgv.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                            dgv.Columns[i].DefaultCellStyle.Format = "C2";
                            break;
                        case "KdvOrani":
                            dgv.Columns[i].HeaderText = "KDV Oranı";
                            dgv.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            break;
                        case "Birim":
                            dgv.Columns[i].HeaderText = "Birim";
                            dgv.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            break;
                        case "Miktar":
                            dgv.Columns[i].HeaderText = "Miktar";
                            dgv.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            break;
                        case "OdemeSeki":
                            dgv.Columns[i].HeaderText = "Ödeme Şekli";
                            dgv.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            break;
                        case "Kart":
                            dgv.Columns[i].HeaderText = "Kart";
                            dgv.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                            dgv.Columns[i].DefaultCellStyle.Format = "C2";
                            break;
                        case "Nakit":
                            dgv.Columns[i].HeaderText = "Nakit";
                            dgv.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                            dgv.Columns[i].DefaultCellStyle.Format = "C2";
                            break;
                        case "Gelir":
                            dgv.Columns[i].HeaderText = "Gelir";
                            dgv.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                            dgv.Columns[i].DefaultCellStyle.Format = "C2";
                            break;
                        case "Gider":
                            dgv.Columns[i].HeaderText = "Gider";
                            dgv.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                            dgv.Columns[i].DefaultCellStyle.Format = "C2";
                            break;
                        case "Kullanici":
                            dgv.Columns[i].HeaderText = "Kullanıcı";
                            dgv.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            break;
                        case "KdvTutari":
                            dgv.Columns[i].HeaderText = "KDV Tutarı";
                            dgv.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                            dgv.Columns[i].DefaultCellStyle.Format = "C2";
                            break;
                        case "Toplam":
                            dgv.Columns[i].HeaderText = "Toplam";
                            dgv.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                            dgv.Columns[i].DefaultCellStyle.Format = "C2";
                            break;
                        case "Tarih":
                            dgv.Columns[i].HeaderText = "Tarih";
                            dgv.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            break;


                    }
                }
            }
        }
        public static void StokHareket(string barkod,string urunad, string birim, double miktar, string urungrup, string kullanici)
        {
            using (var db=new Entities())
            {
                StokHareket sh = new StokHareket();
                sh.Barkod = barkod;
                sh.UrunAd = urunad;
                sh.Birim = birim;
                sh.Miktar = miktar;
                sh.UrunGrup = urungrup;
                sh.Kullanici = kullanici;
                sh.Tarih = DateTime.Now;
                db.StokHareket.Add(sh);
                db.SaveChanges();
            }
        }

        public static Double KartKomisyon()
        {
            double sonuc = 0;
            using (var db = new Entities())
            {
                if (db.Sabit.Any())
                {
                    sonuc = Convert.ToDouble( db.Sabit.First().KartKomisyon);
                }
                else
                {
                   sonuc = 0;
                }
            }
            return sonuc;
        }

        public static void SabitVarsayilan()
        {
            using (var db = new Entities())
            {
                if (!db.Sabit.Any())
                {
                    Sabit s = new Sabit();
                    s.KartKomisyon = 0;
                    s.Yazici = false;
                    s.AdSoyad = "admin";
                    s.Unvan = "admin";
                    s.Adres = "admin";
                    s.Telefon = "admin";
                    s.Eposta = "admin";
                    db.Sabit.Add(s);
                    db.SaveChanges();
                }
                
            }
        }
        
        public static void Backup()
        {
            //SaveFileDialog save = new SaveFileDialog();
            //save.Filter = "Veri Yedek Dosyası|0.bak";
            //save.FileName = "BarkodluSatisProgrami_" + DateTime.Now.ToShortDateString();
            //if (save.ShowDialog() == DialogResult.OK)
            //{
            //    try
            //    {
            //        Cursor.Current = Cursors.WaitCursor;
            //        if (File.Exists(save.FileName))
            //        {
            //            File.Delete(save.FileName);
            //        }
            //        var dbHedef = save.FileName;
            //        string dbKaynak = Application.StartupPath + @"\BarkodDb.mdf";
            //        using (var db = new Entities())
            //        {
            //            var cmd = @"BACKUP DATABASE[" + dbKaynak + "] TO DISK='" + dbHedef + "'";
            //            db.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, cmd);
            //        }
            //        Cursor.Current = Cursors.Default;
            //        MessageBox.Show("Yedekleme işlemi başarılı");
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.ToString());
            //    }
            //}
            //SaveFileDialog save = new SaveFileDialog();
            //save.Filter = "Veri Yedek Dosyası|*.bak";
            //save.FileName = "BarkodluSatisProgrami_" + DateTime.Now.ToString("yyyyMMdd_HHmmss");

            //if (save.ShowDialog() == DialogResult.OK)
            //{
            //    try
            //    {
            //        Cursor.Current = Cursors.WaitCursor;

            //        string dbHedef = save.FileName;
            //        string connectionString = ConfigurationManager.ConnectionStrings["BarkodDbConnectionString"].ConnectionString;
            //        string dbKaynak = "BARKODDB";

            //        // Bağlantı dizesini konsola yazdır (hata ayıklama için)
            //        Console.WriteLine("Bağlantı Dizesi: " + connectionString);

            //        using (SqlConnection connection = new SqlConnection(connectionString))
            //        {
            //            connection.Open();
            //            using (SqlCommand command = new SqlCommand())
            //            {
            //                command.Connection = connection;
            //                command.CommandText = $@"BACKUP DATABASE [{dbKaynak}] TO DISK = '{dbHedef}'";
            //                command.ExecuteNonQuery();
            //            }
            //        }

            //        Cursor.Current = Cursors.Default;
            //        MessageBox.Show("Yedekleme işlemi başarılı");
            //    }
            //    catch (SqlException ex)
            //    {
            //        MessageBox.Show("Veritabanı Hatası: " + ex.Message + "\n\n" + (ex.InnerException != null ? "İç Hata: " + ex.InnerException.Message : ""));
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show("Hata: " + ex.Message + "\n\n" + (ex.InnerException != null ? "İç Hata: " + ex.InnerException.Message : ""));
            //    }
            //}
           
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "Veri Yedek Dosyası|*.bak";
                save.FileName = "BarkodluSatisProgrami_" + DateTime.Now.ToString("yyyyMMdd_HHmmss");

                if (save.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        Cursor.Current = Cursors.WaitCursor;

                        string hedefDosyaYolu = save.FileName;
                        string connectionString = ConfigurationManager.ConnectionStrings["BarkodDbConnectionString"].ConnectionString;
                        string dbKaynak = "BARKODDB";
                        string geciciYedeklemeYolu = Path.Combine(Path.GetTempPath(), "gecici_yedek.bak"); // Geçici dosya yolu


                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();
                            using (SqlCommand command = new SqlCommand())
                            {
                                command.Connection = connection;
                                command.CommandText = $@"BACKUP DATABASE [{dbKaynak}] TO DISK = '{geciciYedeklemeYolu}'"; // Geçici konuma yedekle
                                command.ExecuteNonQuery();
                            }
                        }

                        // Geçici yedeği hedef konuma kopyala
                        File.Copy(geciciYedeklemeYolu, hedefDosyaYolu, true);
                        File.Delete(geciciYedeklemeYolu); // Geçici dosyayı sil


                        Cursor.Current = Cursors.Default;
                        MessageBox.Show("Yedekleme işlemi başarılı");
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Veritabanı Hatası: " + ex.Message + "\n\n" + (ex.InnerException != null ? "İç Hata: " + ex.InnerException.Message : ""));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Hata: " + ex.Message + "\n\n" + (ex.InnerException != null ? "İç Hata: " + ex.InnerException.Message : ""));
                    }
                }
            
        }
    }
}
