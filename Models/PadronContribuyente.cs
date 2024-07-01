using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ServiMun.Models
{
    public class PadronContribuyente
    {
        [Key, Column(Order = 0)]
        public int IdContribuyente { get; set; }

        [Key, Column(Order = 1)]
        public int IdTributoMunicipal { get; set; }

        [Required]
        public int NumeroPadron { get; set; }

        [Required]
        public bool Estado { get; set; }

        // Propiedades de navegación opcionales
        [ForeignKey("IdContribuyente")]
        public virtual Contribuyente Contribuyente { get; set; }
        [ForeignKey("IdTributoMunicipal")]
        public virtual TributoMunicipal TributoMunicipal { get; set; }

        // Relación con Padron Boleta
        public ICollection<PadronBoleta> PadronBoletas { get; set; }

    }
}
