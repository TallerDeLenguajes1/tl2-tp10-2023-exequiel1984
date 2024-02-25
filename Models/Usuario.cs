using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_exequiel1984.ViewModels;

namespace tl2_tp10_2023_exequiel1984.Models
{
    public enum NivelDeAcceso
    {
        administrador,
        operador
    }

    public class Usuario
    {
        private int id;
        private string nombreDeUsuario;
        private string contrasenia;
        private NivelDeAcceso rol;

        public int Id { get => id; set => id = value; }
        public string NombreDeUsuario { get => nombreDeUsuario; set => nombreDeUsuario = value; }
        public string Contrasenia { get => contrasenia; set => contrasenia = value; }
        public NivelDeAcceso Rol { get => rol; set => rol = value; }

        public Usuario()
        {
            
        }

        public Usuario(LoginViewModel loginViewModel)
        {
            NombreDeUsuario = loginViewModel.Nombre;
            Contrasenia = loginViewModel.Contrasenia;
        }

        public Usuario(UsuarioEditarViewModel usuario){
            this.Id = usuario.Id;
            this.NombreDeUsuario = usuario.Nombre;
            this.Contrasenia = usuario.Contrasenia;
            this.Rol = usuario.Rol;
        }

        public Usuario(UsuarioCrearViewModel usuario){
            this.NombreDeUsuario = usuario.Nombre;
            this.Contrasenia = usuario.Contrasenia;
            this.Rol = usuario.Rol;
        }
    }
}