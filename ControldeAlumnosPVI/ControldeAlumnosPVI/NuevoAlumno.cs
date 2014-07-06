using alumnos;
using database;
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
    public partial class NuevoAlumno : Form
    {
        string nombreAlumno;
            string idGrupo;

        public NuevoAlumno(string idGrupo)
        {

            InitializeComponent();
       
            this.idGrupo = idGrupo;
    
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Conexion con = new Conexion();
            Alumno a = new Alumno();
            a.NombreAlumno = textBox1.Text.ToUpper();
            a.Foto = "1";
            a.IdGrupo = idGrupo;
            con.createAlumnoIdGrupo(a);
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
