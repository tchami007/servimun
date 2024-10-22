using ServiMun.Models;
using ServiMun.Shared;

namespace ServiMun.Services
{
    public interface IServicioService
    {
        Task<Result<Servicio>> AddServicio(ServicioDTO servicioDTO);
        Task<Result<Servicio>> DeleteServicio(int id);
        Task<Result<Servicio>> UpdateServicio(int id, ServicioDTO servicioDTO);
        Task<Result<Servicio>> GetServicio(int id);
        Task<IEnumerable<Servicio>> GetAllServicio();
        Task<IEnumerable<Servicio>> GetAllBySintetico(string sintetico);
    }
}
