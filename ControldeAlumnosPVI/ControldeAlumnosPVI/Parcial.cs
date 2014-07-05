using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using grupos;

namespace parciales
{
    class Parcial : Grupo
    {
        private string idParcial;
        private string nombre;

        public Parcial()
        {
        }

        public Parcial(string idParcial, string idGrupo) : base (idGrupo)
        {
            this.idParcial = idParcial;
        }

        public string IdParcial
        {
            get { return idParcial; }
            set { idParcial = value; }
        }

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

    }
}
