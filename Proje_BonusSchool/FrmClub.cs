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
using System.Data.Common;
using System.Net.Configuration;

namespace Proje_BonusSchool
{
    public partial class FrmClub : Form
    {
        public FrmClub()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-IOMSQH7\SQLEXPRESS;Initial Catalog=BonusSchool;Integrated Security=True;");
        void list()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM TblClubs",baglanti);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns["ClubID"].Width = 65;
            dataGridView1.Columns["ClubID"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

        }
        private void FrmClub_Load(object sender, EventArgs e)
        {
            list();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            list();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("INSERT INTO TblClubs (ClubName) VALUES (@p1)", baglanti);
            komut.Parameters.AddWithValue("@p1", txtClubName.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show(" Great! Successfully added to the club list.", "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Information );
            list();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void pictureBox5_MouseHover(object sender, EventArgs e)
        {
            pictureBox5.BackColor = SystemColors.Control;

        }

        private void pictureBox5_MouseLeave(object sender, EventArgs e)
        {
            pictureBox5.BackColor = Color.Transparent;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)//buradaki e datagridin hücre görünümlerinin olayları için çalışıyor.
        {
            txtClubID.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtClubName.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            baglanti.Open();
            SqlCommand komut = new SqlCommand("DELETE FROM TblClubs WHERE ClubID=@p1", baglanti);
            komut.Parameters.AddWithValue("@p1", txtClubID.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show(" The club you selected has been deleted.", "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
            list();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("UPDATE TblClubs SET ClubName=@p1 WHERE ClubID=@p2", baglanti);
            komut.Parameters.AddWithValue("@p1", txtClubName.Text);
            komut.Parameters.AddWithValue("@p2", txtClubID.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show(" Changes have been updated for the club you selected.", "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
            list();
        }
    }
}
