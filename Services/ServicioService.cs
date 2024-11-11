using ServiMun.Models;
using ServiMun.Repository;
using ServiMun.Shared;

namespace ServiMun.Services
{

    public interface IServicioService
    {
        Task<Result<Servicio>> AddServicio(ServicioDTO servicioDTO);
        Task<Result<Servicio>> DeleteServicio(int id);
        Task<Result<Servicio>> UpdateServicio(int id, ServicioDTO servicioDTO);
        Task<Result<Servicio>> GetServicio(int id);
        Task<IEnumerable<Servicio>> GetAllServicio();
    }

    public class ServicioService : IServicioService
    {
        private readonly IRepositoryResult<Servicio> _servicioRepository;

        public ServicioService (IRepositoryResult<Servicio> servicioRepository)
        {
            _servicioRepository = servicioRepository;   
        }

        public async Task<Result<Servicio>> AddServicio(ServicioDTO servicioDTO)
        {
            var encontrado = await _servicioRepository.GetById(servicioDTO.IdServicio);

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

            var resultado = await _servicioRepository.AddItem(servicio);

            return resultado;
        }

        public async Task<Result<Servicio>> DeleteServicio(int id)
        {
            var resultado = await _servicioRepository.DeleteItem(id);
            return resultado;
        }

        public async Task<Result<Servicio>> UpdateServicio(int id, ServicioDTO servicioDTO)
        {
            var encontrado = await _servicioRepository.GetById(id);
            if (!encontrado._succes)
            {
                return encontrado;
            }
            
            if(encontrado._value.IdServicio != servicioDTO.IdServicio)
            {
                return Result<Servicio>.Failure("El id de servicio a modificar no corresponde al de la modificacion");
            }

            // Si hubiera mapeo no seria necesario tanto codigo!!!
            encontrado._value.NombreServicio = servicioDTO.NombreServicio;
            encontrado._value.Sintetico = servicioDTO.Sintetico;
            encontrado._value.Estado = servicioDTO.Estado;

            var resultado = await _servicioRepository.UpdateItem(encontrado._value);

            return resultado;

        }

        public async Task<Result<Servicio>> GetServicio(int id)
        {
            var encontrado = await _servicioRepository.GetById(id);
            return encontrado;
        }

        public async Task<IEnumerable<Servicio>> GetAllServicio()
        {
            var resultados = await _servicioRepository.GetAll();
            return resultados;
            
        }
    }
}
