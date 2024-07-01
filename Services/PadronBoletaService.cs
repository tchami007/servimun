using Microsoft.EntityFrameworkCore;
using ServiMun.Data;
using ServiMun.Models;

namespace ServiMun.Services
{
    public class PadronBoletaService : IPadronBoletaService
    {
        private readonly TributoMunicipalContext _context;

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
                Pagado = padronBoleta.Pagado
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
                    Pagado = pc.Pagado
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

    }

}
