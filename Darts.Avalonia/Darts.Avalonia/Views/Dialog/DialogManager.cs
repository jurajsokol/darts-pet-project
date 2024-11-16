using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using System;
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

    public async Task<T> ShowDialog<T>() where T : ReactiveObject
    {
        IDialog<T> dialog = serviceCollection.GetRequiredService<IDialog<T>>();

        panel.Children.Add(dialog as Control);
        await (dialog as AddPlayerView).Show();
        panel.Children.Remove(dialog as Control);
        return dialog.ViewModel;
    }

}
