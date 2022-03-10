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
        public class Query : IRequest<List<GetDoctorModel>>
        {
            public Query(string stringQuery)
            {
                StringQuery = stringQuery;
            }
            public string StringQuery { get; }
        }

        public class Handler : IRequestHandler<Query, List<GetDoctorModel>>
        {
            private readonly ApplicationDbContext _context;
            private readonly IMapper _mapper;

            public Handler(ApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<List<GetDoctorModel>> Handle(Query request, CancellationToken cancellationToken)
            {
                var doctorsViewModel = new List<GetDoctorModel>();

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
                var doctorsWith = GetDoctorWithNextAppointment(doctors);

                foreach (var doctor in doctors)
                {
                    var viewModel = _mapper.Map<GetDoctorModel>(doctor);
                    doctorsViewModel.Add(viewModel);
                }

                return doctorsViewModel;
            }

            private List<GetDoctorScheduleModel> GetDoctorWithNextAppointment(List<Doctor> doctors) 
            {
                var doctorsWithAppointments = new List<GetDoctorScheduleModel>();
                var currentDate = DateTime.Now;

                foreach (var doctor in doctors)
                {
                    //storing all closes dates from each schedule
                    var closestDatesWithWorkingTime = new List<ClosestDateWithWorkingTime>();

                    //getting all valid schedules
                    var filteredSchedules = doctor.Schedules
                        .Where(s => (currentDate >= s.StartDate && currentDate <= s.EndDate));

                    foreach (var schedule in filteredSchedules) 
                    {
                        //getting closest day
                        var closestDay = schedule.ScheduleDetails
                            //.Where(p => DateTime.Now.AddDays((double)p.Day) >= DateTime.Parse(p.EndDateTime))
                            .OrderBy(s => Math.Abs((currentDate.DayOfWeek-7) - s.Day)).First();

                        //getting datetime
                        var closestDate = currentDate.AddDays(Math.Abs(currentDate.DayOfWeek - (closestDay.Day+7)));
                        
                        //check if closes date is valid
                        if (closestDate <= schedule.EndDate) {
                            var closestDateWithWorkingTime = new ClosestDateWithWorkingTime
                            {
                                ClosestDate = closestDate,
                                EndingDateTime = closestDay.EndDateTime,
                                StartingDateTime = closestDay.StartDateTime
                            };

                            //add to closest dates
                            closestDatesWithWorkingTime.Add(closestDateWithWorkingTime);
                        }
                    }

                    //get closest date in all schedules
                    var closestDateForFreeAppoitment = closestDatesWithWorkingTime
                        .OrderBy(s => s.ClosestDate).FirstOrDefault();

                    var doctorScheduleModel = new GetDoctorScheduleModel 
                    {
                        Doctor = doctor,
                        NextFreeAppointmentDate = closestDateForFreeAppoitment
                    };

                    doctorsWithAppointments.Add(doctorScheduleModel);
                }

                //order by closest free appointment date
                doctorsWithAppointments = doctorsWithAppointments
                    .OrderBy(s => s.NextFreeAppointmentDate.ClosestDate)
                    .ToList();

                return doctorsWithAppointments;
            }
        }
    }
}
