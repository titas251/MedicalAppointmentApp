using DAL.Data.Models;
using DAL.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public interface IMedicalSpecialityRepository : IGenericRepository<MedicalSpeciality>
    {
        Task<IEnumerable<MedicalSpeciality>> GetAllWithPagingAsync(int pageNumber, int pageSize);
        Task<int> GetCountAsync();
        Task DeleteAsync(int specialityId);
    }
}
