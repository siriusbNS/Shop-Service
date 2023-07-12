using System.Text.RegularExpressions;
using Shops.Models;
using Shops.Tools;

namespace Shops.Entities;

public class Shop
{
    private static readonly Regex ShopNameRegex = new (@"^\w*$", RegexOptions.Compiled);
    private static readonly Regex ShopAdressRegex = new (@"^улица(\s\w*\s)дом(\s\d*)$", RegexOptions.Compiled);

    public Shop(int shopId, string shopAdress, string shopName)
    {
        if (!ShopNameRegex.IsMatch(shopName) || string.IsNullOrEmpty(shopName))
        {
            throw new ShopNameException("Uncorrect name of shop.");
        }

        if (shopId <= 0)
        {
            throw new IdShopException("Uncorrect id of shop.");
        }

        if (!ShopAdressRegex.IsMatch(shopAdress) || shopAdress is null)
        {
            throw new AddresException("Uncorrect adress of shop.");
        }

        ShopAdress = shopAdress;
        ShopId = shopId;
        ShopName = shopName;
        ListOfProducts = new List<ProductSupply>();
    }

    public int ShopId { get; private set; }
    public string ShopName { get; private set; }
    public string ShopAdress { get; private set; }
    private List<ProductSupply> ListOfProducts { get; set; }
    public void AddProduct(ProductSupply product)
    {
        if (product == null)
            throw new ProductNullException("Product is nullable.");
        ListOfProducts.Add(product);
    }

    public bool FindPriceProduct(int price, Product product)
    {
        var currentProduct = ListOfProducts
            .FirstOrDefault(x => x.ProductPrice == price && x.Product == product); // Сам Product сравнивается по GUI//
        if (currentProduct is null)
        {
            return false;
        }

        return true;
    }

    public IReadOnlyList<ProductSupply> GetProducts() => ListOfProducts;
}