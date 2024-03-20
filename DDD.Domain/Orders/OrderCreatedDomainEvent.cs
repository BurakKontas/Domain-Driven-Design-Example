using DDD.Domain.Primitives;

namespace DDD.Domain.Orders;

public record OrderCreatedDomainEvent(Guid Id, OrderId OrderId) : DomainEvent(Id);