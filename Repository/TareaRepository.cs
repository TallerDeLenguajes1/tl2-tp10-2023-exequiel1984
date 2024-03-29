using System.Data.SQLite;

namespace tl2_tp10_2023_exequiel1984.Models
{
    public class TareaRepository : ITareaRepository
    {
        private readonly string _cadenaConexion;

        public TareaRepository(string CadenaConexion)
        {
            _cadenaConexion = CadenaConexion;
        }

        public Tarea Create(Tarea tarea)
        {
            int filasAfectadas = 0;
            var query = @"
            INSERT INTO Tarea (id_tablero, nombre, estado, descripcion, color, id_usuario_asignado)
            VALUES (@idTablero, @nombre , @estado, @descripcion, @color, @idUsuarioAsignado);";
            using (SQLiteConnection connection = new SQLiteConnection(_cadenaConexion))
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(query, connection);
                command.Parameters.Add(new SQLiteParameter("@idTablero", tarea.IdTablero));
                command.Parameters.Add(new SQLiteParameter("@nombre", tarea.Nombre));
                command.Parameters.Add(new SQLiteParameter("@estado", tarea.Estado));
                command.Parameters.Add(new SQLiteParameter("@descripcion", tarea.Descripcion));
                command.Parameters.Add(new SQLiteParameter("@color", tarea.Color));
                command.Parameters.Add(new SQLiteParameter("@idUsuarioAsignado", tarea.IdUsuarioAsignado));
                filasAfectadas = command.ExecuteNonQuery();
                connection.Close();
            }
            if (filasAfectadas == 0)
                throw new Exception("No se pudo crear la tarea.");
            return tarea;
        }

        public void UpDate(Tarea tarea)
        {
            int filasAfectadas = 0;

            using (SQLiteConnection connection = new SQLiteConnection(_cadenaConexion))
            {
                var queryString = @"
                UPDATE Tarea SET id_tablero = @idTablero, nombre = @nombre, estado = @estado, descripcion = @descripcion, 
                color = @color, id_usuario_asignado = @idUsuarioAsignado
                WHERE id = @idTarea;";
                connection.Open();
                var command = new SQLiteCommand(queryString, connection);
                command.Parameters.Add(new SQLiteParameter("@idTablero", tarea.IdTablero));
                command.Parameters.Add(new SQLiteParameter("@nombre", tarea.Nombre));
                command.Parameters.Add(new SQLiteParameter("@estado", tarea.Estado));
                command.Parameters.Add(new SQLiteParameter("@descripcion", tarea.Descripcion));
                command.Parameters.Add(new SQLiteParameter("@color", tarea.Color));
                command.Parameters.Add(new SQLiteParameter("@idUsuarioAsignado", tarea.IdUsuarioAsignado));
                command.Parameters.Add(new SQLiteParameter("@idTarea", tarea.Id));
                filasAfectadas = command.ExecuteNonQuery();
                connection.Close();
            }
            if (filasAfectadas == 0)
                throw new Exception("No se pudo actualizar la tarea.");
        }

        public void UpDateNombre(int id, string Nombre){
            int filasAfectadas = 0;

            using (SQLiteConnection connection = new SQLiteConnection(_cadenaConexion))
            {
                var queryString = @"
                UPDATE Tarea SET nombre = @nombre
                WHERE id = @id;";
                connection.Open();
                var command = new SQLiteCommand(queryString, connection);
                command.Parameters.Add(new SQLiteParameter("@nombre", Nombre));
                command.Parameters.Add(new SQLiteParameter("@id", id));
                filasAfectadas = command.ExecuteNonQuery();
                connection.Close();
            }
            if (filasAfectadas == 0)
                throw new Exception($"No se encontró ninguna tarea con el ID {id}.");
        }

        public void UpDateEstado(int id, EstadoTarea Estado){
            int filasAfectadas = 0;
            
            using (SQLiteConnection connection = new SQLiteConnection(_cadenaConexion))
            {
                var queryString = @"
                UPDATE Tarea SET estado = @estado
                WHERE id = @id;";
                connection.Open();
                var command = new SQLiteCommand(queryString, connection);
                command.Parameters.Add(new SQLiteParameter("@estado", Estado));
                command.Parameters.Add(new SQLiteParameter("@id", id));
                filasAfectadas = command.ExecuteNonQuery();
                connection.Close();
            }
            if (filasAfectadas == 0)
                throw new Exception($"No se encontró ninguna tarea con el ID {id}.");
        }

        public void Remove(int id)
        {
            int filasAfectadas = 0;

            using (SQLiteConnection connection = new SQLiteConnection(_cadenaConexion))
            {
                string query = @"UPDATE Tarea SET activo = 0 WHERE id = @idTarea;";
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(query, connection);
                command.Parameters.Add(new SQLiteParameter("@idTarea", id));
                filasAfectadas = command.ExecuteNonQuery();
                connection.Close();
            }
            if (filasAfectadas == 0)
                throw new Exception($"No se encontró ninguna tarea con el ID {id}.");
        }

        public List<Tarea> GetAll()
        {
            string query = @"SELECT * FROM Tarea WHERE activo = 1;";
            List<Tarea> tareas = new List<Tarea>();
            using (SQLiteConnection connection = new SQLiteConnection(_cadenaConexion))
            {
                SQLiteCommand command = new SQLiteCommand(query, connection);
                connection.Open();

                using (SQLiteDataReader Reader = command.ExecuteReader())
                {
                    while (Reader.Read())
                    {
                        Tarea tarea = new Tarea();
                        tarea.Id = Convert.ToInt32(Reader["id"]);
                        tarea.IdTablero = Convert.ToInt32(Reader["id_tablero"]);
                        tarea.Nombre = Reader["nombre"].ToString();
                        tarea.Estado = (EstadoTarea)Convert.ToInt32(Reader["estado"]);
                        tarea.Descripcion = Reader["descripcion"].ToString();
                        tarea.Color = Reader["color"].ToString();
                        if (Reader["id_usuario_asignado"] != DBNull.Value)
                            tarea.IdUsuarioAsignado = Convert.ToInt32(Reader["id_usuario_asignado"]);
                        tareas.Add(tarea);
                    }
                }

                connection.Close();
            }
            return tareas;
        }

        public Tarea GetById(int id)
        {
            Tarea tarea = null;
            using (SQLiteConnection connection = new SQLiteConnection(_cadenaConexion))
            {
                string queryString = @"SELECT * FROM Tarea WHERE id = @idTarea  AND activo = 1;";
                var command = new SQLiteCommand(queryString, connection);
                command.Parameters.Add(new SQLiteParameter("@idTarea", id));
                connection.Open();
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tarea = new Tarea();
                        tarea.Id = Convert.ToInt32(reader["id"]);
                        tarea.IdTablero = Convert.ToInt32(reader["id_tablero"]);
                        tarea.Nombre = reader["nombre"].ToString();
                        tarea.Estado = (EstadoTarea) Convert.ToInt32(reader["estado"]);
                        tarea.Descripcion = reader["descripcion"].ToString();
                        tarea.Color = reader["color"].ToString();
                        if (reader["id_usuario_asignado"] != DBNull.Value)
                            tarea.IdUsuarioAsignado = Convert.ToInt32(reader["id_usuario_asignado"]);
                    }
                }
                connection.Close();
            }
            if (tarea==null)
                throw new Exception("La tarea con el ID especificado no existe.");
            return tarea;
        }

        public List<Tarea> GetByIdUsuarioAsignado(int? idUsuario)
        {
            string query = @"SELECT * FROM Tarea WHERE id_usuario_asignado = @idUsuario AND activo = 1;";
            List<Tarea> tareas = new List<Tarea>();
            using (SQLiteConnection connection = new SQLiteConnection(_cadenaConexion))
            {
                SQLiteCommand command = new SQLiteCommand(query, connection);
                command.Parameters.Add(new SQLiteParameter("@idUsuario", idUsuario));
                connection.Open();

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Tarea tarea = new Tarea();
                        tarea.Id = Convert.ToInt32(reader["id"]);
                        tarea.IdTablero = Convert.ToInt32(reader["id_tablero"]);
                        tarea.Nombre = reader["nombre"].ToString();
                        tarea.Estado = (EstadoTarea) Convert.ToInt32(reader["estado"]);
                        tarea.Descripcion = reader["descripcion"].ToString();
                        tarea.Color = reader["color"].ToString();
                        if (reader["id_usuario_asignado"] != DBNull.Value)
                            tarea.IdUsuarioAsignado = Convert.ToInt32(reader["id_usuario_asignado"]);
                        tareas.Add(tarea);
                    }
                }
                connection.Close();
            }
            return tareas;
        }

        public List<Tarea> GetAllByIdTablero(int idTablero)
        {
            string query = @"SELECT * FROM Tarea WHERE id_tablero = @idTablero AND activo = 1;";
            List<Tarea> tareas = new List<Tarea>();
            using (SQLiteConnection connection = new SQLiteConnection(_cadenaConexion))
            {
                SQLiteCommand command = new SQLiteCommand(query, connection);
                command.Parameters.Add(new SQLiteParameter("@idTablero", idTablero));
                connection.Open();

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Tarea tarea = new Tarea();
                        tarea.Id = Convert.ToInt32(reader["id"]);
                        tarea.IdTablero = Convert.ToInt32(reader["id_tablero"]);
                        tarea.Nombre = reader["nombre"].ToString();
                        tarea.Estado = (EstadoTarea) Convert.ToInt32(reader["estado"]);
                        tarea.Descripcion = reader["descripcion"].ToString();
                        tarea.Color = reader["color"].ToString();
                        if (reader["id_usuario_asignado"] != DBNull.Value)
                            tarea.IdUsuarioAsignado = Convert.ToInt32(reader["id_usuario_asignado"]);
                        tareas.Add(tarea);
                    }
                }

                connection.Close();
            }
            return tareas;
        }

        public List<int> GetListIdTableroByIdUsuario(int idUsuario)
        {
            string query = @"SELECT id_tablero FROM Tarea WHERE id_usuario_asignado = @idUsuario AND activo = 1;";
            List<int> listadoIdTableros = new List<int>();
            using (SQLiteConnection connection = new SQLiteConnection(_cadenaConexion))
            {
                SQLiteCommand command = new SQLiteCommand(query, connection);
                command.Parameters.Add(new SQLiteParameter("@idUsuario", idUsuario));
                connection.Open();

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int idTablero = Convert.ToInt32(reader["id_tablero"]);
                        listadoIdTableros.Add(idTablero);
                    }
                }
                connection.Close();
            }
            return listadoIdTableros;
        }

        public void AsignarUsuarioATarea(int idUsuario, int idTarea)
        {
            int filasAfectadas = 0;
            using (SQLiteConnection connection = new SQLiteConnection(_cadenaConexion))
            {
                var queryString = @"
                UPDATE Tarea SET id_usuario_asignado = @idUsuarioAsignado
                WHERE id = @idTarea;";
                connection.Open();
                var command = new SQLiteCommand(queryString, connection);
                command.Parameters.Add(new SQLiteParameter("@isUsuarioAsignado", idUsuario));
                command.Parameters.Add(new SQLiteParameter("@idTarea", idTarea));
                filasAfectadas = command.ExecuteNonQuery();
                connection.Close();
            }
            if(filasAfectadas == 0)
                throw new Exception("No se pudo asignar la tarea al usuario.");
        }
    }
}