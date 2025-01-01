using Darts.Avalonia.Enums;
using System;

namespace Darts.Avalonia;

public static class ConvertExtensions
{
    public static Models.Player ToModel(this DAL.Entities.Player data)
    {
        return new Models.Player()
        {
            ID = data.ID,
            Name = data.Name,
        };
    }

    public static DAL.Entities.Player ToDalPLayer(this Models.Player data)
    {
        return new DAL.Entities.Player()
        {
            ID = data.ID,
            Name = data.Name,
        };
    }

    public static Darts.Games.Models.Player ToDartPlayer(this Models.Player data, int count)
    {
        return new Games.Models.Player(data.Name, 0, count, false);
    }

    public static Models.Player ToModel(this Games.Models.Player data)
    {
        return new Models.Player()
        {
            Name = data.PlayerName,
        };
    }

    public static Games.Enums.TargetButtonType ToGameType(this DartsNumberModifier type)
    {
        return type switch
        {
            DartsNumberModifier.Triple => Games.Enums.TargetButtonType.Triple,
            DartsNumberModifier.Double => Games.Enums.TargetButtonType.Double,
            DartsNumberModifier.Single => Games.Enums.TargetButtonType.Single,

            _ => throw new NotSupportedException($"Type {type} cannot be converted"),
        };
    }

    public static Games.Enums.TargetButtonNum ToGameType(this DartNumbers number)
    {
        return number switch
        {
            DartNumbers.Miss => Games.Enums.TargetButtonNum.Miss,
            DartNumbers.One => Games.Enums.TargetButtonNum.One,
            DartNumbers.Two => Games.Enums.TargetButtonNum.Two,
            DartNumbers.Three => Games.Enums.TargetButtonNum.Three,
            DartNumbers.Four => Games.Enums.TargetButtonNum.Four,
            DartNumbers.Five => Games.Enums.TargetButtonNum.Five,
            DartNumbers.Six => Games.Enums.TargetButtonNum.Six,
            DartNumbers.Seven => Games.Enums.TargetButtonNum.Seven,
            DartNumbers.Eight => Games.Enums.TargetButtonNum.Eight,
            DartNumbers.Nine => Games.Enums.TargetButtonNum.Nine,
            DartNumbers.Ten => Games.Enums.TargetButtonNum.Ten,
            DartNumbers.Eleven => Games.Enums.TargetButtonNum.Eleven,
            DartNumbers.Twelve => Games.Enums.TargetButtonNum.Twelve,
            DartNumbers.Thirteen => Games.Enums.TargetButtonNum.Thirteen,
            DartNumbers.Fourteen => Games.Enums.TargetButtonNum.Fourteen,
            DartNumbers.Fifteen => Games.Enums.TargetButtonNum.Fifteen,
            DartNumbers.Sixteen => Games.Enums.TargetButtonNum.Sixteen,
            DartNumbers.Seventeen => Games.Enums.TargetButtonNum.Seventeen,
            DartNumbers.Eighteen => Games.Enums.TargetButtonNum.Eighteen,
            DartNumbers.Nineteen => Games.Enums.TargetButtonNum.Nineteen,
            DartNumbers.Twenty => Games.Enums.TargetButtonNum.Twenty,
            DartNumbers.BullsEye => Games.Enums.TargetButtonNum.BullsEye,

            _ => throw new NotSupportedException($"Number {number} cannot be converted"),
        };
    }
}
