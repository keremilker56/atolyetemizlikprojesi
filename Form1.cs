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


namespace atolyetemizlikprojesi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-L2IO4EG\\SQLEXPRESS;Initial Catalog=temizlikprogrami;Integrated Security=True;");
        public void listele()
        {
            SqlCommand komut = new SqlCommand("select * from bugununtemizlikcisi", baglanti);
            SqlDataAdapter adapter = new SqlDataAdapter(komut);
            DataTable table = new DataTable();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
            dataGridView1.Columns["ogr_no"].HeaderText = "Öğrenci No";
            dataGridView1.Columns["ad"].HeaderText = "Öğrenci Adı";
            dataGridView1.Columns["soyad"].HeaderText = "Öğrenci Soyadı";
            dataGridView1.Columns["sinif"].HeaderText = "Öğrenci Sınıfı";
            dataGridView1.Columns["cinsiyet"].HeaderText = "Öğrenci Cinsiyeti";
            dataGridView1.Columns["telefon"].HeaderText = "Öğrenci Telefon Numarası";

        }
        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();

            form2.Hide();
            form2.ShowDialog();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();

            form3.Hide();
            form3.ShowDialog();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();

            form4.Hide();
            form4.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            listele();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
