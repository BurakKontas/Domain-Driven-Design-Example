using DDD.Domain.Products;

namespace DDD.Domain.Orders;

public class LineItem
{
    internal LineItem(LineItemId id, OrderId orderId, ProductId productId, Money price)
    {
        Id = id;
        OrderId = orderId;
        ProductId = productId;
        Price = price;
    }

    internal LineItem(OrderId orderId, ProductId productId, Money price)
        : this(new LineItemId(Guid.NewGuid()), orderId, productId, price) { }

    internal LineItem() { }


    public LineItemId Id { get; private set; }

    public OrderId OrderId { get; private set; }

    public ProductId ProductId { get; private set; }

    public Money Price { get; private set; }
}