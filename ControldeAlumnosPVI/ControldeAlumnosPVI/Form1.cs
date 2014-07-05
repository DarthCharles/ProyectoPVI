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


using materias;


namespace ControldeAlumnosPVI
{
    public partial class Form1 : Form
    {
        public int materItemSize = 176;
        public int grupoItemSize = 160;
        public Form1()
        {
            InitializeComponent();

            Conexion con = new Conexion();

            List<Materia> listaMat = con.readInfoMateriasIdMaestro("1");

            PanelParameters panel = new PanelParameters(this);
            foreach (Materia materia in listaMat)
            {
                panel_materias.Controls.Add(new ItemMaterias(panel, materia.NombreMateria, materia));
            }



            foreach (TabPage tab in tabs_alumnos.TabPages)
            {
                switch (tab.Name)
                {
                    case "tabPage1":
                        tab.Text = "Lista de Asistencia";
                        tab.Controls.Add(new ListaAsistencia());
                        break;

                    case "tabPage2":
                        tab.Text = "Tareas";
                        tab.Controls.Add(new ListaTTE(8));
                        break;

                    case "tabPage3":
                        tab.Text = "Trabajos";
                        tab.Controls.Add(new ListaTTE(3));
                        break;

                    case "tabPage4":

                        tab.Text = "Exámenes";
                        tab.Controls.Add(new ListaTTE(3));
                        break;

                    case "tabPage5":

                        tab.Text = "Participaciones";
                        tab.Controls.Add(new ListaPart_Pextra());

                        break;

                    case "tabPage6":

                        tab.Text = "Puntos Extra";
                        tab.Controls.Add(new ListaPart_Pextra());
                        break;
                    default:
                        break;
                }
            }


        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }






    }
}
