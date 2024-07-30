using Microsoft.AspNetCore.Mvc;
using ServiMun.Models;
using ServiMun.Services;

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
    public async Task<IActionResult> AltaPadronBoleta([FromBody] PadronBoletaDTO padronBoletaDTO)

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

        var result = await _padronBoletaService.AltaPadronBoleta(nuevoPadronBoleta);

        if (result == null)
        {
            return BadRequest();
        }
        return CreatedAtAction(nameof(RecuperarPadronBoletaPorId), new { id = result.IdBoleta }, result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> BajaPadronBoleta(int id)
    {
        var result = await _padronBoletaService.BajaPadronBoleta(id);
        if (!result)
        {
            return NotFound();
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

        var result = await _padronBoletaService.ModificacionPadronBoleta(id, padronBoleta);

        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }

    [HttpPut("pagoPorId/{id}")]
    public async Task<IActionResult> PagoPadronBoleta(int id)
    {
        var boleta = await _padronBoletaService.RecuperarPadronBoletaPorId(id);

        if (boleta == null)
        {
            return NotFound();
        }

        var encontrado = boleta.FirstOrDefault();

        if (encontrado.Pagado)
        {
            return BadRequest("El servicio ya fue pagado");
        }

        var result = await _padronBoletaService.PagoPadronBoleta(id);

        return Ok(result);
    }

    [HttpPut("pagoPorNumeroPadronPeriodo/{numeroPadron}/{periodo}")]
    public async Task<IActionResult> PagoPadronBoleta(int numeroPadron, int periodo)
    {
        var boleta = await _padronBoletaService.RecuperarPadronBoletaPorNumeroPadronPeriodo(numeroPadron,periodo);

        if (boleta == null)
        {
            return NotFound();
        }

        var encontrado = boleta.FirstOrDefault();

        if (encontrado.Pagado)
        {
            return BadRequest("El servicio ya fue pagado");
        }

        var result = await _padronBoletaService.PagoPadronBoleta(encontrado.IdBoleta);

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<IEnumerable<PadronBoletaGetDTO>>> RecuperarPadronBoletaPorId(int id)
    {
        var boletas = await _padronBoletaService.RecuperarPadronBoletaPorId(id);

        if (boletas == null)
        {
            return NotFound();
        }

        return Ok(boletas);
    }

    [HttpGet("numeroPadron/{numeroPadron}")]
    public async Task<ActionResult> RecuperarPadronBoletaPorNumeroPadron(int numeroPadron)
    {

        var result = await _padronBoletaService.RecuperarPadronBoletaPorNumeroPadron(numeroPadron);
        if (result == null || !result.Any())
        {
            return NotFound();
        }
        return Ok(result);
    }

    [HttpGet("tributoPeriodo/{idTributo}/{periodo}")]
    public async Task<ActionResult> RecuperarPadronBoletaPorTributoPeriodo(int idTributo, int periodo)
    {
        var result = await _padronBoletaService.RecuperarPadronBoletaPorTributoPeriodo(idTributo, periodo);
        if (result == null || !result.Any())
        {
            return NotFound();
        }
        return Ok(result);
    }

    [HttpGet("padronPeriodo/{numeroPadron}/{periodo}")]
    public async Task<ActionResult> RecuperarPadronBoletaPorNumeroPadronPeriod(int numeroPadron, int periodo)
    {

        var result = await _padronBoletaService.RecuperarPadronBoletaPorNumeroPadronPeriodo(numeroPadron, periodo);
        if (result == null || !result.Any())
        {
            return NotFound();
        }
        return Ok(result);
    }

}
