using Microsoft.EntityFrameworkCore;
using ServiMun.Data;
using ServiMun.Models;

namespace ServiMun.Services
{
    public class PadronContribuyenteService : IPadronContribuyenteService
    {
        private readonly TributoMunicipalContext _context;

        public PadronContribuyenteService(TributoMunicipalContext context)
        {
            _context = context;
        }

        public async Task<PadronContribuyente> AltaContribuyentePadron(PadronContribuyente padronContribuyente)
        {
            _context.PadronContribuyentes.Add(padronContribuyente);
            await _context.SaveChangesAsync();
            return padronContribuyente;
        }

        public async Task<bool> BajaContribuyentePadron(int idContribuyente, int idTributoMunicipal)
        {
            var padron = await _context.PadronContribuyentes.FindAsync(idContribuyente, idTributoMunicipal);
            if (padron == null) return false;

            _context.PadronContribuyentes.Remove(padron);
            await _context.SaveChangesAsync();
            return true;
        }


        /// <summary>
        /// Modifica un elemento de la asociacion Padron-Contribuyente
        /// </summary>
        /// <param name="padronDTO">Elemento de tipo PadronContribuyenteGetDTO de transferencia de la asociacion</param>
        /// <returns>Valor booleano qie indica si se realizo la modificacion</returns>
        public async Task<bool> ModificacionContribuyentePadron(PadronContribuyenteGetDTO padronDTO)
        {
            // Verificar si el NumeroPadron existe en PadronBoleta
            var padronExists = await _context.PadronBoletas.AnyAsync(pb => pb.NumeroPadron == padronDTO.NumeroPadron);
            if (padronExists)
            {
                return false;
            }

            // Eliminar el registro existente
            var existingPadron = await _context.PadronContribuyentes.FindAsync(padronDTO.IdContribuyente, padronDTO.IdTributoMunicipal);
            if (existingPadron != null)
            {
                _context.PadronContribuyentes.Remove(existingPadron);
                await _context.SaveChangesAsync();
            }

            // Conversion de DTO a Model
            PadronContribuyente pad = new PadronContribuyente()
            {
                IdContribuyente = padronDTO.IdContribuyente,
                IdTributoMunicipal = padronDTO.IdTributoMunicipal,
                NumeroPadron = padronDTO.NumeroPadron,
                Estado = padronDTO.Estado
            };

            // Agregar el nuevo registro
            _context.PadronContribuyentes.Add(pad);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<PadronContribuyenteGetDTO>> RecuperaPadronContribuyente(string numeroDocumentoContribuyente)
        {
            return await _context.PadronContribuyentes
                .Include(p => p.Contribuyente)
                .Include(p => p.TributoMunicipal)
                .Select(p=> new PadronContribuyenteGetDTO {
                    IdContribuyente = p.IdContribuyente,
                    IdTributoMunicipal = p.IdTributoMunicipal,
                    NumeroDocumentoContribuyente = p.Contribuyente.NumeroDocumentoContribuyente,
                    ApellidoNombreContribuyente = p.Contribuyente.ApellidoNombreContribuyente,
                    NumeroPadron = p.NumeroPadron,
                    Estado = p.Estado,
                    TributoDescripcion = p.TributoMunicipal.NombreTributo
                })
                .Where(p => p.NumeroDocumentoContribuyente == numeroDocumentoContribuyente)
                .ToListAsync();
        }

        public async Task<IEnumerable<PadronContribuyenteGetDTO>> RecuperaContribuyentePadron(int numeroPadron)
        {
            return await _context.PadronContribuyentes
                .Include(p => p.Contribuyente)
                .Include(p => p.TributoMunicipal)
                .Select(p=>new PadronContribuyenteGetDTO {

                    IdContribuyente = p.IdContribuyente,
                    IdTributoMunicipal= p.IdTributoMunicipal,
                    NumeroDocumentoContribuyente = p.Contribuyente.NumeroDocumentoContribuyente,
                    ApellidoNombreContribuyente = p.Contribuyente.ApellidoNombreContribuyente,
                    NumeroPadron= p.NumeroPadron,
                    Estado = p.Estado,
                    TributoDescripcion = p.TributoMunicipal.NombreTributo
                })
                .Where(p => p.NumeroPadron == numeroPadron)
                .ToListAsync();
        }

        public async Task<IEnumerable<PadronContribuyenteGetDTO>> RecuperarContribuyentePadronId(int idContribuyente, int idTributoMunicipal)
        {
            return await _context.PadronContribuyentes
                .Include(pc => pc.Contribuyente)
                .Include(pc => pc.TributoMunicipal)
                .Select(pc => new PadronContribuyenteGetDTO 
                    { 
                        IdContribuyente=pc.IdContribuyente,
                        IdTributoMunicipal=pc.IdTributoMunicipal,
                        NumeroDocumentoContribuyente = pc.Contribuyente.NumeroDocumentoContribuyente,
                        ApellidoNombreContribuyente = pc.Contribuyente.ApellidoNombreContribuyente,
                        NumeroPadron = pc.NumeroPadron,
                        Estado=pc.Estado,
                        TributoDescripcion = pc.TributoMunicipal.NombreTributo
                })
                .Where(pc => pc.IdContribuyente == idContribuyente && pc.IdTributoMunicipal == idTributoMunicipal)
                .ToListAsync();
        }

        public async Task<IEnumerable<PadronContribuyenteGetDTO>> RecuperarPadronTributo(int idTributoMunicipal)
        {
            return await _context.PadronContribuyentes
             .Include(pc => pc.Contribuyente)
             .Include(pc => pc.TributoMunicipal)
             .Select(pc => new PadronContribuyenteGetDTO
             {
                 IdContribuyente = pc.IdContribuyente,
                 IdTributoMunicipal = pc.IdTributoMunicipal,
                 NumeroDocumentoContribuyente = pc.Contribuyente.NumeroDocumentoContribuyente,
                 ApellidoNombreContribuyente = pc.Contribuyente.ApellidoNombreContribuyente,
                 NumeroPadron = pc.NumeroPadron,
                 Estado = pc.Estado,
                 TributoDescripcion = pc.TributoMunicipal.NombreTributo
             })
             .Where(pc => pc.IdTributoMunicipal == idTributoMunicipal)
             .ToListAsync();
        }
    }

}
