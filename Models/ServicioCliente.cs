using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ServiMun.Models
{
    public class ServicioCliente
    {
        [Key, Column(Order = 0)]
        public int IdContribuyente { get; set; }

        [Key, Column(Order = 1)]
        public int IdServicio { get; set; }

        [Required]
        public int NumeroServicio { get; set; }

        [Required]
        public int NumeroCliente { get; set; }

        [Required]
        public int NumeroTelefono { get; set; }

        [Required]
        public bool Estado { get; set; }

        // Propiedades de navegación opcionales
        [ForeignKey("IdContribuyente")]
        public virtual Contribuyente Contribuyente { get; set; }

        [ForeignKey("IdServicio")]
        public virtual Servicio Servicio { get; set; }

        // Relación con Padron Boleta
        public ICollection<ServicioBoleta> ServicioBoletas { get; set; }

    }
}
