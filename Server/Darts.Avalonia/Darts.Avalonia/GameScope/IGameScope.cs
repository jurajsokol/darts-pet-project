using Darts.Avalonia.Models;
using System.Collections.Generic;

namespace Darts.Avalonia.GameScope;

public interface IGameScope
{
    public abstract void StartSetup();

    public abstract void StartGame();

    public void ExitGame();

    public void ReturnToGame();

    public void ShowWinnersView(Player[] players);
}
