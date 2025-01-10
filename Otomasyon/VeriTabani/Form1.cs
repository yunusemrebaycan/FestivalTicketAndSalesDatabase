using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace VeriTabani
{
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection("Data Source=;Initial Catalog=;User ID=;Password=");
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            kayitlariGetir();
        }

        public void kayitlariGetir() 

        {
            
            
            SqlCommand cmd = new SqlCommand("select * from satisTable",con); //ExecuteReader
            SqlCommand cmd2 = new SqlCommand("select * from festivalTable", con);
            SqlCommand cmd3 = new SqlCommand("select * from etkinlikTable", con);
            SqlCommand cmd4 = new SqlCommand("select * from biletTable", con);
            SqlCommand cmd5 = new SqlCommand("select * from kullaniciTable", con);
            SqlCommand cmd6 = new SqlCommand("select * from sanatciTable", con);
            SqlCommand cmd7 = new SqlCommand("select * from Bakiye",con);

            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();
            DataTable dt3 = new DataTable();
            DataTable dt4 = new DataTable();
            DataTable dt5 = new DataTable();
            DataTable dt6 = new DataTable();


            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            dataGridViewSatis.DataSource = dt;
            con.Close();

            con.Open();
            SqlDataReader dr2 = cmd2.ExecuteReader();
            dt2.Load(dr2);
            dataGridViewFestival.DataSource = dt2;
            con.Close();

            con.Open();
            SqlDataReader dr3 = cmd3.ExecuteReader();
            dt3.Load(dr3);
            dataGridViewEtkinlik.DataSource = dt3;
            con.Close();

            con.Open();
            SqlDataReader dr4 = cmd4.ExecuteReader();
            dt4.Load(dr4);
            dataGridViewBilet.DataSource = dt4;
            con.Close();

            con.Open();
            SqlDataReader dr5 = cmd5.ExecuteReader();
            dt5.Load(dr5);
            dataGridViewKullanici.DataSource = dt5;
            con.Close();

            con.Open();
            SqlDataReader dr6 = cmd6.ExecuteReader();
            dt6.Load(dr6);
            dataGridViewSanatci.DataSource = dt6;
            con.Close();

            con.Open();

            SqlDataReader dr7 = cmd7.ExecuteReader();
            string bakiye = string.Empty;

            if (dr7.Read()) // İlk satırı okur
            {
                bakiye = dr7[0].ToString(); // İlk sütunun değerini string'e çevirir
            }

            con.Close();

            labelBakiye.Text = bakiye;

        }

        public Boolean formKontrol()
        {
            if (textBoxTckn.Text == string.Empty || textBoxBiletNo.Text == string.Empty)
                return false;
            else
                return true;
        }

        private void buttonSat_Click(object sender, EventArgs e)
        {
            DateTime simdi = DateTime.Now;

            string tarih = simdi.ToString("dd.MM.yyyy");

            try
            {
                if (formKontrol())
                {
                    SqlCommand cmd = new SqlCommand("insert into satisTable(kullanici_id,bilet_id,satis_Tarihi) values(@kullaniciid,@biletid,@tarih)", con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@kullaniciid", int.Parse(textBoxTckn.Text));
                    cmd.Parameters.AddWithValue("@biletid", int.Parse(textBoxBiletNo.Text));
                    cmd.Parameters.AddWithValue("@tarih", simdi);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                
                    MessageBox.Show("Satış kaydı başarıyla eklendi","Yeni Kayıt",MessageBoxButtons.OK,MessageBoxIcon.Information);

                    kayitlariGetir();
                    formuTemizle();
                }
                else
                {
                    MessageBox.Show("Eksik Alanları Doldurun");
                }
            }
            catch
            {

                MessageBox.Show("Doğru parametreler girin");
            }
        }

        private void formuTemizle()
        {
            textBoxBiletNo.Clear();
            textBoxTckn.Clear();
        }

        public void kullaniciEKle() 
        {
            
            SqlCommand cmd = new SqlCommand("insert into kullaniciTable(kullanici_id,ad_soyad,e_posta,telefon) values(@tckn,@adsoyad,@eposta,@telno)",con);

            try 
            {
                if (formkontrol())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@tckn", int.Parse(textBoxTcknn.Text));
                    cmd.Parameters.AddWithValue("@adsoyad", textBoxAdsoyad.Text);
                    cmd.Parameters.AddWithValue("@eposta", textBoxEposta.Text);
                    cmd.Parameters.AddWithValue("@telno", textBoxtel.Text);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("Kullanıcı kaydı başarıyla eklendi", "Yeni Kayıt", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    textBoxTcknn.Text = string.Empty;
                    textBoxAdsoyad.Text = string.Empty;
                    textBoxEposta.Text = string.Empty;  
                    textBoxtel.Text = string.Empty;

                    kayitlariGetir();

                }
                else
                {
                    MessageBox.Show("Eksik Alanları Doldurun");
                }    
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}");
                con.Close();
            }

        }

        public Boolean formkontrol() 
        {
            if (textBoxAdsoyad.Text == string.Empty || textBoxEposta.Text == string.Empty ||
                textBoxTcknn.Text == string.Empty || textBoxtel.Text == string.Empty)
            {
                return false;
            }
            else 
            {
                return true;
            }
            
                
            
        }

        private void btn_ekle_Click(object sender, EventArgs e)
        {
            kullaniciEKle();
        }

        private void buttoniptal_Click(object sender, EventArgs e)
        {
            try 
            {
                SqlCommand cmd = new SqlCommand("delete from satisTable where satis_id = @satisid", con);

                string id = textBoxid.Text;

                if (textBoxid.Text != string.Empty)
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@satisid", id);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("Satış iptal edildi.", "Kayıt İptal", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    kayitlariGetir();
                }
                else
                {
                    MessageBox.Show("Satış Kaydı Seçilmedi");
                }
            }
            catch 
            {
                MessageBox.Show("Hata!!!");
            }


            
            

        }

        private void dataGridViewSatis_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (dataGridViewSatis.CurrentRow != null) 
            {
                DataGridViewRow row = dataGridViewSatis.CurrentRow;

               
                textBoxid.Text = row.Cells[0].Value.ToString();
                textBoxTckn.Text = row.Cells[1].Value.ToString();
                textBoxBiletNo.Text = row.Cells[2].Value.ToString();

                int biletfiyati = int.Parse(textBoxBiletNo.Text);

                SqlCommand cmd = new SqlCommand("select fiyat from biletTable where bilet_id = @biletfiyat", con);

                cmd.CommandType=CommandType.Text;
                cmd.Parameters.AddWithValue("@biletfiyat",biletfiyati);


                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                string fiyat = string.Empty;

                if (dr.Read())
                {
                    fiyat = dr[0].ToString();
                }

                con.Close();

                labelbiletfiyat.Text = fiyat;
            }
        }

        private void btn_sil_Click(object sender, EventArgs e)
        {
            int tckn;

            if (int.TryParse(textBoxTcknn.Text, out tckn))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM kullaniciTable WHERE kullanici_id = @tckn", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@tckn", tckn);

                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Kullanıcı başarıyla silindi.");
                    con.Close();
                    kayitlariGetir();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }
            else
            {
                MessageBox.Show("Lütfen geçerli bir TCKN girin.");
            }
        }

        private void dataGridViewKullanici_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (dataGridViewKullanici.CurrentRow != null)
            {
                DataGridViewRow row = dataGridViewKullanici.CurrentRow;


                textBoxTcknn.Text = row.Cells[0].Value.ToString();
                textBoxAdsoyad.Text = row.Cells[1].Value.ToString();
                textBoxEposta.Text = row.Cells[2].Value.ToString();
                textBoxtel.Text = row.Cells[3].Value.ToString();

            }
        }

        private void btn_guncelle_Click(object sender, EventArgs e)
        {
            try 
            {
                if (formkontrol()) 
                {
                    SqlCommand cmd = new SqlCommand("update kullaniciTable set ad_soyad = @adsoyad, e_posta = @eposta, telefon = @telefon where kullanici_id = @tckn", con);

                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@tckn", int.Parse(textBoxTcknn.Text));
                    cmd.Parameters.AddWithValue("@adsoyad", textBoxAdsoyad.Text);
                    cmd.Parameters.AddWithValue("@eposta", textBoxEposta.Text);
                    cmd.Parameters.AddWithValue("@telefon", textBoxtel.Text);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("Kayıt Başarıyla Güncellendi!");

                    kayitlariGetir();
                }
            }
            catch
            {

                MessageBox.Show("Hata");
                con.Close();
            }
        }

        private void btnyenile_Click(object sender, EventArgs e)
        {
            kayitlariGetir();
        }

        private void btntemizle_Click(object sender, EventArgs e)
        {
            textBoxTcknn.Text = string.Empty;
            textBoxAdsoyad.Text = string.Empty;
            textBoxEposta.Text = string.Empty;
            textBoxtel.Text = string.Empty;
        }
    }
}
