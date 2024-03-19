﻿using DDD.Domain.Customers;
using DDD.Domain.Products;

namespace DDD.Domain.Orders;

public class Order
{
    private readonly HashSet<LineItem> _lineItems = [];

    private Order() { }

    public OrderId Id { get; private set; }

    public CustomerId CustomerId { get; private set; }

    public IReadOnlyCollection<LineItem> LineItems => _lineItems.ToList();

    public static Order Create(CustomerId customerId)
    {
        var order = new Order
        {
            Id = new OrderId(Guid.NewGuid()),
            CustomerId = customerId
        };

        return order;
    }

    public void AddLineItem(ProductId productId, Money price)
    {
        var lineItem = LineItem.Create(Id, productId, price);
        _lineItems.Add(lineItem);
    }
}