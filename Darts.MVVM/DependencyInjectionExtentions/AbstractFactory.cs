namespace Darts.MVVM.DependencyInjectionExtentions;

public class AbstractFactory<T> : IAbstractFactory<T>
{
    private readonly Func<T> factory;

    public AbstractFactory(Func<T> factory)
    {
        this.factory = factory;
    }

    public T Create() => factory();
}

public class AbstractFactory<TIn, TOut> : IAbstractFactory<TIn, TOut> where TIn : new()
{
    private readonly Func<TIn, TOut> factory;

    public AbstractFactory(Func<TIn, TOut> factory)
    {
        this.factory = factory;
    }

    public TOut Create(TIn args) => factory(args);

    public TOut Create() => factory(new TIn());
}
