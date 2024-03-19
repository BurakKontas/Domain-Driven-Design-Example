using DDD.Domain.Customers;
using DDD.Domain.Orders;
using DDD.Service.Contracts;
using MediatR;

namespace DDD.Application.Commands.Customers.Create;

public class CreateOrderCommandHandler(IOrderService orderService, ICustomerService customerService) : IRequestHandler<CreateOrderCommand>
{
    private readonly IOrderService _orderService = orderService;
    private readonly ICustomerService _customerService = customerService;

    public async Task Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var customer = await _customerService.GetByIdAsync(request.CustomerId, cancellationToken);

        var order = Order.Create(customer.Id);

        await _orderService.CreateAsync(order, cancellationToken);

        await _orderService.SaveChangesAsync(cancellationToken);
    }

}