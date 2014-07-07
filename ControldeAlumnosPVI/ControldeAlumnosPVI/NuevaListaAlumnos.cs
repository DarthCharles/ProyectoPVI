using alumnos;
using database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControldeAlumnosPVI
{
    public partial class NuevaListaAlumnos : Form
    {
        string idGrupo;
        public NuevaListaAlumnos(string str)
        {
            this.idGrupo = str;
            InitializeComponent();


        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {


                OpenFileDialog _file = new OpenFileDialog();
                _file.Title = "Seleccione Archivo";
                _file.InitialDirectory = @"C:";
                _file.Filter = "Archivos XLSX(*.xlsx)|*.xlsx";
                _file.FilterIndex = 1;
                _file.RestoreDirectory = true;


                if (_file.ShowDialog() == DialogResult.OK)
                {

   
                        String archivo = _file.FileName;
                        textBox1.Text = archivo;
                        String name = "Hoja1";
                        String constr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                                        archivo +
                                        ";Extended Properties='Excel 12.0 XML;HDR=YES;';";

                        OleDbConnection con = new OleDbConnection(constr);
                        OleDbCommand oconn = new OleDbCommand("Select * From [" + name + "$]", con);
                        con.Open();

                        OleDbDataAdapter sda = new OleDbDataAdapter(oconn);
                        DataTable data = new DataTable();
                        sda.Fill(data);
                        dataGridView1.DataSource = data;
                        dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                   


                    
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Conexion con = new Conexion();
            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (!cell.Size.IsEmpty && cell.Value != null)
                    {

                        Alumno a = new Alumno();
                        a.NombreAlumno = cell.Value.ToString().ToUpper();
                        a.Foto = "1";
                        a.IdGrupo = idGrupo;
                        con.createAlumnoIdGrupo(a);


                    }

                }
            }

            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
