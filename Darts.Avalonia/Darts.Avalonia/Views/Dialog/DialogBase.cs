using Avalonia.Controls;
using ReactiveUI;
using System.Reactive.Linq;
using System;
using System.Threading.Tasks;
using System.Reactive.Disposables;

namespace Darts.Avalonia.Views.Dialog;

public abstract class DialogBase<T> : UserControl where T : ReactiveObject
{
    private TaskCompletionSource<DialogResult>? closeDialog;
    private IObserver<DialogResult>? observer;

    public T ViewModel { get; }

    public DialogBase(T viewModel)
    {
        ViewModel = viewModel;
        DataContext = viewModel;
    }

    public Task<DialogResult> Show()
    {
        closeDialog = new TaskCompletionSource<DialogResult>();
        return closeDialog.Task;
    }

    public IObservable<DialogResult> ShowReactive()
    {
        return Observable.Create<DialogResult>(o =>
        {
            observer = o;
            return Disposable.Empty;
        });
    }

    public void Close(DialogResult result)
    {
        closeDialog?.SetResult(result);
        observer?.OnNext(result);
        observer?.OnCompleted();
    }
}
