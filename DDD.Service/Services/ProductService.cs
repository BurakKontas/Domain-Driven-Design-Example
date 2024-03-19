using DDD.DataAccess.Contracts;
using DDD.Domain.Products;
using DDD.Service.Contracts;

namespace DDD.Service.Services;

public class ProductService(IProductRepository repository) : BaseService<Product, ProductId>(repository), IProductService
{
    
}