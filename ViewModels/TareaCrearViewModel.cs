using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_exequiel1984.Models;

namespace tl2_tp10_2023_exequiel1984.ViewModels
{
    public class TareaCrearViewModel
    {
        private int idTablero;
        private string nombre;
        private EstadoTarea estado;
        private string descripcion;
        private string color;
        private int? idUsuarioAsignado;

        List<Tablero> tableros;
        List<Usuario> usuarios;

        [Display(Name = "Tablero")] 
        public int IdTablero { get => idTablero; set => idTablero = value; }
        

        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Nombre")] 
        public string Nombre { get => nombre; set => nombre = value; }
        
        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Estado")] 
        public EstadoTarea Estado { get => estado; set => estado = value; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Descripcion")] 
        public string Descripcion { get => descripcion; set => descripcion = value; }
        
        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Color")]
        public string Color { get => color; set => color = value; }
        
        [Display(Name = "Usuario asignado")]
        public int? IdUsuarioAsignado { get => idUsuarioAsignado; set => idUsuarioAsignado = value; }
        
        public List<Tablero> Tableros { get => tableros; set => tableros = value; }
        public List<Usuario> Usuarios { get => usuarios; set => usuarios = value; }

        public TareaCrearViewModel()
        {
            
        }

        public TareaCrearViewModel(List<Tablero> listaTableros, List<Usuario> listaUsuarios){
            Tableros = listaTableros;
            Usuarios = listaUsuarios;
        }
    }
}