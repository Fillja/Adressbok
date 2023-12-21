using Adressbok.ClassLibrary.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Contact = Adressbok.ClassLibrary.Models.Contact;

namespace Adressbok.Maui.Viewmodels;

/// <summary>
/// Viewmodel för Edit page som hanterar detaljerad visning och uppdatering av kontakter.
/// </summary>
public partial class EditViewModel(IListService listservice) : ObservableObject, IQueryAttributable
{
    private readonly IListService _listservice = listservice;

    [ObservableProperty]
    private IContact _contact = new Contact();

    /// <summary>
    /// Använder metod från Interface IQueryAttributable för att hämta parametrar från Mainviewmodel
    /// </summary>
    /// <param name="query">Contact som vi får med oss</param>
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        Contact = (query["Contact"] as IContact)!;
    }

    /// <summary>
    /// Metod för att uppdatera kontakten i listan, rensar sedan Contact och går tillbaka till Mainpage.
    /// </summary>
    [RelayCommand]
    private async Task Edit()
    {
        _listservice.UpdateContactInList(Contact);
        Contact = new Contact();
        await Shell.Current.GoToAsync("..");
    }

    /// <summary>
    /// Metod för att gå tillbaka till mainpage utan att uppdatera kontakten, rensar även Contact.
    /// </summary>
    [RelayCommand]
    private async Task GoBack()
    {
        Contact = new Contact();
        await Shell.Current.GoToAsync("..");
    }
}
