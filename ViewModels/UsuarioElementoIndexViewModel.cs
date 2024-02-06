using tl2_tp10_2023_exequiel1984.Models;

namespace tl2_tp10_2023_exequiel1984.ViewModels
{
    public class UsuarioElementoIndexViewModel
    {
        private int id;
        private string nombreDeUsuario;
        private NivelDeAcceso rol;

        public int Id { get => id; set => id = value; }
        public string NombreDeUsuario { get => nombreDeUsuario; set => nombreDeUsuario = value; }
        public NivelDeAcceso Rol { get => rol; set => rol = value; }
    }
}