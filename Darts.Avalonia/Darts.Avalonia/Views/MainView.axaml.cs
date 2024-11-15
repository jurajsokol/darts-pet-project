using Avalonia.ReactiveUI;
using Darts.Avalonia.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Darts.Avalonia.Views;

public partial class MainView : ReactiveUserControl<RoutingViewModel>
{
    public RoutingViewModel ViewModel { get; }

    public MainView()
    {
        ViewModel = App.Services!.GetRequiredService<RoutingViewModel>();
        DataContext = ViewModel;

        InitializeComponent();

        ViewModel.GoNext.Execute();
    }
}