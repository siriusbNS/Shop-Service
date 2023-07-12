using Shops.Entities;
using Shops.Models;
using Shops.Services;
using Xunit;

namespace Shops.Test;

public class ShopManagerTests
{
    private IShopManager _shopManager = new ShopManager();
    [Fact]
    public void AddShop_AddProductsInShopSysList_BuyProduct()
    {
        Shop shop = _shopManager.AddShop("Magnit", "улица Magnit дом 6");
        Product chocolate = _shopManager.AddProduct("Chocolate");
        Product coca_cola = _shopManager.AddProduct("CocaCola");
        int moneyBefore = 100;
        Person person = new Person("Vlad", moneyBefore);
        ProductSupply chocolatee = _shopManager.SupplyProduct(shop, chocolate, 3, 10);
        _shopManager.BuyProduct(person, shop, chocolatee, 10);
        Assert.Equal(0, chocolatee.ProductQuantityInStock);
    }

    [Fact]
    public void PriceSetting_ToEqualMoneyBefore()
    {
        Shop shop = _shopManager.AddShop("Magnit", "улица Magnit дом 6");
        Product chocolate = _shopManager.AddProduct("Chocolate");
        ProductSupply chocolatee = _shopManager.SupplyProduct(shop, chocolate, 8, 10);
        _shopManager.PriceSetting(shop, chocolatee, 100);
        Assert.Equal(100, chocolatee.ProductPrice);
    }

    [Fact]
    public void FindShopWhereProductPriceIsMin()
    {
        Shop magnit = _shopManager.AddShop("Magnit", "улица Magnit дом 6");
        Shop pyterka = _shopManager.AddShop("Pyterka", "улица Magnit дом 7");
        Shop deeksy = _shopManager.AddShop("Deeksy", "улица Magnit дом 8");
        Product chocolate = _shopManager.AddProduct("Chocolate");
        ProductSupply chocolatee = _shopManager.SupplyProduct(magnit, chocolate, 10, 108);
        _shopManager.AddProduct("Chocolate");
        _shopManager.SupplyProduct(pyterka, chocolate, 8, 29);
        _shopManager.AddProduct("Chocolate");
        _shopManager.SupplyProduct(deeksy, chocolate, 12, 19);
        Assert.Equal(pyterka, _shopManager.FindBargainShop(chocolate, 10));
    }

    [Fact]
    public void SupplyProductsToShopBuyProducts_CheckPersonWallet()
    {
        Shop shop = _shopManager.AddShop("Magnit", "улица Magnit дом 6");
        Product chocolate = _shopManager.AddProduct("Chocolate");
        var moneyBefore = 100;
        Person person = new Person("Vlad", moneyBefore);
        ProductSupply chocolatee = _shopManager.SupplyProduct(shop, chocolate, 3, 10);
        int amount = chocolatee.ProductQuantityInStock;
        _shopManager.BuyProduct(person, shop, chocolatee, 10);
        var result = moneyBefore - (chocolatee.ProductPrice * amount);
        Assert.Equal(result, person.PersonWallet);
    }
}