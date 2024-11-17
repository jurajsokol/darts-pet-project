using Avalonia.ReactiveUI;
using ReactiveUI;
using System.Reactive.Linq;
using System;
using System.Reactive.Disposables;

namespace Darts.Avalonia.Views.Dialog;

public abstract class ReactiveDialogBase<T> : ReactiveUserControl<T> where T : ReactiveObject
{
    private IObserver<(DialogResult, T)>? observer;

    public new T ViewModel { get; }

    public ReactiveDialogBase(T viewModel)
    {
        ViewModel = viewModel;
        DataContext = viewModel;
    }

    public IObservable<(DialogResult, T)> Show()
    {
        return Observable.Create<(DialogResult, T)>(o =>
        {
            observer = o;
            return Disposable.Empty;
        });
    }

    public void Close(DialogResult dialogResult)
    {
        if (observer is not null)
        {
            observer.OnNext((dialogResult, ViewModel));
            observer.OnCompleted();
        }
    }
}
