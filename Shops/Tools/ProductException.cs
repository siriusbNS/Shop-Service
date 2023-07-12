namespace Shops.Tools;

internal class ProductException : Exception
{
    public ProductException(string message)
        : base(message) { }
}