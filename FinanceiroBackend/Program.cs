using FinanceiroBackend;
using FinanceiroBackend.Services;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Adicionar DbContext com PostgreSQL
builder.Services.AddDbContext<FinanceiroContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddOpenApi();

builder.Services.AddScoped<ContaService>();
builder.Services.AddScoped<ContaTipoService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    // Documentação OpenAPI JSON
    app.MapOpenApi();

    // Interface Scalar
    app.MapScalarApiReference(options =>
    {
        options.Title = "Financeiro API";
        options.Theme = ScalarTheme.Default; // Light, Dark, Default
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
