using Darts.Games.Models;
using DynamicData;
using System.Reactive.Linq;

namespace Darts.Games.Games
{
    internal class X01 : IDartGame, IDisposable
    {
        private const int MAX_THROWS_PER_ROUND = 3;

        private SourceCache<Player, int> playersCollection;
        private SourceCache<PlayerMove, int> playerRoundScore;
        private Player actualPlayer;
        private int moveCount = 0;

        public Player ActualPLayer => actualPlayer;
        public IObservable<IChangeSet<Player, int>> Players { get; }
        public IObservable<IChangeSet<PlayerMove, int>> PlayerRoundScore { get; }
        public IObservable<bool> CanSetNextPlayer { get; }


        public X01(IList<Player> players, int initialScore)
        {
            playersCollection = new SourceCache<Player, int>(player => player.PlayerOrder);
            Players = playersCollection.Connect();
            playersCollection.Edit(cache =>
            {
                cache.AddOrUpdate(players.Select(p => p with { Score = initialScore }));
            });

            actualPlayer = playersCollection.Items.First() with { IsPlayerActive = true };
            playersCollection.AddOrUpdate(actualPlayer);

            playerRoundScore = new SourceCache<PlayerMove, int>(move => move.OrderNum);
            ResetPlayerScore();
            PlayerRoundScore = playerRoundScore.Connect();
            CanSetNextPlayer = PlayerRoundScore.ToCollection().Select(x => x.All(i => i.TargetButton != TargetButtonNum.None));
        }

        public void PlayerMove(TargetButtonNum number, TargetButtonType type)
        {
            if (moveCount >= MAX_THROWS_PER_ROUND)
            {
                return;
            }

            playerRoundScore.Edit(cache =>
            {
                Models.PlayerMove move = cache.Items.First(x => x.OrderNum == moveCount);
                cache.AddOrUpdate(move with { TargetButton = number, TargetButtonType = type });
            });
            
            moveCount++;
        }

        public void NextPlayer()
        {
            int actualPLayerScore = playerRoundScore.Items.Select(x => x.GetScore).Sum();
            playersCollection.Edit(cache =>
            {
                cache.AddOrUpdate(actualPlayer with { Score = actualPlayer.Score - actualPLayerScore, IsPlayerActive = false });
                actualPlayer = (playersCollection.Items.FirstOrDefault(x => x.PlayerOrder == actualPlayer.PlayerOrder + 1) ?? playersCollection.Items.First()) with { IsPlayerActive = true };
                cache.AddOrUpdate(actualPlayer);
            });

            ResetPlayerScore();
            moveCount = 0;
        }

        private void ResetPlayerScore()
        {
            playerRoundScore.Edit(cache =>
               cache.AddOrUpdate(Enumerable
                   .Range(0, 3)
                   .Select(x => new PlayerMove(TargetButtonNum.None, TargetButtonType.None, x))));
        }

        public void Dispose()
        {
            playersCollection.Dispose();
            playerRoundScore.Dispose();
        }
    }
}
