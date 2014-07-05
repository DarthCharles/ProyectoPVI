using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using materias;
using System.Drawing;
using grupos;
using database;
using alumnos;
using trabajos;

namespace ControldeAlumnosPVI
{
    class ItemGrupo : FlowLayoutPanel
    {
        Label nombreMateria = new Label();
        PanelParameters panel;
        bool click;
        Grupo grupo;
        List<Alumno> listaAlumnos;

        public bool Active
        {
            get { return click; }
            set { click = value; }
        }




        public ItemGrupo(PanelParameters caca, string nombre, Grupo m)
        {
            panel = caca;
            setLabel(nombre);
            this.Controls.Add(nombreMateria);
            this.Location = new System.Drawing.Point(3, 3);
            this.AutoSize = true;
            this.MinimumSize = new System.Drawing.Size(panel.Context.grupoItemSize, 0);
            this.MouseEnter += new System.EventHandler(this.PanelMouseEnter);
            this.MouseLeave += new System.EventHandler(this.PanelMouseLeave);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(PanelMouseClick);
            this.TabIndex = 0;
            this.grupo = m;
            Conexion con = new Conexion();
            listaAlumnos = con.readInfoAlumnosGrupo(m.IdGrupo);

        }


        private void setLabel(string nombre)
        {
            //nombreMateria.Padding = new System.Windows.Forms.Padding(5);

            nombreMateria.AutoSize = true;
            nombreMateria.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            nombreMateria.ForeColor = System.Drawing.Color.Black;
            nombreMateria.MouseEnter += new System.EventHandler(this.PanelMouseEnter);
            nombreMateria.MouseLeave += new System.EventHandler(this.PanelMouseLeave);
            nombreMateria.MouseClick += new System.Windows.Forms.MouseEventHandler(PanelMouseClick);
            nombreMateria.Padding = new System.Windows.Forms.Padding(5);
            nombreMateria.Size = new System.Drawing.Size(74, 36);

            nombreMateria.Text = nombre;
        }
        Color clr = Color.FromArgb(193, 224, 101);

        private void PanelMouseClick(object sender, MouseEventArgs e)
        {
            RefreshList();
            this.BackColor = clr;
            nombreMateria.ForeColor = System.Drawing.Color.White;
            click = true;

            foreach (TabPage tab in panel.Context.tabs_alumnos.TabPages)
            {
                Conexion con = new Conexion();

                switch (tab.Name)
                {
                    case "tabPage1":
                        tab.Controls.Clear();

                        ListaAsistencia lista = new ListaAsistencia();
                        foreach (Alumno alumno in listaAlumnos)
                        {
                            lista.Rows.Add(alumno.IdAlumno, "1", alumno.NombreAlumno);
                        }
                        lista.CheckAll();
                        tab.Controls.Add(lista);
                        break;



                    case "tabPage2":
                        tab.Controls.Clear();
                        tab.Text = "Tareas";
<<<<<<< HEAD
                        int numtareas = con.countTrabajos(grupo.IdGrupo, "tarea");
=======

                        
                          Conexion con = new Conexion();

                        int numtareas = con.countTrabajos(grupo.IdGrupo);
>>>>>>> origin/master
                        ListaTTE tareas = new ListaTTE(numtareas);

                        foreach (Alumno alumno in listaAlumnos)
                        {
                            tareas.Rows.Add(alumno.IdAlumno, "1", alumno.NombreAlumno);
                            List<Trabajo> listaTrabajos = con.readInfoTrabajosAlumno(alumno.IdAlumno, "tarea");
                            int i = 3;
                            foreach (Trabajo trabajo in listaTrabajos)
                            {
                                tareas.Rows[tareas.RowCount - 1].Cells[i++].Value = trabajo.Calificacion;
                            }
                        }
                        tareas.Columns[0].Visible = false;
                        tab.Controls.Add(tareas);
                        break;

                    case "tabPage3":
                        int numtrabajos = con.countTrabajos(grupo.IdGrupo, "trabajo");
                        tab.Controls.Clear();
                        ListaTTE trabajos = new ListaTTE(numtrabajos);
                        foreach (Alumno alumno in listaAlumnos)
                        {
                            trabajos.Rows.Add(alumno.IdAlumno, "1", alumno.NombreAlumno);
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

                        tab.Controls.Clear();

                        int numexamenes = con.countTrabajos(grupo.IdGrupo, "examen");

                        ListaTTE examenes = new ListaTTE(numexamenes);
                        foreach (Alumno alumno in listaAlumnos)
                        {
                            examenes.Rows.Add(alumno.IdAlumno, "1", alumno.NombreAlumno);
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

                        tab.Controls.Add(new ListaPart_Pextra());

                        break;

                    case "tabPage6":
                        tab.Controls.Add(new ListaPart_Pextra());
                        break;
                    default:
                        break;
                }
            }



        }

        private void PanelMouseEnter(object sender, System.EventArgs e)
        {
            //
            // Set the font and forecolor when the cursor hovers over a label.
            //

            this.BackColor = clr;
            this.nombreMateria.ForeColor = System.Drawing.Color.White;

        }

        private void PanelMouseLeave(object sender, System.EventArgs e)
        {
            //
            // Reset the font and forecolor when the mouse stops hovering over a label.
            //

            if (!click)
            {
                this.BackColor = System.Drawing.Color.White;

                this.nombreMateria.ForeColor = System.Drawing.Color.Black;
            }
        }

        public void RefreshList()
        {
            foreach (ItemGrupo x in panel.PanelGrupos.Controls)
            {
                x.BackColor = System.Drawing.Color.White;
                x.nombreMateria.ForeColor = System.Drawing.Color.Black;
                x.Active = false;
            }
        }

    }

}
