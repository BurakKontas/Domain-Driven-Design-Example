using DDD.Domain.Orders;

namespace DDD.Service.Contracts;

public interface IOrderService : IBaseService<Order, OrderId>
{
}