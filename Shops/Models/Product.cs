using System.Text.RegularExpressions;
using Shops.Tools;

namespace Shops.Models;

public class Product : IEquatable<Product>
{
    private static readonly Regex ProductNameRegex = new (@"^\w*$", RegexOptions.Compiled);

    public Product(string productName)
    {
        if (!ProductNameRegex.IsMatch(productName) || productName is null)
        {
            throw new ProductException("Uncorrect name of product.");
        }

        Name = productName;
        Id = Guid.NewGuid();
    }

    public Guid Id { get; }
    public string Name { get; }

    public bool Equals(Product other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Id.Equals(other.Id);
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Product)obj);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}