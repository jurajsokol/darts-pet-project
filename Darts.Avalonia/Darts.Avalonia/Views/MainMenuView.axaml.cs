using Avalonia.Controls;
using Avalonia.Interactivity;
using Darts.Avalonia.Views;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Darts.Avalonia;

public partial class MainMenuView : UserControl
{
    private readonly IServiceProvider services;

    public MainMenuView(IServiceProvider services)
    {
        InitializeComponent();
        this.services = services;
        MainBoard.Content = services.GetRequiredService<CreateGameView>();
    }

    private void GameButton_Click(object? sender, RoutedEventArgs e)
    {
        MainBoard.Content = services.GetRequiredService<CreateGameView>();
    }

    private void PlayersButton_Click(object? sender, RoutedEventArgs e)
    {
        MainBoard.Content = services.GetRequiredService<PlayersView>();
    }

    private void SettingsButton_Click(object? sender, RoutedEventArgs e)
    {
        MainBoard.Content = services.GetRequiredService<SettingsView>();
    }
}