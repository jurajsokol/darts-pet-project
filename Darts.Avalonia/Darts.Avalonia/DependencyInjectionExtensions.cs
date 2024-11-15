using Darts.Avalonia.ViewRouting;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;

namespace Darts.Avalonia
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddPageNavigation(this IServiceCollection services)
        {
            return services.AddSingleton<RoutingState>()
                .AddSingleton<RoutingStateWrapper>()
                .AddSingleton<PageNavigation>()
                .AddSingleton<IPageNavigation, PageNavigation>(s => s.GetRequiredService<PageNavigation>());
        }
    }
}
