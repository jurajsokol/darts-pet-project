using FluentResults;
using System.Threading.Tasks;

namespace Darts.WinUI.DialogWindow
{
    public interface IDialogWindow<TIn, TOut>
    {
        Task<Result<TOut>> ShowWindow(TIn value);
    }
}
