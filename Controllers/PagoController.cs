using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiMun.Models;
using ServiMun.Services;

namespace ServiMun.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagoController : ControllerBase
    {
        private readonly IPagoService _pagoService;

        public PagoController (IPagoService pagoService)
        {
            _pagoService = pagoService;
        }
        [HttpPost ("PagarBoleta/{numeroPadron}/{periodo}")]
        public async Task<ActionResult<Movimiento>> PagarBoleta(int numeroPadron, int periodo)
        {
            var movimiento = await _pagoService.PagarBoleta (numeroPadron, periodo);
            if (!movimiento._succes) 
            { 
                return BadRequest("Error en el pago:" + movimiento._errorMessage);
            }
            return CreatedAtAction(nameof(RecuperarMovimiento), new { id = movimiento._value.IdMovimiento }, movimiento);
        }
        [HttpPost("ReversarPagoBoleta/{numeroPadron}/{periodo}")]
        public async Task<ActionResult<Movimiento>> ReversarPagoBoleta(int numeroPadron, int periodo)
        {
            var movimiento = await _pagoService.ReversarPagoBoleta(numeroPadron, periodo);
            if (!movimiento._succes)
            {
                return BadRequest("Error en el pago:" + movimiento._errorMessage);
            }
            return CreatedAtAction(nameof(RecuperarMovimiento), new { id = movimiento._value.IdMovimiento }, movimiento);
        }
        [HttpPost("PagarServicio/{numeroServicio}/{periodo}")]
        public async Task<ActionResult<Movimiento>> PagarServicio(int numeroServicio, int periodo)
        {
            var movimiento = await _pagoService.PagarServicio(numeroServicio, periodo);
            if (!movimiento._succes)
            {
                return BadRequest("Error en el pago:" + movimiento._errorMessage);
            }
            return CreatedAtAction(nameof(RecuperarMovimiento), new { id = movimiento._value.IdMovimiento }, movimiento);
        }
        [HttpPost("ReversarPagoServicio/{numeroServicio}/{periodo}")]
        public async Task<ActionResult<Movimiento>> ReversarPagoServicio(int numeroServicio, int periodo)
        {
            var movimiento = await _pagoService.ReversarPagoServicio(numeroServicio, periodo);
            if (!movimiento._succes)
            {
                return BadRequest("Error en el pago:" + movimiento._errorMessage);
            }
            return CreatedAtAction(nameof(RecuperarMovimiento), new { id = movimiento._value.IdMovimiento }, movimiento);
        }
        /*--------------------------------------------------------------------------------------------------*/
        /*--------------------------------------------------------------------------------------------------*/
        /*--------------------------------------------------------------------------------------------------*/
        [HttpGet("{id}")]
        public async Task<ActionResult<Movimiento>> RecuperarMovimiento(int id)
        {
            var movimiento = await _pagoService.GetMovimiento(id);
            return Ok(movimiento._value);
        }
        [HttpGet("Neto/{tipo}/{id}/{fecha}")]
        public async Task<ActionResult<IEnumerable<Movimiento>>> RecuperarMovimientoByTipoId(string tipo, int id, DateTime fecha)
        {
            var resultado = await _pagoService.GetMovimientoByTipoId(tipo, id, fecha);
            return Ok(resultado);
        }
        [HttpGet("Total/{tipo}/{id}/{fecha}")]
        public async Task<ActionResult<IEnumerable<Movimiento>>> RecuperarMovimientoTotalByTipoId(string tipo, int id, DateTime fecha)
        {
            var resultado = await _pagoService.GetMovimientoTotalByTipoId(tipo, id, fecha);
            return Ok(resultado);
        }
    }
}
