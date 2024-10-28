using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Darts.DAL;
using Darts.MVVM.DependencyInjectionExtentions;
using Darts.MVVM.Models;
using System.Collections.ObjectModel;

namespace Darts.MVVM.ViewModels;

public partial class EditPlayersViewModel : ObservableObject
{
    private readonly IAbstractFactory<Player, IDialogWindow<Player>> playerDialogWindow;
    private readonly IUnitOfWork db;

    public ObservableCollection<Player> Players { get; } = new();

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(EditUserCommand))]
    [NotifyCanExecuteChangedFor(nameof(RemoveUserCommand))]
    private Player selectedPlayer;

    public EditPlayersViewModel(IAbstractFactory<Player, IDialogWindow<Player>> playerDialogWindow, IUnitOfWork db) 
    {
        this.playerDialogWindow = playerDialogWindow;
        this.db = db;
    }

    public async Task LoadPlayers()
    {
        IEnumerable<Player> players = (await db.Players.GetAll()).Select(x => x.ToModel());
        foreach (Player player in players)
        {
            Players.Add(player);
        }
    }

    [RelayCommand]
    private async Task AddUser()
    {
        var dialog = playerDialogWindow.Create();
        if (await dialog.ShowDialog())
        {
            var addedPlayer = await db.Players.Add(dialog.ViewModel.ToDalPLayer());
            await db.CompleteAsync();
            Players.Add(addedPlayer.ToModel());
        }
    }

    [RelayCommand(CanExecute = nameof(CanEditUser))]
    private async Task EditUser()
    {
        var dialog = playerDialogWindow.Create();
        dialog.ViewModel.Name = SelectedPlayer.Name;
        if (await dialog.ShowDialog())
        {
            (await db.Players.GetById(SelectedPlayer.ID)).Name = dialog.ViewModel.Name;
            await db.CompleteAsync();
            SelectedPlayer.Name = dialog.ViewModel.Name;
        }
    }

    [RelayCommand(CanExecute = nameof(CanEditUser))]
    private async Task RemoveUser()
    {
        db.Players.Delete(SelectedPlayer.ID);
        await db.CompleteAsync();
        Players.Remove(SelectedPlayer);
    }

    private bool CanEditUser()
    {
        return SelectedPlayer is not null;
    }
}
