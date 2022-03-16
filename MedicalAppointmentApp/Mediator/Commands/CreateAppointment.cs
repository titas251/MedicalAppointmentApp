using AutoMapper;
using EntityFramework.Exceptions.Common;
using MediatR;
using MedicalAppointmentApp.Data;
using MedicalAppointmentApp.Data.Models;
using MedicalAppointmentApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MedicalAppointmentApp.Mediator.Commands
{
    public class CreateAppointment
    {
        public class Command : IRequest<CustomResponse>
        {
            public CreateAppointmentModel AppointmentModel { get; set; }
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
                var appointment = _mapper.Map<Appointment>(request.AppointmentModel);

                try
                {
                    await _context.Appointments.AddAsync(appointment);
                    await _context.SaveChangesAsync();
                }
                catch (ReferenceConstraintException)
                {
                    response.AddError(new CustomError { Error = "Failed", Message = "Doctor or institution doesn't exist" });
                }
                catch (DbUpdateException)
                {
                    response.AddError(new CustomError { Error = "Failed", Message = "Failed to create appointment" });
                }

                return response;
            }
        }
    }
}
