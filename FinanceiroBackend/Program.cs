using Application.Ioc;
using dotenv.net;
using Microsoft.Extensions.AI;
using OllamaSharp;
using OpenAI.Chat;
using Scalar.AspNetCore;
using Web.Hubs;

DotEnv.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSignalR();

builder.Services.AddOpenApi();

builder.Services.AddChatClient(services =>
    new ChatClientBuilder(
        new ChatClient("gpt-4o-mini", builder.Configuration["OpenAI:ApiKey"]).AsIChatClient()
    )
        .UseFunctionInvocation()
        .Build()
);

builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddApplicationServices();

builder.Logging.AddConsole();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    // Interface Scalar
    app.MapScalarApiReference(
        "/",
        options =>
        {
            options.Title = "Financeiro API";
            options.Theme = ScalarTheme.Default; // Light, Dark, Default
        }
    );
}

if (app.Environment.IsProduction())
{
    app.UseCors(options =>
        options.WithOrigins(builder.Configuration.GetValue<string>("App:Cors") ?? "")
    );
}
else if (app.Environment.IsDevelopment())
{
    app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapHub<ChatHub>("chat");

app.Run();
