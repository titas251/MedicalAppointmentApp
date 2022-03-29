using DAL.Data.Models;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
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
