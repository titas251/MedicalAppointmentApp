using AutoMapper;
using EntityFramework.Exceptions.Common;
using MediatR;
using DAL.Data;
using DAL.Data.Models;
using MiddleProject.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using DAL.Repositories.Interfaces;

namespace MiddleProject.Commands
{
    public class CreateAppointment
    {
        public class Command : IRequest<CustomResponse>
        {
            public CreateAppointmentModel AppointmentModel { get; set; }
        }

        public class Handler : IRequestHandler<Command, CustomResponse>
        {
            private readonly IAppointmentRepository _appointmentRepository;
            private readonly IMapper _mapper;

            public Handler(IAppointmentRepository appointmentRepository, IMapper mapper)
            {
                _appointmentRepository = appointmentRepository;
                _mapper = mapper;
            }

            public async Task<CustomResponse> Handle(Command request, CancellationToken cancellationToken)
            {
                var response = new CustomResponse();
                var appointment = _mapper.Map<Appointment>(request.AppointmentModel);

                try
                {
                    await _appointmentRepository.AddAsync(appointment);
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
