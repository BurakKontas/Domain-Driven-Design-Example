using DDD.Domain.Orders;

namespace DDD.Service.Contracts;

public interface IOrderSummaryService : IBaseService<OrderSummary, Guid>
{
    
}