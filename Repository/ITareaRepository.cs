namespace tl2_tp10_2023_exequiel1984.Models
{
    public interface ITareaRepository
    {
        public Tarea Create(Tarea tarea);
        public void UpDate(Tarea tarea);
        public void UpDateNombre(int id, string nombre);
        public void UpDateEstado(int id, EstadoTarea Estado);
        public Tarea GetById(int id);
        public List<Tarea> GetAll();
        public List<Tarea> GetAllByIdUsuario(int idUsuario);
        public List<Tarea> GetAllByIdTablero(int idTablero);
        public void Remove(int id);
        public void AsignarUsuarioATarea(int idUsuario, int idTarea);
        //public int CantidadTareasByEstado(EstadoTarea estado);
    }
}