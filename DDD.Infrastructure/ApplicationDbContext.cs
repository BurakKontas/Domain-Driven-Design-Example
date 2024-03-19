using System.Reflection;
using DDD.Domain.Customers;
using DDD.Domain.Orders;
using DDD.Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace DDD.Infrastructure;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{

    public DbSet<Order?> Orders { get; set; }

    public DbSet<Customer?> Customers { get; set; }

    public DbSet<Product?> Products { get; set; }

    public DbSet<OrderSummary> OrderSummaries { get; set; }

    public DbSet<LineItem> LineItems { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //optionsBuilder.UseNpgsql();
        optionsBuilder.UseInMemoryDatabase("DDD");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}