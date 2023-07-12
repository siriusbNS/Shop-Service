using System.Text.RegularExpressions;
using Shops.Tools;

namespace Shops.Models;

public class ProductSupply
{
    public ProductSupply(Product product, int productQuantityInStock, int productPrice)
    {
        if (product is null)
        {
            throw new ProductNullException("Uncorrect name of product.");
        }

        if (productQuantityInStock < 0)
        {
            throw new ProductException("Negative Quantity of products In Stock.");
        }

        if (productPrice < 0)
        {
            throw new ProductException("Uncorrect price of product.");
        }

        Product = product;
        ProductPrice = productPrice;
        ProductQuantityInStock = productQuantityInStock;
    }

    public Product Product { get; }
    public int ProductQuantityInStock { get; set; }
    public int ProductPrice { get; private set; }

    public void ChangePrice(int newPrice)
    {
        if (newPrice <= 0)
        {
            throw new PriceNegativeOrNullException("Price cannot be negative or zero.");
        }

        ProductPrice = newPrice;
    }
}