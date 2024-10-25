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

namespace Proje_SQL_DB
{
    public partial class Frm_Kategori : Form
    {
        public Frm_Kategori()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-B6FJQDJ\\SQLEXPRESS;Initial Catalog=SatisVeriTabani;Integrated Security=True");

        private void Frm_Kategori_Load(object sender, EventArgs e)
        {

        }

        private void btnListele_Click(object sender, EventArgs e)
        {
            Listele();
        }

        public void Listele()
        {
            baglanti.Open();
            SqlCommand komutListele = new SqlCommand("Select * from Tbl_Kategori", baglanti);
            SqlDataAdapter adapter = new SqlDataAdapter(komutListele);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            baglanti.Close();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komutEkle = new SqlCommand("Insert into Tbl_Kategori (KategoriAd) values (@pAd)",baglanti);
            komutEkle.Parameters.AddWithValue("@pAd", tbxKategoriAd.Text);
            komutEkle.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kaydetme işlemi yapıldı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            tbxKategoriId.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString(); 
            tbxKategoriAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komutSil = new SqlCommand("Delete from Tbl_Kategori where KategoriId = @pId", baglanti);
            komutSil.Parameters.AddWithValue("@pId",tbxKategoriId.Text);
            komutSil.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Silme işlemi yapıldı.","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
            Listele();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komutGuncelle = new SqlCommand("Update Tbl_Kategori set KategoriAd = @pAd where KategoriId = @pId", baglanti);
            komutGuncelle.Parameters.AddWithValue("@pAd", tbxKategoriAd.Text);
            komutGuncelle.Parameters.AddWithValue("@pId", tbxKategoriId.Text);
            komutGuncelle.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Güncelleme işlemi yapıldı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele();
        }
    }
}
