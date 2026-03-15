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
using System.Runtime.CompilerServices;

namespace Proje_BonusSchool
{
    public partial class FrmStudent : Form
    {
        public FrmStudent()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-IOMSQH7\SQLEXPRESS;Initial Catalog=BonusSchool;Integrated Security=True;");

        DataSet1TableAdapters.DataTable1TableAdapter ds = new DataSet1TableAdapters.DataTable1TableAdapter();

        string gender = " ";// update ve ekleme için lazım

        private void FrmStudent_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ds.StudentList();
            dataGridView1.Columns["StID"].Width = 60;
            dataGridView1.Columns["StID"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            //combobox için ASP.Net kısmı ile çözümü
            SqlCommand komut = new SqlCommand("Select * from TblClubs",baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboBox1.DisplayMember = "ClubName";
            comboBox1.ValueMember = "ClubID";
            comboBox1.DataSource = dt;

            dataGridView1.Columns["StClub"].Visible = false;

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ds.StudentList();
        }

        
        private void btnAdd_Click(object sender, EventArgs e)
        {
           
            if (radioButton1.Checked == true)
            {
                gender = "Girl";
            }
            if (radioButton2.Checked == true) 
            {
                gender = "Boy";
            }
            ds.StudentAdd(txtName.Text, byte.Parse(comboBox1.SelectedValue.ToString()), gender);
            MessageBox.Show(" The student addition process has been completed. ", "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
            dataGridView1.DataSource=ds.StudentList();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //txtID.Text=comboBox1.SelectedValue.ToString();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtID.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtName.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();

            comboBox1.SelectedValue = dataGridView1.CurrentRow.Cells["StClub"].Value;

            string GENDER = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            if (GENDER == "Girl")
            {
                radioButton1.Checked = true;
            }
            else
            {
                radioButton2.Checked = true;
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            ds.StudentDelete(int.Parse(txtID.Text));
            MessageBox.Show(" The student delete process has been completed. ", "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
            dataGridView1.DataSource = ds.StudentList();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                gender = "Girl";
            }
            if (radioButton2.Checked == true)
            {
                gender = "Boy";
            }
            ds.StudentUpdate(txtName.Text,byte.Parse(comboBox1.SelectedValue.ToString()),gender,int.Parse(txtID.Text));
            MessageBox.Show(" The student update process has been completed. ", "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
            dataGridView1.DataSource = ds.StudentList();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ds.StSearch(txtSearch.Text);
        }
    }
}
