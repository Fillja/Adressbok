using Adressbok.ClassLibrary.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Contact = Adressbok.ClassLibrary.Models.Contact;

namespace Adressbok.Maui.Viewmodels;

/// <summary>
/// Viewmodel för Add page som hanterar tillägg av ny kontakt.
/// </summary>
public partial class AddViewModel: ObservableObject
{
    private readonly IListService _listService;

    public AddViewModel(IListService listService)
    {
        _listService = listService;
    }


    [ObservableProperty]
    private IContact _contact = new Contact();

    /// <summary>
    /// Metod för att lägga till ny kontakt. Använder metoden AddContactToList() från ListService.
    /// Nollställer sedan Contact.
    /// </summary>
    [RelayCommand]
    private async Task Add()
    {
        _listService.AddContactToList(Contact);
        Contact = new Contact();
        await Shell.Current.GoToAsync("..");
    }

    /// <summary>
    /// Metod för att gå tillbaka utan att lägga till en ny kontakt.
    /// Nollställer även Contact.
    /// </summary>
    [RelayCommand]
    private async Task GoBack()
    {
        Contact = new Contact();
        await Shell.Current.GoToAsync("..");
    }
}
