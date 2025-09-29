using System.Text;
using Domain.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.AI;
using ModelContextProtocol.Client;

namespace Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ChatController : ControllerBase
{
    private readonly IChatClient _chatClient;
    private readonly IConfiguration _configuration;
    private Uri mcpUri;

    public ChatController(IChatClient chatClient, IConfiguration configuration)
    {
        _chatClient = chatClient;
        _configuration = configuration;
        mcpUri = new Uri(
            _configuration.GetValue<string>("MCPServer:Url") + "/sse"
                ?? "http://localhost:55626/sse"
        );
    }

    [HttpPost]
    public async Task<ChatResponseDto> Chat(ChatPrompt prompt)
    {
        var mcpClient = await McpClientFactory.CreateAsync(
            new SseClientTransport(
                new SseClientTransportOptions { Endpoint = mcpUri, Name = "FinanceiroMCP" }
            )
        );
        var tools = await mcpClient.ListToolsAsync();
        var messages = new List<ChatMessage>
        {
            new ChatMessage(
                ChatRole.System,
                "Você é um assistente para consumir o MCP FinanceiroMCP e entregar os dados de forma formatada e legivel para o usuário."
            ),
            new(ChatRole.User, prompt.Message),
        };

        List<ChatResponseUpdate> updates = [];
        StringBuilder result = new StringBuilder();

        await foreach (
            var update in _chatClient.GetStreamingResponseAsync(
                messages,
                new() { Tools = [.. tools] }
            )
        )
        {
            result.Append(update);
            updates.Add(update);
        }
        messages.AddMessages(updates);

        return new(result.ToString());
    }
}
