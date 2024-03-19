using DDD.DataAccess.Contracts;
using DDD.Domain.Customers;
using DDD.Domain.Orders;
using DDD.Service.Services;
using Moq;

namespace DDD.Tests.Unit.Service;

public class OrderServiceTests
{
    [Fact]
    public async Task CreateAsync_Should_Create_Order()
    {
        // Arrange
        var orderRepositoryMock = new Mock<IOrderRepository>();
        var orderService = new OrderService(orderRepositoryMock.Object);
        var order = Order.Create(new CustomerId(Guid.NewGuid()));

        // Act
        await orderService.CreateAsync(order);

        // Assert
        orderRepositoryMock.Verify(repo => repo.AddAsync(order, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_Should_Return_Order_When_Found()
    {
        // Arrange
        var expectedOrder = Order.Create(new CustomerId(Guid.NewGuid()));
        var orderRepositoryMock = new Mock<IOrderRepository>();
        orderRepositoryMock.Setup(repo => repo.GetByIdAsync(expectedOrder.Id, CancellationToken.None)).ReturnsAsync(expectedOrder);
        var orderService = new OrderService(orderRepositoryMock.Object);

        // Act
        var result = await orderService.GetByIdAsync(expectedOrder.Id);

        // Assert
        Assert.Equal(expectedOrder, result);
    }

    [Fact]
    public async Task GetByIdAsync_Should_Return_Null_When_Not_Found()
    {
        // Arrange
        var orderId = new OrderId(Guid.NewGuid());
        var orderRepositoryMock = new Mock<IOrderRepository>();
        var orderService = new OrderService(orderRepositoryMock.Object);

        // Act
        var result = await orderService.GetByIdAsync(orderId);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetAllAsync_Should_Return_All_Orders()
    {
        // Arrange
        var expectedOrders = new List<Order> { Order.Create(new CustomerId(Guid.NewGuid())), Order.Create(new CustomerId(Guid.NewGuid())) };
        var orderRepositoryMock = new Mock<IOrderRepository>();
        orderRepositoryMock.Setup(repo => repo.GetAllAsync(CancellationToken.None)).ReturnsAsync(expectedOrders);
        var orderService = new OrderService(orderRepositoryMock.Object);

        // Act
        var result = await orderService.GetAllAsync();

        // Assert
        Assert.Equal(expectedOrders, result);
    }

    [Fact]
    public async Task UpdateAsync_Should_Update_Order()
    {
        // Arrange
        var order = Order.Create(new CustomerId(Guid.NewGuid()));
        var orderRepositoryMock = new Mock<IOrderRepository>();
        var orderService = new OrderService(orderRepositoryMock.Object);

        // Act
        orderService.UpdateAsync(order);

        // Assert
        orderRepositoryMock.Verify(repo => repo.UpdateAsync(order, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_Should_Delete_Order_When_Found()
    {
        // Arrange
        var order = Order.Create(new CustomerId(Guid.NewGuid()));
        var orderRepositoryMock = new Mock<IOrderRepository>();
        orderRepositoryMock.Setup(repo => repo.GetByIdAsync(order.Id, CancellationToken.None)).ReturnsAsync(order);
        var orderService = new OrderService(orderRepositoryMock.Object);

        // Act
        orderService.DeleteAsync(order.Id);

        // Assert
        orderRepositoryMock.Verify(repo => repo.DeleteAsync(order, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_Should_Not_Delete_Order_When_Not_Found()
    {
        // Arrange
        var orderId = new OrderId(Guid.NewGuid());
        var orderRepositoryMock = new Mock<IOrderRepository>();
        var orderService = new OrderService(orderRepositoryMock.Object);

        // Act
        orderService.DeleteAsync(orderId);

        // Assert
        orderRepositoryMock.Verify(repo => repo.DeleteAsync(It.IsAny<Order>(), CancellationToken.None), Times.Never);
    }
}