using Darts.WinUI.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Darts.WinUI.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DartGamePage : Page
    {
        public DartsGameViewModel ViewModel { get; } = App.Current.Services.GetService<DartsGameViewModel>();

        public DartGamePage()
        {
            this.InitializeComponent();
        }

        private void CancelGameButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(CreateGamePage));
        }
    }
}
