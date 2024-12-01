using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using System;

namespace Darts.Avalonia.Views.Dialog;

public class DialogManager : DialogManagerBase, IDialogManager
{
    public DialogManager(Panel panel, IServiceProvider serviceCollection)
        : base(serviceCollection, panel)
    { }

    public IDialogScope<T> CreateDialogScope<T>() where T : ReactiveObject
    {
        return serviceCollection.GetRequiredService<IDialogScope<T>>();
    }
}
