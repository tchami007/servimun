using Microsoft.AspNetCore.Mvc;
using ServiMun.Models;
using ServiMun.Services;
using ServiMun.Shared;

namespace ServiMun.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TributoMunicipalController : ControllerBase
    {
        private readonly ITributoMunicipalService _service;

        public TributoMunicipalController(ITributoMunicipalService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<Result<TributoMunicipal>>> AltaNuevoTributoMunicipal([FromBody]TributoMunicipalDTO tributoDTO)
        {
            var tributoMunicipal = new TributoMunicipal { 
            
                NombreTributo = tributoDTO.NombreTributo,
                Sintetico = tributoDTO.Sintetico,
                Estado = tributoDTO.Estado
            };

            var resultado = await _service.AddTributoMunicipal(tributoMunicipal);

            return CreatedAtAction(nameof(RecuperacionTributoMunicipal), new { id = resultado._value.IdTributo }, resultado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> BajaTributoMunicipal(int id)
        {
            var result = await _service.DeleteTributoMunicipal(id);
            if (!result._succes) return NotFound();
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ModificacionTributoMunicipal(int id, [FromBody] TributoMunicipalDTO tributoDTO)
        {
            if (id != tributoDTO.IdTributo) return BadRequest("El parametro id no coincide con el id del cuerpo de la solicitud");

            var tributoMunicipal = new TributoMunicipal
            {
                IdTributo = id,
                NombreTributo = tributoDTO.NombreTributo,
                Sintetico = tributoDTO.Sintetico,
                Estado = tributoDTO.Estado
            };


            await _service.UpdateTributoMunicipal(tributoMunicipal);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Result<TributoMunicipal>>> RecuperacionTributoMunicipal(int id)
        {
            var tributo = await _service.GetTributoMunicipalById(id);
            if (!tributo._succes) return NotFound(tributo);
            return Ok(tributo);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TributoMunicipal>>> RecuperacionTodosTributoMunicipal()
        {
            var tributos = await _service.GetAllTributoMunicipal();
            return Ok(tributos);
        }
    }

}
