using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControldeAlumnosPVI
{
    class ListaTotal : ListaAlumnos
    {
        public ListaTotal() : base()
        {
            this.ColumnCount = 3;
            this.Columns[0].Visible = false;
            this.Columns[1].HeaderText = "";
            this.Columns[1].Width = 30;
            this.Columns[1].ReadOnly = true;
            this.Columns[2].HeaderText = "Alumno";

               this.Columns[2].Width = 350;

            this.Columns.Add("Column", "Asist.");
            this.Columns[3].Width = 70;
            this.Columns.Add("Column1", "Tareas");
            this.Columns[4].Width = 70;
            this.Columns.Add("Column2", "Trabajos");
            this.Columns[5].Width = 70;
            this.Columns.Add("Column3", "Examenes");
            this.Columns[6].Width = 70;
            this.Columns.Add("Column4", "Part.");
            this.Columns[7].Width = 70;
            this.Columns.Add("Column5", "P. Extras");
            this.Columns[8].Width = 70;
            this.Columns.Add("Column6", "Promedio");
            this.Columns[ColumnCount - 1].Width = 100;
            this.Columns[ColumnCount - 1].ReadOnly = true;
        }
    }
}
