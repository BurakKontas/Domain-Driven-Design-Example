using DDD.DataAccess.Contracts;
using DDD.Domain.Products;
using DDD.Service.Services;
using Moq;

namespace DDD.Tests.Unit.Service;

public class ProductServiceTests
{
    [Fact]
    public async Task CreateAsync_Should_Create_Product()
    {
        // Arrange
        var productRepositoryMock = new Mock<IProductRepository>();
        var productService = new ProductService(productRepositoryMock.Object);
        var product = Product.Create("Product", new Money("USD", 100), Sku.Create("123456789012345"));

        // Act
        await productService.CreateAsync(product);

        // Assert
        productRepositoryMock.Verify(repo => repo.AddAsync(product, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_Should_Return_Product_When_Found()
    {
        // Arrange
        var productId = new ProductId(Guid.NewGuid());
        var expectedProduct = Product.Create("Product", new Money("USD", 100), Sku.Create("123456789012345"));
        var productRepositoryMock = new Mock<IProductRepository>();
        productRepositoryMock.Setup(repo => repo.GetByIdAsync(productId, CancellationToken.None))
            .ReturnsAsync(expectedProduct);
        var productService = new ProductService(productRepositoryMock.Object);

        // Act
        var result = await productService.GetByIdAsync(productId);

        // Assert
        Assert.Equal(expectedProduct, result);
    }

    [Fact]
    public async Task GetByIdAsync_Should_Return_Null_When_Not_Found()
    {
        // Arrange
        var productId = new ProductId(Guid.NewGuid());
        var productRepositoryMock = new Mock<IProductRepository>();
        var productService = new ProductService(productRepositoryMock.Object);

        // Act
        var result = await productService.GetByIdAsync(productId);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetAllAsync_Should_Return_All_Products()
    {
        // Arrange
        var expectedProducts = new List<Product?>
        {
            Product.Create("Product", new Money("USD", 100), Sku.Create("123456789012345")),
            Product.Create("Product", new Money("USD", 100), Sku.Create("123456789012345"))
        };
        var productRepositoryMock = new Mock<IProductRepository>();
        productRepositoryMock.Setup(repo => repo.GetAllAsync(CancellationToken.None)).ReturnsAsync(expectedProducts);
        var productService = new ProductService(productRepositoryMock.Object);

        // Act
        var result = await productService.GetAllAsync();

        // Assert
        Assert.Equal(expectedProducts, result);
    }

    [Fact]
    public async Task UpdateAsync_Should_Update_Product()
    {
        // Arrange
        var product = Product.Create("Product", new Money("USD", 100), Sku.Create("123456789012345"));
        var productRepositoryMock = new Mock<IProductRepository>();
        var productService = new ProductService(productRepositoryMock.Object);

        // Act
        await productService.UpdateAsync(product);

        // Assert
        productRepositoryMock.Verify(repo => repo.UpdateAsync(product, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_Should_Delete_Product_When_Found()
    {
        // Arrange
        var productId = new ProductId(Guid.NewGuid());
        var product = Product.Create("Product", new Money("USD", 100), Sku.Create("123456789012345"));
        var productRepositoryMock = new Mock<IProductRepository>();
        productRepositoryMock.Setup(repo => repo.GetByIdAsync(productId, CancellationToken.None)).ReturnsAsync(product);
        var productService = new ProductService(productRepositoryMock.Object);

        // Act
        await productService.DeleteAsync(productId);

        // Assert
        productRepositoryMock.Verify(repo => repo.DeleteAsync(product, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_Should_Not_Delete_Product_When_Not_Found()
    {
        // Arrange
        var productId = new ProductId(Guid.NewGuid());
        var productRepositoryMock = new Mock<IProductRepository>();
        var productService = new ProductService(productRepositoryMock.Object);

        // Act
        await productService.DeleteAsync(productId);

        // Assert
        productRepositoryMock.Verify(repo => repo.DeleteAsync(It.IsAny<Product>(), CancellationToken.None),
            Times.Never);
    }
}