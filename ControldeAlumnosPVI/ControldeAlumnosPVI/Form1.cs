using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using database;
using materia;

namespace ControldeAlumnosPVI
{
    public partial class Form1 : Form
    {
        int materItemSize = 176;
        int grupoItemSize = 160;
        public Form1()
        {
            InitializeComponent();

            Conexion con = new Conexion();
            List<Materia> listaMat = con.readInfoMateriasIdMaestro("1");

            foreach (Materia materia in listaMat)
            {
                panel_materias.Controls.Add(new ListItem(materItemSize, materia.Nombre, panel_materias, materia));
            }
          
           

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void panel_materias_MouseClick(object sender, MouseEventArgs e)
        {

        }

  

        
      

   
    }
}
