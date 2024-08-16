using Microsoft.EntityFrameworkCore;

namespace EventManagement.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly EventManagementContext _dbContext;

        public UnitOfWork(EventManagementContext dbContext)
        {
            _dbContext = dbContext;
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

        public IGenericRepository<T> GenericRepository<T>() where T : class
        {
            IGenericRepository<T> repository = new GenericRepository<T>(_dbContext);
            return repository;
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
