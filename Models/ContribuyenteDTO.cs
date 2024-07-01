using System.ComponentModel.DataAnnotations;

namespace ServiMun.Models
{
    public class ContribuyenteDTO
    {
        public int IdContribuyente { get; set; }
        public string NumeroDocumentoContribuyente { get; set; }
        public string ApellidoNombreContribuyente { get; set; }
        public string DomicilioCalleContribuyente { get; set; }
        public string DomicilioNumeroContribuyente { get; set; }
        public string TelefonoContribuyente { get; set; }
        public string SexoContribuyente { get; set; }
        public DateTime FechaNacimientoContribuyente { get; set; }
    }
}
