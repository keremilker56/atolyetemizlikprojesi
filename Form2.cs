using atolyetemizlikprojesi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace atolyetemizlikprojesi
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            this.Hide();
            form1.ShowDialog();

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
        public void temel_islemler()
        {
            baglanti.Open();
            komut.ExecuteNonQuery();
            listele();
        }
     
        List<string> ogrenciler = new List<string>();

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (baglanti.State != ConnectionState.Open)
                {
                    baglanti.Open();
                }

                if (ogrenciler.Count < 30) // 30 kişilik limit kontrolü düzeltildi
                {
                    komut = new SqlCommand("INSERT INTO ogrenci (ogr_no, ad, soyad, sinif, cinsiyet, telefon) VALUES(@no, @ad, @soyad, @sinif, @cinsiyet, @tel)", baglanti);
        
                    komut.Parameters.AddWithValue("@no", textBox1.Text);
                    komut.Parameters.AddWithValue("@ad", textBox2.Text);
                    komut.Parameters.AddWithValue("@soyad", textBox3.Text);
                    komut.Parameters.AddWithValue("@sinif", comboBox1.Text);
                    komut.Parameters.AddWithValue("@cinsiyet", comboBox2.Text);
                    komut.Parameters.AddWithValue("@tel", maskedTextBox1.Text);

                    int etkilenenSatirlar = komut.ExecuteNonQuery(); // Veri eklendi mi kontrol et

                    if (etkilenenSatirlar > 0) // Öğrenci ekleme başarılı ise devam et
                    {
                        ogrenciler.Add(textBox1.Text);

                        SqlCommand komut1 = new SqlCommand("INSERT INTO temizlikyapmayanogr (ogr_no, ad, soyad, sinif, cinsiyet, telefon) VALUES(@no, @ad, @soyad, @sinif, @cinsiyet, @tel)", baglanti);
        
                        komut1.Parameters.AddWithValue("@no", textBox1.Text);
                        komut1.Parameters.AddWithValue("@ad", textBox2.Text);
                        komut1.Parameters.AddWithValue("@soyad", textBox3.Text);
                        komut1.Parameters.AddWithValue("@sinif", comboBox1.Text);
                        komut1.Parameters.AddWithValue("@cinsiyet", comboBox2.Text);
                        komut1.Parameters.AddWithValue("@tel", maskedTextBox1.Text);

                        komut1.ExecuteNonQuery();


                   

                        listele();


                    }
                    else
                    {
                        MessageBox.Show("Öğrenci ekleme başarısız!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Kontenjan 30 kişilik, fazlasını alamazsın!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata ! : " + ex.Message);
            }
            finally
            {
                if (baglanti.State == ConnectionState.Open)
                {
                    baglanti.Close();
                }
            }
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            maskedTextBox1.Mask = "(999) 000 00 00";
            listele();

        }
        List<int> silineck = new List<int>();

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                silineck.Clear();
                if (baglanti.State != ConnectionState.Open)
                {
                    baglanti.Open();
                }

                SqlCommand komut1 = new SqlCommand("delete from temizlikyapmayanogr where ogr_no=@no", baglanti);
                komut1.Parameters.AddWithValue("@no", dataGridView1.CurrentRow.Cells["ogr_no"].Value.ToString());
                komut1.ExecuteNonQuery();

                using (SqlCommand liste = new SqlCommand("SELECT ogr_no FROM gelenogrenci WHERE ogr_no=@no UNION SELECT ogr_no FROM gelmeyenogrenci WHERE ogr_no=@no",baglanti))//iki listeyi birleştirmek için
                {
                    liste.Parameters.AddWithValue("@no", dataGridView1.CurrentRow.Cells["ogr_no"].Value.ToString());

                    using (SqlDataReader reader = liste.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            silineck.Add(int.Parse(reader["ogr_no"].ToString()));
                        }
                    }
                }

                if (silineck.Count > 0)
                {
                    SqlCommand silGelen = new SqlCommand("delete from gelenogrenci where ogr_no=@no", baglanti);
                    silGelen.Parameters.AddWithValue("@no", silineck[0]);
                    silGelen.ExecuteNonQuery();

                    SqlCommand silGelmeyen = new SqlCommand("delete from gelmeyenogrenci where ogr_no=@no", baglanti);
                    silGelmeyen.Parameters.AddWithValue("@no", silineck[0]);
                    silGelmeyen.ExecuteNonQuery();
                }

                komut = new SqlCommand("delete from ogrenci where ogr_no=@no", baglanti);
                komut.Parameters.AddWithValue("@no", dataGridView1.CurrentRow.Cells["ogr_no"].Value.ToString());
                komut.ExecuteNonQuery();

                listele();
                for (int i = 0; i < ogrenciler.Count; i++)
                {
                    if (ogrenciler[i] == dataGridView1.CurrentRow.Cells["ogr_no"].Value.ToString())
                    {
                        ogrenciler.RemoveAt(i);
                        break; // Listeyi gereksiz tekrar taramamak için döngüyü sonlandırıyoruz
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata! :" + ex.Message);
            }
            finally
            {
                if (baglanti.State == ConnectionState.Open)
                {
                    baglanti.Close();
                }
            }
        }
        List<int> guncellenecek = new List<int>();
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (baglanti.State != ConnectionState.Open)
                {
                    baglanti.Open();
                }
  
                komut = new SqlCommand("update temizlikyapmayanogr set ad=@ad,soyad=@soyad,sinif=@sinif,cinsiyet=@cinsiyet,telefon=@tel where ogr_no=@no", baglanti);
                komut.Parameters.AddWithValue("@no", dataGridView1.CurrentRow.Cells["ogr_no"].Value.ToString());
                komut.Parameters.AddWithValue("@ad", textBox2.Text);
                komut.Parameters.AddWithValue("@soyad", textBox3.Text);
                komut.Parameters.AddWithValue("@sinif", comboBox1.Text);
                komut.Parameters.AddWithValue("@cinsiyet", comboBox2.Text);
                komut.Parameters.AddWithValue("@tel", maskedTextBox1.Text);
                komut.ExecuteNonQuery();





                using (SqlCommand liste = new SqlCommand("SELECT ogr_no FROM gelenogrenci WHERE ogr_no=@no UNION SELECT ogr_no FROM gelmeyenogrenci WHERE ogr_no=@no", baglanti))//iki listeyi birleştirmek için
                {
                    liste.Parameters.AddWithValue("@no", dataGridView1.CurrentRow.Cells["ogr_no"].Value.ToString());

                    using (SqlDataReader reader = liste.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            guncellenecek.Add(int.Parse(reader["ogr_no"].ToString()));
                        }
                    }
                }

                if (guncellenecek.Count > 0)
                {
                    SqlCommand guncelleGelen = new SqlCommand("update temizlikyapmayanogr set ad=@ad,soyad=@soyad,sinif=@sinif,cinsiyet=@cinsiyet,telefon=@tel where ogr_no=@no", baglanti);
                    guncelleGelen.Parameters.AddWithValue("@no", guncellenecek[0]);
                    guncelleGelen.Parameters.AddWithValue("@ad", textBox2.Text);
                    guncelleGelen.Parameters.AddWithValue("@soyad", textBox3.Text);
                    guncelleGelen.Parameters.AddWithValue("@sinif", comboBox1.Text);
                    guncelleGelen.Parameters.AddWithValue("@cinsiyet", comboBox2.Text);
                    guncelleGelen.Parameters.AddWithValue("@tel", maskedTextBox1.Text);
                    guncelleGelen.ExecuteNonQuery();

                    SqlCommand guncelleGelmeyen = new SqlCommand("update temizlikyapmayanogr set ad=@ad,soyad=@soyad,sinif=@sinif,cinsiyet=@cinsiyet,telefon=@tel where ogr_no=@no", baglanti);
                    guncelleGelmeyen.Parameters.AddWithValue("@no", guncellenecek[0]);
                    guncelleGelmeyen.Parameters.AddWithValue("@ad", textBox2.Text);
                    guncelleGelmeyen.Parameters.AddWithValue("@soyad", textBox3.Text);
                    guncelleGelmeyen.Parameters.AddWithValue("@sinif", comboBox1.Text);
                    guncelleGelmeyen.Parameters.AddWithValue("@cinsiyet", comboBox2.Text);
                    guncelleGelmeyen.Parameters.AddWithValue("@tel", maskedTextBox1.Text);
                    guncelleGelmeyen.ExecuteNonQuery();
                }





                komut = new SqlCommand("update ogrenci set ad=@ad,soyad=@soyad,sinif=@sinif,cinsiyet=@cinsiyet,telefon=@tel where ogr_no=@no", baglanti);
                komut.Parameters.AddWithValue("@no", dataGridView1.CurrentRow.Cells["ogr_no"].Value.ToString());
                komut.Parameters.AddWithValue("@ad", textBox2.Text);
                komut.Parameters.AddWithValue("@soyad", textBox3.Text);
                komut.Parameters.AddWithValue("@sinif", comboBox1.Text);
                komut.Parameters.AddWithValue("@cinsiyet", comboBox2.Text);
                komut.Parameters.AddWithValue("@tel", maskedTextBox1.Text);
                komut.ExecuteNonQuery();
                listele();

                if (baglanti.State == ConnectionState.Open)
                {
                    baglanti.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata!: " + ex.Message);
            }
            finally
            {
                baglanti.Close();
            }

        }

    }
}