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
    public partial class Form21 : Form
    {
        public Form21()
        {
            InitializeComponent();
        }
        static string constring = ("Data Source=DESKTOP-503B5NV\\SQLEXPRESS;Initial Catalog=kutuphaneotomasyonu;Integrated Security=True");
        SqlConnection con = new SqlConnection(constring);
        private void Form21_Load(object sender, EventArgs e)
        {
            con.Open();
            string getir = " select k.adi as KitapAdı, count(o.barkodno) as ÖdünçSayısı from kitaplar k left join odunc o on k.barkodno=o.barkodno group by k.adi order by ödünçsayısı desc";
            SqlCommand cmd = new SqlCommand(getir, con);

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            
            dataGridView1.DataSource = dt;
            con.Close();
           
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Form3 panasayfa = new Form3();
            panasayfa.Show();
            this.Hide();
        }

    }
}
