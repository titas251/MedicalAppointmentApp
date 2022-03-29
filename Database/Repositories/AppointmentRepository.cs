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
    public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task DeleteAsync(int appointmentId)
        {
            _context.Appointments.Remove(await _context.Appointments.FindAsync(appointmentId));
        }

        public async Task<List<Appointment>> GetAllWithPagingAsync(int pageNumber, int pageSize)
        {
            return await _context.Appointments
                    .Include(a => a.Doctor)
                    .Include(a => a.ApplicationUser)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
        }

        public async Task<List<Appointment>> GetAppointmentsByDoctorIdAsync(int doctorId)
        {
            return await _context.Appointments.Include(a => a.Doctor)
                    .Where(a => a.DoctorId.Equals(doctorId))
                    .OrderBy(a => a.StartDateTime)
                    .ToListAsync();
        }

        public async Task<bool> GetAppointmentsByUserIdAndAppointmentIdAsync(string userId, int appointmentId)
        {
            return await _context.Appointments
                    .AnyAsync(a => a.ApplicationUserId.Equals(userId) && a.AppointmentId.Equals(appointmentId));
        }

        public async Task<List<Appointment>> GetAppointmentsByUserIdAsync(string userId, int pageNumber, int pageSize)
        {
            return await _context.Appointments
                    .Include(a => a.Doctor)
                    .Where(a => a.ApplicationUserId.Equals(userId))
                    .OrderBy(a => a.StartDateTime)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Appointments
                    .CountAsync();
        }

        public async Task<int> GetCountByUserIdAsync(string userId)
        {
            return await _context.Appointments
                    .Where(a => a.ApplicationUserId.Equals(userId))
                    .CountAsync();
        }
    }
}
