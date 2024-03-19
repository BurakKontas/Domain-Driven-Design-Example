using DDD.Domain.Customers;

namespace DDD.DataAccess.Contracts;

public interface ICustomerRepository : IBaseRepository<Customer, CustomerId>
{
    
}