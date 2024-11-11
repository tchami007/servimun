using Microsoft.EntityFrameworkCore;
using ServiMun.Data;
using ServiMun.Models;
using ServiMun.Shared;

namespace ServiMun.Repository
{
    public interface IMovimientoRepository
    {
        Task<Result<Movimiento>> AddMovimiento(Movimiento movimiento);
        Task<Result<Movimiento>> UpdateMovimiento(Movimiento movimiento);
        Task<Result<Movimiento>> GetMovimientoById(int id);
        Task<IEnumerable<Movimiento>> GetAllMovimientoByFecha(DateTime fechaMovimiento);
        Task<Result<Movimiento>> GetMovimientoByTipoNumeroPeriodo(string tipo, int numero, int periodo);
        Task<Result<IEnumerable<Movimiento>>> GetAllMovimientoByTipoId(string tipo, int id, DateTime fecha);
        Task<Result<IEnumerable<Movimiento>>> GetAllMovimientoTotalByTipoId(string tipo, int id, DateTime fecha);
    }
    public class MovimientoRepository : IMovimientoRepository
    {
        private readonly TributoMunicipalContext _context;
        public MovimientoRepository(TributoMunicipalContext context)
        {
            _context = context;
        }
        public async Task<Result<Movimiento>> AddMovimiento(Movimiento movimiento)
        {
            await _context.Movimientos.AddAsync(movimiento);
            await _context.SaveChangesAsync();
            return Result<Movimiento>.Success(movimiento);
        }
        public async Task<Result<Movimiento>> UpdateMovimiento(Movimiento movimiento)
        {
            _context.Entry(movimiento).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Result<Movimiento>.Success(movimiento);

        }
        public async Task<Result<Movimiento>> GetMovimientoById(int id)
        {
            var resultado = await _context.Movimientos.FirstOrDefaultAsync(x=>x.IdMovimiento == id);

            if(resultado == null) 
            {
                return Result<Movimiento>.Failure("Movimiento no identificado");
            }

            return Result<Movimiento>.Success(resultado);
        }
        public async Task<IEnumerable<Movimiento>> GetAllMovimientoByFecha(DateTime fechaMovimiento)
        {
            var resultados = await _context.Movimientos
            .Where(x => x.FechaMovimiento == fechaMovimiento)
            .ToListAsync();

            return resultados;
        }
        public async Task<Result<Movimiento>> GetMovimientoByTipoNumeroPeriodo(string tipo, int numero, int periodo)
        {
            var resultado = await _context.Movimientos.FirstOrDefaultAsync(x=>x.Numero==numero && x.Tipo == tipo && x.Periodo == periodo && x.Contrasiento=="");
            return Result<Movimiento>.Success(resultado);
        }
        public async Task<Result<IEnumerable<Movimiento>>> GetAllMovimientoByTipoId(string tipo, int id, DateTime fecha)
        {
            IEnumerable<Movimiento> resultados = await _context.Movimientos
                .Where(x => x.FechaMovimiento == fecha && x.Contrasiento == "")
                .ToListAsync();

            if (tipo == "Tributo")
            {
                resultados = await _context.Movimientos
                    .Where(x => x.IdTributo == id && x.FechaMovimiento == fecha && x.Contrasiento == "")
                    .ToListAsync();
            }

            if (tipo == "Servicio")
            {
                resultados = await _context.Movimientos
                    .Where(x => x.IdServicio == id && x.FechaMovimiento == fecha && x.Contrasiento == "")
                    .ToListAsync();
            }
            return Result<IEnumerable<Movimiento>>.Success(resultados);
        }
        public async Task<Result<IEnumerable<Movimiento>>> GetAllMovimientoTotalByTipoId(string tipo, int id, DateTime fecha)
        {
            IEnumerable<Movimiento> resultados = await _context.Movimientos
                .Where(x => x.FechaMovimiento == fecha )
                .ToListAsync();

            if (tipo == "Tributo")
            {
                resultados = await _context.Movimientos
                    .Where(x => x.IdTributo == id && x.FechaMovimiento == fecha )
                    .ToListAsync();
            }

            if (tipo == "Servicio")
            {
                resultados = await _context.Movimientos
                    .Where(x => x.IdServicio == id && x.FechaMovimiento == fecha )
                    .ToListAsync();
            }
            return Result<IEnumerable<Movimiento>>.Success(resultados);
        }
    }
}
