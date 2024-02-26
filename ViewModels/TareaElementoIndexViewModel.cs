using tl2_tp10_2023_exequiel1984.Models;

namespace tl2_tp10_2023_exequiel1984.ViewModels
{
    public class TareaElementoIndexViewModel
    {
        private int id;
        private int idTablero;
        private string nombreTablero;
        private string nombre;
        private EstadoTarea estado;
        private string descripcion;
        private string color;
        private int? idUsuarioAsignado;
        private string nombreUsuarioAsignado;
        private string nombreUsuarioPropietario;

        public int Id { get => id; set => id = value; }
        public int IdTablero { get => idTablero; set => idTablero = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public EstadoTarea Estado { get => estado; set => estado = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public string Color { get => color; set => color = value; }
        public int? IdUsuarioAsignado { get => idUsuarioAsignado; set => idUsuarioAsignado = value; }
        public string NombreTablero { get => nombreTablero; set => nombreTablero = value; }
        public string NombreUsuarioAsignado { get => nombreUsuarioAsignado; set => nombreUsuarioAsignado = value; }
        public string NombreUsuarioPropietario { get => nombreUsuarioPropietario; set => nombreUsuarioPropietario = value; }

        public bool tienePermisoDeEdicion;
        public bool tienePermisoDeEliminar;

        public TareaElementoIndexViewModel(){
            
        }

        public TareaElementoIndexViewModel(Tarea tarea)
        {
            Id = tarea.Id;
            IdTablero = tarea.IdTablero;
            Nombre = tarea.Nombre;
            Estado = tarea.Estado;
            Descripcion = tarea.Descripcion;
            Color = tarea.Color;
            IdUsuarioAsignado = tarea.IdUsuarioAsignado;
        }
    }
}