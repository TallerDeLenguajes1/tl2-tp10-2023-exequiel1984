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
            var query = $"INSERT INTO Usuario (nombre_de_usuario, contrasenia, rol) VALUES (@nombre, @contrasenia, @rol)";
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(_cadenaConexion))
                {

                    connection.Open();
                    var command = new SQLiteCommand(query, connection);

                    command.Parameters.Add(new SQLiteParameter("@nombre", usuario.NombreDeUsuario));
                    command.Parameters.Add(new SQLiteParameter("@contrasenia", usuario.Contrasenia));
                    command.Parameters.Add(new SQLiteParameter("@rol", Convert.ToInt32(usuario.Rol)));
                    command.ExecuteNonQuery();

                    connection.Close();   
                }
            }
            catch (System.Exception e)
            {
                    
            
            }
            finally
            {
                
            }
            
        }
            
        public List<Usuario> GetAll()
        {
            SQLiteConnection connection = null;
            try
            {
                var queryString = @"SELECT * FROM Usuario;";
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
            catch (System.Exception ex)
            {
                
                throw;
            }finally
            {
                if (connection == null)
                    connection.Close();
            }
            

        }
        
        public Usuario GetById(int id)
        {
            Usuario usuario = new Usuario();
            using (SQLiteConnection connection = new SQLiteConnection(_cadenaConexion))
            {
                string queryString = @"SELECT * FROM Usuario WHERE id = @idUsuario;";
                var command = new SQLiteCommand(queryString, connection);
                command.Parameters.Add(new SQLiteParameter("@idUsuario", id));
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

        public Usuario GetUsuarioLogin(string nombre, string contrasenia)
        {
            Usuario usuario = new Usuario();
            using (SQLiteConnection connection = new SQLiteConnection(_cadenaConexion))
            {
                string queryString = @"SELECT * FROM Usuario WHERE nombre_de_usuario = @nombre AND contrasenia = @contrasenia;";
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

        public void Update(Usuario usuario){
            using (SQLiteConnection connection = new SQLiteConnection(_cadenaConexion))
            {
                var queryString = @"UPDATE Usuario SET nombre_de_usuario = @nombre WHERE id = @idUsuario;";
                connection.Open();
                var command = new SQLiteCommand(queryString, connection);
                command.Parameters.Add(new SQLiteParameter("@nombre", usuario.NombreDeUsuario));
                command.Parameters.Add(new SQLiteParameter("@idUsuario", usuario.Id));
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void Remove(int id)
        {
            using (SQLiteConnection connection = new SQLiteConnection(_cadenaConexion))
            {
                string query = @"DELETE FROM Usuario WHERE id = @id;";
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(query, connection);
                command.Parameters.Add(new SQLiteParameter("@id", id));
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}