using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using grupos;
using parciales;

namespace trabajos
{
    class Trabajo : Parcial
    {
        private string nombre;
        private string tipo;
        private string entregada;
        private string calificacion;
        private string idAlumno;
        private string idTrabajo;


        public Trabajo()
        {
        }

        public Trabajo(string nombre, string tipo, string idParcial, string idGrupo)
            : base(idParcial, idGrupo)
        {
            this.nombre = nombre;
            this.tipo = tipo;
        }

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public string Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }

        public string Entregada
        {
            get { return entregada; }
            set { entregada = value; }
        }

        public string Calificacion
        {
            get { return calificacion; }
            set { calificacion = value; }
        }

        public string IdAlumno
        {
            get { return idAlumno; }
            set { idAlumno = value; }
        }

        public string IdTrabajo
        {
            get { return idTrabajo; }
            set { idTrabajo = value; }
        }
    }
}
