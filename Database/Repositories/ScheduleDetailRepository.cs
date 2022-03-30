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
    public class ScheduleDetailRepository : GenericRepository<ScheduleDetail>, IScheduleDetailRepository
    {
        public ScheduleDetailRepository(ApplicationDbContext context) : base(context)
        {
        }

        public void AddWithoutSaving(ScheduleDetail scheduleDetail)
        {
            _context.ScheduleDetails.Add(scheduleDetail);
        }

        public async Task<List<ScheduleDetail>> GetDoctorScheduleAsync(int doctorId, string address, DateTime currentDate)
        {
            return await _context.ScheduleDetails.Include(s => s.Schedule).ThenInclude(s => s.Institution)
                    .Where(s => s.Schedule.DoctorId.Equals(doctorId)
                    && s.Schedule.Institution.Address.Equals(address)
                    && s.Schedule.StartDate <= currentDate.AddDays(7)
                    && s.Schedule.EndDate > currentDate)
                    .ToListAsync();
        }
    }
}
