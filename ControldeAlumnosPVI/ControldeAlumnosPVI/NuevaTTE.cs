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
    public partial class NuevaTTE : Form
    {
        public NuevaTTE(string str, string str1)
        {
            InitializeComponent();
            label4.Text = str1;
   
            switch (str)
            {
                case "tarea":
                    this.Text = "Nueva tarea";
                    label1.Text = "Asignar nueva tarea";
                    break;

                case "trabajo":
                    this.Text = "Nuevo trabajo";
                    label1.Text = "Asignar nuevo trabajo";
                    break;

                case "examen":
                    this.Text = "Nuevo examen";
                    label1.Text = "Asignar nuevo examen";
                    break;
                default:
                    break;
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
