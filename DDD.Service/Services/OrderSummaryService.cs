using DDD.DataAccess.Contracts;
using DDD.Domain.Orders;
using DDD.Service.Contracts;

namespace DDD.Service.Services;

public class OrderSummaryService(IOrderSummaryRepository repository) : BaseService<OrderSummary, Guid>(repository), IOrderSummaryService
{
    
}