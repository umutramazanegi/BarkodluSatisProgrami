using Lisans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BarkodluSatisProgrami
{
    public class Kontrol
    {
        Entities db = new Entities();
        Guvenlik guvenlik = new Guvenlik();
        Lic lic = new Lic();
        public bool KontrolYap()
        {
            bool durum = false;
            if(db.Guvenlik.Count()==0)
            {
                LisansFormuAc();
            }
            else
            {
                Lic lic = new Lic();
                var guvenlik = db.Guvenlik.First();
                if(lic.TarihCoz(guvenlik.baslangic) < DateTime.Now)
                {
                    guvenlik.baslangic = lic.TarihSifrele(DateTime.Now);
                    db.SaveChanges();
                    durum = true;
                }
                if (lic.TarihKontrol(lic.TarihCoz(guvenlik.baslangic), lic.TarihCoz(guvenlik.bitis)))
                {
                    durum = true;
                }
                else
                {
                    durum = false;
                    LisansFormuAc();
                     
                }
            }
            return durum;
                 
        }

        public void LisansFormuAc()
        {

            Lic lic = new Lic();
            fLisans f = new fLisans();
            f.lKontrolNo.Text = lic.EkrandaGoster().ToString();
            f.Show();
        }

        public void Lisansla(string girilenkod)
        {
            int durum = lic.GirilenLisansiKontrolEt(girilenkod);
            switch (durum)
            {
                case 0: // Geçersiz Lisans kodu
                    System.Windows.Forms.MessageBox.Show("Girilen lisans numarası geçersizdir.");
                    break;
                case 1:
                    DemoOlustur();
                    break;
                case 2:
                    YillikOlustur();
                    break;
                default:
                    break;
            }
        }
        private void GuvenlikEkle(string baslangic, string bitis)
        {
            guvenlik.baslangic = baslangic;
            guvenlik.bitis = bitis;
            db.Guvenlik.Add(guvenlik);
            db.SaveChanges();
        }

        private void GuvenlikGuncelle(string baslangic, string bitis)
        {
            var guvenlikguncelle = db.Guvenlik.First();
            guvenlikguncelle.baslangic = baslangic;
            guvenlikguncelle.bitis = bitis;
            db.SaveChanges();
        }

        private int GuvenlikEkliMi()
        {
            return db.Guvenlik.Count();
        }
        private void DemoOlustur()
        {
            try
            {
                if (GuvenlikEkliMi() == 0)
                {
                    // db ekleme işlemi 
                    GuvenlikEkle(lic.TarihSifrele(DateTime.Now), lic.TarihSifrele(lic.DemoTarihOlustur()));
                }
                else
                {
                    //db guncelleme işlemi
                    GuvenlikGuncelle(lic.TarihSifrele(DateTime.Now), lic.TarihSifrele(lic.DemoTarihOlustur()));
                }
                System.Windows.Forms.MessageBox.Show("Program 10 günlük demo olarak kullanıma açılmıştır. \n Programı tekrar çalıştırınız...");
                Application.Exit();
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show("HATA OLUŞTU");
            }
           
        }

        private void YillikOlustur()
        {
            try
            {
                if (GuvenlikEkliMi() == 0)
                {
                    // db ekleme işlemi 
                    GuvenlikEkle(lic.TarihSifrele(DateTime.Now), lic.TarihSifrele(lic.YillikTarihOlustur()));
                }
                else
                {
                    //db guncelleme işlemi
                    GuvenlikGuncelle(lic.TarihSifrele(DateTime.Now), lic.TarihSifrele(lic.YillikTarihOlustur()));
                }
                System.Windows.Forms.MessageBox.Show("Program 1 yıllık  olarak kullanıma açılmıştır. \n Programı tekrar çalıştırınız...");
                Application.Exit();
            }
            catch (Exception)
            {

                System.Windows.Forms.MessageBox.Show("HATA OLUŞTU");
            }
           
        }
    }
}
