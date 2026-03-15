using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlClient;
using System.Globalization;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Proje_BonusSchool
{
    public partial class FrmGrade : Form
    {
        public FrmGrade()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-IOMSQH7\SQLEXPRESS;Initial Catalog=BonusSchool;Integrated Security=True;");

        DataSet1TableAdapters.TblNotes1TableAdapter ds = new DataSet1TableAdapters.TblNotes1TableAdapter();

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void FrmGrade_Load(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Select * from TblLessons", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboBox1.DisplayMember = "LessonName";
            comboBox1.ValueMember = "ID";
            comboBox1.DataSource = dt;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ds.NotesList(int.Parse(txtID.Text));
            dataGridView1.Columns["LessonName"].DisplayIndex = 0;

            dataGridView1.Columns["Exam1"].DisplayIndex = 1;
            dataGridView1.Columns["Exam2"].DisplayIndex = 2;
            dataGridView1.Columns["Exam3"].DisplayIndex = 3;

            dataGridView1.Columns["Project"].DisplayIndex = 4;

            dataGridView1.Columns["Average"].DisplayIndex = 5;

            dataGridView1.Columns["Status"].DisplayIndex = 6;

            dataGridView1.Columns["LessonName"].AutoSizeMode =
    DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["NoteID"].Visible = false;
            dataGridView1.Columns["LessonId"].Visible = false;
            dataGridView1.Columns["StId"].Visible = false;
            dataGridView1.Columns["StName"].Visible = false; // Gridde görünmesin
            label9.Text = dataGridView1.Rows[0].Cells["StName"].Value.ToString();
        }



        int noteId;

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            noteId = int.Parse(txtID.Text = dataGridView1.Rows[e.RowIndex].Cells["NoteID"].Value.ToString());
            txtID.Text = dataGridView1.Rows[e.RowIndex].Cells["StId"].Value.ToString();
            comboBox1.Text = dataGridView1.Rows[e.RowIndex].Cells["LessonName"].Value.ToString();
            txtExam1.Text = dataGridView1.Rows[e.RowIndex].Cells["Exam1"].Value.ToString();
            txtExam2.Text = dataGridView1.Rows[e.RowIndex].Cells["Exam2"].Value.ToString();
            txtExam3.Text = dataGridView1.Rows[e.RowIndex].Cells["Exam3"].Value.ToString();
            txtProject.Text = dataGridView1.Rows[e.RowIndex].Cells["Project"].Value.ToString();
            txtAverage.Text = dataGridView1.Rows[e.RowIndex].Cells["Average"].Value.ToString();
            txtStatus.Text = dataGridView1.Rows[e.RowIndex].Cells["Status"].Value.ToString();
        }





       
        private void btnCalculate_Click(object sender, EventArgs e)
        {
          

            int Exam1, Exam2, Exam3, Project;

            //  Exam ve Project girişlerini kontrol et
            if (!int.TryParse(txtExam1.Text, out Exam1)) { MessageBox.Show("Exam1 must be a valid number!"); return; }
            if (!int.TryParse(txtExam2.Text, out Exam2)) { MessageBox.Show("Exam2 must be a valid number!"); return; }
            if (!int.TryParse(txtExam3.Text, out Exam3)) { MessageBox.Show("Exam3 must be a valid number!"); return; }
            if (!int.TryParse(txtProject.Text, out Project)) { MessageBox.Show("Project must be a valid number!"); return; }

            // Ortalama hesapla
            double Average = (Exam1 + Exam2 + Exam3 + Project) / 4.0;

            //  TextBoxlara yaz
            txtAverage.Text = Average.ToString(CultureInfo.InvariantCulture);
            txtStatus.Text = Average < 50 ? "False" : "True";
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

            try
            {
                // 1️⃣ Lesson ID
                if (!byte.TryParse(comboBox1.SelectedValue.ToString(), out byte lessonId))
                {
                    MessageBox.Show("Invalid lesson selection!");
                    return;
                }

                // 2️⃣ Student ID
                if (!int.TryParse(txtID.Text, out int studentId))
                {
                    MessageBox.Show("Invalid Student ID!");
                    return;
                }

                // 3️⃣ Exams & Project
                if (!byte.TryParse(txtExam1.Text, out byte exam1) ||
                   !byte.TryParse(txtExam2.Text, out byte exam2) ||
                   !byte.TryParse(txtExam3.Text, out byte exam3) ||
                   !byte.TryParse(txtProject.Text, out byte project))
                {
                    MessageBox.Show("Exam and Project scores must be valid numbers (0-255)!");
                    return;
                }

                // 4️⃣ Average
                if (!decimal.TryParse(txtAverage.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal average))
                {
                    MessageBox.Show("Invalid Average value!");
                    return;
                }

                // 5️⃣ Status
                if (!bool.TryParse(txtStatus.Text.ToLower(), out bool status))
                {
                    MessageBox.Show("Invalid Status value!");
                    return;
                }

                // 6️⃣ Update database
                ds.NotesUpdate(lessonId, studentId, exam1, exam2, exam3, project, average, status, noteId);

                MessageBox.Show("Update successful!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }



            dataGridView1.DataSource = ds.NotesList(int.Parse(txtID.Text));
            // Kolon sıralarını tekrar ayarla
            dataGridView1.Columns["LessonName"].DisplayIndex = 0;
            dataGridView1.Columns["Exam1"].DisplayIndex = 1;
            dataGridView1.Columns["Exam2"].DisplayIndex = 2;
            dataGridView1.Columns["Exam3"].DisplayIndex = 3;
            dataGridView1.Columns["Project"].DisplayIndex = 4;
            dataGridView1.Columns["Average"].DisplayIndex = 5;
            dataGridView1.Columns["Status"].DisplayIndex = 6;

            // Opsiyonel: bazı kolonları gizle
            dataGridView1.Columns["NoteID"].Visible = false;
            dataGridView1.Columns["LessonId"].Visible = false;
            dataGridView1.Columns["StId"].Visible = false;
            dataGridView1.Columns["StName"].Visible = false;
        }
    }
}

