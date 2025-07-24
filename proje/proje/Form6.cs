using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace proje
{
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }
        static string constring = ("Data Source=DESKTOP-503B5NV\\SQLEXPRESS;Initial Catalog=kutuphaneotomasyonu;Integrated Security=True");
        SqlConnection con = new SqlConnection(constring);




        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (con.State == ConnectionState.Closed) 
                {
                    con.Open();
                    string kayit = "insert into kitaplar (adi, yazarlar, yayinevi, BarkodNo, stoksayisi, tur_id, sayfasayisi, rafNo, açiklama, puan )" +
                        "values  (@adi, @yazarlar, @yayinevi, @BarkodNo, @stoksayisi, @tur_id, @sayfasayisi, @rafNo, @açiklama, @puan)";
                    SqlCommand cmd = new SqlCommand(kayit, con);
                    cmd.Parameters.AddWithValue("@adi", textBox5.Text);
                    cmd.Parameters.AddWithValue("@yazarlar", textBox4.Text);
                    cmd.Parameters.AddWithValue("@yayinevi", textBox3.Text);
                    cmd.Parameters.AddWithValue("@BarkodNo", textBox2.Text);
                    cmd.Parameters.AddWithValue("@stoksayisi", textBox1.Text);
                    cmd.Parameters.AddWithValue("@tur_id", comboBox1.SelectedValue);
                    cmd.Parameters.AddWithValue("@sayfasayisi", textBox8.Text);
                    cmd.Parameters.AddWithValue("@rafNo", textBox9.Text);
                    cmd.Parameters.AddWithValue("@açiklama", textBox10.Text);
                    cmd.Parameters.AddWithValue("@puan", textBox11.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Kitap ekleme işlemi tamamlanmıştır");
                    con.Close();
                    
                }
            }


            catch (Exception ex)
            {


                MessageBox.Show("bir hata var" + ex.Message);
                con.Close();

            }
        }
        private void turyukle()
        {

     
                con.Open();
                string tablo = "select tur_id, KitapTurAdi from kitaptür ";
                SqlDataAdapter adapter = new SqlDataAdapter(tablo, con);
                DataTable turtablo = new DataTable();
                adapter.Fill(turtablo);
               
                comboBox1.DataSource = turtablo;
                comboBox1.DisplayMember = "KitapTurAdi";
                comboBox1.ValueMember = "tur_id";
      

                con.Close();
            

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
        public void kayit_getir()
        {
            con.Open();
            string getir = "SELECT  k.adi AS adi, k.yazarlar AS Yazar, k.yayinevi AS Yayinevi,  k.BarkodNo,   k.stoksayisi AS StokSayisi, t.KitapTurAdi AS Tur, k.sayfasayisi AS SayfaSayisi,   k.rafNo AS RafNo,  k.puan AS Puan, k.açiklama AS Aciklama FROM kitaplar k INNER JOIN kitaptür t ON k.tur_id = t.tur_id;";
            SqlCommand cmd = new SqlCommand(getir, con);
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            dataGridView1.DataSource = dt;

            con.Close();

        }
        private void button5_Click(object sender, EventArgs e)
        {
            kayit_getir();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
// TODO: Bu kod satırı 'kutuphaneotomasyonuDataSet.kitaptür' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
this.kitaptürTableAdapter.Fill(this.kutuphaneotomasyonuDataSet.kitaptür);

        }
    }
}
