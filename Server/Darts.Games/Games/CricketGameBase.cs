using Darts.Games.Enums;
using Darts.Games.Models;
using Darts.Games.State;
using DynamicData;
using System.Collections.Immutable;
using System.Reactive.Linq;

namespace Darts.Games.Games;

public abstract class CricketGameBase : IDartGame<CricketPlayer>
{
    private const int MAX_THROWS_PER_ROUND = 3;
    protected readonly Store<CricketPlayer> gameStore;

    public CricketPlayer ActualPlayer => gameStore.Players.Items.First(x => x.IsPlayerActive);
    public IObservable<IChangeSet<CricketPlayer, int>> Players { get; }
    public IObservable<IChangeSet<PlayerMove, int>> PlayerRoundScore { get; }
    public IObservable<bool> CanSetNextPlayer { get; }

    public CricketGameBase(Store<CricketPlayer> gameStore)
    {
        Players = gameStore.Players.Connect();

        PlayerRoundScore = gameStore.PlayerRoundScore.Connect();

        gameStore.ResetPlayerScore();
        CanSetNextPlayer = PlayerRoundScore.ToCollection().Select(x => x.All(i => i.TargetButton != TargetButtonNum.None));
        this.gameStore = gameStore;
    }

    public bool PlayerMove(TargetButtonNum number, TargetButtonType type)
    {
        CricketPlayer actualPlayer = ActualPlayer;

        if (gameStore.MoveCount >= MAX_THROWS_PER_ROUND)
        {
            return false;
        }

        gameStore.MakeSnapshot();
        PlayerMove move = gameStore.PlayerRoundScore.Items
            .First(x => x.OrderNum == gameStore.MoveCount) with { TargetButton = number, TargetButtonType = type };

        gameStore.UpdatePlayerScore(move);

        if (((int)number) < 15 || number == TargetButtonNum.Miss)
        {
            return false;
        }

        CricketDartButtonState buttonState = actualPlayer.CricketDartButtonStates.First(x => x.TargetButtonNum == number);

        // closed position
        if (buttonState.CricketTargetButtonState == CricketTargetButtonState.Closed)
        {
            return false;
        }

        // opened position
        if (buttonState.CricketTargetButtonState == CricketTargetButtonState.Open)
        {
            UpdatePlayersScore(actualPlayer, move.GetScore, number);
        }
        // not yet opened position
        else
        {
            int hitCount = (int)buttonState.CricketTargetButtonState + (int)type;
            int newPlayerScore = actualPlayer.Score;
            CricketDartButtonState newButtonState = buttonState;
            if (hitCount >= (int)CricketTargetButtonState.Open)
            {
                bool isLastPlayer = gameStore.Players.Items
                    .Count(x => x.CricketDartButtonStates
                        .First(y => y.TargetButtonNum == number)
                        .CricketTargetButtonState != CricketTargetButtonState.Open) == 1;
                // last player closes position
                if (isLastPlayer)
                {
                    gameStore.UpdatePlayers(gameStore.Players.Items
                        .Select(x =>
                        {
                            CricketDartButtonState numberToClose = x.CricketDartButtonStates.First(y => y.TargetButtonNum == number);
                            ImmutableArray<CricketDartButtonState> states = x.CricketDartButtonStates
                                .Replace(
                                    numberToClose,
                                    numberToClose with { CricketTargetButtonState = CricketTargetButtonState.Closed });

                            return x with { CricketDartButtonStates = states };
                        })
                        .ToArray());

                    return HasPlayerWon();
                }
                else
                { 
                    int playerPointsMultiplier = hitCount - (int)CricketTargetButtonState.Open;
                    newButtonState = buttonState with { CricketTargetButtonState = CricketTargetButtonState.Open };
                    actualPlayer = UpdatePlayersScore(actualPlayer, playerPointsMultiplier * (int)number, number);
                }
            }
            else
            {
                newButtonState = buttonState with { CricketTargetButtonState = (CricketTargetButtonState)((int)buttonState.CricketTargetButtonState + (int)type) };
            }

            ImmutableArray<CricketDartButtonState> states = actualPlayer.CricketDartButtonStates.Replace(buttonState, newButtonState);
            gameStore.UpdatePlayers(actualPlayer with { CricketDartButtonStates = states});
        }

        // winner
        return HasPlayerWon();
    }

    protected abstract CricketPlayer UpdatePlayersScore(CricketPlayer actualPlayer, int score, TargetButtonNum buttonNum);

    public void NextPlayer()
    {
        gameStore.MakeSnapshot();

        CricketPlayer actualPlayer = (gameStore.Players.Items.FirstOrDefault(x => x.PlayerOrder == ActualPlayer.PlayerOrder + 1) ?? gameStore.Players.Items.First())
            with { IsPlayerActive = true };
        gameStore.UpdatePlayers(ActualPlayer with { IsPlayerActive = false });
        gameStore.UpdatePlayers(actualPlayer);

        gameStore.ResetPlayerScore();
        gameStore.ResetMoveCount();
    }

    public void Undo()
    {
        gameStore.Undo();
    }

    protected abstract bool HasPlayerWon();

    public abstract CricketPlayer[] GetPlayersResults();

    public void Dispose()
    {
        gameStore.Dispose();
    }
}
