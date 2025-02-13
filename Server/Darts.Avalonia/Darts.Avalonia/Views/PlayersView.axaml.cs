using Avalonia.Controls;
using Darts.Avalonia.ViewModels;

namespace Darts.Avalonia;

public partial class PlayersView : UserControl
{
    public PlayersViewModel ViewModel { get; }
    
    public PlayersView(PlayersViewModel viewModel)
    {
        DataContext = viewModel;

        InitializeComponent();
        ViewModel = viewModel;

        Loaded += async (_, _) => await ViewModel.LoadPlayers();
    }
}