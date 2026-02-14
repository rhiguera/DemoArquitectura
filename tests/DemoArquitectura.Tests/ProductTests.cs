using DemoArquitectura.Application.DTOs;
using DemoArquitectura.Application.Services;
using DemoArquitectura.Domain.Entities;
using DemoArquitectura.Domain.Repositories;
using Moq;
using Xunit;

namespace DemoArquitectura.Tests;

public class ProductServiceTests
{
    private readonly Mock<IProductRepository> _mockRepository;
    private readonly IProductService _productService;

    public ProductServiceTests()
    {
        _mockRepository = new Mock<IProductRepository>();
        _productService = new ProductService(_mockRepository.Object);
    }

    [Fact]
    public async Task CreateProductAsync_WithValidData_ReturnsProductDto()
    {
        // Arrange
        var createProductDto = new CreateProductDto
        {
            Name = "Test Product",
            Description = "Test Description",
            Price = 99.99m
        };

        var expectedProduct = new Product
        {
            Id = 1,
            Name = createProductDto.Name,
            Description = createProductDto.Description,
            Price = createProductDto.Price,
            CreatedDate = DateTime.UtcNow
        };

        _mockRepository
            .Setup(r => r.AddAsync(It.IsAny<Product>()))
            .ReturnsAsync(expectedProduct);

        // Act
        var result = await _productService.CreateProductAsync(createProductDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("Test Product", result.Name);
        Assert.Equal(99.99m, result.Price);
        _mockRepository.Verify(r => r.AddAsync(It.IsAny<Product>()), Times.Once);
    }

    [Fact]
    public async Task GetAllProductsAsync_WithProducts_ReturnsListOfProductDtos()
    {
        // Arrange
        var products = new List<Product>
        {
            new Product { Id = 1, Name = "Product 1", Price = 10m, CreatedDate = DateTime.UtcNow },
            new Product { Id = 2, Name = "Product 2", Price = 20m, CreatedDate = DateTime.UtcNow }
        };

        _mockRepository
            .Setup(r => r.GetAllAsync())
            .ReturnsAsync(products);

        // Act
        var result = await _productService.GetAllProductsAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        _mockRepository.Verify(r => r.GetAllAsync(), Times.Once);
    }

    [Fact]
    public async Task GetProductByIdAsync_WithValidId_ReturnsProductDto()
    {
        // Arrange
        var productId = 1;
        var expectedProduct = new Product
        {
            Id = productId,
            Name = "Test Product",
            Price = 50m,
            CreatedDate = DateTime.UtcNow
        };

        _mockRepository
            .Setup(r => r.GetByIdAsync(productId))
            .ReturnsAsync(expectedProduct);

        // Act
        var result = await _productService.GetProductByIdAsync(productId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(productId, result.Id);
        Assert.Equal("Test Product", result.Name);
    }

    [Fact]
    public async Task GetProductByIdAsync_WithInvalidId_ReturnsNull()
    {
        // Arrange
        _mockRepository
            .Setup(r => r.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((Product)null);

        // Act
        var result = await _productService.GetProductByIdAsync(999);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task UpdateProductAsync_WithValidData_ReturnsUpdatedProductDto()
    {
        // Arrange
        var updateProductDto = new UpdateProductDto
        {
            Id = 1,
            Name = "Updated Product",
            Description = "Updated Description",
            Price = 150m
        };

        var updatedProduct = new Product
        {
            Id = 1,
            Name = "Updated Product",
            Description = "Updated Description",
            Price = 150m,
            CreatedDate = DateTime.UtcNow
        };

        _mockRepository
            .Setup(r => r.UpdateAsync(It.IsAny<Product>()))
            .ReturnsAsync(updatedProduct);

        _mockRepository
            .Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync(updatedProduct);

        // Act
        var result = await _productService.UpdateProductAsync(updateProductDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(150m, result.Price);
        Assert.Equal("Updated Product", result.Name);
    }

    [Fact]
    public async Task DeleteProductAsync_WithValidId_ReturnsTrue()
    {
        // Arrange
        _mockRepository
            .Setup(r => r.DeleteAsync(It.IsAny<int>()))
            .ReturnsAsync(true);

        // Act
        var result = await _productService.DeleteProductAsync(1);

        // Assert
        Assert.True(result);
        _mockRepository.Verify(r => r.DeleteAsync(1), Times.Once);
    }

    [Fact]
    public async Task DeleteProductAsync_WithInvalidId_ReturnsFalse()
    {
        // Arrange
        _mockRepository
            .Setup(r => r.DeleteAsync(It.IsAny<int>()))
            .ReturnsAsync(false);

        // Act
        var result = await _productService.DeleteProductAsync(999);

        // Assert
        Assert.False(result);
    }
}