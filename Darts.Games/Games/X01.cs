using Darts.Games.Models;
using DynamicData;

namespace Darts.Games.Games
{
    internal class X01 : IDartGame
    {
        private SourceCache<Player, int> playersCollection;
        private Player actualPlayer;

        public Player ActualPLayer => actualPlayer;

        public X01(IReadOnlyList<Player> players, uint initialScore)
        {
            playersCollection = new SourceCache<Player, int>(player => player.PlayerOrder);
            playersCollection.Edit(cache =>
            { 
                foreach (var player in players)
                {
                    player.Score = initialScore;
                }

                cache.AddOrUpdate(players);
            });
        }

        public void PlayerMove(PlayerRoundScore score)
        {
            int scoreNumber = score.GetRoundScore();
            if (actualPlayer.Score > scoreNumber)
            {
                actualPlayer.Score -= (uint)scoreNumber;
            }
            else if (actualPlayer.Score == scoreNumber)
            {
                // winner
            }
        }
    }
}
