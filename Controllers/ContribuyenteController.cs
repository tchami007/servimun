using Microsoft.AspNetCore.Mvc;
using ServiMun.Models;
using ServiMun.Services;
using ServiMun.Shared;

namespace ServiMun.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContribuyenteController : ControllerBase
    {
        private readonly IContribuyenteService _service;
        public ContribuyenteController(IContribuyenteService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task<ActionResult<Result<Contribuyente>>> AltaNuevoContribuyente([FromBody] ContribuyenteDTO contribuyenteDTO)
        {
            if (contribuyenteDTO == null)
            {
                return BadRequest("contribuyenteDTO is null.");
            }

            var contribuyente = new Contribuyente
            {
                NumeroDocumentoContribuyente = contribuyenteDTO.NumeroDocumentoContribuyente,
                ApellidoNombreContribuyente = contribuyenteDTO.ApellidoNombreContribuyente,
                DomicilioCalleContribuyente = contribuyenteDTO.DomicilioCalleContribuyente,
                DomicilioNumeroContribuyente = contribuyenteDTO.DomicilioNumeroContribuyente,
                TelefonoContribuyente = contribuyenteDTO.TelefonoContribuyente,
                FechaNacimientoContribuyente = contribuyenteDTO.FechaNacimientoContribuyente,
                SexoContribuyente = contribuyenteDTO.SexoContribuyente
            };

            var nuevoContribuyente = await _service.AddContribuyente(contribuyente);

            return CreatedAtAction(nameof(RecuperacionContribuyente), new { id = nuevoContribuyente._value.IdContribuyente }, nuevoContribuyente);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> BajaContribuyente(int id)
        {
            var result = await _service.DeleteContribuyente(id);
            if (!result._succes) return NotFound();
            return NoContent();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> ModificacionContribuyente(int id, [FromBody] ContribuyenteDTO contribuyenteDTO)
        {

            if (id != contribuyenteDTO.IdContribuyente)
            {
                return BadRequest("ID mismatch");
            }

            if (id != contribuyenteDTO.IdContribuyente) return BadRequest("El id recibido no coincide con id del cuerpo de la solicitud");

            var contribuyente = new Contribuyente {
                IdContribuyente = id,
                NumeroDocumentoContribuyente = contribuyenteDTO.NumeroDocumentoContribuyente,
                ApellidoNombreContribuyente = contribuyenteDTO.ApellidoNombreContribuyente,
                DomicilioCalleContribuyente = contribuyenteDTO.DomicilioCalleContribuyente,
                DomicilioNumeroContribuyente = contribuyenteDTO.DomicilioNumeroContribuyente,
                TelefonoContribuyente = contribuyenteDTO.TelefonoContribuyente,
                FechaNacimientoContribuyente = contribuyenteDTO.FechaNacimientoContribuyente,
                SexoContribuyente = contribuyenteDTO.SexoContribuyente
            };

            await _service.UpdateContribuyente(contribuyente);

            return NoContent();
        }
        [HttpGet("porId/{id}")]
        public async Task<ActionResult<Result<Contribuyente>>> RecuperacionContribuyente(int id)
        {
            var contribuyente = await _service.GetContribuyenteById(id);
            if (!contribuyente._succes) return NotFound();
            return Ok(contribuyente);
        }
        [HttpGet("porNumero/{NumeroDocumentoContribuyente}")]
        public async Task<ActionResult<Contribuyente>> RecuperacionContribuyente(string NumeroDocumentoContribuyente)
        {
            var contribuyente = await _service.GetContribuyenteByNumero(NumeroDocumentoContribuyente);
            if (contribuyente.Count()==0) return NotFound();
            var cont = contribuyente.FirstOrDefault();
            return Ok(cont);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contribuyente>>> RecuperacionTodosContribuyente()
        {
            var contribuyentes = await _service.GetAllContribuyente();
            return Ok(contribuyentes);
        }
    }
}
