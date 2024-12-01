using ReactiveUI;
using System;
using System.Threading.Tasks;

namespace Darts.Avalonia.Views.Dialog
{
    public interface IDialogScope<T> : IDisposable where T : ReactiveObject
    {
        T ViewModel { get; }

        Task<(DialogResult, T)> ShowDialog();

        IObservable<(DialogResult, T)> ShowDialogReactive();
    }
}