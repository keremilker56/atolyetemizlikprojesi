using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace atolyetemizlikprojesi
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-L2IO4EG\\SQLEXPRESS;Initial Catalog=temizlikprogrami;Integrated Security=True;");

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            this.Hide();
            form1.ShowDialog();
        }

        public void listele()
        {
            using (SqlCommand komut = new SqlCommand("SELECT * FROM temizlikyapanogr", baglanti))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(komut);
                DataTable table = new DataTable();
                adapter.Fill(table);
                dataGridView1.DataSource = table;
            }
            dataGridView1.Columns["ogr_no"].HeaderText = "Öğrenci No";
            dataGridView1.Columns["ad"].HeaderText = "Öğrenci Adı";
            dataGridView1.Columns["soyad"].HeaderText = "Öğrenci Soyadı";
            dataGridView1.Columns["sinif"].HeaderText = "Öğrenci Sınıfı";
            dataGridView1.Columns["cinsiyet"].HeaderText = "Öğrenci Cinsiyeti";
            dataGridView1.Columns["telefon"].HeaderText = "Öğrenci Telefon Numarası";
        }

        public void listele2()
        {
            using (SqlCommand komut = new SqlCommand("SELECT * FROM temizlikyapacakogr", baglanti))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(komut);
                DataTable table = new DataTable();
                adapter.Fill(table);
                dataGridView2.DataSource = table;
            }
            dataGridView2.Columns["ogr_no"].HeaderText = "Öğrenci No";
            dataGridView2.Columns["ad"].HeaderText = "Öğrenci Adı";
            dataGridView2.Columns["soyad"].HeaderText = "Öğrenci Soyadı";
            dataGridView2.Columns["sinif"].HeaderText = "Öğrenci Sınıfı";
            dataGridView2.Columns["cinsiyet"].HeaderText = "Öğrenci Cinsiyeti";
            dataGridView2.Columns["telefon"].HeaderText = "Öğrenci Telefon Numarası";
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            listele();
            listele2();
        }

      

        private void timer1_Tick(object sender, EventArgs e)
        {

        }
        List<string> ogrenciler = new List<string>();
        List<string> ogrenciler2 = new List<string>();
        List<string> ogrenciler3 = new List<string>();
        List<string> ogrencilerkont = new List<string>();
        List<string> yapmayan = new List<string>();
        List<string> yapmayanbil = new List<string>();






        DateTime başlangic = new DateTime(2025, 5, 15);
        int i = 1;
        int k = 0;
        private void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            try
            {
                
                if (ogrenciler.Count > 0)
                {
                    ogrenciler.Clear();

                }
                if (ogrenciler2.Count > 0)
                {
                    ogrenciler2.Clear();

                }
                if (ogrenciler3.Count > 0)
                {
                    ogrenciler3.Clear();

                }
                if (ogrencilerkont.Count > 0)
                {
                    ogrencilerkont.Clear();

                }
                if (yapmayan.Count > 0)
                {
                    yapmayan.Clear();

                }
                if (yapmayanbil.Count > 0)
                {
                    yapmayanbil.Clear();

                }



                if (baglanti.State == ConnectionState.Closed)
                    baglanti.Open();


                //Öğrencileri temizlikyapacakogr tablosundan al
                SqlCommand Silinecek = new SqlCommand("delete from bugununtemizlikcisi", baglanti);
                Silinecek.ExecuteNonQuery();

                using (SqlCommand komut = new SqlCommand("SELECT ogr_no FROM temizlikyapacakogr ORDER BY ogr_no ASC", baglanti))///////////////
                {
                    using (SqlDataReader reader = komut.ExecuteReader())
                    {
                        while (reader.Read())
                            ogrenciler.Add(reader["ogr_no"].ToString());
                        reader.Close();
                    }
                }
              
                int j = 0;
                if (j < ogrenciler.Count)
                {
                    using (SqlCommand komut = new SqlCommand("SELECT ogr_no FROM gelenogrenci where ogr_no = @no", baglanti))
                    {
                        komut.Parameters.AddWithValue("@no", ogrenciler[j]);
                        j++;
                        

                        using (SqlDataReader reader = komut.ExecuteReader())
                        {
                            while (reader.Read())
                                ogrencilerkont.Add(reader["ogr_no"].ToString());
                            reader.Close();
                        }
                    }
                }
            
                ogrenciler.Clear();
                for (k = 0; k < ogrencilerkont.Count; k++)
                {
                    ogrenciler.Add(ogrencilerkont[k]);
                }
            
       
             

                //Eğer önceki liste boşsa ogrenci tablosundan al

                if (ogrenciler.Count == 0)
                {
                    using (SqlCommand komut = new SqlCommand("SELECT ogr_no FROM ogrenci ORDER BY ogr_no ASC", baglanti))
                    {
                        using (SqlDataReader reader = komut.ExecuteReader())
                        {
                            while (reader.Read())
                                ogrenciler.Add(reader["ogr_no"].ToString());
                            reader.Close();
                        }
                    }
                  

                    if (ogrenciler.Count == 0)
                        return;
                }
                else
                {
                    using (SqlCommand yapacaksil = new SqlCommand("delete from temizlikyapacakogr where ogr_no=@no", baglanti))
                    {
                        yapacaksil.Parameters.AddWithValue("@no", ogrenciler[0]);
                        yapacaksil.ExecuteNonQuery();
                        
                        
                    }
                    listele2();
                }





                if (ogrenciler.Count == 0)
                {
                    MessageBox.Show("Temizlik yapacak öğrenci listesi boş.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (SqlCommand komut = new SqlCommand("SELECT ogr_no FROM temizlikyapmayanogr ORDER BY ogr_no ASC", baglanti))///////////////
                {
                    using (SqlDataReader reader = komut.ExecuteReader())
                    {
                        while (reader.Read())
                            yapmayan.Add(reader["ogr_no"].ToString());
                        reader.Close();
                    }
                }







                if(yapmayan.Count == 0)
                {
                    using (SqlCommand sil = new SqlCommand("DELETE FROM temizlikyapacakogr", baglanti))
                    {
                        sil.ExecuteNonQuery();
                    }

                    using (SqlCommand sil = new SqlCommand("DELETE FROM temizlikyapanogr", baglanti))
                    {
                        sil.ExecuteNonQuery();
                    }

                    for (int m = 0; m < ogrenciler.Count; m++)
                    {
                        using (SqlCommand komut = new SqlCommand("SELECT * FROM ogrenci order by ogr_no asc", baglanti))
                        {
                            using (SqlDataReader reader = komut.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    yapmayanbil.Clear();
                                    yapmayanbil.Add(reader["ogr_no"].ToString());
                                    yapmayanbil.Add(reader["ad"].ToString());
                                    yapmayanbil.Add(reader["soyad"].ToString());
                                    yapmayanbil.Add(reader["sinif"].ToString());
                                    yapmayanbil.Add(reader["cinsiyet"].ToString());
                                    yapmayanbil.Add(reader["telefon"].ToString());


                                }
                                reader.Close();
                            }

                        }
                        using (SqlCommand komut = new SqlCommand("insert into temizlikyapmayanogr(ogr_no, ad, soyad, sinif, cinsiyet, telefon) VALUES (@no, @ad, @soyad, @sinif, @cinsiyet, @tel)", baglanti))
                        {
                            komut.Parameters.AddWithValue("@no", yapmayanbil[0]);
                            komut.Parameters.AddWithValue("@ad", yapmayanbil[1]);
                            komut.Parameters.AddWithValue("@soyad", yapmayanbil[2]);
                            komut.Parameters.AddWithValue("@sinif", yapmayanbil[3]);
                            komut.Parameters.AddWithValue("@cinsiyet", yapmayanbil[4]);
                            komut.Parameters.AddWithValue("@tel", yapmayanbil[5]);
                            komut.ExecuteNonQuery();
                        }
                    }
             
                    

                    listele();
                    listele2();

                }





                int aralik = (int)(DateTime.Now - başlangic).TotalDays % ogrenciler.Count;
                string bugununtemizlikcisi = ogrenciler[aralik];

                //Öğrencinin bilgilerini al
                using (SqlCommand komut1 = new SqlCommand("SELECT * FROM ogrenci WHERE ogr_no = @no", baglanti))
                {
                    komut1.Parameters.AddWithValue("@no", int.Parse(bugununtemizlikcisi));
                    using (SqlDataReader okuma1 = komut1.ExecuteReader())
                    {
                        if (okuma1.Read())
                        {
                            ogrenciler2.Add(okuma1["ogr_no"].ToString());
                            ogrenciler2.Add(okuma1["ad"].ToString());
                            ogrenciler2.Add(okuma1["soyad"].ToString());
                            ogrenciler2.Add(okuma1["sinif"].ToString());
                            ogrenciler2.Add(okuma1["cinsiyet"].ToString());
                            ogrenciler2.Add(okuma1["telefon"].ToString());
                        }
                        okuma1.Close();
                    }
                }

                //Gelmeyen mi kontrolü
                using (SqlCommand gelmeyenibelirle = new SqlCommand("SELECT ogr_no FROM gelmeyenogrenci WHERE ogr_no=@no", baglanti))
                {
                    gelmeyenibelirle.Parameters.AddWithValue("@no", int.Parse(bugununtemizlikcisi));
                    using (SqlDataReader reader = gelmeyenibelirle.ExecuteReader())
                    {
                        if (reader.Read() && reader["ogr_no"] != DBNull.Value)
                        {
                            MessageBox.Show("Bugünün temizlikçisi bugün gelmemiş", "Bilgilendirme", MessageBoxButtons.OKCancel);
                            ogrenciler3.Add(reader["ogr_no"].ToString());
                        }
                        else
                        {
                            MessageBox.Show("Bugünün temizlikçisi bugün gelmiş", "Bilgilendirme", MessageBoxButtons.OKCancel);
                        }
                        reader.Close();
                    }
                }

                if (ogrenciler3.Count > 0)
                {
                    if (ogrenciler2.Count > 0 && ogrenciler2[0] != ogrenciler3[0])
                    {
                        //Temizlik yapan ogrenci ekle

                        label4.Text = ogrenciler2[1].ToString();


                        using (SqlCommand ekle = new SqlCommand("INSERT INTO  bugununtemizlikcisi(ogr_no, ad, soyad, sinif, cinsiyet, telefon) VALUES (@ogr_no, @ad, @soyad, @sinif, @cinsiyet, @telefon)", baglanti))
                        {
                            ekle.Parameters.AddWithValue("@ogr_no", ogrenciler2[0]);
                            ekle.Parameters.AddWithValue("@ad", ogrenciler2[1]);
                            ekle.Parameters.AddWithValue("@soyad", ogrenciler2[2]);
                            ekle.Parameters.AddWithValue("@sinif", ogrenciler2[3]);
                            ekle.Parameters.AddWithValue("@cinsiyet", ogrenciler2[4]);
                            ekle.Parameters.AddWithValue("@telefon", ogrenciler2[5]);
                            ekle.ExecuteNonQuery();
                        }
                        using (SqlCommand ekle = new SqlCommand("INSERT INTO temizlikyapanogr (ogr_no, ad, soyad, sinif, cinsiyet, telefon) VALUES (@ogr_no, @ad, @soyad, @sinif, @cinsiyet, @telefon)", baglanti))
                        {
                            ekle.Parameters.AddWithValue("@ogr_no", ogrenciler2[0]);
                            ekle.Parameters.AddWithValue("@ad", ogrenciler2[1]);
                            ekle.Parameters.AddWithValue("@soyad", ogrenciler2[2]);
                            ekle.Parameters.AddWithValue("@sinif", ogrenciler2[3]);
                            ekle.Parameters.AddWithValue("@cinsiyet", ogrenciler2[4]);
                            ekle.Parameters.AddWithValue("@telefon", ogrenciler2[5]);
                            ekle.ExecuteNonQuery();
                        }
        


                        using (SqlCommand sil = new SqlCommand("DELETE FROM temizlikyapmayanogr WHERE ogr_no=@no", baglanti))
                        {
                            sil.Parameters.AddWithValue("@no", ogrenciler2[0]);
                            sil.ExecuteNonQuery();
                        }

                        using (SqlCommand sil = new SqlCommand("DELETE FROM temizlikyapacakogr WHERE ogr_no=@no", baglanti))
                        {
                            sil.Parameters.AddWithValue("@no", ogrenciler2[0]);
                            sil.ExecuteNonQuery();
                        }
                        listele();
                        listele2();
                    }
                    else
                    {
                        using (SqlCommand ekle = new SqlCommand("INSERT INTO temizlikyapacakogr (ogr_no, ad, soyad, sinif, cinsiyet, telefon) VALUES (@ogr_no, @ad, @soyad, @sinif, @cinsiyet, @telefon)", baglanti))
                        {
                            ekle.Parameters.AddWithValue("@ogr_no", ogrenciler2[0]);
                            ekle.Parameters.AddWithValue("@ad", ogrenciler2[1]);
                            ekle.Parameters.AddWithValue("@soyad", ogrenciler2[2]);
                            ekle.Parameters.AddWithValue("@sinif", ogrenciler2[3]);
                            ekle.Parameters.AddWithValue("@cinsiyet", ogrenciler2[4]);
                            ekle.Parameters.AddWithValue("@telefon", ogrenciler2[5]);
                            ekle.ExecuteNonQuery();
                        }

                        for (i = 1; i < ogrenciler.Count; i++)
                        {

                            if (i == ogrenciler.Count)
                            {
                                MessageBox.Show("Öğrenciler gelmemiş");
                                break;
                            }
                            else
                            {
                                int yeniIndeks = (int)(aralik + i) % ogrenciler.Count;

                                string yeniOgrenci = ogrenciler[yeniIndeks];

                               
                                using (SqlCommand gelmeyenibelirle = new SqlCommand("SELECT * FROM gelmeyenogrenci WHERE ogr_no=@no", baglanti))
                                {
                                    gelmeyenibelirle.Parameters.AddWithValue("@no", int.Parse(yeniOgrenci));
                                    using (SqlDataReader reader = gelmeyenibelirle.ExecuteReader())
                                    {
                                        ogrenciler3.Clear();
                                        if (reader.Read())
                                        {
                                            ogrenciler3.Clear();
                                            MessageBox.Show("Bugünün temizlikçisi bugün gelmemiş", "Bilgilendirme", MessageBoxButtons.OKCancel);
                                            ogrenciler3.Add(reader["ogr_no"].ToString());
                                            ogrenciler3.Add(reader["ad"].ToString());
                                            ogrenciler3.Add(reader["soyad"].ToString());
                                            ogrenciler3.Add(reader["sinif"].ToString());
                                            ogrenciler3.Add(reader["cinsiyet"].ToString());
                                            ogrenciler3.Add(reader["telefon"].ToString());

                                            

                                        }

                                        else
                                        {
                                            MessageBox.Show("Öteki temizlikçi bugün gelmiş", "Bilgilendirme", MessageBoxButtons.OKCancel);
                                        }
                                        reader.Close();

                                    }
                                }
                                if (ogrenciler3.Count == 6)
                                {
                                    SqlCommand Yapacakgelmediyse = new SqlCommand("insert into temizlikyapacakogr (ogr_no, ad, soyad, sinif, cinsiyet, telefon) VALUES (@no, @ad, @soyad, @sinif, @cinsiyet, @telefon)", baglanti);
                                    Yapacakgelmediyse.Parameters.AddWithValue("@no", ogrenciler3[0]);
                                    Yapacakgelmediyse.Parameters.AddWithValue("@ad", ogrenciler3[1]);
                                    Yapacakgelmediyse.Parameters.AddWithValue("@soyad", ogrenciler3[2]);
                                    Yapacakgelmediyse.Parameters.AddWithValue("@sinif", ogrenciler3[3]);
                                    Yapacakgelmediyse.Parameters.AddWithValue("@cinsiyet", ogrenciler3[4]);
                                    Yapacakgelmediyse.Parameters.AddWithValue("@telefon", ogrenciler3[5]);
                                    Yapacakgelmediyse.ExecuteNonQuery();
                                    label4.Text = ogrenciler3[1].ToString() + " Adlı temizlik yapacak öğrenci bugün gelmemiş";





                                    listele();
                                    listele2();




                                    continue;
                                }


                                // Yeni temizlikçi bilgisi alımı
                                using (SqlCommand komut1 = new SqlCommand("SELECT * FROM ogrenci WHERE ogr_no = @no", baglanti))
                                    {
                                        komut1.Parameters.AddWithValue("@no", int.Parse(yeniOgrenci));
                                        using (SqlDataReader okuma1 = komut1.ExecuteReader())
                                        {
                                            if (okuma1.Read())
                                            {
                                                ogrenciler2.Clear();
                                                ogrenciler2.Add(okuma1["ogr_no"].ToString());
                                                ogrenciler2.Add(okuma1["ad"].ToString());
                                                ogrenciler2.Add(okuma1["soyad"].ToString());
                                                ogrenciler2.Add(okuma1["sinif"].ToString());
                                                ogrenciler2.Add(okuma1["cinsiyet"].ToString());
                                                ogrenciler2.Add(okuma1["telefon"].ToString());
                                            }
                                            okuma1.Close(); 
                                        }
                                    }

                                //  Ternary oparatörü : string tablo = (result == DialogResult.Yes) ? "temizlikyapanogr" : "temizlikyapacakogr";//- eğer result değişkeni DialogResult.Yes ise, tablo değişkenine "temizlikyapanogr" değeri atanır.-Aksi takdirde, yani result değeri DialogResult.Yes değilse, tablo değişkenine "temizlikyapacakogr" atanır.

                                label4.Text = ogrenciler2[1].ToString();
                                using (SqlCommand ekle = new SqlCommand("INSERT INTO  bugununtemizlikcisi(ogr_no, ad, soyad, sinif, cinsiyet, telefon) VALUES (@ogr_no, @ad, @soyad, @sinif, @cinsiyet, @telefon)", baglanti))
                                {
                                    ekle.Parameters.AddWithValue("@ogr_no", ogrenciler2[0]);
                                    ekle.Parameters.AddWithValue("@ad", ogrenciler2[1]);
                                    ekle.Parameters.AddWithValue("@soyad", ogrenciler2[2]);
                                    ekle.Parameters.AddWithValue("@sinif", ogrenciler2[3]);
                                    ekle.Parameters.AddWithValue("@cinsiyet", ogrenciler2[4]);
                                    ekle.Parameters.AddWithValue("@telefon", ogrenciler2[5]);
                                    ekle.ExecuteNonQuery();
                                }
                                using (SqlCommand ekle = new SqlCommand("INSERT INTO  temizlikyapanogr(ogr_no, ad, soyad, sinif, cinsiyet, telefon) VALUES (@ogr_no, @ad, @soyad, @sinif, @cinsiyet, @telefon)", baglanti))
                                {
                                        ekle.Parameters.AddWithValue("@ogr_no", ogrenciler2[0]);
                                        ekle.Parameters.AddWithValue("@ad", ogrenciler2[1]);
                                        ekle.Parameters.AddWithValue("@soyad", ogrenciler2[2]);
                                        ekle.Parameters.AddWithValue("@sinif", ogrenciler2[3]);
                                        ekle.Parameters.AddWithValue("@cinsiyet", ogrenciler2[4]);
                                        ekle.Parameters.AddWithValue("@telefon", ogrenciler2[5]);
                                        ekle.ExecuteNonQuery();
                                }
                     
                                using (SqlCommand sil = new SqlCommand("DELETE FROM temizlikyapmayanogr WHERE ogr_no=@no", baglanti))
                                {
                                        sil.Parameters.AddWithValue("@no", ogrenciler2[0]);
                                        sil.ExecuteNonQuery();
                                }
                                using (SqlCommand yapacaksil = new SqlCommand("DELETE FROM temizlikyapacakogr WHERE ogr_no=@no", baglanti))
                                {
                                        yapacaksil.Parameters.AddWithValue("@no", ogrenciler2[0]);
                                        yapacaksil.ExecuteNonQuery();
                                }
                                break;
                                

                            }

                        }
                    }

                    listele();
                    listele2();
                }




                else
                {

                    label4.Text = ogrenciler2[1].ToString();

                    using (SqlCommand ekle = new SqlCommand("INSERT INTO  bugununtemizlikcisi(ogr_no, ad, soyad, sinif, cinsiyet, telefon) VALUES (@ogr_no, @ad, @soyad, @sinif, @cinsiyet, @telefon)", baglanti))
                    {
                        ekle.Parameters.AddWithValue("@ogr_no", ogrenciler2[0]);
                        ekle.Parameters.AddWithValue("@ad", ogrenciler2[1]);
                        ekle.Parameters.AddWithValue("@soyad", ogrenciler2[2]);
                        ekle.Parameters.AddWithValue("@sinif", ogrenciler2[3]);
                        ekle.Parameters.AddWithValue("@cinsiyet", ogrenciler2[4]);
                        ekle.Parameters.AddWithValue("@telefon", ogrenciler2[5]);
                        ekle.ExecuteNonQuery();
                    }

                    using (SqlCommand ekle = new SqlCommand("INSERT INTO temizlikyapanogr (ogr_no, ad, soyad, sinif, cinsiyet, telefon) VALUES (@ogr_no, @ad, @soyad, @sinif, @cinsiyet, @telefon)", baglanti))
                    {
                        ekle.Parameters.AddWithValue("@ogr_no", ogrenciler2[0]);
                        ekle.Parameters.AddWithValue("@ad", ogrenciler2[1]);
                        ekle.Parameters.AddWithValue("@soyad", ogrenciler2[2]);
                        ekle.Parameters.AddWithValue("@sinif", ogrenciler2[3]);
                        ekle.Parameters.AddWithValue("@cinsiyet", ogrenciler2[4]);
                        ekle.Parameters.AddWithValue("@telefon", ogrenciler2[5]);
                        ekle.ExecuteNonQuery();
                    }

                    using (SqlCommand sil = new SqlCommand("DELETE FROM temizlikyapmayanogr WHERE ogr_no=@no", baglanti))
                    {
                        sil.Parameters.AddWithValue("@no", ogrenciler2[0]);
                        sil.ExecuteNonQuery();
                    }


                    listele();
                    listele2();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.Message);
            }
            finally
            {
                if (baglanti.State == ConnectionState.Open)
                    baglanti.Close();


               
            }

        }

      
    }
}