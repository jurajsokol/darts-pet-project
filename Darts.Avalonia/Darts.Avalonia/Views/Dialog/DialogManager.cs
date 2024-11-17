using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
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

    public async Task<(DialogResult, T)> ShowDialog<T>() where T : ObservableObject
    {
        DialogBase<T> dialog = serviceCollection.GetRequiredService<DialogBase<T>>();

        panel.Children.Add(dialog);
        DialogResult result = await dialog.Show();
        panel.Children.Remove(dialog);
        return (result, dialog.ViewModel);
    }

}
