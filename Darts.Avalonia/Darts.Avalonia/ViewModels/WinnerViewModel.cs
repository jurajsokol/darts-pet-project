using Darts.Avalonia.Factories;
using Darts.Avalonia.GameScope;
using Darts.Avalonia.Models;
using ReactiveUI;
using ReactiveUI.SourceGenerators;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Darts.Avalonia.ViewModels;

public partial class WinnerViewModel : ReactiveObject
{
    private readonly IGameScope gameScope;
    private readonly GameConfiguration configuration;

    public ObservableCollection<Player> Players { get; } = new ObservableCollection<Player>();

    public WinnerViewModel(IGameScope gameScope, GameConfiguration configuration)
    {
        this.gameScope = gameScope;
        this.configuration = configuration;
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
        Player[] players = Players.ToArray();
        Array.Reverse<Player>(players);

        configuration.Players = players
            .Select((Player x, int c) =>
            {
                x.OrderNumber = c;
                return x;
            })
            .ToArray();

        gameScope.StartGame();
    }
}
