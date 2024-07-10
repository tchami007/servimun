using ServiMun.Models;

namespace ServiMun.Services
{

    /// <summary>
    /// Interfase de Servicio para Padron-Contribuyente
    /// </summary>
    public interface IPadronContribuyenteService
    {
        Task<PadronContribuyente> AltaContribuyentePadron(PadronContribuyente padronContribuyente);
        Task<bool> BajaContribuyentePadron(int idContribuyente, int idTributoMunicipal);
        Task<bool> ModificacionContribuyentePadron(PadronContribuyenteGetDTO padronGetDTO);
        Task<IEnumerable<PadronContribuyenteGetDTO>> RecuperaPadronContribuyente(string numeroDocumentoContribuyente);
        Task<IEnumerable<PadronContribuyenteGetDTO>> RecuperaContribuyentePadron(int numeroPadron);
        Task<IEnumerable<PadronContribuyenteGetDTO>> RecuperarContribuyentePadronId(int idContribuyente, int idTributoMunicipal);
        Task<IEnumerable<PadronContribuyenteGetDTO>> RecuperarPadronTributo(int idTributoMunicipal);

    }
}
