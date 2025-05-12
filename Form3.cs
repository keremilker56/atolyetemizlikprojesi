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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace atolyetemizlikprojesi
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        SqlCommand komut;
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-L2IO4EG\\SQLEXPRESS;Initial Catalog=temizlikprogrami;Integrated Security=True;");
        public void listele()
        {
            komut = new SqlCommand("select * from ogrenci", baglanti);
            SqlDataAdapter adapter = new SqlDataAdapter(komut);
            DataTable table = new DataTable();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
        }
        public void listele2()
        {
            komut = new SqlCommand("select * from gelmeyenogrenci", baglanti);
            SqlDataAdapter adapter = new SqlDataAdapter(komut);
            DataTable table = new DataTable();
            adapter.Fill(table);
            dataGridView2.DataSource = table;
        }
        public void listele3()
        {
            komut = new SqlCommand("select * from gelenogrenci", baglanti);
            SqlDataAdapter adapter = new SqlDataAdapter(komut);
            DataTable table = new DataTable();
            adapter.Fill(table);
            dataGridView3.DataSource = table;
        }
        public void temel_islemler()
        {
            baglanti.Open();
            komut.ExecuteNonQuery();
            listele();
            baglanti.Close();
        }
        private void Form3_Load(object sender, EventArgs e)
        {
            listele();


            listele2();
            listele3();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            this.Hide();
            form1.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            komut = new SqlCommand("insert into gelmeyenogrenci(ogr_no,ad,soyad,sinif,cinsiyet,telefon)values(@no,@ad,@soyad,@sinif,@cinsiyet,@tel)", baglanti);
            komut.Parameters.AddWithValue("@no", dataGridView1.CurrentRow.Cells["ogr_no"].Value.ToString());
            komut.Parameters.AddWithValue("@ad", dataGridView1.CurrentRow.Cells["ad"].Value.ToString());
            komut.Parameters.AddWithValue("@soyad", dataGridView1.CurrentRow.Cells["soyad"].Value.ToString());
            komut.Parameters.AddWithValue("@sinif", dataGridView1.CurrentRow.Cells["sinif"].Value.ToString());
            komut.Parameters.AddWithValue("@cinsiyet", dataGridView1.CurrentRow.Cells["cinsiyet"].Value.ToString());
            komut.Parameters.AddWithValue("@tel", dataGridView1.CurrentRow.Cells["telefon"].Value.ToString());

            SqlCommand komut1 = new SqlCommand("delete from gelenogrenci where ogr_no=@no", baglanti);
            komut1.Parameters.AddWithValue("@no", dataGridView1.CurrentRow.Cells["ogr_no"].Value.ToString());
            //komut1.Parameters.AddWithValue("@ad", dataGridView1.CurrentRow.Cells["ad"].Value.ToString());
            //komut1.Parameters.AddWithValue("@soyad", dataGridView1.CurrentRow.Cells["soyad"].Value.ToString());
            //komut1.Parameters.AddWithValue("@sinif", dataGridView1.CurrentRow.Cells["sinif"].Value.ToString());
            //komut1.Parameters.AddWithValue("@cinsiyet", dataGridView1.CurrentRow.Cells["cinsiyet"].Value.ToString());
            //komut1.Parameters.AddWithValue("@tel", dataGridView1.CurrentRow.Cells["telefon"].Value.ToString());








            baglanti.Open();
            komut.ExecuteNonQuery();
            
            

            komut1.ExecuteNonQuery();
            listele();
            listele2();
            listele3();
            baglanti.Close();
           
            
         
        }

        private void button2_Click(object sender, EventArgs e)
        {
            komut = new SqlCommand("delete from gelmeyenogrenci where ogr_no=@no", baglanti);
            komut.Parameters.AddWithValue("@no", dataGridView2.CurrentRow.Cells["ogr_no"].Value.ToString());
            temel_islemler();
            baglanti.Close();
            listele2();
        }

        private void button4_Click(object sender, EventArgs e)
        {

            komut = new SqlCommand("delete from gelenogrenci where ogr_no=@no", baglanti);
            komut.Parameters.AddWithValue("@no", dataGridView3.CurrentRow.Cells["ogr_no"].Value.ToString());
            temel_islemler();
            baglanti.Close();
            listele3();


        }

        private void button5_Click(object sender, EventArgs e)
        {
            komut = new SqlCommand("insert into gelenogrenci(ogr_no,ad,soyad,sinif,cinsiyet,telefon)values(@no,@ad,@soyad,@sinif,@cinsiyet,@tel)", baglanti);
            komut.Parameters.AddWithValue("@no", dataGridView1.CurrentRow.Cells["ogr_no"].Value.ToString());
            komut.Parameters.AddWithValue("@ad", dataGridView1.CurrentRow.Cells["ad"].Value.ToString());
            komut.Parameters.AddWithValue("@soyad", dataGridView1.CurrentRow.Cells["soyad"].Value.ToString());
            komut.Parameters.AddWithValue("@sinif", dataGridView1.CurrentRow.Cells["sinif"].Value.ToString());
            komut.Parameters.AddWithValue("@cinsiyet", dataGridView1.CurrentRow.Cells["cinsiyet"].Value.ToString());
            komut.Parameters.AddWithValue("@tel", dataGridView1.CurrentRow.Cells["telefon"].Value.ToString());


            SqlCommand komut1 = new SqlCommand("delete from gelmeyenogrenci where ogr_no=@no", baglanti);
            komut1.Parameters.AddWithValue("@no", dataGridView1.CurrentRow.Cells["ogr_no"].Value.ToString());
            //komut1.Parameters.AddWithValue("@ad", dataGridView1.CurrentRow.Cells["ad"].Value.ToString());
            //komut1.Parameters.AddWithValue("@soyad", dataGridView1.CurrentRow.Cells["soyad"].Value.ToString());
            //komut1.Parameters.AddWithValue("@sinif", dataGridView1.CurrentRow.Cells["sinif"].Value.ToString());
            //komut1.Parameters.AddWithValue("@cinsiyet", dataGridView1.CurrentRow.Cells["cinsiyet"].Value.ToString());
            //komut1.Parameters.AddWithValue("@tel", dataGridView1.CurrentRow.Cells["telefon"].Value.ToString());







            baglanti.Open();
            komut.ExecuteNonQuery();
            komut1.ExecuteNonQuery();
            listele();
            listele2();
            listele3();

            baglanti.Close();
           

        }
    }
}
