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
    public class DeleteAppointmentId
    {
        public class Command : IRequest<CustomResponse>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, CustomResponse>
        {
            private readonly IAppointmentRepository _appointmentRepository;

            public Handler(IAppointmentRepository appointmentRepository)
            {
                _appointmentRepository = appointmentRepository;
            }

            public async Task<CustomResponse> Handle(Command request, CancellationToken cancellationToken)
            {
                var response = new CustomResponse();

                try
                {
                    await _appointmentRepository.DeleteAsync(request.Id);
                    await _appointmentRepository.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    response.AddError(new CustomError { Error = "Failed", Message = "Failed to delete appointment" });
                }

                return response;
            }
        }
    }
}
