using Microsoft.AspNetCore.Mvc;
using ServiMun.Models;
using ServiMun.Services;
using ServiMun.Shared;

namespace ServiMun.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PadronContribuyenteController : ControllerBase
    {
        private readonly IPadronContribuyenteService _service;

        public PadronContribuyenteController(IPadronContribuyenteService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<Result<PadronContribuyenteDTO>>> AltaContribuyentePadron([FromBody] PadronContribuyenteDTO padronContribuyenteDTO)
        {
            var nuevoPadron = new PadronContribuyente
            {
                IdContribuyente = padronContribuyenteDTO.IdContribuyente,
                IdTributoMunicipal = padronContribuyenteDTO.IdTributoMunicipal,
                NumeroPadron = padronContribuyenteDTO.NumeroPadron,
                Estado = padronContribuyenteDTO.Estado
            };

            await _service.AddPadronContribuyente(nuevoPadron);
            return CreatedAtAction(nameof(RecuperaPadronContribuyenteId), new { idContribuyente = nuevoPadron.IdContribuyente, idTributoMunicipal = nuevoPadron.IdTributoMunicipal }, nuevoPadron);
        }

        [HttpDelete("{idContribuyente}/{idTributoMunicipal}")]
        public async Task<IActionResult> BajaContribuyentePadron(int idContribuyente, int idTributoMunicipal)
        {
            var result = await _service.DeletePadronContribuyente(idContribuyente, idTributoMunicipal);
            if (!result._succes) return NotFound();
            return NoContent();
        }

        [HttpPut("{idContribuyente}/{idTributoMunicipal}")]
        public async Task<IActionResult> ModificacionContribuyentePadron(int idContribuyente, int idTributoMunicipal, [FromBody] PadronContribuyenteGetDTO padronContribuyenteGetDTO)
        {
            if (idContribuyente != padronContribuyenteGetDTO.IdContribuyente || idTributoMunicipal != padronContribuyenteGetDTO.IdTributoMunicipal) return BadRequest();

            var padronContribuyente = await _service.GetPadronContribuyentePadronById(idContribuyente, idTributoMunicipal);

            if (!padronContribuyente._succes)
            {
                return NotFound();
            }

            var padron = padronContribuyente._value;

            if(padron == null)
            {
                return NotFound();
            }

            var result = await _service.UpdatePadronContribuyente(padronContribuyenteGetDTO);
            if (!result._succes)
            {
                return BadRequest("No se puede modificar el NumeroPadron porque está relacionado con PadronBoleta.");
            }

            return NoContent();
        }

        [HttpGet("{idContribuyente}/{idTributoMunicipal}")]
        public async Task<ActionResult<IEnumerable<PadronContribuyenteGetDTO>>> RecuperaPadronContribuyenteId(int idContribuyente, int idTributoMunicipal)
        {
            var padrones = await _service.GetPadronContribuyentePadronById(idContribuyente,idTributoMunicipal);

            return Ok(padrones);
        }

        [HttpGet("Tributo/{idTributo}")]
        public async Task<ActionResult<IEnumerable<PadronContribuyenteGetDTO>>> RecuperaPadronContribuyenteByIdTributo(int idTributo)
        {
            var padrones = await _service.GetAllPadronContribuyenteByIdTributo(idTributo);
            return Ok(padrones);
        }
    }
}
