using DemoArquitectura.Domain.Repositories;
using DemoArquitectura.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DemoArquitectura.Infrastructure;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IProductRepository, InMemoryProductRepository>();
        return services;
    }
}