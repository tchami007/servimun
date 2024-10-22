using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiMun.Models;
using ServiMun.Services;
using ServiMun.Shared;

namespace ServiMun.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicioController : ControllerBase
    {
        private readonly IServicioService _servicioService;

        public ServicioController(IServicioService servicioService)
        {
            _servicioService = servicioService;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Servicio>> getServicioById(int id)
        {
            var resultado = await _servicioService.GetServicio(id);
            if (resultado == null) { return NotFound(id); }
            return Ok(resultado);
        }
        [HttpGet] 
        public async Task<ActionResult<IEnumerable<Servicio>>> getAllServicio()
        {
            var resultados = await _servicioService.GetAllServicio();
            return Ok(resultados);
        }
        [HttpPost]
        public async Task<ActionResult<Servicio>> AddServicio(ServicioDTO servicioDTO)
        {
            var resultado = await _servicioService.AddServicio(servicioDTO);
            if (resultado._succes) 
            {
                var nuevo = resultado._value;
                return CreatedAtAction(nameof(getServicioById), new { id = nuevo.IdServicio }, nuevo);
            }
            else
            {
                return BadRequest(resultado._errorMessage);
            }
        }
        [HttpPut]
        public async Task<ActionResult> UpdateServicio(int id, ServicioDTO servicioDTO)
        {
            var resultado = await _servicioService.UpdateServicio( id, servicioDTO);
            if (resultado._succes)
            {
                return NoContent();
            }
            else
            {
                return BadRequest(resultado._errorMessage);
            }
        }
        [HttpDelete]
        public async Task<ActionResult> DeleteServicio(int id)
        {
            var resultado = await _servicioService.DeleteServicio(id);
            if (resultado._succes)
            {
                return NoContent();
            }
            else { return BadRequest(resultado._errorMessage);}
        }
    }
}
