using DemoArquitectura.Application;
using DemoArquitectura.Application.Services;
using DemoArquitectura.Domain.Repositories;
using DemoArquitectura.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace DemoArquitectura.Tests;

public class DependencyInjectionTests
{
    [Fact]
    public void AddApplication_RegistersProductService()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddApplication();
        services.AddInfrastructure();  // Need infrastructure for repository
        var provider = services.BuildServiceProvider();

        // Assert
        var productService = provider.GetService<IProductService>();
        Assert.NotNull(productService);
        Assert.IsType<ProductService>(productService);
    }

    [Fact]
    public void AddInfrastructure_RegistersProductRepository()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddInfrastructure();
        var provider = services.BuildServiceProvider();

        // Assert
        var repository = provider.GetService<IProductRepository>();
        Assert.NotNull(repository);
    }

    [Fact]
    public void AddInfrastructure_RegistersRepositoryAsSingleton()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddInfrastructure();
        var provider = services.BuildServiceProvider();

        // Assert
        var repo1 = provider.GetService<IProductRepository>();
        var repo2 = provider.GetService<IProductRepository>();

        Assert.Same(repo1, repo2);
    }

    [Fact]
    public void CompleteSetup_RegistersAllServices()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddApplication();
        services.AddInfrastructure();
        var provider = services.BuildServiceProvider();

        // Assert
        var productService = provider.GetService<IProductService>();
        var repository = provider.GetService<IProductRepository>();

        Assert.NotNull(productService);
        Assert.NotNull(repository);
    }
}