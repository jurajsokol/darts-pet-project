using Darts.Games.Enums;
using Darts.Games.Models;
using DynamicData;

namespace Darts.Games.State;

public class Store<T> : IDisposable where T : Player
{
    private int moveCount = 0;
    private Stack<GameState<T>> stateHistory = new Stack<GameState<T>>();
    private SourceCache<T, int> playersCollection = new SourceCache<T, int>(player => player.PlayerOrder);
    private SourceCache<PlayerMove, int> playerRoundScore = new SourceCache<PlayerMove, int>(move => move.OrderNum);
    private readonly IEqualityComparer<T> playerEqualityComparer;

    public IObservableCache<T, int> Players { get; }
    public IObservableCache<PlayerMove, int> PlayerRoundScore { get; }

    public int MoveCount => moveCount;

    public Store(T[] players, PlayerMove[] playerMoves, IEqualityComparer<T> playerEqualityComparer)
    {
        playersCollection.Edit(cache => cache.AddOrUpdate(players));
        playerRoundScore.Edit(cache => cache.AddOrUpdate(playerMoves));

        Players = playersCollection.AsObservableCache();
        PlayerRoundScore = playerRoundScore.AsObservableCache();
        this.playerEqualityComparer = playerEqualityComparer;
    }

    public void Dispose()
    {
        playersCollection.Dispose();
        playerRoundScore.Dispose();
    }

    public void UpdatePlayers(params T[] player)
    {
        playersCollection.Edit(cache =>
        {
            cache.AddOrUpdate(player);
        });
    }

    public void UpdatePlayerScore(PlayerMove move)
    {
        playerRoundScore.Edit(cache =>
        {
            cache.AddOrUpdate(move);
        });
        moveCount++;
    }

    public void MakeSnapshot()
    {
        stateHistory.Push(new GameState<T>(playersCollection.Items.ToArray(), playerRoundScore.Items.ToArray().ToArray(), MoveCount));
    }

    public void ResetPlayerScore()
    {
        playerRoundScore.Edit(cache =>
           cache.AddOrUpdate(Enumerable
               .Range(0, 3)
               .Select(x => new PlayerMove(TargetButtonNum.None, TargetButtonType.None, x))));
    }

    public void ResetMoveCount()
    { 
        moveCount = 0;
    }

    public void Undo()
    {
        if (stateHistory.Any())
        { 
            GameState<T> state = stateHistory.Pop();
            playersCollection.EditDiff(state.PlayersState, playerEqualityComparer);
            playerRoundScore.EditDiff(state.PlayerMoves, new PlayerMoveComparer());
            moveCount = state.MoveCount;
        }
    }
}
