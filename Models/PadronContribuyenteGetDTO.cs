namespace ServiMun.Models
{
    public class PadronContribuyenteGetDTO
    {
        public int IdTributoMunicipal { get; set; }
        public int IdContribuyente { get; set; }
        public string NumeroDocumentoContribuyente { get; set; }
        public string ApellidoNombreContribuyente { get; set; }
        public int NumeroPadron { get; set; }
        public bool Estado { get; set; }
        public string TributoDescripcion { get; set; }
    }
}
