using Avalonia.Controls;
using Darts.Avalonia.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Darts.Avalonia.GameScope
{
    public class CricketGameScope : GameScopeBase
    {
        private CricketGameView? gameView;

        public CricketGameScope(IServiceProvider serviceScope, TransitioningContentControl contentControl)
            : base(serviceScope, contentControl)
        {
        }

        public override void ReturnToGame()
        {
            if (gameView is not null)
            {
                contentControl.Content = gameView;
                gameView.ViewModel?.Activator.Activate();
            }
        }

        public override void StartGame()
        {
            gameView = service.GetRequiredService<CricketGameView>();
            contentControl.Content = gameView;
        }

        public override void StartSetup()
        {
            StartGame();
        }
    }
}
