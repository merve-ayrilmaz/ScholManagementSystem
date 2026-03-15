using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Proje_BonusSchool.DataSet1TableAdapters;

namespace Proje_BonusSchool
{
    public partial class FrmLessons : Form
    {
        public FrmLessons()
        {
            InitializeComponent();
        }


        DataSet1TableAdapters.TblLessonsTableAdapter ds = new DataSet1TableAdapters.TblLessonsTableAdapter();

        private void FrmLessons_Load(object sender, EventArgs e)
        {
           
            dataGridView1.DataSource = ds.LessonList();
            dataGridView1.Columns["ID"].Width = 60;
            dataGridView1.Columns["ID"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

        }

        private void btnList_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ds.LessonList();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ds.LessonAdd(txtLessonName.Text);
            MessageBox.Show(" The lesson addition process has been completed. " ,"INFORMATION",MessageBoxButtons.OK,MessageBoxIcon.Information);
            dataGridView1.DataSource = ds.LessonList();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtLessonID.Text))
                {
                    MessageBox.Show("First, choose your lesson!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                byte lessonId = byte.Parse(txtLessonID.Text);

                // TblNotes TableAdapter
                TblNotesTableAdapter notesAdapter = new TblNotesTableAdapter();

                // 1️⃣ Önce bu derse ait notları sil
                notesAdapter.NotesDelete(lessonId); // TableAdapter metodun adı bu, eğer farklıysa değiştir

                // 2️⃣ Ardından dersi sil
                ds.LessonDelete(lessonId);

                MessageBox.Show("The lesson and its notes have been deleted.", "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // DataGridView’i güncelle
                dataGridView1.DataSource = ds.LessonList();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            ds.LessonUpdate(txtLessonName.Text , byte.Parse(txtLessonID.Text));
            MessageBox.Show(" The lesson update process has been completed. ", "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
            dataGridView1.DataSource = ds.LessonList();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtLessonID.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtLessonName.Text= dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
