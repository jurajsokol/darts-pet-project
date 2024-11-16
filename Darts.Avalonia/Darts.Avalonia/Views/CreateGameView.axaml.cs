using Avalonia.Controls;
using Darts.Avalonia.ViewModels;
using Darts.Avalonia.Views.Dialog;

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

}