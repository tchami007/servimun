using Microsoft.EntityFrameworkCore;
using ServiMun.Data;
using ServiMun.Models;
using ServiMun.Shared;

namespace ServiMun.Services
{
    public class ServicioBoletaService : IServicioBoletaService
    {
        private readonly TributoMunicipalContext _context;
        public ServicioBoletaService(TributoMunicipalContext context)
        {
            _context = context;
        }

        public async Task<Result<ServicioBoleta>> AddServicioBoleta(ServicioBoletaDTO servicioBoleteDTO)
        {
            int numeroServicio = servicioBoleteDTO.NumeroServicio;
            int periodo = servicioBoleteDTO.Periodo;

            var encontrado = await _context.ServicioBoletas.FirstOrDefaultAsync(x=>x.NumeroServicio==numeroServicio && x.Periodo == periodo);
            if (encontrado != null) { return Result<ServicioBoleta>.Failure($"El servicio ya existe. Operacion  cancelada"); }
            ServicioBoleta nuevaBoleta = new ServicioBoleta
            {
                NumeroServicio=numeroServicio,
                Periodo=periodo,
                Importe=servicioBoleteDTO.Importe,
                Importe2=servicioBoleteDTO.Importe2,
                Pagado=servicioBoleteDTO.Pagado,
                Vencimiento=servicioBoleteDTO.Vencimiento,
                Vencimiento2=servicioBoleteDTO.Vencimiento2,
            };
            await _context.ServicioBoletas.AddAsync(nuevaBoleta);
            await _context.SaveChangesAsync();
            return Result<ServicioBoleta>.Success(nuevaBoleta);
        }
        public async Task<Result<ServicioBoleta>> DeleteServicioBoleta(int idServicioBoleta)
        {
            var encontrado = await _context.ServicioBoletas.FirstOrDefaultAsync(x=>x.IdBoletaServicio == idServicioBoleta);
            if(encontrado == null)
            {
                return Result<ServicioBoleta>.Failure($"La boleta de servicio no se encuentra. Operacion Cancelada");
            }
            _context.Remove(encontrado);
            await _context.SaveChangesAsync();
            return Result<ServicioBoleta>.Success(encontrado);
        }
        public async Task<Result<ServicioBoleta>> GetServicioBoletaPorId(int idServicioBoleta)
        {
            var encontrado = await _context.ServicioBoletas.FirstOrDefaultAsync(x=>x.IdBoletaServicio==idServicioBoleta);
            if (encontrado == null) { return Result<ServicioBoleta>.Failure($"Servicio no encontrado:{idServicioBoleta}"); }
            else { return Result<ServicioBoleta>.Success(encontrado); }
        }
        public async Task<IEnumerable<ServicioBoleta>> GetServicioBoletaPorNumeroServicio(int numeroServicio)
        {
            var resultado = await _context.ServicioBoletas.Where(x=>x.NumeroServicio==numeroServicio).ToListAsync();
            return resultado;
        }
        public async Task<IEnumerable<ServicioBoleta>> GetServicioBoletaPorNumeroServicioPeriodo(int numeroServicio, int periodo)
        {
            var resultado = await _context.ServicioBoletas.Where(x => x.NumeroServicio == numeroServicio && x.Periodo == periodo).ToListAsync();
            return resultado;
        }
        public async Task<Result<ServicioBoleta>> PagoServicioBoleta(int idServicioBoleta)
        {
            var encontrado = await _context.ServicioBoletas.FirstOrDefaultAsync(x=>x.IdBoletaServicio==idServicioBoleta);
            if (encontrado == null)
            {
                return Result<ServicioBoleta>.Failure($"El sevicio boleta no se encuentra. Operacion Cancelada: {idServicioBoleta}");
            }

            if (encontrado.Pagado)
            {
                return Result<ServicioBoleta>.Failure($"El sevicio boleta ya se encuentra pagado. Operacion Cancelada: {idServicioBoleta}");
            }

            encontrado.Pagado = true;
            _context.Entry(encontrado).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Result<ServicioBoleta>.Success(encontrado);
        }
        public async Task<Result<ServicioBoleta>> UpdateServicioBoleta(int idServicioBoleta, ServicioBoletaDTO sertvicioBoletaDTO)
        {
            if (idServicioBoleta != sertvicioBoletaDTO.IdBoletaServicio)
            {
                return Result<ServicioBoleta>.Failure("Los parametros Id y Dto no se corresponde. Operacion Cancelada");
            }

            var encontrado = await _context.ServicioBoletas.FirstOrDefaultAsync(x => x.IdBoletaServicio == idServicioBoleta);

            if (encontrado == null)
            {
                return Result<ServicioBoleta>.Failure("El servicio boleta no se encuentra. Operacion Cancelada");
            }

            encontrado.NumeroServicio = sertvicioBoletaDTO.NumeroServicio;
            encontrado.Importe = sertvicioBoletaDTO.Importe;
            encontrado.Importe2 = sertvicioBoletaDTO.Importe2;
            encontrado.Vencimiento = sertvicioBoletaDTO.Vencimiento;
            encontrado.Vencimiento2 = sertvicioBoletaDTO.Vencimiento2;
            encontrado.Pagado = sertvicioBoletaDTO.Pagado;

            _context.Entry(encontrado).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Result<ServicioBoleta>.Success(encontrado);
        }
    }
}
