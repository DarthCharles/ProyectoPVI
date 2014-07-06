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
        string clave;
        public NuevaTTE(string str, string str1)
        {
            InitializeComponent();
            label4.Text = str1;
   
            switch (str)
            {
                case "tarea":
                    this.Text = "Nueva tarea";
                    label1.Text = "Asignar nueva tarea";
                    clave = "T";
                    break;

                case "trabajo":
                    this.Text = "Nuevo trabajo";
                    label1.Text = "Asignar nuevo trabajo";
                    clave = "TR";
                    break;

                case "examen":
                    this.Text = "Nuevo examen";
                    label1.Text = "Asignar nuevo examen";
                    clave = "E";
                    break;
                default:
                    break;
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void NuevaTTE_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
          
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Add(textBox1.Text, clave+"" +(dataGridView1.Rows.Count+1));
            textBox1.Text = "";
        }


    }
}
