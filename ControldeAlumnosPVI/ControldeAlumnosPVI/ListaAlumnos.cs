using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControldeAlumnosPVI
{
    class ListaAlumnos : DataGridView
    {
        Color clr = Color.FromArgb(227, 241, 174);
        Color clr2 = Color.FromArgb(193, 224, 101);

        public ListaAlumnos()
        {
            this.ColumnCount = 3;
            this.Columns[2].HeaderText = "Alumno";
            this.Columns[0].ReadOnly = true;
            this.Columns[1].ReadOnly = true;
            this.Columns[2].ReadOnly = true;
            this.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            this.Location = new System.Drawing.Point(0, 0);
            this.Size = new System.Drawing.Size(300, 510);
            this.BackgroundColor = Color.White; //cambiamos el color de fondo de la tabla
            this.ColumnHeadersHeight = 30;
            this.EditMode = DataGridViewEditMode.EditOnEnter;
            this.Dock = System.Windows.Forms.DockStyle.Fill; ;
            this.RowHeadersVisible = true;
      
            this.AllowUserToAddRows = false;
            DataGridViewCellStyle style = new DataGridViewCellStyle();

            //ESTILO CELDAS
            style.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            style.ForeColor = System.Drawing.SystemColors.WindowText;
            style.SelectionBackColor = clr2;
            style.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.DefaultCellStyle = style;
          
            //ESTILO COLUMNAS
            this.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
       
     
        }


    }
}
