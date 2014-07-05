﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControldeAlumnosPVI
{
    class ListaAsistencia : ListaAlumnos
    {
        public ListaAsistencia() : base()
        {
            this.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; //ajustamos el tamaño de las columnas
            this.ColumnCount = 1;
            DataGridViewCheckBoxColumn a = new DataGridViewCheckBoxColumn();
            this.Columns.Add(a);
            this.Columns[0].HeaderText = "Alumno";
            this.Columns[1].HeaderText = "Asistencia";
        }
    }
}