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
    public partial class FrmInlet : Form
    {
        public FrmInlet()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            FrmStNotes fr = new FrmStNotes();
            fr.number =maskedTextBox1.Text;
            fr.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmTeacher teacher = new FrmTeacher();
            teacher.ShowDialog();
            this.Show();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}