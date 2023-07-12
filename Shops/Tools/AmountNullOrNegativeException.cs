namespace Shops.Tools;

internal class AmountNullOrNegativeException : Exception
{
    public AmountNullOrNegativeException(string message)
        : base(message) { }
}