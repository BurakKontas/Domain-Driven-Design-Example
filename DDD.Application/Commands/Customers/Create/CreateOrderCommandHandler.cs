using DDD.Domain.Customers;
using DDD.Domain.Orders;
using DDD.Service.Contracts;
using MediatR;

namespace DDD.Application.Commands.Customers.Create;

public class CreateOrderCommandHandler(IOrderService orderService, ICustomerService customerService) : IRequestHandler<CreateOrderCommand>
{
    public async Task Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var customer = await customerService.GetByIdAsync(request.CustomerId, cancellationToken);

        var order = Order.Create(customer.Id);

        await orderService.CreateAsync(order, cancellationToken);

        await orderService.SaveChangesAsync(cancellationToken);
    }

}