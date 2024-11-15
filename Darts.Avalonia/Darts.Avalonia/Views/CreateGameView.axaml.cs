using Avalonia.Controls;
using Avalonia.ReactiveUI;
using Darts.Avalonia.ViewModels;
using Darts.Avalonia.ViewRouting;
using ReactiveUI;

namespace Darts.Avalonia;

public partial class CreateGameView : ReactiveUserControl<CreateGameViewModel>, IViewFor<CreateGameViewModel>
{
    private CreateGameViewModel? viewModel;
    public CreateGameViewModel? ViewModel
    { 
        get => viewModel;
        set
        {
            viewModel = value;
            DataContext = value;
        }
    }

    object? IViewFor.ViewModel
    {
        get => ViewModel;
        set => ViewModel = (value as RoutableViewModelWrapper<CreateGameViewModel>).ViewModel;
    }

    public CreateGameView()
    {
        InitializeComponent();
        Loaded += async (_, _) => await ViewModel.LoadPlayers();
    }

}