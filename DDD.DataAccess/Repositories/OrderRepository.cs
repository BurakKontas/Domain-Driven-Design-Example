using DDD.DataAccess.Contracts;
using DDD.Domain.Orders;
using DDD.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace DDD.DataAccess.Repositories;

public class OrderRepository(ApplicationDbContext context) : BaseRepository<Order, OrderId>(context), IOrderRepository
{
    private readonly ApplicationDbContext _context = context;

    public new async Task<Order?> GetByIdAsync(OrderId id, CancellationToken cancellationToken)
    {
        return await _context.Orders
            .Include(o => o.LineItems)
            .SingleOrDefaultAsync(o => o.Id == id, cancellationToken);
    }

    public new async Task<IEnumerable<Order>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Orders
            .Include(o => o.LineItems)
            .ToListAsync(cancellationToken);
    }

    public async Task<Order?> GetByIdAsync(OrderId id, LineItemId lineItemId, CancellationToken cancellationToken = default)
    {
        return await _context.Orders
            .Include(o => o.LineItems.Where(li => li.Id == lineItemId))
            .SingleOrDefaultAsync(o => o.Id == id, cancellationToken);
    }
}