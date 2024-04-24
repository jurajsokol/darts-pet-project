using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Darts.DAL;
using Darts.Games.Games;
using Darts.WinUI.Models;
using Darts.WinUI.PageNavigation;
using Darts.WinUI.Views.DialogWindow;
using FluentResults;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Darts.WinUI.DependencyInjectionExtentions;

namespace Darts.WinUI.ViewModels
{
    public partial class CreateGameViewModel : ObservableObject
    {
        private IAbstractFactory<Player, IDialogWindow<Player>> playerDialogWindow;
        private IPageNavigation pageNavigation;
        private readonly IUnitOfWork db;

        public CreateGameViewModel(IAbstractFactory<Player, IDialogWindow<Player>> playerDialogWindow, IPageNavigation pageNavigation, IUnitOfWork db)
        {
            this.pageNavigation = pageNavigation;
            this.db = db;

            var players = db.Players.GetAll();
            players.Wait();

            Players = new(players.Result.Select(x => new Player() { Name = x.Name, OrderNumber = x.ID }));
            this.playerDialogWindow = playerDialogWindow;
            Players.CollectionChanged += (o, args) => StartGameCommand.NotifyCanExecuteChanged();
        }

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(StartGameCommand))]
        private GameTypeModel selectedGameType;

        public ObservableCollection<Player> Players { get; }
        public ObservableCollection<GameTypeModel> GameTypes { get; } = new ObservableCollection<GameTypeModel>(
            Enum.GetValues(typeof(GameTypes))
            .Cast<GameTypes>()
            .Select(x => new GameTypeModel(x)));

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(EditUserCommand))]
        [NotifyCanExecuteChangedFor(nameof(RemoveUserCommand))]
        private Player selectedPlayer;

        [RelayCommand]
        private async Task AddUser()
        {
            var dialog = playerDialogWindow.Create();
            if (await dialog.ShowDialog())
            {
                await db.Players.Add(new DAL.Entities.Player() { Name = dialog.ViewModel.Name });
                await db.CompleteAsync();
                Players.Add(new Player() { Name = dialog.ViewModel.Name, OrderNumber = Players.Count() + 1 });
            }
        }

        [RelayCommand(CanExecute = nameof(CanEditUser))]
        private async Task EditUser()
        {
            var dialog = playerDialogWindow.Create();
            if (await dialog.ShowDialog())
            { 
                SelectedPlayer.Name = dialog.ViewModel.Name;
            }
        }

        [RelayCommand(CanExecute = nameof(CanEditUser))]
        private void RemoveUser()
        {
            Players.Remove(SelectedPlayer);
        }

        public void ReorderPlayers()
        {
            foreach (var player in Players.Select((player, n) => new { player, n }))
            {
                player.player.OrderNumber = player.n +1;
            }
        }

        private bool CanEditUser()
        {
            return SelectedPlayer is not null;
        }

        [RelayCommand(CanExecute = nameof(CanStartGame))]
        private void StartGame()
        {
            pageNavigation.SetPage<DartsGameViewModel>();
        }

        private bool CanStartGame()
        {
            return Players.Any();
        }
    }
}
