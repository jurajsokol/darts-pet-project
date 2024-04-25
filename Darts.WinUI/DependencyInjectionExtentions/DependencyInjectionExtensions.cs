using Darts.WinUI.DependencyInjectionExtentions.Fides.Latis3.FXPSimulator.DependencyInjectionExtension;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Darts.WinUI.DependencyInjectionExtentions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddFactory<T>(this IServiceCollection services)
            where T : class
        {
            services.AddTransient<T>();
            services.AddSingleton<Func<T>>(x => () => x.GetService<T>()!);
            services.AddSingleton<IAbstractFactory<T>, AbstractFactory<T>>();
            return services;
        }

        public static IServiceCollection AddFactory<TService, TImplementation>(this IServiceCollection services)
            where TService : class
            where TImplementation : class, TService
        {
            services.AddTransient<TService, TImplementation>();
            services.AddSingleton<Func<TService>>(x => () => x.GetService<TService>()!);
            services.AddSingleton<IAbstractFactory<TService>, AbstractFactory<TService>>();
            return services;
        }

        public static IServiceCollection AddFactory<TIn, TOut>(this IServiceCollection services, Func<TIn, TOut> function)
            where TIn: new ()
        {
            services.AddSingleton<Func<TIn, TOut>>(x => function);
            services.AddSingleton<IAbstractFactory<TIn, TOut>, AbstractFactory<TIn, TOut>>();
            return services;
        }
    }

}
