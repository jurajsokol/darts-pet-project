using Darts.DAL.Entities;
using Darts.DAL.Repositories;

namespace Darts.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DartsDBContext dbContext;

        public IRepository<Player> Players { get; }

        public UnitOfWork(DartsDBContext dbContext)
        {
            this.dbContext = dbContext;
            Players = new PlayersRepository<Player>(dbContext);
        }

        public async Task CompleteAsync()
        {
            await dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }
    }
}
