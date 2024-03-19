using DDD.Domain.Orders;

namespace DDD.DataAccess.Contracts;

public interface IOrderSummaryRepository : IBaseRepository<OrderSummary, Guid>
{
    
}