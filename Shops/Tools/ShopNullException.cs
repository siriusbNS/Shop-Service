namespace Shops.Tools;

internal class ShopNullException : Exception
{
    public ShopNullException(string message)
        : base(message) { }
}