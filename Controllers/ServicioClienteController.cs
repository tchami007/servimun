using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiMun.Models;
using ServiMun.Services;
using ServiMun.Shared;

namespace ServiMun.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicioClienteController : ControllerBase
    {
        private readonly IServicioClienteService _servicioClienteService;

        public ServicioClienteController(IServicioClienteService servicioClienteService)
        {
            _servicioClienteService = servicioClienteService;
        }

        [HttpGet("GetServicio")]
        public async Task<ActionResult<ServicioCliente>> GetServicioClienteByNumeroServicio(int idServicio, int idContribuyente, int numeroServicio)
        {
            var resultado = await _servicioClienteService.getServicioClienteById(idServicio, idContribuyente, numeroServicio);
            if (resultado._succes)
            {
                return Ok(resultado);
            }
            else
            {
                return BadRequest(resultado);
            }

        }

        [HttpGet ("GetAllServicioCliente")]
        public async Task<ActionResult<IEnumerable<ServicioCliente>>> GetAllServicioCliente(int idServicio)
        {
            var resultado = await _servicioClienteService.getServicioClienteAll(idServicio);
            return Ok(resultado);

        }

        [HttpPost]
        public async Task<ActionResult<ServicioCliente>> AddServicioCliente(ServicioClienteDTO servicioClienteDTO)
        {
            var resultado = await _servicioClienteService.addServicioCliente (servicioClienteDTO);
            if (resultado._succes)
            {
                var res = resultado._value;
                return CreatedAtAction(nameof(GetServicioClienteByNumeroServicio),new { IdServicio = res.IdServicio, IdContribuyente = res.IdContribuyente, NumeroServicio = res.NumeroServicio }, res);
            }
            else
            {
                return BadRequest(resultado._errorMessage);
            }
        }

        [HttpPut]
        public async Task<ActionResult> UpdateServicioCliente([FromBody]ServicioClienteDTO servicioClienteDTO)
        {
            int idContribuyente = servicioClienteDTO.IdContribuyente;
            int idServicio = servicioClienteDTO.IdServicio;
            int numeroServicio = servicioClienteDTO.NumeroServicio;

            var resultado = await _servicioClienteService.updateServicioCliente(idServicio, idContribuyente, numeroServicio, servicioClienteDTO);

            if (resultado._succes)
            {
                var res = resultado._value;
                return Ok(res);
            }
            else
            {
                return BadRequest(resultado._errorMessage);
            }
        }
        [HttpDelete]
        public async Task<ActionResult> DeleteServicioCliente(int idServicio, int idContribuyente, int numeroServicio)
        {
            var resultado = await _servicioClienteService.deleteServicioCliente(idServicio,idContribuyente,numeroServicio);
            if (resultado._succes)
            {
                return NoContent();
            }
            else
            {
                return BadRequest(resultado._errorMessage);
            }
        }
    }
}
