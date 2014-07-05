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

        public ListaAlumnos()
        {
            this.ColumnCount = 2;
            this.Columns[0].HeaderText = "Alumno";
            this.Location = new System.Drawing.Point(0, 0);
            this.Size = new System.Drawing.Size(685, 510);
       
            this.BackgroundColor = clr; //cambiamos el color de fondo de la tabla
            this.ColumnHeadersHeight = 30;
            this.EditMode = DataGridViewEditMode.EditOnEnter;

            this.RowHeadersVisible = false;
            this.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;

            DataGridViewCellStyle style = new DataGridViewCellStyle();

            //ESTILO CELDAS
            style.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            style.ForeColor = System.Drawing.SystemColors.WindowText;
            style.SelectionBackColor = Color.YellowGreen;
            style.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            this.DefaultCellStyle = style;
            //ESTILO COLUMNAS

         
            this.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
       
     
        }


    }
}
