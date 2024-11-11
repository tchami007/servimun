using Microsoft.EntityFrameworkCore;
using ServiMun.Data;
using ServiMun.Models;
using ServiMun.Shared;

namespace ServiMun.Repository
{
    public interface IServicioBoletaRepository
    {
        Task<Result<ServicioBoleta>> AddServicioBoleta(ServicioBoleta servicioBoleta);
        Task<Result<ServicioBoleta>> DeleteServicioBoleta(int idServicioBoleta);
        Task<Result<ServicioBoleta>> UpdateServicioBoleta(ServicioBoleta servicioBoleta);
        Task<Result<ServicioBoleta>> PagoServicioBoleta(ServicioBoleta servicioBoleta);
        Task<Result<ServicioBoleta>> GetServicioBoletaPorId(int idServicioBoleta);
        Task<IEnumerable<ServicioBoleta>> GetServicioBoletaPorNumeroServicio(int numeroServicio);
        Task<Result<ServicioBoleta>> GetServicioBoletaPorNumeroServicioPeriodo(int numeroServicio, int periodo);
    }

    public class ServicioBoletaRepository : IServicioBoletaRepository
    {
        private readonly TributoMunicipalContext _context;
        private Random rand = new Random();
        public ServicioBoletaRepository (TributoMunicipalContext context)
        {
            _context = context;
        }
        public async Task<Result<ServicioBoleta>> AddServicioBoleta(ServicioBoleta servicioBoleta)
        {
            await _context.ServicioBoletas.AddAsync(servicioBoleta);
            await _context.SaveChangesAsync();
            return Result<ServicioBoleta>.Success(servicioBoleta);
        }
        public async Task<Result<ServicioBoleta>> DeleteServicioBoleta(int idServicioBoleta)
        {
            var encontrado = await _context.ServicioBoletas.FirstOrDefaultAsync(x => x.IdBoletaServicio == idServicioBoleta);
            if (encontrado == null)
            {
                return Result<ServicioBoleta>.Failure($"La boleta de servicio no se encuentra. Operacion Cancelada");
            }
            _context.Remove(encontrado);
            await _context.SaveChangesAsync();
            return Result<ServicioBoleta>.Success(encontrado);
        }
        public async Task<Result<ServicioBoleta>> GetServicioBoletaPorId(int idServicioBoleta)
        {
            var encontrado = await _context.ServicioBoletas.FirstOrDefaultAsync(x => x.IdBoletaServicio == idServicioBoleta);
            if (encontrado == null) { return Result<ServicioBoleta>.Failure($"Servicio no encontrado:{idServicioBoleta}"); }
            else { return Result<ServicioBoleta>.Success(encontrado); }
        }
        public async Task<IEnumerable<ServicioBoleta>> GetServicioBoletaPorNumeroServicio(int numeroServicio)
        {
            var resultado = await _context.ServicioBoletas.Where(x => x.NumeroServicio == numeroServicio).ToListAsync();
            return resultado;
        }
        public async Task<Result<ServicioBoleta>> GetServicioBoletaPorNumeroServicioPeriodo(int numeroServicio, int periodo)
        {
            var resultado = await _context.ServicioBoletas
                .FirstOrDefaultAsync(x=>x.NumeroServicio==numeroServicio && x.Periodo ==periodo);
            return Result<ServicioBoleta>.Success(resultado);
        }
        public async Task<Result<ServicioBoleta>> PagoServicioBoleta(ServicioBoleta servicioBoleta)
        {
            _context.Entry(servicioBoleta).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Result<ServicioBoleta>.Success(servicioBoleta);
        }
        public async Task<Result<ServicioBoleta>> UpdateServicioBoleta(ServicioBoleta servicioBoleta)
        {
            _context.Entry(servicioBoleta).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Result<ServicioBoleta>.Success(servicioBoleta);
        }
    }
}
