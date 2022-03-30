using AutoMapper;
using DAL.Data.Models;
using DAL.Repositories.Interfaces;
using EntityFramework.Exceptions.Common;
using MediatR;
using MiddleProject.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

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
            private readonly IGenericRepository<Appointment> _genericRepository;
            private readonly IMapper _mapper;

            public Handler(IGenericRepository<Appointment> genericRepository, IMapper mapper)
            {
                _genericRepository = genericRepository;
                _mapper = mapper;
            }

            public async Task<CustomResponse> Handle(Command request, CancellationToken cancellationToken)
            {
                var response = new CustomResponse();
                var appointment = _mapper.Map<Appointment>(request.AppointmentModel);

                try
                {
                    await _genericRepository.AddAsync(appointment);
                }
                catch (ReferenceConstraintException)
                {
                    response.AddError(new CustomError { Error = "Failed", Message = "Doctor or institution doesn't exist" });
                }
                catch (Exception)
                {
                    response.AddError(new CustomError { Error = "Failed", Message = "Failed to create appointment" });
                }

                return response;
            }
        }
    }
}
