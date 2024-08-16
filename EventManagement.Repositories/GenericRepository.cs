using Microsoft.EntityFrameworkCore;

namespace EventManagement.Repositories
{
    public class GenericRepository<T> : IDisposable, IGenericRepository<T> where T : class
    {
        private readonly EventManagementContext _dbContext;
        internal DbSet<T> _dbSet;

        public GenericRepository(EventManagementContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        private bool _disposed = false;
        private void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this._disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public async Task<T> AddAsync(T entity)
        {
            _dbSet.Add(entity);
            return entity;
        }

        public IEnumerable<T> GetAllAsync()
        {
            return  _dbSet.ToList();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }
        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<int> CountAsync()
        {
            return await _dbSet.CountAsync();
        }
    }
}
