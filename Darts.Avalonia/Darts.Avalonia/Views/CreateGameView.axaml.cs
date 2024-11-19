using Avalonia.Controls;
using Darts.Avalonia.ViewModels;

namespace Darts.Avalonia.Views;

public partial class CreateGameView : UserControl
{
    public CreateGameViewModel ViewModel { get; }

    public CreateGameView(CreateGameViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = ViewModel;
        InitializeComponent();
        Loaded += async (_, _) => await ViewModel.LoadPlayers();
    }

    public CreateGameView()
    { 
    }
}