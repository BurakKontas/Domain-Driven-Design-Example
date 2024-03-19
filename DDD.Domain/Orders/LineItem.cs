using DDD.Domain.Products;

namespace DDD.Domain.Orders;

public class LineItem
{
    private LineItem(LineItemId id, OrderId orderId, ProductId productId, Money price)
    {
        Id = id;
        OrderId = orderId;
        ProductId = productId;
        Price = price;
    }

    internal LineItem() { }

    public static LineItem Create(OrderId orderId, ProductId productId, Money price)
    {
        var id = new LineItemId(Guid.NewGuid());
        return new LineItem(id, orderId, productId, price);
    }

    public LineItemId Id { get; private set; }

    public OrderId OrderId { get; private set; }

    public ProductId ProductId { get; private set; }

    public Money Price { get; private set; }
}