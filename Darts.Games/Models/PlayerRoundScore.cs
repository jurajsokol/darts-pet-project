using System.Collections.ObjectModel;

namespace Darts.Games.Models
{
    public class PlayerRoundScore
    {
        private (int ThrowNumber, TargetButtonNum TargetButton, TargetButtonType TargetButtonType)[] roundScore { get; } = new (int ThrowNumber, TargetButtonNum TargetButton, TargetButtonType TargetButtonType)[3];

        public IReadOnlyCollection<(int ThrowNumber, TargetButtonNum TargetButton, TargetButtonType TargetButtonType)> RoundScore => new ReadOnlyCollection<(int ThrowNumber, TargetButtonNum TargetButton, TargetButtonType TargetButtonType)>(roundScore);

        internal int GetRoundScore()
        {
            return RoundScore.Select(x => (int)x.TargetButton * (int)x.TargetButtonType).Sum();
        }

        public void SetRoundScore(int roundNumber, TargetButtonNum targetButton, TargetButtonType targetButtonType)
        {
            // forbidden combination
            if (targetButton == TargetButtonNum.BullsEye && targetButtonType == TargetButtonType.Triple)
            {
                return;
            }

            int fieldIndex = roundNumber - 1;
            if (fieldIndex > 2 || fieldIndex < 0)
            {
                return;
            }

            roundScore[fieldIndex] = (roundNumber, targetButton, targetButtonType);
        }
    }
}
