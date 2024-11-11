using Microsoft.EntityFrameworkCore;
using ServiMun.Data;
using ServiMun.Models;
using ServiMun.Shared;

namespace ServiMun.Repository
{
    public interface IContribuyenteRepository 
    {
        public Task<Result<Contribuyente>> AddContribuyente(Contribuyente item);
        public Task<Result<Contribuyente>> DeleteContribuyente(int id);
        public Task<Result<Contribuyente>> UpdateContribuyente(Contribuyente item);
        public Task<Result<Contribuyente>> GetContribuyenteById(int id);
        public Task<IEnumerable<Contribuyente>> GetAllContribuyente();
        public Task<IEnumerable<Contribuyente>> GetAllContribuyenteByNumeroDocumento(string numeroDocumento);
    }
    public class ContribuyenteRepository : IContribuyenteRepository
    {
        private readonly TributoMunicipalContext _context;

        public ContribuyenteRepository(TributoMunicipalContext context)
        {
            _context = context; 
        }

        public async Task<Result<Contribuyente>> AddContribuyente(Contribuyente item)
        {
            await _context.Contribuyentes.AddAsync(item);
            await _context.SaveChangesAsync();
            return Result<Contribuyente>.Success(item);
        }

        public async Task<Result<Contribuyente>> DeleteContribuyente(int id)
        {
            var contribuyente = await _context.Contribuyentes.FindAsync(id);
            if (contribuyente == null) 
            {
                return Result<Contribuyente>.Failure("No se identifico el contribuyente");
            }
            _context.Contribuyentes.Remove(contribuyente);
            await _context.SaveChangesAsync();
            return Result<Contribuyente>.Success(contribuyente);
        }

        public async Task<IEnumerable<Contribuyente>> GetAllContribuyente()
        {
            return await _context.Contribuyentes
                .OrderBy(p => p.ApellidoNombreContribuyente)
                .ToListAsync();
        }

        public async Task<IEnumerable<Contribuyente>> GetAllContribuyenteByNumeroDocumento(string numeroDocumento)
        {
            return await _context.Contribuyentes
                .Where(p => p.NumeroDocumentoContribuyente == numeroDocumento)
                .ToListAsync();
        }

        public async Task<Result<Contribuyente>> GetContribuyenteById(int id)
        {
            var resultado = await _context.Contribuyentes.FirstOrDefaultAsync(x=>x.IdContribuyente == id);
            return Result<Contribuyente>.Success(resultado);
        }

        public async Task<Result<Contribuyente>> UpdateContribuyente(Contribuyente item)
        {
            var encontrado = await _context.Contribuyentes.FirstOrDefaultAsync(x => x.IdContribuyente == item.IdContribuyente);

            if (encontrado == null)
            {
                return Result<Contribuyente>.Failure("No se encuentra el contribuyente");
            }

            encontrado.NumeroDocumentoContribuyente = item.NumeroDocumentoContribuyente;
            encontrado.ApellidoNombreContribuyente = item.ApellidoNombreContribuyente;
            encontrado.FechaNacimientoContribuyente = item.FechaNacimientoContribuyente;
            encontrado.DomicilioCalleContribuyente = item.DomicilioCalleContribuyente;
            encontrado.DomicilioNumeroContribuyente = item.DomicilioNumeroContribuyente;
            encontrado.SexoContribuyente = item.SexoContribuyente;
            encontrado.TelefonoContribuyente = item.SexoContribuyente;

            _context.Entry(encontrado).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Result<Contribuyente>.Success(encontrado);
        }
    }
}
