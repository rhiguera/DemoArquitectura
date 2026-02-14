using DemoArquitectura.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DemoArquitectura.Application;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IProductService, ProductService>();
        return services;
    }
}