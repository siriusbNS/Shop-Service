using Shops.Entities;
using Shops.Models;
using Shops.Tools;

namespace Shops.Services;

public class ShopManager : IShopManager
{
    private int id = 1;
    public ShopManager()
    {
        ShopList = new List<Shop>();
        ProductList = new List<Product>();
    }

    private List<Shop> ShopList { get; set; }
    private List<Product> ProductList { get; set; }
    public Shop AddShop(string name, string adress)
    {
        if (name is null)
        {
            throw new ShopNameException("name is nullable.");
        }

        if (adress is null)
        {
            throw new AddresException("adress is nullable.");
        }

        Shop shop = new Shop(id, adress, name);
        id++;
        ShopList.Add(shop);
        return shop;
    }

    public Product AddProduct(string nameOfProduct)
    {
        Product product = new Product(nameOfProduct);
        ProductList.Add(product);
        return product;
    }

    public ProductSupply SupplyProduct(Shop shop, Product product, int price, int amount)
    {
        var currentShop = ShopList.FirstOrDefault(x => x == shop);
        if (shop is null)
        {
            throw new ShopNullException("There is no this shop.");
        }

        if (currentShop is null)
        {
            throw new ShopNullException("There is no this shop.");
        }

        var currentProduct = currentShop.GetProducts()
            .FirstOrDefault(currentProduct => currentProduct.Product.Equals(product));
        if (currentProduct is null)
        {
           ProductSupply currentProductNew = new ProductSupply(product, amount, price);
           currentShop.AddProduct(currentProductNew);
           return currentProductNew;
        }

        currentProduct.ProductQuantityInStock += amount;
        currentProduct.ChangePrice(price);
        return currentProduct;
    }

    public void PriceSetting(Shop shop, ProductSupply product, int newPrice)
    {
        var currentShop = ShopList.FirstOrDefault(x => x == shop);
        if (shop is null)
        {
            throw new ShopNullException("There is no this shop.");
        }

        var currentProduct = currentShop.GetProducts()
            .FirstOrDefault(currentProduct => currentProduct == product);
        if (currentProduct is null)
        {
            throw new ProductNullException("There is no this product in this shop.");
        }

        currentProduct.ChangePrice(newPrice);
    }

    public void BuyProduct(Person person, Shop shop, ProductSupply product, int amount)
    {
        var currentShop = ShopList.FirstOrDefault(x => x == shop);
        if (shop is null)
        {
            throw new ShopNullException("There is no this shop.");
        }

        var currentProduct = currentShop.GetProducts()
            .FirstOrDefault(currentProduct => currentProduct == product);
        if (currentProduct is null)
        {
            throw new ProductNullException("There is no this product in this shop.");
        }

        if (currentProduct.ProductQuantityInStock < amount)
        {
            throw new AmountNullOrNegativeException("Not enough products in shop.");
        }

        if (person.PersonWallet < (currentProduct.ProductPrice * amount))
        {
            throw new PersonWalletException("Not enough money for a person");
        }

        person.PersonWallet = person.PersonWallet - (currentProduct.ProductPrice * amount);
        currentProduct.ProductQuantityInStock -= amount;
    }

    public Shop FindBargainShop(Product product, int amount)
    {
        var currentShops = ShopList
            .Where(shop => shop.GetProducts()
                .FirstOrDefault(currentProduct => currentProduct.ProductQuantityInStock >= amount).Product == product)
            .ToList();
        if (currentShops.Count == 0)
        {
            throw new ShopNullException("There are no shops.");
        }

        var min = currentShops
            .SelectMany(x => x.GetProducts())
            .Min(minPrice => minPrice.ProductPrice);
        if (min <= 0)
            throw new Exception("min is nullable.");
        var currentShop = currentShops
            .FirstOrDefault(shop => shop.FindPriceProduct(min, product) == true);
        if (currentShop is null)
            throw new ShopNullException("There are no shops with that product.");
        return currentShop;
    }
}