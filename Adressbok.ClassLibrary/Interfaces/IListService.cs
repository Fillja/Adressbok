

namespace Adressbok.ClassLibrary.Interfaces;

public interface IListService
{
    // Se kommentarer i service.
    event EventHandler? ContactListUpdated;
    public bool AddContactToList(IContact contact);
    public IContact GetSpecificContactFromList(int ID);
    IEnumerable<IContact> GetAllContactsFromList();
    public bool RemoveContactFromList(string email);
}
