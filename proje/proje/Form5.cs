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
using System.Data.SqlClient;

namespace proje
{
    public partial class Form5 : Form

    {
        
        public Form5()
        {
            InitializeComponent();
        }
        static string constring = ("Data Source=DESKTOP-503B5NV\\SQLEXPRESS;Initial Catalog=kutuphaneotomasyonu;Integrated Security=True");
        SqlConnection con = new SqlConnection(constring);

        private void button1_Click(object sender, EventArgs e)
        {

            string giris = "select * from personel where kullanıcı_adı='" + textBox1.Text + "'and sifre='" + textBox2.Text + "'";

            SqlCommand cmd = new SqlCommand(giris, con);

            con.Open();
            SqlDataReader dr;

            dr = cmd.ExecuteReader();

            if (dr.Read())
            {

                Form3 personel = new Form3();
                personel.Show();
                this.Hide();
            }

            else
            {
                MessageBox.Show("Hatalı şifre veya kullanıcı adı");
            }
            con.Close();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Form1 giris = new Form1();
            giris.Show();
            this.Hide();
        }
    }
 }

