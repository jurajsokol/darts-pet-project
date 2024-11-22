using Darts.Avalonia.Enums;
using System;

namespace Darts.Avalonia.Controls.DartControl;

public delegate void DartButtonClickEventHandler(object sender, DartButtonClickEventArgs e);

public class DartButtonClickEventArgs : EventArgs
{
    public DartNumbers Number { get; }
    public DartsNumberModifier Type { get; }

    public DartButtonClickEventArgs(DartNumbers number, DartsNumberModifier type)
    {
        Number = number;
        Type = type;
    }
}
