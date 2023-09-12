using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Darts.DartGames
{
    internal class Player
    {
        public string PLayerName { get; }
        public int ActualScore { get; set; }

        public Player(string playerName, int initialScore)
        {
            ActualScore = initialScore;
            PLayerName = playerName;
        }
    }
}
