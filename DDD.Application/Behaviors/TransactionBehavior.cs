using DDD.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DDD.Application.Behaviors;

public class TransactionBehavior<TRequest, TResponse>(ApplicationDbContext context) : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));


    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            var response = await next();
            await _context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
            return response;
        }
        catch (Exception)
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}

