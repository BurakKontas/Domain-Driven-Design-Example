using DDD.Domain.Orders;
using DDD.Service.Contracts;
using MediatR;

namespace DDD.Application.Commands.Orders.RemoveLineItem;

public class RemoveLineItemCommandHandler(IOrderService orderService) : IRequestHandler<RemoveLineItemCommand>
{
    public async Task Handle(RemoveLineItemCommand request, CancellationToken cancellationToken)
    {
        var order = await orderService.GetByIdAsync(request.OrderId, cancellationToken)
                    ?? throw new InvalidOperationException($"Order with id {request.OrderId} not found");

        order.RemoveLineItem(request.LineItemId);
        await orderService.UpdateAsync(order, cancellationToken);
    }
}