using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using maestros;
using database;

namespace ControldeAlumnosPVI
{
    public partial class Registro : Form
    {
        Login l;
        public Registro(Login l)
        {
            this.l = l;
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (validar())
            {
                if (!validarContra())
                {
                    MessageBox.Show("Las contraseñas proporcionadas no coinciden.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    Conexion con = new Conexion();
                    Maestro maestro = new Maestro(textBox3.Text, textBox4.Text, textBox1.Text, textBox2.Text);
                    if (con.create(maestro))
                    {
                        MessageBox.Show("Usuario creado con éxito.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        l.Focus();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Ya existe un usuario con ese nombre.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            else
            {
                MessageBox.Show("Existen campos vacíos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public bool validar()
        {
            if (textBox1.Text == "")
            {
                return false;
            }
            else if (textBox2.Text == "")
            {
                return false;
            }
            else if (textBox3.Text == "")
            {
                return false;
            }
            else if (textBox4.Text == "")
            {
                return false;
            }
            else if (textBox5.Text == "")
            {
                return false;
            }
            return true;
        }


        public bool validarContra()
        {
            if (textBox4.Text != textBox5.Text)
            {
                return false;
            }
            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            l.Focus();
            this.Close();
        }
    }
}
