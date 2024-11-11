using Microsoft.EntityFrameworkCore;
using ServiMun.Data;
using ServiMun.Models;
using ServiMun.Shared;

namespace ServiMun.Repository
{
    public class ServicioRepository : IRepositoryResult<Servicio>
    {
        private readonly TributoMunicipalContext _context;

        public ServicioRepository (TributoMunicipalContext context)
        {
            _context = context;
        }

        public async Task<Result<Servicio>> AddItem(Servicio item)
        {
            await _context.Servicios.AddAsync(item);
            await _context.SaveChangesAsync();
            return Result<Servicio>.Success(item);
        }

        public async Task<Result<Servicio>> DeleteItem(int id)
        {
            var item  = await _context.Servicios.FirstOrDefaultAsync(x=>x.IdServicio == id);
            if (item != null)
            {
                _context.Servicios.Remove(item);
                await _context.SaveChangesAsync();
                return Result<Servicio>.Success(item);
            }
            return Result<Servicio>.Failure("Servicio no encontrado");
        }

        public async Task<IEnumerable<Servicio>> GetAll()
        {
            var resultados = await _context.Servicios.ToListAsync();
            return resultados;
        }

        public async Task<Result<Servicio>> GetById(int id)
        {
            var encontrado = await _context.Servicios.FirstOrDefaultAsync(x => x.IdServicio == id);
            if (encontrado == null)
            {
                return Result<Servicio>.Failure("No se encuentra el servicio buscado");
            }
            return Result<Servicio>.Success(encontrado);
        }

        public async Task<Result<Servicio>> UpdateItem(Servicio item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Result<Servicio>.Success(item);

        }
    }
}
