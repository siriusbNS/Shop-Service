using System.Text.RegularExpressions;
using Shops.Tools;

namespace Shops.Entities;

public class Person
{
    private static readonly Regex PersonNameRegex = new (@"^\w*$", RegexOptions.Compiled);

    public Person(string personName, int personWallet)
    {
        if (!PersonNameRegex.IsMatch(personName) || string.IsNullOrEmpty(personName))
        {
            throw new PersonNameException("Uncorrect name of person.");
        }

        if (personWallet < 0)
        {
            throw new PersonWalletException("Negative person wallet.");
        }

        PersonName = personName;
        PersonWallet = personWallet;
    }

    public string PersonName { get; private set; }
    public int PersonWallet { get; set; }
}