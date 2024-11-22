using Darts.Avalonia.Models;
using Darts.Avalonia.ViewRouting;
using Darts.Avalonia.Views;
using ReactiveUI;
using ReactiveUI.SourceGenerators;
using System.Collections.ObjectModel;

namespace Darts.Avalonia.ViewModels;

public partial class DartGameX01ViewModel : ReactiveObject
{
    private readonly IPageNavigation pageNavigation;

    public ObservableCollection<GamePlayer> Players { get; } = new ObservableCollection<GamePlayer>([ new GamePlayer("player1", 501, 1, true) ]);

    public DartGameX01ViewModel(IPageNavigation pageNavigation)
    {
        this.pageNavigation = pageNavigation;
    }

    [ReactiveCommand]
    private void PlayerScore(DartScore score)
    { 
        
    }

    public void CancelGame()
    {
        pageNavigation.GoBack();
    }
}
