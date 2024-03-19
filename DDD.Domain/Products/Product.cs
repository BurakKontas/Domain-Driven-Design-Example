namespace DDD.Domain.Products;

public class Product
{
    public ProductId Id { get; private set; }

    public string Name { get; private set; } = string.Empty;
        
    public Money Price { get; private set; }

    public Sku Sku { get; private set; }

    private Product() { }

    private Product(ProductId id, string name, Money price, Sku sku)
    {
        Id = id;
        Name = name;
        Price = price;
        Sku = sku;
    }

    public static Product Create(string name, Money price, Sku sku)
    {
        var product = new Product(new ProductId(Guid.NewGuid()), name, price, sku);
        return product;
    }

    public void Update(string name, Money price, Sku sku)
    {
        Name = name;
        Price = price;
        Sku = sku;
    }
}