using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proje_BonusSchool
{
    public partial class FrmTeacher : Form
    {
        public FrmTeacher()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmClub club = new FrmClub();
            club.ShowDialog();
            this.Show();
        }

        private void btnLesson_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmLessons Lesson = new FrmLessons();
            Lesson.ShowDialog();
            this.Show();
        }

        private void btnStudent_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmStudent student = new FrmStudent();
            student.ShowDialog();
            this.Show();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmGrade grade = new FrmGrade();
            grade.ShowDialog();
            this.Show();
        }
    }
}
