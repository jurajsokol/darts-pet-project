using System.Reactive.Disposables;

namespace Darts.MVVM;

public static class RXExtensions
{
    public static IDisposable DisposeWith(this IDisposable disposable, CompositeDisposable disposablesCollection)
    { 
        disposablesCollection.Add(disposable);
        return disposable;
    }
}
