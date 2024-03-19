using DDD.Domain.Orders;
using MediatR;

namespace DDD.Application.Commands.Orders.RemoveLineItem;

public record RemoveLineItemCommand(OrderId OrderId, LineItemId LineItemId) : IRequest;