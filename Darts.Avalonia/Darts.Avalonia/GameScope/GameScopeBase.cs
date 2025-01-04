using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Darts.Avalonia.GameScope;

public abstract class GameScopeBase : IGameScope
{
    protected readonly IServiceProvider service;
    protected readonly TransitioningContentControl contentControl;

    public GameScopeBase(IServiceProvider serviceScope, TransitioningContentControl contentControl)
    {
        this.service = serviceScope;
        this.contentControl = contentControl;
    }

    public abstract void StartSetup();

    public abstract void StartGame();

    public void ExitGame()
    {
        contentControl.Content = service.GetRequiredService<MainMenuView>();
    }
}
