using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class, new()
    {
        IEnumerable<TEntity> GetAll();

        Task AddAsync(TEntity entity);

        Task UpdateAsync(TEntity entity);
        Task SaveChangesAsync();
    }
}
