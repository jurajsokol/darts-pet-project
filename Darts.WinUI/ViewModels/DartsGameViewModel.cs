using CommunityToolkit.Mvvm.ComponentModel;
using Darts.WinUI.Models;
using System.Collections.ObjectModel;

namespace Darts.WinUI.ViewModels
{
    public class DartsGameViewModel : ObservableObject
    {
        public ObservableCollection<Player> Players { get; } = new();
    }
}
