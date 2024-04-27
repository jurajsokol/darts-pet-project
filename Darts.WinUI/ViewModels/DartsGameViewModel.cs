using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Darts.Games.Games;
using Darts.Games.Models;
using Darts.WinUI.Enums;
using Darts.WinUI.Models;
using DynamicData;
using DynamicData.Binding;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace Darts.WinUI.ViewModels
{
    public partial class DartsGameViewModel : ObservableObject, IDisposable
    {
        private CompositeDisposable disposables = new();
        private IDartGame game;

        [ObservableProperty]
        private string actualThrowScore;

        private ObservableCollectionExtended<Darts.Games.Models.Player> players = new();
        public ObservableCollection<Darts.Games.Models.Player> Players => players;

        public ObservableCollection<Games.Models.PlayerMove> PlayerRound => playerRound;
        private ObservableCollectionExtended<Games.Models.PlayerMove> playerRound = new();

        public DartsGameViewModel(INewDartGameArgs newGameArgs, IDartGameFactory gameFactory, CurrentThreadScheduler guiScheduler)
        {
            game = gameFactory.GetGame(newGameArgs.GameType, newGameArgs.GamePlayers.Select(x => x.ToDartPlayer()).ToArray());
            game.Players
                .ObserveOn(guiScheduler)
                .Bind(players)
                .Subscribe()
                .DisposeBy(disposables);

            game.PLayerRoundScore
                .Sort(SortExpressionComparer<PlayerMove>.Ascending(p => p.OrderNum))
                .ObserveOn(guiScheduler)
                .Bind(playerRound)
                .Subscribe()
                .DisposeBy(disposables);
        }

        [RelayCommand]
        private void DartClick((DartNumbers number, DartsNumberType type) args)
        {
            game.PlayerMove(args.number.ToGameType(), args.type.ToGameType());
        }

        public void Dispose()
        {
            disposables.Dispose();
        }
    }
}
