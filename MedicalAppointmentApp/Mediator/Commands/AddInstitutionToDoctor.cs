using AutoMapper;
using MediatR;
using MedicalAppointmentApp.Data;
using MedicalAppointmentApp.Data.Models;
using MedicalAppointmentApp.Models;
using System;
using System.Collections.Generic;
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
                doctor.Schedules.Add(schedule);
                _context.Doctors.Update(doctor);

                //save changes and check if success
                var success = await _context.SaveChangesAsync() > 0;
                if (!success)
                {
                    response.AddError(new CustomError { Error = "Failed", Message = "Failed to add institution to doctor" });
                }

                return response;
            }
        }
    }
}
