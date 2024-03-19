using DDD.Service.Contracts;
using MediatR;

namespace DDD.Application.Commands.Customers.RemoveLineItem;

public class RemoveLineItemCommandHandler(IOrderService orderService) : IRequestHandler<RemoveLineItemCommand>
{
    private readonly IOrderService _orderService = orderService;

    public async Task Handle(RemoveLineItemCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderService.GetByIdAsync(request.OrderId, cancellationToken) 
            ?? throw new InvalidOperationException($"Order with id {request.OrderId} not found");

        order.RemoveLineItem(request.LineItemId);
        _orderService.UpdateAsync(order, cancellationToken);

        await _orderService.SaveChangesAsync(cancellationToken);
    }
}