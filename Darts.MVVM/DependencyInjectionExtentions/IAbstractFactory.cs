namespace Darts.MVVM.DependencyInjectionExtentions
{
    public interface IAbstractFactory<T>
    {
        T Create();
    }

    public interface IAbstractFactory<TIn, TOut>
    {
        TOut Create(TIn args);
        TOut Create();
    }
}
