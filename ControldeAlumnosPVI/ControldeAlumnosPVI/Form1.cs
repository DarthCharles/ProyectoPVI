using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControldeAlumnosPVI
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
       

            panel_materias.Controls.Add(new CustomLabel("Investigación de Operaciones"));
            panel_materias.Controls.Add(new CustomLabel("Base de Datos"));
            panel_materias.Controls.Add(new CustomLabel("Sistemas de Información"));
            panel_materias.Controls.Add(new CustomLabel("Testing"));
            panel_materias.Controls.Add(new CustomLabel("Cultura Emprendedora"));
            panel_materias.Controls.Add(new CustomLabel("Investigación de Operaciones"));
            panel_materias.Controls.Add(new CustomLabel("Base de Datos"));
            panel_materias.Controls.Add(new CustomLabel("Sistemas de Información"));
            panel_materias.Controls.Add(new CustomLabel("Testing"));
            panel_materias.Controls.Add(new CustomLabel("Cultura Emprendedora"));
            panel_materias.Controls.Add(new CustomLabel("Investigación de Operaciones"));
            panel_materias.Controls.Add(new CustomLabel("Base de Datos"));
            panel_materias.Controls.Add(new CustomLabel("Sistemas de Información"));
            panel_materias.Controls.Add(new CustomLabel("Testing"));
            panel_materias.Controls.Add(new CustomLabel("Cultura Emprendedora"));
            panel_materias.Controls.Add(new CustomLabel("Investigación de Operaciones"));
            panel_materias.Controls.Add(new CustomLabel("Base de Datos"));
            panel_materias.Controls.Add(new CustomLabel("Sistemas de Información"));
            panel_materias.Controls.Add(new CustomLabel("Testing"));
            panel_materias.Controls.Add(new CustomLabel("Cultura Emprendedora"));


            panel_grupos.Controls.Add(new CustomLabel("102-AS"));
            panel_grupos.Controls.Add(new CustomLabel("1W02-AF"));
            panel_grupos.Controls.Add(new CustomLabel("0A2-DG"));
            panel_grupos.Controls.Add(new CustomLabel("602-PQ"));
        }

  

        private void panel_materias_MouseEnter(object sender, EventArgs e)
        {
            panel_materias.Focus();
        }

      

   
    }
}
