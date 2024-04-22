using Darts.WinUI.Models;
using Darts.WinUI.Views.DialogWindow;
using FluentResults;
using Microsoft.UI.Xaml.Controls;
using System.Xml.Linq;
using System;
using Microsoft.UI.Xaml;
using System.Threading.Tasks;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Darts.WinUI.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddPlayerPage : ContentDialog, IDialogWindow<Player>
    {
        public Player ViewModel { get; }

        public AddPlayerPage(Player viewModel)
        {
            this.ViewModel = viewModel;
            this.XamlRoot = App.Current.MainWindow.Content.XamlRoot;
            this.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            this.InitializeComponent();
        }

        public async Task<bool> ShowDialog()
        {
            return await ShowAsync() == ContentDialogResult.Primary;
        }
    }
}
