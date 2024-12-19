using Avalonia.Controls;
using Darts.Avalonia.Views.X01GameView;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Darts.Avalonia;

public class GameScope
{
    private readonly IServiceProvider service;
    private readonly TransitioningContentControl contentControl;

    public GameScope(IServiceProvider serviceScope, TransitioningContentControl contentControl)
    {
        this.service = serviceScope;
        this.contentControl = contentControl;
    }

    public void StartSetup()
    {
        contentControl.Content = service.GetRequiredService<X01GameSetup>();
    }

    public void StartGame()
    {
        contentControl.Content = service.GetRequiredService<DartGameX01View>();
    }

    public void ExitGame()
    {
        contentControl.Content = service.GetRequiredService<MainMenuView>();
    }
}
