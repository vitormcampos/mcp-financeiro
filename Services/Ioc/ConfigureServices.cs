using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Ioc;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<HashService>();
        services.AddScoped<JwtService>();
        services.AddScoped<UserService>();
        services.AddScoped<ContaService>();

        return services;
    }
}
