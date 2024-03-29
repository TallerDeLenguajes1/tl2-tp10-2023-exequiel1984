using tl2_tp10_2023_exequiel1984.ViewModels;

namespace tl2_tp10_2023_exequiel1984.Models
{
    public enum EstadoTarea
    {
        Ideas,
        ToDo,
        Doing,
        Review,
        Done    
    }

    public class Tarea
    {
        private int id;
        private int idTablero;
        private string nombre;
        private EstadoTarea estado;
        private string descripcion;
        private string color;
        private int? idUsuarioAsignado;

        public int Id { get => id; set => id = value; }
        public int IdTablero { get => idTablero; set => idTablero = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public EstadoTarea Estado { get => estado; set => estado = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public string Color { get => color; set => color = value; }
        public int? IdUsuarioAsignado { get => idUsuarioAsignado; set => idUsuarioAsignado = value; }

        public Tarea(){
            
        }

        public Tarea(TareaCrearViewModel tareaVM){
            IdTablero = tareaVM.IdTablero;
            Nombre = tareaVM.Nombre;
            Estado = tareaVM.Estado;
            Descripcion = tareaVM.Descripcion;
            Color = tareaVM.Color;
            IdUsuarioAsignado = tareaVM.IdUsuarioAsignado;
        }

        public Tarea(TareaEditarViewModel tareaVM){
            Id = tareaVM.Id;
            IdTablero = tareaVM.IdTablero;
            Nombre = tareaVM.Nombre;
            Estado = tareaVM.Estado;
            Descripcion = tareaVM.Descripcion;
            Color = tareaVM.Color;
            IdUsuarioAsignado = tareaVM.IdUsuarioAsignado;
        }

        public Tarea(TareaEditarEstadoViewModel tareaVM){
            Id = tareaVM.Id;
            
            Estado = tareaVM.Estado;
            
        }
    }
}