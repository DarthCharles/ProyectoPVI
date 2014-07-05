using materias;
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
    public partial class OpcionesGrupo : Form
    {
        public OpcionesGrupo()
        {
            InitializeComponent();
        }

        public OpcionesGrupo(string str)
        {
            InitializeComponent();
            this.Text = "Configurar";
            this.label1.Text = "Configurar Grupo";
            this.pictureBox1.Image = global::ControldeAlumnosPVI.Properties.Resources.ic_action_1404533629_98;
            this.textBox1.Text = str;
        }

        private void OpcionesGrupo_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
