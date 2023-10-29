using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_exequiel1984.Models;

namespace tl2_tp10_2023_exequiel1984.ViewModels
{
    public class UsuarioCrearViewModel
    {
        private int id;
        
        private string nombre;
        private string contrasenia;
        private NivelDeAcceso rol;

        public int Id { get => id; set => id = value; }
        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Nombre")] 
        public string Nombre { get => nombre; set => nombre = value; }
        [Display(Name = "Contraseña")] 
        //[PasswordPropertyText(true)]
        
        public string Contrasenia { get => contrasenia; set => contrasenia = value; }
        public NivelDeAcceso Rol { get => rol; set => rol = value; }

        public UsuarioCrearViewModel(){
            
        }
    }
}