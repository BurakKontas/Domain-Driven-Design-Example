using DDD.DataAccess.Repositories;
using DDD.Domain.Products;
using DDD.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace DDD.Tests.Unit.Repository;

public class ProductRepositoryTests : IDisposable
{
    private readonly ApplicationDbContext _context;
    private readonly ProductRepository _repository;

    public ProductRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _context = new ApplicationDbContext(options);
        _repository = new ProductRepository(_context);
    }

    [Fact]
    public async Task GetByIdAsync_Should_Return_Product_When_Found()
    {
        // Arrange
        var productId = new ProductId(Guid.NewGuid());
        const string productName = "Test Product";
        var productPrice = new Money("USD", 100); // Assuming there is a Money class to represent price
        var productSku = Sku.Create("123456789012345"); // Assuming there is a Sku class
        var product = Product.Create(productName, productPrice, productSku);

        product.GetType().GetProperty("Id").SetValue(product, productId); // Manually set the Id property

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.GetByIdAsync(productId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(productId, result.Id);
    }

    [Fact]
    public async Task GetByIdAsync_Should_Return_Null_When_Not_Found()
    {
        // Arrange
        var productId = new ProductId(Guid.NewGuid());

        // Act
        var result = await _repository.GetByIdAsync(productId);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetAllAsync_Should_Return_All_Products()
    {
        // Arrange
        var products = new[]
        {
            Product.Create("Product1", new Money("USD", 100), Sku.Create("123456789012345")),
            Product.Create("Product2", new Money("USD", 200), Sku.Create("098765432112345")),
            Product.Create("Product3", new Money("USD", 300), Sku.Create("123455432112345"))
        };
        _context.Products.AddRange(products);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(products.Length, result.Count());
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}