using tl2_tp10_2023_exequiel1984.Models;

namespace tl2_tp10_2023_exequiel1984.ViewModels
{
    public class UsuarioIndexViewModel
    {
        private List<UsuarioElementoIndexViewModel> usuariosViewModel;
        public List<UsuarioElementoIndexViewModel> UsuariosViewModel { get => usuariosViewModel; set => usuariosViewModel = value; }

        public UsuarioIndexViewModel(List<Usuario> usuarios)
        {
            UsuariosViewModel = new List<UsuarioElementoIndexViewModel>();
            foreach (var usuario in usuarios)
            {
                UsuariosViewModel.Add(new UsuarioElementoIndexViewModel
                {
                    Id = usuario.Id,
                    NombreDeUsuario = usuario.NombreDeUsuario,
                    Rol = usuario.Rol
                });
            }
        }
    }
}