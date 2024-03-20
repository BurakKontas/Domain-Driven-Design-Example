using DDD.DataAccess.Repositories;
using DDD.Domain.Customers;
using DDD.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;

namespace DDD.Tests.Unit.Repository;

public class CustomerRepositoryTests : IDisposable
{
    private readonly ApplicationDbContext _context;
    private readonly CustomerRepository _repository;


    public CustomerRepositoryTests()
    {
        Mock<IPublisher> publisherMock = new();

        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _context = new ApplicationDbContext(options, publisherMock.Object);
        _repository = new CustomerRepository(_context);
    }

    [Fact]
    public async Task GetByIdAsync_Should_Return_Customer_When_Found()
    {
        // Arrange
        const string name = "John";
        const string email = "test@test.com";
        var customer = Customer.Create(email, name);
        var newCustomer = _context.Customers.Add(customer);
        await _context.SaveChangesAsync();
        var customerId = newCustomer.Entity.Id;

        // Act
        var result = await _repository.GetByIdAsync(customerId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(customerId, result.Id);
    }

    [Fact]
    public async Task GetByIdAsync_Should_Return_Null_When_Not_Found()
    {
        // Arrange
        var customerId = new CustomerId(Guid.NewGuid());

        // Act
        var result = await _repository.GetByIdAsync(customerId);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetAllAsync_Should_Return_All_Customers()
    {
        // Arrange
        var customers = new[]
        {
            Customer.Create("test1@test.com", "Tester1"),
            Customer.Create("test2@test.com", "Tester2"),
            Customer.Create("test3@test.com", "Tester3"),
            Customer.Create("test4@test.com", "Tester4"),
            Customer.Create("test5@test.com", "Tester5"),
        };
        _context.Customers.RemoveRange(_context.Customers); // Remove any existing customers
        _context.Customers.AddRange(customers);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(customers.Length, result.Count());
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}