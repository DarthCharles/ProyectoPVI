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
using alumnos;
using trabajos;
using maestros;
using Microsoft.Office.Interop.Excel;


namespace ControldeAlumnosPVI
{
    public partial class Form1 : Form
    {
        public int materItemSize = 176;
        public int grupoItemSize = 160;
        PanelParameters panel;

        public Form1(string str1, string str2)
        {
            InitializeComponent();
            HideTabs();
            nombre_profesor.Text = str1 + " " + str2;
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
            int index = 0;

            foreach (ItemGrupo x in panel_grupos.Controls)
            {
                if (x.Active == true)
                {
                    x.Grupo.Clave = index.ToString();
                    return x.Grupo;
                }
                index++;
            }

            return null;
        }

        private void grupos_agregar_Click(object sender, EventArgs e)
        {

            if (ActiveMateria() != null)
            {
                OpcionesGrupo A = new OpcionesGrupo(ActiveMateria().IdMateria, true);
                A.ShowDialog();
                refreshPaneles(false);

            }
            else
            {
                MessageBox.Show("Por favor primero seleccione una materia.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void grupos_conf_Click(object sender, EventArgs e)
        {
            Conexion con = new Conexion();
            if (ActiveGrupo() != null)
            {

                OpcionesGrupo A = new OpcionesGrupo(ActiveGrupo().NombreGrupo, con.readPonderacion(ActiveGrupo().IdPonderacion), ActiveGrupo().IdGrupo,
                                        ActiveGrupo().IdMateria);
                A.ShowDialog();
                ItemGrupo caca = panel_grupos.Controls[int.Parse(ActiveGrupo().Clave)] as ItemGrupo;
                if (OpcionesGrupo.validado)
                {
                    caca.modifyLabel(OpcionesGrupo.nombreGrupo);
                    OpcionesGrupo.validado = false;
                }
                ActiveGrupo().NombreGrupo = OpcionesGrupo.nombreGrupo;
                panel_grupos.Refresh();

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
            refreshPaneles(true);
        }

        private void materias_conf_Click(object sender, EventArgs e)
        {
            if (ActiveMateria() != null)
            {
                Conexion con = new Conexion();
                OpcionesMateria A = new OpcionesMateria("Configurar materia", ActiveMateria().NombreMateria, false, ActiveMateria().IdMateria);
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
                    panel_grupos.Controls.Clear();
                    HideTabs();
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
            tabs_alumnos.TabPages.Remove(tabPage7);
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
            tabs_alumnos.TabPages.Add(tabPage7);
        }

        //METODO PARA REFRESCAR Y CARGAR LAS GRIDS

        public void refreshTables(string idGrupo)
        {
            Conexion con = new Conexion();
            List<Alumno> listaAlumnos = con.readInfoAlumnosGrupo(idGrupo);
            foreach (TabPage tab in panel.Context.tabs_alumnos.TabPages)
            {

                int a = 1;

                switch (tab.Name)
                {
                    case "tabPage1":
                        tab.Controls.Clear();
                        a = 1;
                        ListaAsistencia lista = new ListaAsistencia();
                        foreach (Alumno alumno in listaAlumnos)
                        {
                            lista.Rows.Add(alumno.IdAlumno, a++, alumno.NombreAlumno);

                        }
                        lista.CheckAll();
                        tab.Controls.Add(lista);
                        break;



                    case "tabPage2":
                        tab.Controls.Clear();
                        a = 1;
                        tab.Text = "Tareas";
                        int numtareas = con.countTrabajos(idGrupo, "tarea");
                        ListaTTE tareas = new ListaTTE(numtareas, "tarea");

                        foreach (Alumno alumno in listaAlumnos)
                        {
                            tareas.Rows.Add(alumno.IdAlumno, a++, alumno.NombreAlumno);
                            List<Trabajo> listaTrabajos = con.readInfoTrabajosAlumno(alumno.IdAlumno, "tarea");
                            //int i = 3;
                            //foreach (Trabajo trabajo in listaTrabajos)
                            //{
                            //    tareas.Rows[tareas.RowCount - 1].Cells[i++].Value = trabajo.Calificacion;
                            //}
                        }
                        tareas.Columns[0].Visible = false;
                        tab.Controls.Add(tareas);
                        break;

                    case "tabPage3":
                        a = 1;
                        int numtrabajos = con.countTrabajos(idGrupo, "trabajo");
                        tab.Controls.Clear();
                        ListaTTE trabajos = new ListaTTE(numtrabajos, "trabajo");
                        foreach (Alumno alumno in listaAlumnos)
                        {
                            trabajos.Rows.Add(alumno.IdAlumno, a++, alumno.NombreAlumno);
                            List<Trabajo> listaTrabajos = con.readInfoTrabajosAlumno(alumno.IdAlumno, "trabajo");
                            int i = 3;
                            foreach (Trabajo trabajo in listaTrabajos)
                            {
                                trabajos.Rows[trabajos.RowCount - 1].Cells[i++].Value = trabajo.Calificacion;
                            }
                        }
                        tab.Controls.Add(trabajos);
                        break;

                    case "tabPage4":
                        a = 1;
                        tab.Controls.Clear();

                        int numexamenes = con.countTrabajos(idGrupo, "examen");

                        ListaTTE examenes = new ListaTTE(numexamenes, "examen");
                        foreach (Alumno alumno in listaAlumnos)
                        {
                            examenes.Rows.Add(alumno.IdAlumno, a++, alumno.NombreAlumno);
                            List<Trabajo> listaTrabajos = con.readInfoTrabajosAlumno(alumno.IdAlumno, "examen");
                            int i = 3;
                            foreach (Trabajo trabajo in listaTrabajos)
                            {
                                examenes.Rows[examenes.RowCount - 1].Cells[i++].Value = trabajo.Calificacion;
                            }
                        }

                        tab.Controls.Add(examenes);
                        break;

                    case "tabPage5":
                        tab.Controls.Clear();
                        a = 1;
                        ListaPart_Pextra ass = new ListaPart_Pextra();
                        foreach (Alumno alumno in listaAlumnos)
                        {
                            ass.Rows.Add(alumno.IdAlumno, a++, alumno.NombreAlumno);
                        }
                        tab.Controls.Add(ass);

                        break;

                    case "tabPage6":
                        tab.Controls.Clear();
                        a = 1;
                        ListaPart_Pextra assa = new ListaPart_Pextra();
                        foreach (Alumno alumno in listaAlumnos)
                        {
                            assa.Rows.Add(alumno.IdAlumno, a++, alumno.NombreAlumno);
                        }
                        tab.Controls.Add(assa);
                        break;


                    case "tabPage7":
                        tab.Controls.Clear();
                        a = 1;
                        ListaTotal total = new ListaTotal();
                        foreach (Alumno alumno in listaAlumnos)
                        {
                            total.Rows.Add(alumno.IdAlumno, a++, alumno.NombreAlumno);
                        }
                        tab.Controls.Add(total);
                        break;
                    default:
                        break;
                }
            }
        }

        //METODO PARA REFRESCAR LOS PANELES DE GRUPO Y MATERIA
        private void refreshPaneles(bool materia)
        {
            if (materia)
            {
                Conexion con = new Conexion();
                List<Materia> listaMat = con.readInfoMateriasIdMaestro("1");
                if (listaMat.Count != panel_materias.Controls.Count)
                {
                    panel_materias.Controls.Add(new ItemMaterias(panel, listaMat[listaMat.Count - 1].NombreMateria, listaMat[listaMat.Count - 1]));
                }
            }
            else
            {
                Conexion con = new Conexion();
                List<Grupo> listaGrupo = con.readInfoGruposIdMateria(ActiveMateria().IdMateria);
                if (listaGrupo.Count != panel_grupos.Controls.Count)
                {
                    panel_grupos.Controls.Add(new ItemGrupo(panel, listaGrupo[listaGrupo.Count - 1].NombreGrupo, listaGrupo[listaGrupo.Count - 1]));
                }
            }
        }

        //METODOS DE LOS BOTONES
        private void pictureBox4_Click(object sender, EventArgs e)
        {

            if (ActiveGrupo() != null)
            {
                string idGrupo = ActiveGrupo().IdGrupo;
                NuevaTTE tarea = new NuevaTTE("tarea", ActiveGrupo().NombreGrupo, idGrupo, "tarea");
                tarea.ShowDialog();
                if (tarea.Cambiado)
                {
                    refreshTables(idGrupo);
                }
            }
            else
            {
                MessageBox.Show("Por favor primero seleccione un grupo.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {


            if (ActiveGrupo() != null)
            {
                string idGrupo = ActiveGrupo().IdGrupo;
                NuevaTTE tarea = new NuevaTTE("trabajo", ActiveGrupo().NombreGrupo, ActiveGrupo().IdGrupo, "trabajo");
                tarea.ShowDialog();
                if (tarea.Cambiado)
                {
                    refreshTables(idGrupo);
                }
            }
            else
            {
                MessageBox.Show("Por favor primero seleccione un grupo.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {

            if (ActiveGrupo() != null)
            {
                string idGrupo = ActiveGrupo().IdGrupo;
                NuevaTTE tarea = new NuevaTTE("examen", ActiveGrupo().NombreGrupo, ActiveGrupo().IdGrupo, "examen");
                tarea.ShowDialog();
                if (tarea.Cambiado)
                {
                    refreshTables(idGrupo);
                }
            }
            else
            {
                MessageBox.Show("Por favor primero seleccione un grupo.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            if (ActiveGrupo() != null)
            {
                string idGrupo = ActiveGrupo().IdGrupo;
                NuevaListaAlumnos a = new NuevaListaAlumnos(idGrupo);
                a.ShowDialog();
                this.refreshTables(idGrupo);
            }
            else
            {
                MessageBox.Show("Por favor primero seleccione un grupo.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void grupos_eliminar_Click(object sender, EventArgs e)
        {
            if (ActiveGrupo() != null)
            {
                DialogResult dialogo = MessageBox.Show("¿Está seguro de querer borrar un grupo?", "Advertencia", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
                if (dialogo == DialogResult.Yes)
                {
                    string idGrupo = ActiveGrupo().IdGrupo;
                    Conexion con = new Conexion();
                    con.deleteGrupo(idGrupo);
                    panel_grupos.Controls.RemoveAt(int.Parse(ActiveGrupo().Clave));
                    panel_grupos.Refresh();
                    HideTabs();

                }

            }
            else
            {

                MessageBox.Show("Por favor primero seleccione un grupo.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (ActiveGrupo() != null)
            {
                string idGrupo = ActiveGrupo().IdGrupo;
                NuevoAlumno nuevo = new NuevoAlumno(idGrupo);
                nuevo.ShowDialog();
                refreshTables(idGrupo);


            }
            else
            {
                MessageBox.Show("Por favor primero seleccione un grupo.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void exportar_excel_Click(object sender, EventArgs e)
        {
            DataGridView jo = (DataGridView)tabs_alumnos.SelectedTab.Controls[0];

            // creating Excel Application
            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();


            // creating new WorkBook within Excel application
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);


            // creating new Excelsheet in workbook
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;

            // see the excel sheet behind the program
            app.Visible = true;

            // get the reference of first sheet. By default its name is Sheet1.
            // store its reference to worksheet
            worksheet = workbook.Sheets["Hoja1"];
            worksheet = workbook.ActiveSheet;

            // changing the name of active sheet
            worksheet.Name = tabs_alumnos.SelectedTab.Name;

            ((Range)worksheet.Cells[1, 2]).EntireColumn.ColumnWidth = 30;
            // storing header part in Excel



            // storing header part in Excel
            for (int i = 1; i < jo.Columns.Count; i++)
            {
                
                    worksheet.Cells[1, i] = jo.Columns[i].HeaderText;
               
          
            }



            // storing Each row and column value to excel sheet
            for (int i = 0; i < jo.Rows.Count; i++)
            {
                for (int j = 1; j < jo.Columns.Count; j++)
                {
                    if (jo.Rows[i].Cells[j].Value != null)
                    {
                        worksheet.Cells[i + 2, j ] = jo.Rows[i].Cells[j].Value.ToString();
                    }
                    
                }
            }
 
        }
    }
}

