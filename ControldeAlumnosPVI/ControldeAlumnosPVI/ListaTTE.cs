using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControldeAlumnosPVI
{
    class ListaTTE : ListaAlumnos
    {
        public ListaTTE(int num, string str)
            : base()
        {

            this.ColumnCount = 3 + num;
            this.Columns[0].Visible = false;
            this.Columns[1].HeaderText = "";
            this.Columns[1].Width = 30;
            this.Columns[1].ReadOnly = true;
            this.Columns[2].HeaderText = "Alumno";
            this.Columns[2].Width = 330;
            this.Columns[2].ReadOnly = true;
     


            switch (str)
            {
                case "tarea":
                    for (int i = 3; i < this.Columns.Count; i++)
                    {
                        this.Columns[i].HeaderText = "T" + (i - 2);
                        this.Columns[i].Width = 40;
                    }
                    break;

                case "trabajo":
                    for (int i = 3; i < this.Columns.Count; i++)
                    {
                        this.Columns[i].HeaderText = "TR" + (i - 2);
                        this.Columns[i].Width = 40;
                    }
                    break;

                case "examen":
                   for (int i = 3; i < this.Columns.Count; i++)
                    {
                        this.Columns[i].HeaderText = "E" + (i - 2);
                        this.Columns[i].Width = 40;
                    }
                    break;
            }

            this.Columns.Add("Column", "Promedio");
            this.Columns[ColumnCount - 1].Width = 100;
            this.Columns[ColumnCount - 1].ReadOnly = true;
            adjustColumns();

    }

        private void adjustColumns(){
            int width = 0;

            foreach (DataGridViewColumn col in this.Columns)
            {
                width += col.Width;
            }
            if (width < 685)
            {
                for (int i = 3; i < this.Columns.Count ; i++)
                {
                   
                    this.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                }

            }
        }
        }
}
