using DDD.DataAccess.Contracts;
using DDD.Domain.Orders;
using DDD.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace DDD.DataAccess.Repositories;

public class OrderSummaryRepository(ApplicationDbContext context) : BaseRepository<OrderSummary, Guid>(context), IOrderSummaryRepository
{
    private readonly ApplicationDbContext _context = context;

    public new async Task<OrderSummary?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Set<OrderSummary>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public new async Task<IEnumerable<OrderSummary>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Set<OrderSummary>().AsNoTracking().ToListAsync(cancellationToken);
    }
}