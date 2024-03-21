using System.Reflection;
using DDD.Domain.Customers;
using DDD.Domain.Orders;
using DDD.Domain.Primitives;
using DDD.Domain.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DDD.Infrastructure;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IPublisher publisher) : DbContext(options)
{
    private readonly IPublisher _publisher = publisher;

    public DbSet<Order?> Orders { get; set; }

    public DbSet<Customer?> Customers { get; set; }

    public DbSet<Product?> Products { get; set; }

    public DbSet<OrderSummary> OrderSummaries { get; set; }

    public DbSet<LineItem> LineItems { get; set; }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var domainEvents = ChangeTracker.Entries<Entity>()
            .Select(e => e.Entity)
            .Where(e => e.DomainEvents.Any())
            .SelectMany(e => e.DomainEvents);


        var result = await base.SaveChangesAsync(cancellationToken);

        foreach (var domainEvent in domainEvents)
        {
            await _publisher.Publish(domainEvent, cancellationToken);
        }

        return result;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql();
        //optionsBuilder.UseInMemoryDatabase("DDD");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}