using DDD.Domain.Orders;
using DDD.IntegrationEvents;
using MassTransit;
using MediatR;

namespace DDD.Application.Commands.Orders.Create;

internal sealed class OrderCreatedDomainEventHandler(ISendEndpointProvider bus) : INotificationHandler<OrderCreatedDomainEvent>
{
    public async Task Handle(OrderCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        await bus.Send(new OrderCreatedIntegrationEvent(notification.OrderId.Value), cancellationToken);
    }
}