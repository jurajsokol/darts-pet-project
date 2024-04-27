using System;
using System.Reactive.Disposables;

namespace Darts.WinUI
{
    public static class RXExtensions
    {
        public static T DisposeWith<T>(this T disposable, CompositeDisposable disposablesCollection) where T : IDisposable
        { 
            disposablesCollection.Add(disposable);
            return disposable;
        }
    }
}
