using DDD.DataAccess.Contracts;
using DDD.Domain.Customers;
using DDD.Infrastructure;

namespace DDD.DataAccess.Repositories;

public class CustomerRepository(ApplicationDbContext context) : BaseRepository<Customer, CustomerId>(context), ICustomerRepository
{
}