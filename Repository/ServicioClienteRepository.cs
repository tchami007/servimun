using Microsoft.EntityFrameworkCore;
using ServiMun.Data;
using ServiMun.Models;
using ServiMun.Shared;
using System.Xml;

namespace ServiMun.Repository
{

    public interface IServicioClienteRepository
    {
        Task<Result<ServicioCliente>> addServicioCliente(ServicioClienteDTO servicioCliente);
        Task<Result<ServicioCliente>> updateServicioCliente(int idServicio, int idContribuyente, int numeroServicio, ServicioClienteDTO servicioCliente);
        Task<Result<ServicioCliente>> deleteServicioCliente(int idServicio, int idContribuyente, int numeroServicio);
        Task<Result<ServicioCliente>> getServicioClienteById(int idServicio, int idContribuyente, int numeroServicio);
        Task<IEnumerable<ServicioCliente>> getServicioClienteAllByIdServicio(int idServicio);
        Task<Result<ServicioCliente>> getServicioClienteByNumeroServicio (int  numeroServicio);

    }

    public class ServicioClienteRepository : IServicioClienteRepository
    {
        private readonly TributoMunicipalContext _context;

        public ServicioClienteRepository(TributoMunicipalContext context)
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
                NumeroTelefono = servicioCliente.NumeroTelefono,
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

        public async Task<IEnumerable<ServicioCliente>> getServicioClienteAllByIdServicio(int idServicio)
        {
            var resultados = await _context.ServicioClientes
                .Where(x => x.IdServicio == idServicio)
                .OrderBy(x => x.NumeroServicio)
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

        public async Task<Result<ServicioCliente>> getServicioClienteByNumeroServicio(int numeroServicio)
        {
            var resultado = await _context.ServicioClientes.FirstOrDefaultAsync(x => x.NumeroServicio == numeroServicio);
            return Result<ServicioCliente>.Success(resultado);
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
    }
}
