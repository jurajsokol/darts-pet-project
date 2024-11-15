using Darts.Avalonia.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using System;
using System.Reactive;

namespace Darts.Avalonia.ViewRouting;

public class PageNavigation : IPageNavigation
{
    private readonly RoutingStateWrapper routingStateWrapper;
    private readonly IServiceProvider services;

    public RoutingState RoutingState { get; }
    public ReactiveCommand<Unit, IRoutableViewModel> GoNextCommand { get; }
    public ReactiveCommand<Unit, IRoutableViewModel> GoBackCommand => RoutingState.NavigateBack;

    public PageNavigation(RoutingStateWrapper routingStateWrapper, RoutingState routingState, IServiceProvider services)
    {
        this.routingStateWrapper = routingStateWrapper;
        RoutingState = routingState;
        this.services = services;
        GoNextCommand = ReactiveCommand.CreateFromObservable(
            () =>  RoutingState.Navigate.Execute(GetWrapper(services.GetRequiredService<CreateGameViewModel>()))
        );
    }

    public void GoNext<T>() where T : ReactiveObject
    {
        RoutableViewModelWrapper<T> viewModel = typeof(T) switch
        {
            Type t when t == typeof(AddPlayerViewModel) => GetWrapper(services.GetRequiredService<T>()),
            Type t when t == typeof(CreateGameViewModel) => GetWrapper(services.GetRequiredService<T>()),

            _ => throw new NotImplementedException(),
        };

        RoutingState.Navigate.Execute(viewModel);
    }

    public void GoBack()
    {
        RoutingState.NavigateBack.Execute();
    }

    private RoutableViewModelWrapper<T> GetWrapper<T>(T viewModel) 
        where T : ReactiveObject
        => new RoutableViewModelWrapper<T>(routingStateWrapper, viewModel);
}
