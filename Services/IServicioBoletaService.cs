using ServiMun.Models;
using ServiMun.Shared;

namespace ServiMun.Services
{
    public interface IServicioBoletaService
    {
        Task<Result<ServicioBoleta>> AddServicioBoleta(ServicioBoletaDTO servicioBoleteDTO);
        Task<Result<ServicioBoleta>> DeleteServicioBoleta(int idServicioBoleta);
        Task<Result<ServicioBoleta>> UpdateServicioBoleta(int idServicioBoleta, ServicioBoletaDTO sertvicioBoletaDTO);
        Task<Result<ServicioBoleta>> PagoServicioBoleta(int idServicioBoleta);
        Task<Result<ServicioBoleta>> GetServicioBoletaPorId(int idServicioBoleta);
        Task<IEnumerable<ServicioBoleta>> GetServicioBoletaPorNumeroServicio(int numeroServicio);
        Task<IEnumerable<ServicioBoleta>> GetServicioBoletaPorNumeroServicioPeriodo(int numeroServicio, int periodo);
    }
}
