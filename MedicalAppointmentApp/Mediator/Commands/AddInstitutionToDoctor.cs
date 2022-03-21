using AutoMapper;
using MediatR;
using MedicalAppointmentApp.Data;
using MedicalAppointmentApp.Data.Models;
using MedicalAppointmentApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MedicalAppointmentApp.Mediator.Commands
{
    public class AddInstitutionToDoctor
    {
        public class Command : IRequest<CustomResponse>
        {
            public int DoctorId { get; set; }
            public int InstitutionId { get; set; }
            public List<ScheduleDetailModel> scheduleDetails { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
        }

        public class Handler : IRequestHandler<Command, CustomResponse>
        {
            private readonly ApplicationDbContext _context;
            private readonly IMapper _mapper;
            public Handler(ApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<CustomResponse> Handle(Command request, CancellationToken cancellationToken)
            {
                var response = new CustomResponse();

                var doctor = await _context.Doctors.FindAsync(request.DoctorId);
                var institution = await _context.Institutions.FindAsync(request.InstitutionId);

                var schedule = new Schedule
                {
                    Institution = institution,
                    Doctor = doctor,
                    StartDate = request.StartDate,
                    EndDate = request.EndDate
                };
                foreach (var scheduleDetail in request.scheduleDetails)
                {
                    if (scheduleDetail.isWorking)
                    {
                        var scheduleDetailModel = _mapper.Map<ScheduleDetail>(scheduleDetail);
                        scheduleDetailModel.Schedule = schedule;
                        _context.ScheduleDetails.Add(scheduleDetailModel);
                    }
                }

                try
                {
                    doctor.Schedules.Add(schedule);

                    //add next free appoitment date
                    doctor.NextFreeAppointmentDate = GetNextFreeAppointment(doctor);

                    _context.Doctors.Update(doctor);
                    _context.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    response.AddError(new CustomError { Error = "Failed", Message = "Failed to add institution to doctor" });
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
