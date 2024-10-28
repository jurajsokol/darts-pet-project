using Darts.MVVM.Models;
using Darts.MVVM.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Linq;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Darts.MVVM.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CreateGamePage : Page
    {
        public CreateGameViewModel ViewModel { get; } = App.Current.Services.GetService<CreateGameViewModel>();

        public CreateGamePage()
        {
            this.InitializeComponent();
            Loaded += async (_, _) => await ViewModel.LoadPlayers();
        }

        private void ListView_DragItemsCompleted(ListViewBase sender, DragItemsCompletedEventArgs args)
        {
            ViewModel.ReorderPlayers();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModel.SelectedPlayers = PlayersListView.SelectedItems.Cast<Player>().ToArray();
        }
    }
}
