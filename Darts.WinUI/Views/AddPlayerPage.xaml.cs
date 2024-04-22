using Darts.WinUI.DialogWindow;
using Darts.WinUI.Models;
using Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Darts.WinUI.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddPlayerPage : Page, IDialogWindow<Player>
    {
        public Player ViewModel { get; }

        public AddPlayerPage(Player viewModel)
        {
            this.ViewModel = viewModel;
            this.InitializeComponent();
        }
    }
}
