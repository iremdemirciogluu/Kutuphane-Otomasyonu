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
    public partial class Form8 : Form
    {
        private int uyeid;
        public Form8(int uyeid)
        {
            InitializeComponent();
            this.uyeid = uyeid;
        }
        static string constring = ("Data Source=DESKTOP-503B5NV\\SQLEXPRESS;Initial Catalog=kutuphaneotomasyonu;Integrated Security=True");
        SqlConnection con = new SqlConnection(constring);
            public void kayit_getir()
            {
                con.Open();
                string getir = "SELECT  k.adi AS adi, k.yazarlar AS Yazar, k.yayinevi AS Yayinevi,  k.BarkodNo,   k.stoksayisi AS StokSayisi, t.KitapTurAdi AS Tur," +
                    "k.sayfasayisi AS SayfaSayisi,   k.rafNo AS RafNo,  k.puan AS Puan, k.açiklama AS Aciklama, durum FROM kitaplar k INNER JOIN kitaptür t ON k.tur_id = t.tur_id ";
                SqlCommand cmd = new SqlCommand(getir, con);
                SqlDataAdapter ad = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                ad.Fill(dt);
                dataGridView1.DataSource = dt;

                con.Close();

            }


 

        private void Form8_Load(object sender, EventArgs e)
        {
            kayit_getir();
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int i = 0;
            i = e.RowIndex;
            textBox1.Text = dataGridView1.Rows[i].Cells[4].Value.ToString();
            textBox3.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
            textBox5.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
            textBox8.Text = dataGridView1.Rows[i].Cells[6].Value.ToString();
            textBox9.Text = dataGridView1.Rows[i].Cells[7].Value.ToString();
            textBox10.Text = dataGridView1.Rows[i].Cells[9].Value.ToString();
            textBox11.Text = dataGridView1.Rows[i].Cells[8].Value.ToString();
            textBox6.Text = dataGridView1.Rows[i].Cells[5].Value.ToString();
            textBox2.Text = dataGridView1.Rows[i].Cells[3].Value.ToString();
        }

        

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            string arama = "select * from kitaplar where adi=@adi or yazarlar=@yazarlar";
            SqlCommand cmd = new SqlCommand(arama, con);

            cmd.Parameters.AddWithValue("@adi", textBox7.Text);
            cmd.Parameters.AddWithValue("@yazarlar", textBox7.Text);


            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }
        

        private void textBox7_Click_1(object sender, EventArgs e)
        {
            textBox7.Clear();
            textBox7.ForeColor = Color.Black;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Form2 uanasayfa = new Form2(uyeid);
            uanasayfa.Show();
            this.Hide();
        }
    }
}
