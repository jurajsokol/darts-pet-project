﻿using Avalonia.Controls;
using Darts.Avalonia.Views.X01GameView;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Darts.Avalonia.GameScope;

public class X01GameScope : GameScopeBase
{
    private DartGameX01View? gameView;

    public X01GameScope(IServiceProvider serviceScope, TransitioningContentControl contentControl)
        : base(serviceScope, contentControl)
    {
    }

    public override void ReturnToGame()
    {
        if (gameView is not null)
        {
            contentControl.Content = gameView;
        }
    }

    public override void StartGame()
    {
        gameView = service.GetRequiredService<DartGameX01View>();
        contentControl.Content = gameView;
    }

    public override void StartSetup()
    {
        contentControl.Content = service.GetRequiredService<X01GameSetup>();
    }
}
