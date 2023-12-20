using Adressbok.ClassLibrary.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Contact = Adressbok.ClassLibrary.Models.Contact;

namespace Adressbok.Maui.Viewmodels;

public partial class EditViewModel : ObservableObject, IQueryAttributable
{
    [ObservableProperty]
    private IContact _contact = new Contact();

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        Contact = (query["Contact"] as IContact)!;
    }

    [RelayCommand]
    private async Task Edit()
    {
        await Shell.Current.GoToAsync("..");
    }
}
