using tl2_tp10_2023_exequiel1984.ViewModels;

namespace tl2_tp10_2023_exequiel1984.Models
{
    public interface IUsuarioRepository
    {
        public void Create(Usuario usuario);
        public void Update(Usuario usuario);
        public List<Usuario> GetAll();
        public Usuario GetById(int id);
        public Usuario GetUsuarioLogin(string nombre, string contrasenia);
        public void Remove(int id);
    }
}