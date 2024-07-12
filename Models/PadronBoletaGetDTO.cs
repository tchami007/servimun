namespace ServiMun.Models
{
    public class PadronBoletaGetDTO
    {
        public int IdBoleta { get; set; }
        public int NumeroPadron { get; set; }
        public int Periodo { get; set; }
        public decimal Importe { get; set; }
        public DateTime Vencimiento { get; set; }
        public bool Pagado { get; set; }
        public DateTime Vencimiento2 { get; set; }
        public decimal Importe2 { get; set; }

        public ContribuyenteDTO Contribuyente { get; set; }
    }
}
