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
using grupos;


namespace ControldeAlumnosPVI
{
    public partial class Form1 : Form
    {
        public int materItemSize = 176;
        public int grupoItemSize = 160;
        PanelParameters panel;

        public Form1()
        {
            InitializeComponent();
            HideTabs();

            label_fecha.Text = DateTime.Today.ToString("D");
            Conexion con = new Conexion();
            panel = new PanelParameters(this);
            List<Materia> listaMat = con.readInfoMateriasIdMaestro("1");

            foreach (Materia materia in listaMat)
            {
                panel_materias.Controls.Add(new ItemMaterias(panel, materia.NombreMateria, materia));
            }





        }

//METODO DE LOS BOTONES DEL PANEL DE MATERIAS
        Grupo ActiveGrupo()
        {

            foreach (ItemGrupo x in panel_grupos.Controls)
            {
                if (x.Active == true)
                {

                    return x.Grupo;

                }
            }

            return null;
        }

        private void grupos_agregar_Click(object sender, EventArgs e)
        {

            if (ActiveMateria() != null)
            {
                OpcionesGrupo A = new OpcionesGrupo();
                A.ShowDialog();
            }
            else
            {
                MessageBox.Show("Por favor primero seleccione una materia.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void grupos_conf_Click(object sender, EventArgs e)
        {
            if (ActiveGrupo() != null)
            {
                OpcionesGrupo A = new OpcionesGrupo(ActiveGrupo().NombreGrupo);
                A.ShowDialog();
            }
            else
            {
                MessageBox.Show("Por favor primero seleccione un grupo.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

  //METODO DE LOS BOTONES DEL PANEL DE MATERIAS
        Materia ActiveMateria()
        {
            int index = 0;
            foreach (ItemMaterias x in panel_materias.Controls)
            {
                
                if (x.Active == true)
                {
                    x.Materia.Clave = index.ToString();
                    return x.Materia;

                }
                index++;
            }

            return null;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpcionesMateria A = new OpcionesMateria("Agregar nueva materia");
            A.ShowDialog();
            Conexion con = new Conexion();
            List<Materia> listaMat = con.readInfoMateriasIdMaestro("1");
            panel_materias.Controls.Add(new ItemMaterias(panel, listaMat[listaMat.Count - 1].NombreMateria, listaMat[listaMat.Count - 1]));

        }

        private void materias_conf_Click(object sender, EventArgs e)
        {
            if (ActiveMateria() != null)
            {
                Conexion con = new Conexion();
                OpcionesMateria A = new OpcionesMateria("Configurar materia", ActiveMateria().NombreMateria,false,ActiveMateria().IdMateria);
                A.ShowDialog();

                ItemMaterias caca = panel_materias.Controls[int.Parse(ActiveMateria().Clave)] as ItemMaterias;
                caca.modifyLabel(con.readInfoMateriaIdMateria(ActiveMateria().IdMateria).NombreMateria);
                panel_materias.Refresh();
            }
            else
            {
                MessageBox.Show("Por favor primero seleccione una materia.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void materias_eliminar_Click(object sender, EventArgs e)
        {
            if (ActiveMateria() != null)
            {


                DialogResult dialogo = MessageBox.Show("¿Está seguro de querer borrar una materia?", "Advertencia", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
                if (dialogo == DialogResult.Yes)
                {
                    Conexion con = new Conexion();
                    con.deleteMateria(ActiveMateria().IdMateria);
                    panel_materias.Controls.RemoveAt(int.Parse(ActiveMateria().Clave));
                    panel_materias.Refresh();
                }

            }
            else
            {

                MessageBox.Show("Por favor primero seleccione una materia.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


//METODO PARA REFRESCAR LAS TABS
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
    }
}
