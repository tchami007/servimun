using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ServiMun.Models
{
    public class ServicioClienteDTO
    {
        public int IdContribuyente { get; set; }
        public int IdServicio { get; set; }
        [Required]
        public int NumeroServicio { get; set; }
        [Required]
        public int NumeroCliente { get; set; }
        [Required]
        public int NumeroTelefono { get; set; }
        [Required]
        public bool Estado { get; set; }
    }
}
