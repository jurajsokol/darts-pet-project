using CommunityToolkit.Mvvm.ComponentModel;
using ReactiveUI;
using System;
using System.Threading.Tasks;

namespace Darts.Avalonia.Views.Dialog
{
    public interface IDialogManager
    {
        Task<(DialogResult, T)> ShowDialog<T>() where T : ObservableObject;
    }

    public interface IReactiveDialogManager
    {
        IObservable<(DialogResult, T)> ShowDialog<T>() where T : ReactiveObject;
    }
}