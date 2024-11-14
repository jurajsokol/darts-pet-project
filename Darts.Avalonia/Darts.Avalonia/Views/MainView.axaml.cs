using Avalonia.Controls;
using Darts.Avalonia.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Darts.Avalonia.Views;

public partial class MainView : UserControl
{
    public CreateGameViewModel ViewModel { get; }

    public MainView()
    {
        InitializeComponent();

        ViewModel = App.Services!.GetRequiredService<CreateGameViewModel>();
        DataContext = ViewModel;

        Loaded += async (_, _) => 
        {
            await ViewModel.LoadPlayers();
        };
    }
}