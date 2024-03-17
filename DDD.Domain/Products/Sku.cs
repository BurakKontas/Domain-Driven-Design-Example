﻿namespace DDD.Domain.Products;

// Stock Keeping Unit
public record Sku
{
    private const int DefaultLength = 15;

    private Sku(string value) => Value = value;

    public string Value { get; init; }

    public static Sku? Create(string value)
    {
        if(string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        if(value.Length != DefaultLength)
        {
            return null;
        }

        return new Sku(value);
    }
}