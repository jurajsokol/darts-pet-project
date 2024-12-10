using System;

namespace Darts.Avalonia.Factories;

public class AbstractFactory<T> : IAbstractFactory<T>
{
    private Func<T> factory;

    public AbstractFactory(Func<T> factory)
    {
        this.factory = factory;
    }

    public T Create()
    {
        return factory();
    }
}
