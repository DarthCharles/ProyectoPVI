using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace maestro
{
    class Maestro
    {
        private string user;
        private string password;
        private string nombre;
        private string apellido;
        private string idMaestro;

        public Maestro()
        {
        }

        public Maestro (string idMaestro){
        this.idMaestro = idMaestro;
        }
        public Maestro(string user, string password)
        {
            this.user = user;
            this.password = password;
        }

        public Maestro(string user, string password, string nombre,
                string apellido)
        {
            this.user = user;
            this.password = password;
            this.nombre = nombre;
            this.apellido = apellido;
        }

        public Maestro(string user, string password, string nombre,
                string apellido, string idMaestro)
        {
            this.user = user;
            this.password = password;
            this.nombre = nombre;
            this.apellido = apellido;
            this.idMaestro = idMaestro;
        }

        public string User
        {
            get { return user; }
            set { user = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public string Apellido
        {
            get { return apellido; }
            set { apellido = value; }
        }

        public string IdMaestro
        {
            get { return idMaestro; }
            set { idMaestro = value; }
        }
    }
}
