using Shops.Entities;
using Shops.Models;
namespace Shops.Services;

public interface IShopManager
{
    Shop AddShop(string name, string adress);
    Product AddProduct(string nameOfProduct);
    ProductSupply SupplyProduct(Shop shop, Product product, int price, int amount);
    void PriceSetting(Shop shop, ProductSupply product, int newPrice);
    void BuyProduct(Person person, Shop shop, ProductSupply product, int amount);
    Shop FindBargainShop(Product product, int amount);
}