using ServiMun.Models;
using ServiMun.Shared;

namespace ServiMun.Services
{
    public interface IServicioClienteService
    {
        Task<IEnumerable<ServicioCliente>> getServicioClienteAll(int idServicio);
        Task<Result<ServicioCliente>> getServicioClienteById(int idServicio, int idContribuyente,int numeroServicio);
        Task<Result<ServicioCliente>> addServicioCliente(ServicioClienteDTO servicioCliente);
        Task<Result<ServicioCliente>> updateServicioCliente(int idServicio, int idContribuyente, int numeroServicio, ServicioClienteDTO cliente);
        Task<Result<ServicioCliente>> deleteServicioCliente(int idServicio, int idContribuyente, int numeroServicio);
    }
}
