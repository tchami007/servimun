using System.ComponentModel.DataAnnotations;

namespace ServiMun.Models
{
    public class TributoMunicipal
    {
        [Key]
        public int IdTributo { get; set; }
        public string NombreTributo { get; set; }
        public string Sintetico { get; set; }
        public bool Estado { get; set; } // True: Activo, False: Inactivo

        // Relacion inversa -  PadronCntribuyente
        public ICollection<PadronContribuyente> PadronContribuyentes { get; set; }

        // Relación inversa - Movimientos
        public ICollection<Movimiento> Movimientos { get; set; }
    }
}
