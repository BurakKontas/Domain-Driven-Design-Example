using DDD.Domain.Orders;
using DDD.Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDD.Infrastructure.Configurations;

public class LineItemConfiguration : IEntityTypeConfiguration<LineItem>
{
    public void Configure(EntityTypeBuilder<LineItem> builder)
    {
        builder.HasKey(li => li.Id);

        builder.Property(li => li.Id)
            .HasConversion(lineItemId => lineItemId.Value, value => new LineItemId(value));

        builder.HasOne<Product>()
            .WithMany()
            .HasForeignKey(li => li.ProductId);

        builder.OwnsOne(li => li.Price, priceBuilder =>
        {
            priceBuilder.Property(m => m.Currency).HasMaxLength(3);
        });
    }
}