namespace Shops.Tools;

internal class PersonNameException : Exception
{
    public PersonNameException(string message)
        : base(message) { }
}