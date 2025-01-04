namespace Darts.Avalonia.Factories;

public interface IAbstractFactory<T>
{
    T Create();
}

public interface IAbstractFactory<TArgs, T>
{
    T Create(TArgs args);
}