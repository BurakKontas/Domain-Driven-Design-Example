using DDD.DataAccess.Contracts;
using DDD.Domain.Customers;
using DDD.Service.Contracts;

namespace DDD.Service.Services;

public class CustomerService(ICustomerRepository repository) : BaseService<Customer, CustomerId>(repository), ICustomerService
{
    
}