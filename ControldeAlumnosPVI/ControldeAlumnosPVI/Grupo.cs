using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using materia;

namespace grupo
{
    class Grupo : Materia
    {
        private string nombre;
        private string idGrupo;

        public Grupo(string nombre, string idMateria):base(idMateria)
        {
            this.nombre = nombre;
        }

        public string NombreGrupo
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public string IdGrupo
        {
            get { return idGrupo; }
            set { idGrupo = value; }
        }

    }
}
