using tl2_tp10_2023_exequiel1984.Models;

namespace tl2_tp10_2023_exequiel1984.ViewModels
{
    public class IndexUsuarioViewModel
    {
        private List<ElementoIndexUsuarioViewModel> usuariosViewModel;
        public List<ElementoIndexUsuarioViewModel> UsuariosViewModel { get => usuariosViewModel; set => usuariosViewModel = value; }

        public IndexUsuarioViewModel(List<Usuario> usuarios)
        {
            UsuariosViewModel = new List<ElementoIndexUsuarioViewModel>();
            foreach (var usuario in usuarios)
            {
                UsuariosViewModel.Add(new ElementoIndexUsuarioViewModel
                {
                    Id = usuario.Id,
                    NombreDeUsuario = usuario.NombreDeUsuario,
                    Rol = usuario.Rol
                });
            }
        }

    }
}