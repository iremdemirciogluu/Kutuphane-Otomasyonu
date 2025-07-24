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
    public partial class Form19 : Form
    {
        public Form19()
        {
            InitializeComponent();
            TurleriYukle();
        }
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-503B5NV\\SQLEXPRESS;Initial Catalog=kutuphaneotomasyonu;Integrated Security=True");
       
        private void TurleriYukle()
        {

            string query = "SELECT dergitür_id, dergitüradı FROM dergitür ";
            SqlDataAdapter adapter = new SqlDataAdapter(query, con);
            DataTable tur = new DataTable();
            adapter.Fill(tur);

            comboBox1.DataSource = tur;
            comboBox1.DisplayMember = "dergitüradı";
            comboBox1.ValueMember = "dergitür_id";


        }
        private void button1_Click(object sender, EventArgs e)
        {

            try
            {

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    string kayit = "insert into dergiler (adi, issn, yayinci, yazar, dergitür_id, baslangic_tarihi, cilt, sayi, rafno, stoksayisi, dergi_id) values" +
                        "(@adi, @issn, @yayinci, @yazar, @dergitür_id, @baslangic_tarihi, @cilt, @sayi, @rafno, @stoksayisi, @dergi_id)";
                    SqlCommand cmd = new SqlCommand(kayit, con);
                    cmd.Parameters.AddWithValue("@adi", textBox1.Text);
                    cmd.Parameters.AddWithValue("@yazar", textBox4.Text);
                    cmd.Parameters.AddWithValue("@yayinci", textBox3.Text);                   
                    cmd.Parameters.AddWithValue("@stoksayisi", textBox10.Text);
                    cmd.Parameters.AddWithValue("@sayi", textBox8.Text);
                    cmd.Parameters.AddWithValue("@rafNo", textBox9.Text);
                    cmd.Parameters.AddWithValue("@issn", textBox2.Text);
                    cmd.Parameters.AddWithValue("@baslangic_tarihi", dateTimePicker1.Value);
                    cmd.Parameters.AddWithValue("@dergitür_id", comboBox1.SelectedValue);
                    cmd.Parameters.AddWithValue("@cilt", textBox7.Text);
                    cmd.Parameters.AddWithValue("@dergi_id", textBox5.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Dergi ekleme işlemi tamamlanmıştır");
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
        public void kayit_getir()
        {
            con.Open();
            string getir = "SELECT  d.adi AS adı, d.issn as ISSN, d.yayinci AS Yayıncı, d.yazar AS Yazarı, t.dergitüradı AS Türü, d.baslangic_tarihi as BaşlangıçTarihi, d.cilt as Cilt, d.sayi as Sayı, d.rafno as RafNo," +
                " d.stoksayisi AS StokSayısı, d.dergi_id as barkodno FROM dergiler d INNER JOIN dergitür t ON d.dergitür_id = t.dergitür_id where durumm='aktif'";
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
            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();
            textBox10.Clear();
            comboBox1.ResetText();
            textBox5.Clear();

        }
        public void verisil(int dergi_id)
        {
            con.Open();
            string sil = "Delete from dergiler where dergi_id=@dergi_id";
            SqlCommand cmd = new SqlCommand(sil, con);
            cmd.Parameters.AddWithValue("@dergi_id", dergi_id);
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
                    int dergi_id = Convert.ToInt32(dr.Cells[10].Value);
                    verisil(dergi_id);
                    kayit_getir();

                }
            }
        }

        private void Form19_Load(object sender, EventArgs e)
        {
            kayit_getir();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            con.Open();
            String güncel = "update dergiler set adi=@adi, issn=@issn, yazar=@yazar, yayinci=@yayinci, stoksayisi=@stoksayisi, baslangic_tarihi=@baslangic_tarihi, cilt=@cilt, sayi=sayi," +
                "dergitür_id=@dergitür_id, rafNo=@rafNo where dergi_id=@barkodno";

            SqlCommand cmd = new SqlCommand(güncel, con);

            cmd.Parameters.AddWithValue("@adi", textBox1.Text);
            cmd.Parameters.AddWithValue("@yazar", textBox4.Text);
            cmd.Parameters.AddWithValue("@yayinci", textBox3.Text);
            cmd.Parameters.AddWithValue("@stoksayisi", textBox10.Text);
            cmd.Parameters.AddWithValue("@sayi", textBox8.Text);
            cmd.Parameters.AddWithValue("@rafNo", textBox9.Text);
            cmd.Parameters.AddWithValue("@issn", textBox2.Text);
            cmd.Parameters.AddWithValue("@baslangic_tarihi", dateTimePicker1.Value);
            cmd.Parameters.AddWithValue("@dergitür_id", comboBox1.SelectedValue);
            cmd.Parameters.AddWithValue("@cilt", textBox7.Text);
            cmd.Parameters.AddWithValue("@barkodno", textBox5.Text);


            cmd.ExecuteNonQuery();
            con.Close();
            kayit_getir();




        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int i = 0;
            i = e.RowIndex;
            textBox1.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
            textBox3.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.Rows[i].Cells[3].Value.ToString();
            textBox7.Text = dataGridView1.Rows[i].Cells[6].Value.ToString();
            textBox8.Text = dataGridView1.Rows[i].Cells[7].Value.ToString();
            textBox9.Text = dataGridView1.Rows[i].Cells[8].Value.ToString();
            textBox10.Text = dataGridView1.Rows[i].Cells[9].Value.ToString();
            comboBox1.Text = dataGridView1.Rows[i].Cells[4].Value.ToString();
            textBox2.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
            dateTimePicker1.Value = Convert.ToDateTime(dataGridView1.Rows[i].Cells[5].Value);
            textBox5.Text = dataGridView1.Rows[i].Cells[10].Value.ToString();


        }

        private void button5_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlTransaction transaction = con.BeginTransaction();
            try
            {
                string arsiv = "insert into arsiv (dergi_id) select dergi_id from dergiler where dergi_id=@dergi_id";
                SqlCommand cmd = new SqlCommand(arsiv, con, transaction);
                cmd.Parameters.AddWithValue("@dergi_id", textBox5.Text);
                cmd.ExecuteNonQuery();

                string degis = "update dergiler set durumm= 'arsiv' where dergi_id=@dergi_id";
                SqlCommand cmd2 = new SqlCommand(degis, con, transaction);
                cmd2.Parameters.AddWithValue("@dergi_id", textBox5.Text);
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

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Form3 panasayfa = new Form3();
            panasayfa.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            string arama = "SELECT  d.adi, d.issn as ISSN, d.yayinci AS Yayıncı, d.yazar AS Yazarı, t.dergitüradı AS Türü, d.baslangic_tarihi as BaşlangıçTarihi, d.cilt as Cilt, d.sayi as Sayı, d.rafno as RafNo," +
                " d.stoksayisi AS StokSayısı, d.dergi_id as barkodno FROM dergiler d INNER JOIN dergitür t ON d.dergitür_id = t.dergitür_id where durumm='aktif' and adi=@adi ";
            SqlCommand cmd = new SqlCommand(arama, con);

            cmd.Parameters.AddWithValue("@adi", textBox6.Text);
          


            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void textBox6_Click(object sender, EventArgs e)
        {
            textBox6.Clear();
            textBox6.ForeColor = Color.Black;
        }
    }
    
}
