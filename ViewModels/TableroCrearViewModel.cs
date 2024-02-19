using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_exequiel1984.Models;

namespace tl2_tp10_2023_exequiel1984.ViewModels
{
    public class TableroCrearViewModel
    { 
        private List<Usuario> usuarios;
        private int idUsuarioPropietario;
        private string nombre;
        private string descripcion;

        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Usuario propietario")]
        public int IdUsuarioPropietario { get => idUsuarioPropietario; set => idUsuarioPropietario = value; }
        
        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Nombre")]
        public string Nombre { get => nombre; set => nombre = value; }
        
        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Descripcion")] 
        public string Descripcion { get => descripcion; set => descripcion = value; }
        
        
        public List<Usuario> Usuarios { get => usuarios; set => usuarios = value; }

        public TableroCrearViewModel(List<Usuario> ListaUsuarios){
            Usuarios = ListaUsuarios;
        } 

        public TableroCrearViewModel(){
            
        }
    }
}