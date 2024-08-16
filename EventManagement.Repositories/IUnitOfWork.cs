namespace EventManagement.Repositories
{
    public interface IUnitOfWork
    {
        IGenericRepository<T> GenericRepository<T>() where T : class;
        Task SaveAsync();
    }
}
