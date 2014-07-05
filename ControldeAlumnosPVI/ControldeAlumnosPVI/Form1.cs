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
using materia;
using maestro;

namespace ControldeAlumnosPVI
{
    public partial class Form1 : Form
    {
        public int materItemSize = 176;
       public  int grupoItemSize = 160;
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
            tabPage1.Text = "Lista de Asistencia";
            tabPage2.Text = "Tareas";
            tabPage3.Text = "Examenes";
            tabPage4.Text = "Participaciones";
            tabPage5.Text = "Puntos Extra";


            foreach (TabPage tab in tabs_alumnos.TabPages)
            {
                switch (tab.Name)
                {
                    case "tabPage1":
                        tab.Controls.Add(new ListaAsistencia());
                        break;

                    case "tabPage2":
                        tab.Controls.Add(new ListaTareas(20));
                        break;

                    case "tabPage3":
                        tab.Controls.Add(new ListaAlumnos());
                        break;

                    case "tabPage4":
                        tab.Controls.Add(new ListaAlumnos());
                        break;

                    case "tabPage5":
                        tab.Controls.Add(new ListaAlumnos());
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
