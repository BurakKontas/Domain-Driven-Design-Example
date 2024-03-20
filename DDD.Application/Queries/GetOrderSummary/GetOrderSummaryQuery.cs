using DDD.Domain.Orders;
using MediatR;

namespace DDD.Application.Queries.GetOrderSummary;

public record GetOrderSummaryQuery(Guid OrderId) : IRequest<OrderSummary?>;