using LaundrySystem.Repositories.Abstract;
using Microsoft.Extensions.Configuration;

namespace LaundrySystem.Repositories.Concrete
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected readonly string _connectionString;

        protected RepositoryBase(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public abstract Task<IEnumerable<T>> GetAllAsync();

        public abstract Task<T?> GetByIdAsync(int id);

        public abstract Task CreateAsync(T entity);

        public abstract Task UpdateAsync(T entity);

        public abstract Task DeleteAsync(int id);
    }
}