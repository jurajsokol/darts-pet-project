using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using System;
using System.Reactive;

namespace Darts.Avalonia.ViewModels;

public class RoutingViewModel : IScreen
{
    public RoutingState Router { get; } = new RoutingState();
    public ReactiveCommand<Unit, IRoutableViewModel> GoNext { get; }
    public ReactiveCommand<Unit, IRoutableViewModel> GoBack => Router.NavigateBack;

    public RoutingViewModel(IServiceProvider services)
    {
        GoNext = ReactiveCommand.CreateFromObservable(
            () => Router.Navigate.Execute(services.GetRequiredService<CreateGameViewModel>())
        );
    }
}
