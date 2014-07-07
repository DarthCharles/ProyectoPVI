using database;
using maestros;
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
    public partial class Login : Form
    {
        Color clr2 = Color.FromArgb(193, 224, 101);
        public Login()
        {
            InitializeComponent();
   
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
        private bool validar()
        {
            if (usuario.Text == "")    return false;
            if (contraseña.Text == "") return false;

            return true;
           
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (validar())
            {
                Maestro maes = new Maestro();
                Conexion con = new Conexion();
                if (con.login(usuario.Text.Trim(), contraseña.Text.Trim(), maes))
                {

                    Form1 principal = new Form1(maes.NombreMaestro, maes.Apellido, maes.IdMaestro);
                    principal.Show();
                    this.Visible = false;


                }
                else
                {
                    MessageBox.Show("Combinación de usuario y contraseña incorrecta.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                Maestro maes = new Maestro();
                Conexion con = new Conexion();
                if (con.login(usuario.Text.Trim(), contraseña.Text.Trim(), maes))
                {

                    Form1 principal = new Form1(maes.NombreMaestro, maes.Apellido, maes.IdMaestro);
                    principal.Show();
                    this.Visible = false;


                }
                else
                {
                    MessageBox.Show("Por favor ingrese su usuario y su contraseña.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
       
            }
           

        }
    }
}
