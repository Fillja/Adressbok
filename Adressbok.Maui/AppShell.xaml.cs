using Adressbok.Maui.Views;

namespace Adressbok.Maui
{
    public partial class AppShell : Shell
    {
        /// <summary>
        /// Routes som hanterar navigering mellan mina pages.
        /// </summary>
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));  
            Routing.RegisterRoute(nameof(AddPage), typeof(AddPage));
            Routing.RegisterRoute(nameof(EditPage), typeof(EditPage));
        }
    }
}
