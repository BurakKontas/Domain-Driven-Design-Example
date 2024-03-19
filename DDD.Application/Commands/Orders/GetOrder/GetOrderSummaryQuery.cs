using DDD.Domain.Orders;
using MediatR;

namespace DDD.Application.Commands.Orders.GetOrder;

public record GetOrderSummaryQuery(Guid OrderId) : IRequest<OrderSummary?>;