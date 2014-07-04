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
    public partial class Form1 : Form
    {
        int materItemSize = 176;
        int grupoItemSize = 160;
        public Form1()
        {
            InitializeComponent();


            for (int i = 0; i < 10; i++)
            {
                  panel_materias.Controls.Add(new ListItem(materItemSize, "Materia " + i, panel_materias));
            }

            for (int i = 0; i < 10; i++)
            {
                panel_grupos.Controls.Add(new ListItem(grupoItemSize, "Grupo " + i, panel_grupos));
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
