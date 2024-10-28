using Darts.MVVM.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using System;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Darts.MVVM.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EditPlayersPage : Page
    {
        public EditPlayersViewModel ViewModel { get; }

        public Frame RootFrame => App.Current.RootFrame; 

        public EditPlayersPage()
        {
            ViewModel = App.Current.Services.GetService<EditPlayersViewModel>();
            Loaded += async (_, _) => await ViewModel.LoadPlayers();
            this.InitializeComponent();
        }

        private void BackButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}
