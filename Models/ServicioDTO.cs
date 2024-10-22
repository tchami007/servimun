using System.ComponentModel.DataAnnotations;

namespace ServiMun.Models
{
    public class ServicioDTO
    {
        public int IdServicio { get; set; }
        public string NombreServicio { get; set; }
        public string Sintetico { get; set; }
        public bool Estado { get; set; } // True: Activo, False: Inactivo
    }
}
