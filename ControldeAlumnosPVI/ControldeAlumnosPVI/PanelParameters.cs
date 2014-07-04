using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControldeAlumnosPVI
{
    class PanelParameters
    {
        FlowLayoutPanel panel_materias;
        FlowLayoutPanel panel_grupos;
        Form1 context;

        public FlowLayoutPanel PanelMaterias { get { return panel_materias; }
            set { panel_materias = value; }
        }

        public FlowLayoutPanel PanelGrupos
        {
            get { return panel_grupos; }
            set { panel_grupos = value; }
        }

        public Form1 Context
        {
            get { return context; }
            set { context = value; }
        }


        public PanelParameters() { }
        public PanelParameters(Form1 con)
        {
            this.context = con;
            this.panel_materias = con.panel_materias;
            this.panel_grupos = con.panel_grupos;
         
        }
    }
}
