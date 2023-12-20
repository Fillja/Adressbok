using Adressbok.Maui.Viewmodels;

namespace Adressbok.Maui.Views;

public partial class EditPage : ContentPage
{
	public EditPage(EditViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}