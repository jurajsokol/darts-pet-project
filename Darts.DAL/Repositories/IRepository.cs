namespace Darts.DAL.Repositories
{
    public interface IRepository<T>
    {
        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetAll();
        Task<T> Add(T entity);
        void Update(T entity);
        Task Delete(int id);
        void Delete(T entity);
    }
}
