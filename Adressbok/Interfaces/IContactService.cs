namespace Adressbok.Interfaces;

public interface IContactService
{
    // Se kommentarer i service.
    public void SetContactProperties(IContact contact, Func<string> getProperty, Action<string> setProperty, string message, string errorMessage);
    public void SetPropertyWithRegex(IContact contact, Func<string> getProperty, Action<string> setProperty, string regexPattern, string message, string errorMessage, string emptyField);
    public void SetEmailWithRegex(List<IContact> contactList, IContact contact, Func<string> getProperty, Action<string> setProperty, string regexPattern, string message, string errorMessage, string emptyField);
}
