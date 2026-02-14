using DemoArquitectura.Domain.Entities;
using DemoArquitectura.Infrastructure.Repositories;
using Xunit;

namespace DemoArquitectura.Tests;

public class InMemoryProductRepositoryTests
{
    private readonly InMemoryProductRepository _repository;

    public InMemoryProductRepositoryTests()
    {
        _repository = new InMemoryProductRepository();
    }

    [Fact]
    public async Task AddAsync_WithValidProduct_ReturnsProductWithId()
    {
        // Arrange
        var product = new Product
        {
            Name = "Test Product",
            Description = "Test Description",
            Price = 99.99m,
            CreatedDate = DateTime.UtcNow
        };

        // Act
        var result = await _repository.AddAsync(product);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Id > 0);
        Assert.Equal("Test Product", result.Name);
    }

    [Fact]
    public async Task GetAllAsync_WithMultipleProducts_ReturnsAllProducts()
    {
        // Arrange
        var product1 = new Product { Name = "Product 1", Price = 10m, CreatedDate = DateTime.UtcNow };
        var product2 = new Product { Name = "Product 2", Price = 20m, CreatedDate = DateTime.UtcNow };

        await _repository.AddAsync(product1);
        await _repository.AddAsync(product2);

        // Act
        var result = await _repository.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task GetByIdAsync_WithValidId_ReturnsProduct()
    {
        // Arrange
        var product = new Product { Name = "Test", Price = 50m, CreatedDate = DateTime.UtcNow };
        var addedProduct = await _repository.AddAsync(product);

        // Act
        var result = await _repository.GetByIdAsync(addedProduct.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Test", result.Name);
        Assert.Equal(50m, result.Price);
    }

    [Fact]
    public async Task GetByIdAsync_WithInvalidId_ReturnsNull()
    {
        // Act
        var result = await _repository.GetByIdAsync(999);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task UpdateAsync_WithValidProduct_UpdatesProductData()
    {
        // Arrange
        var product = new Product { Name = "Original", Price = 100m, CreatedDate = DateTime.UtcNow };
        var addedProduct = await _repository.AddAsync(product);

        addedProduct.Name = "Updated";
        addedProduct.Price = 150m;

        // Act
        var result = await _repository.UpdateAsync(addedProduct);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Updated", result.Name);
        Assert.Equal(150m, result.Price);
    }

    [Fact]
    public async Task UpdateAsync_WithNonexistentProduct_ThrowsKeyNotFoundException()
    {
        // Arrange
        var product = new Product { Id = 999, Name = "Nonexistent", Price = 50m, CreatedDate = DateTime.UtcNow };

        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(() => _repository.UpdateAsync(product));
    }

    [Fact]
    public async Task DeleteAsync_WithValidId_RemovesProductAndReturnsTrue()
    {
        // Arrange
        var product = new Product { Name = "To Delete", Price = 50m, CreatedDate = DateTime.UtcNow };
        var addedProduct = await _repository.AddAsync(product);

        // Act
        var result = await _repository.DeleteAsync(addedProduct.Id);

        // Assert
        Assert.True(result);
        var deletedProduct = await _repository.GetByIdAsync(addedProduct.Id);
        Assert.Null(deletedProduct);
    }

    [Fact]
    public async Task DeleteAsync_WithInvalidId_ReturnsFalse()
    {
        // Act
        var result = await _repository.DeleteAsync(999);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task Repository_DataPersistsBetweenCalls()
    {
        // Arrange
        var product1 = new Product { Name = "Persist Test 1", Price = 10m, CreatedDate = DateTime.UtcNow };
        var product2 = new Product { Name = "Persist Test 2", Price = 20m, CreatedDate = DateTime.UtcNow };

        // Act
        await _repository.AddAsync(product1);
        await _repository.AddAsync(product2);
        var allProducts = await _repository.GetAllAsync();

        // Assert
        Assert.Equal(2, allProducts.Count());
    }
}