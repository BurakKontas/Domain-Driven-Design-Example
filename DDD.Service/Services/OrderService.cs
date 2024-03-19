using DDD.DataAccess.Contracts;
using DDD.Domain.Customers;
using DDD.Domain.Orders;
using DDD.Domain.Products;
using DDD.Service.Contracts;

namespace DDD.Service.Services;

public class OrderService(IOrderRepository repository) : BaseService<Order, OrderId>(repository), IOrderService
{
}

