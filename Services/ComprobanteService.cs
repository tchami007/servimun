using Microsoft.EntityFrameworkCore;
using ServiMun.Data;
using ServiMun.Models;
using ServiMun.Repository;
using ServiMun.Shared;

namespace ServiMun.Services
{

    public interface IComprobanteService
    {
        Task<Result<ComprobanteControl>> GetComprobanteById(int id);
        Task<Result<ComprobanteControl>> UpdateComprobante(int id);
    }
    public class ComprobanteService : IComprobanteService
    {
        private readonly IRepositoryResult<ComprobanteControl> _comprobanteRepository;

        public ComprobanteService(IRepositoryResult<ComprobanteControl> comprobanteRepository)
        {
            _comprobanteRepository = comprobanteRepository;
        }

        public async Task<Result<ComprobanteControl>> GetComprobanteById(int id)
        {
            var resultado = await _comprobanteRepository.GetById(id);
            return resultado;
        }

        public async Task<Result<ComprobanteControl>> UpdateComprobante(int id)
        {
            var encontrado = await _comprobanteRepository.GetById(id);
            if (!encontrado._succes) { return encontrado; }

            var resultado = await _comprobanteRepository.UpdateItem(encontrado._value);
            return resultado;
        }
    }

}
