using System.ComponentModel;
using System.Net.Http.Json;
using Domain;
using ModelContextProtocol.Server;

namespace FinanceiroMCP;

[McpServerToolType]
public static class ApiBridgeTools
{
    private static string _baseUrl = "https://localhost:7097";

    [McpServerTool, Description("Chama o endpoint /contas da API REST")]
    public static async Task<IEnumerable<Conta>> GetContas(
        DateOnly? From,
        DateOnly? To,
        string? status,
        string? type
    )
    {
        using var http = new HttpClient();

        var queryParams = new List<string>();
        if (From.HasValue)
        {
            queryParams.Add($"from={From.Value:yyyy-MM-dd}");
        }
        if (To.HasValue)
        {
            queryParams.Add($"to={To.Value:yyyy-MM-dd}");
        }
        if (!string.IsNullOrEmpty(status))
        {
            queryParams.Add($"status={status}");
        }
        if (!string.IsNullOrEmpty(type))
        {
            queryParams.Add($"type={type}");
        }

        var result = await http.GetAsync($"{_baseUrl}/api/conta?{string.Join("&", queryParams)}");

        if (!result.IsSuccessStatusCode)
        {
            return [];
        }

        var contas = await result.Content.ReadFromJsonAsync<IEnumerable<Conta>>();
        return contas ?? [];
    }

    [McpServerTool, Description("Chama o endpoint /contas/{id} da API REST")]
    public static async Task<Conta?> GetContaById(int id)
    {
        using var http = new HttpClient();
        var result = await http.GetAsync($"{_baseUrl}/api/conta/{id}");

        if (!result.IsSuccessStatusCode)
        {
            return null;
        }

        var conta = await result.Content.ReadFromJsonAsync<Conta>();
        return conta;
    }

    [McpServerTool, Description("Chama o endpoint /contas da API REST para criar uma nova conta")]
    public static async Task<Conta?> CreateConta(string tipo, string status, decimal valor)
    {
        using var http = new HttpClient();

        var tipoExists = await GetTipoContaById(tipo);
        if (tipoExists == null)
        {
            var novoTipo = await CreateTipoConta(tipo);

            if (novoTipo == null)
            {
                return null;
            }

            tipoExists = novoTipo;
        }

        var novaConta = new
        {
            tipoId = tipoExists.Id,
            valor,
            status,
        };
        var result = await http.PostAsJsonAsync($"{_baseUrl}/api/conta", novaConta);

        if (!result.IsSuccessStatusCode)
        {
            return null;
        }

        var contaCriada = await result.Content.ReadFromJsonAsync<Conta>();
        return contaCriada;
    }

    [McpServerTool, Description("Chama o endpoint /ContaTipo da API REST")]
    public static async Task<IEnumerable<ContaTipo>> GetTiposConta()
    {
        using var http = new HttpClient();
        var result = await http.GetAsync($"{_baseUrl}/api/tipo");

        if (!result.IsSuccessStatusCode)
        {
            return [];
        }

        var tipos = await result.Content.ReadFromJsonAsync<IEnumerable<ContaTipo>>();
        return tipos ?? [];
    }

    [
        McpServerTool,
        Description("Chama o endpoint /ContaTipo da API REST para criar um novo tipo de conta")
    ]
    public static async Task<ContaTipo?> CreateTipoConta(string nome)
    {
        using var http = new HttpClient();
        var novoTipo = new { nome };
        var result = await http.PostAsJsonAsync($"{_baseUrl}/api/ContaTipo", novoTipo);

        if (!result.IsSuccessStatusCode)
        {
            return null;
        }

        var tipoCriado = await result.Content.ReadFromJsonAsync<ContaTipo>();
        return tipoCriado;
    }

    [McpServerTool, Description("Chama o endpoint /ContaTipo/{id} da API REST")]
    public static async Task<ContaTipo?> GetTipoContaById(string id)
    {
        using var http = new HttpClient();
        var result = await http.GetAsync($"{_baseUrl}/api/ContaTipo/{id}");

        if (!result.IsSuccessStatusCode)
        {
            return null;
        }

        var tipo = await result.Content.ReadFromJsonAsync<ContaTipo>();
        return tipo;
    }
}
