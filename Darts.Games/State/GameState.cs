using Darts.Games.Models;

namespace Darts.Games.State;

public class GameState<T> where T : Player
{
    public T[] PlayersState { get; }

    public PlayerMove[] PlayerMoves { get; }

    public int MoveCount { get; }

    public GameState(T[] playersState, PlayerMove[] playerMoves, int moveCount)
    {
        PlayersState = playersState;
        PlayerMoves = playerMoves;
        MoveCount = moveCount;
    }
}
