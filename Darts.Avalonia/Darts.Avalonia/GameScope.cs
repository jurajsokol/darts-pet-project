using Avalonia.Controls;
using Darts.Avalonia.Views;
using Darts.Avalonia.Views.X01GameView;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reactive.Disposables;

namespace Darts.Avalonia;

public class GameScope : IDisposable
{
    private readonly IServiceProvider serviceScope;
    private readonly TransitioningContentControl contentControl;
    private bool isDisposed = false;

    public CompositeDisposable Disposables { get; } = new CompositeDisposable();

    public GameScope(IServiceProvider serviceScope, TransitioningContentControl contentControl)
    {
        this.serviceScope = serviceScope;
        this.contentControl = contentControl;
    }

    public void StartSetup()
    {
        contentControl.Content = serviceScope.GetRequiredService<X01GameSetup>();
    }

    public void StartGame()
    {
        contentControl.Content = serviceScope.GetRequiredService<DartGameX01View>();
    }

    public void Dispose()
    {
        if (!isDisposed)
        { 
            isDisposed = true;
            contentControl.Content = serviceScope.GetRequiredService<CreateGameView>();
            Disposables.Dispose();
        }

    }
}
