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
    public partial class Form20 : Form
    {
        private int uyeid;
        public Form20(int uyeid)
        {
            InitializeComponent();
            this.uyeid = uyeid;
        }

        static string constring = ("Data Source=DESKTOP-503B5NV\\SQLEXPRESS;Initial Catalog=kutuphaneotomasyonu;Integrated Security=True");
        SqlConnection con = new SqlConnection(constring);

        private void Form20_Load(object sender, EventArgs e)
        {
            con.Open();
            string getir = "select d.adi, d.issn as ISSN, d.yazar, d.yayinci, t.dergitüradı, d.dergi_id as barkodno, d.cilt, d.sayi, d.baslangic_tarihi, " +
                "d.rafno, d.stoksayisi from dergiler d join dergitür t on d.dergitür_id=t.dergitür_id";

            SqlCommand cm = new SqlCommand(getir, con);

            SqlDataAdapter adapter = new SqlDataAdapter(cm);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            dataGridView1.DataSource = dt;
            con.Close();

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
             Form2 uanasayfa = new Form2(uyeid);
            uanasayfa.Show();
            this.Hide();

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            string arama = "select d.adi, d.issn as ISSN, d.yazar, d.yayinci, t.dergitüradı, d.dergi_id as barkodno, d.cilt, d.sayi, d.baslangic_tarihi, d.rafno, d.stoksayisi from dergiler" +
                " d join dergitür t on d.dergitür_id=t.dergitür_id where adi=@adi or yazar=@yazarlar";
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
    }
}
