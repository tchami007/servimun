using Microsoft.AspNetCore.Mvc;
using ServiMun.Models;
using ServiMun.Services;

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
        public async Task<ActionResult<TributoMunicipal>> AltaNuevoTributoMunicipal(TributoMunicipalDTO tributoDTO)
        {
            var tributoMunicipal = new TributoMunicipal { 
            
                NombreTributo = tributoDTO.NombreTributo,
                Sintetico = tributoDTO.Sintetico,
                Estado = tributoDTO.Estado
            };

            var nuevoTributo = await _service.AltaNuevoTributoMunicipal(tributoMunicipal);

            return CreatedAtAction(nameof(RecuperacionTributoMunicipal), new { id = nuevoTributo.IdTributo }, nuevoTributo);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> BajaTributoMunicipal(int id)
        {
            var result = await _service.BajaTributoMunicipal(id);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ModificacionTributoMunicipal(int id, TributoMunicipalDTO tributoDTO)
        {
            if (id != tributoDTO.IdTributo) return BadRequest("El parametro id no coincide con el id del cuerpo de la solicitud");

            var tributoMunicipal = new TributoMunicipal
            {
                IdTributo = id,
                NombreTributo = tributoDTO.NombreTributo,
                Sintetico = tributoDTO.Sintetico,
                Estado = tributoDTO.Estado
            };


            await _service.ModificacionTributoMunicipal(tributoMunicipal);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TributoMunicipal>> RecuperacionTributoMunicipal(int id)
        {
            var tributo = await _service.RecuperacionTributoMunicipal(id);
            if (tributo == null) return NotFound();
            return tributo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TributoMunicipal>>> RecuperacionTodosTributoMunicipal()
        {
            var tributos = await _service.RecuperacionTodosTributoMunicipal();
            return Ok(tributos);
        }
    }

}
