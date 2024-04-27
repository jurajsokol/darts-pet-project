using System.Collections.ObjectModel;

namespace Darts.Games.Models
{
    public record PlayerMove(TargetButtonNum TargetButton, TargetButtonType TargetButtonType, int OrderNum)
    {
        internal int GetScore => (int)TargetButton * (int)TargetButtonType;
    }
}
