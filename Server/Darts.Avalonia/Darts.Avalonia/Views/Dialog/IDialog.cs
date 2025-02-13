using ReactiveUI;
using System.Threading.Tasks;

namespace Darts.Avalonia.Views.Dialog
{
    public interface IDialog<T> where T : ReactiveObject
    {
        T ViewModel { get; }

        Task Show();
    }
}