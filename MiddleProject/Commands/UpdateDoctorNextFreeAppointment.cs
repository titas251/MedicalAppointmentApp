using AutoMapper;
using MediatR;
using DAL.Data;
using DAL.Data.Models;
using MiddleProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DAL.Repositories.Interfaces;

namespace MiddleProject.Commands
{
    public class UpdateDoctorNextFreeAppointment
    {
        public class Command : IRequest<CustomResponse>
        {

        }

        public class Handler : IRequestHandler<Command, CustomResponse>
        {
            private readonly IDoctorRepository _doctorRepository;

            public Handler(IDoctorRepository doctorRepository)
            {
                _doctorRepository = doctorRepository;
            }

            public async Task<CustomResponse> Handle(Command request, CancellationToken cancellationToken)
            {
                var response = new CustomResponse();
                var doctors = await _doctorRepository.GetAllWithIncludeAsync();

                try
                {
                    foreach (var doctor in doctors)
                    {
                        doctor.NextFreeAppointmentDate = GetNextFreeAppointment(doctor);
                        _doctorRepository.UpdateWithoutSaving(doctor);
                    }
                    await _doctorRepository.SaveChangesAsync();
                }
                catch (Exception)
                {
                    response.AddError(new CustomError { Error = "Failed", Message = "Failed to update doctors" });
                }

                return response;
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
}
