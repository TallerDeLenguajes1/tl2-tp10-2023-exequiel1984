using tl2_tp10_2023_exequiel1984.Models;

namespace tl2_tp10_2023_exequiel1984.ViewModels
{
    public class TareaIndexViewModel
    {
        private List<TareaElementoIndexViewModel> tareasViewModel;
        public List<TareaElementoIndexViewModel> TareasViewModel { get => tareasViewModel; set => tareasViewModel = value; }

        private string nombrePropietarioTablero;
        public string NombrePropietarioTablero { get => nombrePropietarioTablero; set => nombrePropietarioTablero = value; }

        public TareaIndexViewModel(List<Tarea> tareas)
        {
            TareasViewModel = new List<TareaElementoIndexViewModel>();
            foreach (var tarea in tareas)
            {
                TareasViewModel.Add(new TareaElementoIndexViewModel
                {
                    Id = tarea.Id,
                    IdTablero = tarea.IdTablero,
                    Nombre = tarea.Nombre,
                    Estado = tarea.Estado,
                    Descripcion = tarea.Descripcion,
                    Color = tarea.Color,
                    IdUsuarioAsignado = tarea.IdUsuarioAsignado
                });
            }
        }
    }
}