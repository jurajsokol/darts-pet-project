using Darts.Avalonia.Models;
using Darts.Games.Enums;

namespace Darts.Avalonia.Factories
{
    public class GameConfiguration
    {
        public GameTypes GameType { get; set; }
        public Player[] Players { get; set; } = [];
    }
}
