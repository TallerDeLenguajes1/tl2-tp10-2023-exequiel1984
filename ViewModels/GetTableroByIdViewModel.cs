using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_exequiel1984.Models;

namespace tl2_tp10_2023_exequiel1984.ViewModels
{
    public class GetTableroByIdViewModel
    {
        private int id;
        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Id del Tablero")] 
        public int Id { get => id; set => id = value; }

        public GetTableroByIdViewModel()
        {

        }
    }
}