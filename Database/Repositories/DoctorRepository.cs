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
    public class DoctorRepository : GenericRepository<Doctor>, IDoctorRepository
    {
        public DoctorRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task DeleteAsync(int doctorId)
        {
            _context.Doctors.Remove(await _context.Doctors.FindAsync(doctorId));
        }

        public async Task<List<Doctor>> GetAllWithPagingAsync(int pageNumber, int pageSize)
        {
            return await _context.Doctors
                    .Include(doctor => doctor.MedicalSpeciality)
                    .Include(doctor => doctor.Schedules)
                        .ThenInclude(schedule => schedule.Institution)
                    .OrderBy(doctor => doctor.LastName)
                        .ThenBy(doctor => doctor.FirstName)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Doctors
                    .CountAsync();
        }

        public async Task<IEnumerable<Doctor>> GetAllWithIncludeAsync()
        {
            return await _context.Doctors
                    .Include(a => a.Schedules)
                    .ThenInclude(a => a.ScheduleDetails)
                    .ToListAsync();
        }

        public async Task<List<Doctor>> GetByQueryAsync(string stringQuery, int page, int pageSize)
        {
            return await _context.Doctors
                    .Include(doctor => doctor.MedicalSpeciality)
                    .Include(doctor => doctor.Appointments)
                    .Include(doctor => doctor.Schedules)
                        .ThenInclude(schedule => schedule.ScheduleDetails)
                    .Include(doctor => doctor.Schedules)
                        .ThenInclude(schedule => schedule.Institution)
                    .Where(doctor => doctor.FirstName.Contains(stringQuery)
                    || doctor.LastName.Contains(stringQuery)
                    || (doctor.FirstName + " " + doctor.LastName).Contains(stringQuery)
                    || doctor.MedicalSpeciality.Name.Contains(stringQuery)
                    || doctor.Schedules.Any(c => c.Institution.Name.Contains(stringQuery)))
                    .OrderByDescending(doctor => doctor.NextFreeAppointmentDate.HasValue)
                        .ThenBy(doctor => doctor.NextFreeAppointmentDate)
                        .ThenBy(doctor => doctor.LastName)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
        }

        public async Task<int> GetCountByQueryAsync(string stringQuery)
        {
            return await _context.Doctors
                    .Where(doctor => doctor.FirstName.Contains(stringQuery)
                    || doctor.LastName.Contains(stringQuery)
                    || (doctor.FirstName + " " + doctor.LastName).Contains(stringQuery)
                    || doctor.MedicalSpeciality.Name.Contains(stringQuery)
                    || doctor.Schedules.Any(c => c.Institution.Name.Contains(stringQuery)))
                    .CountAsync();
        }

        public async Task<Doctor> GetByIdWithIncludeAsync(int doctorId)
        {
            return await _context.Doctors.Include(doctor => doctor.MedicalSpeciality).Include(doctor => doctor.Schedules)
                    .ThenInclude(schedules => schedules.Institution)
                    .Where(doctor => doctor.DoctorId.Equals(doctorId))
                    .OrderBy(doctor => doctor.LastName)
                        .ThenBy(doctor => doctor.FirstName)
                    .FirstOrDefaultAsync();
        }

        public void UpdateWithoutSaving(Doctor doctor)
        {
            _context.Update(doctor);
        }

        public async Task<Doctor> GetByIdAsync(int doctorId)
        {
            return await _context.Doctors.FindAsync(doctorId);
        }
    }
}
