using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Darts.Avalonia.GameScope
{
    public class CricketGameScope : GameScopeBase
    {
        public CricketGameScope(IServiceProvider serviceScope, TransitioningContentControl contentControl)
            : base(serviceScope, contentControl)
        {
        }

        public override void StartGame()
        {
            contentControl.Content = service.GetRequiredService<CricketGameView>();
        }

        public override void StartSetup()
        {
            StartGame();
        }
    }
}
