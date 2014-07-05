﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControldeAlumnosPVI
{
    class ListaTTE : ListaAlumnos
    {
        public ListaTTE(int num)
            : base()
        {

            this.ColumnCount = 3 + num;
            this.Columns[0].Visible = false;
            this.Columns[1].HeaderText = "";
            this.Columns[1].Width = 30;
            this.Columns[2].HeaderText = "Alumno";
            this.Columns[2].Width = 330;
            for (int i = 3; i < this.Columns.Count; i++)
            {
                this.Columns[i].HeaderText = "T" + (i - 2);
                this.Columns[i].Width = 40;
            }


            this.Columns.Add("Column", "Promedio");
            this.Columns[ColumnCount - 1].Width = 100;
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
                for (int i = 3; i < this.Columns.Count - 1; i++)
                {
                   
                    this.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                }

            }
        }
        }
}