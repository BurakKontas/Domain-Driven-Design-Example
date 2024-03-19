using DDD.Domain.Products;

namespace DDD.Tests.Unit.Domain;

public class ProductTests
{
    [Fact]
    public void CreateProduct_Should_Create_Product_With_Given_Name_Price_And_Sku()
    {
        // Arrange
        const string name = "Product Name";
        var price = new Money("USD", 10.0m);
        var sku = Sku.Create("123456789012345");
        if (sku is null) throw new ArgumentNullException(nameof(sku));

        // Act
        var product = Product.Create(name, price, sku);

        // Assert
        Assert.NotNull(product);
        Assert.Equal(name, product.Name);
        Assert.Equal(price, product.Price);
        Assert.Equal(sku, product.Sku);
        Assert.NotEqual(default, product.Id);
    }

    [Fact]
    public void Update_Should_Update_Product_Details()
    {
        // Arrange
        const string name = "Product Name";
        const string newName = "New Product Name";
        var price = new Money("USD", 10.0m);
        var newPrice = new Money("USD", 15.0m);

        var sku = Sku.Create("123456789012345");
        if (sku is null) throw new ArgumentNullException(nameof(sku));

        var newSku = Sku.Create("098765432154321");
        if (newSku is null) throw new ArgumentNullException(nameof(newSku));

        var product = Product.Create(name, price, sku);

        // Act
        product.Update(newName, newPrice, newSku!);

        // Assert
        Assert.Equal(newName, product.Name);
        Assert.Equal(newPrice, product.Price);
        Assert.Equal(newSku, product.Sku);
    }
}