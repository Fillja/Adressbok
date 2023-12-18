using Adressbok.Interfaces;
using Adressbok.ClassLibrary.Interfaces;
using Adressbok.ClassLibrary.Models;
using System.Text.RegularExpressions;

namespace Adressbok.Services;

public class MenuService(IListService listService, IContactService contactService) : IMenuService
{
    private readonly IListService _listService = listService;
    private readonly IContactService _contactService = contactService;

    /// <summary>
    /// Visar en meny i evighetsloop med flertal val i en switch case. 
    /// Användar-input valideras och leder sedan till separata metoder eller stänger ner programmet (avslutar loopen).
    /// </summary>
    public void ShowMainMenu()
    {

        while(true)
        {
            Console.Clear();
            Console.WriteLine("### ADRESSBOK ###\n");
            Console.WriteLine("Gör ett av följande val:\n");
            Console.WriteLine("1.\t Lägg till en ny kontakt i adressboken.");
            Console.WriteLine("2.\t Visa alla kontakter i adressboken.");
            Console.WriteLine("3.\t Visa en specifik kontakt i adressboken.");
            Console.WriteLine("4.\t Ta bort en kontakt från adressboken.");
            Console.WriteLine("0.\t Avsluta applikationen.");
            Console.WriteLine();

            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    ShowAddMenu();
                    break;

                case "2":
                    ShowAllContacts(); 
                    break;

                case "3":
                    ShowSingleContact();
                    break;

                case "4":
                    ShowRemoveContact();
                    break;

                case "0":
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("Ogiltig inmatning, tryck valfri knapp för att försöka igen.");
                    Console.ReadKey();
                    break;
            }
        }
    }

    /// <summary>
    /// Metod för att lägga till kontakt i listan. Varje egenskap hos kontakt läggs till med hjälp av separat metod med egen validering och loop.
    /// Detta sker i evighetsloop tills användaren är färdig med att lägga till kontakter i adressboken.
    /// </summary>
    public void ShowAddMenu()
    {
        while(true)
        {
            IContact contact = new Contact();
            Console.Clear();

            Console.WriteLine("### LÄGG TILL KONTAKT ###\n");
            _contactService.SetContactProperties(contact, () => contact.FirstName, value => contact.FirstName = value, "Ange förnamn: ", "Förnamn får inte vara tomt!");
            _contactService.SetContactProperties(contact, () => contact.LastName, value => contact.LastName = value, "Ange efternamn: ", "Efternamn får inte vara tomt!");
            _contactService.SetEmailWithRegex(_listService.GetAllContactsFromList().ToList(), contact, () => contact.Email, value => contact.Email = value, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", "Ange e-postadress: ", "E-postadressen måste vara giltig!", "E-postadressen får inte vara tom!");
            _contactService.SetPropertyWithRegex(contact, () => contact.Phone, value => contact.Phone = value, @"^\d{10}$", "Ange telefonnummer: ", "Telefonnumret måste bestå av 10 siffror!", "Telefonnummret får inte vara tomt!");
            _contactService.SetContactProperties(contact, () => contact.City, value => contact.City = value, "Ange hemstad: ", "Hemstaden får inte vara tom!");
            _contactService.SetContactProperties(contact, () => contact.Street, value => contact.Street = value, "Ange gatuadress: ", "Gatuadressen får inte vara tom!");
            _contactService.SetPropertyWithRegex(contact, () => contact.PostalCode, value => contact.PostalCode = value, @"^\d{5}$", "Ange postnummer (5 siffror): ", "Postnumret måste bestå av 5 siffror.", "Postnumret får inte vara tomt!");

            if (_listService.AddContactToList(contact))
                Console.WriteLine("\nKontakt tillagd!");
            else
                Console.WriteLine("\nNågot gick fel, prova igen!");

            ReturnToMainMenu("\nTryck på valfri knapp för att lägga till en till kontakt, eller tryck på 0 för att återgå till huvudmenyn.");
        }
    }

    /// <summary>
    /// Metod för att visa alla kontakter i listan med hjälp av foreach() loop.
    /// Metod från ListService används för funktionalitet.
    /// Formateras på ett snyggt och läsbart vis.
    /// </summary>
    public void ShowAllContacts()
    {
        Console.Clear();
        foreach (var contact in _listService.GetAllContactsFromList())
        {
            Console.WriteLine("----------------------");
            Console.WriteLine($"Förnamn:\t{contact.FirstName}");
            Console.WriteLine($"Efternamn:\t{contact.LastName}");
            Console.WriteLine($"E-postadress:\t{contact.Email}");
            Console.WriteLine($"Telefonnummer:\t{contact.Phone}");
            Console.WriteLine($"Hemstad:\t{contact.City}");
            Console.WriteLine($"Gatuadress:\t{contact.Street}");
            Console.WriteLine($"Postnummer:\t{contact.PostalCode}");
            Console.WriteLine("----------------------\n\n");
        }
        Console.WriteLine("Tryck på valfri knapp för att återgå till huvudmenyn.");
        Console.ReadKey();
    }

    /// <summary>
    /// Metod för att visa enskild kontakt. Alla kontakter listas upp i foreach loop och tilldelas ett ID. 
    /// Metoder från ListService används för funktionalitet.
    /// Användarens input väljer vilken kontakt som ska granskas i detalj utefter ID. Använder metod från ListService.
    /// Input valideras och skriver sedan ut kontakt om valid.
    /// </summary>
    public void ShowSingleContact()
    {
        while (true)
        {
            Console.Clear();
            int i = 1;

            foreach (var contact in _listService.GetAllContactsFromList())
            {
                contact.ID = i;
                Console.WriteLine("----------------------\n");
                Console.WriteLine($"### Kontakt {contact.ID} ###");
                Console.WriteLine($"Namn:\t{contact.FirstName} {contact.LastName}");
                Console.WriteLine($"Tele:\t{contact.Phone}\n");
                Console.WriteLine("----------------------\n");
                i++;
            }

            Console.Write("Skriv siffran på den kontakt du vill granska i detalj: ");
            var option = Console.ReadLine();
            if (int.TryParse(option, out int result))
            {
                IContact chosenContact = _listService.GetSpecificContactFromList(result);
                if(chosenContact != null)
                {
                    Console.Clear();
                    Console.WriteLine("----------------------");
                    Console.WriteLine($"Förnamn:\t{chosenContact.FirstName}");
                    Console.WriteLine($"Efternamn:\t{chosenContact.LastName}");
                    Console.WriteLine($"E-postadress:\t{chosenContact.Email}");
                    Console.WriteLine($"Telefonnummer:\t{chosenContact.Phone}");
                    Console.WriteLine($"Hemstad:\t{chosenContact.City}");
                    Console.WriteLine($"Gatuadress:\t{chosenContact.Street}");
                    Console.WriteLine($"Postnummer:\t{chosenContact.PostalCode}");
                    Console.WriteLine("----------------------\n\n");
                    ReturnToMainMenu("Tryck på valfri knapp för att granska en ny kontakt, eller tryck på 0 för att återgå till huvudmenyn.");
                }
                else
                {
                    ReturnToMainMenu("Den kontakten hittades inte. Tryck på valfri knapp för att försöka igen eller tryck på 0 för att återgå till huvudmenyn.");
                }
            }
            else
            {
                ReturnToMainMenu("Du måste ange ett ID. Tryck på valfri knapp för att försöka igen eller tryck på 0 för att återgå till huvudmenyn.");
            }
        }
    }

    /// <summary>
    /// Metod som listar upp alla kontakters namn & epost och tillåter sedan användaren att plocka bort valfri kontakt med hjälp av e-post.
    /// Metoder från ListService används för funktionalitet.
    /// Använmdarens input valideras och plockar sedan bort kontakt om valid.
    /// </summary>
    public void ShowRemoveContact()
    {
        while (true)
        {
            Console.Clear();
            foreach(var contact in _listService.GetAllContactsFromList())
            {
                Console.WriteLine("----------------------");
                Console.WriteLine($"Namn:\t{contact.FirstName} {contact.LastName}");
                Console.WriteLine($"E-postadress:\t{contact.Email}");
                Console.WriteLine("----------------------\n");
            }

            Console.Write("Skriv in e-postadressen på den du vill plocka bort från adressboken: ");
            var option = Console.ReadLine();
            if (!string.IsNullOrEmpty(option))
            {
                if (_listService.RemoveContactFromList(option))
                {
                    ReturnToMainMenu("Kontakten är borttagen ur adressboken. Tryck på valfri knapp för att ta bort en till kontakt, eller tryck på 0 för att återgå till huvudmenyn.");
                }
                else
                {
                    ReturnToMainMenu("Den kontakten hittades inte. Tryck på valfri knapp för att försöka igen eller tryck på 0 för att återgå till huvudmenyn.");
                }
            }
            else
            {
                ReturnToMainMenu("Du måste ange en e-postadress. Tryck på valfri knapp för att försöka igen eller tryck på 0 för att återgå till huvudmenyn.");
            }
        }
    }

    /// <summary>
    /// Hjälpmetod för att kunna återvända till huvudmenyn efter att önskad åtgärd i adressboken är färdig, eller om input ej är godkänd.
    /// </summary>
    /// <param name="message">Skriver ut meddelande med alternativ att återvända till huvudmeny, efter att åtgärd är färdig eller att validering ej blivit godkänd</param>
    public void ReturnToMainMenu(string message)
    {
        Console.WriteLine(message);
        var choice = Console.ReadKey();
        if (choice.KeyChar == '0')
            ShowMainMenu();
    }
}
