using Microsoft.EntityFrameworkCore;
using ServiMun.Data;
using ServiMun.Models;
using ServiMun.Shared;

namespace ServiMun.Services
{
    public class ServicioBoletaService : IServicioBoletaService
    {
        private readonly TributoMunicipalContext _context;
        private Random rand = new Random();
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
        public async Task<Result<IEnumerable<ServicioBoletaDTO>>> GenerarServicioBoleta(int numeroServicio, int periodoInicial, int cantidad)
        {
            int per = periodoInicial;

            int anio = int.Parse(periodoInicial.ToString().Substring(0, 4));

            int mes = int.Parse(periodoInicial.ToString().Substring(4, 2));

            if (per < 202401 || per > 202601)
            {
                return Result<IEnumerable<ServicioBoletaDTO>>.Failure("Error: periodo Inicial debe ser mayor 202401 y menor a 202601. Operacion Cancelada");
            }

            if (anio < 2024 || anio > 2026)
            {
                return Result<IEnumerable<ServicioBoletaDTO>>.Failure("Error: periodo Inicial debe ser mayor 202401 y menor a 202601. OperacioCancelada");
            }

            if (mes + cantidad - 1 > 12)
            {
                return Result<IEnumerable<ServicioBoletaDTO>>.Failure("Error: periodo + cantidad supera mes 12. OperacioCancelada");
            }


            for (int i = 0; i <= cantidad - 1; i++)
            {
                var encontrado = await _context.ServicioBoletas.FirstOrDefaultAsync(x => x.NumeroServicio == numeroServicio && x.Periodo == periodoInicial);
                if (encontrado == null)
                {
                    // Armado de importes
                    decimal importe = rand.Next(1000, 9000);
                    decimal importe2 = importe + (importe * 10 / 100);

                    // Armado de vencimientos
                    DateTime vencimiento = new DateTime();
                    DateTime vencimiento2 = new DateTime();

                    vencimiento = DateTime.Parse(anio.ToString() + "-" + (mes + i).ToString() + "-10");
                    vencimiento2 = vencimiento.AddDays(10);

                    // Creacion de boleta
                    ServicioBoleta nuevoServicio = new ServicioBoleta
                    {
                        NumeroServicio = numeroServicio,
                        Periodo = periodoInicial,
                        Importe = importe,
                        Importe2 = importe2,
                        Vencimiento = vencimiento,
                        Vencimiento2 = vencimiento2,
                        Pagado = false
                    };

                    // Registro de nuevo padron
                    await _context.ServicioBoletas.AddAsync(nuevoServicio);
                }

                // Proximo periodo
                periodoInicial = periodoInicial + 1;

            }

            await _context.SaveChangesAsync();

            var resultado = await _context.ServicioBoletas
                .Where(x => x.NumeroServicio == numeroServicio)
                .OrderBy(x => x.Periodo)
                .Select(x => new ServicioBoletaDTO
                {
                    IdBoletaServicio = x.IdBoletaServicio,
                    NumeroServicio = x.NumeroServicio,
                    Periodo = x.Periodo,
                    Importe = x.Importe,
                    Vencimiento = x.Vencimiento,
                    Pagado = x.Pagado,
                    Vencimiento2 = x.Vencimiento2,
                    Importe2 = x.Importe2
                }
                ).ToListAsync();

            return Result<IEnumerable<ServicioBoletaDTO>>.Success(resultado);
        }
    }
}
