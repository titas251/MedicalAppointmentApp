using DAL.Data.Models;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public interface IMedicalSpecialityRepository : IRepository<MedicalSpeciality>
    {
        Task<IEnumerable<MedicalSpeciality>> GetAllWithPagingAsync(int pageNumber, int pageSize);
        Task<int> GetCountAsync();
        Task DeleteAsync(int specialityId);
    }
}
