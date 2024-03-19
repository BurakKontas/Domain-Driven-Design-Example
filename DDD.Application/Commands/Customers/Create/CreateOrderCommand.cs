using DDD.Domain.Customers;
using MediatR;

namespace DDD.Application.Commands.Customers.Create;

public record CreateOrderCommand(CustomerId CustomerId) : IRequest;