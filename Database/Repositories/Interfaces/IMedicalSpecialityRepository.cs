using DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public interface IMedicalSpecialityRepository
    {
        Task<IEnumerable<MedicalSpeciality>> GetAllAsync(int pageNumber, int pageSize);
        //MedicalSpeciality GetById();
        Task<int> GetCountAsync();
        Task AddAsync(MedicalSpeciality MedicalSpeciality);
        //void Update(MedicalSpeciality MedicalSpeciality);
        Task DeleteAsync(int specialityId);
        Task SaveChangesAsync();
    }
}
