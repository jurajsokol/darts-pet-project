using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Darts.WinUI.Enums;
using Darts.WinUI.Models;
using DynamicData;
using System.Collections.ObjectModel;
using System.Linq;

namespace Darts.WinUI.ViewModels
{
    public partial class DartsGameViewModel : ObservableObject
    {
        [ObservableProperty]
        private string actualThrowScore;

        public ObservableCollection<Player> Players { get; } = new();

        public ObservableCollection<PlayerThrow> PlayerRound { get; } = new();

        [ObservableProperty]
        private PlayerThrow selectedPlayerThrow;

        public DartsGameViewModel()
        {
            GenerateEmptyPLayerRound();
        }

        [RelayCommand]
        private void DartClick((DartNumbers number, DartsNumberType type) args)
        {
            SelectedPlayerThrow.Number = args.number;
            SelectedPlayerThrow.Type = args.type;
            SetNextPlayerThrow();
        }

        private void GenerateEmptyPLayerRound()
        {
            PlayerRound.Clear();
            PlayerRound.AddRange(Enumerable.Range(0, 3).Select(x => new PlayerThrow() { Index = x }));
            SelectedPlayerThrow = PlayerRound.FirstOrDefault();
        }

        private void SetNextPlayerThrow()
        {
            int nextThrowIndex = SelectedPlayerThrow.Index + 1;
            var nextThrow = PlayerRound.FirstOrDefault(x => x.Index == nextThrowIndex);
            if (nextThrow != null)
            { 
                SelectedPlayerThrow = nextThrow;
            }
        }
    }
}
