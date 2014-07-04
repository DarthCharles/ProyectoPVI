using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using maestro;
using System.Data.SqlClient;
using System.Data;
using materia;

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
                        maestro.Nombre = reader["nombre"].ToString();
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
                    comando.Parameters.AddWithValue("@nombre", maestro.Nombre);
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

        public bool isMaestroRepetido(string user){
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
                        maestro.Nombre = reader["nombre"].ToString();
                        maestro.Apellido = reader["apellido"].ToString();
                        maestro.User = reader["user"].ToString();
                        maestro.Password = reader["password"].ToString();
                    }
                }
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

        public List<Maestro> readInfoMaestros()
        {
            List<Maestro> listaMaestros = new List<Maestro>();
            try
            {
                if (conexion.State == ConnectionState.Closed)
                {
                    conexion.Open();
                }
                string query = "SELECT idmaestros, nombre, apellido, user, pass from maestros";
                MySqlCommand comando = new MySqlCommand(query);
                comando.Connection = conexion;
                MySqlDataReader reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Maestro maestro = new Maestro();
                        maestro.IdMaestro = reader["idmaestros"].ToString();
                        maestro.Nombre = reader["nombre"].ToString();
                        maestro.Apellido = reader["apellido"].ToString();
                        maestro.User = reader["user"].ToString();
                        maestro.Password = reader["password"].ToString();
                        listaMaestros.Add(maestro);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Ha ocurrido un error al leer la información del maestro: " + e.Message);
            }
            finally
            {
                conexion.Close();
            }
            return listaMaestros;
        }

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
                comando.Parameters.AddWithValue("@nombre", maestro.Nombre);
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
                string query = "DELETE FROM maestros "+
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
            if (!isMateriaRepetida(materia.Clave))
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
                    comando.Parameters.AddWithValue("@nombre", materia.Nombre);
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

        public bool isMateriaRepetida(string clave)
        {
            try
            {
                if (conexion.State == ConnectionState.Closed)
                {
                    conexion.Open();
                }
                string query = "SELECT * from materias where clave = @clave";
                MySqlCommand comando = new MySqlCommand(query);
                comando.Parameters.AddWithValue("@clave", clave);
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

        public List<Materia> readInfoMaterias()
        {
            List<Materia> listaMaterias = new List<Materia>();
            try
            {
                if (conexion.State == ConnectionState.Closed)
                {
                    conexion.Open();
                }
                string query = "SELECT * FROM materias";
                MySqlCommand comando = new MySqlCommand(query);
                comando.Connection = conexion;
                MySqlDataReader reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Materia materia = new Materia();
                        materia.IdMateria = reader["idmaterias"].ToString();
                        materia.Nombre = reader["nombre"].ToString();
                        materia.Clave = reader["clave"].ToString();
                        materia.IdMaestro = reader["idmaestros"].ToString();

                        listaMaterias.Add(materia);
                    }
                }
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
                        materia.Nombre = reader["nombre"].ToString();
                        materia.Clave = reader["clave"].ToString();
                        materia.IdMaestro = reader["idmaestros"].ToString();

                        listaMaterias.Add(materia);
                    }
                }
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
                        materia.Nombre = reader["nombre"].ToString();
                        materia.Clave = reader["clave"].ToString();
                        materia.IdMaestro = reader["idmaterias"].ToString();
                    }
                }
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
                        materia.Nombre = reader["nombre"].ToString();
                        materia.Clave = reader["clave"].ToString();
                        materia.IdMaestro = reader["idmaterias"].ToString();
                    }
                }
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
                comando.Parameters.AddWithValue("@nombre", materia.Nombre);
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
        


    }
}