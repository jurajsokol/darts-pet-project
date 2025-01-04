namespace Darts.Avalonia.GameScope;

public interface IGameScope
{
    public abstract void StartSetup();

    public abstract void StartGame();

    public void ExitGame();
}
