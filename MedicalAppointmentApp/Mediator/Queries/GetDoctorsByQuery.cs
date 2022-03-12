using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MediatR;
using MedicalAppointmentApp.Data;
using MedicalAppointmentApp.Models;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MedicalAppointmentApp.Data.Models;

namespace MedicalAppointmentApp.Mediator.Queries
{
    public class GetDoctorsByQuery
    {
        public class Query : IRequest<List<GetDoctorsWithNextAppointment>>
        {
            public Query(string stringQuery)
            {
                StringQuery = stringQuery;
            }
            public string StringQuery { get; }
        }

        public class Handler : IRequestHandler<Query, List<GetDoctorsWithNextAppointment>>
        {
            private readonly ApplicationDbContext _context;
            private readonly IMapper _mapper;

            public Handler(ApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<List<GetDoctorsWithNextAppointment>> Handle(Query request, CancellationToken cancellationToken)
            {
                var doctors = await _context.Doctors
                    .Include(doctor => doctor.MedicalSpeciality)
                    .Include(doctor => doctor.Appointments)
                    .Include(doctor => doctor.Schedules)
                        .ThenInclude(schedule => schedule.ScheduleDetails)
                    .Include(doctor => doctor.Schedules)
                        .ThenInclude(schedule => schedule.Institution)
                    .Where(doctor => doctor.FirstName.Contains(request.StringQuery)
                    || doctor.LastName.Contains(request.StringQuery)
                    || (doctor.FirstName + " " + doctor.LastName).Contains(request.StringQuery)
                    || doctor.MedicalSpeciality.Name.Contains(request.StringQuery)
                    || doctor.Schedules.Any(c => c.Institution.Name.Contains(request.StringQuery)))
                    .ToListAsync();

                //getting sorted doctors
                var doctorsWithNextAppointment = GetDoctorWithNextAppointment(doctors);

                return doctorsWithNextAppointment;
            }

            private List<GetDoctorsWithNextAppointment> GetDoctorWithNextAppointment(List<Doctor> doctors) 
            {
                var doctorsWithNextAppointment = new List<GetDoctorsWithNextAppointment>();
                var currentDate = DateTime.Today;

                foreach (var doctor in doctors)
                {
                    //storing all closes dates from each schedule
                    var closestDates = new List<DateTime>();
                    var closestDate = DateTime.MaxValue;

                    //getting all valid schedules
                    var filteredSchedules = doctor.Schedules
                        .Where(s => (currentDate >= s.StartDate && currentDate <= s.EndDate));

                    foreach (var schedule in filteredSchedules) 
                    {
                        //select only days in schedule details
                        var scheduleDetailsDays = schedule.ScheduleDetails
                            .Select(s => s.Day)
                            .ToList();

                        //need to check if day is valid
                        for (var day = currentDate; day.Date <= schedule.EndDate; day = day.AddDays(1))
                        {
                            if (scheduleDetailsDays.Contains(day.DayOfWeek)) 
                            {
                                //get current day schedule details
                                var currentDetail = schedule.ScheduleDetails
                                    .Where(s => s.Day == day.DayOfWeek)
                                    .First();

                                TimeSpan startTime = TimeSpan.Parse(currentDetail.StartDateTime);
                                TimeSpan endTime = TimeSpan.Parse(currentDetail.EndDateTime);

                                var startDateTime = day.Date + startTime;
                                var startEndTime = day.Date + endTime;
                                
                                //check for free appointments
                                for (var currentStartDT = startDateTime; currentStartDT < startEndTime; currentStartDT = currentStartDT.AddMinutes(30))
                                {
                                    var appointment = doctor.Appointments
                                        .Where(appointment => appointment.StartDateTime == currentStartDT)
                                        .FirstOrDefault();

                                    if (appointment == null) {
                                        closestDate = currentStartDT;
                                        break;
                                    }
                                }
                            }

                            //add to closest dates
                            if (closestDate != DateTime.MaxValue)
                            {
                                closestDates.Add(closestDate);
                                closestDate = DateTime.MaxValue;
                                break;
                            }
                        }
                    }

                    //get closest date in all schedules
                    var closestDateForFreeAppoitment = closestDates
                        .OrderBy(s => s).FirstOrDefault();

                    var doctorWithNextAppointment = new GetDoctorsWithNextAppointment
                    {
                        Doctor = doctor,
                        NextFreeAppointmentDate = closestDateForFreeAppoitment
                    };

                    doctorsWithNextAppointment.Add(doctorWithNextAppointment);
                }

                //order by closest free appointment date
                doctorsWithNextAppointment = doctorsWithNextAppointment
                    .OrderBy(s => s.NextFreeAppointmentDate)
                    .ToList();

                return doctorsWithNextAppointment;
            }
        }
    }
}
