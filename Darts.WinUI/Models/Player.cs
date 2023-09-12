using CommunityToolkit.Mvvm.ComponentModel;

namespace Darts.WinUI.Models
{
    public partial class Player : ObservableObject
    {
        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private int orderNumber;
    }
}
