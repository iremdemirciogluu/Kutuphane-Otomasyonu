using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace proje
{
    public partial class Form1 : Form
    {
        
        
       

        public Form1()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        static string constring = ("Data Source=DESKTOP-503B5NV\\SQLEXPRESS;Initial Catalog=kutuphaneotomasyonu;Integrated Security=True");
        SqlConnection con = new SqlConnection(constring);

        private void button1_Click(object sender, EventArgs e)
        {
            
            string giris = "select * from uyeler where kullanıcı_adı=@kullaniciadi and sifre=sifre";
            
            SqlCommand cmd = new SqlCommand(giris, con);
            cmd.Parameters.AddWithValue("@kullaniciadi", textBox1.Text);
            cmd.Parameters.AddWithValue("@sifre", textBox2.Text);

            con.Open();
            SqlDataReader dr;
            
            dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                int uyeid = Convert.ToInt32(dr["uye_id"]);
                Form2 uye = new Form2(uyeid);
                uye.Show();
                this.Hide();
               
            }

            else
            {
                if (dilIngilizce)
                {
                    MessageBox.Show("username or password is incorrect");
                }
                else
                {
                    MessageBox.Show("Kullanıcı adı veya şifre hatalı");
                }
            }
            con.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form4 kayitform = new Form4();
            kayitform.Show();
            this.Hide();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form5 pkayitform = new Form5();
            pkayitform.Show();
            this.Hide();
        }

        public static bool dilIngilizce = false;
        private void button2_Click(object sender, EventArgs e)
        {
            if (dilIngilizce)
            {
                
                dilIngilizce = false;
                label1.Text = "kullanıcı adı";
                label2.Text = "şifre";
                button1.Text = "GİRİŞ";
                linkLabel1.Text = "üye ol";
                linkLabel2.Text = "personel girişi";
                button2.Text = "Türkçe";

            }
            else 
            {
                dilIngilizce = true;
                label1.Text = "username";
                label2.Text = "password";
                button1.Text = "LOGIN";
                linkLabel1.Text = "sign up";
                linkLabel2.Text = "staff entrance";
                button2.Text = "English";
            }
        }
    }
}
