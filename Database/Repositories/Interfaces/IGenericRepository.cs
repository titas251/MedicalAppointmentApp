using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class, new()
    {
        IEnumerable<TEntity> GetAll();

        Task AddAsync(TEntity entity);

        Task UpdateAsync(TEntity entity);
        Task SaveChangesAsync();
    }
}
