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

public class DialogScope<T> : IDialogScope<T> where T : ReactiveObject
{
    private readonly IServiceScope serviceScope;
    protected readonly Panel panel;

    public T ViewModel { get; }

    public DialogScope(IServiceScope serviceScope, Panel panel)
    {
        this.serviceScope = serviceScope;
        ViewModel = serviceScope.ServiceProvider.GetRequiredService<T>();
        this.panel = panel;
    }

    public async Task<DialogResult> ShowDialog()
    {
        DialogBase<T> dialog = serviceScope.ServiceProvider.GetRequiredService<DialogBase<T>>();
        Control? c = panel.Children.FirstOrDefault();
        c.Effect = BlurEffect.Parse("blur(10)");
        panel.Children.Add(dialog);
        DialogResult result = await dialog.Show();
        panel.Children.Remove(dialog);
        c.Effect = null;
        return result;
    }

    public IObservable<DialogResult> ShowDialogReactive()
    {
        return Observable.Create<DialogResult>(o =>
        {
            DialogBase<T> dialog = serviceScope.ServiceProvider.GetRequiredService<DialogBase<T>>();
            IObservable<DialogResult> result = dialog.ShowReactive()
                .Publish()
                .RefCount();

            panel.Children.Add(dialog);

            return new CompositeDisposable(
                result.Subscribe(_ => { }),
                result.Subscribe(o));
        });
    }

    public void Dispose()
    {
        serviceScope.Dispose();
    }
}
