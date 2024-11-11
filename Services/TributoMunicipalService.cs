using Microsoft.EntityFrameworkCore;
using ServiMun.Data;
using ServiMun.Models;
using ServiMun.Repository;
using ServiMun.Shared;

namespace ServiMun.Services
{
    public interface ITributoMunicipalService
    {
        Task<Result<TributoMunicipal>> AddTributoMunicipal(TributoMunicipal tributo);
        Task<Result<TributoMunicipal>> DeleteTributoMunicipal(int id);
        Task<Result<TributoMunicipal>> UpdateTributoMunicipal(TributoMunicipal tributo);
        Task<Result<TributoMunicipal>> GetTributoMunicipalById(int id);
        Task<IEnumerable<TributoMunicipal>> GetAllTributoMunicipal();
    }

    public class TributoMunicipalService : ITributoMunicipalService
    {
        private readonly IRepositoryResult<TributoMunicipal> _tributo;

        public TributoMunicipalService(IRepositoryResult<TributoMunicipal> tributo)
        {
            _tributo = tributo;
        }

        public async Task<Result<TributoMunicipal>> AddTributoMunicipal(TributoMunicipal tributo)
        {
            var resultado = await _tributo.AddItem(tributo);
            return resultado;
        }

        public async Task<Result<TributoMunicipal>> DeleteTributoMunicipal(int id)
        {
            var resultado = await _tributo.DeleteItem(id);
            return resultado;
        }

        public async Task<Result<TributoMunicipal>> UpdateTributoMunicipal(TributoMunicipal tributo)
        {
            var resultado = await _tributo.UpdateItem(tributo);
            return resultado;
        }

        public async Task<Result<TributoMunicipal>> GetTributoMunicipalById(int id)
        {
            return await _tributo.GetById(id);
        }

        public async Task<IEnumerable<TributoMunicipal>> GetAllTributoMunicipal()
        {
            return await _tributo.GetAll();
        }
    }
}