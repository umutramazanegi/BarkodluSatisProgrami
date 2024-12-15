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
    public partial class fGelirGider : Form
    {
        public fGelirGider()
        {
            InitializeComponent();
        }

        public string gelirgider { get; set; }
        public string kullanici { get; set; }
        private void fGelirGider_Load(object sender, EventArgs e)
        {
            lGelirGider.Text = gelirgider + " İŞLEMİ YAPILIYOR";
        }

        private void cmbOdemeTuru_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbOdemeTuru.SelectedIndex==0) //Nakit
            {
                tNakit.Enabled = true;
                tKart.Enabled = false;
            }
            else if (cmbOdemeTuru.SelectedIndex==1) //Kart
            {
                tKart.Enabled = true;
                tNakit.Enabled = false;
            }else if (cmbOdemeTuru.SelectedIndex==2) //Kart ve Nakit
            {
                tKart.Enabled = true;
                tNakit.Enabled = true;
            }
            tNakit.Text = "0";
            tKart.Text = "0";
        }

        private void bEkle_Click(object sender, EventArgs e)
        {
            if (cmbOdemeTuru.Text !="")
            {
                if (tNakit.Text !="" && tKart.Text !="")
                {
                    using (var db =new Entities())
                    {
                        IslemOzet io = new IslemOzet();
                        io.IslemNo = 0;
                        io.Iade = false;
                        io.OdemeSekli=cmbOdemeTuru.Text;
                        io.Nakit = Islemler.DoubleYap(tNakit.Text);
                        io.Kart = Islemler.DoubleYap(tKart.Text);
                        if (gelirgider=="GELİR")
                        {
                            io.Gelir = true;
                            io.Gider = false;
                        }
                        else
                        {
                            io.Gelir = false;
                            io.Gider = true;
                        }
                        io.AlisFiyatToplam = 0;
                        io.Aciklama = gelirgider + " - İşlemi " + tAciklama.Text;
                        io.Tarih = dtTarih.Value;
                        io.Kullanici = kullanici;
                        db.IslemOzet.Add(io);
                        db.SaveChanges();
                        MessageBox.Show(gelirgider + " işlemi kaydedidi.");
                        tNakit.Text = "0";
                        tKart.Text = "0";
                        tAciklama.Clear();
                        cmbOdemeTuru.SelectedIndex = -1;
                        fRapor f = (fRapor)Application.OpenForms["fRapor"];
                        if (f!=null)
                        {
                            f.bGoster_Click(null , null);
                        }
                        this.Hide();
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen Ödeme Türü Seçiniz");
            }
        }
    }
}
