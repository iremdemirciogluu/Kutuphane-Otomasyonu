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
    public partial class Form12 : Form
    {
        public Form12()
        {
            InitializeComponent();
        }
        static string constring = ("Data Source=DESKTOP-503B5NV\\SQLEXPRESS;Initial Catalog=kutuphaneotomasyonu;Integrated Security=True");
        SqlConnection con = new SqlConnection(constring);
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (con.State == ConnectionState.Closed) ;
                {
                    con.Open();
                    string kayit = "insert into personel (ad, soyad, email, telefon, kullanıcı_adı, sifre) values  (@ad, @soyad, @email, @telefon, @kullanıcı_adı, @sifre)";
                    SqlCommand cmd = new SqlCommand(kayit, con);
                    cmd.Parameters.AddWithValue("@ad", textBox5.Text);
                    cmd.Parameters.AddWithValue("@soyad", textBox4.Text);
                    cmd.Parameters.AddWithValue("@email", textBox3.Text);
                    cmd.Parameters.AddWithValue("@telefon", textBox2.Text);
                    cmd.Parameters.AddWithValue("@kullanıcı_adı", textBox1.Text);
                    cmd.Parameters.AddWithValue("@sifre", textBox6.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Personel kayıt işlemi başarıyla tamamlanmıştır");
                    con.Close();
                    Form11 uyekayıt = new Form11();
                    uyekayıt.Show();
                    this.Hide();
                }
            }


            catch (Exception ex)
            {


                MessageBox.Show("bir hata var" + ex.Message);




            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Form11 uyeler = new Form11();
            uyeler.Show();
            this.Hide();
        }
    }
}
