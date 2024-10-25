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
        
        [HttpGet("porIdServicioBoleta/{idServicioBoleta}")]
        public async Task<ActionResult<ServicioBoleta>> GetServicioBoletaById(int idServicioBoleta)
        {
            var resultado = await _servicioBoletaService.GetServicioBoletaPorId(idServicioBoleta);
            var res = resultado._value;
            if (res == null) { return NotFound(resultado._errorMessage); }
            return Ok(res);
        }

        [HttpGet("porNumeroServicio/{numeroServicio}")]
        public async Task<IEnumerable<ServicioBoleta>> GetAllServicioBoletaByNumeroServicio(int numeroServicio)
        {
            var resultado = await _servicioBoletaService.GetServicioBoletaPorNumeroServicio(numeroServicio);
            return resultado;
        }
        [HttpGet("porNumeroServicioNumeroPeriodo/{numeroServicio}/{numeroPeriodo}")]
        public async Task<IEnumerable<ServicioBoleta>> GetAllServicioBoletaByNumeroServicioPeriodo(int numeroServicio, int numeroPeriodo)
        {
            var resultado = await _servicioBoletaService.GetServicioBoletaPorNumeroServicioPeriodo(numeroServicio,numeroPeriodo);
            return resultado;
        }
        [HttpPost]
        public async Task<ActionResult<ServicioBoleta>> AddServicioBoleta([FromBody] ServicioBoletaDTO servicioBoletaDTO)
        {
            var resultado = await _servicioBoletaService.AddServicioBoleta(servicioBoletaDTO);
            if(!resultado._succes) 
            { 
                return BadRequest(resultado._errorMessage);
            }
            return CreatedAtAction(nameof(GetServicioBoletaById),new {idServicioBoleta=resultado._value.IdBoletaServicio}, resultado._value);
        }
        [HttpPut("{idServicioBoleta}")]
        public async Task<ActionResult> UpdateServicioBoleta(int idServicioBoleta, [FromBody] ServicioBoletaDTO servicioBoletaDTO)
        {
            var resultado = await _servicioBoletaService.UpdateServicioBoleta(idServicioBoleta , servicioBoletaDTO);
            if (!resultado._succes)
            { return BadRequest(resultado._errorMessage);}
            else { return NoContent(); }
        }
        [HttpDelete("{idServicioBoleta}")]
        public async Task<ActionResult> DeleteServicioBoleta(int idServicioBoleta)
        {
            var resultado = await _servicioBoletaService.DeleteServicioBoleta(idServicioBoleta);
            if (!resultado._succes)
            {
                return BadRequest(resultado._errorMessage);
            }
            return NoContent();
        }
        [HttpPost("PagoServicioBoleta/{idServicioBoleta}")]
        public async Task<ActionResult> PagoServicioBoleta(int idServicioBoleta)
        {
            var resultado = await _servicioBoletaService.PagoServicioBoleta(idServicioBoleta);
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
