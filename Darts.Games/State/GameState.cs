using Darts.Games.Models;

namespace Darts.Games.State;

public class GameState
{
    public Player[] PlayersState { get; }

    public PlayerMove[] PlayerMoves { get; }

    public int MoveCount { get; }

    public GameState(Player[] playersState, PlayerMove[] playerMoves, int moveCount)
    {
        PlayersState = playersState;
        PlayerMoves = playerMoves;
        MoveCount = moveCount;
    }
}
