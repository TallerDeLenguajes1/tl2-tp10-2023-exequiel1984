using System.Data.SQLite;
using tl2_tp10_2023_exequiel1984.ViewModels;


namespace tl2_tp10_2023_exequiel1984.Models
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly string _cadenaConexion;

        public UsuarioRepository(string cadenaConexion)
        {
            _cadenaConexion = cadenaConexion;
        }

        public void Create(Usuario usuario)
        {
            int filasAfectadas = 0;
            var query = @"INSERT INTO Usuario (nombre_de_usuario, contrasenia, rol) VALUES (@nombre, @contrasenia, @rol)";
                using (SQLiteConnection connection = new SQLiteConnection(_cadenaConexion))
                {
                    connection.Open();
                    var command = new SQLiteCommand(query, connection);
                    command.Parameters.Add(new SQLiteParameter("@nombre", usuario.NombreDeUsuario));
                    command.Parameters.Add(new SQLiteParameter("@contrasenia", usuario.Contrasenia));
                    command.Parameters.Add(new SQLiteParameter("@rol", Convert.ToInt32(usuario.Rol)));
                    filasAfectadas = command.ExecuteNonQuery();
                    connection.Close();   
                }
            if (filasAfectadas == 0)
                throw new Exception("No se pudo crear el usuario.");
        }

        public void Update(Usuario usuario){
            int filasAfectadas = 0;

            using (SQLiteConnection connection = new SQLiteConnection(_cadenaConexion))
            {
                var queryString = @"UPDATE Usuario SET nombre_de_usuario = @nombre, contrasenia = @contrasenia, rol = @rol
                WHERE id = @idUsuario;";
                connection.Open();
                var command = new SQLiteCommand(queryString, connection);
                command.Parameters.Add(new SQLiteParameter("@nombre", usuario.NombreDeUsuario));
                command.Parameters.Add(new SQLiteParameter("@contrasenia", usuario.Contrasenia));
                command.Parameters.Add(new SQLiteParameter("@rol", usuario.Rol));
                command.Parameters.Add(new SQLiteParameter("@idUsuario", usuario.Id));
                filasAfectadas = command.ExecuteNonQuery();
                connection.Close();
            }
            if (filasAfectadas == 0)
                throw new Exception("No se pudo actualizar el usuario.");
        }

        public void Remove(int id)
        {
            using (SQLiteConnection connection = new SQLiteConnection(_cadenaConexion))
            {
                string queryTareaPropia = @"UPDATE Tarea
                                SET activo = 0
                                WHERE id_tablero IN (SELECT id FROM Tablero WHERE id_usuario_propietario = @idUsuario)";
                connection.Open();
                SQLiteCommand commandTareaPropia = new SQLiteCommand(queryTareaPropia, connection);
                commandTareaPropia.Parameters.Add(new SQLiteParameter("@idUsuario", id));
                commandTareaPropia.ExecuteNonQuery();
                connection.Close();

                string queryTareaAjenaAsignada = @"UPDATE Tarea
                                SET id_usuario_asignado = NULL
                                WHERE id_tablero IN (SELECT id FROM Tablero WHERE id_usuario_propietario != @idUsuario)
                                AND id_usuario_asignado = @idUsuario;";
                connection.Open();
                SQLiteCommand commandTareaAjena = new SQLiteCommand(queryTareaAjenaAsignada, connection);
                commandTareaAjena.Parameters.Add(new SQLiteParameter("@idUsuario", id));
                commandTareaAjena.ExecuteNonQuery();
                connection.Close();

                string queryTablero = @"UPDATE Tablero
                                SET activo = 0
                                WHERE id_usuario_propietario = @idUsuario;";
                connection.Open();
                SQLiteCommand commandTablero = new SQLiteCommand(queryTablero, connection);
                commandTablero.Parameters.Add(new SQLiteParameter("@idUsuario", id));
                commandTablero.ExecuteNonQuery();
                connection.Close();


                //string query = @"DELETE FROM Usuario WHERE id = @id;";
                string query = @"UPDATE Usuario SET activo = 0 WHERE id = @idUsuario;";
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(query, connection);
                command.Parameters.Add(new SQLiteParameter("@idUsuario", id));
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
            
        public List<Usuario> GetAll()
        {
            SQLiteConnection connection = null;
                var queryString = @"SELECT * FROM Usuario WHERE activo = 1;";
                List<Usuario> usuarios = new List<Usuario>();
                using (connection = new SQLiteConnection(_cadenaConexion))
                {
                    SQLiteCommand command = new SQLiteCommand(queryString, connection);
                    connection.Open();
                
                    using(SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var usuario = new Usuario();
                            usuario.Id = Convert.ToInt32(reader["id"]);
                            usuario.NombreDeUsuario = reader["nombre_de_usuario"].ToString();
                            usuario.Contrasenia = reader["contrasenia"].ToString();
                            usuario.Rol = (NivelDeAcceso) Convert.ToInt32(reader["rol"]);
                            usuarios.Add(usuario);
                        }
                    }
                    connection.Close();
                }
                return usuarios;
        }
        
        public Usuario GetById(int id)
        {
            Usuario usuario = null;
            using (SQLiteConnection connection = new SQLiteConnection(_cadenaConexion))
            {
                string queryString = @"SELECT * FROM Usuario WHERE id = @idUsuario AND activo = 1;";
                var command = new SQLiteCommand(queryString, connection);
                command.Parameters.Add(new SQLiteParameter("@idUsuario", id));
                connection.Open();
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        usuario = new Usuario();
                        usuario.Id = Convert.ToInt32(reader["id"]);
                        usuario.NombreDeUsuario = reader["nombre_de_usuario"].ToString();
                        usuario.Contrasenia = reader["contrasenia"].ToString();
                        usuario.Rol = (NivelDeAcceso) Convert.ToInt32(reader["rol"]);
                    }
                }
                connection.Close();
            }
            if (usuario==null)
                throw new Exception("El usuario con el ID especificado no existe.");
            return usuario;
        }

        public string GetNameById(int? id)
        {
            string nombreUsuario = null;
            using (SQLiteConnection connection = new SQLiteConnection(_cadenaConexion))
            {
                string queryString = @"SELECT * FROM Usuario WHERE id = @idUsuario AND activo = 1;";
                var command = new SQLiteCommand(queryString, connection);
                command.Parameters.Add(new SQLiteParameter("@idUsuario", id));
                connection.Open();
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                        nombreUsuario = reader["nombre_de_usuario"].ToString();
                }
                connection.Close();
            }
            return nombreUsuario;
        }

        public Usuario GetUsuarioLogin(string nombre, string contrasenia)
        {
            Usuario usuario = new Usuario();
            using (SQLiteConnection connection = new SQLiteConnection(_cadenaConexion))
            {
                string queryString = @"SELECT * FROM Usuario WHERE nombre_de_usuario = @nombre AND contrasenia = @contrasenia AND activo = 1;";
                var command = new SQLiteCommand(queryString, connection);
                command.Parameters.Add(new SQLiteParameter("@nombre", nombre));
                command.Parameters.Add(new SQLiteParameter("@contrasenia", contrasenia));
                
                connection.Open();
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        usuario.Id = Convert.ToInt32(reader["id"]);
                        usuario.NombreDeUsuario = reader["nombre_de_usuario"].ToString();
                        usuario.Contrasenia = reader["contrasenia"].ToString();
                        usuario.Rol = (NivelDeAcceso) Convert.ToInt32(reader["rol"]);
                    }
                }
                connection.Close();
            }
            return usuario;
        }
    }
}