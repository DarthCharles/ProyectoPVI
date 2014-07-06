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
using System.Threading;

namespace ControldeAlumnosPVI
{
    class ItemGrupo : FlowLayoutPanel
    {
        Label nombreMateria = new Label();
        PanelParameters panel;
        bool click;
        Grupo grupo;
        List<Alumno> listaAlumnos;

        public Grupo Grupo
        {
            get { return grupo; }
            set { grupo = value; }
        }
        public bool Active
        {
            get { return click; }
            set { click = value; }
        }

        public Label NombreMateria
        {
            get { return nombreMateria; }
            set { nombreMateria = value; }
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
     
            this.panel.Context.ShowTabs();
            this.panel.Context.refreshTables(grupo.IdGrupo);
        }

        public void modifyLabel(string str)
        {
            this.setLabel(str);
            this.NombreMateria.ForeColor = Color.White;
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
     
            this.panel.Context.HideTabs();
        }

    }

}
