using System.ComponentModel;
using System.Net.Http.Json;
using Application.Services;
using Domain;
using Domain.Dtos;
using ModelContextProtocol.Server;

namespace FinanceiroMCP;

[McpServerToolType]
public class FinanceiroMCPTools
{
    private readonly ContaService _contaService;
    private readonly ContaTipoService _contaTipoService;

    public FinanceiroMCPTools(ContaService contaService, ContaTipoService contaTipoService)
    {
        _contaService = contaService;
        _contaTipoService = contaTipoService;
    }

    [
        McpServerTool(Name = nameof(GetContas), Title = nameof(GetContas)),
        Description("Chama o endpoint /contas da API REST")
    ]
    public async Task<IEnumerable<Conta>> GetContas(
        DateTime from,
        DateTime to,
        string status,
        string category
    )
    {
        var query = new GetAll(from, to, status, category);

        return await _contaService.GetAllAsync(query);
    }

    [
        McpServerTool(Name = nameof(GetContaById), Title = nameof(GetContaById)),
        Description("Chama o endpoint /contas/{id} da API REST")
    ]
    public async Task<Conta?> GetContaById(string id)
    {
        return await _contaService.GetByIdAsync(id);
    }

    [
        McpServerTool(Name = nameof(CreateConta), Title = nameof(CreateConta)),
        Description("Chama o endpoint /contas da API REST para criar uma nova conta")
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

    //[McpServerTool, Description("Chama o endpoint /ContaTipo da API REST")]
    //public async Task<IEnumerable<ContaTipo>> GetTiposConta()
    //{
    //    using var http = new HttpClient();
    //    var result = await http.GetAsync($"{_baseUrl}/api/tipo");

    //    if (!result.IsSuccessStatusCode)
    //    {
    //        return [];
    //    }

    //    var tipos = await result.Content.ReadFromJsonAsync<IEnumerable<ContaTipo>>();
    //    return tipos ?? [];
    //}

    //[
    //    McpServerTool,
    //    Description("Chama o endpoint /ContaTipo da API REST para criar um novo tipo de conta")
    //]
    //public async Task<ContaTipo?> CreateTipoConta(string nome)
    //{
    //    using var http = new HttpClient();
    //    var novoTipo = new { nome };
    //    var result = await http.PostAsJsonAsync($"{_baseUrl}/api/ContaTipo", novoTipo);

    //    if (!result.IsSuccessStatusCode)
    //    {
    //        return null;
    //    }

    //    var tipoCriado = await result.Content.ReadFromJsonAsync<ContaTipo>();
    //    return tipoCriado;
    //}

    //[McpServerTool, Description("Chama o endpoint /ContaTipo/{id} da API REST")]
    //public async Task<ContaTipo?> GetTipoContaById(string id)
    //{
    //    using var http = new HttpClient();
    //    var result = await http.GetAsync($"{_baseUrl}/api/ContaTipo/{id}");

    //    if (!result.IsSuccessStatusCode)
    //    {
    //        return null;
    //    }

    //    var tipo = await result.Content.ReadFromJsonAsync<ContaTipo>();
    //    return tipo;
    //}
}
