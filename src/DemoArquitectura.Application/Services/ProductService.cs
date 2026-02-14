using DemoArquitectura.Application.DTOs;
using DemoArquitectura.Domain.Entities;
using DemoArquitectura.Domain.Repositories;

namespace DemoArquitectura.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
    {
        var products = await _productRepository.GetAllAsync();
        return products.Select(MapToDto);
    }

    public async Task<ProductDto?> GetProductByIdAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        return product != null ? MapToDto(product) : null;
    }

    public async Task<ProductDto> CreateProductAsync(CreateProductDto createProductDto)
    {
        var product = new Product
        {
            Name = createProductDto.Name,
            Description = createProductDto.Description,
            Price = createProductDto.Price,
            CreatedDate = DateTime.UtcNow
        };

        var createdProduct = await _productRepository.AddAsync(product);
        return MapToDto(createdProduct);
    }

    public async Task<ProductDto?> UpdateProductAsync(UpdateProductDto updateProductDto)
    {
        var product = await _productRepository.GetByIdAsync(updateProductDto.Id);
        if (product == null)
            return null;

        product.Name = updateProductDto.Name;
        product.Description = updateProductDto.Description;
        product.Price = updateProductDto.Price;

        var updatedProduct = await _productRepository.UpdateAsync(product);
        return MapToDto(updatedProduct);
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        return await _productRepository.DeleteAsync(id);
    }

    private static ProductDto MapToDto(Product product)
    {
        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            CreatedDate = product.CreatedDate
        };
    }
}