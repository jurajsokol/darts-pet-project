using ReactiveUI;
using System.Threading.Tasks;

namespace Darts.Avalonia.Views.Dialog
{
    public interface IDialogManager
    {
        Task<T> ShowDialog<T>() where T : ReactiveObject;
    }
}