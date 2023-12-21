using Adressbok.ClassLibrary.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;

namespace Adressbok.Maui.Viewmodels;

/// <summary>
/// Viewmodel för Mainpage som hanterar visning av adressboken och navigering till andra pages.
/// </summary>
public partial class MainViewModel : ObservableObject
{
    private readonly IListService _listService;

    public MainViewModel(IListService listService)
    {
        _listService = listService;
        _contactList = new ObservableCollection<IContact>(_listService.GetAllContactsFromList());
        UpdateContactList();
    }

    [ObservableProperty]
    private ObservableCollection<IContact> _contactList;

    /// <summary>
    /// Metod för att uppdatera gränssnitt när Eventhandler i Listservice har flaggat.
    /// </summary>
    private void UpdateContactList()
    {
        _listService.ContactListUpdated += (object? sender, EventArgs e) =>
        {
            ContactList = new ObservableCollection<IContact>(_listService.GetAllContactsFromList());
        };
    }

    /// <summary>
    /// Metod för navigering till Add Page.
    /// </summary>
    [RelayCommand]
    public async Task NavigateToAddAsync()
    {
        await Shell.Current.GoToAsync("AddPage");
    }

    /// <summary>
    /// Metod för navigering till Edit Page.
    /// <param name="contact">Skickar med Contact för att kunna uppdatera den.</param>
    /// </summary>
    [RelayCommand]
    public async Task NavigateToEditAsync(IContact contact)
    {
        var parameters = new ShellNavigationQueryParameters
        {
            {"Contact", contact }
        };
        await Shell.Current.GoToAsync("EditPage", parameters);
    }

    /// <summary>
    /// Metod för att ta bort kontakt från listan, använder RemoveContactFromList() från ListService
    /// </summary>
    /// <param name="Contact">Tar emot kontakten som ska plockas bort.</param>
    [RelayCommand]
    public void Remove(IContact Contact)
    {
        _listService.RemoveContactFromList(Contact.Email);
    }
}
