using Darts.Games.Enums;

namespace Darts.Games.Models;

public record PlayerMove(TargetButtonNum TargetButton, TargetButtonType TargetButtonType, int OrderNum)
{
    internal int GetScore => (int)TargetButton * (int)TargetButtonType;

    public bool IsEmpty => TargetButton == TargetButtonNum.None;

    public override string ToString()
    {
        if (TargetButton == TargetButtonNum.Miss)
        {
            return "Miss";
        }

        return TargetButtonType switch
        {
            TargetButtonType.None => $"Dart {OrderNum+1}",
            TargetButtonType.Single => ButtonNumberAsString(),
            TargetButtonType.Double => $"D {ButtonNumberAsString()}",
            TargetButtonType.Triple => $"T {ButtonNumberAsString()}",

            _ => ButtonNumberAsString()
        };
    }

    private string ButtonNumberAsString()
    {
        return ((int)TargetButton).ToString();
    }
}
