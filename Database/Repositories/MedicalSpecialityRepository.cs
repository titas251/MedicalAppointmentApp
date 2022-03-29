using DAL.Data;
using DAL.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class MedicalSpecialityRepository : IMedicalSpecialityRepository
    {
        private readonly ApplicationDbContext _context;
        public MedicalSpecialityRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task DeleteAsync(int specialityId)
        {
            _context.MedicalSpecialities.Remove(await _context.MedicalSpecialities.FindAsync(specialityId));
        }

        public async Task<IEnumerable<MedicalSpeciality>> GetAllAsync(int pageNumber, int pageSize)
        {
            return await _context.MedicalSpecialities
                    .OrderBy(specialty => specialty.Name)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.MedicalSpecialities.CountAsync();
        }

        public async Task AddAsync(MedicalSpeciality medicalSpeciality)
        {
            await _context.MedicalSpecialities.AddAsync(medicalSpeciality);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
