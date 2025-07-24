using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace proje
{
    public partial class Form15 : Form
    {
        public Form15()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Form3 anasayfa = new Form3();
            anasayfa.Show();
            this.Hide();
        }
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-503B5NV\\SQLEXPRESS;Initial Catalog=kutuphaneotomasyonu;Integrated Security=True");
       

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int i = 0;
            i = e.RowIndex;
           
            textBox2.Text = dataGridView1.Rows[i].Cells[4].Value.ToString();
            textBox6.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
        }

        private void Form15_Load(object sender, EventArgs e)
        {
            kayitgetir();
            getir();
        }
        private void kayitgetir()
        {
           
            string getir = "select a.arsiv_id, k.adi, k.yazarlar, k.yayinevi, k.barkodno from arsiv a " +
                "join kitaplar k on k.barkodno=a.barkodno  ";
            SqlCommand cmd = new SqlCommand(getir, con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            dataGridView1.DataSource = dt;
           
            
            textBox2.Clear();
            textBox6.Clear();

        }
        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlTransaction transaction = con.BeginTransaction();

            DialogResult sonuc = MessageBox.Show("Arşivden çıkartmak istediğinize emin misiniz?", "ONAY", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (sonuc == DialogResult.Yes)
            {

                try
                {
                    string aktif = "update kitaplar set durum = 'aktif' where barkodno=@barkodno";
                    SqlCommand cmd = new SqlCommand(aktif, con, transaction);
                    cmd.Parameters.AddWithValue("@barkodno", textBox2.Text);
                    cmd.ExecuteNonQuery();


                    string arsivsil = "delete from arsiv where arsiv_id=@arsiv_id";
                    SqlCommand cmd2 = new SqlCommand(arsivsil, con, transaction);
                    cmd2.Parameters.AddWithValue("@arsiv_id", textBox6.Text);
                    cmd2.ExecuteNonQuery();

                    transaction.Commit();

                    MessageBox.Show("Arşivden çıkartıldı");


                }
                catch (Exception ex)
                {

                    transaction.Rollback();
                    MessageBox.Show("HATA" + ex.Message);

                }
            }
            kayitgetir();
            con.Close();
            

        }

        private void getir()
        {
          
           
            string getir = "select a.arsiv_id, d.dergi_id as barkodno, d.adi, d.yazar, d.yayinci, d.issn from arsiv a join dergiler d on d.dergi_id = a.dergi_id ";
            SqlCommand cmd = new SqlCommand(getir, con);

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            dataGridView2.DataSource = dt;

            textBox2.Clear();
            textBox6.Clear();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlTransaction transaction = con.BeginTransaction();

            DialogResult sonuc = MessageBox.Show("Arşivden çıkartmak istediğinize emin misiniz?", "ONAY", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (sonuc == DialogResult.Yes)
            {
                try
                {
                   
                    string getir = "delete from arsiv where arsiv_id=@arsiv_id";
                    SqlCommand cmd = new SqlCommand(getir, con, transaction);
                    cmd.Parameters.AddWithValue("@arsiv_id", textBox6.Text);
                   
                    cmd.ExecuteNonQuery();


                    string güncelle = "update dergiler set durumm='aktif' where dergi_id=@dergi_id";
                    SqlCommand cmd2 = new SqlCommand(güncelle, con, transaction);
                    cmd2.Parameters.AddWithValue("@dergi_id", textBox2.Text);
            
                    cmd2.ExecuteNonQuery();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("HATA" + ex.Message);
                }
                getir();
            }


            else
            {
               

            }
            con.Close();
         }
           
         
        

        private void dataGridView2_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int i = 0;
            i = e.RowIndex;

            textBox2.Text = dataGridView2.Rows[i].Cells[1].Value.ToString();
            textBox6.Text = dataGridView2.Rows[i].Cells[0].Value.ToString();
        }
    }
}
