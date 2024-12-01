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
    private readonly IServiceProvider services;
    private readonly TransitioningContentControl contentControl;
    private Stack<Control> navigationStack = new Stack<Control>();

    public ReactiveCommand<Unit, Unit> GoBackCommand { get; }

    public PageNavigation(TransitioningContentControl contentControl, IServiceProvider services)
    {
        this.contentControl = contentControl;
        this.services = services;
        GoBackCommand = ReactiveCommand.Create(GoBack);
    }

    public void GoToRootPage()
    {
        contentControl.Content = navigationStack.FirstOrDefault();
        navigationStack.Clear();
    }

    public void GoNext<T>() where T : ReactiveObject
    {
        Control? actualControl = contentControl.Content as Control;
        if (actualControl is not null)
        { 
            navigationStack.Push(actualControl);
        }

        contentControl.Content = ResolveView<T>();
    }

    private Control ResolveView<T>() where T : ReactiveObject
    {
        return typeof(T) switch
        {
            Type t when t == typeof(CreateGameViewModel) => services.GetRequiredService<CreateGameView>(),
            Type t when t == typeof(DartGameX01ViewModel) => services.GetRequiredService<DartGameX01View>(),
            Type t when t == typeof(X01SetupViewModel) => services.GetRequiredService<X01GameSetup>(),
            Type t when t == typeof(PlayersViewModel) => services.GetRequiredService<PlayersView>(),

            _ => throw new NotImplementedException(),
        };
    }
    
    public void GoBack()
    {
        if (navigationStack.Any())
        {
            contentControl.Content = navigationStack.Pop();
        }
    }
}
