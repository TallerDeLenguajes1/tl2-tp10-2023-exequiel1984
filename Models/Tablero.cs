using System.Runtime.CompilerServices;
using tl2_tp10_2023_exequiel1984.ViewModels;

namespace tl2_tp10_2023_exequiel1984.Models
{
    public class Tablero
    {
        private int id;
        private int idUsuarioPropietario;
        private string nombre;
        private string  descripcion;

        public int Id { get => id; set => id = value; }
        public int IdUsuarioPropietario { get => idUsuarioPropietario; set => idUsuarioPropietario = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }

        public Tablero()
        {
            
        }

        public Tablero(CrearTableroViewModel tableroVM)
        {
            IdUsuarioPropietario = tableroVM.IdUsuarioPropietario;
            Nombre = tableroVM.Nombre;
            Descripcion = tableroVM.Descripcion;
        }

        public Tablero(TableroEditarViewModel tableroVM)
        {
            this.Id = tableroVM.Id;
            this.IdUsuarioPropietario = tableroVM.IdUsuarioPropietario;
            this.Nombre = tableroVM.Nombre;
            this.Descripcion = tableroVM.Descripcion;
        }
    }
}