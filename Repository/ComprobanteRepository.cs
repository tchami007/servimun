using Microsoft.EntityFrameworkCore;
using ServiMun.Data;
using ServiMun.Models;
using ServiMun.Shared;

using System.ComponentModel.DataAnnotations;




namespace ServiMun.Repository
{
    public class ComprobanteRepository : IRepositoryResult<ComprobanteControl>
    {

        private readonly TributoMunicipalContext _context;

        public ComprobanteRepository(TributoMunicipalContext context)
        {
            _context = context;                
        }

        public Task<Result<ComprobanteControl>> AddItem(ComprobanteControl item)
        {
            throw new NotImplementedException();
        }

        public Task<Result<ComprobanteControl>> DeleteItem(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ComprobanteControl>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<Result<ComprobanteControl>> GetById(int id)
        {
            var resultado = await _context.ComprobantesControl.FirstOrDefaultAsync(x=>x.IdControl==id);
            return Result<ComprobanteControl>.Success(resultado);
        }

        public async Task<Result<ComprobanteControl>> UpdateItem(ComprobanteControl item)
        {
            var resultado = await _context.ComprobantesControl.FirstOrDefaultAsync(x => x.IdControl == item.IdControl);

            // No encuentra el comprobante solicitado
            if (resultado == null)
            {
                return Result<ComprobanteControl>.Failure("Comprobante no identificado");
            }
            // Proximo numero
            var ultimo = resultado.UltimoNumeroComprobante + 1;

            // Actualizacion
            resultado.UltimoNumeroComprobante = ultimo;
            _context.Entry(resultado).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return Result<ComprobanteControl>.Success(resultado);
        }
    }
}
