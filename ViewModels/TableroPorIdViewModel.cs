using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_exequiel1984.Models;

namespace tl2_tp10_2023_exequiel1984.ViewModels
{
    public class TableroPorIdViewModel
    {
        private int id;
        private int idUsuarioPropietario;
        private string nombre;
        private string  descripcion;

        

        public int Id { get => id; set => id = value; }
        public int IdUsuarioPropietario { get => idUsuarioPropietario; set => idUsuarioPropietario = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }

        public TableroPorIdViewModel(Tablero tablero)
        {
            Id = tablero.Id;
            IdUsuarioPropietario = tablero.IdUsuarioPropietario;
            Nombre = tablero.Nombre;
            Descripcion = tablero.Descripcion;
        }
    }
}