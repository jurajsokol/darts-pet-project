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

public class AbstractFactory<TArgs, T> : IAbstractFactory<TArgs, T>
{
    private Func<TArgs, T> factory;

    public AbstractFactory(Func<TArgs, T> factory)
    {
        this.factory = factory;
    }

    public T Create(TArgs args)
    {
        return factory(args);
    }
}
