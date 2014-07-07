using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using maestros;
using System.Data.SqlClient;
using System.Data;
using materias;
using grupos;
using alumnos;
using trabajos;
using parciales;

namespace database
{
    class Conexion
    {
        private string server;
        private string user;
        private string password;
        private string database;
        private MySqlConnection conexion;

        public Conexion()
        {
            this.server = DatabaseConstants.DB_SERVER;
            this.user = DatabaseConstants.DB_USER;
            this.password = DatabaseConstants.DB_PASS;
            this.database = DatabaseConstants.DB_NAME;

            this.conexion = new MySqlConnection();
            this.conexion.ConnectionString = "Server=" + server + ";" +
                "Database=" + database + ";" +
                "Uid=" + user + ";" +
                "Pwd=" + password + ";";
        }

        //Maestros

        public bool login(string user, string password, Maestro maestro)
        {
            try
            {
                if (conexion.State == ConnectionState.Closed)
                {
                    conexion.Open();
                }
                string query = "SELECT * from maestros WHERE " +
                    "user = @user and password = @password";
                MySqlCommand comando = new MySqlCommand(query);
                comando.Parameters.AddWithValue("@user", user);
                comando.Parameters.AddWithValue("@password", password);

                comando.Connection = conexion;

                MySqlDataReader reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        maestro.IdMaestro = reader["idmaestros"].ToString();
                        maestro.NombreMaestro = reader["nombre"].ToString();
                        maestro.Apellido = reader["apellido"].ToString();
                        maestro.User = reader["user"].ToString();
                        maestro.Password = reader["password"].ToString();
                    }
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Ha ocurrido un error al leer la información del maestro: " + e.Message);
                return false;
            }
            finally
            {
                conexion.Close();
            }
            return false;
        }

        public bool create(Maestro maestro)
        {
            if (!isMaestroRepetido(maestro.User))
            {
                try
                {
                    if (conexion.State == ConnectionState.Closed)
                    {
                        conexion.Open();
                    }
                    string query = "INSERT INTO maestros (nombre, apellido, user, password) " +
                        "VALUES (@nombre, @apellido, @user, @password)";
                    MySqlCommand comando = new MySqlCommand(query);
                    comando.Parameters.AddWithValue("@nombre", maestro.NombreMaestro);
                    comando.Parameters.AddWithValue("@apellido", maestro.Apellido);
                    comando.Parameters.AddWithValue("@user", maestro.User);
                    comando.Parameters.AddWithValue("@password", maestro.Password);
                    comando.Connection = conexion;
                    int a = comando.ExecuteNonQuery();
                    if (a == 0)
                    {
                        return false;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Ha ocurrido un error al registrar un nuevo maestro: " + e.Message);
                    return false;
                }
                finally
                {
                    conexion.Close();
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool isMaestroRepetido(string user)
        {
            try
            {
                if (conexion.State == ConnectionState.Closed)
                {
                    conexion.Open();
                }
                string query = "SELECT * from maestros where user = @user";
                MySqlCommand comando = new MySqlCommand(query);
                comando.Parameters.AddWithValue("@user", user);
                comando.Connection = conexion;
                MySqlDataReader reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Ha ocurrido un error al registrar un nuevo maestro: " + e.Message);
                return false;
            }
            finally
            {
                conexion.Close();
            }
            return false;
        }

        public Maestro readInfoMaestro(string idMaestro)
        {
            Maestro maestro = new Maestro();
            try
            {
                if (conexion.State == ConnectionState.Closed)
                {
                    conexion.Open();
                }
                string query = "SELECT nombre, apellido, user, pass from maestros WHERE " +
                    "idmaestros = @idmaestros";
                MySqlCommand comando = new MySqlCommand(query);
                comando.Parameters.AddWithValue("@idmaestros", idMaestro);
                comando.Connection = conexion;

                MySqlDataReader reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        maestro.IdMaestro = idMaestro;
                        maestro.NombreMaestro = reader["nombre"].ToString();
                        maestro.Apellido = reader["apellido"].ToString();
                        maestro.User = reader["user"].ToString();
                        maestro.Password = reader["password"].ToString();
                    }
                }
                reader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Ha ocurrido un error al leer la información del maestro: " + e.Message);
            }
            finally
            {
                conexion.Close();
            }
            return maestro;
        }

        //public List<Maestro> readInfoMaestros()
        //{
        //    List<Maestro> listaMaestros = new List<Maestro>();
        //    try
        //    {
        //        if (conexion.State == ConnectionState.Closed)
        //        {
        //            conexion.Open();
        //        }
        //        string query = "SELECT idmaestros, nombre, apellido, user, pass from maestros";
        //        MySqlCommand comando = new MySqlCommand(query);
        //        comando.Connection = conexion;
        //        MySqlDataReader reader = comando.ExecuteReader();
        //        if (reader.HasRows)
        //        {
        //            while (reader.Read())
        //            {
        //                Maestro maestro = new Maestro();
        //                maestro.IdMaestro = reader["idmaestros"].ToString();
        //                maestro.NombreMaestro = reader["nombre"].ToString();
        //                maestro.Apellido = reader["apellido"].ToString();
        //                maestro.User = reader["user"].ToString();
        //                maestro.Password = reader["password"].ToString();
        //                listaMaestros.Add(maestro);
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine("Ha ocurrido un error al leer la información del maestro: " + e.Message);
        //    }
        //    finally
        //    {
        //        conexion.Close();
        //    }
        //    return listaMaestros;
        //}

        public bool update(Maestro maestro)
        {
            try
            {
                if (conexion.State == ConnectionState.Closed)
                {
                    conexion.Open();
                }
                string query = "UPDATE maestros SET nombre = @nombre, apellido = @apellido, " +
                    " user = @user, password = @password WHERE idmaestros = @idmaestros";
                MySqlCommand comando = new MySqlCommand(query);
                comando.Parameters.AddWithValue("@nombre", maestro.NombreMaestro);
                comando.Parameters.AddWithValue("@apellido", maestro.Apellido);
                comando.Parameters.AddWithValue("@user", maestro.User);
                comando.Parameters.AddWithValue("@password", maestro.Password);
                comando.Parameters.AddWithValue("@idmaestros", maestro.IdMaestro);

                comando.Connection = conexion;
                int a = comando.ExecuteNonQuery();
                if (a == 0)
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Ha ocurrido un error al actualizar un maestro: " + e.Message);
                return false;
            }
            finally
            {
                conexion.Close();
            }
            return true;
        }

        public bool deleteMaestro(string idMaestro)
        {
            try
            {
                if (conexion.State == ConnectionState.Closed)
                {
                    conexion.Open();
                }
                string query = "DELETE FROM maestros " +
                    "WHERE idmaestros = @idmaestros";
                MySqlCommand comando = new MySqlCommand(query);
                comando.Parameters.AddWithValue("@idmaestros", idMaestro);
                comando.Connection = conexion;
                int a = comando.ExecuteNonQuery();
                if (a == 0)
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Ha ocurrido un error al borrar un maestro: " + e.Message);
                return false;
            }
            finally
            {
                conexion.Close();
            }
            return true;
        }

        //Materias

        public bool create(Materia materia)
        {
            if (!isMateriaRepetida(materia.NombreMateria, materia.IdMaestro))
            {
                try
                {
                    if (conexion.State == ConnectionState.Closed)
                    {
                        conexion.Open();
                    }
                    string query = "INSERT INTO materias (nombre, clave, idmaestros) " +
                        "VALUES (@nombre, @clave, @idmaestros)";
                    MySqlCommand comando = new MySqlCommand(query);
                    comando.Parameters.AddWithValue("@nombre", materia.NombreMateria);
                    comando.Parameters.AddWithValue("@clave", materia.Clave);
                    comando.Parameters.AddWithValue("@idmaestros", materia.IdMaestro);
                    comando.Connection = conexion;
                    int a = comando.ExecuteNonQuery();
                    if (a == 0)
                    {
                        return false;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Ha ocurrido un error al registrar una nueva materia: " + e.Message);
                    return false;
                }
                finally
                {
                    conexion.Close();
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool isMateriaRepetida(string nombre, string idMaestro)
        {
            try
            {
                if (conexion.State == ConnectionState.Closed)
                {
                    conexion.Open();
                }
                string query = "SELECT * from materias where nombre = @nombre and idmaestros = @idmaestros";
                MySqlCommand comando = new MySqlCommand(query);
                comando.Parameters.AddWithValue("@nombre", nombre);
                comando.Parameters.AddWithValue("@idmaestros", idMaestro);

                comando.Connection = conexion;
                MySqlDataReader reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Ha ocurrido un error al comprobar si existe la clave: " + e.Message);
                return false;
            }
            finally
            {
                conexion.Close();
            }
            return false;
        }

        //public List<Materia> readInfoMaterias()
        //{
        //    List<Materia> listaMaterias = new List<Materia>();
        //    try
        //    {
        //        if (conexion.State == ConnectionState.Closed)
        //        {
        //            conexion.Open();
        //        }
        //        string query = "SELECT * FROM materias";
        //        MySqlCommand comando = new MySqlCommand(query);
        //        comando.Connection = conexion;
        //        MySqlDataReader reader = comando.ExecuteReader();
        //        if (reader.HasRows)
        //        {
        //            while (reader.Read())
        //            {
        //                Materia materia = new Materia();
        //                materia.IdMateria = reader["idmaterias"].ToString();
        //                materia.NombreMateria = reader["nombre"].ToString();
        //                materia.Clave = reader["clave"].ToString();
        //                materia.IdMaestro = reader["idmaestros"].ToString();

        //                listaMaterias.Add(materia);
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine("Ha ocurrido un error al leer la información del maestro: " + e.Message);
        //    }
        //    finally
        //    {
        //        conexion.Close();
        //    }
        //    return listaMaterias;
        //}

        public List<Materia> readInfoMateriasIdMaestro(string idMaestro)
        {
            List<Materia> listaMaterias = new List<Materia>();
            try
            {
                if (conexion.State == ConnectionState.Closed)
                {
                    conexion.Open();
                }
                string query = "SELECT * FROM materias WHERE idmaestros = @idmaestros";
                MySqlCommand comando = new MySqlCommand(query);
                comando.Parameters.AddWithValue("@idmaestros", idMaestro);
                comando.Connection = conexion;
                MySqlDataReader reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Materia materia = new Materia();
                        materia.IdMateria = reader["idmaterias"].ToString();
                        materia.NombreMateria = reader["nombre"].ToString();
                        materia.Clave = reader["clave"].ToString();
                        materia.IdMaestro = reader["idmaestros"].ToString();

                        listaMaterias.Add(materia);
                    }
                }
                reader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Ha ocurrido un error al leer la información del maestro: " + e.Message);
            }
            finally
            {
                conexion.Close();
            }
            return listaMaterias;
        }

        public Materia readInfoMateriaIdMateria(string idMateria)
        {
            Materia materia = new Materia();
            try
            {
                if (conexion.State == ConnectionState.Closed)
                {
                    conexion.Open();
                }
                string query = "SELECT nombre, clave, idmaterias FROM materias WHERE " +
                    "idmaterias = @idmaterias";
                MySqlCommand comando = new MySqlCommand(query);
                comando.Parameters.AddWithValue("@idmaterias", idMateria);
                comando.Connection = conexion;

                MySqlDataReader reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        materia.IdMateria = idMateria;
                        materia.NombreMateria = reader["nombre"].ToString();
                        materia.Clave = reader["clave"].ToString();
                        materia.IdMaestro = reader["idmaterias"].ToString();
                    }
                }
                reader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Ha ocurrido un error al leer la información de la materia: " + e.Message);
            }
            finally
            {
                conexion.Close();
            }
            return materia;
        }

        public Materia readInfoMateriaIdMaestro(string idMaestro)
        {
            Materia materia = new Materia();
            try
            {
                if (conexion.State == ConnectionState.Closed)
                {
                    conexion.Open();
                }
                string query = "SELECT idmaterias, nombre, clave, idmaterias FROM materias WHERE " +
                    "idmaestros = @idmaestros";
                MySqlCommand comando = new MySqlCommand(query);
                comando.Parameters.AddWithValue("@idmaestros", idMaestro);
                comando.Connection = conexion;

                MySqlDataReader reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        materia.IdMateria = reader["idmaterias"].ToString();
                        materia.NombreMateria = reader["nombre"].ToString();
                        materia.Clave = reader["clave"].ToString();
                        materia.IdMaestro = reader["idmaterias"].ToString();
                    }
                }
                reader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Ha ocurrido un error al leer la información de la materia: " + e.Message);
            }
            finally
            {
                conexion.Close();
            }
            return materia;
        }

        public bool update(Materia materia)
        {
            try
            {
                if (conexion.State == ConnectionState.Closed)
                {
                    conexion.Open();
                }
                string query = "UPDATE materias SET nombre = @nombre, clave = @clave, " +
                    " idmaestros = @idmaestros WHERE idmaterias = @idmaterias";
                MySqlCommand comando = new MySqlCommand(query);
                comando.Parameters.AddWithValue("@nombre", materia.NombreMateria);
                comando.Parameters.AddWithValue("@clave", materia.Clave);
                comando.Parameters.AddWithValue("@idmaestros", materia.IdMaestro);
                comando.Parameters.AddWithValue("@idmaterias", materia.IdMateria);

                comando.Connection = conexion;
                int a = comando.ExecuteNonQuery();
                if (a == 0)
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Ha ocurrido un error al actualizar una materia: " + e.Message);
                return false;
            }
            finally
            {
                conexion.Close();
            }
            return true;
        }

        public bool deleteMateria(string idMateria)
        {
            try
            {
                if (conexion.State == ConnectionState.Closed)
                {
                    conexion.Open();
                }
                string query = "DELETE FROM materias " +
                    "WHERE idmaterias = @idmaterias";
                MySqlCommand comando = new MySqlCommand(query);
                comando.Parameters.AddWithValue("@idmaterias", idMateria);
                comando.Connection = conexion;
                int a = comando.ExecuteNonQuery();
                if (a == 0)
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Ha ocurrido un error al borrar una materia: " + e.Message);
                return false;
            }
            finally
            {
                conexion.Close();
            }
            return true;
        }

        //Grupos

        public bool create(Grupo grupo, string idPonderacion)
        {
            if (!isGrupoRepetido(grupo.NombreGrupo, grupo.IdMateria))
            {
                try
                {
                    if (conexion.State == ConnectionState.Closed)
                    {
                        conexion.Open();
                    }
                    string query = "INSERT INTO grupos (nombre, idmaterias, idponderacion) VALUES (@nombre, @idmaterias, @idponderacion)";
                    MySqlCommand comando = new MySqlCommand(query);
                    comando.Parameters.AddWithValue("@nombre", grupo.NombreGrupo);
                    comando.Parameters.AddWithValue("@idmaterias", grupo.IdMateria);
                    comando.Parameters.AddWithValue("@idponderacion", idPonderacion);

                    comando.Connection = conexion;
                    int a = comando.ExecuteNonQuery();
                    if (a == 0)
                    {
                        return false;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Ha ocurrido un error al registrar un nuevo grupo: " + e.Message);
                    return false;
                }
                finally
                {
                    conexion.Close();
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool createPonderacion(string asistencia, string participacion,
                string trabajos, string tareas, string examenes, Grupo grupo)
        {
            try
            {
                if (conexion.State == ConnectionState.Closed)
                {
                    conexion.Open();
                }
                string query = "INSERT INTO ponderacion (asistencia, participacion, trabajos, tareas, examenes) VALUES (@asistencia, @participacion, @trabajos, @tareas, @examenes)";
                MySqlCommand comando = new MySqlCommand(query);
                comando.Parameters.AddWithValue("@asistencia", asistencia);
                comando.Parameters.AddWithValue("@participacion", participacion);
                comando.Parameters.AddWithValue("@trabajos", trabajos);
                comando.Parameters.AddWithValue("@examenes", examenes);
                comando.Parameters.AddWithValue("@tareas", tareas);

                comando.Connection = conexion;
                int a = comando.ExecuteNonQuery();
                if (a == 0)
                {
                    return false;
                }

                if (!create(grupo, comando.LastInsertedId.ToString()))
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Ha ocurrido un error al registrar una nueva ponderacion: " + e.Message);
                return false;
            }
            finally
            {
                conexion.Close();
            }
            return true;
        }

        public string[] readPonderacion(string idPonderacion)
        {
            
                string[] listaPonde = new string[6];
                try
                {
                if (conexion.State == ConnectionState.Closed)
                {
                    conexion.Open();
                }
                string query = "SELECT idponderacion, asistencia, participacion, trabajos, tareas, examenes FROM ponderacion WHERE " +
                    "idponderacion = @idponderacion";
                MySqlCommand comando = new MySqlCommand(query);
                comando.Parameters.AddWithValue("@idponderacion", idPonderacion);
                comando.Connection = conexion;

                MySqlDataReader reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        listaPonde[0] = reader["asistencia"].ToString();
                        listaPonde[1] = reader["participacion"].ToString();
                        listaPonde[2] = reader["trabajos"].ToString();
                        listaPonde[3] = reader["tareas"].ToString();
                        listaPonde[4] = reader["examenes"].ToString();
                        listaPonde[5] = reader["idponderacion"].ToString();

                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Ha ocurrido un error al registrar un nuevo grupo: " + e.Message);
            }
            finally
            {
                conexion.Close();
            }
            return listaPonde;
        }
        

        public bool isGrupoRepetido(string nombre, string idMateria)
        {
            try
            {
                if (conexion.State == ConnectionState.Closed)
                {
                    conexion.Open();
                }
                string query = "SELECT * from grupos where nombre = @nombre and idmaterias = @idmaterias";
                MySqlCommand comando = new MySqlCommand(query);
                comando.Parameters.AddWithValue("@nombre", nombre);
                comando.Parameters.AddWithValue("@idmaterias", idMateria);
                comando.Connection = conexion;
                MySqlDataReader reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Ha ocurrido un error al comprobar si existe el grupo: " + e.Message);
                return false;
            }
            finally
            {
                conexion.Close();
            }
            return false;
        }

        public List<Grupo> readInfoGruposIdMateria(string idMateria)
        {
            List<Grupo> listaGrupos = new List<Grupo>();

            if (conexion.State == ConnectionState.Closed)
            {
                conexion.Open();
            }
            string query = "SELECT * FROM grupos WHERE idmaterias = @idmaterias";
            MySqlCommand comando = new MySqlCommand(query);
            comando.Parameters.AddWithValue("@idmaterias", idMateria);
            comando.Connection = conexion;
            MySqlDataReader reader = comando.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Grupo grupo = new Grupo();
                    grupo.IdMateria = reader["idmaterias"].ToString();
                    grupo.NombreGrupo = reader["nombre"].ToString();
                    grupo.IdGrupo = reader["idgrupos"].ToString();
                    grupo.IdPonderacion = reader["idponderacion"].ToString();

                    listaGrupos.Add(grupo);
                }
            }
            reader.Close();
            return listaGrupos;
        }

        public Grupo readInfoGrupoIdMateria(string idMateria)
        {
            Grupo grupo = new Grupo();
            try
            {
                if (conexion.State == ConnectionState.Closed)
                {
                    conexion.Open();
                }
                string query = "SELECT idgrupos, nombre FROM grupos WHERE " +
                    "idmaterias = @idmaterias";
                MySqlCommand comando = new MySqlCommand(query);
                comando.Parameters.AddWithValue("@idmaterias", idMateria);
                comando.Connection = conexion;

                MySqlDataReader reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        grupo.IdGrupo = reader["idgrupos"].ToString();
                        grupo.NombreGrupo = reader["nombre"].ToString();
                    }
                }
                reader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Ha ocurrido un error al leer la información del grupo: " + e.Message);
            }
            finally
            {
                conexion.Close();
            }
            return grupo;
        }

        public bool update(Grupo grupo, string[] listaPonde)
        {
            try
            {
                if (conexion.State == ConnectionState.Closed)
                {
                    conexion.Open();
                }
                string query = "UPDATE grupos SET nombre = @nombre, idmaterias = @idmaterias " +
                    " WHERE idgrupos = @idgrupos";
                MySqlCommand comando = new MySqlCommand(query);
                comando.Parameters.AddWithValue("@nombre", grupo.NombreGrupo);
                comando.Parameters.AddWithValue("@idmaterias", grupo.IdMateria);
                comando.Parameters.AddWithValue("@idgrupos", grupo.IdGrupo);

                comando.Connection = conexion;
                int a = comando.ExecuteNonQuery();
                if (a == 0)
                {
                    return false;
                }
                updatePonderacion(listaPonde);
            }
            catch (Exception e)
            {
                Console.WriteLine("Ha ocurrido un error al actualizar un grupo: " + e.Message);
                return false;
            }
            finally
            {
                conexion.Close();
            }
            return true;
        }

        public bool updatePonderacion(string[] listaPonde)
        {
            try
            {
                if (conexion.State == ConnectionState.Closed)
                {
                    conexion.Open();
                }
                string query = "UPDATE ponderacion SET asistencia = @asistencia, participacion = @participacion, trabajos = @trabajos, " +
                    "tareas = @tareas, examenes = @examenes " +
                    " WHERE idponderacion = @idponderacion";
                MySqlCommand comando = new MySqlCommand(query);
                comando.Parameters.AddWithValue("@asistencia", listaPonde[0]);
                comando.Parameters.AddWithValue("@participacion", listaPonde[1]);
                comando.Parameters.AddWithValue("@trabajos", listaPonde[2]);
                comando.Parameters.AddWithValue("@tareas", listaPonde[3]);
                comando.Parameters.AddWithValue("@examenes", listaPonde[4]);
                comando.Parameters.AddWithValue("@idponderacion", listaPonde[5]);

                comando.Connection = conexion;
                int a = comando.ExecuteNonQuery();
                if (a == 0)
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Ha ocurrido un error al actualizar un grupo: " + e.Message);
                return false;
            }
            finally
            {
                conexion.Close();
            }
            return true;
        }

        public bool deleteGrupo(string idGrupo)
        {
            try
            {
                if (conexion.State == ConnectionState.Closed)
                {
                    conexion.Open();
                }
                string query = "DELETE FROM grupos " +
                    "WHERE idgrupos = @idgrupos";
                MySqlCommand comando = new MySqlCommand(query);
                comando.Parameters.AddWithValue("@idgrupos", idGrupo);
                comando.Connection = conexion;
                int a = comando.ExecuteNonQuery();
                if (a == 0)
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Ha ocurrido un error al borrar un grupo: " + e.Message);
                return false;
            }
            finally
            {
                conexion.Close();
            }
            return true;
        }

        //Alumnos

        public bool create(Alumno alumno)
        {
            try
            {
                if (conexion.State == ConnectionState.Closed)
                {
                    conexion.Open();
                }
                string query = "INSERT INTO alumnos (nombre, foto) VALUES (@nombre, @foto)";
                MySqlCommand comando = new MySqlCommand(query);
                comando.Parameters.AddWithValue("@nombre", alumno.NombreAlumno);
                comando.Parameters.AddWithValue("@foto", alumno.Foto);
                comando.Connection = conexion;
                int a = comando.ExecuteNonQuery();
                if (a == 0)
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Ha ocurrido un error al registrar un nuevo alumno: " + e.Message);
                return false;
            }
            finally
            {
                conexion.Close();
            }
            return true;
        }

        public bool addAlumnoGrupo(Alumno alumno)
        {
            try
            {
                if (conexion.State == ConnectionState.Closed)
                {
                    conexion.Open();
                }
                string query = "INSERT INTO alumnos_grupo (idalumnos, idgrupos) VALUES (@idalumnos, @idgrupos)";
                MySqlCommand comando = new MySqlCommand(query);
                comando.Parameters.AddWithValue("@idalumnos", alumno.IdAlumno);
                comando.Parameters.AddWithValue("@idgrupos", alumno.IdGrupo);

                comando.Connection = conexion;
                int a = comando.ExecuteNonQuery();
                if (a == 0)
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Ha ocurrido un error al registrar un nuevo alumno en un grupo: " + e.Message);
                return false;
            }
            finally
            {
                conexion.Close();
            }
            return true;
        }

        public bool createAlumnoIdGrupo(Alumno alumno)
        {
            try
            {
                if (conexion.State == ConnectionState.Closed)
                {
                    conexion.Open();
                }
                string query = "INSERT INTO alumnos (nombre, foto) VALUES (@nombre, @foto)";
                MySqlCommand comando = new MySqlCommand(query);
                comando.Parameters.AddWithValue("@nombre", alumno.NombreAlumno);
                comando.Parameters.AddWithValue("@foto", alumno.Foto);
                comando.Connection = conexion;
                int a = comando.ExecuteNonQuery();
                String lastId = comando.LastInsertedId.ToString();
                alumno.IdAlumno = lastId;
                addAlumnoGrupo(alumno);
                if (a == 0)
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Ha ocurrido un error al registrar un nuevo alumno en un grupo: " + e.Message);
                return false;
            }
            finally
            {
                conexion.Close();
            }
            return true;
        }

        public List<Alumno> readInfoAlumnos()
        {
            List<Alumno> listaAlumnos = new List<Alumno>();
            try
            {
                if (conexion.State == ConnectionState.Closed)
                {
                    conexion.Open();
                }
                string query = "SELECT * FROM alumnos";
                MySqlCommand comando = new MySqlCommand(query);
                comando.Connection = conexion;
                MySqlDataReader reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Alumno alumno = new Alumno();
                        alumno.IdAlumno = reader["idalumnos"].ToString();
                        alumno.NombreAlumno = reader["nombre"].ToString();
                        alumno.Foto = reader["foto"].ToString();
                        listaAlumnos.Add(alumno);
                    }
                }
                reader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Ha ocurrido un error al leer la información del alumno: " + e.Message);
            }
            finally
            {
                conexion.Close();
            }
            return listaAlumnos;
        }

        public List<Alumno> readInfoAlumnosGrupo(string idGrupo)
        {
            List<Alumno> listaAlumnos = new List<Alumno>();
            try
            {
                if (conexion.State == ConnectionState.Closed)
                {
                    conexion.Open();
                }
                string query = "SELECT * FROM alumnos_grupo INNER JOIN alumnos ON alumnos_grupo.idalumnos = " +
                        "alumnos.idalumnos WHERE idgrupos = @idgrupos ORDER by nombre";
                MySqlCommand comando = new MySqlCommand(query);
                comando.Parameters.AddWithValue("@idgrupos", idGrupo);
                comando.Connection = conexion;
                MySqlDataReader reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Alumno alumno = new Alumno();
                        alumno.IdAlumno = reader["idalumnos"].ToString();
                        alumno.NombreAlumno = reader["nombre"].ToString();
                        alumno.Foto = reader["foto"].ToString();
                        alumno.Calificacion = reader["calificacion"].ToString();
                        listaAlumnos.Add(alumno);
                    }
                }
                reader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Ha ocurrido un error al leer la información del alumno: " + e.Message);
            }
            finally
            {
                conexion.Close();
            }
            return listaAlumnos;
        }

        public bool update(Alumno alumno)
        {
            try
            {
                if (conexion.State == ConnectionState.Closed)
                {
                    conexion.Open();
                }
                string query = "UPDATE alumnos SET nombre = @nombre, foto = @foto " +
                    " WHERE idalumnos = @idalumnos";
                MySqlCommand comando = new MySqlCommand(query);
                comando.Parameters.AddWithValue("@nombre", alumno.NombreAlumno);
                comando.Parameters.AddWithValue("@foto", alumno.Foto);
                comando.Parameters.AddWithValue("@idalumnos", alumno.IdAlumno);

                comando.Connection = conexion;
                int a = comando.ExecuteNonQuery();
                if (a == 0)
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Ha ocurrido un error al actualizar un alumno: " + e.Message);
                return false;
            }
            finally
            {
                conexion.Close();
            }
            return true;
        }

        public bool deleteAlumno(string idAlumno)
        {
            try
            {
                if (conexion.State == ConnectionState.Closed)
                {
                    conexion.Open();
                }
                string query = "DELETE FROM alumnos " +
                    "WHERE idalumnos = @idalumnos";
                MySqlCommand comando = new MySqlCommand(query);
                comando.Parameters.AddWithValue("@idalumnos", idAlumno);
                comando.Connection = conexion;
                int a = comando.ExecuteNonQuery();
                if (a == 0)
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Ha ocurrido un error al borrar un alumno: " + e.Message);
                return false;
            }
            finally
            {
                conexion.Close();
            }
            return true;
        }

        public bool deleteAlumnoGrupo(string idAlumno)
        {
            try
            {
                if (conexion.State == ConnectionState.Closed)
                {
                    conexion.Open();
                }
                string query = "DELETE FROM alumnos_grupo " +
                    "WHERE idalumnos = @idalumnos";
                MySqlCommand comando = new MySqlCommand(query);
                comando.Parameters.AddWithValue("@idalumnos", idAlumno);
                comando.Connection = conexion;
                int a = comando.ExecuteNonQuery();
                if (a == 0)
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Ha ocurrido un error al borrar un alumno: " + e.Message);
                return false;
            }
            finally
            {
                conexion.Close();
            }
            return true;
        }

        //Trabajos

        public bool create(Trabajo trabajo)
        {

            if (conexion.State == ConnectionState.Closed)
            {
                conexion.Open();
            }
            string query = "INSERT INTO trabajos_dejados (nombre, tipo, idgrupos) VALUES (@nombre, @tipo, @idgrupos)";
            MySqlCommand comando = new MySqlCommand(query);
            comando.Parameters.AddWithValue("@nombre", trabajo.Nombre);
            comando.Parameters.AddWithValue("@tipo", trabajo.Tipo);
            comando.Parameters.AddWithValue("@idgrupos", trabajo.IdGrupo);

            comando.Connection = conexion;
            int a = comando.ExecuteNonQuery();
            if (a == 0)
            {
                return false;
            }

            return true;
        }

        public bool delete(string str)
        {

            if (conexion.State == ConnectionState.Closed)
            {
                conexion.Open();
            }
            string query = "DELETE FROM trabajos_dejados WHERE idtareas_dejadas = @nombre";
            MySqlCommand comando = new MySqlCommand(query);
            comando.Parameters.AddWithValue("@nombre", str);

            comando.Connection = conexion;
            int a = comando.ExecuteNonQuery();
            if (a == 0)
            {
                return false;
            }

            return true;
        }

        public bool nuevoTrabajo(string idAlumno, string idtareas, string calificacion, string tipo)
        {
            
                if (conexion.State == ConnectionState.Closed)
                {
                    conexion.Open();
                }
                string query = "INSERT INTO trabajos (calificacion, idalumnos_grupo, " +
                    "idtareas_dejadas, tipo) " +
                    "VALUES (@calificacion, @id_alumnosgrupo, @clave, @tipo)";
                MySqlCommand comando = new MySqlCommand(query);
                comando.Parameters.AddWithValue("@calificacion", calificacion);
                comando.Parameters.AddWithValue("@id_alumnosgrupo", idAlumno);
                comando.Parameters.AddWithValue("@clave", idtareas);
                comando.Parameters.AddWithValue("@tipo", tipo);

                comando.Connection = conexion;
                int a = comando.ExecuteNonQuery();

                if (a == 0)
                {
                    return false;
                }

            return true;
        }

        public bool updateTrabajo(string idAlumno, string idtareas, string calificacion)
        {
            
                if (conexion.State == ConnectionState.Closed)
                {
                    conexion.Open();
                }
                string query = "UPDATE trabajos SET calificacion = @calificacion " +
                    " WHERE idtareas_dejadas = @clave and idalumnos_grupo = @idalumnos_grupo";
                MySqlCommand comando = new MySqlCommand(query);
                comando.Parameters.AddWithValue("@calificacion", calificacion);
                comando.Parameters.AddWithValue("@idalumnos_grupo", idAlumno);
                comando.Parameters.AddWithValue("@clave", idtareas);

                comando.Connection = conexion;
                int a = comando.ExecuteNonQuery();

                if (a == 0)
                {
                    return false;
                }
            return true;
        }

        public bool registroTarea(string idAlumno, string idTrabajo, string calificacion, string tipo)
        {
            if (existeTarea(idAlumno, idTrabajo))
            {
                updateTrabajo(idAlumno, idTrabajo, calificacion);
            }
            else
            {
                nuevoTrabajo(idAlumno, idTrabajo, calificacion, tipo);
            }
            return true;
        }

        public bool existeTarea(string idAlumno, string idtareas)
        {
            {
                try
                {
                    if (conexion.State == ConnectionState.Closed)
                    {
                        conexion.Open();
                    }
                    string query = "SELECT * from trabajos where idalumnos_grupo = @idalumnos and idtareas_dejadas = @clave";
                    MySqlCommand comando = new MySqlCommand(query);
                    comando.Parameters.AddWithValue("@idalumnos", idAlumno);
                    comando.Parameters.AddWithValue("@clave", idtareas);
                    comando.Connection = conexion;
                    MySqlDataReader reader = comando.ExecuteReader();
                    if (reader.HasRows)
                    {
                        return true;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Ha ocurrido un error al comprobar si existe el alumno: " + e.Message);
                    return false;
                }
                finally
                {
                    conexion.Close();
                }
                return false;
            }
        }

        public List<Trabajo> readInfoTrabajosAlumno(string idAlumno, string idtrabajos)
        {
            List<Trabajo> listaTrabajos = new List<Trabajo>();
            try
            {
                if (conexion.State == ConnectionState.Closed)
                {
                    conexion.Open();
                }

                string query = "select * from trabajos " +
                    " where idalumnos_grupo = @idalumno and idtareas_dejadas = @clave";

                MySqlCommand comando = new MySqlCommand(query);
                comando.Parameters.AddWithValue("@idalumno", idAlumno);
                comando.Parameters.AddWithValue("@clave", idtrabajos);
                comando.Connection = conexion;
                MySqlDataReader reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Trabajo trabajo = new Trabajo();
                        trabajo.Calificacion = reader["calificacion"].ToString();
                        listaTrabajos.Add(trabajo);
                    }
                }
                reader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Ha ocurrido un error al leer la información del alumno: " + e.Message);
            }
            finally
            {
                conexion.Close();
            }
            return listaTrabajos;
        }

        public int countTrabajos(string idGrupo, string tipo)
        {
            int numTrabajos = 0;
            try
            {
                if (conexion.State == ConnectionState.Closed)
                {
                    conexion.Open();
                }
                string query = "SELECT COUNT(*) FROM trabajos_dejados where tipo = @tipo and idgrupos = @idgrupos";
                MySqlCommand comando = new MySqlCommand(query);
                comando.Parameters.AddWithValue("@tipo", tipo);
                comando.Parameters.AddWithValue("@idgrupos", idGrupo);

                comando.Connection = conexion;

                numTrabajos = int.Parse(comando.ExecuteScalar().ToString());

            }
            catch (Exception e)
            {
                Console.WriteLine("Ha ocurrido un error al leer la información de la materia: " + e.Message);
            }
            finally
            {
                conexion.Close();
            }
            return numTrabajos;
        }

        public List<Trabajo> readInfoTrabajosGrupo(string idGrupo, string tipo)
        {
            List<Trabajo> listaTrabajos = new List<Trabajo>();

            if (conexion.State == ConnectionState.Closed)
            {
                conexion.Open();
            }

            string query = "SELECT * FROM trabajos_dejados  " +
                "WHERE idgrupos = @idgrupos and tipo = @tipo";

            MySqlCommand comando = new MySqlCommand(query);
            comando.Parameters.AddWithValue("@idgrupos", idGrupo);
            comando.Parameters.AddWithValue("@tipo", tipo);
            comando.Connection = conexion;
            MySqlDataReader reader = comando.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Trabajo trabajo = new Trabajo();
                    trabajo.Nombre = reader["nombre"].ToString();
                    trabajo.IdTrabajo = reader["idtareas_dejadas"].ToString();
                    listaTrabajos.Add(trabajo);

                }
            }
            reader.Close();

            return listaTrabajos;
        }

    }
}

