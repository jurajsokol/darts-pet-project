using Darts.WinUI.Models;
using Darts.WinUI.ViewModels;
using Darts.WinUI.Views;
using FluentResults;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Threading.Tasks;

namespace Darts.WinUI.DialogWindow
{
    public class PLayerDialogWindow : IDialogWindow<string, string>
    {
        public async Task<Result<string>> ShowWindow(string name)
        {
            ContentDialog dialog = new ContentDialog();

            // XamlRoot must be set in the case of a ContentDialog running in a Desktop app
            dialog.XamlRoot = App.Current.MainWindow.Content.XamlRoot;
            dialog.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            dialog.Title = "Pridať hráča";
            dialog.PrimaryButtonText = "Pridať";
            dialog.SecondaryButtonText = "Zrušiť";
            dialog.DefaultButton = ContentDialogButton.Primary;
            Player addVM = new Player();
            addVM.Name = name;
            AddPlayerPage userpage = new AddPlayerPage(addVM);
            dialog.Content = userpage;

            var result = await dialog.ShowAsync();

            Result<string> r = new Result<string>(); 

            if (result == ContentDialogResult.Primary)
            {
                return r.WithValue(addVM.Name);
            }

            return r.WithError("User cancelled player dialog window");
        }
    }
}
