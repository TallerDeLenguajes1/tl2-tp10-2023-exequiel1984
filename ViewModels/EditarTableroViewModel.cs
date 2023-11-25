using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_exequiel1984.Models;

namespace tl2_tp10_2023_exequiel1984.ViewModels
{
    public class EditarTableroViewModel
    { 
        private int id;
        private int idUsuarioPropietario;
        private string nombre;
        private string descripcion;

        public int Id { get => id; set => id = value; }
        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Id del usuario propietario")] 
        public int IdUsuarioPropietario { get => idUsuarioPropietario; set => idUsuarioPropietario = value; }
        [Required(ErrorMessage = "Este campo es requerido.")]
        public string Nombre { get => nombre; set => nombre = value; }
        [Required(ErrorMessage = "Este campo es requerido.")]
        public string Descripcion { get => descripcion; set => descripcion = value; }

        public EditarTableroViewModel()
        {
            
        }

        public EditarTableroViewModel(Tablero tablero)
        {
            Id = tablero.Id;
            IdUsuarioPropietario = tablero.IdUsuarioPropietario;
            Nombre = tablero.Nombre;
            Descripcion = tablero.Descripcion;
        }
    }
}