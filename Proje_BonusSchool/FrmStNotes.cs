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

namespace Proje_BonusSchool
{
    public partial class FrmStNotes : Form
    {
        public FrmStNotes()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-IOMSQH7\SQLEXPRESS;Initial Catalog=BonusSchool;Integrated Security=True;");
        public string number;
        private void FrmStNotes_Load(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand(
                @"SELECT 
            TblLessons.LessonName,
            TblStudents.StName,
            TblNotes.Exam1,
            TblNotes.Exam2,
            TblNotes.Exam3,
            TblNotes.Project,
            TblNotes.Average,
            CASE WHEN TblNotes.Status = 1 THEN 'True' ELSE 'False' END AS Status
        FROM TblNotes
        INNER JOIN TblLessons ON TblNotes.LessonId = TblLessons.ID
        INNER JOIN TblStudents ON TblNotes.StId = TblStudents.StID
        WHERE TblNotes.StId = @p1", baglanti);

            komut.Parameters.AddWithValue("@p1", number); // globalde tanımlı öğrenci ID

            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);

            // Form başlığına öğrenci adı yaz
            if (dt.Rows.Count > 0)
            {
                this.Text = dt.Rows[0]["StName"].ToString();
            }
            else
            {
                this.Text = "Öğrenci Bulunamadı";
            }

            // DataGridView'e veri kaynağı atama
            dataGridView1.DataSource = dt;

            // StName sütununu DataGridView'de gizle
            if (dataGridView1.Columns["StName"] != null)
            {
                dataGridView1.Columns["StName"].Visible = false;
            }
        }
    }
}
