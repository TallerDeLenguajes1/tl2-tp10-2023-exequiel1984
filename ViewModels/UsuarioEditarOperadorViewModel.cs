using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_exequiel1984.Models;

namespace tl2_tp10_2023_exequiel1984.ViewModels
{
    public class UsuarioEditarOperadorViewModel
    {
        private int id;
        private string nombre;
        private string contrasenia;

        public int Id { get => id; set => id = value; }
        [Display(Name = "Nombre")]
        public string Nombre { get => nombre; set => nombre = value; }
        [Display(Name = "Contraseña")]
        public string Contrasenia { get => contrasenia; set => contrasenia = value; }

        public UsuarioEditarOperadorViewModel(Usuario usuario){
            this.Id = usuario.Id;
            this.Nombre = usuario.NombreDeUsuario;
            this.Contrasenia = usuario.Contrasenia;
        }

        public UsuarioEditarOperadorViewModel(){
            
        }
    }
}