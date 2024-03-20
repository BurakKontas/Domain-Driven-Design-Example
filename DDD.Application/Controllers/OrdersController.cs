using DDD.Application.Commands.Orders.Create;
using DDD.Application.Queries.GetOrderSummary;
using DDD.Domain.Customers;
using DDD.Domain.Orders;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DDD.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController(ISender mediator) : ControllerBase
    {
        [HttpPost("/create")]
        public async Task<IActionResult> Get([FromBody] CustomerId customerId)
        {
            await mediator.Send(new CreateOrderCommand(customerId));
            return Ok("Order Created");
        }

        [HttpGet("/get/{id:guid}/summary")]
        public async Task<IActionResult> GetAll([FromRoute] Guid id)
        {
            var orders = await mediator.Send(new GetOrderSummaryQuery(id));
            return Ok(orders);
        }
    }
}
