namespace Darts.DAL.Repositories
{
    public interface IRepository<T>
    {
        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetAll();
        public IAsyncEnumerable<T> GetAsyncEnumerable();
        Task<T> Add(T entity);
        void Update(T entity);
        Task Delete(int id);
        void Delete(T entity);
    }
}
