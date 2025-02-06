using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Darts.DAL
{
    public static class DALFactory
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, string dbPath)
        {
            services.AddDbContext<DartsDBContext>(options => options.UseSqlite(($"Data Source={dbPath}")));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
