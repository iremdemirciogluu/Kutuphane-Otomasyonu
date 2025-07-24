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
    public partial class Form17 : Form
    {
        public Form17()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-503B5NV\\SQLEXPRESS;Initial Catalog=kutuphaneotomasyonu;Integrated Security=True");
        private void Form17_Load(object sender, EventArgs e)
        {
            string sorgu = "select u.ad, u.soyad, k.adi, k.yazarlar as yazari, k.yayinevi,  o.veris_tarihi as EmanetTarihi, o.alis_tarihi as TeslimAlisTarihi" +
                ", o.durumu from odunc o join uyeler u on o.uye_id = u.uye_id join kitaplar k on k.barkodno = o.barkodno order by veris_tarihi asc, durumu desc";
            SqlCommand cmd = new SqlCommand(sorgu, con);

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

            Form3 uanasayfa = new Form3();
            uanasayfa.Show();
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            string arama = "select u.ad, u.soyad, k.adi, k.yazarlar as yazari, k.yayinevi,  o.veris_tarihi as EmanetTarihi, o.alis_tarihi as TeslimAlisTarihi, o.durumu from odunc o join uyeler u on o.uye_id = u.uye_id" +
                " join kitaplar k on k.barkodno = o.barkodno order by veris_tarihi asc, durumu desc where ad=@ad";
            SqlCommand cmd = new SqlCommand(arama, con);

            cmd.Parameters.AddWithValue("@ad", textBox13.Text);     
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void textBox13_Click(object sender, EventArgs e)
        {
            textBox13.Clear();
            textBox13.ForeColor = Color.Black;
        }
    }
}
