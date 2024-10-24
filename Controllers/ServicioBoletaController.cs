using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiMun.Models;
using ServiMun.Services;
using ServiMun.Shared;

namespace ServiMun.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicioBoletaController : ControllerBase
    {
        private readonly IServicioBoletaService _servicioBoletaService;

        public ServicioBoletaController(IServicioBoletaService servicioBoletaService)
        {
            _servicioBoletaService = servicioBoletaService;
        }
        
        [HttpGet("GetServicioBoletaById")]
        public async Task<ActionResult<ServicioBoleta>> GetServicioBoletaById(int id)
        {
            var resultado = await _servicioBoletaService.GetServicioBoletaPorId(id);
            var res = resultado._value;
            if (res == null) { return NotFound(resultado._errorMessage); }
            return Ok(res);
        }

        [HttpGet("GetServicioBoletaByNumeroServicio")]
        public async Task<IEnumerable<ServicioBoleta>> GetAllServicioBoletaByNumeroServicio(int numeroServicio)
        {
            var resultado = await _servicioBoletaService.GetServicioBoletaPorNumeroServicio(numeroServicio);
            return resultado;
        }
        [HttpGet("GetServicioBoletaByNumeroServicioPeriodo")]
        public async Task<IEnumerable<ServicioBoleta>> GetAllServicioBoletaByNumeroServicioPeriodo(int numeroServicio, int numeroPeriodo)
        {
            var resultado = await _servicioBoletaService.GetServicioBoletaPorNumeroServicioPeriodo(numeroServicio,numeroPeriodo);
            return resultado;
        }
        [HttpPost]
        public async Task<ActionResult<ServicioBoleta>> AddServicioBoleta(ServicioBoletaDTO servicioBoletaDTO)
        {
            var resultado = await _servicioBoletaService.AddServicioBoleta(servicioBoletaDTO);
            if(!resultado._succes) 
            { 
                return BadRequest(resultado._errorMessage);
            }
            return CreatedAtAction(nameof(GetServicioBoletaById),new {id=resultado._value.IdBoletaServicio}, resultado._value);
        }
        [HttpPut]
        public async Task<ActionResult> UpdateServicioBoleta(int idServicioBoleta, ServicioBoletaDTO servicioBoletaDTO)
        {
            var resultado = await _servicioBoletaService.UpdateServicioBoleta(idServicioBoleta , servicioBoletaDTO);
            if (!resultado._succes)
            { return BadRequest(resultado._errorMessage);}
            else { return NoContent(); }
        }
        [HttpDelete]
        public async Task<ActionResult> DeleteServicioBoleta(int idServicioBoleta)
        {
            var resultado = await _servicioBoletaService.DeleteServicioBoleta(idServicioBoleta);
            if (!resultado._succes)
            {
                return BadRequest(resultado._errorMessage);
            }
            return NoContent();
        }
        [HttpPost("PagoServicioBoleta")]
        public async Task<ActionResult> PagoServicioBoleta(int IdServicioBoleta)
        {
            var resultado = await _servicioBoletaService.PagoServicioBoleta(IdServicioBoleta);
            if (!resultado._succes)
            {
                return BadRequest(resultado._errorMessage);
            }
            return Ok(resultado._value);
        }
        [HttpGet("GenerarServicioBoleta/{numeroServicio}/{periodo}/{cantidad}")]
        public async Task<ActionResult<IEnumerable<PadronBoleta>>> GenerarBoleta(int numeroServicio, int periodo, int cantidad)
        {

            var resultado = await _servicioBoletaService.GenerarServicioBoleta(numeroServicio, periodo, cantidad);
            return Ok(resultado);

        }
    }
}
