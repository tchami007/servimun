﻿using Microsoft.AspNetCore.Mvc;
using ServiMun.Models;
using ServiMun.Services;

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
        public async Task<ActionResult<PadronContribuyenteDTO>> AltaContribuyentePadron([FromBody] PadronContribuyenteDTO padronContribuyenteDTO)
        {
            var nuevoPadron = new PadronContribuyente
            {
                IdContribuyente = padronContribuyenteDTO.IdContribuyente,
                IdTributoMunicipal = padronContribuyenteDTO.IdTributoMunicipal,
                NumeroPadron = padronContribuyenteDTO.NumeroPadron,
                Estado = padronContribuyenteDTO.Estado
            };

            await _service.AltaContribuyentePadron(nuevoPadron);
            return CreatedAtAction(nameof(RecuperaPadronContribuyenteId), new { idContribuyente = nuevoPadron.IdContribuyente, idTributoMunicipal = nuevoPadron.IdTributoMunicipal }, nuevoPadron);
        }

        [HttpDelete("{idContribuyente}/{idTributoMunicipal}")]
        public async Task<IActionResult> BajaContribuyentePadron(int idContribuyente, int idTributoMunicipal)
        {
            var result = await _service.BajaContribuyentePadron(idContribuyente, idTributoMunicipal);
            if (!result) return NotFound();
            return NoContent();
        }


        /// <summary>
        /// Api de modificacion de enlace Padron-Contribuyente
        /// </summary>
        /// <param name="idContribuyente">Elemento entero, identificador de contribuyente</param>
        /// <param name="idTributoMunicipal">Elemento entero, identificador del tributo municipal</param>
        /// <param name="padronContribuyenteDTO">Elemento de tipo PadronContribuyente con el resto de atributos a modificar</param>
        /// <returns></returns>
        [HttpPut("{idContribuyente}/{idTributoMunicipal}")]
        public async Task<IActionResult> ModificacionContribuyentePadron(int idContribuyente, int idTributoMunicipal, [FromBody] PadronContribuyenteDTO padronContribuyenteDTO)
        {
            if (idContribuyente != padronContribuyenteDTO.IdContribuyente || idTributoMunicipal != padronContribuyenteDTO.IdTributoMunicipal) return BadRequest();

            var padronContribuyente = await _service.RecuperarContribuyentePadronId(idContribuyente, idTributoMunicipal);

            if (padronContribuyente == null)
            {
                return NotFound();
            }

            var padron = padronContribuyente.FirstOrDefault();

            if(padron == null)
            {
                return NotFound();
            }

            padron.NumeroPadron = padronContribuyenteDTO.NumeroPadron;
            padron.Estado = padronContribuyenteDTO.Estado;

            var result = await _service.ModificacionContribuyentePadron(padron);
            if (!result)
            {
                return BadRequest("No se puede modificar el NumeroPadron porque está relacionado con PadronBoleta.");
            }

            return NoContent();
        }

        [HttpGet("{idContribuyente}/{idTributoMunicipal}")]
        public async Task<ActionResult<IEnumerable<PadronContribuyenteGetDTO>>> RecuperaPadronContribuyenteId(int idContribuyente, int idTributoMunicipal)
        {
            var padrones = await _service.RecuperarContribuyentePadronId(idContribuyente,idTributoMunicipal);

            return Ok(padrones);
        }

        [HttpGet("contribuyente/{numeroDocumentoContribuyente}")]
        public async Task<ActionResult<IEnumerable<PadronContribuyenteGetDTO>>> RecuperaPadronContribuyente(string numeroDocumentoContribuyente)
        {
            var padrones = await _service.RecuperaPadronContribuyente(numeroDocumentoContribuyente);
            return Ok(padrones);
        }

        [HttpGet("padron/{numeroPadron}")]
        public async Task<ActionResult<IEnumerable<PadronContribuyenteGetDTO>>> RecuperaContribuyentePadron(int numeroPadron)
        {
            var padrones = await _service.RecuperaContribuyentePadron(numeroPadron);
            return Ok(padrones);
        }

        [HttpGet("tributo/{idTributoMunicipal}")]
        public async Task<ActionResult<IEnumerable<PadronContribuyenteGetDTO>>> RecuperarContribuyenteTributo(int idTributoMunicipal)
        {
            var padrones = await _service.RecuperarPadronTributo(idTributoMunicipal);
            return Ok(padrones);
        }
    }
}
