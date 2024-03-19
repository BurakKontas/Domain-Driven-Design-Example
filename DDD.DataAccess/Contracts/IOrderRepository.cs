using DDD.Domain.Orders;

namespace DDD.DataAccess.Contracts;

public interface IOrderRepository : IBaseRepository<Order, OrderId>
{
    Task<Order?> GetByIdAsync(OrderId id, LineItemId lineItemId, CancellationToken cancellationToken = default);
}