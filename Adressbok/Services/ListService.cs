using Adressbok.Interfaces;
using Adressbok.Models;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Adressbok.Services;

public class ListService : IListService
{
    private readonly IFileService _fileService;
    private List<IContact> contactList;
    private readonly string filePath = @"C:\Programmering\EC\CSHARP-COURSE\Adressbok\Contacts.json";

    public ListService (IFileService fileService)
    {
        _fileService = fileService;
        contactList = GetAllContactsFromList().ToList();
    }

    /// <summary>
    /// Metod som lägger till ett contact-objekt av typen IContact i contactlist och sparar sedan ner det som json-objekt med hjälp av 
    /// TypeNameHandling & metoden SaveContentToFile() från FileService.
    /// </summary>
    /// <param name="contact">Tar emot en contact som ska adderas i listan och sparas.</param>
    /// <returns>Returnerar true vid lyckad utföring, annars false.</returns>
    public bool AddContactToList(IContact contact)
    {
        try
        {
                contactList.Add(contact);
                _fileService.SaveContentToFile(JsonConvert.SerializeObject(contactList, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Objects,
                }), filePath);
                return true;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return false;
    }

    /// <summary>
    /// Hämtar upp sparad json-data med hjälp av TypeNameHandling och metoden GetContentFromFile() från FileService.
    /// Sparar detta sedan i variabel contactList.
    /// </summary>
    /// <returns>
    /// Om filen inte är tom så returneras contactList med allt innehåll.
    /// Om filen är tom så returneras en tom lista av typen IContact.
    /// Vid misslyckande returneras null.
    /// </returns>
    public IEnumerable<IContact> GetAllContactsFromList()
    {
        try
        {
            var content = _fileService.GetContentFromFile(filePath);
            if (!string.IsNullOrEmpty(content))
            {
                contactList = JsonConvert.DeserializeObject<List<IContact>>(content, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Objects,
                })!;
                return contactList;
            }
            else
            {
                List<IContact> contactList = [];
                return contactList;
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }

    /// <summary>
    /// Metod som använder mottaget id och letar upp önskad kontakt i contactList.
    /// </summary>
    /// <param name="ID">Tar emot ID som ska matchas mot kontakt i contactList.</param>
    /// <returns>Vid lyckad matchning returneras kontakten med korrekt ID. Annars returneras null.</returns>
    public IContact GetSpecificContactFromList(int ID)
    {
        var contact = contactList.FirstOrDefault(x => x.ID == ID);
        return contact ??= null!;
    }


    /// <summary>
    /// Metod som använder mottagen string "email" och letar upp önskad kontakt i contactList. 
    /// Efter validering plockas kontakt med motsvarande email bort från listan, och listan sparas med hjälp av SaveContentToFile() från FileService.
    /// </summary>
    /// <param name="Email">Sträng som ska representera egenskapen email, och söks efter i listan.</param>
    /// <returns>Returnerar true vid lyckad borttagning. Returnerar false vid mysslickad borttagning eller annat fel.</returns>
    public bool RemoveContactFromList(string Email)
    {
        try
        {
            var contact = contactList.FirstOrDefault(x => x.Email == Email);
            if (contact != null!)
            {
                contactList.Remove(contact);
                _fileService.SaveContentToFile(JsonConvert.SerializeObject(contactList, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Objects,
                }), filePath);
                return true;
            }
            return false;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return false;
    }
}
