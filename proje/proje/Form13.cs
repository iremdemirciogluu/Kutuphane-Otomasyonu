using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace proje
{
    public partial class Form13 : Form
    {
        public Form13()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-503B5NV\\SQLEXPRESS;Initial Catalog=kutuphaneotomasyonu;Integrated Security=True");

        

        private void kitapgetir()
        {
            con.Open();
            string kitap = "SELECT  barkodno, adi, yazarlar as yazarı, yayinevi, sayfasayisi as sayfasayısı, stoksayisi as adet, rafno from kitaplar where durum = 'aktif' ";
            SqlCommand cmd = new SqlCommand(kitap, con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);  
            dataGridView2.DataSource = dt;
            con.Close();
        }
        private void uyegetir()
        {
            con.Open() ;
            string uye = "select uye_id, ad, soyad, telefon, email from uyeler";
            SqlCommand cmd = new SqlCommand(uye, con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd) ;
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }
        private void Form13_Load(object sender, EventArgs e)
        {
            kitapgetir();
            uyegetir();

            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = dateTimePicker1.Value.AddDays(15);
           
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int i = 0;
            i = e.RowIndex;
            textBox1.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.Rows[i].Cells[3].Value.ToString();
            textBox15.Text = dataGridView1.Rows[i].Cells[4].Value.ToString();
        }

    

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Form3 uanasayfa = new Form3();
            uanasayfa.Show();
            this.Hide();
        }
        

        

        private void textBox12_Click(object sender, EventArgs e)
        {
            textBox12.Clear();
            textBox12.ForeColor = Color.Black;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

            string arama = "SELECT  barkodno, adi, yazarlar as yazarı, yayinevi, sayfasayisi as sayfasayısı, stoksayisi as adet, rafno from"+
                " kitaplar where barkodno=@barkodno";
            SqlCommand cmd = new SqlCommand(arama, con);

           
            cmd.Parameters.AddWithValue("@barkodno", textBox12.Text);


            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView2.DataSource = dt;
            con.Close();
        }

        private void textBox13_Click(object sender, EventArgs e)
        {
            textBox13.Clear();
            textBox13.ForeColor = Color.Black;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            string arama = "select uye_id, ad, soyad, telefon, email from uyeler where ad=@ad or soyad=@soyad";
            SqlCommand cmd = new SqlCommand(arama, con);

            cmd.Parameters.AddWithValue("@ad", textBox13.Text);
            cmd.Parameters.AddWithValue("@soyad", textBox13.Text);


            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void dataGridView2_CellMouseClick_1(object sender, DataGridViewCellMouseEventArgs e)
        {
            int i = 0;
            i = e.RowIndex;
            textBox5.Text = dataGridView2.Rows[i].Cells[0].Value.ToString();
            textBox6.Text = dataGridView2.Rows[i].Cells[1].Value.ToString();
            textBox7.Text = dataGridView2.Rows[i].Cells[2].Value.ToString();
            textBox8.Text = dataGridView2.Rows[i].Cells[3].Value.ToString();
            textBox9.Text = dataGridView2.Rows[i].Cells[4].Value.ToString();         
            textBox11.Text = dataGridView2.Rows[i].Cells[6].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form14 teslimal = new Form14();
            teslimal.Show();
            this.Hide();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            DateTime veris = dateTimePicker1.Value;
            DateTime alis = veris.AddDays(15);


            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Lütfen üye ve kitap bilgilerini giriniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            else
            {
                con.Open();
                string emanet = "insert into odunc (barkodno, uye_id, veris_tarihi, alis_tarihi) values (@barkodno, @uye_id, @veris_tarihi," +
                    " @alis_tarihi)";
                SqlCommand cmd = new SqlCommand(emanet, con);
                cmd.Parameters.AddWithValue("@barkodno", textBox5.Text);
                cmd.Parameters.AddWithValue("@uye_id", textBox1.Text);
                cmd.Parameters.AddWithValue("@veris_tarihi", veris);
                cmd.Parameters.AddWithValue("@alis_tarihi", alis);

                int result = cmd.ExecuteNonQuery();


                if (result > 0)
                {
                    string güncelle = "update kitaplar set stoksayisi = stoksayisi-1" +
                        "where barkodno = @barkodno and stoksayisi >0";
                    SqlCommand komut = new SqlCommand(güncelle, con);

                    komut.Parameters.AddWithValue("barkodno", textBox5.Text);
                    int stokresult = komut.ExecuteNonQuery();

                    if (stokresult > 0)
                    {
                        MessageBox.Show("Emanet işlemi başarıyla gerçekleşti. Lütfen teslim tarihinde getirmeye dikkat edin!");
                    }
                    else
                    {
                        MessageBox.Show("işlem başarısız stokta kitap yok");

                    }

                }
                else
                {
                    MessageBox.Show("Emanet işlemi başarısız oldu ");
                }
            }
            con.Close();
            kitapgetir();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DateTime veris = dateTimePicker1.Value;
            dateTimePicker2.Value = veris.AddDays(15);
        }
    }
}
