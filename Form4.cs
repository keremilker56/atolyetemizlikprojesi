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
using System.Threading;

namespace atolyetemizlikprojesi
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }


        SqlCommand komut;
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-L2IO4EG\\SQLEXPRESS;Initial Catalog=temizlikprogrami;Integrated Security=True;");
        public void listele()
        {
            komut = new SqlCommand("select * from temizlikyapanogr", baglanti);
            SqlDataAdapter adapter = new SqlDataAdapter(komut);
            DataTable table = new DataTable();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
        }
        public void listele2()
        {
            komut = new SqlCommand("select * from temizlikyapacakogr", baglanti);
            SqlDataAdapter adapter = new SqlDataAdapter(komut);
            DataTable table = new DataTable();
            adapter.Fill(table);
            dataGridView2.DataSource = table;
        }








        private void Form4_Load(object sender, EventArgs e)
        {
            listele();
            listele2();
            timer1.Start();

            //TimeSpan.FromHours(168);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            this.Hide();
            form1.ShowDialog();
        }

        int aralik = 0;


        List<string> ogrenciler = new List<string>();
        List<string> ogrenciler2 = new List<string>();
        List<string> ogrenciler3 = new List<string>();
        List<string> ogrencilerden = new List<string>();

        List<string> ogrencilerden2 = new List<string>();
        List<string> ogrenciler4 = new List<string>();

        List<string> ogrenciler5 = new List<string>();



        // List<string> ogrenciler4 = new List<string>();


        DateTime başlangic = new DateTime(2025, 5, 11);

        int indeks=0;
        int index = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            ogrenciler.Clear();
            ogrenciler2.Clear();
            ogrenciler3.Clear();
            ogrencilerden.Clear();
            ogrencilerden2.Clear();
            ogrenciler4.Clear();
            ogrenciler5.Clear();
            index = 0;


            //ogrenciler4.Clear();

            try
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }

                using (SqlCommand komut = new SqlCommand("SELECT * FROM temizlikyapmayanogr ORDER BY ogr_no ASC", baglanti))
                {




                    using (SqlDataReader reader = komut.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            ogrenciler.Add(reader["ogr_no"].ToString());
                        }

                    }
                    using (SqlDataReader reading = komut.ExecuteReader())
                    {
                        while (index < ogrenciler.Count / 2)
                        {
                            ogrencilerden.Add(reading["ogr_no"].ToString());
                            index++;

                        }
                    }
                    using (SqlDataReader reading2 = komut.ExecuteReader())
                    {
                        while (ogrenciler.Count > index)
                        {
                            ogrencilerden2[index] = (reading2["ogr_no"].ToString());
                            index++;
                        }
                    }

                }




                if (indeks >= ogrenciler2.Count || indeks >= ogrenciler3.Count)
                {
                    indeks = 0;
                }
                if (ogrenciler.Count > 0)
                {
                    aralik = (int)(DateTime.Now - başlangic).TotalDays % ogrenciler.Count;
                    string bugununtemizlikcisi = ogrencilerden[aralik];
                    string bugununtemizlikcisi2 = ogrencilerden2[aralik];

                    //bugünün öğrencileri ayarlaması
                    if (aralik >= 0 && aralik < ogrenciler.Count)
                    {
                        using (SqlCommand komut1 = new SqlCommand("SELECT * FROM temizlikyapmayanogr WHERE ogr_no = @no", baglanti))
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
                            }
                        }
                        using (SqlCommand komut1 = new SqlCommand("SELECT * FROM temizlikyapmayanogr WHERE ogr_no = @no", baglanti))
                        {
                            komut1.Parameters.AddWithValue("@no", int.Parse(bugununtemizlikcisi2));


                            using (SqlDataReader okuma1 = komut1.ExecuteReader())
                            {
                                if (okuma1.Read())
                                {
                                    ogrenciler4.Add(okuma1["ogr_no"].ToString());
                                    ogrenciler4.Add(okuma1["ad"].ToString());
                                    ogrenciler4.Add(okuma1["soyad"].ToString());
                                    ogrenciler4.Add(okuma1["sinif"].ToString());
                                    ogrenciler4.Add(okuma1["cinsiyet"].ToString());
                                    ogrenciler4.Add(okuma1["telefon"].ToString());
                                }
                            }
                        }







                        //gelmeyen öğrenci ayarlaması
                        using (SqlCommand cmd = new SqlCommand("SELECT ogr_no FROM gelmeyenogrenci where ogr_no=@no", baglanti))
                        {
                            cmd.Parameters.AddWithValue("@no", int.Parse(bugununtemizlikcisi));


                            using (SqlDataReader sqlDataReader = cmd.ExecuteReader())
                            {
                                if (sqlDataReader.Read())
                                {
                                    ogrenciler3.Add(sqlDataReader["ogr_no"].ToString());
                                }
                            }
                        }


                        using (SqlCommand cmd = new SqlCommand("SELECT ogr_no FROM gelmeyenogrenci where ogr_no=@no", baglanti))
                        {
                            cmd.Parameters.AddWithValue("@no", int.Parse(bugununtemizlikcisi2));


                            using (SqlDataReader sqlDataReader = cmd.ExecuteReader())
                            {
                                if (sqlDataReader.Read())
                                {
                                    ogrenciler5.Add(sqlDataReader["ogr_no"].ToString());
                                }
                            }
                        }
                        if (ogrenciler3.Count > 0)
                        {
                            if (ogrenciler2.Count > 0 && indeks < ogrenciler2.Count)
                            {
                                if (int.TryParse(ogrenciler3[indeks], out int ogrenci3No) &&//int parse den farkı eğer değerin içinde string bir değer olsaydı program çökedi try parse güvenli kullanım sunar 
                                    int.TryParse(ogrenciler2[indeks], out int ogrenci2No))
                                {
                                    if (ogrenci3No != ogrenci2No)
                                    {

                                        using (SqlCommand yapanekle = new SqlCommand("INSERT INTO temizlikyapanogr (ogr_no, ad, soyad, sinif, cinsiyet, telefon) VALUES (@ogr_no, @ad, @soyad, @sinif, @cinsiyet, @telefon)", baglanti))
                                        {
                                            yapanekle.Parameters.AddWithValue("@ogr_no", ogrenciler2[0]);
                                            yapanekle.Parameters.AddWithValue("@ad", ogrenciler2[1]);
                                            yapanekle.Parameters.AddWithValue("@soyad", ogrenciler2[2]);
                                            yapanekle.Parameters.AddWithValue("@sinif", ogrenciler2[3]);
                                            yapanekle.Parameters.AddWithValue("@cinsiyet", ogrenciler2[4]);
                                            yapanekle.Parameters.AddWithValue("@telefon", ogrenciler2[5]);
                                            yapanekle.ExecuteNonQuery();
                                        }


                                        using (SqlCommand yapansil = new SqlCommand("delete from temizlikyapmayanogr where ogr_no=@no", baglanti))
                                        {
                                            yapansil.Parameters.AddWithValue("@no", ogrenciler2[0]);
                                            yapansil.ExecuteNonQuery();
                                        }

                                        listele();
                                        listele2();
                                    }
                                    else
                                    {
                                        DialogResult cevap = MessageBox.Show("Öğrenci bugün okula gelmemiş sıradaki öğrenci gelsin mi", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        if (cevap == DialogResult.OK)
                                        {


                                            using (SqlCommand yapansil = new SqlCommand("select * from temizlikyapmayanogr where ogr_no=@no", baglanti))
                                            {
                                                yapansil.Parameters.AddWithValue("@no", ogrenciler[int.Parse(bugununtemizlikcisi) + 1]);
                                                yapansil.ExecuteNonQuery();


                                                using (SqlCommand yapansil2 = new SqlCommand("delete from temizlikyapmayanogr where ogr_no=@no", baglanti))
                                                {
                                                    yapansil2.Parameters.AddWithValue("@no", int.Parse(bugununtemizlikcisi));
                                                    yapansil2.ExecuteNonQuery();
                                                }
                                                using (SqlCommand yapanekle1 = new SqlCommand("INSERT INTO temizlikyapacakogr (ogr_no, ad, soyad, sinif, cinsiyet, telefon) VALUES (@ogr_no, @ad, @soyad, @sinif, @cinsiyet, @telefon)", baglanti))
                                                {
                                                    yapanekle1.Parameters.AddWithValue("@ogr_no", ogrenciler2[0]);
                                                    yapanekle1.Parameters.AddWithValue("@ad", ogrenciler2[1]);
                                                    yapanekle1.Parameters.AddWithValue("@soyad", ogrenciler2[2]);
                                                    yapanekle1.Parameters.AddWithValue("@sinif", ogrenciler2[3]);
                                                    yapanekle1.Parameters.AddWithValue("@cinsiyet", ogrenciler2[4]);
                                                    yapanekle1.Parameters.AddWithValue("@telefon", ogrenciler2[5]);
                                                    yapanekle1.ExecuteNonQuery();

                                                }
                                                using (SqlDataReader ok = yapansil.ExecuteReader())
                                                {



                                                    while (ok.Read())
                                                    {
                                                        ogrenciler2.Clear();
                                                        ogrenciler2.Add(ok["ogr_no"].ToString());
                                                        ogrenciler2.Add(ok["ad"].ToString());
                                                        ogrenciler2.Add(ok["soyad"].ToString());
                                                        ogrenciler2.Add(ok["sinif"].ToString());
                                                        ogrenciler2.Add(ok["cinsiyet"].ToString());
                                                        ogrenciler2.Add(ok["telefon"].ToString());

                                                    }
                                                }





                                            }
                                            using (SqlCommand yapanekle1 = new SqlCommand("INSERT INTO temizlikyapanogr (ogr_no, ad, soyad, sinif, cinsiyet, telefon) VALUES (@ogr_no, @ad, @soyad, @sinif, @cinsiyet, @telefon)", baglanti))
                                            {
                                                yapanekle1.Parameters.AddWithValue("@ogr_no", ogrenciler2[0]);
                                                yapanekle1.Parameters.AddWithValue("@ad", ogrenciler2[1]);
                                                yapanekle1.Parameters.AddWithValue("@soyad", ogrenciler2[2]);
                                                yapanekle1.Parameters.AddWithValue("@sinif", ogrenciler2[3]);
                                                yapanekle1.Parameters.AddWithValue("@cinsiyet", ogrenciler2[4]);
                                                yapanekle1.Parameters.AddWithValue("@telefon", ogrenciler2[5]);
                                                yapanekle1.ExecuteNonQuery();

                                            }



                                        }
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("İndeks sınırları aşıyor!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        else
                        {
                             using (SqlCommand yapanekle = new SqlCommand("INSERT INTO temizlikyapanogr (ogr_no, ad, soyad, sinif, cinsiyet, telefon) VALUES (@ogr_no, @ad, @soyad, @sinif, @cinsiyet, @telefon)", baglanti))
                             {
                                    yapanekle.Parameters.AddWithValue("@ogr_no", ogrenciler2[0]);
                                    yapanekle.Parameters.AddWithValue("@ad", ogrenciler2[1]);
                                    yapanekle.Parameters.AddWithValue("@soyad", ogrenciler2[2]);
                                    yapanekle.Parameters.AddWithValue("@sinif", ogrenciler2[3]);
                                    yapanekle.Parameters.AddWithValue("@cinsiyet", ogrenciler2[4]);
                                    yapanekle.Parameters.AddWithValue("@telefon", ogrenciler2[5]);
                                    yapanekle.ExecuteNonQuery();
                             }


                              using (SqlCommand yapansil = new SqlCommand("delete from temizlikyapmayanogr where ogr_no=@no", baglanti))
                              {
                                    yapansil.Parameters.AddWithValue("@no", ogrenciler2[0]);
                                    yapansil.ExecuteNonQuery();
                              }

                               listele();
                               listele2();
                            }


                        }//
                        if (ogrenciler5.Count > 0)
                        {
                            if (ogrenciler4.Count > 0 && indeks < ogrenciler4.Count)
                            {
                                if (int.TryParse(ogrenciler5[indeks], out int ogrenci5No) &&//int parse den farkı eğer değerin içinde string bir değer olsaydı program çökedi try parse güvenli kullanım sunar 
                                    int.TryParse(ogrenciler4[indeks], out int ogrenci4No))
                                {
                                    if (ogrenci5No != ogrenci4No)
                                    {

                                        using (SqlCommand yapanekle = new SqlCommand("INSERT INTO temizlikyapanogr (ogr_no, ad, soyad, sinif, cinsiyet, telefon) VALUES (@ogr_no, @ad, @soyad, @sinif, @cinsiyet, @telefon)", baglanti))
                                        {
                                            yapanekle.Parameters.AddWithValue("@ogr_no", ogrenciler4[0]);
                                            yapanekle.Parameters.AddWithValue("@ad", ogrenciler4[1]);
                                            yapanekle.Parameters.AddWithValue("@soyad", ogrenciler4[2]);
                                            yapanekle.Parameters.AddWithValue("@sinif", ogrenciler4[3]);
                                            yapanekle.Parameters.AddWithValue("@cinsiyet", ogrenciler4[4]);
                                            yapanekle.Parameters.AddWithValue("@telefon", ogrenciler4[5]);
                                            yapanekle.ExecuteNonQuery();
                                        }


                                        using (SqlCommand yapansil = new SqlCommand("delete from temizlikyapmayanogr where ogr_no=@no", baglanti))
                                        {
                                            yapansil.Parameters.AddWithValue("@no", ogrenciler4[0]);
                                            yapansil.ExecuteNonQuery();
                                        }

                                        listele();
                                        listele2();
                                    }
                                    else
                                    {
                                        DialogResult cevap = MessageBox.Show("Öğrenci bugün okula gelmemiş sıradaki öğrenci gelsin mi", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        if (cevap == DialogResult.OK)
                                        {


                                            using (SqlCommand yapansil = new SqlCommand("select * from temizlikyapmayanogr where ogr_no=@no", baglanti))
                                            {
                                                yapansil.Parameters.AddWithValue("@no", ogrenciler[int.Parse(bugununtemizlikcisi2) + 1]);
                                                yapansil.ExecuteNonQuery();


                                                using (SqlCommand yapansil2 = new SqlCommand("delete from temizlikyapmayanogr where ogr_no=@no", baglanti))
                                                {
                                                    yapansil.Parameters.AddWithValue("@no", int.Parse(bugununtemizlikcisi2));
                                                    yapansil.ExecuteNonQuery();
                                                }
                                                using (SqlCommand yapanekle1 = new SqlCommand("INSERT INTO temizlikyapacakogr (ogr_no, ad, soyad, sinif, cinsiyet, telefon) VALUES (@ogr_no, @ad, @soyad, @sinif, @cinsiyet, @telefon)", baglanti))
                                                {
                                                    yapanekle1.Parameters.AddWithValue("@ogr_no", ogrenciler4[0]);
                                                    yapanekle1.Parameters.AddWithValue("@ad", ogrenciler4[1]);
                                                    yapanekle1.Parameters.AddWithValue("@soyad", ogrenciler4[2]);
                                                    yapanekle1.Parameters.AddWithValue("@sinif", ogrenciler4[3]);
                                                    yapanekle1.Parameters.AddWithValue("@cinsiyet", ogrenciler4[4]);
                                                    yapanekle1.Parameters.AddWithValue("@telefon", ogrenciler4[5]);
                                                    yapanekle1.ExecuteNonQuery();

                                                }
                                                using (SqlDataReader ok = yapansil.ExecuteReader())
                                                {



                                                    while (ok.Read())
                                                    {
                                                        ogrenciler4.Clear();
                                                        ogrenciler4.Add(ok["ogr_no"].ToString());
                                                        ogrenciler4.Add(ok["ad"].ToString());
                                                        ogrenciler4.Add(ok["soyad"].ToString());
                                                        ogrenciler4.Add(ok["sinif"].ToString());
                                                        ogrenciler4.Add(ok["cinsiyet"].ToString());
                                                        ogrenciler4.Add(ok["telefon"].ToString());

                                                    }
                                                }

                                            }
                                            using (SqlCommand yapanekle1 = new SqlCommand("INSERT INTO temizlikyapanogr (ogr_no, ad, soyad, sinif, cinsiyet, telefon) VALUES (@ogr_no, @ad, @soyad, @sinif, @cinsiyet, @telefon)", baglanti))
                                            {
                                                yapanekle1.Parameters.AddWithValue("@ogr_no", ogrenciler4[0]);
                                                yapanekle1.Parameters.AddWithValue("@ad", ogrenciler4[1]);
                                                yapanekle1.Parameters.AddWithValue("@soyad", ogrenciler4[2]);
                                                yapanekle1.Parameters.AddWithValue("@sinif", ogrenciler4[3]);
                                                yapanekle1.Parameters.AddWithValue("@cinsiyet", ogrenciler4[4]);
                                                yapanekle1.Parameters.AddWithValue("@telefon", ogrenciler4[5]);
                                                yapanekle1.ExecuteNonQuery();

                                            }



                                        }
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("İndeks sınırları aşıyor!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }

                            indeks++;

                        }
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.Message);
            }
            finally
            {
                if (baglanti.State == ConnectionState.Open)
                {
                    baglanti.Close();
                }
            }
        }
    }
}
