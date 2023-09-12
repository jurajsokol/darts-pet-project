using System.Collections.Generic;
using System.Linq;

namespace Darts.DartGames
{
    internal class X01
    {
        private IList<Player> players;
        private uint rounds;

        public uint Rounds => rounds;
        private Player ActualPlayer => players.GetActualPlayer();

        internal X01(IList<Player> players) 
        {
            this.players = players;
        }

        public void PLayerRoundScore(IList<(TargetButtonNum, TargetButtonType)> playerScore)
        {
            Player actualPlayer = ActualPlayer;

            int score = GetScore(playerScore);
            if (score < actualPlayer.ActualScore)
            {
                actualPlayer.ActualScore -= score;
            }
            else if (score == actualPlayer.ActualScore)
            {
               // vyhráva actuálny hráč
            }

            rounds++;
        }

        private int GetScore(IList<(TargetButtonNum ButtonNum, TargetButtonType ButtonType)> score)
        {
            return score.Select(throwScore => (int)throwScore.ButtonNum * (int)throwScore.ButtonType).Sum();
        }
    }
}
