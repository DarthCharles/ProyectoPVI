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
        ListaTTE tareas;
        ListaTTE trabajos;
        ListaTTE examenes;
        ListaAsistencia lista;
        List<Alumno> listaAlumnos;
        ListaTotal total;
        ListaPart_Pextra ass;
        ListaPart_Pextra assa;

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
            listaAlumnos = con.readInfoAlumnosGrupo(idGrupo);
            foreach (TabPage tab in panel.Context.tabs_alumnos.TabPages)
            {

                int a = 1;

                switch (tab.Name)
                {
                    case "tabPage1":
                        tab.Controls.Clear();
                        a = 1;
                        lista = new ListaAsistencia();
                        foreach (Alumno alumno in listaAlumnos)
                        {
                            lista.Rows.Add(alumno.IdAlumno, a++, alumno.NombreAlumno);
                            (lista.Rows[lista.RowCount - 1].Cells[3] as DataGridViewCheckBoxCell).Value = con.loadLista(alumno.IdAlumno,
                                DateTime.Today.ToString("yyyy-MM-dd")).ToString();
                        }
                        tab.Controls.Add(lista);
                   
                        break;



                    case "tabPage2":
                        tab.Controls.Clear();
                        a = 1;
                        tab.Text = "Tareas";
                        int numtareas = con.countTrabajos(idGrupo, "tarea");
                        tareas = new ListaTTE(numtareas, "tarea");
                        List<Trabajo> tareasGrupo = con.readInfoTrabajosGrupo(idGrupo, "tarea");

                        foreach (Alumno alumno in listaAlumnos)
                        {
                            tareas.Rows.Add(alumno.IdAlumno, a++, alumno.NombreAlumno);
                            for (int i = 0; i < numtareas; i++)
                            {
                                List<Trabajo> listaTrabajos = con.readInfoTrabajosAlumno(alumno.IdAlumno, tareasGrupo[i].IdTrabajo);

                                if (listaTrabajos.Count != 0)
                                    tareas.Rows[tareas.RowCount - 1].Cells[i + 3].Value = listaTrabajos[0].Calificacion;
                            }
                        }
                        tareas.Columns[0].Visible = false;
                        tab.Controls.Add(tareas);
                        promediar(tareas, numtareas);

                        break;

                    case "tabPage3":
                        a = 1;
                        int numtrabajos = con.countTrabajos(idGrupo, "trabajo");
                        tab.Controls.Clear();
                        trabajos = new ListaTTE(numtrabajos, "trabajo");
                        List<Trabajo> trabajosGrupo = con.readInfoTrabajosGrupo(idGrupo, "trabajo");

                        foreach (Alumno alumno in listaAlumnos)
                        {
                            trabajos.Rows.Add(alumno.IdAlumno, a++, alumno.NombreAlumno);
                            for (int i = 0; i < numtrabajos; i++)
                            {
                                List<Trabajo> listaTrabajos = con.readInfoTrabajosAlumno(alumno.IdAlumno, trabajosGrupo[i].IdTrabajo);

                                if (listaTrabajos.Count != 0)
                                    trabajos.Rows[trabajos.RowCount - 1].Cells[i + 3].Value = listaTrabajos[0].Calificacion;
                            }
                        }
                        tab.Controls.Add(trabajos);
                        promediar(trabajos, numtrabajos);
                        break;

                    case "tabPage4":
                        a = 1;
                        tab.Controls.Clear();

                        int numexamenes = con.countTrabajos(idGrupo, "examen");

                        examenes = new ListaTTE(numexamenes, "examen");
                        List<Trabajo> examenesGrupo = con.readInfoTrabajosGrupo(idGrupo, "examen");

                        foreach (Alumno alumno in listaAlumnos)
                        {
                            examenes.Rows.Add(alumno.IdAlumno, a++, alumno.NombreAlumno);
                            for (int i = 0; i < numexamenes; i++)
                            {
                                List<Trabajo> listaExamenes = con.readInfoTrabajosAlumno(alumno.IdAlumno, examenesGrupo[i].IdTrabajo);

                                if (listaExamenes.Count != 0)
                                    examenes.Rows[examenes.RowCount - 1].Cells[i + 3].Value = listaExamenes[0].Calificacion;
                            }
                        }

                        tab.Controls.Add(examenes);
                        promediar(examenes, numexamenes);
                        break;

                    case "tabPage5":
                        tab.Controls.Clear();
                        a = 1;
                        ass = new ListaPart_Pextra();
                        foreach (Alumno alumno in listaAlumnos)
                        {
                            ass.Rows.Add(alumno.IdAlumno, a++, alumno.NombreAlumno);
                            ass.Rows[ass.RowCount - 1].Cells[4].Value = (con.numeroParticipaciones(alumno.IdAlumno));
                        }
                        tab.Controls.Add(ass);

                        break;

                    case "tabPage6":
                        tab.Controls.Clear();
                        a = 1;
                        assa = new ListaPart_Pextra();
                        foreach (Alumno alumno in listaAlumnos)
                        {
                            assa.Rows.Add(alumno.IdAlumno, a++, alumno.NombreAlumno);
                            assa.Rows[assa.RowCount - 1].Cells[4].Value = (con.numPuntosExtra(alumno.IdAlumno));

                        }
                        tab.Controls.Add(assa);
                        break;


                    case "tabPage7":
                        tab.Controls.Clear();
                        a = 1;
                        total = new ListaTotal();
                        foreach (Alumno alumno in listaAlumnos)
                        {
                            total.Rows.Add(alumno.IdAlumno, a++, alumno.NombreAlumno);
                            total.Rows[total.RowCount - 1].Cells[3].Value = (con.numeroAsistencias(alumno.IdAlumno) + "/" +
                                (int.Parse(con.numeroFaltas(alumno.IdAlumno)) +
                                int.Parse(con.numeroAsistencias(alumno.IdAlumno))));
                        }
                        tab.Controls.Add(total);
                        break;
                    default:
                        break;
                }
            }
        }

        public void promediar(DataGridView tareas, int numtareas)
        {
            try
            {

      
            int promedio = 0;

            if (numtareas > 0)
            {
                for (int i = 0; i < tareas.Rows.Count; i++)
                {
                    for (int j = 3; j < tareas.Columns.Count - 1; j++)
                    {
                        promedio += int.Parse(tareas.Rows[i].Cells[j].Value.ToString());
                    }
                    tareas.Rows[i].Cells[tareas.ColumnCount - 1].Value = (promedio / numtareas);
                    promedio = 0;
                }
            }
            else
            {
                for (int i = 0; i < tareas.Rows.Count; i++)
                {
                    tareas.Rows[i].Cells[tareas.ColumnCount - 1].Value = 0;
                }
            }
            }
            catch (Exception)
            {

  
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
                NuevaTTE tarea = new NuevaTTE("trabajo", ActiveGrupo().NombreGrupo, idGrupo, "trabajo");
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
                NuevaTTE tarea = new NuevaTTE("examen", ActiveGrupo().NombreGrupo, idGrupo, "examen");
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
                if (a.Cambiado)
                {
                    refreshTables(idGrupo);
                }
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
                if (nuevo.Cambiado)
                {
                    refreshTables(idGrupo);
                }
            }
            else
            {
                MessageBox.Show("Por favor primero seleccione un grupo.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            if (ActiveGrupo() != null)
            {
                
            
            label_fecha.Focus();

         

                    guardarLista();

                    guardarTareas();
        
                    guardarTrabajos();
         
                    guardarExamenes();
 
                    guardarParticipaciones();
    
                    guardarPuntos();

                    MessageBox.Show("Datos guardados con éxito.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    refreshTables(ActiveGrupo().IdGrupo);

            }
        }

        public void guardarTareas()
        {
            Conexion con = new Conexion();
            string idGrupo = ActiveGrupo().IdGrupo;
            int numtareas = con.countTrabajos(idGrupo, "tarea");
            List<Trabajo> trabajos = con.readInfoTrabajosGrupo(idGrupo, "tarea");
            for (int j = 0; j < tareas.RowCount; j++)
            {
                for (int i = 0; i <= numtareas - 1; i++)
                {
                    string calificacion = "0";
                    try
                    {
                        calificacion = tareas.Rows[j].Cells[i + 3].Value.ToString();
                    }
                    catch (Exception) { }

                    con.registroTarea(tareas.Rows[j].Cells[0].Value.ToString(),
                        trabajos[i].IdTrabajo, calificacion,
                        "tarea");
                }
            }

        }

        public void guardarTrabajos()
        {
            Conexion con = new Conexion();
            string idGrupo = ActiveGrupo().IdGrupo;
            int numtrabajos = con.countTrabajos(idGrupo, "trabajo");
            List<Trabajo> trabajoslist = con.readInfoTrabajosGrupo(idGrupo, "trabajo");
            for (int j = 0; j < trabajos.RowCount; j++)
            {
                for (int i = 0; i <= numtrabajos - 1; i++)
                {
                    string calificacion = "0";
                    try
                    {
                        calificacion = trabajos.Rows[j].Cells[i + 3].Value.ToString();
                    }
                    catch (Exception) { }

                    con.registroTarea(tareas.Rows[j].Cells[0].Value.ToString(),
                        trabajoslist[i].IdTrabajo, calificacion,
                        "trabajo");
                }
            }

        }

        public void guardarExamenes()
        {
            Conexion con = new Conexion();
            string idGrupo = ActiveGrupo().IdGrupo;
            int numexamenes = con.countTrabajos(idGrupo, "examen");
            List<Trabajo> exameneslist = con.readInfoTrabajosGrupo(idGrupo, "examen");
            for (int j = 0; j < examenes.RowCount; j++)
            {
                for (int i = 0; i <= numexamenes - 1; i++)
                {
                    string calificacion = "0";
                    try
                    {
                        calificacion = examenes.Rows[j].Cells[i + 3].Value.ToString();
                    }
                    catch (Exception) { }

                    con.registroTarea(tareas.Rows[j].Cells[0].Value.ToString(),
                        exameneslist[i].IdTrabajo, calificacion,
                        "examen");
                }
            }
        }

        public void guardarLista()
        {
            Conexion con = new Conexion();

            for (int i = 0; i < lista.RowCount; i++)
            {
                con.tomarLista(listaAlumnos[i].IdAlumno, DateTime.Today.ToString("yyyy-MM-dd"), lista.Rows[i].Cells[3].Value.ToString());
            }

        }

        public void guardarParticipaciones()
        {
            Conexion con = new Conexion();
            for (int i = 0; i < lista.RowCount; i++)
            {
                con.tomarPart(listaAlumnos[i].IdAlumno, ass.Rows[i].Cells[4].Value.ToString());
            }
        }

        public void guardarPuntos()
        {
            Conexion con = new Conexion();
            for (int i = 0; i < lista.RowCount; i++)
            {
                con.tomarPuntos(listaAlumnos[i].IdAlumno, assa.Rows[i].Cells[4].Value.ToString());
            }
        }

        private void exportar_excel_Click(object sender, EventArgs e)
        {
            if (ActiveGrupo() != null)
            {

                DataGridView jo = (DataGridView)tabs_alumnos.SelectedTab.Controls[0];

                // crea una aplixacion de excel
                Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
                Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
                app.Visible = true;
                worksheet = workbook.Sheets["Hoja1"];
                worksheet = workbook.ActiveSheet;

                worksheet.Name = tabs_alumnos.SelectedTab.Text;

                ((Range)worksheet.Cells[1, 2]).EntireColumn.ColumnWidth = 30;


                for (int i = 1; i < jo.Columns.Count; i++)
                {
                    worksheet.Cells[1, i] = jo.Columns[i].HeaderText;

                }

                for (int i = 0; i < jo.Rows.Count; i++)
                {
                    for (int j = 1; j < jo.Columns.Count; j++)
                    {
                        if (jo.Rows[i].Cells[j].Value != null)
                        {
                            worksheet.Cells[i + 2, j] = jo.Rows[i].Cells[j].Value.ToString();
                        }
                    }
                }
                this.Focus();
            }
            else
            {
                MessageBox.Show("Por favor primero seleccione un grupo.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void delete_alumnos_Click(object sender, EventArgs e)
        {
            DialogResult eliminar = eliminar = MessageBox.Show("¿Está seguro de querer eliminar?", "Alerta", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (eliminar == DialogResult.Yes)
            {


                if (ActiveGrupo() != null)
                {
                    try
                    {
                        Conexion con = new Conexion();
                        DataGridView datos = (DataGridView)tabs_alumnos.SelectedTab.Controls[0];
                        con.deleteAlumnoGrupo(datos.Rows[datos.SelectedCells[0].RowIndex].Cells[0].Value.ToString());
                        datos.Rows.RemoveAt(datos.SelectedCells[0].RowIndex);
                        refreshTables(ActiveGrupo().IdGrupo);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Nada que eliminar", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Por favor primero seleccione un grupo.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
}

