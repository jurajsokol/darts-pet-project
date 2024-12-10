namespace Darts.Avalonia.Factories;

public interface IAbstractFactory<T>
{
    T Create();
}