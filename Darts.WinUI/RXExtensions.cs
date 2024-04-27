using System;
using System.Reactive.Disposables;

namespace Darts.WinUI
{
    public static class RXExtensions
    {
        public static void DisposeBy(this IDisposable disposable, CompositeDisposable disposablesCollection)
        { 
            disposablesCollection.Add(disposable);
        }
    }
}
