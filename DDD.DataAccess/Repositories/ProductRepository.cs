using DDD.DataAccess.Contracts;
using DDD.Domain.Products;
using DDD.Infrastructure;

namespace DDD.DataAccess.Repositories;

public class ProductRepository(ApplicationDbContext context) : BaseRepository<Product, ProductId>(context), IProductRepository
{
}