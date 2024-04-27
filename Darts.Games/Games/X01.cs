using Darts.Games.Models;
using DynamicData;
using System.Reactive.Linq;

namespace Darts.Games.Games
{
    internal class X01 : IDartGame
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


        public X01(IList<Player> players, uint initialScore)
        {
            playersCollection = new SourceCache<Player, int>(player => player.PlayerOrder);
            Players = playersCollection.Connect();
            playersCollection.Edit(cache =>
            {
                cache.AddOrUpdate(players.Select(p => p with { Score = initialScore }));
            });

            actualPlayer = players.FirstOrDefault()!;

            playerRoundScore = new SourceCache<PlayerMove, int>(move => move.OrderNum);
            playerRoundScore.Edit(cache =>
                cache.AddOrUpdate(Enumerable
                    .Range(0, 3)
                    .Select(x => new PlayerMove(TargetButtonNum.None, TargetButtonType.None, x))));
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
    }
}
