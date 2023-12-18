using Adressbok.Interfaces;
using Adressbok.ClassLibrary.Interfaces;
using System.Text.RegularExpressions;

namespace Adressbok.Services;

public class ContactService : IContactService
{
    /// <summary>
    /// Metod för att sätta en kontakts egenskap, med validering vid tomt fält.
    /// </summary>
    /// <param name="contact">Tar emot ett contact-objekt av typen IContact</param>
    /// <param name="getProperty">Funktion som kallas "Function delegate" som hämtar värdet av egenskapen från contact-objektet</param>
    /// <param name="setProperty">Funktion som kallas "Action delegate" som tar emot en sträng för att ställa in värdet av egenskapen på contact-objektet</param>
    /// <param name="message">Meddelande som skrivs ut till användaren, ombeder dem att mata in en egenskap till kontakten.</param>
    /// <param name="errorMessage">Felmeddelande om användaren gör en ogiltig inmatning.</param>
    public void SetContactProperties(IContact contact, Func<string> getProperty, Action<string> setProperty, string message, string errorMessage)
    {
        while (true)
        {
            Console.Write($"{message}");
            var input = Console.ReadLine()!;
            if (!string.IsNullOrEmpty(input))
            {
                setProperty(input);
                break;
            }
            else
            {
                Console.WriteLine($"{errorMessage}\n");
            }
        }
    }



    /// <summary>
    /// Metod för att sätta en kontakts egenskap, med specifik Regex-validering.
    /// </summary>
    /// <param name="contact">Tar emot ett contact-objekt av typen IContact</param>
    /// <param name="getProperty">Funktion som kallas "Function delegate" som hämtar värdet av egenskapen från contact-objektet</param>
    /// <param name="setProperty">Funktion som kallas "Action delegate" som tar emot en sträng för att ställa in värdet av egenskapen på contact-objektet</param>
    /// <param name="regexPattern">Tar emot ett valfritt Regex-mönster för validering av diverse egenskaper</param>
    /// <param name="message">Meddelande som skrivs ut till användaren, ombeder dem att mata in en egenskap till kontakten.</param>
    /// <param name="errorMessage">Felmeddelande om användaren gör en ogiltig inmatning.</param>
    /// <param name="emptyField">Felmeddelande om användaren lämnar tomt fält</param>
    public void SetPropertyWithRegex(IContact contact, Func<string> getProperty, Action<string> setProperty, string regexPattern, string message, string errorMessage, string emptyField)
    {
        while (true)
        {
            string _regex = regexPattern;
            Console.Write(message);

            var input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input))
            {
                Regex regex = new(_regex);
                bool isMatch = regex.IsMatch(input);
                if (isMatch)
                {
                    setProperty(input);
                    break;
                }
                else
                {
                    Console.WriteLine(errorMessage);
                }
            }
            else
            {
                Console.WriteLine(emptyField);
            }
        }
    }



    /// <summary>
    /// Metod för att sätta en kontakts e-post, med specifik Regex-validering
    /// och kontroll för att se om samma e-post redan förekommer i adressboken.
    /// Ger felmeddelande om en kontakt med samma e-post redan finns i boken.
    /// </summary>
    /// <param name="contactList">Tar emot en lista av typen IContact</param>
    /// <param name="contact">Tar emot ett contact-objekt av typen IContact</param>
    /// <param name="getProperty">Funktion som kallas "Function delegate" som hämtar värdet av egenskapen från contact-objektet</param>
    /// <param name="setProperty">Funktion som kallas "Action delegate" som tar emot en sträng för att ställa in värdet av egenskapen på contact-objektet</param>
    /// <param name="regexPattern">Tar emot ett valfritt Regex-mönster för validering av diverse egenskaper</param>
    /// <param name="message">Meddelande som skrivs ut till användaren, ombeder dem att mata in en egenskap till kontakten.</param>
    /// <param name="errorMessage">Felmeddelande om användaren gör en ogiltig inmatning.</param>
    /// <param name="emptyField">Felmeddelande om användaren lämnar tomt fält</param>
    public void SetEmailWithRegex(List<IContact> contactList, IContact contact, Func<string> getProperty, Action<string> setProperty, string regexPattern, string message, string errorMessage, string emptyField)
    {
        while (true)
        {
            string _regex = regexPattern;
            Console.Write(message);

            var input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input))
            {
                Regex regex = new(_regex);
                bool isMatch = regex.IsMatch(input);
                if (isMatch)
                {
                    contact.Email = input;

                    if (!contactList.Any(x => x.Email.ToLower() == contact.Email.ToLower()))
                    {
                        setProperty(input); 
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"{contact.Email} finns redan i adressboken.");
                    }
                }
                else
                {
                    Console.WriteLine(errorMessage);
                }
            }
            else
            {
                Console.WriteLine(emptyField);
            }
        }
    }
}
