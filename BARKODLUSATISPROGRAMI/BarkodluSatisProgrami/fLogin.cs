using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lisans;
namespace BarkodluSatisProgrami
{
    public partial class fLogin : Form
    {
        public fLogin()
        {
            InitializeComponent();
        }

        private void bGirisYap_Click(object sender, EventArgs e)
        {
           girisyap();
        }
        private void girisyap()
        {
             if (tKullaniciAdi.Text !="" && tSifre.Text!="")
            {
                try
                {
                    using (var db = new Entities())
                    {
                        if (db.Kullanici.Any())
                        {
                            var bak = db.Kullanici.Where(x => x.KullaniciAd == tKullaniciAdi.Text && x.Sifre == tSifre.Text).FirstOrDefault();
                            if (bak !=null)
                            {
                                Cursor.Current = Cursors.WaitCursor;
                                Kontrol kontrol = new Kontrol();
                                if(kontrol.KontrolYap())
                                {

                                    fBaslangic f = new fBaslangic();
                                    f.bSatisIslemi.Enabled = (bool)bak.Satis;
                                    f.bGenelRapor.Enabled = (bool)bak.Rapor;
                                    f.bStok.Enabled = (bool)bak.Stok;
                                    f.bUrunGiris.Enabled = (bool)bak.UrunGiris;
                                    f.bAyarlar.Enabled = (bool)bak.Ayarlar;
                                    f.bFiyatGuncelle.Enabled = (bool)bak.FiyatGuncelle;
                                    f.bYedekle.Enabled = (bool)bak.Yedekleme;
                                    f.lKullanici.Text = bak.AdSoyad;
                                    var isyeri = db.Sabit.FirstOrDefault();
                                    f.lIsYeri.Text = isyeri.Unvan;
                                    f.Show();
                                    this.Hide();
                                }

                                Cursor.Current = Cursors.Default;
                            }
                            else
                            {
                                MessageBox.Show("Kullanıcı adı veya şifre hatalı");
                                tSifre.Clear();
                                tSifre.Focus();
                            }
                        }
                        else
                        {
                            Cursor.Current = Cursors.WaitCursor;
                            Kullanici k = new Kullanici();
                            k.AdSoyad = "admin";
                            k.Ayarlar = true;
                            k.E_Posta = "ozgurmemis3@gmail.com";
                            k.FiyatGuncelle = true;
                            k.KullaniciAd = "admin";
                            k.Sifre = "admin";
                            k.Rapor = true;
                            k.Satis = true;
                            k.Stok = true;
                            k.Telefon = "5423916347";
                            k.UrunGiris = true;
                            k.Yedekleme = true;
                            db.Kullanici.Add(k);
                            db.SaveChanges();

                            var bak = db.Kullanici.Where(x => x.KullaniciAd == tKullaniciAdi.Text && x.Sifre == tSifre.Text).FirstOrDefault();
                            fBaslangic f = new fBaslangic();
                            f.bSatisIslemi.Enabled = (bool)bak.Satis;
                            f.bGenelRapor.Enabled = (bool)bak.Rapor;
                            f.bStok.Enabled = (bool)bak.Stok;
                            f.bUrunGiris.Enabled = (bool)bak.UrunGiris;
                            f.bAyarlar.Enabled = (bool)bak.Ayarlar;
                            f.bFiyatGuncelle.Enabled = (bool)bak.FiyatGuncelle;
                            f.bYedekle.Enabled = (bool)bak.Yedekleme;
                            f.lKullanici.Text = bak.AdSoyad;
                            var isyeri = db.Sabit.FirstOrDefault();
                            f.lIsYeri.Text = isyeri.Unvan;
                            f.Show();
                            this.Hide();
                            Cursor.Current = Cursors.Default;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
        private void fLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                girisyap();
            }
        }

        private void bİletisim_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            fIletisim filetisim = new fIletisim();
            filetisim.ShowDialog();
            Cursor.Current = Cursors.Default;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Lic lic = new Lic();
            label1.Text = lic.CpuNo() + "\n" + lic.CpuKarakterToplam().ToString();
        }

        private void fLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
