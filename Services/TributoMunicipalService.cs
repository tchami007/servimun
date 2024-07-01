using Microsoft.EntityFrameworkCore;
using ServiMun.Data;
using ServiMun.Models;

namespace ServiMun.Services
{
    public class TributoMunicipalService : ITributoMunicipalService
    {
        private readonly TributoMunicipalContext _context;

        public TributoMunicipalService(TributoMunicipalContext context)
        {
            _context = context;
        }

        public async Task<TributoMunicipal> AltaNuevoTributoMunicipal(TributoMunicipal tributo)
        {
            _context.TributosMunicipales.Add(tributo);
            await _context.SaveChangesAsync();
            return tributo;
        }

        public async Task<bool> BajaTributoMunicipal(int id)
        {
            var tributo = await _context.TributosMunicipales.FindAsync(id);
            if (tributo == null) return false;

            _context.TributosMunicipales.Remove(tributo);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<TributoMunicipal> ModificacionTributoMunicipal(TributoMunicipal tributo)
        {
            _context.Entry(tributo).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return tributo;
        }

        public async Task<TributoMunicipal> RecuperacionTributoMunicipal(int id)
        {
            return await _context.TributosMunicipales.FindAsync(id);
        }

        public async Task<IEnumerable<TributoMunicipal>> RecuperacionTodosTributoMunicipal()
        {
            return await _context.TributosMunicipales.ToListAsync();
        }
    }

}
