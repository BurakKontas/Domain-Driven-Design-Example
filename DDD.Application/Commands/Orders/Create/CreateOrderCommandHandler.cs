using DDD.Domain.Customers;
using DDD.Domain.Orders;
using DDD.Service.Contracts;
using MediatR;

namespace DDD.Application.Commands.Orders.Create;

public class CreateOrderCommandHandler(IOrderService orderService, ICustomerService customerService, IOrderSummaryService orderSummaryService) : IRequestHandler<CreateOrderCommand>
{
    public async Task Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var customer = await customerService.GetByIdAsync(request.CustomerId, cancellationToken);

        if (customer == null) throw new InvalidOperationException($"Customer with id {request.CustomerId} not found");
        
        var order = Order.Create(customer.Id);

        var orderSummary = new OrderSummary(order!.Id.Value, order.CustomerId.Value, 0);
        await orderSummaryService.CreateAsync(orderSummary, cancellationToken); // Do this with a domain event 

        await orderService.CreateAsync(order, cancellationToken);
        
    }

}