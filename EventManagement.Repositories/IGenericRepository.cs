namespace EventManagement.Repositories
{
    public interface IGenericRepository<T> : IDisposable where T : class
    {
        void Add(T entity);
        Task<T> AddAsync(T entity);
        IEnumerable<T> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<int> CountAsync();
    }
}
