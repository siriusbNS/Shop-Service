namespace Shops.Tools;

internal class PriceNegativeOrNullException : Exception
{
    public PriceNegativeOrNullException(string message)
        : base(message) { }
}