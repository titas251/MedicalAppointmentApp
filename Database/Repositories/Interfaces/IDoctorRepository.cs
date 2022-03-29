using DAL.Data.Models;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IDoctorRepository : IGenericRepository<Doctor>
    {
        Task<int> GetCountAsync();
        Task DeleteAsync(int doctorId);
        Task<List<Doctor>> GetAllWithPagingAsync(int pageNumber, int pageSize);
        Task<IEnumerable<Doctor>> GetAllWithIncludeAsync();
        Task<List<Doctor>> GetByQueryAsync(string stringQuery, int page, int pageSize);
        Task<int> GetCountByQueryAsync(string stringQuery);
        Task<Doctor> GetByIdWithIncludeAsync(int doctorId);
        Task<Doctor> GetByIdAsync(int doctorId);
        void UpdateWithoutSaving(Doctor doctor);
    }
}
