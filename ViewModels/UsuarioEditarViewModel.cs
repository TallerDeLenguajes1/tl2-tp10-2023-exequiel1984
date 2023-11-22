using tl2_tp10_2023_exequiel1984.Models;

namespace tl2_tp10_2023_exequiel1984.ViewModels
{
    public class UsuarioEditarViewModel
    {
        private int id;
        private string nombre;
        private string contrasenia;
        private NivelDeAcceso rol;

        public int Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Contrasenia { get => contrasenia; set => contrasenia = value; }
        public NivelDeAcceso Rol { get => rol; set => rol = value; }

        public UsuarioEditarViewModel(Usuario usuario){
            this.Id = usuario.Id;
            this.Nombre = usuario.NombreDeUsuario;
            this.Contrasenia = usuario.Contrasenia;
            this.Rol = usuario.Rol;
        }
    }
}