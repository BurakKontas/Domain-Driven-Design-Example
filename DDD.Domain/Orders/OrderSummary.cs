namespace DDD.Domain.Orders;

public record OrderSummary(Guid Id, Guid CustomerId, decimal TotalPrice);