using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiMun.Data;
using ServiMun.Models;
using ServiMun.Repository;
using ServiMun.Shared;

namespace ServiMun.Services
{
    public interface IServicioClienteService 
    { 
        Task<Result<ServicioCliente>> addServicioCliente(ServicioClienteDTO servicioClienteDTO);
        Task<Result<ServicioCliente>> deleteServicioCliente(int idServicio, int idContribuyente, int numeroServicio);
        Task<Result<ServicioCliente>> updateServicioCliente(int idServicio, int idContribuyente, int numeroServicio, ServicioClienteDTO servicioCliente);
        Task<Result<ServicioCliente>> getServicioClienteById(int idServicio, int idContribuyente, int numeroServicio);
        Task<IEnumerable<ServicioCliente>> getServicioClienteAll(int idServicio);
        Task<Result<ServicioCliente>> getServicioClienteByNumeroServicio(int numeroServicio);
    }

    public class ServicioClienteService : IServicioClienteService
    {
        private readonly IServicioClienteRepository _servicioClienteRepository;
        public ServicioClienteService(IServicioClienteRepository servicioClienteRepository)
        {
            _servicioClienteRepository = servicioClienteRepository;
        }
        public async Task<Result<ServicioCliente>> addServicioCliente(ServicioClienteDTO servicioCliente)
        {
            var resultado = await _servicioClienteRepository.addServicioCliente(servicioCliente);
            return resultado;
        }
        public async Task<Result<ServicioCliente>> deleteServicioCliente(int idServicio, int idContribuyente, int numeroServicio)
        {
            var resultado = await _servicioClienteRepository.deleteServicioCliente(idServicio, idContribuyente, numeroServicio);
            return resultado;
        }
        public async Task<Result<ServicioCliente>> updateServicioCliente(int idServicio, int idContribuyente, int numeroServicio, ServicioClienteDTO servicioCliente)
        {
            var resultado = await _servicioClienteRepository.updateServicioCliente(idServicio, idContribuyente, numeroServicio, servicioCliente);
            return resultado;
        }
        public async Task<IEnumerable<ServicioCliente>> getServicioClienteAll(int idServicio)
        {
            var resultados = await _servicioClienteRepository.getServicioClienteAllByIdServicio(idServicio);
            return resultados;
        }
        public async Task<Result<ServicioCliente>> getServicioClienteById(int idServicio, int idContribuyente, int numeroServicio)
        {
            var resultado = await _servicioClienteRepository.getServicioClienteById(idServicio,idContribuyente,numeroServicio);
            return resultado;
        }

        public async Task<Result<ServicioCliente>> getServicioClienteByNumeroServicio(int numeroServicio)
        {
            var resultado = await _servicioClienteRepository.getServicioClienteByNumeroServicio(numeroServicio);
            return resultado;
        }
    }
}
