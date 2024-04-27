using Darts.Games.Games;
using Darts.WinUI.Models;
using System.Collections.Generic;

namespace Darts.WinUI.ViewModels
{
    public interface INewDartGameArgs
    {
        IList<Player> GamePlayers { get; }
        GameTypes GameType { get; }
    }
}
