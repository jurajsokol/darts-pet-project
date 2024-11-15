using Avalonia.ReactiveUI;
using Darts.Avalonia.ViewModels;

namespace Darts.Avalonia;

public partial class CreateGameView : ReactiveUserControl<CreateGameViewModel>
{
    public CreateGameView(CreateGameViewModel viewModel)
    {
        DataContext = viewModel;
        InitializeComponent();

        Loaded += async (_, _) => await viewModel.LoadPlayers();
    }
}