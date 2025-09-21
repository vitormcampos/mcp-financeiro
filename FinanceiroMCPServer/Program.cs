using Application.Ioc;
using FinanceiroMCP;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices(builder.Configuration);

builder.Services.AddMcpServer().WithHttpTransport().WithTools<FinanceiroMCPTools>();

var app = builder.Build();

app.MapMcp();

app.Run();
