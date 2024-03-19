using DDD.DataAccess.Repositories;
using DDD.Domain.Customers;
using DDD.Domain.Orders;
using DDD.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace DDD.Tests.Unit.Repository;

public class OrderRepositoryTests : IDisposable
{
    private readonly ApplicationDbContext _context;
    private readonly OrderRepository _repository;

    public OrderRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _context = new ApplicationDbContext(options);
        _repository = new OrderRepository(_context);
    }

    [Fact]
    public async Task GetByIdAsync_Should_Return_Order_When_Found()
    {
        // Arrange
        var customerId = new CustomerId(Guid.NewGuid());
        var order = Order.Create(customerId);
        var newOrder = _context.Orders.Add(order);
        await _context.SaveChangesAsync();
        var orderId = newOrder.Entity.Id;

        // Act
        var result = await _repository.GetByIdAsync(orderId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(orderId, result.Id);
    }

    [Fact]
    public async Task GetByIdAsync_Should_Return_Null_When_Not_Found()
    {
        // Arrange
        var orderId = new OrderId(Guid.NewGuid());

        // Act
        var result = await _repository.GetByIdAsync(orderId);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetAllAsync_Should_Return_All_Orders()
    {
        // Arrange
        var orders = new[]
        {
            Order.Create(new CustomerId(Guid.NewGuid())),        
            Order.Create(new CustomerId(Guid.NewGuid())),        
            Order.Create(new CustomerId(Guid.NewGuid())),        
            Order.Create(new CustomerId(Guid.NewGuid())),        
            Order.Create(new CustomerId(Guid.NewGuid())),        
            Order.Create(new CustomerId(Guid.NewGuid())),        
        };

        _context.Orders.RemoveRange(_context.Orders); // Remove any existing orders

        _context.Orders.AddRange(orders);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(orders.Length, result.Count());
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}