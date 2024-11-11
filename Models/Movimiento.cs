using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiMun.Models
{
    public class Movimiento
    {
        [Key]
        public int IdMovimiento { get; set; }
        [Required]
        public DateTime FechaReal { get; set; }
        [Required]
        public DateTime FechaMovimiento { get; set; }
        [Required]
        public int NumeroComprobante { get; set; }
        [Required]
        public string Tipo { get; set; } /* TRIBUTO o SERVICIO */
        [Required]
        public int Numero { get; set; }
        [Required]
        public int Periodo { get; set; } /* YYYYMM, por ejemplo 202401 */
        [Required]
        [Column(TypeName = "decimal(12, 2)")]
        public decimal Importe { get; set; }
        public string Contrasiento { get; set; } /* vacio -> sin contrasentar, C -> contrasiento, M -> movimiento */

        // Claves foráneas
        public int? IdTributo { get; set; }
        public int? IdServicio { get; set; }

        // Navegación
        public virtual TributoMunicipal Tributo { get; set; }
        public virtual Servicio Servicio { get; set; }

    }
}
