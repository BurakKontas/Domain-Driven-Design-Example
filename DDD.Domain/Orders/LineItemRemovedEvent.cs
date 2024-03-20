using DDD.Domain.Primitives;

namespace DDD.Domain.Orders;

public record LineItemRemovedEvent(Guid Id, OrderId OrderId, LineItemId LineItemId) : DomainEvent(Id);