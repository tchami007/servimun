using Microsoft.AspNetCore.Mvc;
using ServiMun.Models;
using ServiMun.Services;

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
        public async Task<ActionResult<Contribuyente>> AltaNuevoContribuyente(ContribuyenteDTO contribuyenteDTO)
        {

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

            var nuevoContribuyente = await _service.AltaNuevoContribuyente(contribuyente);

            return CreatedAtAction(nameof(RecuperacionContribuyente), new { id = nuevoContribuyente.IdContribuyente }, nuevoContribuyente);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> BajaContribuyente(int id)
        {
            var result = await _service.BajaContribuyente(id);
            if (!result) return NotFound();
            return NoContent();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> ModificacionContribuyente(int id, ContribuyenteDTO contribuyenteDTO)
        {
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

            await _service.ModificacionContribuyente(contribuyente);

            return NoContent();
        }
        [HttpGet("porId/{id}")]
        public async Task<ActionResult<Contribuyente>> RecuperacionContribuyente(int id)
        {
            var contribuyente = await _service.RecuperacionContribuyente(id);
            if (contribuyente == null) return NotFound();
            return contribuyente;
        }
        [HttpGet("porNumero/{NumeroDocumentoContribuyente}")]
        public async Task<ActionResult<Contribuyente>> RecuperacionContribuyente(string NumeroDocumentoContribuyente)
        {
            var contribuyente = await _service.RecuperacionContribuyentePorNumero(NumeroDocumentoContribuyente);
            if (contribuyente == null) return NotFound();
            var cont = contribuyente.FirstOrDefault();
            return cont;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contribuyente>>> RecuperacionTodosContribuyente()
        {
            var contribuyentes = await _service.RecuperacionTodosContribuyente();
            return Ok(contribuyentes);
        }
    }
}
