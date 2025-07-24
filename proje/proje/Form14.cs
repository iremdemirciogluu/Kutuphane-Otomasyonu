using proje.kutuphaneotomasyonuDataSet1TableAdapters;
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
    public partial class Form14 : Form
    {
        public Form14()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-503B5NV\\SQLEXPRESS;Initial Catalog=kutuphaneotomasyonu;Integrated Security=True");
        private void odunc()
        {
            con.Open();
            string getir = "select o.odunc_id, u.uye_id, k.barkodno, u.ad, u.soyad, k.adi, k.yazarlar, k.yayinevi from odunc o join uyeler u on o.uye_id=u.uye_id join kitaplar k on k.barkodno=o.barkodno where durum = 'aktif' and durumu = 'teslimedilmedi'";
            SqlCommand cmd = new SqlCommand(getir, con);
           
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int i = 0;
            i = e.RowIndex;
            textBox1.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
            
        }

        private void Form14_Load(object sender, EventArgs e)
        {
            odunc();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Form13 emanet = new Form13();
            emanet.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlTransaction transaction = con.BeginTransaction();
            try
            {
                DateTime teslimtarih = dateTimePicker1.Value;
           

                string oduncguncel = "update odunc set alis_tarihi=@alis_tarihi where odunc_id=@odunc_id";
                SqlCommand cmd2 = new SqlCommand(@oduncguncel, con, transaction);
                cmd2.Parameters.AddWithValue("@odunc_id", textBox1.Text);
                cmd2.Parameters.AddWithValue("@alis_tarihi", teslimtarih);
                cmd2.ExecuteNonQuery();

                string uyeguncel = "update uyeler set okunankitapsayısı=okunankitapsayısı+1 where uye_id=@uye_id";
                SqlCommand cmd1 = new SqlCommand(uyeguncel, con, transaction);
                cmd1.Parameters.AddWithValue("@uye_id", textBox2.Text);
                cmd1.ExecuteNonQuery();

                String kitapgunelle = "update kitaplar set stoksayisi= 1 + stoksayisi where barkodno=@barkodno";
                SqlCommand cmd3 = new SqlCommand(kitapgunelle, con, transaction);
                cmd3.Parameters.AddWithValue("@barkodno", textBox3.Text );
                cmd3.ExecuteNonQuery();

                string guncel = "update odunc set durumu='teslimalındı' where odunc_id=@odunc_id";
                SqlCommand cmd4 = new SqlCommand(guncel, con, transaction);
                cmd4.Parameters.AddWithValue("@odunc_id", textBox1.Text);
                cmd4.ExecuteNonQuery();


                transaction.Commit();
                MessageBox.Show("Teslim alma işlemi gerçekleştirildi");

                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();

            }
            catch (Exception ex) {

                transaction.Rollback();
                MessageBox.Show("Bir hata oluştu: " + ex.Message);
            }
            con.Close();
            odunc();
        }
    }
}
