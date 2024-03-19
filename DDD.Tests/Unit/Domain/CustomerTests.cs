using DDD.Domain.Customers;

namespace DDD.Tests.Unit.Domain;

public class CustomerTests
{
    [Fact]
    public void CreateCustomer_Should_Create_Customer_With_Given_Email_And_Name()
    {
        // Arrange
        const string email = "example@example.com";
        const string name = "John Doe";

        // Act
        var customer = Customer.Create(email, name);

        // Assert
        Assert.NotNull(customer);
        Assert.Equal(email, customer.Email);
        Assert.Equal(name, customer.Name);
        Assert.NotEqual(default, customer.Id);
    }

    [Fact]
    public void Update_Should_Update_Customer_Name()
    {
        // Arrange
        const string email = "example@example.com";
        const string name = "John Doe";
        const string newName = "Jane Doe";
        var customer = Customer.Create(email, name);

        // Act
        customer.Update(newName);

        // Assert
        Assert.Equal(newName, customer.Name);
    }
}