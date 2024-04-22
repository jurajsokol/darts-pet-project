using System.Threading.Tasks;

namespace Darts.WinUI.Views.DialogWindow
{
    public interface IDialogWindow<T>
    {
        T ViewModel { get; }
        Task<bool> ShowDialog();
    }
}
