namespace Minimal.Infrastructure.Repositories.Base
{
    public interface IRepository<T>
    {
        public Task<T> PostAsync(T entity);

        public Task<IEnumerable<T>> GetAllAsync();
    }
}
