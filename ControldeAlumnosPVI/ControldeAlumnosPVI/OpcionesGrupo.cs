using database;
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
using grupos;

namespace ControldeAlumnosPVI
{
    public partial class OpcionesGrupo : Form
    {
        string idMateria;

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

        public OpcionesGrupo(string idMateria, bool materia)
        {
            InitializeComponent();
            this.idMateria = idMateria;
        }

        private void OpcionesGrupo_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (idMateria != null)
            {
                Conexion con = new Conexion();
                Grupo grupo = new Grupo();
                grupo.NombreGrupo = textBox1.Text;
                grupo.IdMateria = idMateria;
                con.createPonderacion(textBox2.Text,textBox3.Text,textBox4.Text,textBox5.Text,textBox6.Text,grupo);
                this.Close();
            }
        }

    }
}
