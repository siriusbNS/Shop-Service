namespace Shops.Tools;

internal class ProductNullException : Exception
{
    public ProductNullException(string message)
        : base(message) { }
}