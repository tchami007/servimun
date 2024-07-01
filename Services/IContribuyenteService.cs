using ServiMun.Models;

namespace ServiMun.Services
{
    public interface IContribuyenteService
    {
        Task<Contribuyente> AltaNuevoContribuyente(Contribuyente contribuyente);
        Task<bool> BajaContribuyente(int id);
        Task<Contribuyente> ModificacionContribuyente(Contribuyente contribuyente);
        Task<Contribuyente> RecuperacionContribuyente(int id);
        Task<IEnumerable<Contribuyente>> RecuperacionContribuyentePorNumero(string numeroDocumentoContribuyente);
        Task<IEnumerable<Contribuyente>> RecuperacionTodosContribuyente();
    }
}
