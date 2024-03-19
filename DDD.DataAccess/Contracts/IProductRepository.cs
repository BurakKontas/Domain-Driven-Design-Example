using DDD.Domain.Products;

namespace DDD.DataAccess.Contracts;

public interface IProductRepository : IBaseRepository<Product, ProductId>
{
    
}