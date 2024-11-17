﻿using System;

namespace Darts.Avalonia.Controls.DartControl;

public delegate void DartButtonClickEventHandler(object sender, DartButtonClickEventArgs e);

public class DartButtonClickEventArgs : EventArgs
{
    public DartNumbers Number { get; }
    public DartsNumberType Type { get; }

    public DartButtonClickEventArgs(DartNumbers number, DartsNumberType type)
    {
        Number = number;
        Type = type;
    }
}
