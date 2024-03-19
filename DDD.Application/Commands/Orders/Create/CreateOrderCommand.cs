using DDD.Domain.Customers;
using MediatR;

namespace DDD.Application.Commands.Orders.Create;

public record CreateOrderCommand(CustomerId CustomerId) : IRequest;