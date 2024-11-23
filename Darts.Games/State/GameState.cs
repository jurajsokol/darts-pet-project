using Darts.Games.Models;
using DynamicData;

namespace Darts.Games.State;

public class GameState
{
    public IChangeSet<Player, int> PLayerChanges { get; }

    public IChangeSet<PlayerMove, int> PlayerMoves { get; }

    public GameState(IChangeSet<Player, int> pLayerChanges, IChangeSet<PlayerMove, int> playerMoves)
    {
        PLayerChanges = pLayerChanges;
        PlayerMoves = playerMoves;
    }
}
