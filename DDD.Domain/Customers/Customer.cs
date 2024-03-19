namespace DDD.Domain.Customers;

public class Customer
{
    public CustomerId Id { get; private set; }

    public string Email { get; private set; } = string.Empty;

    public string Name { get; private set; } = string.Empty;
       
    private Customer() { }

    private Customer(CustomerId id, string email, string name)
    {
        Id = id;
        Email = email;
        Name = name;
    }

    public static Customer? Create(string email, string name)
    {
        var customer = new Customer(new CustomerId(Guid.NewGuid()), email, name);
        return customer;
    }

    public void Update(string name)
    {
        Name = name;
    }
}