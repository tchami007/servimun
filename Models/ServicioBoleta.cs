using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ServiMun.Models
{
    public class ServicioBoleta
    {
        [Key]
        public int IdBoletaServicio { get; set; }
        [Required]
        public int NumeroServicio { get; set; }
        [Required]
        public int Periodo { get; set; } // Formato AAAAMM, por ejemplo, 202406 para junio de 2024
        [Required]
        [Column(TypeName = "decimal(12, 2)")]
        public decimal Importe { get; set; }
        [Required]
        public DateTime Vencimiento { get; set; }
        [Required]
        public Boolean Pagado { get; set; }
        public DateTime Vencimiento2 { get; set; }
        [Column(TypeName = "decimal(12, 2)")]
        public decimal Importe2 { get; set; }
        [ForeignKey("NumeroServicio")]
        public virtual ServicioCliente ServicioCliente { get; set; }
    }
}
