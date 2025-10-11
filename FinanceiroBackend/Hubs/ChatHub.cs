using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.AI;
using ModelContextProtocol.Client;

namespace Web.Hubs;

public class ChatHub : Hub
{
    private readonly IChatClient _chatClient;
    private readonly IConfiguration _configuration;
    private readonly Uri mcpUri;

    public ChatHub(IChatClient chatClient, IConfiguration configuration)
    {
        _chatClient = chatClient;
        _configuration = configuration;
        mcpUri = new Uri(
            _configuration.GetValue<string>("MCPServer:Url") + "/sse"
                ?? "http://localhost:55626/sse"
        );
    }

    public async Task SendPrompt(string prompt)
    {
        var mcpClient = await McpClient.CreateAsync(
            new HttpClientTransport(new() { Endpoint = mcpUri, Name = "FinanceiroMCP" })
        );
        var tools = await mcpClient.ListToolsAsync();
        var messages = new List<ChatMessage>
        {
            new ChatMessage(
                ChatRole.System,
                "Você é um assistente para consumir o MCP FinanceiroMCP e entregar os dados em markdown para o usuário."
            ),
            new(ChatRole.User, prompt),
        };

        var response = await _chatClient.GetResponseAsync(
            messages,
            options: new() { Tools = [.. tools], AllowMultipleToolCalls = true }
        );

        await Clients.Caller.SendAsync("ReceivePrompt", response.Text);
    }
}
