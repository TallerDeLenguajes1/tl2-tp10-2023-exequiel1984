using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_exequiel1984.Models;

namespace tl2_tp10_2023_exequiel1984.ViewModels
{
    public class TareaEditarEstadoViewModel
    {
        private int id;
        
        private EstadoTarea estado;
        

        public int Id { get => id; set => id = value; }
        
        
        
        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Estado")] 
        public EstadoTarea Estado { get => estado; set => estado = value; }

        

        public TareaEditarEstadoViewModel(){
            
        }

        public TareaEditarEstadoViewModel(Tarea tarea)
        {
            Id = tarea.Id;
            
            Estado = tarea.Estado;
            
        }
    }
}