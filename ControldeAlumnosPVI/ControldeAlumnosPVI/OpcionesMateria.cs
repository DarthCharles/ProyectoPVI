using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using materias;
using database;

namespace ControldeAlumnosPVI
{
    public partial class OpcionesMateria : Form
    {
        bool nuevo = true;
        string idMateria;
        string idMaestro;
        public OpcionesMateria(String str, string idMaestro)
        {
            InitializeComponent();
            this.idMaestro = idMaestro;
            this.Text = str;
        }

        public OpcionesMateria(String str, string nombre, bool nuevo, string idMateria)
        {
            InitializeComponent();
            this.Text = "Configurar Materia";
            this.pictureBox1.Image = global::ControldeAlumnosPVI.Properties.Resources.ic_action_1404533629_98;
            textBox1.Text = nombre;
            label1.Text = "Nombre:";
            button1.Text = "Aceptar";
            this.nuevo = nuevo;
            this.idMateria = idMateria;

        }
        private bool validar()
        {
            if (textBox1.Text == "") return false;
            return true;

        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (validar())
            {
                if (nuevo)
                {
                    Conexion con = new Conexion();
                    Materia materia = new Materia(this.idMaestro, textBox1.Text, "1");
                    if (!con.create(materia))
                    {
                        MessageBox.Show("Ya existe una materia con ese nombre");
                    }
                    else
                    {
                        this.Close();
                    }
                }
                else
                {
                    Conexion con = new Conexion();
                    Materia materia = new Materia(this.idMaestro, textBox1.Text, "1", idMateria);
                    if (!con.update(materia))
                    {
                        MessageBox.Show("Ha habido un error");
                    }
                    else
                    {
                        this.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor ingrese un nombre válido.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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
