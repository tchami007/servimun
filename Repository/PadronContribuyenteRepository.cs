using Microsoft.EntityFrameworkCore;
using ServiMun.Data;
using ServiMun.Models;
using ServiMun.Shared;

namespace ServiMun.Repository
{
    public interface IPadronContribuyenteRepository
    {
        public Task<Result<PadronContribuyente>> AddItem(PadronContribuyente item);
        public Task<Result<PadronContribuyente>> DeleteItem(int idContribuyente, int idTributo);
        public Task<Result<PadronContribuyente>> UpdateItem(PadronContribuyente item);
        public Task<Result<PadronContribuyente>> GetById(int idContribuyente, int IdTributo);
        public Task<IEnumerable<PadronContribuyente>> GetAllByIdTributo(int idTributo);
        public Task<Result<PadronContribuyente>> GetByNumeroPadron(int numeroPadron);
    }

    public class PadronContribuyenteRepository : IPadronContribuyenteRepository
    {
        private readonly TributoMunicipalContext _context;

        public PadronContribuyenteRepository(TributoMunicipalContext context) 
        {
            _context = context;
        }
        public async Task<Result<PadronContribuyente>> AddItem(PadronContribuyente item)
        {
            await _context.PadronContribuyentes.AddAsync(item);
            await _context.SaveChangesAsync();
            return Result<PadronContribuyente>.Success(item);
        }

        public async Task<Result<PadronContribuyente>> DeleteItem(int idContribuyente, int idTributo)
        {
            var padron = await _context.PadronContribuyentes.FirstOrDefaultAsync(x=>x.IdContribuyente==idContribuyente && x.IdTributoMunicipal == idTributo);
            if (padron == null) return Result<PadronContribuyente>.Failure("Padron Contribuyente no identificado");

            _context.PadronContribuyentes.Remove(padron);
            await _context.SaveChangesAsync();
            return Result<PadronContribuyente>.Success(padron);
        }
        public async Task<Result<PadronContribuyente>> UpdateItem(PadronContribuyente item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Result<PadronContribuyente>.Success(item);
        }
        public async Task<IEnumerable<PadronContribuyente>> GetAllByIdTributo(int idTributo)
        {
            return await _context.PadronContribuyentes
                .Where(x=>x.IdTributoMunicipal==idTributo)
                .OrderBy(x=>x.NumeroPadron)
                .ToListAsync();
        }

        public async Task<Result<PadronContribuyente>> GetById(int idContribuyente, int idTributo)
        {
            var resultado = await _context.PadronContribuyentes
            .FirstOrDefaultAsync(pc => pc.IdContribuyente == idContribuyente && pc.IdTributoMunicipal == idTributo);
            return Result<PadronContribuyente>.Success(resultado);

        }

        public async Task<Result<PadronContribuyente>> GetByNumeroPadron(int numeroPadron)
        {
            var resultado = await _context.PadronContribuyentes
            .FirstOrDefaultAsync(pc => pc.NumeroPadron == numeroPadron);
            return Result<PadronContribuyente>.Success(resultado);

        }
    }
}
