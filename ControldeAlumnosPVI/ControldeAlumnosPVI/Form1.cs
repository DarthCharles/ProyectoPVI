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
        public int materItemSize = 176;
       public  int grupoItemSize = 160;
        public Form1()
        {
            InitializeComponent();

            Conexion con = new Conexion();
            List<Materia> listaMat = con.readInfoMateriasIdMaestro("1");

            PanelParameters panel = new PanelParameters(this);
            foreach (Materia materia in listaMat)
            {
                panel_materias.Controls.Add(new ItemMaterias(panel, materia.Nombre, materia));
            }
          
           

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }


        
      

   
    }
}
