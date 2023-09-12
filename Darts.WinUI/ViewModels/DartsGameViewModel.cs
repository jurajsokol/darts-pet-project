using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Darts.WinUI.Enums;
using Darts.WinUI.Models;
using System;
using System.Collections.ObjectModel;

namespace Darts.WinUI.ViewModels
{
    public partial class DartsGameViewModel : ObservableObject
    {
        public ObservableCollection<Player> Players { get; } = new();

        [RelayCommand]
        private void DartClick((DartNumbers number, DartsNumberType type) args)
        {
            
        }
    }
}
