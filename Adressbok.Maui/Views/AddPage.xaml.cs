using Adressbok.Maui.Viewmodels;

namespace Adressbok.Maui.Views;

public partial class AddPage : ContentPage
{
	public AddPage(AddViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}