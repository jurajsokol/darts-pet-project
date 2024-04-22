using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Darts.DAL
{
    public static class DALFactory
    {
        public static void AddDatabase(this ServiceCollection services, string dbPath)
        {
            services.AddDbContext<DartsDBContext>(options => options.UseSqlite(($"Data Source={dbPath}")));
            services.AddSingleton<IUnitOfWork, UnitOfWork>();
        }
    }
}
