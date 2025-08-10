using FinanceiroBackend.Dtos;
using FinanceiroBackend.Models;
using FinanceiroBackend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinanceiroBackend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContaController : ControllerBase
{
    private readonly ContaService _contaService;

    public ContaController(ContaService contaService)
    {
        _contaService = contaService;
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateConta conta)
    {
        var result = await _contaService.AddAsync(conta);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _contaService.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var result = await _contaService.GetByIdAsync(id);
        if (result == null)
            return NotFound();
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] Conta conta)
    {
        var result = await _contaService.UpdateAsync(id, conta);
        if (result == null)
            return NotFound();
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _contaService.DeleteAsync(id);
        return NoContent();
    }
}
