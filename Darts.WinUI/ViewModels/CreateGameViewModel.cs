using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Darts.Games.Games;
using Darts.WinUI.DialogWindow;
using Darts.WinUI.Models;
using Darts.WinUI.PageNavigation;
using FluentResults;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Darts.WinUI.ViewModels
{
    public partial class CreateGameViewModel : ObservableObject
    {
        private IDialogWindow<string, string> playerDialogWindow;
        private string localFolderPath;
        private IPageNavigation pageNavigation;

        public CreateGameViewModel(IDialogWindow<string, string> playerDialogWindow, IPageNavigation pageNavigation)
        {
            this.pageNavigation = pageNavigation;
            Players = new();
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
            Result<string> name = await playerDialogWindow.ShowWindow(string.Empty);

            if (name.IsSuccess)
            {
                Players.Add(new Player() { Name = name.Value, OrderNumber = Players.Count() + 1 });

            }
        }

        [RelayCommand(CanExecute = nameof(CanEditUser))]
        private async Task EditUser()
        {
            Result<string> name = await playerDialogWindow.ShowWindow(SelectedPlayer.Name);

            if (name.IsSuccess)
            {
                SelectedPlayer.Name = name.Value;
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

            pageNavigation.SetPage(PageEnums.DartGamePage);
        }

        private bool CanStartGame()
        {
            return Players.Any();
        }
    }
}
