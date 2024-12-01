using Avalonia.Controls;
using Avalonia.Media;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using System;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace Darts.Avalonia.Views.Dialog;

public abstract class DialogManagerBase
{
    protected readonly IServiceProvider serviceCollection;
    protected readonly Panel panel;

    public DialogManagerBase(IServiceProvider serviceCollection, Panel panel)
    {
        this.serviceCollection = serviceCollection;
        this.panel = panel;
    }

    public async Task<(DialogResult, T)> ShowDialog<T>() where T : ReactiveObject
    {
        DialogBase<T> dialog = serviceCollection.GetRequiredService<DialogBase<T>>();
        Control? c = panel.Children.FirstOrDefault();
        c.Effect = BlurEffect.Parse("blur(10)");
        panel.Children.Add(dialog);
        DialogResult result = await dialog.Show();
        panel.Children.Remove(dialog);
        c.Effect = null;
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
