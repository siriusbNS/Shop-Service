namespace Shops.Tools;

internal class PersonWalletException : Exception
{
    public PersonWalletException(string message)
        : base(message) { }
}