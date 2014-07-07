using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using database;
using trabajos;
using Microsoft.Office.Interop.Excel;

namespace ControldeAlumnosPVI
{
    public partial class NuevaTTE : Form
    {
        string tipo;
        string idGrupo;
        string clave;
        List<Trabajo> trabajos;
        bool cambiado;
        string str;
        public bool Cambiado
        {
            get { return cambiado; }
            set { cambiado = value; }
        }

        private bool validar()
        {
            if (textBox1.Text == "") return false;
            return true;

        }
        public NuevaTTE(string str, string str1, string idGrupo, string tipo)
        {
            InitializeComponent();
            dataGridView1.Columns.Add("c2", "Nombre");
            dataGridView1.Columns.Add("c2", "Clave");
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[1].Width = 50;
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridView1.AllowUserToAddRows = false;
            label4.Text = str1;

            this.idGrupo = idGrupo;
            this.tipo = tipo;
            this.str = str;
            switch (str)
            {
                case "tarea":
                    this.Text = "Nueva tarea";
                    label1.Text = "Asignar nueva tarea";
                    this.detalles.Text = "Detalles de tareas";
                    this.pictureBox1.Image = global::ControldeAlumnosPVI.Properties.Resources.ic_nuevo_trabajo_small;
                    clave = "T";
                    break;

                case "trabajo":
                    this.Text = "Nuevo trabajo";
                    label1.Text = "Asignar nuevo trabajo";
                    this.detalles.Text = "Detalles de trabajos";
                    this.pictureBox1.Image = global::ControldeAlumnosPVI.Properties.Resources.ic_nuevo_examen1;
                    clave = "TR";
                    break;

                case "examen":
                    this.Text = "Nuevo examen";
                    label1.Text = "Asignar nuevo examen";
                    this.detalles.Text = "Detalles de exámenes";
                    this.pictureBox1.Image = global::ControldeAlumnosPVI.Properties.Resources.ic_examen_small;
                    clave = "E";
                    break;
                default:
                    break;
            }
            llenarTrabajos(idGrupo, tipo);


        }



        private void llenarTrabajos(string idGrupo, string tipo)
        {
            Conexion con = new Conexion();
            trabajos = con.readInfoTrabajosGrupo(idGrupo, tipo);
            foreach (Trabajo trabajo in trabajos)
            {
                dataGridView1.Rows.Add(trabajo.Nombre, clave + "" + (dataGridView1.Rows.Count + 1));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[0].Value.ToString()

            Conexion con = new Conexion();
            con.delete(trabajos[dataGridView1.SelectedCells[0].RowIndex].IdTrabajo);
            dataGridView1.Rows.RemoveAt(dataGridView1.SelectedCells[0].RowIndex);
            Cambiado = true;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (validar())
            {


                Conexion con = new Conexion();
                Trabajo trabajo = new Trabajo(textBox1.Text, tipo, "1", idGrupo);
                con.create(trabajo);
                trabajos = con.readInfoTrabajosGrupo(idGrupo, tipo);
                dataGridView1.Rows.Add(textBox1.Text, clave + "" + (dataGridView1.Rows.Count + 1));
                textBox1.Text = "";
                Cambiado = true;

            }
            else
            {
                MessageBox.Show("Por favor ingrese un nombre válido.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void Exportar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Desea exportar una lista con los detalles de " + str + "?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                DataGridView jo = dataGridView1;

                // crea una aplixacion de excel
                Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
                Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
                app.Visible = true;
                worksheet = workbook.Sheets["Hoja1"];
                worksheet = workbook.ActiveSheet;

                worksheet.Name = str.ToUpper();

                ((Range)worksheet.Cells[1, 1]).EntireColumn.ColumnWidth = 30;


                for (int i = 1; i < jo.Columns.Count; i++)
                {
                    worksheet.Cells[1, i] = jo.Columns[i - 1].HeaderText;

                }

                for (int i = 0; i < jo.Rows.Count; i++)
                {
                    for (int j = 0; j < jo.Columns.Count; j++)
                    {
                        if (jo.Rows[i].Cells[j].Value != null)
                        {
                            worksheet.Cells[i + 2, j + 1] = jo.Rows[i].Cells[j].Value.ToString();
                        }
                    }
                }

                this.Focus();
            }
        }




    }
}
