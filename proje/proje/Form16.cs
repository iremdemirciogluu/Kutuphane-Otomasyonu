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
    public partial class Form16 : Form
    {
        private int uyeid;
        public Form16(int uyeid)
        {
            InitializeComponent();
            this.uyeid = uyeid;
        }
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-503B5NV\\SQLEXPRESS;Initial Catalog=kutuphaneotomasyonu;Integrated Security=True");

        private void Form16_Load(object sender, EventArgs e)
        {
            con.Open();
            string x = "select k.adi as kitapadi, k.yazarlar as yazari, k.yayinevi, o.alis_tarihi as AlisTarihi, o.veris_tarihi as TeslimTarihi from kitaplar k join odunc o on o.barkodno=k.barkodno where o.uye_id=@uye_id";
            SqlCommand cmd = new SqlCommand(x, con);
            cmd.Parameters.AddWithValue("@uye_id", uyeid);

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
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
    }
}
