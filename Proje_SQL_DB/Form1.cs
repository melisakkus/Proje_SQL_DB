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

namespace Proje_SQL_DB
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Frm_Kategori kategoriForm = new Frm_Kategori();
            kategoriForm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Frm_Musteri musteriForm = new Frm_Musteri();
            musteriForm.ShowDialog();
        }

        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-B6FJQDJ\\SQLEXPRESS;Initial Catalog=SatisVeriTabani;Integrated Security=True");

        private void Form1_Load(object sender, EventArgs e)
        {
            //Ürünlerin Durum Seviyesi
            SqlCommand komutKritikSeviyeProsedur = new SqlCommand("Execute Case_KritikSeviye",baglanti);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(komutKritikSeviyeProsedur);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;

            //grafiğe veri çekme
            //chart1.Series["Akdeniz"].Points.AddXY("Adana",24);
            //chart1.Series["Akdeniz"].Points.AddXY("İstanbul", 34);
            baglanti.Open();
            SqlCommand komutGrafik = new SqlCommand("Select KategoriAd, Count(*) from Tbl_Kategori \r\ninner join Tbl_Urunler\r\non Tbl_Kategori.KategoriId = Tbl_Urunler.UrunKategori\r\ngroup by KategoriAd", baglanti);
            SqlDataReader dataReader = komutGrafik.ExecuteReader();
            while (dataReader.Read())
            {
                chart1.Series["Kategoriler"].Points.AddXY(dataReader[0], dataReader[1]);
            }
            dataReader.Close();
            baglanti.Close();
        }


    }
}
