using DDD.Domain.Customers;
using DDD.Domain.Orders;
using DDD.Domain.Products;

namespace DDD.Tests.Unit.Domain;

public class OrderTests
{
    [Fact]
    public void CreateOrder_Should_Create_Order_With_Given_CustomerId()
    {
        // Arrange
        var customerId = new CustomerId(Guid.NewGuid());

        // Act
        var order = Order.Create(customerId);

        // Assert
        Assert.NotNull(order);
        Assert.Equal(customerId, order.CustomerId);
        Assert.NotEqual(default, order.Id);
    }

    [Fact]
    public void AddLineItem_Should_Add_LineItem_To_Order()
    {
        // Arrange
        var order = Order.Create(new CustomerId(Guid.NewGuid()));
        var productId = new ProductId(Guid.NewGuid());
        var price = new Money( "USD", 10.0m);

        // Act
        order.AddLineItem(productId, price);

        // Assert
        Assert.NotEmpty(order.LineItems);
        Assert.Single(order.LineItems);
        Assert.Contains(order.LineItems, li => li.ProductId == productId);
    }

    [Fact]
    public void RemoveLineItem_Should_Remove_LineItem_From_Order()
    {
        // Arrange
        var order = Order.Create(new CustomerId(Guid.NewGuid()));
        var productId = new ProductId(Guid.NewGuid());
        var price = new Money("USD", 10.0m);
        order.AddLineItem(productId, price);
        var lineItemId = order.LineItems.First().Id;

        // Act
        order.RemoveLineItem(lineItemId);

        // Assert
        Assert.Empty(order.LineItems);
    }
}