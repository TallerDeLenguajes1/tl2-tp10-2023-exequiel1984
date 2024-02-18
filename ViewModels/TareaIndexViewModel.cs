using tl2_tp10_2023_exequiel1984.Models;

namespace tl2_tp10_2023_exequiel1984.ViewModels
{
    public class TareaIndexViewModel
    {
        private List<TareaElementoIndexViewModel> tareasViewModel;
        public List<TareaElementoIndexViewModel> TareasViewModel { get => tareasViewModel; set => tareasViewModel = value; }

        public TareaIndexViewModel(List<Tarea> tareas, List<Tablero> tableros, List<Usuario> usuarios)
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

            foreach (var tareaVM in TareasViewModel)
            {
                foreach (var tablero in tableros)
                {
                    if(tareaVM.IdTablero == tablero.Id)
                        tareaVM.NombreTablero = tablero.Nombre;
                }

                foreach (var usuario in usuarios)
                {
                    if(tareaVM.IdUsuarioAsignado == usuario.Id)
                        tareaVM.NombreUsuarioAsignado = usuario.NombreDeUsuario;
                }
            }
        }
    }
}