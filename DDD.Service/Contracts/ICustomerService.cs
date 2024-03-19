using DDD.Domain.Customers;

namespace DDD.Service.Contracts;

public interface ICustomerService : IBaseService<Customer, CustomerId>
{
    
}