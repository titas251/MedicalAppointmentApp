using DAL.Data;
using DAL.Data.Models;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DAL.Repositories
{
    public class InstitutionRepository : GenericRepository<Institution>, IInstitutionRepository
    {
        public InstitutionRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task DeleteAsync(int institutionId)
        {
            _context.Institutions.Remove(await _context.Institutions.FindAsync(institutionId));
        }

        public async Task<IEnumerable<Institution>> GetAllWithPagingAsync(int pageNumber, int pageSize)
        {
            return await _context.Institutions
                    .Include(institution => institution.Schedules)
                        .ThenInclude(schedule => schedule.Doctor)
                    .OrderBy(institution => institution.InstitutionId)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
        }

        public async Task<IEnumerable<Institution>> GetAllWithInclude()
        {
            return await _context.Institutions
                    .Include(institution => institution.Schedules)
                    .ThenInclude(schedule => schedule.Doctor)
                    .OrderBy(institution => institution.Address)
                    .ToListAsync();
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Institutions.CountAsync();
        }

        public async Task<Institution> GetByIdAsync(int institutionId)
        {
            return await _context.Institutions.FindAsync(institutionId);
        }
    }
}
