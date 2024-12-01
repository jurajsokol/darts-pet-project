using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using System;
using System.Threading.Tasks;

namespace Darts.Avalonia.Views.Dialog;

public class DialogScope<T> : DialogManagerBase, IDialogScope<T> where T : ReactiveObject
{
    private readonly IServiceScope serviceScope;

    public T ViewModel { get; }

    public DialogScope(IServiceScope serviceScope, Panel panel)
        :base (serviceScope.ServiceProvider, panel)
    {
        this.serviceScope = serviceScope;
        ViewModel = serviceScope.ServiceProvider.GetRequiredService<T>();
    }

    public void Dispose()
    {
        serviceScope.Dispose();
    }

    public Task<(DialogResult, T)> ShowDialog()
    {
        return base.ShowDialog<T>();
    }

    public IObservable<(DialogResult, T)> ShowDialogReactive()
    {
        return base.ShowDialogReactive<T>();
    }
}
