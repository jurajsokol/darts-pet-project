using Darts.Avalonia.GameScope;
using Darts.Avalonia.Models;
using ReactiveUI;
using ReactiveUI.SourceGenerators;
using System.Collections.ObjectModel;

namespace Darts.Avalonia.ViewModels;

public partial class WinnerViewModel : ReactiveObject
{
    public ObservableCollection<Player> Players { get; } = new ObservableCollection<Player>();
    private IGameScope gameScope { get; }

    public WinnerViewModel(IGameScope gameScope)
    {
        this.gameScope = gameScope;
    }

    [ReactiveCommand]
    public void Undo()
    {
        gameScope.ReturnToGame();
    }

    [ReactiveCommand]
    public void CancelGame()
    {
        gameScope.ExitGame();
    }

    [ReactiveCommand]
    public void NewGame()
    {

    }
}
