using Darts.DAL.Entities;
using Darts.DAL.Repositories;

namespace Darts.DAL
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Player> Players { get; }
        Task CompleteAsync();
    }
}
