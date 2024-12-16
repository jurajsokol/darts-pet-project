using Avalonia.Controls;
using Darts.Avalonia.ViewModels;
using Darts.Avalonia.Views;
using Darts.Avalonia.Views.X01GameView;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;

namespace Darts.Avalonia.ViewRouting;

public class PageNavigation : IPageNavigation
{
    private readonly IServiceProvider serviceProvider;
    private readonly TransitioningContentControl contentControl;
    private Stack<ReactiveObject> navigationStack = new Stack<ReactiveObject>();

    public ReactiveCommand<Unit, Unit> GoBackCommand { get; }

    public PageNavigation(TransitioningContentControl contentControl, IServiceProvider services)
    {
        this.contentControl = contentControl;
        this.serviceProvider = services;
        GoBackCommand = ReactiveCommand.Create(GoBack);
    }

    public void GoToRootPage()
    {
        contentControl.Content = navigationStack.FirstOrDefault();
        navigationStack.Clear();
    }

    public void GoNext<T>() where T : ReactiveObject
    {
        GoNext<T>(serviceProvider);
    }

    public void GoNext<T>(IServiceProvider services) where T : ReactiveObject
    {
        Control? actualControl = contentControl.Content as Control;
        ReactiveObject? viewModel = actualControl?.DataContext as ReactiveObject;
        if (viewModel is not null)
        { 
            navigationStack.Push(viewModel);
        }

        contentControl.Content = ResolveView(typeof(T), services);
    }

    private Control ResolveView(Type type, IServiceProvider services)
    {
        return type switch
        {
            Type t when t == typeof(CreateGameViewModel) => services.GetRequiredService<CreateGameView>(),
            Type t when t == typeof(DartGameX01ViewModel) => services.GetRequiredService<DartGameX01View>(),
            Type t when t == typeof(X01SetupViewModel) => services.GetRequiredService<X01GameSetup>(),
            Type t when t == typeof(PlayersViewModel) => services.GetRequiredService<PlayersView>(),

            _ => throw new NotImplementedException(),
        };
    }

    public void Switch<T>() where T : ReactiveObject
    {
        Switch<T>(serviceProvider);
    }

    public void Switch<T>(IServiceProvider services) where T : ReactiveObject
    {
        contentControl.Content = ResolveView(typeof(T), services);
    }
    
    public void GoBack()
    {
        if (navigationStack.Any())
        {
            ReactiveObject viewModel = navigationStack.Pop();
            contentControl.Content = ResolveView(viewModel.GetType(), serviceProvider);
        }
    }
}
