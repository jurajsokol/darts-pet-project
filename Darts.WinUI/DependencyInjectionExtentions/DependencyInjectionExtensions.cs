using Darts.WinUI.DependencyInjectionExtentions.Fides.Latis3.FXPSimulator.DependencyInjectionExtension;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Darts.WinUI.DependencyInjectionExtentions
{
    public static class DependencyInjectionExtensions
    {
        public static void AddFactory<T>(this IServiceCollection services)
            where T : class
        {
            services.AddTransient<T>();
            services.AddSingleton<Func<T>>(x => () => x.GetService<T>()!);
            services.AddSingleton<IAbstractFactory<T>, AbstractFactory<T>>();
        }

        public static void AddFactory<TService, TImplementation>(this IServiceCollection services)
            where TService : class
            where TImplementation : class, TService
        {
            services.AddTransient<TService, TImplementation>();
            services.AddSingleton<Func<TService>>(x => () => x.GetService<TService>()!);
            services.AddSingleton<IAbstractFactory<TService>, AbstractFactory<TService>>();
        }

        public static void AddFactory<TIn, TOut>(this IServiceCollection services, Func<TIn, TOut> function)
            where TIn: new ()
        {
            services.AddSingleton<Func<TIn, TOut>>(x => function);
            services.AddSingleton<IAbstractFactory<TIn, TOut>, AbstractFactory<TIn, TOut>>();
        }
    }

}
