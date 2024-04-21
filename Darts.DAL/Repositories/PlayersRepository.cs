using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Darts.DAL.Repositories
{
    public class PlayersRepository<T> : IRepository<T> where T : class
    {
        private readonly DbContext context;
        private readonly DbSet<T> dbSet;

        public PlayersRepository(DbContext context)
        {
            this.context = context;
            dbSet = context.Set<T>();
        }

        public async Task Add(T entity)
        {
            dbSet.AddAsync(entity);
        }

        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await dbSet.ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public void Update(T entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }
    }
}
