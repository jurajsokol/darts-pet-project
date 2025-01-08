using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.ReactiveUI;
using Darts.Avalonia.Enums;
using Darts.Avalonia.ViewModels;
using Darts.Avalonia.Views;
using ReactiveUI;
using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace Darts.Avalonia;

public partial class CricketGameView : ReactiveUserControl<CricketGameViewModel>
{
    public CricketGameView(CricketGameViewModel viewModel)
    {
        DataContext = viewModel;
        InitializeComponent();
    }
}