using tl2_tp10_2023_exequiel1984.Models;

namespace tl2_tp10_2023_exequiel1984.ViewModels
{
    public class TableroIndexViewModel
    {
        private List<TableroElementoIndexViewModel> tablerosViewModel;
        public List<TableroElementoIndexViewModel> TablerosViewModel { get => tablerosViewModel; set => tablerosViewModel = value; }

        public TableroIndexViewModel(List<Tablero> tableros)
        {
            TablerosViewModel = new List<TableroElementoIndexViewModel>();
            foreach (var tablero in tableros)
            {
                TablerosViewModel.Add(new TableroElementoIndexViewModel
                {
                    Id = tablero.Id,
                    IdUsuarioPropietario = tablero.IdUsuarioPropietario,
                    Nombre = tablero.Nombre,
                    Descripcion = tablero.Descripcion
                });
            }
        }

    }
}