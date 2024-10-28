using Darts.Games.Games;
using Darts.MVVM.Models;

namespace Darts.MVVM.ViewModels;

public interface INewDartGameArgs
{
    IList<Player> GamePlayers { get; }
    GameTypes GameType { get; }
}
