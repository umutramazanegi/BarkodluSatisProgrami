using System;
using System.Windows.Forms;

namespace BarkodluSatisProgrami
{
    public partial class fNakitKart : Form
    {
        private bool _updating = false; // Durum kontrol değişkeni

        public fNakitKart()
        {
            InitializeComponent();
            tKart.TextChanged += tKart_TextChanged;
            tNakit.TextChanged += tNakit_TextChanged;
        }

        private void tNakit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Hesapla();
            }
        }

        private void Hesapla()
        {
            if (_updating) return; // Hesaplamayı engelle
            _updating = true;

            fSatis f = (fSatis)Application.OpenForms["fSatis"];
            double nakit = Islemler.DoubleYap(tNakit.Text);
            double genelToplam = Islemler.DoubleYap(f.tGenelToplam.Text);
            double kart = genelToplam - nakit;

            f.lNakit.Text = nakit.ToString("C2");
            f.lKart.Text = kart.ToString("C2");
            tKart.Text = kart.ToString("C2");

            f.SatisYap("Kart-Nakit");

            _updating = false;
        }

        private void tKart_TextChanged(object sender, EventArgs e)
        {
            if (_updating) return; // Hesaplamayı engelle

            if (IsNumeric(tKart.Text))
            {
                _updating = true;
                fSatis f = (fSatis)Application.OpenForms["fSatis"];
                double genelToplam = Islemler.DoubleYap(f.tGenelToplam.Text);
                double kartValue = Islemler.DoubleYap(tKart.Text);
                double newNakit = genelToplam - kartValue;

                tNakit.Text = newNakit.ToString("C2");
                f.lNakit.Text = newNakit.ToString("C2");

                _updating = false;
            }
        }

        private void tNakit_TextChanged(object sender, EventArgs e)
        {
            if (_updating) return; // Hesaplamayı engelle

            if (IsNumeric(tNakit.Text))
            {
                _updating = true;
                fSatis f = (fSatis)Application.OpenForms["fSatis"];
                double genelToplam = Islemler.DoubleYap(f.tGenelToplam.Text);
                double nakitValue = Islemler.DoubleYap(tNakit.Text);
                double newKart = genelToplam - nakitValue;

                tKart.Text = newKart.ToString("C2");
                f.lKart.Text = newKart.ToString("C2");

                _updating = false;
            }
        }

        private void bNx_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if (b.Text == ",")
            {
                if (!tNakit.Text.Contains(","))
                {
                    tNakit.Text += b.Text;
                }
            }
            else if (b.Text == "<")
            {
                if (tNakit.Text.Length > 0)
                {
                    tNakit.Text = tNakit.Text.Substring(0, tNakit.Text.Length - 1);
                }
            }
            else
            {
                tNakit.Text += b.Text;
            }
        }

        private void bEnter_Click(object sender, EventArgs e)
        {
            if (IsNumeric(tNakit.Text) || IsNumeric(tKart.Text))
            {
                Hesapla();

            }
        }

        private void tNakit_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Kullanıcının sadece rakam ve virgül girmesine izin ver
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != ',')
            {
                e.Handled = true;
            }
        }

        private void bTemizle_Click(object sender, EventArgs e)
        {
            // TextBox'ları temizle
            tKart.Text = "";
            tNakit.Text = "";
        }

        private bool IsNumeric(string text)
        {
            double number;
            return double.TryParse(text.Replace(",", "."), out number);
        }
    }
}
