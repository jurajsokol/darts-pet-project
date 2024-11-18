using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace Darts.Avalonia.Views.Dialog;

public class DialogManager : IDialogManager
{
    private readonly Panel panel;
    private readonly IServiceProvider serviceCollection;

    public DialogManager(Panel panel, IServiceProvider serviceCollection)
    {
        this.panel = panel;
        this.serviceCollection = serviceCollection;
    }

    public async Task<(DialogResult, T)> ShowDialog<T>() where T : ReactiveObject
    {
        DialogBase<T> dialog = serviceCollection.GetRequiredService<DialogBase<T>>();

        panel.Children.Add(dialog);
        DialogResult result = await dialog.Show();
        panel.Children.Remove(dialog);
        return (result, dialog.ViewModel);
    }

    public IObservable<(DialogResult, T)> ShowDialogReactive<T>() where T : ReactiveObject
    {
        return Observable.Create<(DialogResult, T)>(o =>
        {
            DialogBase<T> dialog = serviceCollection.GetRequiredService<DialogBase<T>>();
            IObservable<(DialogResult, T)> result = dialog.ShowReactive()
                .Publish()
                .RefCount();

            panel.Children.Add(dialog);

            return new CompositeDisposable(
                result.Subscribe(_ => { }),
                result.Subscribe(o));
        });
    }
}
