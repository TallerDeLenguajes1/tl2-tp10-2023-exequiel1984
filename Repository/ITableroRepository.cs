namespace tl2_tp10_2023_exequiel1984.Models
{
    public interface ITableroRepository
    {
        public Tablero Create(Tablero tablero);
        public void UpDate(Tablero tablero);
        public Tablero GetById(int id);
        public List<Tablero> GetAll();
        public List<Tablero> GetByIdUsuarioPropietario(int idUsuario);
        public List<int> GetListIdByUsuarioPropietario(int idUsuarioPropietario);
        public void Remove(int id);
        public string GetNameById(int idTablero);
        public int GetIdUsuarioPropietarioById(int idTablero);
    }
}