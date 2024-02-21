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

namespace UrunTakip
{
    public partial class Frmistatistik : Form
    {
        public Frmistatistik()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=LAPTOP-6OCSKQ8T;Initial Catalog=DbUrunler;Integrated Security=True");
        private void Frmistatistik_Load(object sender, EventArgs e)
        {
            //Toplam Kategori Sayısı
            baglanti.Open();
            SqlCommand komut1 = new SqlCommand("Select Count(*) From TblKategori",baglanti);
            SqlDataReader dr=komut1.ExecuteReader();
            while (dr.Read())
            {
                lblToplamKategori.Text = dr[0].ToString();
            }
            baglanti.Close();

            // Toplam Ürün Sayısı
            baglanti.Open();
            SqlCommand komut2 = new SqlCommand("Select Count(*) From TblUrunler", baglanti);
            SqlDataReader dr2= komut2.ExecuteReader();
            while (dr2.Read())
            {
                lblToplamUrun.Text= dr2[0].ToString();
            }
            baglanti.Close();

            //Toplam Beyaz Eşya Sayısı
            baglanti.Open();
            SqlCommand komut3 = new SqlCommand("Select Count(*) From TblUrunler where Kategori=(Select ID From TblKategori where Ad='Beyaz Eşya')", baglanti);
            SqlDataReader dr3 = komut3.ExecuteReader();
            while (dr3.Read())
            {
                lblBeyazEsya.Text = dr3[0].ToString();
            }
            baglanti.Close();

            //Toplam Küçük Ev Aletleri Sayısı
            baglanti.Open();
            SqlCommand komut4 = new SqlCommand("Select Count(*) From TblUrunler where Kategori=(Select ID From TblKategori where Ad='Küçük Ev Aletleri')", baglanti);
            SqlDataReader dr4 = komut4.ExecuteReader();
            while (dr4.Read())
            {
                lblKucukEvAletleri.Text = dr4[0].ToString();
            }
            baglanti.Close();

            //En Yüksek Stoklu Ürün
            baglanti.Open();
            SqlCommand komut5 = new SqlCommand("Select * From TblUrunler where Stok=(Select Max(Stok) From TblUrunler)", baglanti);
            SqlDataReader dr5 = komut5.ExecuteReader();
            while (dr5.Read())
            {
                lblEnYuksekStok.Text = dr5["UrunAd"].ToString();
            }
            baglanti.Close();

            //En Düşük Stoklu Ürün
            baglanti.Open();
            SqlCommand komut6 = new SqlCommand("Select * From TblUrunler where Stok=(Select Min(Stok) From TblUrunler)", baglanti);
            SqlDataReader dr6 = komut6.ExecuteReader();
            while (dr6.Read())
            {
                lblEnDusukStok.Text = dr6["UrunAd"].ToString();
            }
            baglanti.Close();

            //Laptop Toplam Kâr Oranı
            baglanti.Open();
            SqlCommand komut7 = new SqlCommand("Select Stok*(SatisFiyat-AlisFiyat) From TblUrunler where urunAd='Laptop'", baglanti);
            SqlDataReader dr7 = komut7.ExecuteReader();
            while (dr7.Read())
            {
                lblToplamLaptopKar.Text = dr7[0].ToString()+" ₺";
            }
            baglanti.Close();

            //Beyaz Eşya Toplam Kâr Oranı
            baglanti.Open();
            SqlCommand komut8 = new SqlCommand("Select Sum(stok*(SatisFiyat-AlisFiyat)) from TblUrunler where Kategori=(Select ID From TblKategori where Ad='Beyaz Eşya')", baglanti);
            SqlDataReader dr8 = komut8.ExecuteReader();
            while (dr8.Read())
            {
                lblBeyazEsyaToplamKar.Text = dr8[0].ToString() + " ₺";
            }
            baglanti.Close();
        }
    }
}
