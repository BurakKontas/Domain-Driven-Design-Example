using DDD.Domain.Products;

namespace DDD.Service.Contracts;

public interface IProductService : IBaseService<Product, ProductId>
{
    
}