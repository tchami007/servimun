using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiMun.Data;
using ServiMun.Models;
using ServiMun.Shared;

namespace ServiMun.Services
{
    public class ServicioClienteService : IServicioClienteService
    {
        private readonly TributoMunicipalContext _context;
        public ServicioClienteService(TributoMunicipalContext context)
        {
            _context = context;
        }
        public async Task<Result<ServicioCliente>> addServicioCliente(ServicioClienteDTO servicioCliente)
        {
            // Busqueda del Contribuyente + Servicio + Nro
            var encontrado = await _context.ServicioClientes
                .FirstOrDefaultAsync(x => x.IdServicio == servicioCliente.IdServicio 
                && x.IdContribuyente == servicioCliente.IdContribuyente
                && x.NumeroServicio == servicioCliente.NumeroServicio);
            if (encontrado != null)
            {
                return Result<ServicioCliente>.Failure($"El servicio {servicioCliente.NumeroServicio} ya fue ingresado");
            }
            
            var servicioClienteNuevo = new ServicioCliente 
            { 
                IdContribuyente = servicioCliente.IdContribuyente,
                IdServicio = servicioCliente.IdServicio,
                NumeroServicio = servicioCliente.NumeroServicio,
                NumeroCliente = servicioCliente.NumeroCliente,
                NumeroTelefono = servicioCliente .NumeroTelefono,
                Estado = servicioCliente.Estado
            };

            await _context.ServicioClientes.AddAsync(servicioClienteNuevo);
            await _context.SaveChangesAsync();

            return Result<ServicioCliente>.Success(servicioClienteNuevo);

        }
        public async Task<Result<ServicioCliente>> deleteServicioCliente(int idServicio, int idContribuyente, int numeroServicio)
        {
            // Busqueda del Contribuyente + Servicio + Nro
            var encontrado = await _context.ServicioClientes
                .FirstOrDefaultAsync(x => x.IdServicio == idServicio
                && x.IdContribuyente == idContribuyente
                && x.NumeroServicio == numeroServicio);

            if (encontrado == null) 
            {
                return Result<ServicioCliente>.Failure($"El servicio {numeroServicio} no se encuentra");
            }
            _context.ServicioClientes.Remove(encontrado);
            await _context.SaveChangesAsync();
            return Result<ServicioCliente>.Success(encontrado);
        }
        public async Task<Result<ServicioCliente>> updateServicioCliente(int idServicio, int idContribuyente, int numeroServicio, ServicioClienteDTO servicioCliente)
        {
            // Validez
            if (idServicio != servicioCliente.IdServicio || idContribuyente != servicioCliente.IdContribuyente || numeroServicio != servicioCliente.NumeroServicio)
            {
                return Result<ServicioCliente>.Failure("Los parametros no se corresponden");
            }

            // Busqueda del Contribuyente + Servicio + Nro
            var encontrado = await _context.ServicioClientes
                .FirstOrDefaultAsync(x => x.IdServicio == servicioCliente.IdServicio
                && x.IdContribuyente == servicioCliente.IdContribuyente
                && x.NumeroServicio == servicioCliente.NumeroServicio);
            if (encontrado != null)
            {
                encontrado.NumeroCliente = servicioCliente.NumeroCliente;
                encontrado.NumeroTelefono = servicioCliente.NumeroTelefono;
                encontrado.Estado = servicioCliente.Estado;

                _context.Entry(encontrado).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Result<ServicioCliente>.Success(encontrado);
            }
            else
            {
                return Result<ServicioCliente>.Failure($"El servicio {numeroServicio} no existe");
            }
        }
        public async Task<IEnumerable<ServicioCliente>> getServicioClienteAll(int idservicio)
        {
            var resultados = await _context.ServicioClientes
                .Where(x=>x.IdServicio==idservicio)
                .OrderBy(x=>x.NumeroServicio)
                .ToListAsync();
            return resultados;
        }
        public async Task<Result<ServicioCliente>> getServicioClienteById(int idServicio, int idContribuyente, int numeroServicio)
        {
            var encontrado = await _context.ServicioClientes.FirstOrDefaultAsync(x => x.IdServicio == idServicio && 
            x.IdContribuyente == idContribuyente &&
            x.NumeroServicio == numeroServicio);
            if (encontrado == null)
            {
                return Result<ServicioCliente>.Failure($"El servicio {numeroServicio} no existe ");
            }
            else
            {
                return Result<ServicioCliente>.Success(encontrado);
            }
        }

    }
}
