using System.ComponentModel.DataAnnotations;

namespace ServiMun.Models
{
    public class Contribuyente
    {
        [Key]
        public int IdContribuyente { get; set; }

        [Required]
        [StringLength(11, MinimumLength = 11)]
        public string NumeroDocumentoContribuyente { get; set; }

        [Required]
        public string ApellidoNombreContribuyente { get; set; }

        [Required]
        public string DomicilioCalleContribuyente { get; set; }

        [Required]
        public string DomicilioNumeroContribuyente { get; set; }

        public string TelefonoContribuyente { get; set; }

        [Required]
        [RegularExpression("^[MF]$")]
        public string SexoContribuyente { get; set; }

        [Required]
        public DateTime FechaNacimientoContribuyente { get; set; }

        //Relacion con Padron Contribuyente
        public ICollection<PadronContribuyente> PadronContribuyentes { get; set; }
    }
}
