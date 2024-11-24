using Darts.Games.Models;
using DynamicData;

namespace Darts.Games.State;

public class Store : IDisposable
{
    private int moveCount = 0;
    private Stack<GameState> stateHistory = new Stack<GameState>();
    private SourceCache<Player, int> playersCollection = new SourceCache<Player, int>(player => player.PlayerOrder);
    private SourceCache<PlayerMove, int> playerRoundScore = new SourceCache<PlayerMove, int>(move => move.OrderNum);

    public IObservableCache<Player, int> Players { get; }
    public IObservableCache<PlayerMove, int> PlayerRoundScore { get; }

    public int MoveCount => moveCount;

    public Store(Player[] players, PlayerMove[] playerMoves)
    {
        playersCollection.Edit(cache => cache.AddOrUpdate(players));
        playerRoundScore.Edit(cache => cache.AddOrUpdate(playerMoves));

        Players = playersCollection.AsObservableCache();
        PlayerRoundScore = playerRoundScore.AsObservableCache();
    }

    public void Dispose()
    {
        playersCollection.Dispose();
        playerRoundScore.Dispose();
    }

    public void UpdatePlayers(Player player)
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
        stateHistory.Push(new GameState(playersCollection.Items.ToArray(), playerRoundScore.Items.ToArray().ToArray(), MoveCount));
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
            GameState state = stateHistory.Pop();
            playersCollection.EditDiff(state.PlayersState, new PlayerComparer());
            playerRoundScore.EditDiff(state.PlayerMoves, new PlayerMoveComparer());
            moveCount = state.MoveCount;
        }
    }
}
