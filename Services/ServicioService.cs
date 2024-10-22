using Microsoft.EntityFrameworkCore;
using ServiMun.Data;
using ServiMun.Models;
using ServiMun.Shared;

namespace ServiMun.Services
{
    public class ServicioService : IServicioService
    {
        private readonly TributoMunicipalContext _context;

        public ServicioService (TributoMunicipalContext context)
        {
            _context = context;
        }

        public async Task<Result<Servicio>> AddServicio(ServicioDTO servicioDTO)
        {
            var encontrado = await _context.Servicios.FirstOrDefaultAsync(x => x.IdServicio == servicioDTO.IdServicio);

            if (encontrado != null) 
            {
                return Result<Servicio>.Failure("El servicio ya existe");
            }

            Servicio servicio = new Servicio 
            { 
                IdServicio = servicioDTO.IdServicio,
                NombreServicio  = servicioDTO.NombreServicio,
                Sintetico = servicioDTO.Sintetico,
                Estado = servicioDTO.Estado
            };

            await _context.Servicios.AddAsync(servicio);
            await _context.SaveChangesAsync();

            return Result<Servicio>.Success(servicio);
        }

        public async Task<Result<Servicio>> DeleteServicio(int id)
        {
            var encontrado = _context.Servicios.FirstOrDefault(x=> x.IdServicio == id);
            if (encontrado != null)
            {
                _context.Servicios.Remove(encontrado);
                await _context.SaveChangesAsync();
                return Result<Servicio>.Success(encontrado);
            }
            else
            {
                return Result<Servicio>.Failure("El servicio no se pudo borrar");
            }
        }

        public async Task<Result<Servicio>> UpdateServicio(int id, ServicioDTO servicioDTO)
        {
            var encontrado = await _context.Servicios.FirstOrDefaultAsync(x=>x.IdServicio == id);
            if (encontrado == null)
            {
                return Result<Servicio>.Failure("El servicio a modificar no fue encontrado");
            }
            
            if(encontrado.IdServicio != servicioDTO.IdServicio)
            {
                return Result<Servicio>.Failure("El id de servicio a modificar no corresponde al de la modificacion");
            }

            // Si hubiera mapeo no seria necesario tanto codigo!!!
            encontrado.NombreServicio = servicioDTO.NombreServicio;
            encontrado.Sintetico = servicioDTO.Sintetico;
            encontrado.Estado = servicioDTO.Estado;

            _context.Entry(encontrado).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return Result<Servicio>.Success(encontrado);

        }

        public async Task<Result<Servicio>> GetServicio(int id)
        {
            var encontrado = await _context.Servicios.FirstOrDefaultAsync(x=>x.IdServicio == id);
            if (encontrado == null)
            {
                return Result<Servicio>.Failure("No se encuentra el servicio buscado");
            }
            return Result<Servicio>.Success(encontrado);
        }

        public async Task<IEnumerable<Servicio>> GetAllServicio()
        {
            var resultados = await _context.Servicios.ToListAsync();
            return resultados;
            
        }

        public async Task<IEnumerable<Servicio>> GetAllBySintetico(string sintetico)
        {
            return await _context.Servicios.Where(x => x.Sintetico == sintetico).ToListAsync();
        }
    }
}
