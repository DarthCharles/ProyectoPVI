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
    public partial class OpcionesMateria : Form
    {
        public OpcionesMateria(String str)
        {
            InitializeComponent();
            this.Text = str;
        }

        public OpcionesMateria(String str, string nombre)
        {
            InitializeComponent();
            this.Text = "Configurar Materia";
            this.pictureBox1.Image = global::ControldeAlumnosPVI.Properties.Resources.ic_action_1404533629_98;
            textBox1.Text = nombre;
            label1.Text = "Nombre:";
            button1.Text = "Aceptar";
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OpcionesMateria_Load(object sender, EventArgs e)
        {

        }


    }
}
