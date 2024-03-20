using DDD.Domain.Orders;
using DDD.Domain.Primitives;
using MassTransit;
using MediatR;

namespace DDD.Application.Commands.Orders.Create;

internal sealed class OrderCreatedDomainEventHandler(IBus bus) : INotificationHandler<OrderCreatedDomainEvent>
{
    public async Task Handle(OrderCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        await bus.Send(new OrderCreatedEvent(notification.OrderId.Value), cancellationToken);
    }
}