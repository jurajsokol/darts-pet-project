namespace Darts.MVVM.DependencyInjectionExtentions;

public interface IDialogWindow<T>
{
    T ViewModel { get; }
    Task<bool> ShowDialog();
}
