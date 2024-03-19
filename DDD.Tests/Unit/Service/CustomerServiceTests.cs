using DDD.DataAccess.Contracts;
using DDD.Domain.Customers;
using DDD.Service.Services;
using Moq;

namespace DDD.Tests.Unit.Service;

public class CustomerServiceTests
{
    [Fact]
    public async Task CreateAsync_Should_Add_Customer()
    {
        // Arrange
        var customerRepositoryMock = new Mock<ICustomerRepository>();
        var customerService = new CustomerService(customerRepositoryMock.Object);
        var customer = Customer.Create("test@test.com", "tester");

        // Act
        await customerService.CreateAsync(customer);

        // Assert
        customerRepositoryMock.Verify(repo => repo.AddAsync(customer, default), Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_Should_Return_Customer_When_Found()
    {
        // Arrange
        var expectedCustomer = Customer.Create("test@test.com", "tester");
        var customerRepositoryMock = new Mock<ICustomerRepository>();
        customerRepositoryMock.Setup(repo => repo.GetByIdAsync(expectedCustomer.Id, default))
            .ReturnsAsync(expectedCustomer);
        var customerService = new CustomerService(customerRepositoryMock.Object);

        // Act
        var result = await customerService.GetByIdAsync(expectedCustomer.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedCustomer, result);
    }

    [Fact]
    public async Task GetByIdAsync_Should_Return_Null_When_Not_Found()
    {
        // Arrange
        var customerId = new CustomerId(Guid.NewGuid());
        var customerRepositoryMock = new Mock<ICustomerRepository>();
        customerRepositoryMock.Setup(repo => repo.GetByIdAsync(customerId, default)).ReturnsAsync((Customer)null);
        var customerService = new CustomerService(customerRepositoryMock.Object);

        // Act
        var result = await customerService.GetByIdAsync(customerId);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetAllAsync_Should_Return_All_Customers()
    {
        // Arrange
        var customers = Enumerable.Range(1, 5).Select(i => Customer.Create($"test{i}@test.com", $"Tester{i}"));
        var customerRepositoryMock = new Mock<ICustomerRepository>();
        customerRepositoryMock.Setup(repo => repo.GetAllAsync(default)).ReturnsAsync(customers);
        var customerService = new CustomerService(customerRepositoryMock.Object);

        // Act
        var result = await customerService.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(customers.ToList().Count, result.ToList().Count);
    }

    [Fact]
    public async Task UpdateAsync_Should_Update_Customer()
    {
        // Arrange
        var customer = Customer.Create("test@test.com", "tester");
        var customerRepositoryMock = new Mock<ICustomerRepository>();
        var customerService = new CustomerService(customerRepositoryMock.Object);

        // Act
        await customerService.UpdateAsync(customer);

        // Assert
        customerRepositoryMock.Verify(repo => repo.UpdateAsync(customer, default), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_Should_Delete_Customer_When_Found()
    {
        // Arrange
        var expectedCustomer = Customer.Create("test@test.com", "tester");
        var customerRepositoryMock = new Mock<ICustomerRepository>();
        customerRepositoryMock.Setup(repo => repo.GetByIdAsync(expectedCustomer.Id, default))
            .ReturnsAsync(expectedCustomer);
        var customerService = new CustomerService(customerRepositoryMock.Object);

        // Act
        await customerService.DeleteAsync(expectedCustomer.Id);

        // Assert
        customerRepositoryMock.Verify(repo => repo.DeleteAsync(expectedCustomer, default), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_Should_Not_Delete_Customer_When_Not_Found()
    {
        // Arrange
        var customerId = new CustomerId(Guid.NewGuid());
        var customerRepositoryMock = new Mock<ICustomerRepository>();
        customerRepositoryMock.Setup(repo => repo.GetByIdAsync(customerId, default)).ReturnsAsync((Customer)null);
        var customerService = new CustomerService(customerRepositoryMock.Object);

        // Act
        await customerService.DeleteAsync(customerId);

        // Assert
        customerRepositoryMock.Verify(repo => repo.DeleteAsync(It.IsAny<Customer>(), default), Times.Never);
    }
}