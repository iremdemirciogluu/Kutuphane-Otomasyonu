using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace proje
{
    public partial class Form2 : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-503B5NV\\SQLEXPRESS;Initial Catalog=kutuphaneotomasyonu;Integrated Security=True");

        private int uyeid;
        public Form2(int uyeid)
        {
            InitializeComponent();
            this.uyeid = uyeid;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            con.Open();
            Form16 form16 = new Form16(uyeid);
            form16.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form8 ukitap = new Form8(uyeid);
            ukitap.Show();
            this.Hide();
        }



        private void button2_Click(object sender, EventArgs e)
        {
            Form10 populer = new Form10(uyeid);
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

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            Form20 form20 = new Form20(uyeid);
            form20.Show();
            this.Hide();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            if (Form1.dilIngilizce)
            {
                button1.Text = "BOOKS";
                button2.Text = "POPULAR BOOKS";
                button4.Text = " JOURNALS";
                button3.Text = "BORROWED BOOKS";
            }
            else
            {
               
                button1.Text = "KİTAPLAR";
                button2.Text = "POPÜLLER KİTAPLAR";
                button4.Text = " DERGİLER";
                button3.Text = "ÖDÜNÇ ALINAN KİTAPLAR";
            }

        }
        
    }
}
