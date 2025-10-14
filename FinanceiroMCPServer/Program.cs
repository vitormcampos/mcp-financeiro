using Application.Ioc;
using FinanceiroMCP;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddApplicationServices();

builder.Services.AddMcpServer().WithHttpTransport().WithTools<FinanceiroMCPTools>();

var app = builder.Build();

app.MapMcp();

app.Run();
