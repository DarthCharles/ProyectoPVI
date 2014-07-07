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
        string idPonderacion;
        string idGrupo;
        public static string nombreGrupo;
        public static bool validado;

        public OpcionesGrupo()
        {
            InitializeComponent();
        }
        private bool validar()
        {
            if (textBox1.Text == "") return false;
            if (textBox2.Text == "") return false;
            if (textBox3.Text == "") return false;
            if (textBox4.Text == "") return false;
            if (textBox5.Text == "") return false;
            if (textBox6.Text == "") return false;
            if (idPonderacion == "") return false;
            return true;

        }
        public OpcionesGrupo(string str, string[] listaPonde, string idGrupo, string idMateria)
        {
            InitializeComponent();
            this.Text = "Configurar";
            this.label1.Text = "Configurar Grupo";
            this.pictureBox1.Image = global::ControldeAlumnosPVI.Properties.Resources.ic_action_1404533629_98;
            this.textBox1.Text = str;
            this.textBox2.Text = listaPonde[0];
            this.textBox3.Text = listaPonde[1];
            this.textBox4.Text = listaPonde[2];
            this.textBox5.Text = listaPonde[3];
            this.textBox6.Text = listaPonde[4];
            this.idPonderacion = listaPonde[5];
            this.idGrupo = idGrupo;
            this.idMateria = idMateria;

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
            if (validar())
            {


                if (idPonderacion == null)
                {
                    Conexion con = new Conexion();
                    Grupo grupo = new Grupo();
                    grupo.NombreGrupo = textBox1.Text;
                    grupo.IdMateria = idMateria;
                    if (!con.createPonderacion(textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text, grupo))
                    {
                        MessageBox.Show("Ya existe un grupo con ese nombre", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        this.Close();

                    }
                }
                else
                {
                    Conexion con = new Conexion();
                    Grupo grupo = new Grupo();
                    grupo.NombreGrupo = textBox1.Text;
                    grupo.IdMateria = idMateria;
                    grupo.IdGrupo = idGrupo;
                    string[] listaPonde = new string[6];
                    listaPonde[0] = textBox2.Text;
                    listaPonde[1] = textBox3.Text;
                    listaPonde[2] = textBox4.Text;
                    listaPonde[3] = textBox5.Text;
                    listaPonde[4] = textBox6.Text;
                    listaPonde[5] = idPonderacion;
                    nombreGrupo = textBox1.Text;
                    validado = true;
                    if (!con.update(grupo, listaPonde))
                    {
                        MessageBox.Show("No se pudo actualizar");
                    }
                    else
                    {
                        this.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor rellene todos los campos", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
                && !char.IsDigit(e.KeyChar)
                )
            {
                e.Handled = true;
            }
        }


    }
}
