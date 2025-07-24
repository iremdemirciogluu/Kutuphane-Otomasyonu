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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace proje
{
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
            TurleriYukle();

        }
        static string constring = ("Data Source=DESKTOP-503B5NV\\SQLEXPRESS;Initial Catalog=kutuphaneotomasyonu;Integrated Security=True");
        SqlConnection con = new SqlConnection(constring);
        private void TurleriYukle()
        {
 
                string query = "SELECT tur_id, KitapTurAdi FROM kitaptür ";
                SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                DataTable tur = new DataTable();
                adapter.Fill(tur);

                comboBox1.DataSource = tur;
                comboBox1.DisplayMember = "KitapTurAdi";
                comboBox1.ValueMember = "tur_id";
               
           
        }
        private void button1_Click(object sender, EventArgs e)
        {
           

            try
            {

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    string kayit = "insert into kitaplar (adi, yazarlar, yayinevi, BarkodNo, stoksayisi, tur_id, sayfasayisi, rafNo, açiklama, puan ) values"+
                        "(@adi, @yazarlar, @yayinevi, @BarkodNo, @stoksayisi, @tur_id, @sayfasayisi, @rafNo, @açiklama, @puan)";
                    SqlCommand cmd = new SqlCommand(kayit, con);
                    cmd.Parameters.AddWithValue("@adi", textBox5.Text);
                    cmd.Parameters.AddWithValue("@yazarlar", textBox4.Text);
                    cmd.Parameters.AddWithValue("@yayinevi", textBox3.Text);
                    cmd.Parameters.AddWithValue("@BarkodNo", textBox2.Text);
                    cmd.Parameters.AddWithValue("@stoksayisi", textBox1.Text);
                    cmd.Parameters.AddWithValue("@sayfasayisi", textBox8.Text);
                    cmd.Parameters.AddWithValue("@rafNo", textBox9.Text);
                    cmd.Parameters.AddWithValue("@açiklama", textBox10.Text);
                    cmd.Parameters.AddWithValue("@puan", textBox11.Text);
                    cmd.Parameters.AddWithValue("@tur_id", comboBox1.SelectedValue);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Kitap ekleme işlemi tamamlanmıştır");
                    con.Close();
                    
                    kayit_getir();

                }
            }


            catch (Exception ex)
            {
                MessageBox.Show("bir hata var" + ex.Message);
                con.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlTransaction transaction = con.BeginTransaction();
            try
            {
                string arsiv = "insert into arsiv (barkodno) select barkodno from kitaplar where barkodno=@barkodno";
                SqlCommand cmd = new SqlCommand(arsiv, con, transaction);
                cmd.Parameters.AddWithValue("@barkodno", textBox2.Text);
                cmd.ExecuteNonQuery();

                string degis = "update kitaplar set durum = 'arsiv' where barkodno=@barkodno";
                SqlCommand cmd2 = new SqlCommand(degis, con, transaction);
                cmd2.Parameters.AddWithValue("@barkodno", textBox2.Text);
                cmd2.ExecuteNonQuery();


                MessageBox.Show("Arşiv işlemi tamamlandı");
                transaction.Commit();
                
            }
            catch (Exception ex)
            {

                transaction.Rollback();
                MessageBox.Show("HATA" + ex.Message);
            }

            con.Close();
            kayit_getir();
        }

            private void Form7_Load(object sender, EventArgs e)
        {
            kayit_getir();
        }

        public void kayit_getir()
        {
            con.Open();
            string getir = "SELECT  k.adi AS adı, k.yazarlar AS Yazarı, k.yayinevi AS Yayınevi,  k.barkodno as BarkodNo,   k.stoksayisi AS StokSayısı, t.KitapTurAdi AS Türü,"+
                "k.sayfasayisi AS SayfaSayısı,   k.rafNo AS RafNo,  k.puan AS Puan, k.açiklama AS Açıklama FROM kitaplar k INNER JOIN kitaptür t ON k.tur_id = t.tur_id where durum='aktif'";
            SqlCommand cmd = new SqlCommand(getir, con);
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            dataGridView1.DataSource = dt;

            con.Close();
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox8.Clear();
            textBox9.Clear();
            textBox10.Clear();
            textBox11.Clear();
            comboBox1.ResetText();

        }


        public void verisil(int BarkodNo)
        {


            string sil = "Delete from kitaplar where barkodno=@barkodNo";
            SqlCommand cmd = new SqlCommand(sil, con);
            
            con.Open();
            cmd.Parameters.AddWithValue("@barkodNo", BarkodNo);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
           
                DialogResult sonuc = MessageBox.Show("Silmek istediğinize emin misiniz?", "ONAY", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (sonuc == DialogResult.Yes)
                {

                    foreach (DataGridViewRow dr in dataGridView1.SelectedRows)
                    {
                        int BarkodNo = Convert.ToInt32(dr.Cells[3].Value);
                        verisil(BarkodNo);
                        kayit_getir();

                    }
                }
            
           
        }

        int i = 0;
        private void button3_Click(object sender, EventArgs e)
        { 


            con.Open();
            String güncel = "update kitaplar set adi=@adi, yazarlar=@yazarlar, yayinevi=@yayinevi, stoksayisi=@stoksayisi, " +
                "tur_id=@tur_id, rafNo=@rafNo, sayfasayisi=@sayfasayisi, puan=@puan, açiklama=@açiklama where barkodno=@barkodno";
           
            SqlCommand cmd =new SqlCommand(güncel, con);
          
            cmd.Parameters.AddWithValue("@adi", textBox5.Text);
            cmd.Parameters.AddWithValue("@yazarlar", textBox4.Text);
            cmd.Parameters.AddWithValue("@yayinevi", textBox3.Text);
            cmd.Parameters.AddWithValue("@stoksayisi", textBox1.Text);
            cmd.Parameters.AddWithValue("@sayfasayisi", textBox8.Text);
            cmd.Parameters.AddWithValue("@rafNo", textBox9.Text);
            cmd.Parameters.AddWithValue("@açiklama", textBox10.Text);
            cmd.Parameters.AddWithValue("@puan", textBox11.Text);
            cmd.Parameters.AddWithValue("@tur_id", comboBox1.SelectedValue);
            cmd.Parameters.AddWithValue("@barkodno", textBox2.Text);

            cmd.ExecuteNonQuery();
            
             
            con.Close();
            kayit_getir();

           
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            i = e.RowIndex;
            textBox1.Text = dataGridView1.Rows [i].Cells[4].Value.ToString();
            textBox3.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
            textBox5.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
            textBox8.Text = dataGridView1.Rows[i].Cells[6].Value.ToString();
            textBox9.Text = dataGridView1.Rows[i].Cells[7].Value.ToString();
            textBox10.Text = dataGridView1.Rows[i].Cells[9].Value.ToString();
            textBox11.Text = dataGridView1.Rows[i].Cells[8].Value.ToString();
            comboBox1.Text = dataGridView1.Rows[i].Cells[5].Value.ToString();
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

        private void textBox7_Click(object sender, EventArgs e)
        {
            textBox7.Clear();
            textBox7.ForeColor = Color.Black;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Form3 panasayfa = new Form3();
            panasayfa.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                string arsiv = "insert into populer (barkodno) select barkodno from kitaplar where barkodno=@barkodno";
                SqlCommand cmd = new SqlCommand(arsiv, con);
                cmd.Parameters.AddWithValue("@barkodno", textBox2.Text);
                cmd.ExecuteNonQuery();


                MessageBox.Show("İşlem tamamlandı");

            }

            catch (Exception ex)
            {


                MessageBox.Show("HATA" + ex.Message);
            }

             con.Close();
            kayit_getir();
}
    }

}
  