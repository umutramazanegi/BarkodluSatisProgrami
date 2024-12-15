using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BarkodluSatisProgrami
{
    public partial class fSatis : Form
    {
        public fSatis()
        {
            InitializeComponent();
        }

        Entities db = new Entities();
        /* private void tBarkod_KeyDown(object sender, KeyEventArgs e)
         {
             if (e.KeyCode == Keys.Enter)
             {
                 string barkod = tBarkod.Text.Trim();
                 if (barkod.Length <= 2)
                 {
                     tMiktar.Text = barkod;
                     tBarkod.Clear();
                     tBarkod.Focus();


                 }
                 else
                 {
                     if (db.Urun.Any(x => x.Barkod == barkod))
                     {
                         var urun = db.Urun.Where(x => x.Barkod == barkod).FirstOrDefault();


                         UrunGetirListeye(urun, barkod, Convert.ToDouble(tMiktar.Text));
                         //burayı dene gpt yazdı
                       //  tMiktar.Text = 1.ToString();//deneme
                     }
                     else
                     {
                         int onek = Convert.ToInt32(barkod.Substring(0, 2));
                         if (db.Terazi.Any(x => x.TeraziOnEk == onek))
                         {
                             string teraziurunno = barkod.Substring(2, 5);
                             if (db.Urun.Any(x => x.Barkod == teraziurunno))
                             {
                                 var urunterazi = db.Urun.Where(x => x.Barkod == teraziurunno).FirstOrDefault();
                                 double miktarkg = Convert.ToDouble(barkod.Substring(7, 5)) / 1000;
                                 UrunGetirListeye(urunterazi, teraziurunno, miktarkg);
                             }
                             else
                             {
                                 Console.Beep(1000, 1000);
                                 MessageBox.Show("Kg Ürün Ekleme Sayfası");
                             }


                         }
                         else
                         {
                             Console.Beep(1000, 1000);
                             fUrunGiris f = new fUrunGiris();
                             f.tBarkod.Text = barkod;
                             f.ShowDialog();
                         }
                     }


                 }
                 gridSatisListesi.ClearSelection();
                 GenelToplam();
             }
         }  */
        private void tBarkod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string barkod = tBarkod.Text.Trim();

                // Barkod 2 haneli veya daha küçükse
                if (barkod.Length <= 2)
                {
                    tMiktar.Text = barkod; // Barkodun kendisini miktar olarak ayarla
                    tBarkod.Clear(); // Barkodu temizle
                    tBarkod.Focus(); // Barkod alanına odaklan
                }
                else
                {
                    if (db.Urun.Any(x => x.Barkod == barkod))
                    {
                        var urun = db.Urun.FirstOrDefault(x => x.Barkod == barkod);
                        double miktar = Convert.ToDouble(tMiktar.Text); // Miktarı geçici olarak al
                        UrunGetirListeye(urun, barkod, miktar);

                        // `GenelToplam` burada çağrılacak
                        gridSatisListesi.ClearSelection();
                        GenelToplam();

                        tBarkod.Clear();
                        tBarkod.Focus();
                    }
                    else
                    {
                        int onek = Convert.ToInt32(barkod.Substring(0, 2));
                        if (db.Terazi.Any(x => x.TeraziOnEk == onek))
                        {
                            string teraziurunno = barkod.Substring(2, 5);
                            if (db.Urun.Any(x => x.Barkod == teraziurunno))
                            {
                                var urunterazi = db.Urun.FirstOrDefault(x => x.Barkod == teraziurunno);
                                double miktarkg = Convert.ToDouble(barkod.Substring(7, 5)) / 1000;
                                UrunGetirListeye(urunterazi, teraziurunno, miktarkg);

                                // `GenelToplam` burada çağrılacak
                                gridSatisListesi.ClearSelection();
                                GenelToplam();

                                tBarkod.Clear();
                                tBarkod.Focus();
                            }
                            else
                            {
                                Console.Beep(1000, 1000);
                                MessageBox.Show("Kg Ürün Ekleme Sayfası");
                            }
                        }
                        else
                        {
                            Console.Beep(1000, 1000);
                            fUrunGiris f = new fUrunGiris();
                            f.tBarkod.Text = barkod;
                            f.ShowDialog();
                        }
                    }
                }
                gridSatisListesi.ClearSelection();
            }

        }


        private void UrunGetirListeye(Urun urun, string barkod, double miktar)
        {
            int satirsayisi = gridSatisListesi.Rows.Count;
             //double miktar = Convert.ToDouble(tMiktar.Text);
            bool eklenmismi = false;
            if (satirsayisi > 0)
            {
                for (int i = 0; i < satirsayisi; i++)
                {
                    if (gridSatisListesi.Rows[i].Cells["Barkod"].Value.ToString() == barkod)
                    {
                        gridSatisListesi.Rows[i].Cells["Miktar"].Value = miktar + Convert.ToDouble(gridSatisListesi.Rows[i].Cells["Miktar"].Value);
                        gridSatisListesi.Rows[i].Cells["Toplam"].Value = Math.Round(Convert.ToDouble(gridSatisListesi.Rows[i].Cells["Miktar"].Value) * Convert.ToDouble(gridSatisListesi.Rows[i].Cells["Fiyat"].Value), 2);
                        double dblKdvTutari = (double)urun.KdvTutari;
                        gridSatisListesi.Rows[i].Cells["KdvTutari"].Value = Convert.ToDouble(gridSatisListesi.Rows[i].Cells["Miktar"].Value) * dblKdvTutari;
                        eklenmismi = true;

                    }
                }
            }

            if (eklenmismi == false)
            {
                gridSatisListesi.Rows.Add();
                gridSatisListesi.Rows[satirsayisi].Cells["Barkod"].Value = urun.Barkod;
                gridSatisListesi.Rows[satirsayisi].Cells["UrunAdi"].Value = urun.UrunAd;
                gridSatisListesi.Rows[satirsayisi].Cells["UrunGrup"].Value = urun.UrunGrup;
                gridSatisListesi.Rows[satirsayisi].Cells["Birim"].Value = urun.Birim;
                gridSatisListesi.Rows[satirsayisi].Cells["Fiyat"].Value = urun.SatisFiyat;
                gridSatisListesi.Rows[satirsayisi].Cells["Miktar"].Value = miktar;
                gridSatisListesi.Rows[satirsayisi].Cells["Toplam"].Value = Math.Round(miktar * (double)urun.SatisFiyat, 2);
                gridSatisListesi.Rows[satirsayisi].Cells["AlisFiyat"].Value = urun.AlisFiyat;
                gridSatisListesi.Rows[satirsayisi].Cells["KdvTutari"].Value = urun.KdvTutari;
            }
        }

        private void GenelToplam()
        {

            double toplam = 0;
            for (int i = 0; i < gridSatisListesi.Rows.Count; i++)
            {
                toplam += Convert.ToDouble(gridSatisListesi.Rows[i].Cells["Toplam"].Value);
            }
            //                                   C2 para birimidir
            tGenelToplam.Text = toplam.ToString("C2");
            tMiktar.Text = 1.ToString();
            tBarkod.Clear();
            tBarkod.Focus();

        }

        private void gridSatisListesi_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 9)
            {
                gridSatisListesi.Rows.Remove(gridSatisListesi.CurrentRow);
                gridSatisListesi.ClearSelection();
                GenelToplam();
                tBarkod.Focus();
            }
        }

        private void fSatis_Load(object sender, EventArgs e)
        {
            HizliButonDoldur();
            //b5.Text = 5.ToString("C2");
            //b10.Text = 10.ToString("C2");
            //b20.Text = 20.ToString("C2");
            //b50.Text = 50.ToString("C2");
            //b100.Text = 100.ToString("C2");
            //b200.Text = 200.ToString("C2");
            using (var db = new Entities())
            {
                var sabit = db.Sabit.FirstOrDefault();
                chYazdirmaDurumu.Checked = Convert.ToBoolean(sabit.Yazici);
            }
        }
        private void HizliButonDoldur()
        {
            var hizliurun = db.HizliUrun.ToList();
            foreach (var item in hizliurun)
            {
                Button bH = this.Controls.Find("bH" + item.Id, true).FirstOrDefault() as Button;
                if (bH != null)
                {
                    double fiyat = Islemler.DoubleYap(item.Fiyat.ToString());
                    bH.Text = item.UrunAd + "\n" + fiyat.ToString("C2");

                }

            }
        }

        private void HizliButonClick(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            int butonid = Convert.ToInt16(b.Name.ToString().Substring(2, b.Name.Length - 2));
            if (b.Text.ToString().StartsWith("-"))
            {
                fHizliButonUrunEkle f = new fHizliButonUrunEkle();
                f.lButonId.Text = butonid.ToString();
                f.ShowDialog();
            }
            else
            {

                var urunbarkod = db.HizliUrun.Where(x => x.Id == butonid).Select(x => x.Barkod).FirstOrDefault();
                var urun = db.Urun.Where(x => x.Barkod == urunbarkod).FirstOrDefault();
                UrunGetirListeye(urun, urunbarkod, Convert.ToDouble(tMiktar.Text));
                GenelToplam();
            }
        }
        private void bh_MouseDown(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Right)
            {
                Button b = (Button)sender;
                if (!b.Text.StartsWith("-"))
                {
                    int butonid = Convert.ToInt16(b.Name.ToString().Substring(2, b.Name.Length - 2));
                    ContextMenuStrip s = new ContextMenuStrip();
                    ToolStripMenuItem sil = new ToolStripMenuItem();
                    sil.Text = "Temizle - Buton No:" + butonid.ToString();
                    sil.Click += Sil_Click;
                    s.Items.Add(sil);
                    this.ContextMenuStrip = s;
                }
            }
        }

        private void Sil_Click(object sender, EventArgs e)
        {
            int butonid = Convert.ToInt16(sender.ToString().Substring(19, sender.ToString().Length - 19));
            var guncelle = db.HizliUrun.Find(butonid);
            guncelle.Barkod = "-";
            guncelle.UrunAd = "-";
            guncelle.Fiyat = 0;
            db.SaveChanges();
            Button b = this.Controls.Find("bH" + butonid, true).FirstOrDefault() as Button;
            b.Text = "-" + "\n" + 0.ToString("C2");
        }

        private void bNx_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if (b.Text == ",")
            {
                int virgul = tNumarator.Text.Count(x => x == ',');
                if (virgul < 1)
                {
                    tNumarator.Text += b.Text;
                }
            }
            else if (b.Text == "<")
            {
                if (tNumarator.Text.Length > 0)
                {
                    tNumarator.Text = tNumarator.Text.Substring(0, tNumarator.Text.Length - 1);
                }
            }
            else
            {
                tNumarator.Text += b.Text;
            }
        }

        private void bAdet_Click(object sender, EventArgs e)
        {
            if (tNumarator.Text!="")
            {
                tMiktar.Text = tNumarator.Text;
                tNumarator.Clear();
                tBarkod.Clear();
                tBarkod.Focus();
            }
        }

        private void bOdenen_Click(object sender, EventArgs e)
        {
            if(tNumarator.Text!="")
            {
                double odenen =Islemler.DoubleYap(tNumarator.Text) - Islemler.DoubleYap(tGenelToplam.Text);
                tParaUstu.Text = odenen.ToString("C2");
                tOdenen.Text = Islemler.DoubleYap(tNumarator.Text).ToString("C2");
                tNumarator.Clear();
                GenelToplam();
                tBarkod.Focus();
                
            }
        }

        private void bBarkod_Click(object sender, EventArgs e)
        {
            if(tNumarator.Text!="")
            {
                
                
                if (db.Urun.Any(x => x.Barkod == tNumarator.Text))
                {
                    var urun = db.Urun.Where(x => x.Barkod == tNumarator.Text).FirstOrDefault();
                    UrunGetirListeye(urun, tNumarator.Text, Convert.ToDouble(tMiktar.Text));
                }
                else
                {
                    MessageBox.Show("Ürün Ekleme Sayfasını Aç");
                }
                tNumarator.Clear();
                GenelToplam();
                tBarkod.Focus();
            }
        }
        private void ParaUstuHesapla_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            double odenen = Islemler.DoubleYap(b.Text) - Islemler.DoubleYap(tGenelToplam.Text);
            tOdenen.Text = Islemler.DoubleYap(b.Text).ToString("C2");
            tParaUstu.Text = odenen.ToString("C2");

        }

        private void bDigerUrun_Click(object sender, EventArgs e)
        {
            if (tNumarator.Text != "")
            {
                int satirsayisi = gridSatisListesi.Rows.Count;
                gridSatisListesi.Rows.Add();
                gridSatisListesi.Rows[satirsayisi].Cells["Barkod"].Value= "1111111111116";
                gridSatisListesi.Rows[satirsayisi].Cells["UrunAdi"].Value = "Diğer Ürün";
                gridSatisListesi.Rows[satirsayisi].Cells["UrunGrup"].Value = "Diğer Ürün";
                gridSatisListesi.Rows[satirsayisi].Cells["Birim"].Value= "Adet";
                gridSatisListesi.Rows[satirsayisi].Cells["Miktar"].Value = 1;
                gridSatisListesi.Rows[satirsayisi].Cells["AlisFiyat"].Value = 0;  // bu satırı sonradan ekledim
                gridSatisListesi.Rows[satirsayisi].Cells["Fiyat"].Value = Islemler.DoubleYap(tNumarator.Text);
                gridSatisListesi.Rows[satirsayisi].Cells["KdvTutari"].Value = 0;
                gridSatisListesi.Rows[satirsayisi].Cells["Toplam"].Value = Islemler.DoubleYap(tNumarator.Text);
                tNumarator.Text = "";
                GenelToplam();
                tBarkod.Focus();

            }
        }

        private void bIade_Click(object sender, EventArgs e)
        {
            if (chSatisIadeIslemi.Checked)
            {
                chSatisIadeIslemi.Checked = false;
                chSatisIadeIslemi.Text = "Satış Yapılıyor";
            }
            else
            {
                chSatisIadeIslemi.Checked = true;
                chSatisIadeIslemi.Text = "İade Yapılıyor..!";
            }
        }

        private void bTemizle_Click(object sender, EventArgs e)
        {
            Temizle();
        }
        private void Temizle()
        {
            tMiktar.Text = 1.ToString();
            tBarkod.Clear();
            tOdenen.Clear();
            tParaUstu.Clear();
            tGenelToplam.Text = 0.ToString("C2");
            chSatisIadeIslemi.Checked = false;
            tNumarator.Clear();
            gridSatisListesi.Rows.Clear();
            tBarkod.Focus();
        }
        public void SatisYap(string odemesekli)
        {
            int satirsayisi = gridSatisListesi.Rows.Count;
            bool satisiade = chSatisIadeIslemi.Checked;
            double alisfiyattoplam = 0;
            if (satirsayisi>0)
            {
                int? islemno = db.Islem.First().IslemNo;
                Satis satis = new Satis();
                for (int i=0 ; i<satirsayisi ; i++)
                {
                    satis.IslemNo = islemno;
                    satis.UrunAd = gridSatisListesi.Rows[i].Cells["UrunAdi"].Value.ToString();
                    satis.UrunGrup = gridSatisListesi.Rows[i].Cells["UrunGrup"].Value.ToString();
                    satis.Barkod = gridSatisListesi.Rows[i].Cells["Barkod"].Value.ToString();
                    satis.Birim = gridSatisListesi.Rows[i].Cells["Birim"].Value.ToString();
                    satis.AlisFiyat = Islemler.DoubleYap(gridSatisListesi.Rows[i].Cells["AlisFiyat"].Value.ToString());
                    satis.SatisFiyat = Islemler.DoubleYap(gridSatisListesi.Rows[i].Cells["Fiyat"].Value.ToString());
                    satis.Miktar = Islemler.DoubleYap(gridSatisListesi.Rows[i].Cells["Miktar"].Value.ToString());
                    satis.Toplam = Islemler.DoubleYap(gridSatisListesi.Rows[i].Cells["Toplam"].Value.ToString());
                    satis.KdvTutari= Islemler.DoubleYap(gridSatisListesi.Rows[i].Cells["KdvTutari"].Value.ToString()) * Islemler.DoubleYap(gridSatisListesi.Rows[i].Cells["Miktar"].Value.ToString());
                    satis.OdemeSekli = odemesekli;
                    satis.Iade = satisiade;
                    satis.Tarih = DateTime.Now;
                    satis.Kullanici=lKullanici.Text;
                    db.Satis.Add(satis);
                    db.SaveChanges();


                    if (!satisiade)
                    {
                        Islemler.StokAzalt(gridSatisListesi.Rows[i].Cells["Barkod"].Value.ToString(), Islemler.DoubleYap(gridSatisListesi.Rows[i].Cells["Miktar"].Value.ToString()));

                    }
                    else
                    {
                        Islemler.StokArttir(gridSatisListesi.Rows[i].Cells["Barkod"].Value.ToString(), Islemler.DoubleYap(gridSatisListesi.Rows[i].Cells["Miktar"].Value.ToString()));

                    }
                    alisfiyattoplam += Islemler.DoubleYap(gridSatisListesi.Rows[i].Cells["AlisFiyat"].Value.ToString()) * Islemler.DoubleYap(gridSatisListesi.Rows[i].Cells["Miktar"].Value.ToString());


                }

                IslemOzet io = new IslemOzet();
                io.IslemNo = islemno;
                io.Iade = satisiade;
                io.AlisFiyatToplam = alisfiyattoplam;
                io.Gelir = false;
                io.Gider = false;
                if (!satisiade)
                {
                    io.Aciklama = odemesekli + " Satış İşlemi";
                }
                else
                {
                    io.Aciklama = " İade İşlemi (" + odemesekli + ")";
                }
                io.OdemeSekli = odemesekli;
                io.Kullanici = lKullanici.Text;
                io.Tarih = DateTime.Now;
                switch(odemesekli)
                {
                    case "Nakit":
                        io.Nakit = Islemler.DoubleYap(tGenelToplam.Text);
                        io.Kart = 0;
                        break;
                    case "Kart":
                        io.Nakit = 0;
                        io.Kart = Islemler.DoubleYap(tGenelToplam.Text);
                        break;
                    case "Kart-Nakit":
                        io.Nakit = Islemler.DoubleYap(lNakit.Text);
                        io.Kart = Islemler.DoubleYap(lKart.Text);
                        break;
                }
                db.IslemOzet.Add(io);
                db.SaveChanges();

                var islemnoarttir = db.Islem.First();
                islemnoarttir.IslemNo ++ ;
                db.SaveChanges();
                if (chYazdirmaDurumu.Checked)
                {
                    //Yazdır.
                    Yazdir yazdir = new Yazdir(islemno);
                    yazdir.YazdirmayaBasla();
                }
                Temizle();
            }
        }

        private void bNakit_Click(object sender, EventArgs e)
        {
             SatisYap("Nakit");
        }

        private void bKart_Click(object sender, EventArgs e)
        {
            SatisYap("Kart");
        }

        private void bKismi_Click(object sender, EventArgs e)
        {
            fNakitKart f = new fNakitKart();
            f.ShowDialog();
        }
        

        //Klavye tuşlarına basıldığında sadece sayı ve backspace tuşlarının çalışmasını sağlar
        private void tBarkod_KeyPress(object sender, KeyPressEventArgs e)
        {
            //eğer girilen karakter sayı değilse ve backspace değilse
            if (char.IsDigit(e.KeyChar) == false && e.KeyChar != (char)08)
            {
                e.Handled = true;
            }
        }

        private void tMiktar_KeyPress(object sender, KeyPressEventArgs e)
        {
            //eğer girilen karakter sayı değilse ,backspace değilse ve virgül değilse
            if (char.IsDigit(e.KeyChar) == false && e.KeyChar != (char)08 && e.KeyChar != ',')
            {
                e.Handled = true;
            }
        }
        //bunu f1 f2 vs tuşlarına basıldığında hızlı butonlara basılmış gibi çalıştırmak için yazdık
        private void fSatis_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.F1)
            {
                SatisYap("Nakit");
            }
            if (e.KeyCode == Keys.F2)
            {
                SatisYap("Kart");
            }
            if (e.KeyCode == Keys.F3)
            {
                fNakitKart f = new fNakitKart();
                f.ShowDialog();
            }
            
        }

        private void bIslemBeklet_Click(object sender, EventArgs e)
        {
            if (bIslemBeklet.Text=="İşlem Beklet")
            {
                Bekle();
                bIslemBeklet.BackColor = System.Drawing.Color.OrangeRed;
                bIslemBeklet.Text = "İşlem Bekiyor";
                gridSatisListesi.Rows.Clear();
                tGenelToplam.Text = 0.ToString("C2");
            }
            else
            {
                BeklemedenCikar();
                bIslemBeklet.BackColor = Color.FromArgb(0, 192, 0);
                bIslemBeklet.Text = "İşlem Beklet";
                gridBekle.Rows.Clear();
                GenelToplam();
                
            }


        }
        private void Bekle()
        {
            int satirsayisi = gridSatisListesi.Rows.Count;
            int sutunsayisi = gridSatisListesi.Columns.Count;
            if (satirsayisi > 0)
            {
                for (int i = 0; i < satirsayisi; i++)
                {
                    gridBekle.Rows.Add();
                    for (int j = 0; j < sutunsayisi - 1; j++)
                    {
                        gridBekle.Rows[i].Cells[j].Value = gridSatisListesi.Rows[i].Cells[j].Value;  
                    }
                }
            }
        }
        private void BeklemedenCikar()
        {
            int satirsayisi = gridBekle.Rows.Count;
            int sutunsayisi = gridBekle.Columns.Count;
            if (satirsayisi > 0)
            {
                for (int i = 0; i < satirsayisi; i++)
                {
                    gridSatisListesi.Rows.Add();
                    for (int j = 0; j < sutunsayisi - 1; j++)
                    {
                        gridSatisListesi.Rows[i].Cells[j].Value = gridBekle.Rows[i].Cells[j].Value;
                    }
                }
            }
        }

        private void chSatisIadeIslemi_CheckedChanged(object sender, EventArgs e)
        {
            if (chSatisIadeIslemi.Checked)
            {
                chSatisIadeIslemi.Text = "İade Yapılıyor";
            }
            else
            {
                chSatisIadeIslemi.Text = "Satış Yapılıyor";
            }
        }

  
    }
}
