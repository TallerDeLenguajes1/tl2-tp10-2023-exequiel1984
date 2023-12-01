using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_exequiel1984.Models;

namespace tl2_tp10_2023_exequiel1984.ViewModels
{
    public class CrearTableroViewModel
    { 
        private List<Usuario> usuarios;
        private int idUsuarioPropietario;
        private string nombre;
        private string descripcion;

        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Id del usuario propietario")] 
        public int IdUsuarioPropietario { get => idUsuarioPropietario; set => idUsuarioPropietario = value; }
        [Required(ErrorMessage = "Este campo es requerido.")]
        public string Nombre { get => nombre; set => nombre = value; }
        [Required(ErrorMessage = "Este campo es requerido.")]
        public string Descripcion { get => descripcion; set => descripcion = value; }
        [Required(ErrorMessage = "Este campo es requerido.")]
        public List<Usuario> Usuarios { get => usuarios; set => usuarios = value; }

        public CrearTableroViewModel(List<Usuario> ListaUsuarios){
            Usuarios = ListaUsuarios;
        }
        public CrearTableroViewModel(){
        }
    }
}