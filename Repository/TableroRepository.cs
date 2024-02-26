using System.Data.SQLite;

namespace tl2_tp10_2023_exequiel1984.Models
{
    public class TableroRepository : ITableroRepository
    {
        private readonly string _cadenaConexion;

        public TableroRepository(string CadenaConexion)
        {
            _cadenaConexion = CadenaConexion;
        }

        public Tablero Create(Tablero tablero)
        {
            var query = @"
            INSERT INTO Tablero (id_usuario_propietario, nombre, descripcion)
            VALUES (@usuarioPropietario, @nombre , @descripcion);";
            using (SQLiteConnection connection = new SQLiteConnection(_cadenaConexion))
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(query, connection);
                command.Parameters.Add(new SQLiteParameter("@usuarioPropietario", tablero.IdUsuarioPropietario));
                command.Parameters.Add(new SQLiteParameter("@nombre", tablero.Nombre));
                command.Parameters.Add(new SQLiteParameter("@descripcion", tablero.Descripcion));
                command.ExecuteNonQuery();
                connection.Close();
            }
            return tablero;
        }

        public void UpDate(Tablero tablero)
        {
            using (SQLiteConnection connection = new SQLiteConnection(_cadenaConexion))
            {
                var queryString = @"
                UPDATE Tablero SET id_usuario_propietario = @idUsuarioPropietario, nombre = @nombre, descripcion = @descripcion
                WHERE id = @idTablero;";
                connection.Open();
                var command = new SQLiteCommand(queryString, connection);
                command.Parameters.Add(new SQLiteParameter("@idUsuarioPropietario", tablero.IdUsuarioPropietario));
                command.Parameters.Add(new SQLiteParameter("@nombre", tablero.Nombre));
                command.Parameters.Add(new SQLiteParameter("@descripcion", tablero.Descripcion));
                command.Parameters.Add(new SQLiteParameter("@idTablero", tablero.Id));
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public Tablero GetById(int id)
        {
            Tablero tablero = null;
            using (SQLiteConnection connection = new SQLiteConnection(_cadenaConexion))
            {
                string queryString = @"SELECT * FROM Tablero WHERE id = @id AND activo = 1;";
                var command = new SQLiteCommand(queryString, connection);
                command.Parameters.Add(new SQLiteParameter("@id", id));
                connection.Open();
                using (SQLiteDataReader reader = command.ExecuteReader())
                {

                    if (reader.Read())
                    {
                        tablero = new Tablero();
                        tablero.Id = Convert.ToInt32(reader["id"]);
                        tablero.IdUsuarioPropietario = Convert.ToInt32(reader["id_usuario_propietario"]);
                        tablero.Nombre = reader["nombre"].ToString();
                        tablero.Descripcion = reader["descripcion"].ToString();
                    }
                }
                connection.Close();
            }
            if (tablero == null)
                throw new Exception("Tablero no creado");
            return tablero;
        }

        public string GetNameById(int idTablero)
        {
            string nombre = null;
            using (SQLiteConnection connection = new SQLiteConnection(_cadenaConexion))
            {
                string queryString = @"SELECT nombre FROM Tablero WHERE id = @idTablero AND activo = 1;";
                var command = new SQLiteCommand(queryString, connection);
                command.Parameters.Add(new SQLiteParameter("@idTablero", idTablero));
                connection.Open();
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                        nombre = reader["nombre"].ToString();
                    
                }
                connection.Close();
            }
            if (nombre == null)
                throw new Exception("Tablero no creado");
            return nombre;
        }

        public int GetIdUsuarioPropietarioById(int idTablero)
        {
            int idUsuarioPropietario = -1;
            using (SQLiteConnection connection = new SQLiteConnection(_cadenaConexion))
            {
                string queryString = @"SELECT id_usuario_propietario FROM Tablero WHERE id = @idTablero AND activo = 1;";
                var command = new SQLiteCommand(queryString, connection);
                command.Parameters.Add(new SQLiteParameter("@idTablero", idTablero));
                connection.Open();
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                        idUsuarioPropietario = Convert.ToInt32(reader["id_usuario_propietario"]); 
                }
                connection.Close();
            }
            if (idUsuarioPropietario==-1)
                throw new Exception("Tablero no creado.");
            return idUsuarioPropietario;
        }
        
        public List<Tablero> GetAll()
        {
            string query = @"SELECT * FROM Tablero WHERE activo = 1;";
            List<Tablero> tableros = new List<Tablero>();
            using (SQLiteConnection connection = new SQLiteConnection(_cadenaConexion))
            {
                SQLiteCommand command = new SQLiteCommand(query, connection);
                connection.Open();

                using (SQLiteDataReader Reader = command.ExecuteReader())
                {
                    while (Reader.Read())
                    {
                        Tablero tablero = new Tablero();
                        tablero.Id = Convert.ToInt32(Reader["id"]);
                        tablero.IdUsuarioPropietario = Convert.ToInt32(Reader["id_usuario_propietario"]);
                        tablero.Nombre = Reader["nombre"].ToString();
                        tablero.Descripcion = Reader["descripcion"].ToString();
                        tableros.Add(tablero);
                    }
                }

                connection.Close();
            }
            return tableros;
        }

        public List<Tablero> GetByIdUsuarioPropietario(int idUsuario)
        {
            string query = @"SELECT * FROM Tablero WHERE id_usuario_propietario = @idUsuario AND activo = 1;";
            List<Tablero> tableros = new List<Tablero>();
            using (SQLiteConnection connection = new SQLiteConnection(_cadenaConexion))
            {
                SQLiteCommand command = new SQLiteCommand(query, connection);
                command.Parameters.Add(new SQLiteParameter("@idUsuario", idUsuario));
                connection.Open();

                using (SQLiteDataReader Reader = command.ExecuteReader())
                {
                    while (Reader.Read())
                    {
                        Tablero tablero = new Tablero();
                        tablero.Id = Convert.ToInt32(Reader["id"]);
                        tablero.IdUsuarioPropietario = Convert.ToInt32(Reader["id_usuario_propietario"]);
                        tablero.Nombre = Reader["nombre"].ToString();
                        tablero.Descripcion = Reader["descripcion"].ToString();
                        tableros.Add(tablero);
                    }
                }
                connection.Close();
            }
            return tableros;
        }

        public List<int> GetListIdByUsuarioPropietario(int idUsuarioPropietario)
        {
            List<int> listaIdTablero = new List<int>();
            using (SQLiteConnection connection = new SQLiteConnection(_cadenaConexion))
            {
                string queryString = @"SELECT id FROM Tablero WHERE id_usuario_propietario = @idUsuarioPropietario AND activo = 1;";
                var command = new SQLiteCommand(queryString, connection);
                command.Parameters.Add(new SQLiteParameter("@idUsuarioPropietario", idUsuarioPropietario));
                connection.Open();
                using (SQLiteDataReader reader = command.ExecuteReader())
                {

                    if (reader.Read())
                    {
                        int id = Convert.ToInt32(reader["id"]);
                        listaIdTablero.Add(id);
                    }
                }
                connection.Close();
            }
            return listaIdTablero;
        }

        

        public void Remove(int id)
        {
            using (SQLiteConnection connection = new SQLiteConnection(_cadenaConexion))
            {
                //delete from tareas inner join tablero on tarea.idtablero = tablero.id where tablero.id = id


                
                string query = @"DELETE FROM Tablero WHERE id = @id;";
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(query, connection);
                command.Parameters.Add(new SQLiteParameter("@id", id));
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}