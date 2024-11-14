using Microsoft.Extensions.DependencyInjection;
using Darts.DAL;
using Darts.Games.Games;
using Darts.MVVM.ViewModels;

namespace Darts.MVVM;

public static class MVVMFactory
{
    public static IServiceCollection AddViewModels(this IServiceCollection services, string dbPath)
    {
        return services.AddDatabase(dbPath)
            .AddSingleton<IDartGameFactory, DartGameFactory>()
            .AddTransient<DartsGameViewModel>()
            .AddTransient<EditPlayersViewModel>()
            .AddTransient<CreateGameViewModel>();
    }
}
