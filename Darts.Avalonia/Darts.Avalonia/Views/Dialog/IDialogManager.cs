using ReactiveUI;
using System;
using System.Threading.Tasks;

namespace Darts.Avalonia.Views.Dialog
{
    public interface IDialogManager
    {
        Task<(DialogResult, T)> ShowDialog<T>() where T : ReactiveObject;

        IObservable<(DialogResult, T)> ShowDialogReactive<T>() where T : ReactiveObject;
    }
}