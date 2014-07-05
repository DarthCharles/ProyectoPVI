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
using System.Threading;


namespace ControldeAlumnosPVI
{
    public partial class Form1 : Form
    {
        public int materItemSize = 176;
        public int grupoItemSize = 160;
        public Form1()
        {
            InitializeComponent();
            HideTabs();

            label_fecha.Text = DateTime.Today.ToString("D");
            Conexion con = new Conexion();

            List<Materia> listaMat = con.readInfoMateriasIdMaestro("1");

            PanelParameters panel = new PanelParameters(this);
            foreach (Materia materia in listaMat)
            {
                panel_materias.Controls.Add(new ItemMaterias(panel, materia.NombreMateria, materia));
            }





        }
        public void HideTabs()
        {




            tabs_alumnos.TabPages.Remove(tabPage6);
            tabs_alumnos.TabPages.Remove(tabPage5);
            tabs_alumnos.TabPages.Remove(tabPage4);
            tabs_alumnos.TabPages.Remove(tabPage3);
            tabs_alumnos.TabPages.Remove(tabPage2);
            tabs_alumnos.TabPages.Remove(tabPage1);




        }

        public void ShowTabs()
        {

            tabs_alumnos.TabPages.Add(tabPage1);
            tabs_alumnos.TabPages.Add(tabPage2);
            tabs_alumnos.TabPages.Add(tabPage3);
            tabs_alumnos.TabPages.Add(tabPage4);
            tabs_alumnos.TabPages.Add(tabPage5);
            tabs_alumnos.TabPages.Add(tabPage6);





        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpcionesMateria A = new OpcionesMateria("Agregar nueva materia");
            A.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void grupos_agregar_Click(object sender, EventArgs e)
        {

            if (ActiveMateria() != null)
            {
                OpcionesMateria A = new OpcionesMateria("Agregar nuevo grupo");
                A.ShowDialog();
            }
            else
            {
                MessageBox.Show("Por favor primero seleccione una materia.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        Materia ActiveMateria()
        {
    
            foreach (ItemMaterias x in panel_materias.Controls)
            {
                if (x.Active == true)
                {
            
                    return x.Materia;

                }
            }

            return null;
        }

        private void materias_conf_Click(object sender, EventArgs e)
        {
            if (ActiveMateria() != null)
            {
                OpcionesMateria A = new OpcionesMateria("Configurar materia", ActiveMateria().NombreMateria);
                A.ShowDialog();
            }
            else
            {
                MessageBox.Show("Por favor primero seleccione una materia.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
       
        


        }






    }
}
