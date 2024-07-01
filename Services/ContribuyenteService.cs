using Microsoft.EntityFrameworkCore;
using ServiMun.Data;
using ServiMun.Models;

namespace ServiMun.Services
{
    public class ContribuyenteService : IContribuyenteService
    {
        private readonly TributoMunicipalContext _context;

        public ContribuyenteService(TributoMunicipalContext context)
        {
            _context = context;
        }

        public async Task<Contribuyente> AltaNuevoContribuyente(Contribuyente contribuyente)
        {
            _context.Contribuyentes.Add(contribuyente);
            await _context.SaveChangesAsync();
            return contribuyente;
        }

        public async Task<bool> BajaContribuyente(int id)
        {
            var contribuyente = await _context.Contribuyentes.FindAsync(id);
            if (contribuyente == null) return false;

            _context.Contribuyentes.Remove(contribuyente);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Contribuyente> ModificacionContribuyente(Contribuyente contribuyente)
        {
            _context.Entry(contribuyente).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return contribuyente;
        }

        public async Task<Contribuyente> RecuperacionContribuyente(int id)
        {
            return await _context.Contribuyentes.FindAsync(id);
        }

        public async Task<IEnumerable<Contribuyente>> RecuperacionContribuyentePorNumero(string numeroDocumentoContribuyente)
        {
            return await _context.Contribuyentes
                .Where(p => p.NumeroDocumentoContribuyente == numeroDocumentoContribuyente)
                .ToListAsync();
        }

        public async Task<IEnumerable<Contribuyente>> RecuperacionTodosContribuyente()
        {
            return await _context.Contribuyentes
                .OrderBy(p=>p.ApellidoNombreContribuyente)
                .ToListAsync();
        }
    }
}
