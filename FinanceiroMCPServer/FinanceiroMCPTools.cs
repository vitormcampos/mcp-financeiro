using System.ComponentModel;
using Application.Services;
using Domain;
using Domain.Dtos;
using ModelContextProtocol.Server;

namespace FinanceiroMCP;

[McpServerToolType]
public class FinanceiroMCPTools
{
    private readonly ContaService _contaService;

    public FinanceiroMCPTools(ContaService contaService)
    {
        _contaService = contaService;
    }

    [
        McpServerTool(Name = nameof(GetContas), Title = nameof(GetContas)),
        Description("Obtem todas as contas com filtros opcionais")
    ]
    public async Task<IEnumerable<Conta>> GetContas(
        string description = "",
        decimal minValue = 0,
        decimal maxValue = 0,
        sbyte month = 0,
        short year = 0,
        string status = "",
        string type = ""
    )
    {
        var query = new ContasGetAll(description, minValue, maxValue, month, year, status, type);

        return await _contaService.GetAllAsync(query);
    }

    [
        McpServerTool(Name = nameof(GetContaById), Title = nameof(GetContaById)),
        Description("Obtem uma conta pelo ID ou descrição")
    ]
    public async Task<Conta?> GetContaById(string idOrDescription)
    {
        return await _contaService.GetByIdAsync(idOrDescription);
    }

    [
        McpServerTool(Name = nameof(CreateConta), Title = nameof(CreateConta)),
        Description("Cria uma nova conta na base de dados")
    ]
    public async Task<Conta?> CreateConta(
        string description,
        string status,
        decimal amount,
        string category
    )
    {
        return await _contaService.AddAsync(
            new CreateConta
            {
                Description = description,
                Amount = amount,
                Status = status,
                Type = category,
            }
        );
    }
}
