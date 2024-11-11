using Microsoft.EntityFrameworkCore;
using ServiMun.Data;
using ServiMun.Models;
using ServiMun.Shared;
using System.Collections.Generic;

namespace ServiMun.Repository
{
    public interface IPadronBoletaRepository
    {
        public Task<Result<PadronBoleta>> AddItem(PadronBoleta item);
        public Task<Result<PadronBoleta>> DeleteItem(int id);
        public Task<Result<PadronBoleta>> UpdateItem(PadronBoleta item);
        public Task<Result<PadronBoleta>> PagoItem(int id);
        public Task<Result<PadronBoleta>> GetById(int id);
        public Task<IEnumerable<PadronBoleta>> GetAllByNumeroPadron(int numeroPadron);
        public Task<PadronBoleta> GetByNumeroPadronPeriodo(int numeroPadron, int numeroPeriodo);
        Task<Result<IEnumerable<PadronBoleta>>> GenerarPadronBoleta(int numeroPadron, int periodoInicial, int cantidad);
    }


    public class PadronBoletaRepository : IPadronBoletaRepository
    {

        private readonly TributoMunicipalContext _context;
        private Random rand = new Random();

        public PadronBoletaRepository(TributoMunicipalContext context)
        {
            _context = context;
        }
        public async Task<Result<PadronBoleta>> AddItem(PadronBoleta item)
        {
            await _context.PadronBoletas.AddAsync(item);
            await _context.SaveChangesAsync();
            return Result<PadronBoleta>.Success(item);
        }

        public async Task<Result<PadronBoleta>> DeleteItem(int id)
        {
            var encontrado = await _context.PadronBoletas.FirstOrDefaultAsync(x => x.IdBoleta == id);
            if (encontrado == null)
            {
                return Result<PadronBoleta>.Failure("Padron Boleta no identificado");
            }
            _context.PadronBoletas.Remove(encontrado);
            await _context.SaveChangesAsync();
            return Result<PadronBoleta>.Success(encontrado);
        }

        public async Task<Result<PadronBoleta>> UpdateItem(PadronBoleta item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Result<PadronBoleta>.Success(item);
        }

        public async Task<Result<PadronBoleta>> PagoItem(int id)
        {
            var padronBoleta = await _context.PadronBoletas.FirstOrDefaultAsync(x => x.IdBoleta == id);
            if (padronBoleta == null)
            {
                return Result<PadronBoleta>.Failure("Padron Boleta no identificado");
            }

            //Actualizo la boleta
            padronBoleta.Pagado = true;
            _context.Entry(padronBoleta).State = EntityState.Modified;

            // Actualizo los cambios
            await _context.SaveChangesAsync();

            return Result<PadronBoleta>.Success(padronBoleta);
        }


        public async Task<IEnumerable<PadronBoleta>> GetAllByNumeroPadron(int numeroPadron)
        {
            var resultado = await _context.PadronBoletas
                .Where(x => x.NumeroPadron == numeroPadron)
                .ToListAsync();
            return resultado;
        }

        public async Task<Result<PadronBoleta>> GetById(int id)
        {
            var resultado = await _context.PadronBoletas.FirstOrDefaultAsync(x => x.IdBoleta == id);
            return Result<PadronBoleta>.Success(resultado);
        }

        public async Task<PadronBoleta> GetByNumeroPadronPeriodo(int numeroPadron, int numeroPeriodo)
        {
            var resultado = await _context.PadronBoletas
                .FirstOrDefaultAsync(x => x.NumeroPadron == numeroPadron && x.Periodo == numeroPeriodo);

            return resultado;
        }

        public async Task<Result<IEnumerable<PadronBoleta>>> GenerarPadronBoleta(int numeroPadron, int periodoInicial, int cantidad)
        {
            int per = periodoInicial;

            int anio = int.Parse(periodoInicial.ToString().Substring(0, 4));

            int mes = int.Parse(periodoInicial.ToString().Substring(4, 2));

            if (per < 202401 || per > 202601)
            {
                return Result<IEnumerable<PadronBoleta>>.Failure("Error: periodo Inicial debe ser mayor 202401 y menor a 202601. Operacion Cancelada");
            }

            if (anio < 2024 || anio > 2026)
            {
                return Result<IEnumerable<PadronBoleta>>.Failure("Error: periodo Inicial debe ser mayor 202401 y menor a 202601. OperacioCancelada");
            }

            if (mes + cantidad - 1 > 12)
            {
                return Result<IEnumerable<PadronBoleta>>.Failure("Error: periodo + cantidad supera mes 12. OperacioCancelada");
            }

            for (int i = 0; i <= cantidad - 1; i++)
            {
                var encontrado = await _context.PadronBoletas.FirstOrDefaultAsync(x => x.NumeroPadron == numeroPadron && x.Periodo == periodoInicial);
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
                    PadronBoleta nuevoServicio = new PadronBoleta
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
                    await _context.PadronBoletas.AddAsync(nuevoServicio);
                }

                // Proximo periodo
                periodoInicial = periodoInicial + 1;
            }

            await _context.SaveChangesAsync();

            var resultado = await _context.PadronBoletas
                .Where(x => x.NumeroPadron == numeroPadron)
                .OrderBy(x => x.Periodo)
                .ToListAsync();

            return Result<IEnumerable<PadronBoleta>>.Success(resultado);
        }
    }
}
