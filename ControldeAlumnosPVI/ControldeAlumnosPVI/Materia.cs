using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using maestro;

namespace materia
{
    class Materia : Maestro
    {
        private string idMateria;
        private string nombre;
        private string clave;


        public Materia()
        {
        }

        public Materia(string idMateria)
        {
            this.idMateria = idMateria;
        }

        public Materia(string idMaestro, string nombre, string clave) 
            : base(idMaestro)
        {
            this.nombre = nombre;
            this.clave = clave;
        }

        public Materia(string idMaestro, string nombre, string clave, string idMateria)
            : base(idMaestro)
        {
            this.nombre = nombre;
            this.clave = clave;
            this.IdMateria = IdMateria;
        }

        public string IdMateria
        {
            get { return idMateria; }
            set { idMateria = value; }
        }

        public string NombreMateria
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public string Clave
        {
            get { return clave; }
            set { clave = value; }
        }
    
    }

     
}
