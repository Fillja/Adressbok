using Adressbok.ClassLibrary.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;

namespace Adressbok.Maui.Viewmodels;

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

    private void UpdateContactList()
    {
        _listService.ContactListUpdated += (object? sender, EventArgs e) =>
        {
            ContactList = new ObservableCollection<IContact>(_listService.GetAllContactsFromList());
        };
    }

    [RelayCommand]
    public async Task NavigateToAddAsync()
    {
        await Shell.Current.GoToAsync("AddPage");
    }

    [RelayCommand]
    public async Task NavigateToEditAsync(IContact contact)
    {
        var parameters = new ShellNavigationQueryParameters
        {
            {"Contact", contact }
        };
        await Shell.Current.GoToAsync("EditPage", parameters);
    }

    [RelayCommand]
    public void Remove(IContact Contact)
    {
        _listService.RemoveContactFromList(Contact.Email);
    }
}
