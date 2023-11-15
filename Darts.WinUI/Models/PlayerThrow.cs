using CommunityToolkit.Mvvm.ComponentModel;
using Darts.WinUI.Enums;

namespace Darts.WinUI.Models
{
    public partial class PlayerThrow : ObservableObject
    {
        public int Index { get; init; }

        [ObservableProperty]
        private DartNumbers number;

        [ObservableProperty]
        private DartsNumberType type;
    }
}
