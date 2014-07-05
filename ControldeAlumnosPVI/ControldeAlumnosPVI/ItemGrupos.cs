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
                switch (tab.Name)
                {
                    case "tabPage1":
                        tab.Controls.Clear();
                        tab.Text = "Lista de Asistencia";
                        ListaAsistencia lista = new ListaAsistencia();
                        foreach (Alumno alumno in listaAlumnos)
                        {
                            lista.Rows.Add(alumno.IdAlumno, "1", alumno.NombreAlumno);
                        }
                        lista.Columns[0].Visible = false;
                        tab.Controls.Add(lista);
                        break;

                    case "tabPage2":
                                             tab.Controls.Clear();
                        tab.Text = "Tareas";
                        ListaTTE tareas = new ListaTTE(5);
                        foreach (Alumno alumno in listaAlumnos)
                        {
                            tareas.Rows.Add("1", alumno.NombreAlumno);
                        }
                        //tareas.Columns[0].Visible = false;
                        tab.Controls.Add(tareas);
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
