using System.ComponentModel.DataAnnotations;

namespace ServiMun.Models
{
    public class TributoMunicipalDTO
    {
        public int IdTributo { get; set; }
        public string NombreTributo { get; set; }
        public string Sintetico { get; set; }
        public bool Estado { get; set; } // True: Activo, False: Inactivo
    }
}
