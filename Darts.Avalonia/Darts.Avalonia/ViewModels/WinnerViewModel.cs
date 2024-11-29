using Darts.Avalonia.ViewRouting;
using Darts.Games.Models;
using ReactiveUI;
using ReactiveUI.SourceGenerators;

namespace Darts.Avalonia.ViewModels;

public partial class WinnerViewModel : ReactiveObject
{
    private readonly IPageNavigation pageNavigation;

    public Player[] Players { get; }
    public WinnerViewModel(IPageNavigation pageNavigation, Player[] players)
    {
        this.pageNavigation = pageNavigation;
        Players = players;
    }

    [ReactiveCommand]
    private void GoBack()
    { 
        pageNavigation.GoBack();
    }

    [ReactiveCommand]
    private void NewGame()
    { 
        
    }

    [ReactiveCommand]
    private void GoToMainMenu()
    { 
        
    }
}
