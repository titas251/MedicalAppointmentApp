using MedicalAppointmentApp.Data;
using MedicalAppointmentApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgroundJobs.UpdateAppointment
{
    public class UpdateNextFreeAppointments : IJob
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UpdateNextFreeAppointments> _logger;

        public UpdateNextFreeAppointments(ApplicationDbContext context, ILogger<UpdateNextFreeAppointments> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var doctors = await _context.Doctors
                .Include(a => a.Schedules)
                .ThenInclude(a => a.ScheduleDetails)
                .ToListAsync();

            try
            {
                foreach (var doctor in doctors)
                {
                    doctor.NextFreeAppointmentDate = GetNextFreeAppointment(doctor);
                    _context.Doctors.Update(doctor);
                }
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                _logger.LogError("Failed to update upcoming free appoinments");
                return;
            }

            _logger.LogInformation("Successfully updated all upcoming free appointments");
        }

        private DateTime? GetNextFreeAppointment(Doctor doctor)
        {
            //adding one day to start checking from tomorrow
            var currentDate = DateTime.Today.AddDays(1);
            var filteredScheduleDetails = doctor.Schedules
                    .Where(s => currentDate <= s.EndDate)
                    .SelectMany(s => s.ScheduleDetails)
                    .OrderByDescending(s => s.Schedule.EndDate);

            if (filteredScheduleDetails.Any())
            {
                var maxEndDateTime = filteredScheduleDetails.Select(s => s.Schedule.EndDate).First();

                for (var day = currentDate; day.Date <= maxEndDateTime; day = day.AddDays(1))
                {
                    if (filteredScheduleDetails.Any(s => s.Day == day.DayOfWeek && s.Schedule.EndDate.Date >= day.Date
                        && s.Schedule.StartDate.Date <= day.Date))
                    {
                        var filteredScheduleDetail = filteredScheduleDetails
                            .Where(s => s.Day == day.DayOfWeek && s.Schedule.EndDate.Date >= day.Date
                                && s.Schedule.StartDate.Date <= day.Date)
                            .First();

                        TimeSpan startTime = TimeSpan.Parse(filteredScheduleDetail.StartDateTime);
                        TimeSpan endTime = TimeSpan.Parse(filteredScheduleDetail.EndDateTime);

                        var startDateTime = day.Date + startTime;
                        var startEndTime = day.Date + endTime;

                        for (var currentStartDT = startDateTime; currentStartDT < startEndTime; currentStartDT = currentStartDT.AddMinutes(30))
                        {
                            var appointment = doctor.Appointments
                                .Where(appointment => appointment.StartDateTime == currentStartDT)
                                .FirstOrDefault();

                            if (appointment == null)
                            {
                                return currentStartDT;
                            }
                        }
                    }
                }
                return null;
            }
            return null;
        }
    }
}
