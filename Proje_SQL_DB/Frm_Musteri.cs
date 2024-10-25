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
    public partial class Frm_Musteri : Form
    {
        public Frm_Musteri()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-B6FJQDJ\\SQLEXPRESS;Initial Catalog=SatisVeriTabani;Integrated Security=True");

        public void Listele()
        {
            SqlCommand komutListele = new SqlCommand("Select * from Tbl_Musteri",baglanti);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(komutListele);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;   
        }

        private void Frm_Musteri_Load(object sender, EventArgs e)
        {
            Listele();

            baglanti.Open();
            SqlCommand komutSehirGetir = new SqlCommand("Select * from Tbl_Sehir", baglanti);
            SqlDataReader dataReader = komutSehirGetir.ExecuteReader();
            while (dataReader.Read())
            {
                cbxMusteriSehir.Items.Add(dataReader[1]);
            }
            dataReader.Close();
            baglanti.Close();
        }

        private void btnListele_Click(object sender, EventArgs e)
        {
            tbxMusteriId.Clear();
            tbxMusteriAd.Clear();
            tbxMusteriSoyad.Clear();
            cbxMusteriSehir.Text = "";
            tbxMusteriBakiye.Clear();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            tbxMusteriId.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            tbxMusteriAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            tbxMusteriSoyad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            cbxMusteriSehir.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            tbxMusteriBakiye.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komutKaydet = new SqlCommand("Insert into Tbl_Musteri (MusteriAd,MusteriSoyad,MusteriSehir,MusteriBakiye) values (@pAd,@pSoyad,@pSehir,@pBakiye)",baglanti);
            komutKaydet.Parameters.AddWithValue("@pAd",tbxMusteriAd.Text);
            komutKaydet.Parameters.AddWithValue("@pSoyad",tbxMusteriSoyad.Text);
            komutKaydet.Parameters.AddWithValue("@pSehir",cbxMusteriSehir.Text);
            komutKaydet.Parameters.AddWithValue("@pBakiye",tbxMusteriBakiye.Text);
            komutKaydet.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kaydetme işlemi yapıldı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komutSil = new SqlCommand("Delete from Tbl_Musteri where MusteriId = @pId",baglanti);
            komutSil.Parameters.AddWithValue("@pId",tbxMusteriId.Text);
            komutSil.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Silme işlemi yapıldı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele();

        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komutGuncelle = new SqlCommand("Update Tbl_Musteri set MusteriAd=@pAd, MusteriSoyad = @pSoyad, MusteriSehir = @pSehir3, MusteriBakiye = @pBakiye where MusteriId = @pId",baglanti);
            komutGuncelle.Parameters.AddWithValue("@pAd",tbxMusteriAd.Text);
            komutGuncelle.Parameters.AddWithValue("@pSoyad",tbxMusteriSoyad.Text);
            komutGuncelle.Parameters.AddWithValue("@pSehir", System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(cbxMusteriSehir.Text.ToLower()));
            komutGuncelle.Parameters.AddWithValue("@pBakiye",Decimal.Parse(tbxMusteriBakiye.Text));
            komutGuncelle.Parameters.AddWithValue("@pId",tbxMusteriId.Text);
            komutGuncelle.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Güncelleme işlemi yapıldı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele();
        }

        private void btnAra_Click(object sender, EventArgs e)
        {
            if(tbxMusteriAd.Text != null)
            {
                baglanti.Open();
                SqlCommand komutAra = new SqlCommand("Select * from Tbl_Musteri where MusteriAd like @pAd ", baglanti);
                komutAra.Parameters.AddWithValue("@pAd", "%" + tbxMusteriAd.Text + "%");
                SqlDataAdapter dataAdapter = new SqlDataAdapter(komutAra);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
                baglanti.Close();
            }
            else
            {
                Listele();
            }

        }
    }
}
