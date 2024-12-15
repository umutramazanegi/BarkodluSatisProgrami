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
    public partial class fBaslangic : Form
    {
        public fBaslangic()
        {
            InitializeComponent();
        }

        private void bSatisIslemi_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            fSatis fSatis = new fSatis();
            fSatis.lKullanici.Text = lKullanici.Text;
            fSatis.ShowDialog();
            Cursor.Current = Cursors.Default;
        }

        private void bGenelRapor_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            fRapor fRapor = new fRapor();
            fRapor.lKullanici.Text = lKullanici.Text;
            fRapor.ShowDialog();
            Cursor.Current = Cursors.Default;
        }

        private void bStok_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            fStok fStok = new fStok();
            fStok.lKullanici.Text = lKullanici.Text;
            fStok.ShowDialog();
            Cursor.Current = Cursors.Default;
        }

        private void bUrunGiris_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            fUrunGiris fUrunGiris = new fUrunGiris();
            fUrunGiris.lKullanici.Text = lKullanici.Text;
            fUrunGiris.ShowDialog();
            Cursor.Current = Cursors.Default;
        }

        private void bCikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bFiyatGuncelle_Click(object sender, EventArgs e)
        {
            fFiyatGuncelle fFiyatGuncelle = new fFiyatGuncelle();
            fFiyatGuncelle.lKullanici.Text = lKullanici.Text;
            fFiyatGuncelle.ShowDialog();

        }

        private void bAyarlar_Click(object sender, EventArgs e)
        {
            fAyarlar fAyarlar = new fAyarlar();
            fAyarlar.lKullanici.Text = lKullanici.Text;
            fAyarlar.ShowDialog();
        }

        private void bYedekle_Click(object sender, EventArgs e)
        {
            Islemler.Backup();
        }

        private void bKullaniciDegistir_Click(object sender, EventArgs e)
        {
            fLogin fLogin = new fLogin();
            fLogin.Show();
            this.Hide();
        }

        private void fBaslangic_Load(object sender, EventArgs e)
        {

        }

        private void lisansla_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            fLisans fLisans = new fLisans();
            fLisans.Show();
            Cursor.Current = Cursors.Default;
           // this.Hide();
        }
    }
}
