using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace proje
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form7 pkitaplar = new Form7();
            pkitaplar.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form9 populer = new Form9();
            populer.Show();
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            DialogResult sonuc = MessageBox.Show("Çıkış yapmak istediğinize emin misiniz?", "ONAY", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (sonuc == DialogResult.Yes)
            {
                Form1 giris = new Form1();
                giris.Show();
                this.Hide();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form11 uyeler = new Form11();
            uyeler.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form13 islemler = new Form13();
            islemler.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form15 arsiv = new Form15();
            arsiv.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form17 form17 = new Form17();   
            form17.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form19 form19 = new Form19();
            form19.Show();
            this.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Form21 form21 = new Form21();
            form21.Show();
            this.Hide();
        }
    }
}
