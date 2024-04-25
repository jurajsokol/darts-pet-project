using CommunityToolkit.Mvvm.ComponentModel;

namespace Darts.WinUI.Models
{
    public partial class Player : ObservableObject
    {
        public int ID { get; init; }

        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private int orderNumber;
    }
}
