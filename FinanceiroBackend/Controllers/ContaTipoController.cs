using FinanceiroBackend.Dtos;
using FinanceiroBackend.Models;
using FinanceiroBackend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinanceiroBackend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContaTipoController : ControllerBase
{
    private readonly ContaTipoService _contaTipoService;

    public ContaTipoController(ContaTipoService contaTipoService)
    {
        _contaTipoService = contaTipoService;
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateContaTipo contaTipo)
    {
        var result = await _contaTipoService.AddAsync(contaTipo);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _contaTipoService.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var result = await _contaTipoService.GetByIdAsync(id);
        if (result == null)
            return NotFound();
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] ContaTipo contaTipo)
    {
        var result = await _contaTipoService.UpdateAsync(id, contaTipo);
        if (result == null)
            return NotFound();
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _contaTipoService.DeleteAsync(id);
        return NoContent();
    }
}
