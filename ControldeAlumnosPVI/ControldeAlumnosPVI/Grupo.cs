using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using materias;

namespace grupos
{
    class Grupo : Materia
    {
        private string nombre;
        private string idGrupo;

        public Grupo()
        {
        }

        public Grupo(string idGrupo)
        {
            this.idGrupo = idGrupo;
        }

        public Grupo(string nombre, string idGrupo, string idMaestro)
        {
            this.idGrupo = idGrupo;
        }

        public Grupo(string nombre, string idMateria)
            : base(idMateria)
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
