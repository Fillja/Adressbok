using Adressbok.ClassLibrary.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Contact = Adressbok.ClassLibrary.Models.Contact;

namespace Adressbok.Maui.Viewmodels;

public partial class AddViewModel: ObservableObject
{
    private readonly IListService _listService;

    public AddViewModel(IListService listService)
    {
        _listService = listService;
    }


    [ObservableProperty]
    private IContact _contact = new Contact();

    [RelayCommand]
    private async Task Add()
    {
        _listService.AddContactToList(Contact);
        Contact = new Contact();
        await Shell.Current.GoToAsync("..");
    }
}
