using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiMun.Models
{
    public class PadronBoleta
    {
        [Key]
        public int IdBoleta { get; set; }
        [Required]
        public int NumeroPadron { get; set; }
        [Required]
        public int Periodo { get; set; } // Formato AAAAMM, por ejemplo, 202406 para junio de 2024
        [Required]
        [Column(TypeName = "decimal(12, 2)")]
        public decimal Importe { get; set; }
        [Required]
        public DateTime Vencimiento { get; set; }
        [Required]
        public Boolean Pagado { get; set; }
        [ForeignKey("NumeroPadron")]
        public virtual PadronContribuyente PadronContribuyente { get; set; }

    }
}
