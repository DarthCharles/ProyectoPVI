using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControldeAlumnosPVI
{
    class ListaAsistencia : ListaAlumnos
    {
        public ListaAsistencia()
            : base()
        {
            this.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; //ajustamos el tamaño de las columnas
            this.ColumnCount = 3;

            this.Columns[0].Visible = false;
            this.Columns[1].HeaderText = "";
            this.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            this.Columns[1].Width = 30;
            DataGridViewCheckBoxColumn a = new DataGridViewCheckBoxColumn();

            this.Columns.Add(a);
            this.Columns[2].HeaderText = "Alumno";
            this.Columns[3].HeaderText = "Asistencia";


        }

        public void CheckAll()
        {
            foreach (DataGridViewRow row in this.Rows)
            {

                row.Cells[3].Value = true;
            }
        }
    }
}
