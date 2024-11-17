using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace Darts.Avalonia.Views.Dialog;

public class ReactiveDialogManager : IReactiveDialogManager
{
    private readonly Panel panel;
    private readonly IServiceProvider serviceCollection;

    public ReactiveDialogManager(Panel panel, IServiceProvider serviceCollection)
    {
        this.panel = panel;
        this.serviceCollection = serviceCollection;
    }

    public IObservable<(DialogResult, T)> ShowDialog<T>() where T : ReactiveObject
    {
        return Observable.Create<(DialogResult, T)>(o =>
        {
            ReactiveDialogBase<T> dialog = serviceCollection.GetRequiredService<ReactiveDialogBase<T>>();
            IObservable<(DialogResult, T)> result = dialog.Show()
                .Publish()
                .RefCount();
                
            panel.Children.Add(dialog);

            return new CompositeDisposable(
                result.Subscribe(_ => { }),
                result.Subscribe(o));
        });
    }
}
