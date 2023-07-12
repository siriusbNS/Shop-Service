namespace Shops.Tools;

internal class ShopNameException : Exception
{
    public ShopNameException(string message)
        : base(message) { }
}