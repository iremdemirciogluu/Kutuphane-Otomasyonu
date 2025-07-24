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
    public partial class Form9 : Form
    {
        public Form9()
        {
            InitializeComponent();


        }
        static string constring = ("Data Source=DESKTOP-503B5NV\\SQLEXPRESS;Initial Catalog=kutuphaneotomasyonu;Integrated Security=True");
        SqlConnection con = new SqlConnection(constring);
        private void kayit()
        {
            con.Open();
            string getir = "select p.populer_id, k.adi, k.yazarlar, k.yayinevi, k.barkodno from populer p join kitaplar k on p.barkodno=k.barkodno ";
            SqlCommand cmd = new SqlCommand(getir, con);
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            dataGridView1.DataSource = dt;

            con.Close();
        }
        private void Form9_Load(object sender, EventArgs e)
        {
            kayit();

            
            
        }  


        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Form3 panasayfa = new Form3();
            panasayfa.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            foreach (DataGridViewRow dr in dataGridView1.SelectedRows)
            {
                int BarkodNo = Convert.ToInt32(dr.Cells[0].Value);
                verisil(BarkodNo);
                kayit();

            }
        }
        public void verisil(int populer_id)
        {
            con.Open();
            string sil = "Delete from populer where populer_id=@populer_id";
            SqlCommand cmd = new SqlCommand(sil, con);
            cmd.Parameters.AddWithValue("@populer_id", populer_id);
            cmd.ExecuteNonQuery();
            con.Close();
        }
       
     
    }
}
