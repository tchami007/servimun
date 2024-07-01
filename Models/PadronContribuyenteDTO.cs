using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ServiMun.Models
{
    public class PadronContribuyenteDTO
    {
        public int IdContribuyente { get; set; }
        public int IdTributoMunicipal { get; set; }
        public int NumeroPadron { get; set; }
        public bool Estado { get; set; }

    }
}
