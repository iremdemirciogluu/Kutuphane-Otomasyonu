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
    public partial class Form11 : Form
    {
        public Form11()
        {
            InitializeComponent();
        }
        static string constring = ("Data Source=DESKTOP-503B5NV\\SQLEXPRESS;Initial Catalog=kutuphaneotomasyonu;Integrated Security=True");
        SqlConnection con = new SqlConnection(constring);

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            string arama = "select uye_id, ad, soyad, telefon, email, okunankitapsayısı from uyeler where ad=@ad or soyad=@soyad";
            SqlCommand cmd = new SqlCommand(arama, con);

            cmd.Parameters.AddWithValue("@ad", textBox7.Text);
            cmd.Parameters.AddWithValue("@soyad", textBox7.Text);


            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }
        private void textBox7_Click(object sender, EventArgs e)
        {
            textBox7.Clear();
            textBox7.ForeColor = Color.Black;
        }

        public void kayit_getir()
        {
            con.Open();
            string getir = "SELECT uye_id as üyeid, ad, soyad, telefon, email, okunankitapsayısı from uyeler";
            SqlCommand cmd = new SqlCommand(getir, con);
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            dataGridView1.DataSource = dt;

            con.Close();

        }
        public void Pkayit_getir()
        {
            con.Open();
            string getir = "SELECT p_id as personelid, ad, soyad, telefon, email from personel";
            SqlCommand cmd = new SqlCommand(getir, con);
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            dataGridView2.DataSource = dt;

            con.Close();

        }

        private void Form11_Load(object sender, EventArgs e)
        {
            kayit_getir();
            Pkayit_getir();
        }

        public void verisil(int uye_id)
        {
            con.Open();
            string sil = "Delete from uyeler where uye_id=@uye_id";
            SqlCommand cmd = new SqlCommand(sil, con);
            cmd.Parameters.AddWithValue("@uye_id", uye_id);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult sonuc = MessageBox.Show("Üyeyi silmek istediğinize emin misiniz?", "ONAY", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (sonuc == DialogResult.Yes)
            {
                foreach (DataGridViewRow dr in dataGridView1.SelectedRows)
                {
                    int uye_id = Convert.ToInt32(dr.Cells[0].Value);
                    verisil(uye_id);
                    kayit_getir();
                }
            }
        }
        public void personelverisil(int p_id)
        {
            con.Open();
            string sill = "Delete from personel where p_id=@p_id";
            SqlCommand cmd = new SqlCommand(sill, con);
            cmd.Parameters.AddWithValue("@p_id", p_id);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult sonuc = MessageBox.Show("Presoneli silmek istediğinize emin misiniz?", "ONAY", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (sonuc == DialogResult.Yes)
            {
                foreach (DataGridViewRow dr in dataGridView2.SelectedRows)
                {
                    int p_id = Convert.ToInt32(dr.Cells[0].Value);
                    personelverisil(p_id);
                    Pkayit_getir();
                }
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Form4 uyeekle = new Form4();
            uyeekle.Show();
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Form3 panasayfa = new Form3();
            panasayfa.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form12 personelkayıt = new Form12();
            personelkayıt.Show();
            this.Hide();
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox1.ForeColor = Color.Black;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            string parama = "select p_id as personelid, ad, soyad, telefon, email from personel where ad=@ad or soyad=@soyad";
            SqlCommand cmd = new SqlCommand(parama, con);

            cmd.Parameters.AddWithValue("@ad", textBox1.Text);
            cmd.Parameters.AddWithValue("@soyad", textBox1.Text);


            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView2.DataSource = dt;
            con.Close();
        }
    }
}