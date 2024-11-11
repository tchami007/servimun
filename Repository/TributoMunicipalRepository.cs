using Microsoft.EntityFrameworkCore;
using ServiMun.Data;
using ServiMun.Models;
using ServiMun.Shared;

namespace ServiMun.Repository
{
    public class TributoMunicipalRepository : IRepositoryResult<TributoMunicipal>
    {
        private readonly TributoMunicipalContext _context;

        public TributoMunicipalRepository (TributoMunicipalContext context)
        {
            _context = context;
        }

        public async Task<Result<TributoMunicipal>> AddItem(TributoMunicipal item)
        {
            await _context.TributosMunicipales.AddAsync(item);
            await _context.SaveChangesAsync();
            return Result<TributoMunicipal>.Success(item);
        }

        public async Task<Result<TributoMunicipal>> DeleteItem(int id)
        {
            var tributo = await _context.TributosMunicipales.FindAsync(id);
            if (tributo == null) return Result<TributoMunicipal>.Failure("Tributo no identificado");
            _context.TributosMunicipales.Remove(tributo);
            await _context.SaveChangesAsync();
            return Result<TributoMunicipal>.Success(tributo);
        }

        public async Task<IEnumerable<TributoMunicipal>> GetAll()
        {
            return await _context.TributosMunicipales.ToListAsync();
        }

        public async Task<Result<TributoMunicipal>> GetById(int id)
        {
            var resultado = await _context.TributosMunicipales.FirstOrDefaultAsync(x=>x.IdTributo==id);
            return Result<TributoMunicipal>.Success(resultado);
        }

        public async Task<Result<TributoMunicipal>> UpdateItem(TributoMunicipal item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Result<TributoMunicipal>.Success(item);
        }
    }
}
