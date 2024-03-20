using DDD.Domain.Orders;
using DDD.Service.Contracts;
using MediatR;

namespace DDD.Application.Queries.GetOrderSummary;

internal sealed class GetOrderSummaryQueryHandler(IOrderSummaryService orderSummaryService) : IRequestHandler<GetOrderSummaryQuery, OrderSummary?>
{
    public async Task<OrderSummary?> Handle(GetOrderSummaryQuery request, CancellationToken cancellationToken)
    {
        return await orderSummaryService.GetByIdAsync(request.OrderId, cancellationToken)
                   ?? throw new InvalidOperationException($"Order with id {request.OrderId} not found");
    }
}