using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControldeAlumnosPVI
{
    class ListaPart_Pextra : ListaAlumnos
    {
        public ListaPart_Pextra()
        {
            this.ColumnCount = 2 ;
            this.Columns[0].HeaderText = "";
            this.Columns[0].Width = 30;
  
            this.Columns[1].HeaderText = "Alumno";
            this.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            // Initialize the button column.
            DataGridViewButtonColumn buttonColumn =
                new DataGridViewButtonColumn();
   

            buttonColumn.Text = "+";
            // Use the Text property for the button text for all cells rather 
            // than using each cell's value as the text for its own button.
            buttonColumn.UseColumnTextForButtonValue = true;
            buttonColumn.DefaultCellStyle.Padding = new Padding(2,0,2,0);
            this.Columns.Add(buttonColumn);
            this.Columns[2].Width = 40;

            this.Columns.Add("Column", "Total");
            this.Columns[3].Width = 100;


            buttonColumn.Text = "+";

            DataGridViewButtonColumn buttonColumn1 =
          new DataGridViewButtonColumn();


            buttonColumn1.Text = "-";
            // Use the Text property for the button text for all cells rather 
            // than using each cell's value as the text for its own button.
            buttonColumn1.UseColumnTextForButtonValue = true;
            buttonColumn1.DefaultCellStyle.Padding = new Padding(2, 0, 2, 0);
            this.Columns.Add(buttonColumn1);
            this.Columns[4].Width = 40;
            


            this.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(asas);
     
        }

        private void asas(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                int a = Int32.Parse(this.Rows[e.RowIndex].Cells[3].Value.ToString());

                if (e.ColumnIndex == 2)
                {                   
                    a++;
                    this.Rows[e.RowIndex].Cells[3].Value = a;
                }

                if (e.ColumnIndex == 4)
                {
                     a--;
                    this.Rows[e.RowIndex].Cells[3].Value = a;
                }
             
            }
        }
    }
}
