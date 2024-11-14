using CommunityToolkit.Mvvm.ComponentModel;

namespace Darts.MVVM.Models;

public partial class Player : ObservableObject
{
    public int ID { get; init; }

    [ObservableProperty]
    private string name = string.Empty;

    [ObservableProperty]
    private int orderNumber;
}
