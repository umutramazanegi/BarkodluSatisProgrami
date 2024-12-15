using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace BarkodluSatisProgrami
{
    internal class Yazdir
    {
        public int? IslemNo { get; set; }
        public Yazdir(int? islemno)
        {
            IslemNo = islemno;
        }
        PrintDocument pd = new PrintDocument();
        public void YazdirmayaBasla()
        {
            try
            {
                pd.PrintPage += Pd_PrintPage;
                pd.Print();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Pd_PrintPage(object sender, PrintPageEventArgs e)
        {
            Entities db = new Entities();
            var isyeri  = db.Sabit.FirstOrDefault();
            var liste = db.Satis.Where(x => x.IslemNo == IslemNo).ToList();  // Satis tablosundan verileri çekmek için 
            if (isyeri != null && liste != null)
            {
                int kagituzunluk = 120;
                for (int i = 0; i < liste.Count; i++)
                {
                    kagituzunluk += 15;
                }
                PaperSize ps58 = new PaperSize("58mm Termal", 220, kagituzunluk + 120);
                pd.DefaultPageSettings.PaperSize = ps58;

                Font fontBaslik = new Font("Calibri", 10, FontStyle.Bold);
                Font fontBilgi = new Font("Calibri", 8, FontStyle.Bold);
                Font fontIcerikBaslik = new Font("Calibri", 8, FontStyle.Underline);
                StringFormat ortala = new StringFormat(StringFormatFlags.FitBlackBox);
                ortala.Alignment = StringAlignment.Center;
                RectangleF rcUnvanKonum = new RectangleF(0, 20, 220, 20);
                e.Graphics.DrawString(isyeri.Unvan, fontBaslik, Brushes.Black,rcUnvanKonum, ortala);
                e.Graphics.DrawString("Telefon : " + isyeri.Telefon, fontBilgi, Brushes.Black, new Point(5, 45));
                e.Graphics.DrawString("İşlem No : " + IslemNo.ToString(), fontBilgi, Brushes.Black, new Point(5, 60));
                e.Graphics.DrawString("Tarih : " + DateTime.Now, fontBilgi, Brushes.Black, new Point(5, 75));
                e.Graphics.DrawString("-----------------------------------------------------------------", fontBilgi, Brushes.Black, new Point(5,90));

                e.Graphics.DrawString("Ürün Adı", fontBilgi, Brushes.Black, new Point(5, 105));
                e.Graphics.DrawString("Miktar", fontBilgi, Brushes.Black, new Point(100, 105));
                e.Graphics.DrawString("Fiyat", fontBilgi, Brushes.Black, new Point(140, 105));
                e.Graphics.DrawString("Tutar", fontBilgi, Brushes.Black, new Point(195, 105));

                // KDV hesaplamaları için değişkenler
                double toplamKdvsiz = 0;
                double toplamKdv = 0;
                double kdvOrani = 0.08;
                int yuksekik = 120;
                foreach (var item in liste)
                {
                    e.Graphics.DrawString(item.UrunAd, fontBilgi, Brushes.Black, new Point(5, yuksekik));
                    e.Graphics.DrawString(item.Miktar.ToString(), fontBilgi, Brushes.Black, new Point(110, yuksekik));
                    e.Graphics.DrawString(Convert.ToDouble(item.SatisFiyat).ToString("C2"), fontBilgi, Brushes.Black, new Point(140, yuksekik));
                    e.Graphics.DrawString(Convert.ToDouble(item.Toplam).ToString("C2"), fontBilgi, Brushes.Black, new Point(195, yuksekik));
                    double kdvsizFiyat = item.SatisFiyat.GetValueOrDefault();
                    double miktar = item.Miktar.GetValueOrDefault();
                    double kdvsizTutar = kdvsizFiyat * miktar;
                    toplamKdvsiz += kdvsizTutar;

                    double urunKdv = kdvsizFiyat * miktar * kdvOrani; // Ürün için KDV hesapla
                    toplamKdv += urunKdv; // Toplam KDV'ye ekle
                    yuksekik += 15;
                }
                double genelToplam = toplamKdvsiz + toplamKdv;
                e.Graphics.DrawString("-----------------------------------------------------------------", fontBilgi, Brushes.Black, new Point(5, yuksekik));
                yuksekik += 20;

                if (toplamKdv > 0)
                {
                    e.Graphics.DrawString("Toplam (KDV Hariç): " + toplamKdvsiz.ToString("C2"), fontBilgi, Brushes.Black, new Point(5, yuksekik));
                    yuksekik += 15;

                    e.Graphics.DrawString("KDV: " + toplamKdv.ToString("C2"), fontBilgi, Brushes.Black, new Point(5, yuksekik));
                    yuksekik += 15;

                    e.Graphics.DrawString("Genel Toplam (KDV Dahil): " + genelToplam.ToString("C2"), fontBaslik, Brushes.Black, new Point(5, yuksekik));
                    yuksekik += 20;
                }
                else
                {
                    e.Graphics.DrawString("Genel Toplam: " + genelToplam.ToString("C2"), fontBaslik, Brushes.Black, new Point(5, yuksekik));


                    yuksekik += 20;


                }
                //e.Graphics.DrawString("-----------------------------------------------------------------", fontBilgi, Brushes.Black, new Point(5, yuksekik));
                //e.Graphics.DrawString("Toplam : " + Convert.ToDouble( liste.Sum(x => x.Toplam)).ToString("C2"), fontBaslik, Brushes.Black, new Point(5, yuksekik + 20));
                e.Graphics.DrawString("-----------------------------------------------------------------", fontBilgi, Brushes.Black, new Point(5, yuksekik + 40));
                e.Graphics.DrawString("(Mali Değeri Yoktur.)", fontBilgi, Brushes.Black, new Point(5, yuksekik + 20));
            }
        }
    }
}
