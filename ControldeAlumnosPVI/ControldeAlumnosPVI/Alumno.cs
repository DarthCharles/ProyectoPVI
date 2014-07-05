using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using grupos;

namespace alumnos
{
    class Alumno : Grupo
    {
        private string idAlumno;
        private string nombre;
        private string foto;
        private string calificacion;


        public Alumno(string nombre, string foto, string idGrupo)
            : base(idGrupo)
        {
            this.nombre = nombre;
            this.foto = foto;
        }

        public Alumno(string idAlumno, string idGrupo)
            : base(idGrupo)
        {
            this.idAlumno = idAlumno;

        }

        public Alumno(string nombre)
        {
            this.nombre = nombre;
        }

        public string IdAlumno
        {
            get { return idAlumno; }
            set { idAlumno = value; }
        }

        public string NombreAlumno
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public string Foto
        {
            get { return foto; }
            set { foto = value; }
        }

        public string Calificacion
        {
            get { return calificacion; }
            set { calificacion = value; }
        }
    }
}
