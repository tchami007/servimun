using Microsoft.AspNetCore.Mvc;
using ServiMun.Models;
using ServiMun.Services;
using ServiMun.Shared;

[ApiController]
[Route("api/[controller]")]
public class PadronBoletaController : ControllerBase
{
    private readonly IPadronBoletaService _padronBoletaService;

    public PadronBoletaController(IPadronBoletaService padronBoletaService)
    {
        _padronBoletaService = padronBoletaService;
    }

    [HttpPost]
    public async Task<ActionResult<Result<PadronBoleta>>> AltaPadronBoleta([FromBody] PadronBoletaDTO padronBoletaDTO)

    {
        var nuevoPadronBoleta = new PadronBoleta
        { 
            NumeroPadron = padronBoletaDTO.NumeroPadron,
            Importe = padronBoletaDTO.Importe,
            Vencimiento = padronBoletaDTO.Vencimiento,
            Periodo = padronBoletaDTO.Periodo,
            Pagado = padronBoletaDTO.Pagado,
            Vencimiento2 = padronBoletaDTO.Vencimiento2,
            Importe2 = padronBoletaDTO.Importe2

        };

        var result = await _padronBoletaService.AddPadronBoleta(nuevoPadronBoleta);

        if (!result._succes)
        {
            return BadRequest(result);
        }

        return CreatedAtAction(nameof(RecuperarPadronBoletaPorId), new { id = result._value.IdBoleta }, result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> BajaPadronBoleta(int id)
    {
        var result = await _padronBoletaService.DeletePadronBoleta(id);
        if (!result._succes)
        {
            return NotFound(result);
        }
        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> ModificacionPadronBoleta(int id, [FromBody] PadronBoletaDTO padronBoletaDTO)
    {
        if (id != padronBoletaDTO.IdBoleta)
        {
            return BadRequest("El id del request no coincide con el id en el cuerpo de la solicitud");
        }

        var padronBoleta = new PadronBoleta
        {
            IdBoleta = id,
            NumeroPadron = padronBoletaDTO.NumeroPadron,
            Importe = padronBoletaDTO.Importe,
            Vencimiento = padronBoletaDTO.Vencimiento,
            Periodo = padronBoletaDTO.Periodo,
            Pagado = padronBoletaDTO.Pagado,
            Vencimiento2 = padronBoletaDTO.Vencimiento2,
            Importe2 = padronBoletaDTO.Importe2
        };

        var result = await _padronBoletaService.UpdatePadronBoleta(id, padronBoleta);

        if (!result._succes)
        {
            return NotFound(result);
        }
        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PadronBoleta>> RecuperarPadronBoletaPorId(int id)
    {
        var resultado = await _padronBoletaService.GetPadronBoletaById(id);

        if (!resultado._succes)
        {
            return NotFound(resultado);
        }

        return Ok(resultado._value);
    }

    [HttpGet("numeroPadron/{numeroPadron}")]
    public async Task<ActionResult> RecuperarPadronBoletaPorTributoPeriodo(int numeroPadron)
    {
        var result = await _padronBoletaService.GetAllPadronBoletaByNumeroPadron(numeroPadron);
        if (result == null || !result.Any())
        {
            return NotFound();
        }
        return Ok(result);
    }

    [HttpGet("generar/{numeroPadron}/{periodoInicial}/{cantidad}")]
    public async Task<ActionResult> Generar (int numeroPadron, int periodoInicial, int cantidad)
    {
        var resultado = await _padronBoletaService.Generar(numeroPadron, periodoInicial, cantidad);
        return Ok(resultado);
    }

}
