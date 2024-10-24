using Microsoft.EntityFrameworkCore;
using ServiMun.Data;
using ServiMun.Models;
using ServiMun.Shared;
using System.Runtime.InteropServices;

namespace ServiMun.Services
{
    public class PadronBoletaService : IPadronBoletaService
    {
        private readonly TributoMunicipalContext _context;
        private Random rand = new Random();

        public PadronBoletaService(TributoMunicipalContext context)
        {
            _context = context;
        }

        public async Task<PadronBoleta> AltaPadronBoleta(PadronBoleta padronBoleta)
        {
            _context.PadronBoletas.Add(padronBoleta);
            await _context.SaveChangesAsync();

            return padronBoleta
            ;
        }

        public async Task<bool> BajaPadronBoleta(int id)
        {
            var padronBoleta = await _context.PadronBoletas.FindAsync(id);
            if (padronBoleta == null)
            {
                return false;
            }

            _context.PadronBoletas.Remove(padronBoleta);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<PadronBoleta> ModificacionPadronBoleta(int id, PadronBoleta padronBoleta)
        {
            var encontrado = await _context.PadronBoletas.FindAsync(id);
            if (encontrado == null)
            {
                return null;
            }

            encontrado.NumeroPadron = padronBoleta.NumeroPadron;
            encontrado.Periodo = padronBoleta.Periodo;
            encontrado.Importe = padronBoleta.Importe;
            encontrado.Vencimiento = padronBoleta.Vencimiento;
            encontrado.Pagado = padronBoleta.Pagado;
            encontrado.Vencimiento2 = padronBoleta.Vencimiento2;
            encontrado.Importe2 = padronBoleta.Importe2;

            await _context.SaveChangesAsync();

            return encontrado;
        }

        public async Task<PadronBoletaDTO> PagoPadronBoleta(int id)
        {
            var padronBoleta = await _context.PadronBoletas.FindAsync(id);
            if (padronBoleta == null)
            {
                return null;
            }

            padronBoleta.Pagado = true;
            await _context.SaveChangesAsync();

            return new PadronBoletaDTO
            {
                IdBoleta = padronBoleta.IdBoleta,
                NumeroPadron = padronBoleta.NumeroPadron,
                Periodo = padronBoleta.Periodo,
                Importe = padronBoleta.Importe,
                Vencimiento = padronBoleta.Vencimiento,
                Pagado = padronBoleta.Pagado,
                Vencimiento2 = padronBoleta.Vencimiento2,
                Importe2 = padronBoleta.Importe2
            };
        }

        public async Task<IEnumerable<PadronBoletaDTO>> RecuperarPadronBoletaPorId(int id)
        {
            return await _context.PadronBoletas
                .Select( pc => new PadronBoletaDTO
                {
                    IdBoleta = pc.IdBoleta,
                    NumeroPadron = pc.NumeroPadron,
                    Periodo = pc.Periodo,
                    Vencimiento= pc.Vencimiento,
                    Importe = pc.Importe,
                    Pagado = pc.Pagado,
                    Vencimiento2= pc.Vencimiento2,
                    Importe2 = pc.Importe2
                }
                )
                .Where( pc => pc.IdBoleta == id)
                .ToListAsync();
        }

        public async Task<IEnumerable<PadronBoletaGetDTO>> RecuperarPadronBoletaPorNumeroPadron(int numeroPadron)
        {
            return await _context.PadronBoletas
                .Where(pb => pb.NumeroPadron == numeroPadron)
                .OrderBy(pb => pb.Periodo)
                .Select(pb => new PadronBoletaGetDTO
                {
                    IdBoleta = pb.IdBoleta,
                    NumeroPadron = pb.NumeroPadron,
                    Periodo = pb.Periodo,
                    Importe = pb.Importe,
                    Vencimiento = pb.Vencimiento,
                    Pagado = pb.Pagado,
                    Vencimiento2= pb.Vencimiento2,
                    Importe2 = pb.Importe2,
                    Contribuyente = new ContribuyenteDTO
                    {
                        IdContribuyente = pb.PadronContribuyente.IdContribuyente,
                        NumeroDocumentoContribuyente = pb.PadronContribuyente.Contribuyente.NumeroDocumentoContribuyente,
                        ApellidoNombreContribuyente = pb.PadronContribuyente.Contribuyente.ApellidoNombreContribuyente,
                        DomicilioCalleContribuyente = pb.PadronContribuyente.Contribuyente.DomicilioCalleContribuyente,
                        DomicilioNumeroContribuyente = pb.PadronContribuyente.Contribuyente.DomicilioNumeroContribuyente,
                        TelefonoContribuyente = pb.PadronContribuyente.Contribuyente.TelefonoContribuyente,
                        SexoContribuyente = pb.PadronContribuyente.Contribuyente.SexoContribuyente,
                        FechaNacimientoContribuyente = pb.PadronContribuyente.Contribuyente.FechaNacimientoContribuyente
                    }
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<PadronBoletaGetDTO>> RecuperarPadronBoletaPorTributoPeriodo(int idTributo, int periodo)
        {
            return await _context.PadronBoletas
                .Where(pb => pb.PadronContribuyente.IdTributoMunicipal == idTributo && pb.Periodo == periodo)
                .OrderBy(pb => pb.NumeroPadron)
                .Select(pb => new PadronBoletaGetDTO
                {
                    IdBoleta = pb.IdBoleta,
                    NumeroPadron = pb.NumeroPadron,
                    Periodo = pb.Periodo,
                    Importe = pb.Importe,
                    Vencimiento = pb.Vencimiento,
                    Pagado = pb.Pagado,
                    Vencimiento2 = pb.Vencimiento2,
                    Importe2 = pb.Importe2,
                    Contribuyente = new ContribuyenteDTO
                    {
                        IdContribuyente = pb.PadronContribuyente.IdContribuyente,
                        NumeroDocumentoContribuyente = pb.PadronContribuyente.Contribuyente.NumeroDocumentoContribuyente,
                        ApellidoNombreContribuyente = pb.PadronContribuyente.Contribuyente.ApellidoNombreContribuyente,
                        DomicilioCalleContribuyente = pb.PadronContribuyente.Contribuyente.DomicilioCalleContribuyente,
                        DomicilioNumeroContribuyente = pb.PadronContribuyente.Contribuyente.DomicilioNumeroContribuyente,
                        TelefonoContribuyente = pb.PadronContribuyente.Contribuyente.TelefonoContribuyente,
                        SexoContribuyente = pb.PadronContribuyente.Contribuyente.SexoContribuyente,
                        FechaNacimientoContribuyente = pb.PadronContribuyente.Contribuyente.FechaNacimientoContribuyente
                    }
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<PadronBoletaGetDTO>> RecuperarPadronBoletaPorNumeroPadronPeriodo(int numeroPadron, int periodo)
        {
            return await _context.PadronBoletas
                .Where(pb => pb.NumeroPadron == numeroPadron && pb.Periodo == periodo)
                .OrderBy(pb => pb.Periodo)
                .Select(pb => new PadronBoletaGetDTO
                {
                    IdBoleta = pb.IdBoleta,
                    NumeroPadron = pb.NumeroPadron,
                    Periodo = pb.Periodo,
                    Importe = pb.Importe,
                    Vencimiento = pb.Vencimiento,
                    Pagado = pb.Pagado,
                    Vencimiento2 = pb.Vencimiento2,
                    Importe2 = pb.Importe2,
                    Contribuyente = new ContribuyenteDTO
                    {
                        IdContribuyente = pb.PadronContribuyente.IdContribuyente,
                        NumeroDocumentoContribuyente = pb.PadronContribuyente.Contribuyente.NumeroDocumentoContribuyente,
                        ApellidoNombreContribuyente = pb.PadronContribuyente.Contribuyente.ApellidoNombreContribuyente,
                        DomicilioCalleContribuyente = pb.PadronContribuyente.Contribuyente.DomicilioCalleContribuyente,
                        DomicilioNumeroContribuyente = pb.PadronContribuyente.Contribuyente.DomicilioNumeroContribuyente,
                        TelefonoContribuyente = pb.PadronContribuyente.Contribuyente.TelefonoContribuyente,
                        SexoContribuyente = pb.PadronContribuyente.Contribuyente.SexoContribuyente,
                        FechaNacimientoContribuyente = pb.PadronContribuyente.Contribuyente.FechaNacimientoContribuyente
                    }
                })
                .ToListAsync();
        }

        public async Task<Result<IEnumerable<PadronBoletaDTO>>> GenerarPadronBoleta(int numeroPadron, int periodoInicial, int cantidad)
        {

            int per = periodoInicial;

            int anio = int.Parse(periodoInicial.ToString().Substring(0, 4));

            int mes = int.Parse(periodoInicial.ToString().Substring(4, 2));

            if (per < 202401 || per > 202601)
            {
                return Result<IEnumerable<PadronBoletaDTO>>.Failure("Error: periodo Inicial debe ser mayor 202401 y menor a 202601. OperacioCancelada");
            }

            if (anio < 2024 || anio > 2026)
            {
                return Result<IEnumerable<PadronBoletaDTO>>.Failure("Error: periodo Inicial debe ser mayor 202401 y menor a 202601. OperacioCancelada");
            }

            if (mes + cantidad - 1 > 12)
            {
                return Result<IEnumerable<PadronBoletaDTO>>.Failure("Error: periodo + cantidad supera mes 12. OperacioCancelada");
            }


            for (int i = 0; i <= cantidad-1; i++)
            {
                var encontrado = await _context.PadronBoletas.FirstOrDefaultAsync(x=> x.NumeroPadron==numeroPadron && x.Periodo==periodoInicial);
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
                    PadronBoleta nuevoPadron = new PadronBoleta
                    {
                        NumeroPadron = numeroPadron,
                        Periodo = periodoInicial,
                        Importe = importe,
                        Importe2 = importe2,
                        Vencimiento = vencimiento,
                        Vencimiento2 = vencimiento2,
                        Pagado = false
                    };

                    // Registro de nuevo padron
                    await _context.PadronBoletas.AddAsync(nuevoPadron);
                }

                // Proximo periodo
                periodoInicial = periodoInicial + 1;

            }

            await _context.SaveChangesAsync();

            var resultado = await _context.PadronBoletas
                .Where(x => x.NumeroPadron == numeroPadron)
                .OrderBy(x => x.Periodo)
                .Select(x => new PadronBoletaDTO
                {
                    IdBoleta = x.IdBoleta,
                    NumeroPadron = x.NumeroPadron,
                    Periodo = x.Periodo,
                    Importe = x.Importe,
                    Vencimiento = x.Vencimiento,
                    Pagado = x.Pagado,
                    Vencimiento2 = x.Vencimiento2,
                    Importe2 = x.Importe2
                }
                ).ToListAsync();

            return Result<IEnumerable<PadronBoletaDTO>>.Success(resultado);
        }
    }

}
