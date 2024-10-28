using Darts.DAL.Entities;
using Darts.Games.Games;

namespace Darts.MVVM.ViewModels;

public interface INewDartGameArgs
{
    IList<Player> GamePlayers { get; }
    GameTypes GameType { get; }
}
