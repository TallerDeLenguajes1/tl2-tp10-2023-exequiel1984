using System.Data.SQLite;

namespace tl2_tp10_2023_exequiel1984.Models
{
    public class TableroRepository : ITableroRepository
    {
        private string cadenaConexion = "Data Source=DB/kanban.db;Cache=Shared";

        public Tablero Create(Tablero tablero)
        {
            var query = @"
            INSERT INTO Tablero (id_usuario_propietario, nombre, descripcion)
            VALUES (@usuarioPropietario, @nombre , @descripcion);";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
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
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
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
            Tablero tablero = new Tablero();
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                string queryString = @"SELECT * FROM Tablero WHERE id = @id;";
                var command = new SQLiteCommand(queryString, connection);
                command.Parameters.Add(new SQLiteParameter("@id", id));
                connection.Open();
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tablero.Id = Convert.ToInt32(reader["id"]);
                        tablero.IdUsuarioPropietario = Convert.ToInt32(reader["id_usuario_propietario"]);
                        tablero.Nombre = reader["nombre"].ToString();
                        tablero.Descripcion = reader["descripcion"].ToString();
                    }
                }
                connection.Close();
            }
            return tablero;
        }
        
        public List<Tablero> GetAll()
        {
            string query = @"SELECT * FROM Tablero;";
            List<Tablero> tableros = new List<Tablero>();
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
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

        public List<Tablero> GetAllByIdUsuario(int idUsuario)
        {
            string query = @"SELECT * FROM Tablero WHERE id_usuario_propietario = @idUsuario;";
            List<Tablero> tableros = new List<Tablero>();
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
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

        public void Remove(int id)
        {
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
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