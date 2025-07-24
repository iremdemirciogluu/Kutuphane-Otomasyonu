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
    public partial class Form10 : Form
    {
        private int uyeid;
        private BindingManagerBase bm;
        private DataTable dt;
        public Form10(int uyeid)
        {
            InitializeComponent();
            this.uyeid = uyeid;
        }
        static string constring = ("Data Source=DESKTOP-503B5NV\\SQLEXPRESS;Initial Catalog=kutuphaneotomasyonu;Integrated Security=True");
        SqlConnection con = new SqlConnection(constring);

        private void Form10_Load(object sender, EventArgs e)
        {
            try
            {
                con.Open();

                string getir = "select k.adi, k.yazarlar, t.kitapturadi, k.rafno, k.stoksayisi from populer p join kitaplar k on p.barkodno=k.barkodno join kitaptür t on k.tur_id=t.tur_id ";
                SqlCommand cmd = new SqlCommand(getir, con);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                 dt = new DataTable();
                adapter.Fill(dt);

                textBox1.DataBindings.Add("text", dt, "adi");
                textBox2.DataBindings.Add("text", dt, "yazarlar");
                textBox3.DataBindings.Add("text", dt, "kitapturadi");
                textBox4.DataBindings.Add("text", dt, "rafno");
                textBox5.DataBindings.Add("text", dt, "stoksayisi");

                bm = this.BindingContext[dt];
                con.Close();



            }
            catch (Exception ex)
            {

                MessageBox.Show("HATA" + ex.Message);

            }


        }
     
  

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Form2 uanasayfa = new Form2( uyeid);
            uanasayfa.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(bm.Position < dt.Rows.Count - 1)
            {
                bm.Position += 1;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (bm.Position > 0)
            {
                bm.Position -= 1;
            }
        }
    }
}
