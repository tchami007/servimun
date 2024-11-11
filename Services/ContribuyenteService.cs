using Microsoft.EntityFrameworkCore;
using ServiMun.Data;
using ServiMun.Models;
using ServiMun.Repository;
using ServiMun.Shared;

namespace ServiMun.Services
{
    public interface IContribuyenteService
    {
        Task<Result<Contribuyente>> AddContribuyente(Contribuyente contribuyente);
        Task<Result<Contribuyente>> DeleteContribuyente(int id);
        Task<Result<Contribuyente>> UpdateContribuyente(Contribuyente contribuyente);
        Task<Result<Contribuyente>> GetContribuyenteById(int id);
        Task<IEnumerable<Contribuyente>> GetContribuyenteByNumero(string numeroDocumentoContribuyente);
        Task<IEnumerable<Contribuyente>> GetAllContribuyente();
    }

    public class ContribuyenteService : IContribuyenteService
    {
        private readonly IContribuyenteRepository _contribuyenteRepository;

        public ContribuyenteService(IContribuyenteRepository contribuyenteRepository)
        {
            _contribuyenteRepository = contribuyenteRepository;
        }

        public async Task<Result<Contribuyente>> AddContribuyente(Contribuyente contribuyente)
        {
            var resultado = await _contribuyenteRepository.AddContribuyente(contribuyente);
            return resultado;
        }

        public async Task<Result<Contribuyente>> DeleteContribuyente(int id)
        {
            var resultado = await _contribuyenteRepository.DeleteContribuyente(id);
            return resultado;
        }

        public async Task<Result<Contribuyente>> UpdateContribuyente(Contribuyente contribuyente)
        {
            var resultado = await _contribuyenteRepository.UpdateContribuyente(contribuyente);
            return resultado;
        }

        public async Task<Result<Contribuyente>> GetContribuyenteById(int id)
        {
            var resultado = await _contribuyenteRepository.GetContribuyenteById(id);
            return resultado;
        }

        public async Task<IEnumerable<Contribuyente>> GetContribuyenteByNumero(string numeroDocumentoContribuyente)
        {
            var resultado = await _contribuyenteRepository.GetAllContribuyenteByNumeroDocumento(numeroDocumentoContribuyente);  
            return resultado;
        }

        public async Task<IEnumerable<Contribuyente>> GetAllContribuyente()
        {
            var resultado = await _contribuyenteRepository.GetAllContribuyente();
            return resultado;
        }
    }
}
