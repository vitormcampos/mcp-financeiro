using Application.Ioc;
using dotenv.net;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;
using OpenAI.Chat;
using Scalar.AspNetCore;

DotEnv.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddOpenApi();

builder.Services.AddChatClient(services =>
    new ChatClientBuilder(
        new ChatClient("gpt-4o-mini", builder.Configuration["OpenAI:ApiKey"]).AsIChatClient()
    )
        .UseFunctionInvocation()
        .Build()
);

builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    // Interface Scalar
    app.MapScalarApiReference(options =>
    {
        options.Title = "Financeiro API";
        options.Theme = ScalarTheme.Default; // Light, Dark, Default
    });
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
app.UseAuthorization();
app.MapControllers();

app.Run();
