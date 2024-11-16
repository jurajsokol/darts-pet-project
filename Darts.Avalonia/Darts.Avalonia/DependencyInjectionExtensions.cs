using Darts.Avalonia.ViewRouting;
using Darts.Avalonia.Views;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;

namespace Darts.Avalonia
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddPageNavigation(this IServiceCollection services)
        {
            return services
                .AddSingleton<PageNavigation>(s => new PageNavigation(s.GetRequiredService<MainView>().NavigationPanel, s))
                .AddSingleton<IPageNavigation, PageNavigation>(s => s.GetRequiredService<PageNavigation>());
        }
    }
}
