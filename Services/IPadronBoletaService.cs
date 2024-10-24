using ServiMun.Models;
using ServiMun.Shared;

namespace ServiMun.Services
{
    public interface IPadronBoletaService
    {
        Task<PadronBoleta> AltaPadronBoleta(PadronBoleta padronBoleta);
        Task<bool> BajaPadronBoleta(int id);
        Task<PadronBoleta> ModificacionPadronBoleta(int id, PadronBoleta padronBoleta);
        Task<PadronBoletaDTO> PagoPadronBoleta(int id);
        Task<IEnumerable<PadronBoletaDTO>> RecuperarPadronBoletaPorId(int id);
        Task<IEnumerable<PadronBoletaGetDTO>> RecuperarPadronBoletaPorNumeroPadron(int numeroPadron);
        Task<IEnumerable<PadronBoletaGetDTO>> RecuperarPadronBoletaPorTributoPeriodo(int idTributo, int periodo);
        Task<IEnumerable<PadronBoletaGetDTO>> RecuperarPadronBoletaPorNumeroPadronPeriodo(int numeroPadron, int periodo);
        Task<Result<IEnumerable<PadronBoletaDTO>>> GenerarPadronBoleta(int numeroPadron, int periodoInicial, int cantidad);
    }

}
