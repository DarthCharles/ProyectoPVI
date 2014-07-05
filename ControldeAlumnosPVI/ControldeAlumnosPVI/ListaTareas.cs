using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControldeAlumnosPVI
{
    class ListaTareas :ListaAlumnos
    {
        public ListaTareas(int num) : base()
        {
           
             this.ColumnCount = 1 +  num;

             for (int i = 1; i < this.Columns.Count; i++)
             {
                 this.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                 this.Columns[i].Width = 40;
             }

             this.Columns[0].HeaderText = "Alumno";
             this.Columns.Add("Column", "Promedio");
         
 
        }
    }
}
