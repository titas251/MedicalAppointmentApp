using DAL.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IInstitutionRepository : IGenericRepository<Institution>
    {
        Task<int> GetCountAsync();
        Task DeleteAsync(int institutionId);
        Task<IEnumerable<Institution>> GetAllWithPagingAsync(int pageNumber, int pageSize);
        Task<IEnumerable<Institution>> GetAllWithInclude();
        Task<Institution> GetByIdAsync(int institutionId);
    }
}
