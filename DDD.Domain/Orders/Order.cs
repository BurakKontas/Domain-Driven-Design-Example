using DDD.Domain.Customers;
using DDD.Domain.Products;

namespace DDD.Domain.Orders;

public class Order
{
    private readonly HashSet<LineItem> _lineItems = new();

    private Order() { }

    public OrderId Id { get; private set; }

    public CustomerId CustomerId { get; private set; }

    public static Order Create(CustomerId customerId)
    {
        var order = new Order
        {
            Id = new OrderId(Guid.NewGuid()),
            CustomerId = customerId
        };

        return order;
    }

    public void Add(ProductId productId, Money price)
    {

        var lineItemId = new LineItemId(Guid.NewGuid());
        var lineItem = new LineItem(lineItemId, Id, productId, price);
        _lineItems.Add(lineItem);
    }
}