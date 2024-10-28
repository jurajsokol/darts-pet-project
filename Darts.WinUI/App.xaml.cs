using Darts.DAL;
using Darts.Games.Games;
using Darts.MVVM.DependencyInjectionExtentions;
using Darts.MVVM.Models;
using Darts.MVVM.PageNavigation;
using Darts.MVVM.ViewModels;
using Darts.MVVM.Views;
using Darts.MVVM.Views.DialogWindow;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.IO;
using System.Reactive.Concurrency;
using Windows.Storage;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Darts.MVVM
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        private Window m_window;
        public Frame RootFrame { get; } = new Frame();
        public Window MainWindow => m_window;

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            Services = ConfigureServices();
            CreateDB(Services);
            
            this.InitializeComponent();
        }

        /// <summary>
        /// Gets the current <see cref="App"/> instance in use
        /// </summary>
        public new static App Current => (App)Application.Current;

        /// <summary>
        /// Gets the <see cref="IServiceProvider"/> instance to resolve application services.
        /// </summary>
        public IServiceProvider Services { get; }

        /// <summary>
        /// Configures the services for the application.
        /// </summary>
        private IServiceProvider ConfigureServices()
        {
            CurrentThreadScheduler guiScheduler = Scheduler.CurrentThread;

            var services = new ServiceCollection();

            string dbPath = Path.Join(ApplicationData.Current.LocalFolder.Path, "darts.db");

            services.AddDatabase(dbPath)
                .AddSingleton<CurrentThreadScheduler>(_ => guiScheduler)
                .AddSingleton<IPageNavigation>(_ => new PageNavigation.PageNavigation(RootFrame))
                .AddFactory<Player, IDialogWindow<Player>>(model => new AddPlayerPage(model))
                .AddSingleton<CreateGameViewModel>()
                .AddSingleton<INewDartGameArgs, CreateGameViewModel>(services => services.GetService<CreateGameViewModel>())
                .AddSingleton<IDartGameFactory, DartGameFactory>()
                .AddTransient<DartsGameViewModel>()
                .AddTransient<EditPlayersViewModel>();

            return services.BuildServiceProvider();
        }

        private void CreateDB(IServiceProvider services)
        {
            using (var serviceScope = services.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<DartsDBContext>();
                context.Database.EnsureCreated();
            }
        }

        /// <summary>
        /// Invoked when the application is launched.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            m_window = new MainWindow();
            RootFrame.NavigationFailed += OnNavigationFailed;
            // Navigate to the first page, configuring the new page
            // by passing required information as a navigation parameter
            RootFrame.Navigate(typeof(CreateGamePage), args.Arguments);
            // Place the frame in the current Window
            m_window.Content = RootFrame;
            // Ensure the MainWindow is active

            m_window.Activate();
        }

        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }
    }
}
