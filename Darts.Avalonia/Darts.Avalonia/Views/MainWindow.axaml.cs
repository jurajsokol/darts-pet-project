using Avalonia;
using Avalonia.Controls;
using System.Linq;

namespace Darts.Avalonia.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

#if DEBUG
        this.AttachDevTools();
#endif
    }
}